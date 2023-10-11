using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IO.Swagger.DataAccess.Objects
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
        public string JWT { get; set; }
    }
}