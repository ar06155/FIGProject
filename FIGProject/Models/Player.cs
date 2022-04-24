using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FIGProject.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [JsonIgnore]
        public virtual Team? Team { get; set; } //Team is nullable so player creation and team assignment can be separate actions
    }
}