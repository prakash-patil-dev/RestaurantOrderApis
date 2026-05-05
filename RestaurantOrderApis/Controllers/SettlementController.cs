using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestaurantOrderApis.Models;

namespace RestaurantOrderApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettlementController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SettlementController> _logger;


        public SettlementController(IConfiguration config, ILogger<SettlementController> logger)
        {
            _config = config;
            _logger = logger;
        }
        // GET: SettlementController
        [HttpPost("SettledCurrentBill")]
        public async Task<IActionResult> SettledCurrentBill([FromBody] SettlementRequest settlement)
        {
            if (settlement == null)
                return BadRequest("Invalid request");
            var roundedTotal = Math.Round(settlement.CashTotal + settlement.CardTotal + settlement.CurrencyTotal, 2, MidpointRounding.AwayFromZero);

            var roundedBill = Math.Round(settlement.BillAmount, 2, MidpointRounding.AwayFromZero);

            if (roundedTotal != roundedBill)
                return BadRequest("Cash + Card total must match bill amount");
            
            string connStr = _config.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connStr))
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (settlement.CashTotal > 0)
                        {
                            string cashQuery = @"INSERT INTO CASHDRAW(BRANCHCODE,TXNDATE,LASTUSER,LASTTIME,BILLNO,SHIFT,MODE,TOPUPAMT,CASHAMT,UPDATED)
                                                 VALUES (@BRANCHCODE,@TXNDATE,@LASTUSER,@LASTTIME,@BILLNO,@SHIFT,@MODE,0,@CASHAMT,@UPDATED)";
                            await connection.ExecuteAsync(cashQuery,  settlement.CashEntry, transaction );
                        }

                        if (settlement.CardTotal > 0)
                        {
                            string cardQuery = @"INSERT INTO INVCARD(TXNNO,CRNO,TXNDT,AMOUNT,CRCODE,LASTUSER,LASTDATE,LASTTIME,BRANCHCODE,MODE,UPDATED)
                                                 VALUES (@TXNNO,@CRNO,@TXNDT,@AMOUNT,@CRCODE,@LASTUSER,@LASTDATE,@LASTTIME,@BRANCHCODE,@MODE,@UPDATED)";
                            await connection.ExecuteAsync(cardQuery, settlement.InvCardEntry, transaction);
                        }


                        //if (settlement.CurrencyTotal > 0)
                        //{
                        //    string currQuery = @"INSERT INTO INVCURRENCY(BRANCHCODE,cur_code,TXNNO,TXNDT,AMOUNT,STATUS,LASTUSER,LASTDATE,LASTTIME,UPDATED,EXCHRATE)
                        //                         VALUES (@BranchCode,@CurCode,@TxnNo,@TxnDt,@Amount,@Status,@LastUser,@LastDate,@LastTime,@Updated,@ExchRate)";
                        //    await connection.ExecuteAsync(currQuery, settlement.CurrencyEntry, transaction);
                        //}
                        if (settlement.CurrencyTotal > 0 && settlement.CurrencyEntry?.Any() == true)
                        {
                            string currQuery = @"INSERT INTO INVCURRENCY (BRANCHCODE,cur_code,TXNNO,TXNDT,AMOUNT,STATUS,LASTUSER,LASTDATE,LASTTIME,UPDATED,EXCHRATE)
                                                VALUES (@BranchCode,@CurCode,@TxnNo,@TxnDt,@Amount,@Status,@LastUser,@LastDate,@LastTime,@Updated,@ExchRate)";

                            await connection.ExecuteAsync(currQuery, settlement.CurrencyEntry, transaction);
                        }
                        string updateHeadQuery = @$"UPDATE INVHEAD  SET STATUS = 'C', LASTDATE = @LASTDATE, LASTUSER =@LASTUSER,LASTTIME=@LASTTIME  WHERE TXNNO = @BILLNO";
                        await connection.ExecuteAsync( updateHeadQuery,  settlement, transaction );


                        string updateDetailsQuery = @$"UPDATE INVLINE  SET STATUS = 'C', LASTDATE = @LASTDATE, LASTUSER =@LASTUSER,LASTTIME=@LASTTIME  WHERE TXNNO = @BILLNO";
                        await connection.ExecuteAsync(updateDetailsQuery, settlement, transaction);
                        transaction.Commit();

                        return Ok($"{settlement.BILLNO} settled successfully");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return StatusCode(500, ex.Message);
                    }
                }
            }
        }


        [HttpGet]
        [Route("GetSysParam")]
        public async Task<ActionResult> GetSysParam()
        {
            try
            {
                SysParam model = new();
                string connStr = _config.GetConnectionString("DefaultConnection");
                using (var connection = new SqlConnection(connStr))
                {
                    string query = @"SELECT cash1, cash2, cash3, cash4, cash5, cash6, cash7, cash8 FROM SYSPARAM";
                    model = (await connection.QueryAsync<SysParam>(query)).FirstOrDefault();
                }
                if (model == null)
                    return NotFound();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching GetSysParam : {ex.Message}");
            }
        }


        [HttpGet("GetCurrencytypes")]
        public async Task<ActionResult<List<EXRATE>>> GetCurrencytypes()
        {
            // var Users = new List<Customer>();

            try
            {
                string connStr = _config.GetConnectionString("DefaultConnection");

                //using var conn = new SqlConnection(connStr);
                //await conn.OpenAsync();
                using (var connection = new SqlConnection(connStr))
                {
                    string query = @"select * from EXRATE order by ERATE_DATE asc";

                    var invLineList = (await connection.QueryAsync<EXRATE>(query)).ToList();
                    return Ok(invLineList);
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching invoice lines: {ex.Message}");
            }
            // return View();
        }

    }
}
