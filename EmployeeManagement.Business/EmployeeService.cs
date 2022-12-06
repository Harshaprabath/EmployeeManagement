using EmployeeManagement.Data.Data;
using EmployeeManagement.Model;
using EmployeeManagement.Model.Enums;
using EmployeeManagement.Util;
using EmployeeManagement.ViewModel;
using EmployeeManagement.ViewModel.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmployeeManagement.Business
{
    public class EmployeeService
    {
        private readonly EmployeeManagementContext employeeDb;
        private readonly IConfiguration config;

        public EmployeeService(EmployeeManagementContext employeeDb, IConfiguration config)
        {
            this.employeeDb = employeeDb;
            this.config = config;          
        }

        public List<EmployeeViewModel> GetAll()
        {
            var response = new List<EmployeeViewModel>();

            var query = employeeDb.Employees
                            .Where(e => e.IsActive == true)
                            .OrderBy(c => c.CreatedDate); 

            var employeeList = query.ToList();

            foreach (var employee in employeeList)
            {
                var employeeProjets = new List<DropDownViewModel>();

                var employeeProjetList = employeeDb.Project.Where(row => row.EmplyeeId == employee.Id).ToList();

                foreach (var item in employeeProjetList)
                {
                    var employeeProjetlVM = new DropDownViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    };
                    employeeProjets.Add(employeeProjetlVM);
                }

                var vm = new EmployeeViewModel
                {
                    Id = employee.Id,
                    Firstname = employee.Firstname,
                    Lastname = employee.Lastname,
                    Email = employee.Email,
                    MobileNumber = employee.MobileNumber,
                    DateOfBirth = employee.DateOfBirth,
                    DpartmentName = employee.Department,
                    IsActive = employee.IsActive,                 
                    CreatedDate = employee.CreatedDate,
                };

                response.Add(vm);
            }
            return response;
        }


        public async Task<ResponseViewModel> SaveSubject(EmployeeViewModel vm)
        {
            var response = new ResponseViewModel();
            try
            {

                var employee = employeeDb.Employees
                                .FirstOrDefault(x => x.Id == vm.Id);

                if (employee == null)
                {
                    employee = new Model.Employee()
                    {
                        Id = vm.Id,
                        Firstname = vm.Firstname,
                        Lastname = vm.Lastname,
                        Email = vm.Email,
                        IsActive = true,
                        MobileNumber = vm.MobileNumber,
                        DateOfBirth = vm.DateOfBirth,
                        CreatedDate = DateTime.UtcNow,
                    };

                    if (vm.DpartmentType == DpartmentType.IT)
                    {
                        employee.Department = "IT";
                        
                    }
                    else if (vm.DpartmentType == DpartmentType.HR)
                    {
                        employee.Department = "HR";
                    }
                    else
                    {
                        employee.Department = "NO";
                    }

                    employeeDb.Employees.Add(employee);
                    await employeeDb.SaveChangesAsync();

                    foreach (var project in vm.EmployeeProjets)
                    {
                        var employeeProjet = new Project()
                        {   
                            Id = project.Id,
                            EmplyeeId = vm.Id,
                            IsActive= true,
                            Name = project.Name,
                        };

                        employeeDb.Project.Add(employeeProjet);
                        await employeeDb.SaveChangesAsync();
                    }
                    response.IsSuccess = true;
                    response.Message = "SAVE SUCCESS";
                }
                else
                {
                    employee.Firstname = vm.Firstname;
                    employee.Lastname = vm.Lastname;
                    employee.Email = vm.Email;
                    employee.MobileNumber = vm.MobileNumber;
                    employee.CreatedDate = vm.CreatedDate;
                    employee.DateOfBirth = vm.DateOfBirth;
                    employee.IsActive = true;

                    if (vm.DpartmentType == DpartmentType.IT)
                    {
                        employee.Department = "IT";

                    }
                    else if (vm.DpartmentType == DpartmentType.HR)
                    {
                        employee.Department = "HR";
                    }
                    else
                    {
                        employee.Department = "NO";
                    }

                    var query = employeeDb.Project
                                    .Where(e => e.IsActive == true && e.EmplyeeId == vm.Id);
                    
                    var employeeProjectList = query.ToList();

                    var updatedProjectList = vm.EmployeeProjets.ToList();

                    foreach (var employeeProject in employeeProjectList)
                    {
                        foreach (var updatedProject in updatedProjectList)
                        {
                            if (employeeProject.Id == updatedProject.Id)
                            {
                                employeeProject.Name = updatedProject.Name;
                                employeeProject.IsActive = updatedProject.IsActive;

                                employeeDb.Project.Update(employeeProject);

                            }
                            else
                            {
                                employeeProject.Id = updatedProject.Id;
                                employeeProject.Name = updatedProject.Name;
                                employeeProject.IsActive = updatedProject.IsActive;
                                employeeProject.EmplyeeId = vm.Id;

                                employeeDb.Project.Add(employeeProject);
                            }
                            
                        }

                    }

                    employeeDb.Employees.Update(employee);

                    response.IsSuccess = true;
                    response.Message = "UPDATE SUCCESS"; ;
                }
                await employeeDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<ResponseViewModel> DeleteEmploye(int id)
        {
            var response = new ResponseViewModel();

            try
            {
                var employee = employeeDb.Employees.FirstOrDefault(x => x.Id == id);

                employee.IsActive = false;

                employeeDb.Employees.Update(employee);
                await employeeDb.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "DELETE SUCCESS";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public List<DropDownViewModel> GetAllDepartment()
        {
            var response = new List<DropDownViewModel>();

                var vm = new DropDownViewModel
                {
                    Id= (int)DpartmentType.IT,
                    Name= "IT",

                };

                response.Add(vm);
                
                vm = new DropDownViewModel
                {
                    Id = (int)DpartmentType.HR,
                    Name = "HR",

                };
                response.Add(vm);
       
            return response;
        }

        public List<EmployeeViewModel> GetAllFilterDepartment(int dId)
        {
            var query = employeeDb.Employees
                            .Where(e => e.IsActive == true)
                            .OrderBy(c => c.CreatedDate);

            if (dId == (int)DpartmentType.IT) 
            {
                query = employeeDb.Employees
                            .Where(e => e.IsActive == true && e.Department == "IT")
                            .OrderBy(c => c.CreatedDate);

            }
            else if (dId == (int)DpartmentType.HR)
            {
                query = employeeDb.Employees
                            .Where(e => e.IsActive == true && e.Department == "HR")
                            .OrderBy(c => c.CreatedDate);

            }
            else
            {
                query = employeeDb.Employees
                            .Where(e => e.IsActive == true)
                            .OrderBy(c => c.CreatedDate);

            }
          
            var response = new List<EmployeeViewModel>();

            var employeeList = query.ToList();

            foreach (var employee in employeeList)
            {
                var employeeProjets = new List<ProjectViewModel>();

                var employeeProjetList = employeeDb.Project
                                        .Where(row => row.EmplyeeId == employee.Id && row.IsActive == true)
                                        .ToList();

                foreach (var item in employeeProjetList)
                {
                    var employeeProjetlVM = new ProjectViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    };
                    employeeProjets.Add(employeeProjetlVM);
                }

                var vm = new EmployeeViewModel
                {
                    Id = employee.Id,
                    Firstname = employee.Firstname,
                    Lastname = employee.Lastname,
                    Email = employee.Email,
                    MobileNumber = employee.MobileNumber,
                    DateOfBirth = employee.DateOfBirth,
                    DpartmentName = employee.Department,
                    IsActive = employee.IsActive,
                    CreatedDate = employee.CreatedDate,
                    EmployeeProjets = employeeProjets,
                };

                response.Add(vm);
            }
            return response;
        }


    }
}
