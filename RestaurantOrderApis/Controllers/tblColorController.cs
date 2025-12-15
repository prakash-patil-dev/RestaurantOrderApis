using RestaurantOrderApis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace RestaurantOrderApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class tblColorController : ControllerBase
    {
  
        private readonly IConfiguration _config;
        public tblColorController(IConfiguration config)
        {
            _config = config;
        }
    
        //[HttpGet(Name = "GetColor")]
        [HttpGet("GetColor")]
        public async Task<ActionResult<List<ColorModel>>> GetColor()
        {
            var color = new List<ColorModel>();
            try
            {
        
                string connStr = _config.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();

                var command = new SqlCommand("SELECT * FROM COLOR", conn);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    color.Add(new ColorModel
                    {
                        CODE = reader.IsDBNull(0) ? null : reader.GetString(0),
                        NAME = reader.IsDBNull(1) ? null : reader.GetString(1),
                        UPDATED = reader.IsDBNull(2) ? null : reader.GetString(2),
                        LASTUSER = reader.IsDBNull(3) ? null : reader.GetString(3),
                        LASTDATE = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                        LASTTIME = reader.IsDBNull(5) ? null : reader.GetString(5),
                    });
                }

                return Ok(color);
            }
            catch(Exception ex)
            {
                var message = "EXCEPTION : " + ex.Message;
                color.Add(new ColorModel { CODE = "-1", NAME = "DefaultConnection : " + _config.GetConnectionString("DefaultConnection") + message + "   STACKSTRACE : " + ex.StackTrace });
                return Ok(color);
            }

        }
    }
}
