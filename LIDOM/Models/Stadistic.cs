
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIDOM.Models
{
    public class Stadistic
    {
        [Key, Column(Order = 0)]
        public int Id_Calendar { get; set; }

        [Key, Column(Order = 1)]
        public int Id_Team { get; set; }

        public bool Win { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey("Id_Calendar")]
        public Calendar Calendar { get; set; }

        [ForeignKey("Id_Team")]
        public LidomTeam LidomTeam { get; set; }

    }
}
