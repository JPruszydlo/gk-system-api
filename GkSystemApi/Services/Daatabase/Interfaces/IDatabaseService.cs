using gk_system_api.Entities;
using gk_system_api.Models;
using gk_system_api.Utils;

namespace gk_system_api.Services.Interfaces
{
    public interface IDatabaseService
    {
        List<OfferViewModel> OffersMapped { get; }
        List<RealisationViewModel> RealisationsMapped { get; }
        Dictionary<string, GeneralConfigViewModel>? GetGeneralConfig(ConfigGroup group = ConfigGroup.Undefined);
        bool SaveModel<T>(T model);
        bool UpdateModel<T>(T model);

        IEnumerable<Visitor>? GetAllVisitors();
        string[]? GetTopVisitors();
        void AddVisitor(VisitorViewModel visitor);
        double GetTotalVisitors();
        User? GetUserByEmail(string username, string email);
        User? GetUserByPassword(string username, string password);
        User? GetUserByLogin(string login);
        bool SetGeneralConfig(GeneralConfigViewModel[] configs);
        List<object>? GetHomePageConfig();
        List<CarouselConfigViewModel>? GetCarouselConfig(string subPage);
        bool UpdateCarouselConfig(CarouselConfigViewModel[] configs);
        List<Email>? RemoveEmails(string ids);
        List<Email> GetEmails();
        bool SetOfferState(bool offerAvailable, int offerId);
        OfferViewModel? GetOffer(int id);
        List<OfferViewModel>? GetOffersThumbnails();
        bool AddOffer(OfferViewModel offer);
        bool UpdateOffer(int id, OfferViewModel offerModel);
        bool DeleteOffer(int id);
        bool SetFavouriteRealisation(int id, bool value);
        List<RealisationImageViewModel>? GetFavouriteRealisations();
        bool DeleteRealisationImage(int id);
        bool DeleteRealisation(int id);
        bool AddRealisation(RealisationViewModel realisation);
        List<ReferenceViewModel>? GetReferences(int? limit);
        bool DeleteReference(int id);
        bool AddReference(ReferenceViewModel reference);
    }
}
