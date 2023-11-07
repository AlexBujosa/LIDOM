using System.ComponentModel.DataAnnotations;

namespace LIDOM.Models
{
    public class Standing
    {
        [Key]
        public int TeamId { get; set; }    

        public string TeamName { get; set; }

        public int TotalGame { get; set; }

        public int WonGames { get; set; }

        public int LostGames { get; set; }
    }
}
