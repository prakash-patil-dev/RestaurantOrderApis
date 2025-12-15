namespace RestaurantOrderApis.Models
{
    public class User
    {
        public string? Username { get; set; }
        public string? GroupName { get; set; }
        public string? MacName { get; set; }
        public string? Password { get; set; }
        public string? LDays { get; set; }
        public DateTime LfTime { get; set; }
        public DateTime LtTime { get; set; }
        public string? UserCode { get; set; }

        public double SubDisc { get; set; }
        public double ItemDisc { get; set; }
        public double LessAmt { get; set; }

        public int FreeQty { get; set; }
        public double FreeVal { get; set; }

        public int IdKey { get; set; }
        public string? Administrator { get; set; }

        public string? Updated { get; set; }
        public int LanguageID { get; set; }

        public string? Image { get; set; }
    }
    public class LoginRequestFilter
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

}


