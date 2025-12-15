using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestaurantOrderApis.Models;

namespace RestaurantOrderApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class INVHEADController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<INVHEADController> _logger;

        public INVHEADController(IConfiguration config, ILogger<INVHEADController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetOpenBills")]
        public async Task<ActionResult<List<INVHEAD>>> GetOpenBills()
        {
            try
            {
                string connStr = _config.GetConnectionString("DefaultConnection");

                //using var conn = new SqlConnection(connStr);
                //await conn.OpenAsync();
                using (var connection = new SqlConnection(connStr))
                {
                    var query = @"SELECT * FROM INVHEAD WHERE STATUS = 'O' ORDER BY TXNDT, TXNNO ASC;";

                    var invHeadList = (await connection.QueryAsync<INVHEAD>(query)).ToList();

                    return Ok(invHeadList);
                }
            }
            catch (Exception ex)
            {
                // Log error
                return default;
            }
        }


        [HttpGet("GetInvLineFromTXNNO/{txnNo}")]
        public async Task<ActionResult<List<INVLINE>>> GetInvLineFromTXNNO(double txnNo)
        {
            try
            {
                string connStr = _config.GetConnectionString("DefaultConnection");

                //using var conn = new SqlConnection(connStr);
                //await conn.OpenAsync();
                using (var connection = new SqlConnection(connStr))
                {
                    string query = @"SELECT * FROM INVLINE WHERE TXNNO = @txnNo ORDER BY TXNDT ASC";

                    var invLineList = (await connection.QueryAsync<INVLINE>(query, new { txnNo })).ToList();
                    return Ok(invLineList);
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching invoice lines: {ex.Message}");
            }
        }


        //[HttpGet("GetOpenBillsWithMaster")]
        //public async Task<ActionResult<List<INVHEAD>>> GetOpenBillsWithMaster()
        //{
        //    var InvHead = new List<INVHEAD>();
        //    try
        //    {
        //        string connStr = _config.GetConnectionString("DefaultConnection");

        //        using var conn = new SqlConnection(connStr);
        //        await conn.OpenAsync();
        //        using (var connection = new SqlConnection(connStr))
        //        {
        //            var sql = @"SELECT bm.*, bd.* FROM INVHEAD bm
        //                        LEFT JOIN INVLINE bd ON bm.TXNNO = bd.TXNNO
        //                        WHERE bm.STATUS = 'o'";

        //            var lookup = new Dictionary<double, INVHEAD>();

        //            var result = connection.Query<INVHEAD, INVLINE, INVHEAD>(
        //                sql,
        //                (head, line) =>
        //                {
        //                    if (!lookup.TryGetValue(head.TXNNO, out var invHead))
        //                    {
        //                        invHead = head;
        //                        invHead.INVLINE = new List<INVLINE>();
        //                        lookup.Add(invHead.TXNNO, invHead);
        //                    }
        //                    if (line != null)
        //                        invHead.INVLINE.Add(line);

        //                    return invHead;
        //                },
        //                 splitOn: "BRANCHCODE"
        //            );

        //            var finalList = lookup.Values.ToList();
        //            return Ok(finalList);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var message = "EXCEPTION : " + ex.Message;
        //        return Ok(InvHead);
        //        //Users.Add(new INVHEAD { Password = "-1", Username = "
        //    }
        //}


        //[HttpGet("GetOpenBills")]
        //public async Task<ActionResult<List<INVHEAD>>> GetOpenBills()
        //{
        //    var InvHead = new List<INVHEAD>();
        //    try
        //    {

        //        string connStr = _config.GetConnectionString("DefaultConnection");

        //        using var conn = new SqlConnection(connStr);
        //        await conn.OpenAsync();

        //        var command = new SqlCommand("select *  from INVHEAD where STATUS='O' Order by TXNDT,TXNNO Asc;", conn);
        //        var reader = await command.ExecuteReaderAsync();

        //        while (await reader.ReadAsync())
        //        {

        //            InvHead.Add(new INVHEAD
        //            {
        //                SUBTOTAL = reader["SUBTOTAL"] == DBNull.Value ? 0 : Convert.ToDouble(reader["SUBTOTAL"]),
        //                BRANCHCODE = reader["BRANCHCODE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["BRANCHCODE"]),
        //                TXNNO = reader["TXNNO"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TXNNO"]),
        //                TXNDT = reader["TXNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["TXNDT"]),
        //                VIPNO = reader["VIPNO"] == DBNull.Value ? string.Empty : Convert.ToString(reader["VIPNO"]),
        //                SHIFT = reader["SHIFT"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SHIFT"]),
        //                USER = reader["USER"] == DBNull.Value ? string.Empty : Convert.ToString(reader["USER"]),
        //                STAFF = reader["STAFF"] == DBNull.Value ? string.Empty : Convert.ToString(reader["STAFF"]),
        //                ITEMDISC = reader["ITEMDISC"] == DBNull.Value ? 0 : Convert.ToDouble(reader["ITEMDISC"]),
        //                BILLAMOUNT = reader["BILLAMOUNT"] == DBNull.Value ? 0 : Convert.ToDouble(reader["BILLAMOUNT"]),
        //                TOTCOSTAMT = reader["TOTCOSTAMT"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TOTCOSTAMT"]),
        //                DUEDATE = reader["DUEDATE"] == DBNull.Value ? null : Convert.ToDateTime(reader["DUEDATE"]),
        //                SPLPERC = reader["SPLPERC"] == DBNull.Value ? null : Convert.ToDouble(reader["SPLPERC"]),
        //                SPLSALE = reader["SPLSALE"] == DBNull.Value ? 0 : Convert.ToDouble(reader["SPLSALE"]),
        //                SPLNAME = reader["SPLNAME"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SPLNAME"]),
        //                DISCPERC = reader["DISCPERC"] == DBNull.Value ? 0 : Convert.ToDouble(reader["DISCPERC"]),
        //                BILLDISC = reader["BILLDISC"] == DBNull.Value ? 0 : Convert.ToDouble(reader["BILLDISC"]),
        //                BILLCASH = reader["BILLCASH"] == DBNull.Value ? 0 : Convert.ToDouble(reader["BILLCASH"]),
        //                BILLCARD = reader["BILLCARD"] == DBNull.Value ? 0 : Convert.ToDouble(reader["BILLCARD"]),
        //                DEPCASH = reader["DEPCASH"] == DBNull.Value ? 0 : Convert.ToDouble(reader["DEPCASH"]),
        //                DEPCARD = reader["DEPCARD"] == DBNull.Value ? 0 : Convert.ToDouble(reader["DEPCARD"]),
        //                DEPCURR = reader["DEPCURR"] == DBNull.Value ? 0 : Convert.ToDouble(reader["DEPCURR"]),
        //                BILLCURR = reader["BILLCURR"] == DBNull.Value ? 0 : Convert.ToDouble(reader["BILLCURR"]),
        //                EXCHRATE = reader["EXCHRATE"] == DBNull.Value ? null : Convert.ToDouble(reader["EXCHRATE"]),
        //                TIPCASH = reader["TIPCASH"] == DBNull.Value ? null : Convert.ToDouble(reader["TIPCASH"]),
        //                TIPCARD = reader["TIPCARD"] == DBNull.Value ? null : Convert.ToDouble(reader["TIPCARD"]),
        //                TIPCRDNAME = reader["TIPCRDNAME"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TIPCRDNAME"]),
        //                TAXAMT = reader["TAXAMT"] == DBNull.Value ? null : Convert.ToDouble(reader["TAXAMT"]),
        //                TAX1AMT = reader["TAX1AMT"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TAX1AMT"]),
        //                TAX2AMT = reader["TAX2AMT"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TAX2AMT"]),
        //                TAXVATAMT = reader["TAXVATAMT"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TAXVATAMT"]),
        //                TABLENO = reader["TABLENO"] == DBNull.Value ? 0 : Convert.ToDouble(reader["TABLENO"]),
        //                NOOFPERSNS = reader["NOOFPERSNS"] == DBNull.Value ? 0 : Convert.ToDouble(reader["NOOFPERSNS"]),
        //                EATTAKE = reader["EATTAKE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["EATTAKE"]),
        //                LPRINTED = reader["LPRINTED"] == DBNull.Value ? null : Convert.ToDouble(reader["LPRINTED"]),
        //                ABSORBGST = reader["ABSORBGST"] == DBNull.Value ? null : Convert.ToDouble(reader["ABSORBGST"]),
        //                GSTREFNO = reader["GSTREFNO"] == DBNull.Value ? string.Empty : Convert.ToString(reader["GSTREFNO"]),
        //                REFERENCE1 = reader["REFERENCE1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["REFERENCE1"]),
        //                REFERENCE2 = reader["REFERENCE2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["REFERENCE2"]),
        //                CUSTNAME = reader["CUSTNAME"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CUSTNAME"]),
        //                CUSTADD1 = reader["CUSTADD1"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CUSTADD1"]),
        //                CUSTADD2 = reader["CUSTADD2"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CUSTADD2"]),
        //                CUSTADD3 = reader["CUSTADD3"] == DBNull.Value ? string.Empty : Convert.ToString(reader["CUSTADD3"]),
        //                RECEIPTNO = reader["RECEIPTNO"] == DBNull.Value ? null : Convert.ToDouble(reader["RECEIPTNO"]),
        //                STATUS = reader["STATUS"] == DBNull.Value ? string.Empty : Convert.ToString(reader["STATUS"]),
        //                REMARKS = reader["REMARKS"] == DBNull.Value ? string.Empty : Convert.ToString(reader["REMARKS"]),
        //                REPRINT = reader["REPRINT"] == DBNull.Value ? null : Convert.ToDouble(reader["REPRINT"]),
        //                UPDATED = reader["UPDATED"] == DBNull.Value ? string.Empty : Convert.ToString(reader["UPDATED"]),
        //                lessamount = reader["lessamount"] == DBNull.Value ? 0 : Convert.ToDouble(reader["lessamount"]),
        //                GST = reader["GST"] == DBNull.Value ? 0 : Convert.ToDouble(reader["GST"]),
        //                tendered = reader["tendered"] == DBNull.Value ? 0 : Convert.ToDouble(reader["tendered"]),
        //                BALANCE = reader["BALANCE"] == DBNull.Value ? 0 : Convert.ToDouble(reader["BALANCE"]),
        //                LASTUSER = reader["LASTUSER"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LASTUSER"]),
        //                LASTDATE = reader["LASTDATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["LASTDATE"]),
        //                LASTTIME = reader["LASTTIME"] == DBNull.Value ? string.Empty : Convert.ToString(reader["LASTTIME"]),
        //                STATIONID = reader["STATIONID"] == DBNull.Value ? string.Empty : Convert.ToString(reader["STATIONID"]),
        //                SETTLETYPE = reader["SETTLETYPE"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SETTLETYPE"]),
        //                ROOMNO = reader["ROOMNO"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ROOMNO"]),
        //                ProviderCd = reader["ProviderCd"] == DBNull.Value ? string.Empty : Convert.ToString(reader["ProviderCd"]),
        //            });
        //        }

        //        return Ok(InvHead);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = "EXCEPTION : " + ex.Message;

        //        //Users.Add(new INVHEAD { Password = "-1", Username = "DefaultConnection : " + _config.GetConnectionString("DefaultConnection") + message + "   STACKSTRACE : " + ex.StackTrace });
        //        return Ok(InvHead);
        //    }

        //}


        [HttpPost("SaveCurrentBill")]
        public async Task<ActionResult> SaveCurrentBillDetails([FromBody] INVHEADDETAILS CurrentBill)
        {
            try
            {
                //if (customer?.ObjCustomer == null)
                //{
                //    return Ok("Customer data is required.");
                //}


                string connStr = _config.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connStr))
                {
                    await connection.OpenAsync();
                    if (CurrentBill.CurrentOpenBill.TXNNO > 0)
                    {

                        using (var transaction = connection.BeginTransaction())
                        {

                            try
                            {
                                string masterQuery = @"UPDATE INVHEAD SET 
					                                                    SUBTOTAL = @SUBTOTAL,BRANCHCODE =@BRANCHCODE,TXNDT =CONVERT(datetime2(0), @TXNDT),VIPNO =@VIPNO,SHIFT =@SHIFT,[USER] =@USER, STAFF =@STAFF,ITEMDISC =@ITEMDISC,BILLAMOUNT =@BILLAMOUNT,
                                                                        TOTCOSTAMT =@TOTCOSTAMT,DUEDATE =@DUEDATE,SPLPERC =@SPLPERC,SPLSALE =@SPLSALE,SPLNAME =@SPLNAME,DISCPERC =@DISCPERC,BILLDISC =@BILLDISC,BILLCASH =@BILLCASH,
					                                                    BILLCARD =@BILLCARD,DEPCASH =@DEPCASH,DEPCARD =@DEPCARD,DEPCURR =@DEPCURR,BILLCURR =@BILLCURR,EXCHRATE =@EXCHRATE,TIPCASH =@TIPCASH,TIPCARD =@TIPCARD,TIPCRDNAME =@TIPCRDNAME,
					                                                    TAXAMT =@TAXAMT,TAX1AMT =@TAX1AMT,TAX2AMT =@TAX2AMT,TAXVATAMT =@TAXVATAMT,TABLENO =@TABLENO,NOOFPERSNS =@NOOFPERSNS,EATTAKE =@EATTAKE,LPRINTED =@LPRINTED,ABSORBGST =@ABSORBGST,
					                                                    GSTREFNO =@GSTREFNO, REFERENCE1 =@REFERENCE1,REFERENCE2 =@REFERENCE2,CUSTNAME =@CUSTNAME,CUSTADD1 =@CUSTADD1,CUSTADD2 =@CUSTADD2,CUSTADD3 =@CUSTADD3,RECEIPTNO =@RECEIPTNO,
					                                                    STATUS =@STATUS,REMARKS =@REMARKS,REPRINT =@REPRINT,UPDATED =@UPDATED,lessamount =@lessamount,GST =@GST,tendered =@tendered,BALANCE =@BALANCE,LASTUSER =@LASTUSER,LASTDATE =CONVERT(datetime2(0), @LASTDATE),
					                                                    LASTTIME =@LASTTIME,STATIONID =@STATIONID,SETTLETYPE =@SETTLETYPE,ROOMNO =@ROOMNO,ProviderCd =@ProviderCd WHERE  TXNNO = @TXNNO;";

                                var masterId = await connection.ExecuteScalarAsync<double>(masterQuery, CurrentBill.CurrentOpenBill, transaction);
                                foreach (var detail in CurrentBill.CurrentOpenBillDetails)
                                {
                                    detail.VIPNO = CurrentBill.CurrentOpenBill.VIPNO;
                                    //detail.LASTUSER = "ADMIN";
                                    detail.LASTDATE = DateTime.Now;
                                    detail.LASTTIME = DateTime.Now.ToString("hh:mm:ss tt");
                                    //detail.STATUS = "O";
                                    //detail.BRANCHCODE = "HQ";

                                    string checkTxnItemExists = $@"SELECT CASE 
                                                                              WHEN EXISTS (SELECT 1 FROM INVLINE WHERE TXNNO = {detail.TXNNO} and ITEMCODE ='{detail.ITEMCODE}' and LINE ={detail.LINE})
                                                                              THEN CAST(1 AS BIT) 
                                                                              ELSE CAST(0 AS BIT) 
                                                                          END";
                                     
                                    bool itemExists = await connection.ExecuteScalarAsync<bool>(checkTxnItemExists, transaction: transaction);
                                    string detailQuery = string.Empty;
                                    if (itemExists)
                                    {
                                        detailQuery = @"UPDATE INVLINE SET
                                                                          BRANCHCODE=@BRANCHCODE,TXNDT= CONVERT(datetime2(0), @TXNDT),VIPNO=@VIPNO,SHIFT=@SHIFT,[USER]=@USER,STAFF=@STAFF,ITEMNAME1=@ITEMNAME1,
                                                                          ITEMNAME2=@ITEMNAME2,CATCODE=@CATCODE,SUBCATCODE=@SUBCATCODE,BRANDCODE=@BRANDCODE,UNITCODE=@UNITCODE, QUANTITY=@QUANTITY,UNITRATE=@UNITRATE,
                                                                          AMOUNT=@AMOUNT,TAXPERC=@TAXPERC,TAXVALUE=@TAXVALUE,COSTAMT=@COSTAMT,COSTAMTSPA=@COSTAMTSPA,SPLDISC=@SPLDISC,DISCPERC=@DISCPERC,DISCOUNT=@DISCOUNT,
                                                                          LESSAMT=@LESSAMT,PRINTED=@PRINTED,BPRINTED=@BPRINTED,KPRINTED=@KPRINTED,EATTAKE=@EATTAKE,STATUS=@STATUS, UPDATED =@UPDATED,
                                                                          STYLECODE=@STYLECODE,COLORCODE=@COLORCODE,SIZECODE=@SIZECODE,STARTTIME=@STARTTIME,ENDTIME=@ENDTIME,PACKAGEID=@PACKAGEID,PACKLINE=@PACKLINE,
                                                                          REQCUST=@REQCUST,REQCOMPANY=@REQCOMPANY,REQFLAG=@REQFLAG,LASTUSER=@LASTUSER,LASTDATE=CONVERT(datetime2(0), @LASTDATE),LASTTIME=@LASTTIME,REPORTFLAG=@REPORTFLAG,
                                                                          KOT=@KOT,SCANITEMCODE=@SCANITEMCODE,STATIONID=@STATIONID,PREPARED=@PREPARED,PRICETYPE=@PRICETYPE,TOPPING=@TOPPING,DEPTCODE=@DEPTCODE,
                                                                          SEASONCODE=@SEASONCODE,DINETAKEVAL=@DINETAKEVAL  WHERE TXNNO =@TXNNO and ITEMCODE=@ITEMCODE and LINE= @LINE;";
                                    }
                                    else 
                                    {
                                        detail.TXNDT = DateTime.Now;
                                        //detail.LINE = await connection.ExecuteScalarAsync<int>(@"SELECT ISNULL(MAX(LINE), 0) + 1 FROM INVLINE WHERE TXNNO = @TXNNO", transaction: transaction);
                                        detail.LINE = await connection.ExecuteScalarAsync<int>($@"SELECT ISNULL(MAX(LINE), 0) + 1 FROM INVLINE WHERE TXNNO = {detail.TXNNO}", transaction: transaction);
                                        detailQuery = @"INSERT INTO INVLINE 
                                                                           (BRANCHCODE,TXNNO,TXNDT,VIPNO,SHIFT,[USER],STAFF,LINE,ITEMCODE,ITEMNAME1,ITEMNAME2,CATCODE,SUBCATCODE,BRANDCODE,
                                                                            UNITCODE,QUANTITY,UNITRATE,AMOUNT,TAXPERC,TAXVALUE,COSTAMT,COSTAMTSPA,SPLDISC,DISCPERC,DISCOUNT,LESSAMT,PRINTED,BPRINTED,
                                                                            KPRINTED,EATTAKE,STATUS, UPDATED,STYLECODE,COLORCODE,SIZECODE,STARTTIME,ENDTIME,PACKAGEID,PACKLINE,REQCUST,REQCOMPANY, 
                                                                            REQFLAG,LASTUSER,LASTDATE,LASTTIME,REPORTFLAG,KOT,SCANITEMCODE,STATIONID,PREPARED,PRICETYPE,TOPPING,DEPTCODE,SEASONCODE,DINETAKEVAL)
                                                                         VALUES
                                                                            (@BRANCHCODE,@TXNNO,CONVERT(datetime2(0), @TXNDT),@VIPNO,@SHIFT,@USER,@STAFF,@LINE,@ITEMCODE,@ITEMNAME1,@ITEMNAME2,@CATCODE,@SUBCATCODE,@BRANDCODE,
                                                                             @UNITCODE,@QUANTITY,@UNITRATE,@AMOUNT,@TAXPERC,@TAXVALUE,@COSTAMT,@COSTAMTSPA,@SPLDISC,@DISCPERC,@DISCOUNT,@LESSAMT,@PRINTED,@BPRINTED,
                                                                             @KPRINTED,@EATTAKE,@STATUS,@UPDATED,@STYLECODE,@COLORCODE,@SIZECODE,@STARTTIME,@ENDTIME,@PACKAGEID,@PACKLINE,@REQCUST,@REQCOMPANY, 
                                                                             @REQFLAG,@LASTUSER,CONVERT(datetime2(0), @LASTDATE),@LASTTIME,@REPORTFLAG,@KOT,@SCANITEMCODE,@STATIONID,@PREPARED,@PRICETYPE,@TOPPING,@DEPTCODE,@SEASONCODE,@DINETAKEVAL);";
                                        //detailQuery = @"INSERT INTO INVLINE 
                                        //                                   (BRANCHCODE,TXNNO,TXNDT,VIPNO,SHIFT,[USER],STAFF,LINE,ITEMCODE,ITEMNAME1,ITEMNAME2,CATCODE,SUBCATCODE,BRANDCODE,
                                        //                                    UNITCODE,QUANTITY,UNITRATE,AMOUNT,TAXPERC,TAXVALUE,COSTAMT,COSTAMTSPA,SPLDISC,DISCPERC,DISCOUNT,LESSAMT,PRINTED,BPRINTED,
                                        //                                    KPRINTED,EATTAKE,STATUS, UPDATED,STYLECODE,COLORCODE,SIZECODE,STARTTIME,ENDTIME,PACKAGEID,PACKLINE,REQCUST,REQCOMPANY, 
                                        //                                    REQFLAG,LASTUSER,LASTDATE,LASTTIME,REPORTFLAG,KOT,SCANITEMCODE,STATIONID,PREPARED,PRICETYPE,TOPPING,DEPTCODE,SEASONCODE,DINETAKEVAL)
                                        //                                 VALUES
                                        //                                    (@BRANCHCODE,@TXNNO,@TXNDT,@VIPNO,@SHIFT,@USER,@STAFF,@LINE,@ITEMCODE,@ITEMNAME1,@ITEMNAME2,@CATCODE,@SUBCATCODE,@BRANDCODE,
                                        //                                     @UNITCODE,@QUANTITY,@UNITRATE,@AMOUNT,@TAXPERC,@TAXVALUE,@COSTAMT,@COSTAMTSPA,@SPLDISC,@DISCPERC,@DISCOUNT,@LESSAMT,@PRINTED,@BPRINTED,
                                        //                                     @KPRINTED,@EATTAKE,@STATUS,@UPDATED,@STYLECODE,@COLORCODE,@SIZECODE,@STARTTIME,@ENDTIME,@PACKAGEID,@PACKLINE,@REQCUST,@REQCOMPANY, 
                                        //                                     @REQFLAG,@LASTUSER,@LASTDATE,@LASTTIME,@REPORTFLAG,@KOT,@SCANITEMCODE,@STATIONID,@PREPARED,@PRICETYPE,@TOPPING,@DEPTCODE,@SEASONCODE,@DINETAKEVAL);";

                                    }
                                    if(!string.IsNullOrEmpty(detailQuery))
                                        await connection.ExecuteAsync(detailQuery, detail, transaction);
                                }

                                transaction.Commit();
                                return Ok($"{CurrentBill.CurrentOpenBill.TXNNO} : Updated");
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                return Ok(-1);
                            }
                        }
                    }
                    else
                    {
                        using (var transaction = connection.BeginTransaction())
                        {

                            try
                            {
                              
                                //bool txnExists = await connection.ExecuteScalarAsync<bool>(checkTxnExistsQuery,new { TxnNo = yourTxnNo },transaction: transaction);

                                string getMaxTxnNoQuery = "SELECT CAST(ISNULL(MAX(TXNNO), 0) AS INT) FROM INVHEAD";
                                //int maxTxnNo = await connection.ExecuteScalarAsync<int>(getMaxTxnNoQuery);
                                int maxTxnNo = await connection.ExecuteScalarAsync<int>(getMaxTxnNoQuery, transaction: transaction);

                                CurrentBill.CurrentOpenBill.TXNNO = maxTxnNo + 1;

                                string masterQuery = @"INSERT INTO INVHEAD (SUBTOTAL ,BRANCHCODE ,TXNNO,TXNDT,VIPNO,SHIFT,[USER], STAFF,ITEMDISC,BILLAMOUNT,
                                                                            TOTCOSTAMT,DUEDATE,SPLPERC,SPLSALE,SPLNAME,DISCPERC,BILLDISC,BILLCASH,BILLCARD,DEPCASH,DEPCARD,DEPCURR,BILLCURR,
                                                                            EXCHRATE,TIPCASH,TIPCARD,TIPCRDNAME,TAXAMT,TAX1AMT,TAX2AMT,TAXVATAMT,TABLENO,NOOFPERSNS,EATTAKE,LPRINTED,ABSORBGST,GSTREFNO,
				                                                            REFERENCE1,REFERENCE2,CUSTNAME,CUSTADD1,CUSTADD2,CUSTADD3,RECEIPTNO,STATUS,REMARKS,REPRINT,UPDATED,lessamount,GST,
				                                                            tendered,BALANCE,LASTUSER,LASTDATE,LASTTIME,STATIONID,SETTLETYPE,ROOMNO,ProviderCd)
                                                                     VALUES
                                                                           (@SUBTOTAL,@BRANCHCODE,@TXNNO,CONVERT(datetime2(0), @TXNDT),@VIPNO,@SHIFT,@USER,@STAFF,@ITEMDISC,@BILLAMOUNT,
                                                                            @TOTCOSTAMT,@DUEDATE,@SPLPERC,@SPLSALE,@SPLNAME,@DISCPERC,@BILLDISC,@BILLCASH,@BILLCARD,@DEPCASH,@DEPCARD,@DEPCURR,@BILLCURR,
                                                                            @EXCHRATE,@TIPCASH,@TIPCARD,@TIPCRDNAME,@TAXAMT,@TAX1AMT,@TAX2AMT,@TAXVATAMT, @TABLENO, @NOOFPERSNS,@EATTAKE,@LPRINTED,@ABSORBGST,@GSTREFNO,
				                                                            @REFERENCE1,@REFERENCE2,@CUSTNAME,@CUSTADD1,@CUSTADD2,@CUSTADD3,@RECEIPTNO,@STATUS,@REMARKS,@REPRINT,@UPDATED,@lessamount,@GST,
				                                                            @tendered,@BALANCE,@LASTUSER,CONVERT(datetime2(0), @LASTDATE),@LASTTIME,@STATIONID,@SETTLETYPE,@ROOMNO,@ProviderCd);";
                                var masterId = await connection.ExecuteScalarAsync<double>(masterQuery, CurrentBill.CurrentOpenBill, transaction);

                                int line = 0;
                                foreach (var detail in CurrentBill.CurrentOpenBillDetails)
                                {
                                    line += 1;
                                    detail.TXNNO = maxTxnNo + 1; // Link to master
                                    detail.LINE = line;
                                    detail.TXNDT = DateTime.Now;
                                    detail.VIPNO = CurrentBill.CurrentOpenBill.VIPNO;
                                   // detail.LASTUSER = "ADMIN";
                                    detail.LASTDATE = DateTime.Now;
                                    detail.LASTTIME = DateTime.Now.ToString("hh:mm:ss tt");
                                    //detail.STATUS = "O";
                                    //detail.BRANCHCODE = "HQ";
                                    string detailQuery = @"INSERT INTO INVLINE (BRANCHCODE,TXNNO,TXNDT,VIPNO,SHIFT,[USER],STAFF,LINE,ITEMCODE,ITEMNAME1,ITEMNAME2,CATCODE,SUBCATCODE,BRANDCODE,
                                                                            UNITCODE,QUANTITY,UNITRATE,AMOUNT,TAXPERC,TAXVALUE,COSTAMT,COSTAMTSPA,SPLDISC,DISCPERC,DISCOUNT,LESSAMT,PRINTED,BPRINTED,
                                                                            KPRINTED,EATTAKE,STATUS, UPDATED,STYLECODE,COLORCODE,SIZECODE,STARTTIME,ENDTIME,PACKAGEID,PACKLINE,REQCUST,REQCOMPANY, 
                                                                            REQFLAG,LASTUSER,LASTDATE,LASTTIME,REPORTFLAG,KOT,SCANITEMCODE,STATIONID,PREPARED,PRICETYPE,TOPPING,DEPTCODE,SEASONCODE,DINETAKEVAL)
                                                                         VALUES
                                                                            (@BRANCHCODE,@TXNNO,CONVERT(datetime2(0), @TXNDT),@VIPNO,@SHIFT,@USER,@STAFF,@LINE,@ITEMCODE,@ITEMNAME1,@ITEMNAME2,@CATCODE,@SUBCATCODE,@BRANDCODE,
                                                                             @UNITCODE,@QUANTITY,@UNITRATE,@AMOUNT,@TAXPERC,@TAXVALUE,@COSTAMT,@COSTAMTSPA,@SPLDISC,@DISCPERC,@DISCOUNT,@LESSAMT,@PRINTED,@BPRINTED,
                                                                             @KPRINTED,@EATTAKE,@STATUS,@UPDATED,@STYLECODE,@COLORCODE,@SIZECODE,@STARTTIME,@ENDTIME,@PACKAGEID,@PACKLINE,@REQCUST,@REQCOMPANY, 
                                                                             @REQFLAG,@LASTUSER,CONVERT(datetime2(0), @LASTDATE),@LASTTIME,@REPORTFLAG,@KOT,@SCANITEMCODE,@STATIONID,@PREPARED,@PRICETYPE,@TOPPING,@DEPTCODE,@SEASONCODE,@DINETAKEVAL);";
                                    await connection.ExecuteAsync(detailQuery, detail, transaction);
                                }

                                transaction.Commit();
                                return Ok($"{CurrentBill.CurrentOpenBill.TXNNO} : Inserted");
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                return Ok(-1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(-1);
            }
            return Ok(-1);
        }

    }
}
