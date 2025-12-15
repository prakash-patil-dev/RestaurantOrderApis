using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestaurantOrderApis.Extensions;
using RestaurantOrderApis.Models;

namespace RestaurantOrderApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ItemController> _logger;
        public ItemController(IConfiguration config, ILogger<ItemController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetItems")]
        public async Task<ActionResult<List<item>>> GetItems()
        {
            var ObjItems = new List<item>();
            try
            {

                string connStr = _config.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();

                var command = new SqlCommand("select * from item order by itemname1 , itemcode", conn);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {

                    ObjItems.Add( new item
                    {
                        ITEMCODE = reader["ITEMCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ITEMCODE"]),
                        ITEMNAME1 = reader["ITEMNAME1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ITEMNAME1"]),
                        ITEMNAME2 = reader["ITEMNAME2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ITEMNAME2"]),
                        STYLECODE = reader["STYLECODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["STYLECODE"]),
                        SEASONCODE = reader["SEASONCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SEASONCODE"]),
                        FABRICCODE = reader["FABRICCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FABRICCODE"]),
                        MANUFACTUR = reader["MANUFACTUR"] == DBNull.Value ? string.Empty : Convert.ToString(reader["MANUFACTUR"]),
                        SUPLCODE = reader["SUPLCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SUPLCODE"]),
                        DEPTCODE = reader["DEPTCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DEPTCODE"]),
                        CATCODE = reader["CATCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CATCODE"]),
                        SUBCATCODE = reader["SUBCATCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SUBCATCODE"]),
                        BRANDCODE = reader["BRANDCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BRANDCODE"]),
                        SIZECODE = reader["SIZECODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SIZECODE"]),
                        COLORCODE = reader["COLORCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["COLORCODE"]),
                        UNITCODE = reader["UNITCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UNITCODE"]),
                        ALTUNIT1 = reader["ALTUNIT1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ALTUNIT1"]),
                        ALTBAR1 = reader["ALTBAR1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ALTBAR1"]),
                        CONVFACT1 = reader["CONVFACT1"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVFACT1"]),
                        CONVAL1 = reader["CONVAL1"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVAL1"]),
                        CONVALSP1 = reader["CONVALSP1"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVALSP1"]),
                        TYPE1 = reader["TYPE1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TYPE1"]),
                        ALTUNIT2 = reader["ALTUNIT2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ALTUNIT2"]),
                        ALTBAR2 = reader["ALTBAR2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ALTBAR2"]),
                        CONVFACT2 = reader["CONVFACT2"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVFACT2"]),
                        CONVAL2 = reader["CONVAL2"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVAL2"]),
                        CONVALSP2 = reader["CONVALSP2"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVALSP2"]),
                        TYPE2 = reader["TYPE2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TYPE2"]),
                        ALTUNIT3 = reader["ALTUNIT3"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ALTUNIT3"]),
                        ALTBAR3 = reader["ALTBAR3"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ALTBAR3"]),
                        CONVFACT3 = reader["CONVFACT3"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVFACT3"]),
                        CONVAL3 = reader["CONVAL3"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVAL3"]),
                        CONVALSP3 = reader["CONVALSP3"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVALSP3"]),
                        TYPE3 = reader["TYPE3"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TYPE3"]),
                        MAXIMUM = reader["MAXIMUM"] == DBNull.Value ? 0f : Convert.ToSingle(reader["MAXIMUM"]),
                        REORDER = reader["REORDER"] == DBNull.Value ? 0f : Convert.ToSingle(reader["REORDER"]),
                        COSTPRIC = reader["COSTPRIC"] == DBNull.Value ? 0f : Convert.ToSingle(reader["COSTPRIC"]),
                        COSTPRIC2 = reader["COSTPRIC2"] == DBNull.Value ? 0f : Convert.ToSingle(reader["COSTPRIC2"]),
                        COSTPRIC3 = reader["COSTPRIC3"] == DBNull.Value ? 0f : Convert.ToSingle(reader["COSTPRIC3"]),
                        SALEPRIC = reader["SALEPRIC"] == DBNull.Value ? 0f : Convert.ToSingle(reader["SALEPRIC"]),
                        WHOLEPRIC = reader["WHOLEPRIC"] == DBNull.Value ? 0f : Convert.ToSingle(reader["WHOLEPRIC"]),
                        DISCFLAG = reader["DISCFLAG"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DISCFLAG"]),
                        TAXCODE = reader["TAXCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TAXCODE"]),
                        OPGSTOCK = reader["OPGSTOCK"] == DBNull.Value ? 0f : Convert.ToSingle(reader["OPGSTOCK"]),
                        OPGVALUE = reader["OPGVALUE"] == DBNull.Value ? 0f : Convert.ToSingle(reader["OPGVALUE"]),
                        CLOSESTOCK = reader["CLOSESTOCK"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CLOSESTOCK"]),
                        CLOSEVALUE = reader["CLOSEVALUE"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CLOSEVALUE"]),
                        QNTYRCP = reader["QNTYRCP"] == DBNull.Value ? 0f : Convert.ToSingle(reader["QNTYRCP"]),
                        QNTYISS = reader["QNTYISS"] == DBNull.Value ? 0f : Convert.ToSingle(reader["QNTYISS"]),
                        QNTYADJ = reader["QNTYADJ"] == DBNull.Value ? 0f : Convert.ToSingle(reader["QNTYADJ"]),
                        NOOFTXNS = reader["NOOFTXNS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NOOFTXNS"]),
                        PRIC1 = reader["PRIC1"] == DBNull.Value ? 0f : Convert.ToSingle(reader["PRIC1"]),
                        PRIC2 = reader["PRIC2"] == DBNull.Value ? 0f : Convert.ToSingle(reader["PRIC2"]),
                        PRIC3 = reader["PRIC3"] == DBNull.Value ? 0f : Convert.ToSingle(reader["PRIC3"]),
                        UPDATED = reader["UPDATED"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UPDATED"]),
                        LASTUSER = reader["LASTUSER"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LASTUSER"]),
                        LASTDATE = reader["LASTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["LASTDATE"]),
                        LASTTIME = reader["LASTTIME"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LASTTIME"]),
                        KCode = reader["KCode"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KCode"]),
                        STOCKTYPE = reader["STOCKTYPE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["STOCKTYPE"]),
                        Topping = reader["Topping"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Topping"]),
                        ComboMeal = reader["ComboMeal"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ComboMeal"]),
                        FastMoving = reader["FastMoving"] == DBNull.Value ? string.Empty : Convert.ToString(reader["FastMoving"]),
                        PrepMeth = reader["PrepMeth"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PrepMeth"]),
                        image = reader["image"] == DBNull.Value ? string.Empty : Convert.ToString(reader["image"]),
                        KOTRecipe = reader["KOTRecipe"] == DBNull.Value ? string.Empty : Convert.ToString(reader["KOTRecipe"]),
                        BINID = reader["BINID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BINID"]),
                        ALTUNIT4 = reader["ALTUNIT4"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ALTUNIT4"]),
                        ALTBAR4 = reader["ALTBAR4"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ALTBAR4"]),
                        CONVFACT4 = reader["CONVFACT4"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVFACT4"]),
                        CONVAL4 = reader["CONVAL4"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVAL4"]),
                        CONVALSP4 = reader["CONVALSP4"] == DBNull.Value ? 0f : Convert.ToSingle(reader["CONVALSP4"]),
                        TYPE4 = reader["TYPE4"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TYPE4"]),
                        EXCHFLAG = reader["EXCHFLAG"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EXCHFLAG"]),
                        EXCHRATE = reader["EXCHRATE"] == DBNull.Value ? 0f : Convert.ToSingle(reader["EXCHRATE"]),
                        HAPPYFLAG = reader["HAPPYFLAG"] == DBNull.Value ? string.Empty : Convert.ToString(reader["HAPPYFLAG"]),
                        ISWEIGHITEM = reader["ISWEIGHITEM"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ISWEIGHITEM"])
                    });


                }

                return Ok(ObjItems);
            }
            catch (Exception ex)
            {
                var message = "EXCEPTION : " + ex.Message;
                //  ObjShifts.Add(new Shift { BranchCode = "-1", ShiftName = "DefaultConnection : " + _config.GetConnectionString("DefaultConnection") + message + "   STACKSTRACE : " + ex.StackTrace });
                return default;
            }

        }
    }
}
