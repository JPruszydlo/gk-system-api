using gk_system_api.Models;

namespace gk_system_api.Services.Interfaces
{
    public interface IVisitorService
    {
        GetVisitorsViewModel? GetVisitors();

        void CheckVisitor(VisitorViewModel model);
    }
}
