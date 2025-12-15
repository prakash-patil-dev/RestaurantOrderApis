using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestaurantOrderApis.Models;

namespace RestaurantOrderApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ShiftController> _logger;

        public ShiftController(IConfiguration config, ILogger<ShiftController> logger)
        {
            _config = config;
            _logger = logger;
        }

       // [HttpGet(Name = "GetShift")]
        [HttpGet("GetShift")]
        public async Task<ActionResult<List<Shift>>> GetShift()
        {
            var ObjShifts = new List<Shift>();
            try
            {

                string connStr = _config.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();

                var command = new SqlCommand("select * from shifts", conn);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    ObjShifts.Add(new Shift
                    {
                        //Id = reader.GetInt32(0),
                        BranchCode = reader.IsDBNull(0) ? null : reader.GetString(0),
                        ShiftCode = reader.IsDBNull(1) ? null : reader.GetString(1),
                        ShiftName = reader.IsDBNull(2) ? null : reader.GetString(2),
                        ShiftTime1 = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                        ShiftTime2 = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                        Updated = reader.IsDBNull(5) ? null : reader.GetString(5),
                        LastUser = reader.IsDBNull(6) ? null : reader.GetString(6),
                        LastDate = reader.IsDBNull(7) ? DateTime.MinValue : reader.GetDateTime(7),
                        LastTime = reader.IsDBNull(8) ? null : reader.GetString(8)
                    });
                }

                return Ok(ObjShifts);
            }
            catch (Exception ex)
            {
                var message = "EXCEPTION : " + ex.Message;
                ObjShifts.Add(new Shift { BranchCode = "-1", ShiftName = "DefaultConnection : " + _config.GetConnectionString("DefaultConnection") + message + "   STACKSTRACE : " + ex.StackTrace });
                return Ok(ObjShifts);
            }

        }
        //private void LogException(Exception ex)
        //{
        //    string logPath = Path.Combine(_env.WebRootPath, "ErrorLog.txt");

        //    string message = $"Time: {DateTime.Now:dd/MM/yyyy hh:mm:ss tt}\n" +
        //                     $"Message: {ex.Message}\n" +
        //                     $"StackTrace: {ex.StackTrace}\n" +
        //                     $"Source: {ex.Source}\n" +
        //                     $"TargetSite: {ex.TargetSite}\n" +
        //                     "-----------------------------------------------------------\n";

        //    using (StreamWriter writer = new StreamWriter(logPath, true))
        //    {
        //        writer.WriteLine(message);
        //    }
        //}
    }
}
