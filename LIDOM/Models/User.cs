namespace LIDOM.Models
{
    public class User
    {
        public int Id { get; set; } 

        public string UserName { get; set; }    

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
