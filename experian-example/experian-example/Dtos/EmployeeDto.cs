using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace experian_example.Dtos
{
    public class EmployeeDto
    {
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<ContactDto> Contacts { get; set; }
    }
}
