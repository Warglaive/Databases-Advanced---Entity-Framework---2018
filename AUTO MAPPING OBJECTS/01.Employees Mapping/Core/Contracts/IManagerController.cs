using Banicharnica.App.Core.DTOs;

namespace Banicharnica.App.Core.Contracts
{
    public interface IManagerController
    {
        void SetManagerCommand(int employeeId, int managerId);
        ManagerDto GetManagerInfo(int employeeId);
    }
}