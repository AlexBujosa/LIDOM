
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebLIDOM.Models
{
    public class Stadistic
    {
        public int Id_Calendar { get; set; }

        public int Id_Team { get; set; }

        public bool Win { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}
