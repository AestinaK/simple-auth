using simple_authentication.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_authentication.Entity
{
    [Table("user")]
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }    
        public char Status  { get; set; }= UserStatus.Active;
        public string Type { get; set; } = UserType.normal;
        public DateTime CreatedDate { get; set; }= DateTime.Now;
    }
}
