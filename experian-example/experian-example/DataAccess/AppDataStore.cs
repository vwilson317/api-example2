using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;

namespace experian_example.DataAccess
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }

    public class Contact
    {
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PrimaryHomeAddress { get; set; }
    }

    public interface IDataAccess
    {
        Task<Employee> Get(int id);
        Task<int> Create(int id, Contact contact);
        Task<int> Create(Employee employee);
    }

    public class DataAccess : IDataAccess
    {
        private ConcurrentDictionary<int, Employee> _dataStore { get; } = new ConcurrentDictionary<int, Employee>();

        public async Task<int> Create(int id, Contact contact)
        {
            Employee employee;
            _dataStore.TryGetValue(id, out employee);
            employee.Contacts.ToList().Add(contact);
            return id;
        }

        public async Task<int> Create(Employee employee)
        {
            _dataStore.TryAdd(employee.Id, employee);
            return employee.Id;
        }

        public async Task<Employee> Get(int id)
        {
            Employee employee;
            _dataStore.TryGetValue(id, out employee);
            return employee;
        }
    }
}
