using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FIGProject.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [JsonIgnore]
        [MaxLength(8)]
        public virtual List<Player>? Players { get; set; } //Players is nullable so team creation and player assignment can be separate actions
    }
}