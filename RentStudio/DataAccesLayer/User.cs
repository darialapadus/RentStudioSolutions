using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;
using RentStudio.Models.Enums;

namespace RentStudio.DataAccesLayer
{
    public class User 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public Role Role { get; set; }

        public string CNP { get; set; } 
        public string Address { get; set; } 
    }
}
