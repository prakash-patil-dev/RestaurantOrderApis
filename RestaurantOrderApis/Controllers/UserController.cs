using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using RestaurantOrderApis.Models;

namespace RestaurantOrderApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<UserController> _logger;


        public UserController(IConfiguration config, ILogger<UserController> logger)
        {
            _config = config;
            _logger = logger;
        }
        public static string EncryptPassword(string input)
        {
            try
            {
                string pattern = string.Concat(Enumerable.Range(162, 10).Select(i => (char)i));

                if (string.IsNullOrWhiteSpace(input))
                    return pattern;

                StringBuilder encrypted = new StringBuilder();

                for (int i = 0; i < input.Length && i < pattern.Length; i++)
                {
                    char inputChar = input[i];
                    char patternChar = pattern[i];

                    int encoded = (int)patternChar + (int)inputChar;
                    encrypted.Append((char)encoded);
                }

                // Append any unused part of the pattern
                if (input.Length < pattern.Length)
                    encrypted.Append(pattern.Substring(input.Length));

                return encrypted.ToString();
            }
            catch(Exception ex)
            {
                return "";
            }
        }
        public static string DecryptPassword(string encrypted)
        {
            try
            {
                string pattern = string.Concat(Enumerable.Range(162, 10).Select(i => (char)i));

                if (string.IsNullOrWhiteSpace(encrypted))
                    return "";

                StringBuilder decrypted = new StringBuilder();

                for (int i = 0; i < pattern.Length && i < encrypted.Length; i++)
                {
                    char encryptedChar = encrypted[i];
                    char patternChar = pattern[i];

                    int originalAscii = (int)encryptedChar - (int)patternChar;
                    decrypted.Append((char)originalAscii);
                }
                return decrypted.ToString().TrimEnd('\0');
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        //[HttpGet(Name = "GetUsers")]
        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var Users = new List<User>();
            try
            {

                string connStr = _config.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();

                var command = new SqlCommand("SELECT * FROM tblusers", conn);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {

                    Users.Add(new User
                    {
                        Username = reader.IsDBNull(0) ? null : reader.GetString(0),
                        GroupName = reader.IsDBNull(1) ? null : reader.GetString(1),
                        MacName = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Password = reader.IsDBNull(3) ? null : DecryptPassword(reader.GetString(3)),
                        LDays = reader.IsDBNull(4) ? null : reader.GetString(4),
                        LfTime = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                        LtTime = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6),
                        UserCode = reader.IsDBNull(7) ? null : reader.GetString(7),
                        SubDisc = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                        ItemDisc = reader.IsDBNull(9) ? 0 : reader.GetDouble(9),
                        LessAmt = reader.IsDBNull(10) ? 0 : reader.GetDouble(10),

                        FreeQty = reader.IsDBNull(11) ? 0 : reader.GetInt32(11),
                        FreeVal = reader.IsDBNull(12) ? 0 : reader.GetDouble(12),
                        IdKey = reader.IsDBNull(13) ? 0 : reader.GetInt32(13),
                        Administrator = reader.IsDBNull(14) ? "" : reader.GetString(14),
                        Updated = reader.IsDBNull(15) ? "" : reader.GetString(15),
                        LanguageID = reader.IsDBNull(16) ? 0 : reader.GetInt32(16),
                        Image = reader.IsDBNull(17) ? "" : reader.GetString(17),
                    });
                }

                return Ok(Users);
            }
            catch (Exception ex)
            {
                var message = "EXCEPTION : " + ex.Message;
                
                Users.Add(new User { Password = "-1", Username = "DefaultConnection : " + _config.GetConnectionString("DefaultConnection") + message + "   STACKSTRACE : " + ex.StackTrace });
                return Ok(Users);
            }

        }


       // [HttpPost(Name = "VerifyUser")]
        [HttpPost("VerifyUser")]
        public async Task<ActionResult<User>> GetVerifyUser([FromBody] LoginRequestFilter model)
        {
            var objUser = new User();
            try
            {
                var username = model.Username;
                var password = EncryptPassword(model.Password);

                string connStr = _config.GetConnectionString("DefaultConnection");

                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();
               // string query = "SELECT * FROM tblusers WHERE username = '"+ model.Username + "' AND password = '"+ EncryptPassword(model.Password)+"'";
                string query = "SELECT * FROM tblusers WHERE username = @username AND password = @password";
                var command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);


                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    objUser = new User
                    {
                        Username = reader.IsDBNull(reader.GetOrdinal("username")) ? "" : reader.GetString(reader.GetOrdinal("username")),// reader["username"].ToString(),
                        GroupName = reader.IsDBNull(reader.GetOrdinal("groupname")) ? "" : reader.GetString(reader.GetOrdinal("groupname")),//  reader["groupname"].ToString(),
                        MacName = reader.IsDBNull(reader.GetOrdinal("macname")) ? "" : reader.GetString(reader.GetOrdinal("macname")),// reader["macname"].ToString(),
                        Password = reader.IsDBNull(reader.GetOrdinal("password")) ? "" : reader.GetString(reader.GetOrdinal("password")),//  reader["password"].ToString(),
                        LDays = reader.IsDBNull(reader.GetOrdinal("ldays")) ? "" : reader.GetString(reader.GetOrdinal("ldays")),//  reader["ldays"].ToString(),
                        LfTime = reader.IsDBNull(reader.GetOrdinal("lftime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("lftime")),
                        LtTime = reader.IsDBNull(reader.GetOrdinal("lttime")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("lttime")),
                        UserCode = reader.IsDBNull(reader.GetOrdinal("usercode")) ? null : reader.GetString(reader.GetOrdinal("usercode")),
                        SubDisc = reader.IsDBNull(reader.GetOrdinal("subdisc")) ? 0 : reader.GetDouble(reader.GetOrdinal("subdisc")),
                        ItemDisc = reader.IsDBNull(reader.GetOrdinal("itemdisc")) ? 0 : reader.GetDouble(reader.GetOrdinal("itemdisc")),
                        LessAmt = reader.IsDBNull(reader.GetOrdinal("lessamt")) ? 0 : reader.GetDouble(reader.GetOrdinal("lessamt")),
                        FreeQty = reader.IsDBNull(reader.GetOrdinal("freeqty")) ? 0 : reader.GetInt32(reader.GetOrdinal("freeqty")),
                        FreeVal = reader.IsDBNull(reader.GetOrdinal("freeval")) ? 0 : reader.GetDouble(reader.GetOrdinal("freeval")),
                        IdKey = reader.IsDBNull(reader.GetOrdinal("IDKEY")) ? 0 : reader.GetInt32(reader.GetOrdinal("IDKEY")),
                        Administrator = reader.IsDBNull(reader.GetOrdinal("administrator")) ? "" : reader.GetString(reader.GetOrdinal("administrator")),
                        Updated = reader.IsDBNull(reader.GetOrdinal("updated")) ? "" : reader.GetString(reader.GetOrdinal("updated")),
                        LanguageID = reader.IsDBNull(reader.GetOrdinal("languageID")) ? 0 : reader.GetInt32(reader.GetOrdinal("languageID")),
                        Image = reader.IsDBNull(reader.GetOrdinal("image")) ? "" : reader.GetString(reader.GetOrdinal("image"))
                    };
                }
                else
                {
                    objUser.Username = username;
                    objUser.Password = "Invalid login";
                    objUser.GroupName = "Invalid login";
                }

                return Ok(objUser);
            }
            catch (Exception ex)
            {
                var message = "EXCEPTION : " + ex.Message;
                
                objUser.Username = model.Username;
                objUser.Password = "Invalid login";
                objUser.GroupName = "Invalid login";
                objUser.MacName = "DefaultConnection : " + _config.GetConnectionString("DefaultConnection") + message + "   STACKSTRACE : " + ex.StackTrace;
                return Ok(objUser);
            }

        }
    }
}
