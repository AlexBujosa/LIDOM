using LIDOM.utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIDOM.Models
{
    public class Calendar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        public int Id_FirstTeam { get; set; }

        [Required]
        public int Id_SecondTeam { get; set; }

        [Required]
        public DateTime GameDate { get; set; }

        [Required]
        public string? Home { get; set; }

        [DefaultValue(GameStatus.Incoming)]
        public GameStatus? Status { get; set; } 

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey("Id_FirstTeam")]
        public LidomTeam? LidomFirstTeam { get; set; }

        [ForeignKey("Id_SecondTeam")]
        public LidomTeam? LidomSecondTeam { get; set; }
    }
}
 