using System.ComponentModel.DataAnnotations;

namespace OrderProcessingSystem.Model.DTO
{
    public class CustomerRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required]

        public string Email { get; set; }

    }
}
