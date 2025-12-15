using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestaurantOrderApis.Models;

namespace RestaurantOrderApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CATEGORYController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<CATEGORYController> _logger;
        public CATEGORYController(IConfiguration config, ILogger<CATEGORYController> logger)
        {
            _config = config;
            _logger = logger;
        }
      
        [HttpGet("GetCategorys")]
        public async Task<ActionResult<List<CATEGORY>>> GetShift()
        {
            var ObjCategorys = new List<CATEGORY>();
            try
            {

                string connStr = _config.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();

                var command = new SqlCommand("select * from CATEGORY order by  NAME", conn);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    ObjCategorys.Add(new CATEGORY
                    {
                        //Id = reader.GetInt32(0),
                        CODE = reader["CODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CODE"]),
                        NAME = reader["NAME"] == DBNull.Value ? string.Empty : Convert.ToString(reader["NAME"]),
                        UPDATED = reader["UPDATED"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UPDATED"]),
                        LASTUSER = reader["LASTUSER"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LASTUSER"]),
                        LASTDATE = reader["LASTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["LASTDATE"]),
                        LASTTIME = reader["LASTTIME"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LASTTIME"]),
                        DEPTCODE = reader["DEPTCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DEPTCODE"]),

                    });
                }

                return Ok(ObjCategorys);
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
