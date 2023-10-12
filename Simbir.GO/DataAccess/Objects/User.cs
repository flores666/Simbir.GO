using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Simbir.GO.DataAccess.Objects
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string Name { get; set; }
        
        [Required]
        [Column(TypeName = "text")]
        public string PasswordHash { get; set; }

        public RefreshToken? RefreshToken { get; set; }
    }
}