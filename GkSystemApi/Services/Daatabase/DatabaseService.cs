using AutoMapper;
using gk_system_api.Entities;
using gk_system_api.Models;
using gk_system_api.Services.Interfaces;
using gk_system_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace gk_system_api.Services
{
    public class DatabaseService : IDatabaseService
    {

        private readonly GkSystemDbContext _db;
        private readonly ILogger _log;
        private readonly IMapper _mapper;

        public List<OfferViewModel> OffersMapped
        {
            get => _mapper.Map<List<OfferViewModel>>(_db.Offers
                    .Include(offer => offer.OfferPlans)
                    .Include(offer => offer.OfferParams)
                    .Include("OfferPlans.OfferPlanParams")
                    .Include(offer => offer.OfferVisualisations)
                    .ToList());
        }
        public List<RealisationViewModel> RealisationsMapped
        {
            get => _mapper.Map<List<RealisationViewModel>>(
                    _db.Realisations.Include(x => x.RealisationImages)
                ).OrderByDescending(x => x.CratedAt).ToList();
        }

        public DatabaseService(GkSystemDbContext db, ILogger<DatabaseService> log, IMapper mapper)
        {
            _db = db;
            _log = log;
            _mapper = mapper;
        }

        public bool SaveModel<T>(T model)
        {
            try
            {
                var dbModel = (T)Convert.ChangeType(model, typeof(T));
                _db.Add(dbModel);
                _db.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }
        public bool UpdateModel<T>(T model)
        {
            try
            {
                var dbModel = (T)Convert.ChangeType(model, typeof(T));
                _db.Entry(dbModel).CurrentValues.SetValues(dbModel);
                _db.SaveChanges();
                
                return true;
            }
            catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public User? GetUserByEmail(string username, string email)
        {
            try
            {
                return _db.Users.FirstOrDefault(x => x.Username == username && x.Email == email);
            }
            catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }        
        
        public User? GetUserByPassword(string username, string password)
        {
            try
            {
                return _db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            }
            catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }        
        public User? GetUserByLogin(string login)
        {
            try
            {
                return _db.Users.FirstOrDefault(x => x.Username == login);
            }
            catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }

        public void AddVisitor(VisitorViewModel model)
        {
            try
            {
                _db.Visitors.Add(_mapper.Map<Visitor>(model));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
            }
        }

        public IEnumerable<Visitor>? GetAllVisitors()
        {
            try
            {
                return _db.Visitors.Include(x => x.Localisation);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }

        public Dictionary<string, GeneralConfigViewModel>? GetGeneralConfig(ConfigGroup group = ConfigGroup.Undefined)
        {
            try
            {
                if (group == ConfigGroup.Undefined)
                {
                    var configsMapped = _mapper.Map<List<GeneralConfigViewModel>>(_db.GeneralConfig.ToList());
                    return configsMapped.ToDictionary(x => x.Name, y => y);
                }
                var configMapped = _mapper.Map<List<GeneralConfigViewModel>>(
                    _db.GeneralConfig.Where(
                        x => x.ConfigGroup == group
                    )
                ).ToDictionary(x => x.Name, y => y);

                return configMapped;
            }
            catch(Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }

        public string[]? GetTopVisitors()
        {
            try
            {
                return _db.Visitors
                    .Include(x => x.Localisation)
                    .GroupBy(x => x.Localisation.State).ToList()
                    .Select(x => new { Location = x.FirstOrDefault().Localisation, Total = x.Count() })
                    .OrderByDescending(x => x.Total)
                    .Select(x => $"{x.Location.State} - {x.Location.CountryName}, {x.Location.CountryCode}: {x.Total}")
                    .Take(10)
                    .ToArray();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }

        public double GetTotalVisitors()
        {
            try
            {
                return _db.Visitors.Count();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return 0;
            }
        }

        public bool SetGeneralConfig(GeneralConfigViewModel[] configs)
        {
            try
            {
                var configsMapped = _mapper.Map<List<GeneralConfig>>(configs);

                foreach (var config in configsMapped)
                {
                    var cfg = _db.GeneralConfig.FirstOrDefault(x => x.Name == config.Name);
                    if (cfg == null)
                        continue;

                    cfg.Name = config.Name;
                    cfg.Value = config.Value;
                    cfg.Image = config.Image;
                    cfg.ImageType = config.ImageType;
                    _db.GeneralConfig.Update(cfg);
                }

                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public List<object>? GetHomePageConfig()
        {
            try
            {
                var generalMapped = _mapper.Map<List<GeneralConfigViewModel>>(_db.GeneralConfig.ToList());
                var carouselMapped = _mapper.Map<List<CarouselConfigViewModel>>(_db.CarouselConfig.ToList());
                if (generalMapped == null || carouselMapped == null)
                {
                    return null;
                }
                var results = new List<object> { generalMapped.ToDictionary(x => x.Name, y => y), carouselMapped };
                return results;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }

        public List<CarouselConfigViewModel>? GetCarouselConfig(string subPage)
        {
            try
            {
                var config = _db.CarouselConfig.Where(c => c.SubPage == subPage);
                var mapped = _mapper.Map<List<CarouselConfigViewModel>>(config);
                return mapped;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }

        public bool UpdateCarouselConfig(CarouselConfigViewModel[] configs)
        {
            try
            {
                var subPage = configs.FirstOrDefault()?.SubPage;
                var oldConfigs = _db.CarouselConfig.Where(c => c.SubPage == subPage).ToList();
                _db.CarouselConfig.RemoveRange(oldConfigs);
                _db.SaveChanges();

                var carouselConfigMapped = _mapper.Map<List<CarouselConfig>>(configs);

                _db.CarouselConfig.AddRange(carouselConfigMapped);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public List<Email> RemoveEmails(string ids)
        {
            try
            {
                foreach (var id in ids.Split(","))
                {
                    var email = _db.Emails.FirstOrDefault(x => x.Id == int.Parse(id));
                    if (email == null)
                        continue;
                    _db.Emails.Remove(email);
                }
                _db.SaveChanges();
                return _db.Emails.ToList();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return new List<Email>();
            }
        }

        public List<Email> GetEmails()
        {
            try
            {
                return _db.Emails.ToList();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return new List<Email>();
            }
        }

        public bool SetOfferState(bool offerAvailable, int offerId)
        {
            try
            {
                var offer = _db.Offers.FirstOrDefault(x => x.Id == offerId);
                if (offer == null)
                {
                    _log.LogWarning($"Cannot update offer state with given id: {offerId}");
                    return false;
                }

                offer.Available = offerAvailable;
                _db.Offers.Update(offer);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }
        public OfferViewModel? GetOffer(int id)
        {
            try
            {
                var offer = OffersMapped.FirstOrDefault(x => x.Id == id);
                if (offer == null)
                {
                    _log.LogWarning($"Cannot find offer with id: {id}");
                    return null;
                }
                return offer;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }
        public List<OfferViewModel>? GetOffersThumbnails()
        {
            try
            {
                var offers = _db.Offers
                    .Include(offer => offer.OfferVisualisations)
                    .ToList();

                var offersMapped = _mapper.Map<List<OfferViewModel>>(offers);

                return offersMapped;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }
        public bool AddOffer(OfferViewModel offer)
        {
            try
            {
                var mappedOffer = _mapper.Map<Offer>(offer);
                mappedOffer.Available = true;

                SaveModel(mappedOffer);

                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public bool UpdateOffer(int id, OfferViewModel offerModel)
        {
            try
            {
                var offer = _db.Offers
                    .Include(o => o.OfferPlans)
                    .Include(o => o.OfferParams)
                    .Include("OfferPlans.OfferPlanParams")
                    .Include(o => o.OfferVisualisations)
                    .FirstOrDefault(z => z.Id == id);

                if (offer == null)
                {
                    _log.LogWarning($"Cannot update offer with id {id}");
                    return false;
                }

                _db.Remove(offer);

                var offerMapped = _mapper.Map<Offer>(offerModel);
                offerMapped.CreateAt = offer.CreateAt;

                SaveModel(offerMapped);

                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }
        public bool DeleteOffer(int id)
        {
            try
            {
                var offer = _db.Offers
                    .Include(o => o.OfferPlans)
                    .Include(o => o.OfferParams)
                    .Include("OfferPlans.OfferPlanParams")
                    .Include(o => o.OfferVisualisations)
                    .FirstOrDefault(z => z.Id == id);

                if (offer == null)
                {
                    _log.LogWarning($"Cannot delete offer with id {id}");
                    return false;
                }

                _db.Offers.Remove(offer);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public bool SetFavouriteRealisation(int id, bool value)
        {
            try
            {
                var image = _db.RealisationImages.FirstOrDefault(x => x.RealisationImageId == id);
                if (image == null)
                    return false;

                image.IsFavourite = value;
                UpdateModel(image);
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public List<RealisationImageViewModel>? GetFavouriteRealisations()
        {
            try
            {
                var realisations = _db.RealisationImages
                    .Include(x => x.Realisation)
                    .Where(y => y.IsFavourite)
                    .ToList();

                var mapped = _mapper.Map<List<RealisationImageViewModel>>(realisations);
                return mapped;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }

        public bool DeleteRealisationImage(int id)
        {
            try
            {
                var item = _db.RealisationImages.FirstOrDefault(x => x.RealisationImageId.Equals(id));
                if (item == null)
                    return false;

                _db.RealisationImages.Remove(item);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public bool DeleteRealisation(int id)
        {
            try
            {
                var item = _db.Realisations.FirstOrDefault(x => x.Id.Equals(id));
                if (item == null)
                    return false;

                _db.Realisations.Remove(item);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public bool AddRealisation(RealisationViewModel realisation)
        {
            try
            {
                var realisationMapped = _mapper.Map<Realisation>(realisation);
                if (realisation.Id == -1)
                {
                    realisationMapped.CratedAt = DateTime.Now.Ticks;
                    _db.Realisations.Add(realisationMapped);
                    _db.SaveChanges();
                    return true;
                }
                var existing = _db.Realisations.FirstOrDefault(x => x.Id == realisation.Id);
                if (existing == null)
                    return false;

                realisationMapped.CratedAt = existing.CratedAt;

                _db.Realisations.Remove(existing);
                _db.Realisations.Add(realisationMapped);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public List<ReferenceViewModel>? GetReferences(int? limit)
        {
            try
            {
                var references = _mapper.Map<List<ReferenceViewModel>>(_db.References);
                if (limit == null)
                    return references;
                return references.OrderByDescending(x => x).Take(limit ?? 0).ToList();
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return null;
            }
        }

        public bool DeleteReference(int id)
        {
            try
            {
                var reference = _db.References.FirstOrDefault(x => x.Id == id);
                if (reference == null)
                    return false;
                _db.References.Remove(reference);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }

        public bool AddReference(ReferenceViewModel reference)
        {
            try
            {
                var referenceMapped = _mapper.Map<Reference>(reference);
                if (reference.Id == -1)
                {
                    SaveModel(referenceMapped);
                    return true;
                }

                referenceMapped.Id = reference.Id;
                UpdateModel(referenceMapped);

                return true;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
                return false;
            }
        }
    }
}
