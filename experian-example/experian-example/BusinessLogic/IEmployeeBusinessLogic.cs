using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using experian_example.Dtos;

namespace experian_example.BusinessLogic
{
    public interface IEmployeeBusinessLogic
    {
        Task<int> CreateContact(int id, ContactDto contact);
        Task<int> CreateEmployee(EmployeeDto obj);
        Task<EmployeeDto> Get(int id);
    }
}
