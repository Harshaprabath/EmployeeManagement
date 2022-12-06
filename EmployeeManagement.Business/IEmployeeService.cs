using EmployeeManagement.ViewModel;
using EmployeeManagement.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Business
{
    public interface IEmployeeService
    {
        List<EmployeeViewModel> GetAllFilterDepartment(int dId) ;
        List<DropDownViewModel> GetAllDepartment();
        Task<ResponseViewModel> DeleteEmployee(int id);
        List<EmployeeViewModel> GetAll();
        Task<ResponseViewModel> SaveEmployee(EmployeeViewModel vm);

    }
}
