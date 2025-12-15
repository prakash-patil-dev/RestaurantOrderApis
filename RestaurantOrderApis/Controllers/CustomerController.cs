using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestaurantOrderApis.Models;

namespace RestaurantOrderApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(IConfiguration config, ILogger<CustomerController> logger)
        {
            _config = config;
            _logger = logger;
        }
        
        
        [HttpGet("GetCustomers")]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
           // var Users = new List<Customer>();

            try
            {
                string connStr = _config.GetConnectionString("DefaultConnection");

                //using var conn = new SqlConnection(connStr);
                //await conn.OpenAsync();
                using (var connection = new SqlConnection(connStr))
                {
                    string query = @"select * from CUSTOMER order by FirstName ,LASTNAME asc";

                    var invLineList = (await connection.QueryAsync<Customer>(query)).ToList();
                    return Ok(invLineList);
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching invoice lines: {ex.Message}");
            }
            // return View();
        }


        //[HttpPost("InsertCustomer")]
        //public async Task<ActionResult> SaveCustomer1([FromBody] CustomerFilter customer)
        //{
        //    if (customer?.ObjCustomer == null)
        //    {
        //        return BadRequest("Customer data is required.");
        //    }

        //    try
        //    {
        //        string connStr = _config.GetConnectionString("DefaultConnection");

        //        using (var connection = new SqlConnection(connStr))
        //        {
        //            if (customer.Mode == "INSERT")
        //            {
        //                string query = @"INSERT INTO CUSTOMER (BRANCHCODE ,CODE ,PREFIX,PARTYID,FIRSTNAME, LASTNAME, ADDRESS1, ADDRESS2,ADDRESS3,CITY,
        //                             AREA,TELEPHONE,FAX,EMAIL,MOBILE,NATIONALTY,STATUS,CONTACT,DATEJOIN,EXPIRYDATE,REGAMOUNT,SALEPER,COMPNAME,
        //                             CARDNO,UPDATED,LASTUSER,LASTDATE,LASTTIME,CUSTTYPE,DISCOUNT,PRICETYPE,CUSTIMAGE,TYPE)
        //                             VALUES
        //                            (@branchCode,@code,@prefix,@partyId,@firstName,@lastName,@address1,@address2,@address3,@city,
        //                             @area,@telephone,@fax,@email,@mobile,@nationalty,@status,@contact,@dateJoin,@expiryDate,@regAmount,@salePer,@compName,
        //                             @cardNo,@updated,@lastUser,@lastDate,@lastTime,@custType,@discount,@priceType, @custImage, @type);";

        //                var result = await connection.ExecuteAsync(query, customer.ObjCustomer);

        //                if (result > 0)
        //                {
        //                    return Ok("1");
        //                }
        //                else
        //                {
        //                    return StatusCode(500, "2");
        //                }
        //            }
        //            else if(customer.Mode == "UPDATE")
        //            {
        //                string query = @"INSERT INTO CUSTOMER (BRANCHCODE ,CODE ,PREFIX,PARTYID,FIRSTNAME, LASTNAME, ADDRESS1, ADDRESS2,ADDRESS3,CITY,
        //                             AREA,TELEPHONE,FAX,EMAIL,MOBILE,NATIONALTY,STATUS,CONTACT,DATEJOIN,EXPIRYDATE,REGAMOUNT,SALEPER,COMPNAME,
        //                             CARDNO,UPDATED,LASTUSER,LASTDATE,LASTTIME,CUSTTYPE,DISCOUNT,PRICETYPE,CUSTIMAGE,TYPE)
        //                             VALUES
        //                            (@branchCode,@code,@prefix,@partyId,@firstName,@lastName,@address1,@address2,@address3,@city,
        //                             @area,@telephone,@fax,@email,@mobile,@nationalty,@status,@contact,@dateJoin,@expiryDate,@regAmount,@salePer,@compName,
        //                             @cardNo,@updated,@lastUser,@lastDate,@lastTime,@custType,@discount,@priceType, @custImage, @type);";

        //                var result = await connection.ExecuteAsync(query, customer.ObjCustomer);

        //                if (result > 0)
        //                {
        //                    return Ok("3");
        //                }
        //                else
        //                {
        //                    return StatusCode(500, "4");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error saving customer: {ex.Message}");
        //    }
        //}

        [HttpPost("InsertCustomer")]
        public async Task<ActionResult> SaveCustomer([FromBody] CustomerFilter customer)
        {
            try
            {
                if (customer?.ObjCustomer == null)
                {
                    return Ok("Customer data is required.");
                }


                string connStr = _config.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connStr))
                {
                    if (customer.Mode == "INSERT")
                    {
                        string query = @"INSERT INTO CUSTOMER (BRANCHCODE ,CODE ,PREFIX,PARTYID,FIRSTNAME, LASTNAME, ADDRESS1, ADDRESS2,ADDRESS3,CITY,
                                        AREA,TELEPHONE,FAX,EMAIL,MOBILE,NATIONALTY,STATUS,CONTACT,DATEJOIN,EXPIRYDATE,REGAMOUNT,SALEPER,COMPNAME,
                                        CARDNO,UPDATED,LASTUSER,LASTDATE,LASTTIME,CUSTTYPE,DISCOUNT,PRICETYPE,CUSTIMAGE,TYPE)
                                        VALUES
                                        (@branchCode,@code,@prefix,@partyId,@firstName,@lastName,@address1,@address2,@address3,@city,
                                        @area,@telephone,@fax,@email,@mobile,@nationalty,@status,@contact,@dateJoin,@expiryDate,@regAmount,@salePer,@compName,
                                        @cardNo,@updated,@lastUser,@lastDate,@lastTime,@custType,@discount,@priceType, @custImage, @type);";

                        var result = await connection.ExecuteAsync(query, customer.ObjCustomer);
                        return result > 0 ? Ok("Inserted") : Ok("Insert Failed");
                    }
                    else if (customer.Mode == "UPDATE")
                    {
                        string query = @"UPDATE CUSTOMER SET
                                         PREFIX = @prefix,PARTYID = @partyId,FIRSTNAME = @firstName,LASTNAME = @lastName,ADDRESS1 = @address1,
                                         ADDRESS2 = @address2,ADDRESS3 = @address3,CITY = @city,AREA = @area,TELEPHONE = @telephone,FAX = @fax,
                                         EMAIL = @email,MOBILE = @mobile,NATIONALTY = @nationalty,STATUS = @status,CONTACT = @contact,DATEJOIN = @dateJoin,
                                         EXPIRYDATE = @expiryDate,REGAMOUNT = @regAmount,SALEPER = @salePer,COMPNAME = @compName,CARDNO = @cardNo,UPDATED = @updated,
                                         LASTUSER = @lastUser,LASTDATE = @lastDate,LASTTIME = @lastTime,CUSTTYPE = @custType,DISCOUNT = @discount,PRICETYPE = @priceType,
                                         CUSTIMAGE = @custImage,TYPE = @type WHERE  CODE = @code;";

                        var result = await connection.ExecuteAsync(query, customer.ObjCustomer);
                        return result > 0 ? Ok("Updated") : Ok("Updated Failed");

                    }
                }

                // This line is unreachable but required for completeness
                return Ok("Unexpected error.");
            }
            catch (Exception ex)
            {
                return Ok($"Error saving customer: {ex.Message}");
            }

            // Final fallback for invalid mode
            // return BadRequest("Invalid customer mode.");
        }
    }
}
