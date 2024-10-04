using AutoMapper;
using gk_system_api.Entities;
using gk_system_api.Models;
using gk_system_api.Services.Interfaces;

namespace gk_system_api.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IDatabaseService _db;
        private readonly ILogger _log;
        public VisitorService(IDatabaseService db, ILogger<VisitorService> log)
        {
            _db = db;
            _log = log;
        }
        public void CheckVisitor(VisitorViewModel model)
        {
            try
            {
                var visitors = _db.GetAllVisitors();
                if(visitors == null || !visitors.Any())
                    return;

                if (visitors.FirstOrDefault(v => v.FingerPrint == model.Fingerprint) != null)
                {
                    if (visitors.FirstOrDefault(v => v.Localisation.IPv4 == model.Localisation.IPv4) != null)
                        return;
                    else
                        IncreaseVisitor(model);
                }
                else
                {
                    if (visitors.FirstOrDefault(v => v.Localisation.IPv4 == model.Localisation.IPv4) != null)
                        return;
                    else
                        IncreaseVisitor(model);
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.ToString());
            }
        }

        public GetVisitorsViewModel? GetVisitors()
        {
            var topVisitors = _db.GetTopVisitors();
            if (topVisitors == null)
                return null;

            var result = new GetVisitorsViewModel()
            {
                Top = topVisitors,
                Total = _db.GetTotalVisitors()
            };
            return result;
        }

        private void IncreaseVisitor(VisitorViewModel model)
            => _db.AddVisitor(model);
    }
}
