using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmplyeeId { get; set; }
        public bool IsActive { get; set; }

        public Employee Empoyee { get; set; }
       
    }
}
