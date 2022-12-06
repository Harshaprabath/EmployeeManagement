using EmployeeManagement.Model.Enums;
using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            EmployeeProjets = new List<ProjectViewModel>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? DpartmentName { get; set; }
        public DpartmentType DpartmentType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        

        public List<ProjectViewModel> EmployeeProjets { get; set; }
       
    }
}
