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

            // ✅ Amount validation
            if (settlement.CashTotal + settlement.CardTotal != settlement.BillAmount)
                return BadRequest("Cash + Card total must match bill amount");

            //if (settlement.Mode == "D")
            //    return BadRequest("Invalid settlement mode");

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

    }
}
