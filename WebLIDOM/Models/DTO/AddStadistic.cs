namespace WebLIDOM.Models.DTO
{
    public class AddStadistic
    {
        public int Id_Calendar { get; set; }

        public int Id_FirstTeam { get; set; }

        public int Id_SecondTeam { get; set; }

        public int? Winner { get; set;  }
    }
}
