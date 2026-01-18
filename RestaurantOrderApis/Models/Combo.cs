namespace RestaurantOrderApis.Models
{
    //public class Combo
    //{
    //}
    public class Combo
    {
        public string Code { get; set; }
        public string Name { get; set; } 
        public string Unit { get; set; }
        public double Price { get; set; }
        public double Qty { get; set; }
        public string MainType { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public double ItemPrice { get; set; }
        public string ItemUnit { get; set; }

        public double BaseQty { get; set; }

        public string Alt1Unit { get; set; }
        public double Alt1Amt { get; set; }
        public double Alt1Qty { get; set; }
        public string Alt2Unit { get; set; }
        public double Alt2Amt { get; set; }
        public double Alt2Qty { get; set; }
        public string Alt3Unit { get; set; }
        public double Alt3Amt { get; set; }
        public double Alt3Qty { get; set; }
        public string Alt4Unit { get; set; }
        public double Alt4Amt { get; set; }
        public double Alt4Qty { get; set; }

        public string LastUser { get; set; }
        public DateTime LastDate { get; set; }
        public string LastTime { get; set; }

        public string SubType { get; set; }
        public string Updated { get; set; }
    }

    public class ComboDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

}
