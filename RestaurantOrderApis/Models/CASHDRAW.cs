namespace RestaurantOrderApis.Models
{
    public class CASHDRAW
    {
        public string BRANCHCODE { get; set; }
        public DateTime TXNDATE { get; set; }
        public string LASTUSER { get; set; }
        public string LASTTIME { get; set; }
        public double BILLNO { get; set; }
        public DateTime? BILLDATE { get; set; }
        public string SHIFT { get; set; }
        public string? DESC0 { get; set; }
        public string? DESC1 { get; set; }
        public string? DESC2 { get; set; }
        public string? DESC3 { get; set; }
        public string? DESC4 { get; set; }
        public string MODE { get; set; }
        public double TOPUPAMT { get; set; }
        public double CASHAMT { get; set; }
        public double? DEPCASH { get; set; }
        public double? CARDAMT { get; set; }
        public string? CARDNAME { get; set; }
        public string UPDATED { get; set; }

    }
    public class INVCARD
    {
        public double TXNNO { get; set; }
        public string? CRNO { get; set; }
        public DateTime TXNDT { get; set; }
        public double AMOUNT { get; set; }
        public string? CRCODE { get; set; }
        public string LASTUSER { get; set; }
        public DateTime LASTDATE { get; set; }
        public string LASTTIME { get; set; }
        public string BRANCHCODE { get; set; }
        public string MODE { get; set; }
        public string UPDATED { get; set; }
    }


    public class SettlementRequest
    {
        public double BILLNO { get; set; }
        public double BillAmount { get; set; }
        public string Mode { get; set; }
        public double CashTotal { get; set; }
        public double CardTotal { get; set; }
        public INVCARD InvCardEntry { get; set; }
        public CASHDRAW CashEntry { get; set; }
        public string LASTUSER { get; set; }
        public DateTime LASTDATE { get; set; }
        public string LASTTIME { get; set; }
    }


}
