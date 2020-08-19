using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using experian_example.DataAccess;
using experian_example.Dtos;
using System.Linq;

namespace experian_example.BusinessLogic
{
    public class EmployeeBusinessLogic : IEmployeeBusinessLogic
    {
        private IDataAccess _dataAccess;

        public EmployeeBusinessLogic(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateContact(int id, ContactDto contact)
        {
            var entity = new Contact
            {
                EmailAddress = contact.EmailAddress,
                PhoneNumber = contact.PhoneNumber,
                PrimaryHomeAddress = contact.PrimaryHomeAddress
            };//todo: add automapper

            await _dataAccess.Create(id, entity);
            return id; 
        }

        public async Task<int> CreateEmployee(EmployeeDto obj)
        {
            var entity = new Employee
            {
                Contacts = obj.Contacts.Select(x =>
                    new Contact
                    {
                        EmailAddress = x.EmailAddress,
                        PhoneNumber = x.PhoneNumber,
                        PrimaryHomeAddress = x.PrimaryHomeAddress
                    }
                ).ToList(),
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Id = obj.Id
            };
            //todo: use automapper

            var id = await _dataAccess.Create(entity);
            return id;
        }

        public async Task<EmployeeDto> Get(int id)
        {
            var employee = await _dataAccess.Get(id);

            //map employee to employeeDto
            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Contacts = employee.Contacts.Select(x => new ContactDto
                {
                    EmailAddress = x.EmailAddress,
                    PhoneNumber = x.PhoneNumber,
                    PrimaryHomeAddress = x.PrimaryHomeAddress
                })
            };
        }
    }
}
