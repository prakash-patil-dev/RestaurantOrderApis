using System.Collections.ObjectModel;

namespace RestaurantOrderApis.Models
{
    public class INVHEAD
    {
        public double SUBTOTAL { get; set; }
        public string? BRANCHCODE { get; set; }
        public double TXNNO { get; set; }
        public DateTime TXNDT { get; set; }
        public string? VIPNO { get; set; }
        public string? SHIFT { get; set; }
        public string? USER { get; set; }
        public string? STAFF { get; set; }
        public double ITEMDISC { get; set; }
        public double BILLAMOUNT { get; set; }
        public double TOTCOSTAMT { get; set; }
        public DateTime? DUEDATE { get; set; }
        public double? SPLPERC { get; set; }
        public double SPLSALE { get; set; }
        public string? SPLNAME { get; set; }
        public double DISCPERC { get; set; }
        public double BILLDISC { get; set; }
        public double BILLCASH { get; set; }
        public double BILLCARD { get; set; }
        public double DEPCASH { get; set; }
        public double DEPCARD { get; set; }
        public double DEPCURR { get; set; }
        public double BILLCURR { get; set; }
        public double? EXCHRATE { get; set; }
        public double? TIPCASH { get; set; }
        public double? TIPCARD { get; set; }
        public string? TIPCRDNAME { get; set; }
        public double? TAXAMT { get; set; }
        public double TAX1AMT { get; set; }
        public double TAX2AMT { get; set; }
        public double TAXVATAMT { get; set; }
        public double TABLENO { get; set; }
        public double NOOFPERSNS { get; set; }
        public string? EATTAKE { get; set; }
        public double? LPRINTED { get; set; }
        public double? ABSORBGST { get; set; }
        public string? GSTREFNO { get; set; }
        public string? REFERENCE1 { get; set; }
        public string? REFERENCE2 { get; set; }
        public string? CUSTNAME { get; set; }
        public string? CUSTADD1 { get; set; }
        public string? CUSTADD2 { get; set; }
        public string? CUSTADD3 { get; set; }
        public double? RECEIPTNO { get; set; }
        public string? STATUS { get; set; }
        public string? REMARKS { get; set; }
        public double? REPRINT { get; set; }
        public string? UPDATED { get; set; }
        public double lessamount { get; set; }
        public double GST { get; set; }
        public double tendered { get; set; }
        public double BALANCE { get; set; }
        public string? LASTUSER { get; set; }
        public DateTime LASTDATE { get; set; }
        public string? LASTTIME { get; set; }
        public string? STATIONID { get; set; }
        public string? SETTLETYPE { get; set; }
        public string? ROOMNO { get; set; }
        public string? ProviderCd { get; set; }
       // public List<INVLINE> INVLINE { get; set; } = new List<INVLINE>();
    }

    public class INVHEADDETAILS
    {
        public INVHEAD CurrentOpenBill {  get; set; }
        public ObservableCollection<INVLINE> CurrentOpenBillDetails { get; set; }
    }
}