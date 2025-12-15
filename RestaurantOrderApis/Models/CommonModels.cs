namespace RestaurantOrderApis.Models
{
    //public class CommonModels
    //{
    //}
    public class Shift
    {
        public string? BranchCode { get; set; }
        public string? ShiftCode { get; set; }
        public string? ShiftName { get; set; }
        public DateTime ShiftTime1 { get; set; }
        public DateTime ShiftTime2 { get; set; }
        public string? Updated { get; set; }
        public string? LastUser { get; set; }
        public DateTime LastDate { get; set; }
        public string? LastTime { get; set; }
    }

    public class CATEGORY
    {
        public string? CODE { get; set; } // Unchecked, so nullable
        public string? NAME { get; set; } = string.Empty;
        public string? UPDATED { get; set; } = string.Empty; // Typically "0" or "1"
        public string? LASTUSER { get; set; } = string.Empty;
        public DateTime LASTDATE { get; set; }
        public string? LASTTIME { get; set; } = string.Empty;
        public string? DEPTCODE { get; set; } = string.Empty;
    }
 
    public class item
    {
        public string ITEMCODE { get; set; } = string.Empty;
        public string ITEMNAME1 { get; set; } = string.Empty;
        public string ITEMNAME2 { get; set; } = string.Empty;
        public string STYLECODE { get; set; } = string.Empty;
        public string SEASONCODE { get; set; } = string.Empty;
        public string FABRICCODE { get; set; } = string.Empty;
        public string MANUFACTUR { get; set; } = string.Empty;
        public string SUPLCODE { get; set; } = string.Empty;
        public string DEPTCODE { get; set; } = string.Empty;
        public string CATCODE { get; set; } = string.Empty;
        public string SUBCATCODE { get; set; } = string.Empty;
        public string BRANDCODE { get; set; } = string.Empty;
        public string SIZECODE { get; set; } = string.Empty;
        public string COLORCODE { get; set; } = string.Empty;
        public string UNITCODE { get; set; } = string.Empty;
        public string ALTUNIT1 { get; set; } = string.Empty;
        public string ALTBAR1 { get; set; } = string.Empty;
        public float CONVFACT1 { get; set; }
        public float CONVAL1 { get; set; }
        public float CONVALSP1 { get; set; }
        public string TYPE1 { get; set; } = string.Empty;
        public string ALTUNIT2 { get; set; } = string.Empty;
        public string ALTBAR2 { get; set; } = string.Empty;
        public float CONVFACT2 { get; set; }
        public float CONVAL2 { get; set; }
        public float CONVALSP2 { get; set; }
        public string TYPE2 { get; set; } = string.Empty;
        public string ALTUNIT3 { get; set; } = string.Empty;
        public string ALTBAR3 { get; set; } = string.Empty;
        public float CONVFACT3 { get; set; }
        public float CONVAL3 { get; set; }
        public float CONVALSP3 { get; set; }
        public string TYPE3 { get; set; } = string.Empty;
        public float MAXIMUM { get; set; }
        public float REORDER { get; set; }
        public float COSTPRIC { get; set; }
        public float COSTPRIC2 { get; set; }
        public float COSTPRIC3 { get; set; }
        public float SALEPRIC { get; set; }
        public float WHOLEPRIC { get; set; }
        public string DISCFLAG { get; set; } = string.Empty;
        public string TAXCODE { get; set; } = string.Empty;
        public float OPGSTOCK { get; set; }
        public float OPGVALUE { get; set; }
        public float CLOSESTOCK { get; set; }
        public float CLOSEVALUE { get; set; }
        public float QNTYRCP { get; set; }
        public float QNTYISS { get; set; }
        public float QNTYADJ { get; set; }
        public int NOOFTXNS { get; set; }
        public float PRIC1 { get; set; }
        public float PRIC2 { get; set; }
        public float PRIC3 { get; set; }
        public string UPDATED { get; set; } = string.Empty;
        public string LASTUSER { get; set; } = string.Empty;
        public DateTime LASTDATE { get; set; }
        public string LASTTIME { get; set; } = string.Empty;
        public string KCode { get; set; } = string.Empty;
        public string STOCKTYPE { get; set; } = string.Empty;
        public string Topping { get; set; } = string.Empty;
        public string ComboMeal { get; set; } = string.Empty;
        public string FastMoving { get; set; } = string.Empty;
        public string PrepMeth { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string KOTRecipe { get; set; } = string.Empty;
        public string BINID { get; set; } = string.Empty;
        public string ALTUNIT4 { get; set; } = string.Empty;
        public string ALTBAR4 { get; set; } = string.Empty;
        public float CONVFACT4 { get; set; }
        public float CONVAL4 { get; set; }
        public float CONVALSP4 { get; set; }
        public string TYPE4 { get; set; } = string.Empty;
        public string EXCHFLAG { get; set; } = string.Empty;
        public float EXCHRATE { get; set; }
        public string HAPPYFLAG { get; set; } = string.Empty;
        public string ISWEIGHITEM { get; set; } = string.Empty;
    }
}
