using WebLIDOM.utils;

namespace WebLIDOM.Models.DTO
{
    public class AddNewCalendar
    {
        public int? Id { get; set; }

        public int Id_FirstTeam { get; set; }

        public int Id_SecondTeam { get; set; }

        public DateTime GameDate { get; set; }

        public string? Home { get; set; }

        public GameStatus? Status { get; set; }
    }
}
