using LIDOM.utils;

namespace WebLIDOM.Models.DTO
{
    public class UpdateCalendar
    {
        public int Id { get; set; }

        public string? Home { get; set; }

        public GameStatus? Status { get; set; }

        public DateTime? GameDate { get; set; }
    }
}
