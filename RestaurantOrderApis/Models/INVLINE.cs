namespace RestaurantOrderApis.Models
{
    //public class INVLINE
    //{
    //}
    public class INVLINE
    {
        public string? BRANCHCODE { get; set; }
        public double TXNNO { get; set; }
        public DateTime TXNDT { get; set; }
        public string? VIPNO { get; set; }
        public string? SHIFT { get; set; }
        public string? USER { get; set; }
        public string? STAFF { get; set; }
        public int LINE { get; set; }
        public string? ITEMCODE { get; set; }
        public string? ITEMNAME1 { get; set; }
        public string? ITEMNAME2 { get; set; }
        public string? CATCODE { get; set; }
        public string? SUBCATCODE { get; set; }
        public string? BRANDCODE { get; set; }
        public string? UNITCODE { get; set; }
        public double QUANTITY { get; set; }
        public double UNITRATE { get; set; }
        public double AMOUNT { get; set; }
        public double TAXPERC { get; set; }
        public double TAXVALUE { get; set; }
        public double COSTAMT { get; set; }
        public double COSTAMTSPA { get; set; }
        public string? SPLDISC { get; set; }
        public double DISCPERC { get; set; }
        public double DISCOUNT { get; set; }
        public double LESSAMT { get; set; }
        public string? PRINTED { get; set; }
        public string? BPRINTED { get; set; }
        public string? KPRINTED { get; set; }
        public string? EATTAKE { get; set; }
        public string? STATUS { get; set; }
        public string? UPDATED { get; set; }
        public string? STYLECODE { get; set; }
        public string? COLORCODE { get; set; }
        public string? SIZECODE { get; set; }
        public string? STARTTIME { get; set; }
        public string? ENDTIME { get; set; }
        public double PACKAGEID { get; set; }
        public double PACKLINE { get; set; }
        public double? REQCUST { get; set; }
        public double? REQCOMPANY { get; set; }
        public string? REQFLAG { get; set; }
        public string? LASTUSER { get; set; }
        public DateTime LASTDATE { get; set; }
        public string? LASTTIME { get; set; }
        public string? REPORTFLAG { get; set; }
        public string? KOT { get; set; }
        public string? SCANITEMCODE { get; set; }
        public string? STATIONID { get; set; }
        public string? PREPARED { get; set; }
        public string? PRICETYPE { get; set; }
        public string? TOPPING { get; set; }
        public string? DEPTCODE { get; set; }
        public string? SEASONCODE { get; set; }
        public double? DINETAKEVAL { get; set; }
    }
    public class InvLineFilter
    {
        public double TXNNO { get; set; }
        //public string? Password { get; set; }
    }
}
