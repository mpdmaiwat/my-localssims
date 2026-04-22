using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Data.OleDb;

namespace SIMSBDAL
{
    public class SysNotif : cBase
    {
        #region "PROPERTIES"
        public int Id { get; set; }
        public string NotificationType { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Title { get; set; }
        public string ContentMessage { get; set; }
        public string Status { get; set; }
        public DateTime DateInsert { get; set; }
        #endregion

        public string SendToAll(string notificationType, string senderId, string status, string title = null, string contentMessage = null, int sourceId = 0)
        {
            string notifId = string.Empty;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spSEND_NOTIFICATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@notifType", notificationType);
                    cmd.Parameters.AddWithValue("@sourceId", sourceId);
                    cmd.Parameters.AddWithValue("@senderId", senderId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@contentMsg", contentMessage);
                    cmd.Parameters.AddWithValue("@notifStat", status);
                    cmd.Parameters.AddWithValue("@sendType", "A");
                    notifId = cmd.ExecuteScalar().ToString();
                }
            }
            return notifId;
        }

        public string Send(string notificationType, string senderId, string recipientId, string title, string contentMessage, string status)
        {
            string notifId = string.Empty;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spSEND_NOTIFICATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@notifType", notificationType);
                    cmd.Parameters.AddWithValue("@senderId", senderId);
                    cmd.Parameters.AddWithValue("@recipientId", recipientId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@contentMsg", ContentMessage);
                    cmd.Parameters.AddWithValue("@notifStat", status);
                    cmd.Parameters.AddWithValue("@sendType", "S");
                    notifId = cmd.ExecuteScalar().ToString();
                }
            }
            return notifId;
        }

        public string Send(string notificationType, string senderId, int notifGroupId, string title, string contentMessage, string status)
        {
            string notifId = string.Empty;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spSEND_NOTIFICATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@notifType", notificationType);
                    cmd.Parameters.AddWithValue("@senderId", senderId);
                    cmd.Parameters.AddWithValue("@notifGroupId", notifGroupId);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@contentMsg", ContentMessage);
                    cmd.Parameters.AddWithValue("@notifStat", status);
                    cmd.Parameters.AddWithValue("@sendType", "G");
                    notifId = cmd.ExecuteScalar().ToString();
                }
            }
            return notifId;
        }

        public DataTable Get(string recipientId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spGET_NOTIFICATIONS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@recipientId", recipientId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public SysNotif Open(int id)
        {
            SysNotif sysNotif = new SysNotif();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spOPEN_NOTIFICATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            sysNotif = new SysNotif
                            {
                                Id = id,
                                NotificationType = dr["notif_type"].ToString(),
                                SenderId = dr["sender_id"].ToString(),
                                RecipientId = dr["recipient_id"].ToString(),
                                Title = dr["title"].ToString(),
                                ContentMessage = dr["msg_content"].ToString(),
                                Status = dr["notif_stat"].ToString(),
                                DateInsert = Convert.ToDateTime(dr["sys_dt_ins"].ToString())
                            };
                        }
                    }
                }
            }

            return sysNotif;
        }
    }

    public class xSystem : cBase
    {
        #region "PROPERTIES"
        public bool UserStat { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public bool flgUser { get; set; }
        public string AType { get; set; }

        public string SYSTART { get; set; }

        public int DEFAULT_SEM { get; set; }


        public string AppPrefix { get; set; }
        public string StudPrefix { get; set; }

        public string SearchUserId { get; set; }
        public string SearchPassword { get; set; }

        public bool AccessEdit { get; set; }
        public bool AccessAdd { get; set; }
        public bool AccessDelete { get; set; }
        public bool AccessPreview { get; set; }
        #endregion

        #region "PAYMENT CONFIRMATION"

        public DataTable GET_PYMT_CONFIRMATION(int pageIndex = 0, int pageSize = 0)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("SYSTEM.spGET_PYMT_CONFIRMATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public void APPROVE_PAYMENT(int orderId)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("SYSTEM.spAPPROVE_PYMT", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void REJECT_PAYMENT(int orderId)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("SYSTEM.spREJECT_PYMT", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_ORDER_DETAILS(int orderId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("SYSTEM.spGET_ORDER_DETAILS", cn))
                {
                    cn.Open();
                    cmd.CommandTimeout = 360;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        #endregion

        public string GetDocumentNumber(string code)
        {
            string r = null;
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spGET_DOCUMENT_NUMBER", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@code", code);
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            int next_num = Convert.ToInt32(dr["Series"].ToString());
                            r = dr["CodePrefix"].ToString() + "-" + next_num.ToString("0000");
                        }
                    }
                }
            }

            return r;
        }

        public void UpdateDocumentNumver(string code)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spUPDATE_DOCUMENT_NUMBER", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public _AdmissionSetup GET_ADMISSION_SETUP()
        {
            _AdmissionSetup adm = new _AdmissionSetup();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [xSystem].[AdmissionSetup] WHERE Status = 1", cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        adm = new _AdmissionSetup
                        {
                            __STARTSY = Convert.ToInt32(dr["SYStart"].ToString()),
                            __ADMSY = dr["SY"].ToString(),
                            __OPENRSV = (bool)dr["Reservation"],
                            __OPENENR = (bool)dr["Enrollment"],
                            __OPENSECT = (bool)dr["OpenSection"]
                        };
                    }
                }
            }
            return adm;
        }

        public DataTable GET_SCHEDULED_CLASS(string cols = null, string where = null, string group = null, string order = null)
        {
            DataTable dt = new DataTable();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_CLASS_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cols", cols);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool CHECK_EXIST_SECT_SCHED(string lvl_code, string sect, int d)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCHECK_EXIST_SECT_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvl_code", lvl_code);
                    cmd.Parameters.AddWithValue("@sect", sect);
                    cmd.Parameters.AddWithValue("@sched_day", d);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public void INSERT_SECT_SCHED(string lvl_code, string sect, int d, DateTime time_from, DateTime time_to, string user_id)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spINSERT_SECT_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvl_code", lvl_code);
                    cmd.Parameters.AddWithValue("@sect", sect);
                    cmd.Parameters.AddWithValue("@sched_day", d);
                    cmd.Parameters.AddWithValue("@time_from", time_from);
                    cmd.Parameters.AddWithValue("@time_to", time_to);
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_SECT_SCHED(int id, DateTime time_from, DateTime time_to, string user_id)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_SECT_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@time_from", time_from);
                    cmd.Parameters.AddWithValue("@time_to", time_to);
                    cmd.Parameters.AddWithValue("@userid", user_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DELETE_SECT_SCHED(int id, string user_id)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spDELETE_CLASS_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@userid", user_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_UNASGN_SECT_ADVISER()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_UNASGN_SECT_ADVISER", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GET_COUNT_SECT_PER_LEVEL()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCOUNT_SECT_PER_LEVEL", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GET_COUNT_STUD_PER_LVLSECT()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCOUNT_STUD_PER_LVLSECT", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public void CreateModuleDetails(string col1, string col2, string opt_col3 = "", string opt_col4 = "", string opt_col5 = "", string opt_col6 = "",
                                        string opt_col7 = "", string opt_col8 = "", string opt_col9 = "", string opt_col10 = "")
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string qry = "INSERT INTO dbo.temp_mod_details VALUES (@col1, @col2, @col3, @col4, @col5, @col6, @col7, @col8, @col9, @col10)";
                using (SqlCommand cmd = new SqlCommand(qry, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@col1", col1);
                    cmd.Parameters.AddWithValue("@col2", col2);
                    cmd.Parameters.AddWithValue("@col3", opt_col3);
                    cmd.Parameters.AddWithValue("@col4", opt_col4);
                    cmd.Parameters.AddWithValue("@col5", opt_col5);
                    cmd.Parameters.AddWithValue("@col6", opt_col6);
                    cmd.Parameters.AddWithValue("@col7", opt_col7);
                    cmd.Parameters.AddWithValue("@col8", opt_col8);
                    cmd.Parameters.AddWithValue("@col9", opt_col9);
                    cmd.Parameters.AddWithValue("@col10", opt_col10);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void TruncateModuleDetails()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE dbo.temp_mod_details", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string getPassword(string USERID)
        {
            string Password = "";

            //try
            //{
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select UserId, Uname, Status, Password,AType from xSystem.UserCredentials_RF where UserId = '" + USERID + "' and Status=1 ";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        flgUser = true;
                        while (dr.Read())
                        {
                            Password = dr["Password"].ToString();
                            UserStat = (bool)dr["Status"];
                            UserName = dr["Uname"].ToString();
                            UserId = dr["UserId"].ToString();
                            AType= dr["AType"].ToString();
                        }
                    }

                    else

                    {
                        flgUser = false;
                        Password = "";
                    }
                }
            }


            return Password;

        }

        public bool CHECK_LOGIN_LOCK()
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select cast(gp_Value as bit) from Utilities.Global_Parameters where gp_Code ='LCKSYS'", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public string getUserInfo(string useridsearch)
        {
            string usrnme = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select UserId, Uname, Password from xSystem.UserCredentials_RF where Id =  '" + useridsearch + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            usrnme = dr["Uname"].ToString();
                            SearchUserId = dr["UserId"].ToString();
                            SearchPassword = dr["Password"].ToString();
                        }

                    }

                    else
                    {
                        usrnme = "";
                    }
                }
            }
            return usrnme;
        }

        public string getUsername(string searchtxt)
        {
            string usrnme = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select UserId, Uname from xSystem.UserCredentials_RF where Uname like  '%" + searchtxt + "%'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            usrnme = dr["Uname"].ToString();
                            SearchUserId = dr["UserId"].ToString();
                        }

                    }

                    else
                    {
                        usrnme = "";
                    }
                }
            }

            return usrnme;
        }

        public DataTable GET_ALL_USER()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_ALL_USER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_IT_HEAD()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select TOP 1 emp.*, pos.Position from xSystem.Employee_MF emp inner join xSystem.EmpPosition_MF pos on pos.POSID=emp.PosID where pos.Position='IT Head'", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_IT_STAFF()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select emp.*,pos.Position from xSystem.Employee_MF emp inner join xSystem.EmpPosition_MF pos on pos.POSID=emp.PosID inner join xSystem.Department_MF dept on dept.DEPTID=emp.DeptID where DeptShort='IT Department' and Position<>'IT Head' and emp.isActive=1 order by pos.emp_lvl, emp.EmpNum  asc", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public void INSERT_ACTIVITY_LOGS_WTIME(string LogType, string FlagType,string Activity, string UserId,string trans, DateTime? dtins, string transKey = null)
        {
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spINSERT_ACTIVITY_LOGS_WTIME]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@log_type", LogType);
                    cmd.Parameters.AddWithValue("@flag_type", FlagType);
                    cmd.Parameters.AddWithValue("@activity", Activity);
                    cmd.Parameters.AddWithValue("@user_id", UserId);
                    cmd.Parameters.AddWithValue("@trans", trans);
                    cmd.Parameters.AddWithValue("@transKey", transKey);
                    if (trans == "1")
                    {
                        cmd.Parameters.AddWithValue("@dtins", dtins);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@dtins", DBNull.Value);
                    }
                    
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_ACTIVITY_LOGS(string swhere, string topselect)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_ACTIVITY_LOGS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@topselect", topselect);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cn.Close();
                }
            }

            return dt;
        }

        public DataTable GET_LOG_TYPE(string swhere)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_LOG_TYPE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_MENU_UPDATES(int menuid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_CHANGE_LOG]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@menuid", menuid);
                    cmd.Parameters.AddWithValue("@transtype", "W");
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }
            return dt;
        }



        public DataTable GET_CHANGE_LOGS(string swhere)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_CHANGE_LOG]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_CHANGE_LOGS(int menuid, string clog,string userid)
        {
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_CHANGE_LOG]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@menuid", menuid);
                    cmd.Parameters.AddWithValue("@clog", clog);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@cstat", "Pending");
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void PUBLISH_CHANGE_LOG(int id)
        {
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_CHANGE_LOG]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@transtype", "P");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void EDIT_CHANGE_LOG(int id, string clog)
        {
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_CHANGE_LOG]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@clog", clog);
                    cmd.Parameters.AddWithValue("@transtype", "U");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public int INSERT_SYS_ANNOUNCEMENT(DateTime datestart, DateTime timestart, DateTime dateend, DateTime timeend, string title, string det, string userid, string systype)
        {
            int insertedId = 0;
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_SYS_ANNOUNCEMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@datestart", datestart);
                    cmd.Parameters.AddWithValue("@timestart", timestart);
                    cmd.Parameters.AddWithValue("@dateend", dateend);
                    cmd.Parameters.AddWithValue("@timeend", timeend);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@det", det);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@systype", systype);
                    cmd.Parameters.AddWithValue("@qtype", "I");
                    insertedId = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    cn.Close();
                }
            }

            return insertedId;
        }

        public void UPDATE_SYS_ANNOUNCEMENT(DateTime datestart, DateTime timestart, DateTime dateend, DateTime timeend, string title, string det, int ancid)
        {
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_SYS_ANNOUNCEMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@datestart", datestart);
                    cmd.Parameters.AddWithValue("@timestart", timestart);
                    cmd.Parameters.AddWithValue("@dateend", dateend);
                    cmd.Parameters.AddWithValue("@timeend", timeend);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@det", det);
                    cmd.Parameters.AddWithValue("@ancid", ancid);
                    cmd.Parameters.AddWithValue("@qtype", "U");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_SYS_ANNOUNCEMENT_LIST(string swhere)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_SYS_ANNOUNCEMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@qtype", "G");
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_SYS_ANNOUNCEMENT_FOR_LOGIN()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_SYS_ANNOUNCEMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@qtype", "L");
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_COMMENTS()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string sqlcon = "[xSystem].[spTRANS_COMMENT]";
                using (SqlCommand cmd = new SqlCommand(sqlcon, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GET");
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public void INSERT_NEW_POST(string postdesc, bool isanon, string userid)
        {
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_COMMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@postdesc", postdesc);
                    cmd.Parameters.AddWithValue("@isanon", isanon);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@transtype", "ADD");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_POST(int fid)
        {
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_COMMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fid", fid);
                    cmd.Parameters.AddWithValue("@transtype", "DEL");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_QUOTES()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Attendance].[spGET_QUOTES]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (SqlException)
                    {

                    }
                }
            }
            return dt;
        }

        public DataTable GET_LEVEL_TABLE()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_LEVEL]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GL");
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void UPDATE_CUR_SEM(string lvlcode, int cursem)
        {
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_LEVEL]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "US");
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@cursem", cursem);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_CUR_TERM(string lvlcode, int curterm)
        {
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_LEVEL]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "UT");
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@curterm", curterm);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        //Get the Date of SQL Server..
        public DateTime GetServerDate()
        {
            DateTime serverDate;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select getdate()";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    serverDate = (DateTime)cmd.ExecuteScalar();
                }
            }

            return serverDate;
            
        }

        public string GetActiveSY()
        {
          string SY = "";

          using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select SYStart, SYDesc, Semester from xSystem.SchoolYear_RF where Status = '" + true + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //SY = cmd.ExecuteScalar().ToString();
                            SY = dr["SYDesc"].ToString();
                            SYSTART = dr["SYStart"].ToString();
                            DEFAULT_SEM = Convert.ToInt32(dr["Semester"].ToString());
                        }
                    }
                }
            }

            return SY;
            
        }

        public int GetCurrentSY()
        {
            int sy = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string qry = "SELECT gp_Value FROM Utilities.Global_Parameters WHERE gp_Code = 'CURSY'";
                using (SqlCommand cmd = new SqlCommand(qry, cn))
                {
                    cn.Open();
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            sy = Convert.ToInt32(dr["gp_Value"].ToString());
                        }
                    }
                }
            }
            return sy;
        }

        public int GET_SEM_TERM(string transtype, string lvlcode)
        {
            int st = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_SEM_TERM]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", transtype);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cn.Open();
                    st = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return st;
        }

        public DataTable GET_SCHOOL_YEARS(string swhere)
        {
            DataTable dt = new DataTable();
            string strSQL = "Select SYStart, SYDesc FROM xSystem.SchoolYear_RF " + swhere + " group by SYStart, SYDesc  ORDER BY SYStart desc";
            dt = queryCommandDT(strSQL);
            return dt;
        }

        public int GetAsmtSY()
        {
            int sy = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string qry = "SELECT gp_Value FROM Utilities.Global_Parameters WHERE gp_Code = 'ASMTSY'";
                using (SqlCommand cmd = new SqlCommand(qry, cn))
                {
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            sy = Convert.ToInt32(dr["gp_Value"].ToString());
                        }
                    }
                }
            }
            return sy;
        }

        public int GetActiveSYTerm()
        {
            int SYterm = 1;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select term from xSystem.School_Term_RF";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();

                    SYterm = (int)cmd.ExecuteScalar();
                   
                }
            }


            return SYterm;
        }
        
        public void getMasterSetupDetails()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                
                using (SqlCommand cmd = new SqlCommand("spDisplayMasterSetup", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            //SY = cmd.ExecuteScalar().ToString();
                            AppPrefix = dr["applicant_prefix"].ToString();
                            StudPrefix = dr["Student_Prefix"].ToString();

                        }
                    }
                }
            }
        }

        public bool existTargetApplicant(string SY, string LEVELTYPECODE)
        {
            bool x = false;

              using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select SY, LevelTypeCode from xSystem.TargetStudents_MF where SY = '" + SY + "' and LevelTypeCode = '" + LEVELTYPECODE + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        x = true;

                    }
                    else
                    { x = false; }
                }
              }


              return x;
        
        }

        //Getting List of Reports Type
        public DataTable GET_REPORTS_LIST()
        {
            DataTable dt = new DataTable();
            string strSQL = "xSystem.spGET_REPORT_LIST";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;  
        }
        
        
        //LIST OF GRADE LEVEL OFFERED
        public DataTable GET_GRADE_LEVEL_LIST()
        {
            DataTable dt = new DataTable();
            string strSQL = "[xSystem].[spGET_GRADE_LEVEL_LIST]";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;  
        }


        //List of Active Teacher
        public DataTable GET_TEACHER_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[xSystem].[spGET_TEACHER_LIST]");

            return dt;
        }

        // Get the list of Subjects based on Subject Class Reference and SY
        public DataTable GET_SUBJECT_LINKED_TO_CLASSES()
        {
            var strSQL = @"SELECT 
                            Subject.Id,
                            '(' + Subject.lvl_code + ') ' + REPLACE(Subject.subj_title, '-', ' ') as 'Subject'
                            FROM Evltn.MFE003 as Subject
                            INNER JOIN xSystem.LevelType_RF as LevelType_RF ON Subject.lvl_code = LevelType_RF.LevelTypeCode
                            WHERE Subject.Id IN (
	                            SELECT subj_id FROM Registration.Subj_Class_RF as Subj_Class_RF 
	                            WHERE Subj_Class_RF.SY IN (
		                            SELECT TOP(1) SYDesc FROM xSystem.SchoolYear_RF WHERE Status = 1 
	                            )
                            )
                            ORDER BY LevelType_RF.ordr, Subject.subj_title";

            return queryCommandDT(strSQL);
        }

        //Getting Applicant Type list from database
        public DataTable getLevelCategory()
        {
            DataTable dt = new DataTable();
            //string strSQL = "Select LevelCatCode, LevelCatDesc from xSystem.LevelCategory_RF order by Arr";
            //dt = queryCommandDT(strSQL);
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_LEVEL]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    cmd.Parameters.AddWithValue("@transtype", "GC");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }

            return dt;
        }



        //Getting Applicant Level Applying
        public DataTable getApplicantLevel()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select LevelTypeCode, LevelTypeDesc from xSystem.LevelType_RF order by Arr";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        //OverLoaded
        public DataTable getApplicantLevel(string LEVELCATEGORY)
        {
            DataTable dt = new DataTable();
            string strSQL = "Select LevelTypeCode, LevelTypeDesc from xSystem.LevelType_RF where levelCatCode='" + LEVELCATEGORY + "' order by Arr";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        public DataTable DISPLAY_STRAND()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select StrandCode, StrandName from xSystem.Strand_RF order by ID";
            dt = queryCommandDT(strSQL);

            return dt;
        }


        //Get Target Applicant Total
        public DataTable getTargetApplicant()
        {
             DataTable dt = new DataTable();
            string strSQL = "Select * from xSystem.TargetStudents_MF";
            dt = queryCommandDT(strSQL);
            return dt;
        }

        //Get Slot Details Total
        //Special Stored Procedure because of Parameter

        public DataTable getSlotDetails(string SY)
        {

            string strSQL = "spSlotDetails";

            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SY", SY);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable getCountApplicant()
        {
            DataTable dt = new DataTable();
            string strSQL = "spCountCurrentApplicant";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;            
        }


        //Old-Student outstanding Balance Checker

        public Boolean checkOutstanding(string lastName, string firstName, string MI)
        {
            Boolean x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select lastName, firstname from xSystem.Uncleared_Dump where lastName=@lastName and firstname=@firstName and MI=@MI";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@MI", MI);

                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        x = true;
                    }

                    else

                    {
                        x = false;
                    }
                }
            }

            return x;
        }


        //DISPLAY TARGET STUDENT
        public DataTable DISPLAY_TARGETSTUDENTS(string _sy)
        {
            DataTable dt = new DataTable();
            //string strSQL = "spDisplayTargetStudents";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayTargetStudents", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SY", _sy);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public bool CHECK_TARGETSTUDENTS(string _sy, string _leveltype, string _strandcode)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "spCheckTargetStudents";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cmd.Parameters.AddWithValue("@LEVELTYPECODE", _leveltype);
                    cmd.Parameters.AddWithValue("@STRANDCODE", _strandcode);

                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        x = true;
                    }

                    else
                    {
                        x = false;
                    }
                }
            }

            return x;
        }

        #region GET ALL RESERVED STUDENTS
        public DataTable GET_RESERVED_STUDENT_LIST()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("_V_TESTDEV_RESERVED", cn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        da.Dispose();
                    }
                }
                return dt;
            }
            catch (Exception exc)
            {
                LogException(exc);
                return dt;
            }
        }
        #endregion

        //STATISTICS FOR HOME DASHBOARD
        public DataSet GET_STUDENT_RESERVED_STAT(string _sy)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spCOUNT_RESERVED]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SY", _sy);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

            return ds;
        }

        public DataSet GET_STUDENT_ENROLLED_STAT(string sy)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spCOUNT_ENROLLED", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_sy", sy);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }

            return ds;
        }

        /// <summary>
        /// Get Levels Assigned per Teacher or if not Teacher, depends by Level Department.
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="isTeaching"></param>
        /// <param name="allowEdit"></param>
        /// <returns>DataTable</returns>
        public DataTable GET_TEACHER_ASSIGNED_LEVELS(int empId, bool isTeaching, bool allowEdit)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_TEACHER_ASSIGNED_LEVELS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empId", empId);
                    cmd.Parameters.AddWithValue("@isTeaching", isTeaching);
                    cmd.Parameters.AddWithValue("@allowEdit", allowEdit);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }
        
        #region "TRANSACTION/ DATA MANIPULATION SECTIONS"

        /*
         INSERT TARGET STUDENTS INPUT
         * 02/01/2016 - RUSSEL VASQUEZ
         */

        public void INSERT_TARGETSTUDENTS(string _sy, string _levelcatcode, string _leveltypecode, string _strandcode,
            int _regularcount, int _ssiccount, int _studentcount, string _remarks, string _userid)
        { 
        using (SqlConnection cn = new SqlConnection(CS))
        {
            using (SqlCommand cmd = new SqlCommand("spInsertTargetStudents", cn))
            {
                cn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SY", _sy);
                cmd.Parameters.AddWithValue("@LEVELCATCODE", _levelcatcode);
                cmd.Parameters.AddWithValue("@LEVELTYPECODE", _leveltypecode);
                cmd.Parameters.AddWithValue("@STRANDCODE", _strandcode);
                cmd.Parameters.AddWithValue("@REGULARCOUNT", _regularcount);
                cmd.Parameters.AddWithValue("@SSICCOUNT", _ssiccount);
                cmd.Parameters.AddWithValue("@STUDENTCOUNT", _studentcount);
                cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                cmd.Parameters.AddWithValue("@USERID", _userid);

             
                cmd.ExecuteNonQuery();


            }
            
            }
        }

        
        /*
         UPDATE TARGET STUDENTS
         * 02/01/2016 - RUSSEL VASQUEZ
         * */
      public void UPDATE_TARGETSTUDENTS(int _id, int _regularcount, int _ssiccount, int _studentcount, string _remarks, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateTargetStudents", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@REGULARCOUNT", _regularcount);
                    cmd.Parameters.AddWithValue("@SSICCOUNT", _ssiccount);
                    cmd.Parameters.AddWithValue("@STUDENTCOUNT", _studentcount);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@USERID", _userid);


                    cmd.ExecuteNonQuery();


                }

            }
        }




/*
 TRANSACTION WITH SAP INTEGRATION SECTION
 * 
 */
      //SAP DATA ENTRY TESTING - 02/22/2016
      //REV.
      public void INSERT_SAP_BP(string _code, string _name, string _Ustudnum, string _Uname, string _Ulevel, string _Utype, string _Udescription, string _Usection,
                                 string _Uaction, string _Uprocessed, string _Uforprocess, string _UAccountCode)
      {
          using (SqlConnection cn = new SqlConnection(SAPCS))
          {
             
              using (SqlCommand cmd = new SqlCommand("spSIMSInsertBP", cn))
              {
                  cn.Open();

                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@CODE", _code);
                  cmd.Parameters.AddWithValue("@NAME", _name);
                  cmd.Parameters.AddWithValue("@U_STUDENTNO", _Ustudnum);
                  cmd.Parameters.AddWithValue("@U_NAME", _Uname);
                  cmd.Parameters.AddWithValue("@U_LEVEL", _Ulevel);
                  cmd.Parameters.AddWithValue("@U_TYPE", _Utype);
                  cmd.Parameters.AddWithValue("@U_DESCRIPTION", _Udescription);
                  cmd.Parameters.AddWithValue("@U_SECTION", _Usection);
                  cmd.Parameters.AddWithValue("@U_ACTION", _Uaction);
                  cmd.Parameters.AddWithValue("@U_PROCESSED", _Uprocessed);
                  cmd.Parameters.AddWithValue("@U_FORPROCESS", _Uforprocess);
                  cmd.Parameters.AddWithValue("@U_ACCTCODE", _UAccountCode);


                  cmd.ExecuteNonQuery();


              }

          }
      }

      public void UPDATE_SAP_BP(string _code, string _Ulevel, string _Usection, string _Uaction, string _Uprocessed, string _Uforprocess)
      {
          using (SqlConnection cn = new SqlConnection(SAPCS))
          {
              //string sqlstr= string.Format("INSERT INTO {0}(Code,Name,U_StudentNo,U_Name,U_Type,U_Description,U_Section,U_Dunning,U_Action,U_Processed,U_ForProcess,U_Level) " +  
              //               "VALUES (@code,@name,@stud_num,@type,@desc,@sec,@dunning,@action,@process,@forprocess,@level)", "@FT_OCRD");

              //using (SqlCommand cmd = new SqlCommand(string.Format("UPDATE [{0}] SET U_Level=@level,U_Section@section,U_Action=@action " +
              //                                                      "U_Processed=@processed, U_ForProcess=@forprocess WHERE Code=@Code", "@FT_OCRD"), cn))

              using (SqlCommand cmd = new SqlCommand("spSIMSUpdateBP", cn))
                {
                  cn.Open();

                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@CODE", _code);
                  cmd.Parameters.AddWithValue("@U_LEVEL", _Ulevel);
                  cmd.Parameters.AddWithValue("@U_SECTION", _Usection);
                  cmd.Parameters.AddWithValue("@U_ACTION", _Uaction);
                  cmd.Parameters.AddWithValue("@U_PROCESSED", _Uprocessed);
                  cmd.Parameters.AddWithValue("@U_FORPROCESS", _Uforprocess);

                  cmd.ExecuteNonQuery();
              }
          }
      }

      public void UPDATE_SAP_BP_SECTION(string _code, string _section)
      {
          using (SqlConnection cn = new SqlConnection(SAPCS))
          {
             using (SqlCommand cmd = new SqlCommand("spSIMSUpdateBP_SECTION", cn))
                    {
                      cn.Open();

                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.Parameters.AddWithValue("@CODE", _code);
                      cmd.Parameters.AddWithValue("@U_SECTION", _section);

                      cmd.ExecuteNonQuery();
                  }
          }
      }

      public void UPDATE_STUDENT_AGE()
      {
          using (SqlConnection cn = new SqlConnection(CS))
          {
              using (SqlCommand cmd = new SqlCommand("xSystem.AGE_UPDATE", cn))
              {
                  cn.Open();
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.ExecuteNonQuery();
              }
          }
      }

    //Refresh Enrollment list from SAP
      public void Execute_EnrollmentUpdate()
      {
          using (SqlConnection cn = new SqlConnection(CS))
          {
              using (SqlCommand cmd = new SqlCommand("spGenerateEnrolledStudentFromSAP", cn))
              {
                  cn.Open();
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.ExecuteNonQuery();
              }
          }
      }

      //Refresh Reservation list from SAP
      public void Execute_ReservationUpdate()
      {
          using (SqlConnection cn = new SqlConnection(CS))
          {
              cn.Open();
              using (SqlCommand cmd = new SqlCommand("spGenerateReserveStudentFromSAP", cn))
              {
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.ExecuteNonQuery();
              }

              //using (SqlCommand cmd = new SqlCommand("spGenerateReservedOld_ISAMS", cn))
              //{
              //    cmd.CommandType = CommandType.StoredProcedure;
              //    cmd.ExecuteNonQuery();
              //}

              //using (SqlCommand cmd = new SqlCommand("spGenerateReservedNew_ISAMS", cn))
              //{
              //    cmd.CommandType = CommandType.StoredProcedure;
              //    cmd.ExecuteNonQuery();
              //}

              //using (SqlCommand cmd = new SqlCommand("spGenerateReservationOldStud_Default", cn))
              //{
              //    cmd.CommandType = CommandType.StoredProcedure;
              //    cmd.ExecuteNonQuery();
              //}
          }
      }

        public void Execute_OpenSectioning()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spExecuteOpenSectioning", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Automatically update the tagging of Reservation and Enrollment (w/ account and extension) from data imported to database
      public void Execute_RegistrationTaggingUpdate_Import()
      {
          using (SqlConnection cn = new SqlConnection(CS))
          {
              using (SqlCommand cmd = new SqlCommand("spGenerateRegistrationTaggingFromImport", cn))
              {
                  cn.Open();
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.ExecuteNonQuery();
              }
          }
      }


/* TRANSACTION WTIH iSAMS 
 * */
      public void INSERT_SIMS_TO_iSAMS(string _studNum, string _studName, string _levelCode, string _levelType, string _dateAdmit, string _bday, string _schYear, string _appnum, string _lname, string _fname, string _mname, string _sex)

      {
          using (SqlConnection cn = new SqlConnection(ISAMSCS))
          {
              using (SqlCommand cmd = new SqlCommand("spINSERT_SIMS_TO_ISAMS_TEMP", cn))
              {
                  cn.Open();

                  cmd.CommandType = CommandType.StoredProcedure;

                  cmd.Parameters.AddWithValue("@STUDNUM", _studNum);
                  cmd.Parameters.AddWithValue("@STUDNAME", _studName);
                  cmd.Parameters.AddWithValue("@LEVELCODE", _levelCode);
                  cmd.Parameters.AddWithValue("@LEVELTYPE", _levelType);
                  cmd.Parameters.AddWithValue("@DATEADMITTED", _dateAdmit);
                  cmd.Parameters.AddWithValue("@BIRTHDATE", _bday);
                  cmd.Parameters.AddWithValue("@SCHYEAR", _schYear);
                  cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                  cmd.Parameters.AddWithValue("@LNAME", _lname);
                  cmd.Parameters.AddWithValue("@FNAME", _fname);
                  cmd.Parameters.AddWithValue("@MNAME", _mname);
                  cmd.Parameters.AddWithValue("@SEX", _sex);

                  cmd.ExecuteNonQuery();
              }
          }
      }

#endregion

        #region "GET FUNCTION"

     

        #endregion

        #region "UPDATE FUNCTION"

      public void UPDATE_TEACHER(string _teacherID, string _teacherBarcode, string _teacherName, bool _teacherStatus)
      {
          using (SqlConnection cn = new SqlConnection(CS))
          {
              using (SqlCommand cmd = new SqlCommand("xSystem.spUPDATE_TEACHER", cn))
              {
                  cn.Open();
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.AddWithValue("@EMPID", _teacherID);
                  cmd.Parameters.AddWithValue("@EMPBARCODE", _teacherBarcode);
                  cmd.Parameters.AddWithValue("@EMPNAME", _teacherName);
                  cmd.Parameters.AddWithValue("@EMPSTATUS", _teacherStatus);

                  cmd.ExecuteNonQuery();
              }
          }
      }

        #endregion

        public DataTable GET_MENU_INFO(int menuId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("xSystem.spGET_MENU_INFO", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@menuId", menuId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    } //End of Class

    //LIST OF SETUP 
    public class xScreeningScheduleSetup : cBase
    { 
    
        /*DISPLAY LIST OF SCHEDULES*/

        public DataTable GET_SCREENING_SCHED(string _sy)
        { 
         DataTable dt = new DataTable();
            //string strSQL = "spDisplayTargetStudents";
         using (SqlConnection cn = new SqlConnection(CS))
         {
             using (SqlCommand cmd = new SqlCommand("xSystem.spGET_SCREENING_SCHEDULE", cn))
             {
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@SY", _sy);

                 SqlDataAdapter da = new SqlDataAdapter();
                 da.SelectCommand = cmd;
                 da.Fill(dt);
             }

             return dt;
         }

        }

        public static DataTable GetData(string select = null, string where = null, string group = null, string order = null, string limit = null)
        {
            DataTable dt = new DataTable();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Admission.spGET_SCREENING_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        //CHECK SCHEDULE ENTRY IF EXIST
        public bool EXIST_SCREENIG_SCHED(string _leveltypecode, string _schedCode, DateTime _screeningDate, DateTime _screeningTime, int sched_id = 0)
        { 
        
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spCHECK_SCHEDULE_EXIST", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SCHED_ID", sched_id);
                    cmd.Parameters.AddWithValue("@LEVELTYPECODE", _leveltypecode);
                    cmd.Parameters.AddWithValue("@SCREENINGCODE", _schedCode);
                    cmd.Parameters.AddWithValue("@SDATE", _screeningDate);
                    cmd.Parameters.AddWithValue("@STIME", _screeningTime);
                    
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;

        }

        public static bool IsUsed(int sched_id)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spCHECK_USED_SCHEDULE", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sched_id", sched_id);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }
        

      /*
       SCHEDULING SETUP CRUD
       */

        //Insert Screening Schedule
        public int INSERT_SCREENING_SCHED(string _sy, string _levelTypeCode, string _screeningCode, DateTime _sDate,
                                            DateTime _sTime, int _schedSlot, string _sDesc, string _userCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("xSystem.spINSERT_SCREENING_SCHEDULE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cmd.Parameters.AddWithValue("@LEVELTYPECODE", _levelTypeCode);
                    cmd.Parameters.AddWithValue("@SCREENINGCODE", _screeningCode);
                    cmd.Parameters.AddWithValue("@SDATE", _sDate);
                    cmd.Parameters.AddWithValue("@STIME", _sTime);
                    cmd.Parameters.AddWithValue("@SCHEDSLOT", _schedSlot);
                    cmd.Parameters.AddWithValue("@SDESC", _sDesc);
                    cmd.Parameters.AddWithValue("@USERID", _userCode);

                    cn.Open();

                    return int.Parse(cmd.ExecuteScalar().ToString());
                }
            }

        }

        //Update Screening Schedule
        public void UPDATE_SCREENING_SCHED(DateTime _screeningDate, DateTime _screeningTime, int _schedSlot, int _slotLeft, string _sDesc,
                                           bool _status, string _userCode, int _id)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("xSystem.spUPDATE_SCREENING_SCHEDULE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SDATE", _screeningDate);
                    cmd.Parameters.AddWithValue("@STIME", _screeningTime);
                    cmd.Parameters.AddWithValue("@SCHEDSLOT", _schedSlot);
                    cmd.Parameters.AddWithValue("@SLOTLEFT", _slotLeft);
                    cmd.Parameters.AddWithValue("@SDESC", _sDesc);
                    cmd.Parameters.AddWithValue("@STATUS", _status);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);

                    //Filter
                    cmd.Parameters.AddWithValue("@ID", _id);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        
        }

        /*POST Screening Schedule TO ONLINE-SSIMS*/
        public void POST_GUIDANCE_SCHEDULE_TO_ONLINE_SSIMS(int scheduleId, string transType)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[Admsn].[spPOST_GUIDANCE_SCHEDULE_TO_ONLINE]", cn))
                {
                    cn.Open();
                    cmd.CommandTimeout = 100;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@scheduleId", scheduleId);
                    cmd.Parameters.AddWithValue("@transType", transType);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*Check if the Schedule in Online SSIMS was already assigned in admission*/
        public bool OSSIMS_SCHEDULE_WAS_ALREADY_ASSIGNED(int sched_id)
        {
            //using (SqlConnection cn = new SqlConnection(SSIDB))
            //{
            //    using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(isAlreadyAssigned, 0) FROM [ph15178183719_admission].[dbo].[ScreeningSched_RF] WHERE localSSIMSId = @sched_id", cn))
            //    {
            //        cn.Open();
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Parameters.AddWithValue("@sched_id", sched_id);

            //        var value = cmd.ExecuteScalar();
            //        if (value == null)
            //            return false;

            //        return (bool)value;
            //    }
            //}
           var dt =  GET_OSSIMS_SCHEDULE(sched_id);
            if (dt.Rows.Count <= 0 || dt == null)
                return false;

            return (bool)dt.Rows[0]["isAlreadyAssigned"];
        }

        public DataTable GET_OSSIMS_SCHEDULE(int sched_id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[Admsn].[spGET_OSSIMS_SCHEDULE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sched_id", sched_id);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }

                return dt;
            }

        }
    }


   
    public class ClassDAL : cBase
    {
      
        public DataTable DISPLAY_SUBJECT_CLASS_LIST()
        {
            DataTable dt = new DataTable();
            string strSQL = "Registration.spGET_CLASS_LIST";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;
           
        }

        public DataTable GET_CLASS_TYPE()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[Registration].[spGET_CLASS_TYPE]");
            return dt;

        }


        public void UPDATE_CLASS_LIST(int _id, string _classname, bool _status, bool _locking, string _usercode)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_CLASS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@CLASSNAME", _classname);
                    cmd.Parameters.AddWithValue("@STATUS", _status);
                    cmd.Parameters.AddWithValue("@LOCKING", _locking);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void INSERT_CLASS_LIST(string _classcode, string _classname, string _usercode)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spINSERT_CLASS_LIST", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CLASSCODE", _classcode);
                    cmd.Parameters.AddWithValue("@CLASSNAME", _classname);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }
        
    }

    public class SubjectClassDAL : cBase
    {

        public DataTable DISPLAY_SUBJECT_CLASS_LIST()
        {
            DataTable dt = new DataTable();
            string strSQL = "Registration.spGET_SUBJECT_CLASS_LIST";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;

        }


        public bool CHECK_SUBJECT_CLASS(string _classcode, string _subjCode)
          {
              bool x = false;
              using (SqlConnection cn = new SqlConnection(CS))
              {
                  using (SqlCommand cmd = new SqlCommand("Registration.spCHECK_SUBJECT_CLASS", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.Parameters.AddWithValue("@CLASSCODE", _classcode);
                      cmd.Parameters.AddWithValue("@SUBJCODE", _subjCode);

                      cn.Open();
                      x = (bool)cmd.ExecuteScalar();
                  }
              }

              return x;
          }

        public void INSERT_SUBJECT_CLASS(string _classcode, string _subjCode, string _teacherCode, string _levelCode, string _classtypeCode , string _usercode)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spINSERT_SUBJECT_CLASS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CLASSCODE", _classcode);
                    cmd.Parameters.AddWithValue("@SUBJCODE", _subjCode);
                    cmd.Parameters.AddWithValue("@TEACHERCODE", _teacherCode);
                    cmd.Parameters.AddWithValue("@LEVELCODE", _levelCode);
                    cmd.Parameters.AddWithValue("@CLASSTYPECODE", _classtypeCode);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void UPDATE_SUBJECT_CLASS(int _id, string _classcode, string _subjcode, string _teacherCode, string _levelCode, string _classTypeCode, bool _status,string _usercode)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_SUBJECT_CLASS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@CLASSCODE", _classcode);
                    cmd.Parameters.AddWithValue("@SUBJCODE", _subjcode);
                    cmd.Parameters.AddWithValue("@TEACHERCODE", _teacherCode);
                    cmd.Parameters.AddWithValue("@LEVELCODE", _levelCode);
                    cmd.Parameters.AddWithValue("@CLASSTYPECODE", _classTypeCode);
                    cmd.Parameters.AddWithValue("@STATUS", _status);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }

    }

    public class SubjectLevelTeacherDAL : cBase
    {
        public DataTable DISPLAY_SUBJECT_LEVEL_TEACHER()
        {
            DataTable dt = new DataTable();
            string strSQL = "Registration.spGET_SUBJECT_LEVEL_TEACHER";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;

        }

        

        public void UPDATE_SUBJECT_LEVEL_TEACHER(int _id,  string _levelcode, string _teachercode,bool _status,string _usercode)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_SUBJECT_LEVEL_TEACHER", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@LEVELCODE", _levelcode);
                    cmd.Parameters.AddWithValue("@TEACHERCODE", _teachercode);
                    cmd.Parameters.AddWithValue("@STATUS", _status);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }

    }


    public class TeacherDAL : cBase
    {
        public DataTable DISPLAY_TEACHER_LIST()
        {
            DataTable dt = new DataTable();
            string strSQL = "xSystem.spGET_TEACHER_LIST";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;

        }

        public DataTable DISPLAY_TEACHER_LIST_ACTIVE()
        {
            DataTable dt = new DataTable();
            string strSQL = "xSystem.spGET_TEACHER_LIST_ACTIVE";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;

        }


        public void UPDATE_TEACHER_LIST(int _id, string _teachername, int _empnum, bool _status, string _usercode)
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_TEACHER_LIST", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@TEACHERNAME", _teachername);
                    cmd.Parameters.AddWithValue("@EMPNUM", _empnum);
                    cmd.Parameters.AddWithValue("@STATUS", _status);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }

    }

    public class SubjectDAL : cBase
    {
        //public DataTable GET_SUBECT_TYPE()
        //{
        //    DataTable dt = new DataTable();
        //    dt = queryCommandDT_StoredProc("[Registration].[spGET_CLASS_TYPE]");
        //    return dt;
           
        //}

        public DataTable GET_SUBJECT_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[Registration].[spGET_SUBJECT_LIST]");
            return dt;
        }

    }


    public class SystemImportDAL : cBase
    {
        // Only *.xlsx || *.xls Files
        // Fill DataTable with data from Excel
        public DataTable GET_DATA_FROM_EXCEL(string _filepath, string _qry)
        {
            DataTable dt = new DataTable();
            string ocon = "";
            if (_filepath.Substring(_filepath.LastIndexOf(".")).ToLower() == ".xlsx")
            {
                ocon = @"Provider=Microsoft.ACE.OLEDB.12.0.Data Source=" + _filepath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
            }
            else
            {
                ocon = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _filepath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
            }
            using (OleDbConnection ocn = new OleDbConnection(ocon))
            {
                using (OleDbCommand cmd = new OleDbCommand(_qry, ocn))
                {
                    ocn.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public int CHECK_STUDNUM_SY_EXIST(_Tagging_WAccount_WExtension _tag)
        {
            //bool res = false;
            int res = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spCHECK_STUDENT_SY_TAG_RA_RW_EXIST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_regtagging", _tag.__regtagging);
                    cmd.Parameters.AddWithValue("@p_taggingcode", _tag.__tagcode);
                    cmd.Parameters.AddWithValue("@p_studnum", _tag.__studnum);
                    cmd.Parameters.AddWithValue("@p_sy", _tag.__SY);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        res = (int)dr["ID"];
                    }
                }
            }
            return res;
        }

        public int CHECK_STUDNUM_SY_EXIST(string _regtagging, string _taggingcode, string _studnum, string _sy)
        {
            //bool res = false;
            int res = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spCHECK_STUDENT_SY_TAG_RA_RW_EXIST", cn))
                {

                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_regtagging", _regtagging);
                    cmd.Parameters.AddWithValue("@p_taggingcode", _taggingcode);
                    cmd.Parameters.AddWithValue("@p_studnum", _studnum);
                    cmd.Parameters.AddWithValue("@p_sy", _sy);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        res = (int)dr["ID"];
                    }
                }
            }
            return res;
        }


        public bool CHECK_SAME_AMOUNT(_Tagging_WAccount_WExtension _tag)
        {
            bool res = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spCHECK_SAME_AMOUNT", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_regtagging", _tag.__regtagging);
                    cmd.Parameters.AddWithValue("@p_tagcode", _tag.__tagcode);
                    cmd.Parameters.AddWithValue("@p_sy", _tag.__SY);
                    cmd.Parameters.AddWithValue("@p_studnum", _tag.__studnum);
                    cmd.Parameters.AddWithValue("@p_amount", _tag.__amount);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    if ((int)dr["NumRows"] > 0)
                    {
                        res = true;
                    }
                }
            }

            return res;
        }

        public void UPDATE_AMOUNT_TAGGING(_Tagging_WAccount_WExtension _tag)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spUPDATE_AMOUNT_RA_RW", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id", _tag.__id);
                    cmd.Parameters.AddWithValue("@p_amount", _tag.__amount);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void INSERT_DATA_TAGGING(_Tagging_WAccount_WExtension _tag)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spINSERT_DATA_TAGGING_FROM_EXCEL", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_regtagging", _tag.__regtagging);
                    cmd.Parameters.AddWithValue("@p_taggingcode", _tag.__tagcode);
                    cmd.Parameters.AddWithValue("@p_sy", _tag.__SY);
                    cmd.Parameters.AddWithValue("@p_studnum", _tag.__studnum);
                    cmd.Parameters.AddWithValue("@p_amount", _tag.__amount);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_STAT_TAGGING(_Tagging_WAccount_WExtension _tag, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spUPDATE_STAT_TAGGING", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_regtagging", _tag.__regtagging);
                    cmd.Parameters.AddWithValue("@p_taggingcode", _tag.__tagcode);
                    cmd.Parameters.AddWithValue("@p_studnum", _tag.__studnum);
                    cmd.Parameters.AddWithValue("@p_userid", _userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string GET_STAT_TAGGING(_Tagging_WAccount_WExtension _tag)
        {
            string res = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spGET_STAT_FOR_TAGGING_RA_RW", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_regtagging", _tag.__regtagging);
                    cmd.Parameters.AddWithValue("@p_studnum", _tag.__studnum);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        res = dr["Stat"].ToString();
                    }
                }
            }
            return res;
        }

        public string GET_CURRENT_TAG_FROM_IMPORTED_DATA(_Tagging_WAccount_WExtension _tag)
        {
            string res = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TagCode FROM xSystem.Tagging_RA_RW WHERE ID = @p_id", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@p_id", _tag.__id);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        res = dr["TagCode"].ToString();
                    }
                }
            }
            return res;
        }

        public string GET_STUDENT_TYPE_CODE(_Tagging_WAccount_WExtension _tag)
        {
            string res = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT StudTypeCode FROM Registration.Student_MF WHERE StudNum = @p_studnum", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@p_studnum", _tag.__studnum);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        res = dr["StudTypeCode"].ToString();
                    }
                }
            }
            return res;
        }

        public void SAVE_SUMMARY_TO_TEMPTABLE(DataTable dt)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("TRUNCATE TABLE dbo.temp_import_summary", cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    string qry = "INSERT INTO dbo.temp_import_summary (_row, _colname, _value, _msg, _action) VALUES (@p_row, @p_colname, @p_value, @p_msg, @p_action)";
                    using (SqlCommand cmd = new SqlCommand(qry, cn))
                    {
                        cn.Open();
                        cmd.Parameters.AddWithValue("@p_row", row[0].ToString());
                        cmd.Parameters.AddWithValue("@p_colname", row[1].ToString());
                        cmd.Parameters.AddWithValue("@p_value", row[2].ToString());
                        cmd.Parameters.AddWithValue("@p_msg", row[3].ToString());
                        cmd.Parameters.AddWithValue("@p_action", row[4].ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    public static class LockForms_RF
    {
        public static int RAWSCORES { get; private set; } = 1;
        public static int COMPUTATION_OF_GRADES { get; private set; } = 2;
        public static int POSTING_OF_GRADES { get; private set; } = 3;
        public static int GENERATION_OF_HONORS { get; private set; } = 4;
    }

    public class LockingSchedules : cBase
    {
        #region "PROPERTIES"
        public int Id { get; set; }
        public string SY { get; set; }
        public string Description { get; set; }
        public string LockedLevels { get; set; }
        
        public string LockedSujects { get; set; }
        public string LockedSujectsDescription { get; set; }

        public DateTime LockedFrom { get; set; }
        public DateTime LockedTo { get; set; }
        public string LockedUsers { get; set; }
        public int LockedForm { get; set; }
        public bool FlagStatus { get; set; }
        public bool FlagDelete { get; set; }

        public class LockingForms
        {
            #region "PROPERTIES"
            public int Id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public int MenuId { get; set; }
            #endregion

            #region "METHODS"
            public static DataTable GetData(string where = null)
            {
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(CS))
                { 
                    using (SqlCommand cmd = new SqlCommand("xSystem.spGET_LOCK_FORMS", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@where", where);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }

            public static LockingForms GetInfo(int menuid)
            {
                LockingForms lockingForms = new LockingForms();
                using(SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("xSystem.spGET_LOCK_FORMS", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@where", string.Format("WHERE frms.menu_id = {0}", menuid));
                        using(SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                lockingForms = new LockingForms
                                {
                                    Id = Convert.ToInt32(dr["Id"].ToString()),
                                    Code = dr["frm_code"].ToString(),
                                    Description = dr["frm_desc"].ToString(),
                                    MenuId = menuid
                                };
                            }
                        }
                    }
                }

                return lockingForms;
            }
            #endregion
        }
        #endregion

        #region "METHODS"
        public static DataTable GetData(string where = null, string limit = null)
        {
            DataTable dt = new DataTable();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spGET_LOCK_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static LockingSchedules[] GetList(string where = null, string limit = null)
        {
            List<LockingSchedules> list = new List<LockingSchedules>();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spGET_LOCK_SCHED",cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                list.Add(new LockingSchedules
                                {
                                    Id = Convert.ToInt32(dr["Id"].ToString()),
                                    Description = dr["lock_desc"].ToString(),
                                    LockedLevels = dr["lock_levels"].ToString(),
                                    LockedForm = Convert.ToInt32(dr["lock_frm"].ToString()),
                                    LockedFrom = Convert.ToDateTime(dr["lock_fr"].ToString()),
                                    LockedTo = Convert.ToDateTime(dr["lock_to"].ToString()),
                                    LockedSujects = dr["lock_subjects"].ToString(),
                                    LockedSujectsDescription = dr["lock_subjectsDesc"].ToString(),
                                    LockedUsers = dr["lock_users"].ToString(),
                                    SY = dr["sy"].ToString(),
                                    FlagStatus = Convert.ToBoolean(dr["flag_stat"].ToString()),
                                    FlagDelete = Convert.ToBoolean(dr["flag_del"].ToString())
                                });
                            }
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public static LockingSchedules GetInfo(string where = null, string limit = null)
        {
            LockingSchedules lockingSchedules = new LockingSchedules();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spGET_LOCK_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@limit", "xsched.rowNum = 1");
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            lockingSchedules = new LockingSchedules
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                Description = dr["lock_desc"].ToString(),
                                LockedLevels = dr["lock_levels"].ToString(),
                                LockedForm = Convert.ToInt32(dr["lock_frm"].ToString()),
                                LockedFrom = Convert.ToDateTime(dr["lock_fr"].ToString()),
                                LockedTo = Convert.ToDateTime(dr["lock_to"].ToString()),
                                LockedUsers = dr["lock_users"].ToString(),
                                SY = dr["sy"].ToString(),
                                FlagDelete = Convert.ToBoolean(dr["flag_del"].ToString()),
                                FlagStatus = Convert.ToBoolean(dr["flag_stat"].ToString())
                            };
                        }
                    }
                }
            }

            return lockingSchedules;
        }

        public static LockingSchedules GetInfo(int id)
        {
            LockingSchedules lockingSchedules = new LockingSchedules();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spGET_LOCK_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", "WHERE sched.Id = " + id.ToString());
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            lockingSchedules = new LockingSchedules
                            {
                                Id = id,
                                Description = dr["lock_desc"].ToString(),
                                LockedLevels = dr["lock_levels"].ToString(),
                                LockedForm = Convert.ToInt32(dr["lock_frm"].ToString()),
                                LockedFrom = Convert.ToDateTime(dr["lock_fr"].ToString()),
                                LockedTo = Convert.ToDateTime(dr["lock_to"].ToString()),
                                LockedUsers = dr["lock_users"].ToString(),
                                SY = dr["sy"].ToString(),
                                FlagStatus = Convert.ToBoolean(dr["flag_stat"].ToString()),
                                FlagDelete = Convert.ToBoolean(dr["flag_del"].ToString())
                            };
                        }
                    }
                }
            }

            return lockingSchedules;
        }

        public static LockingSchedules GetSubjectsLocked(int lockForm)
        {
            var lockingSchedules = new LockingSchedules();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                var sql = @"SELECT * FROM xSystem.LockSched_MF
                    WHERE sy in (
	                    SELECT TOP (1) SYStart FROM xSystem.SchoolYear_RF WHERE Status = 1 ORDER BY SYDESC DESC
                    )
                    AND ISNULL(lock_frm, 0) = @lock_frm
                    AND ISNULL(lock_subjects, '') <> ''
                    AND ISNULL(flag_del, 0) = 0
                    AND (GETDATE() BETWEEN lock_fr AND lock_to)";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@lock_frm", lockForm);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            lockingSchedules = new LockingSchedules
                            {
                                Id = (int)dr["id"],
                                Description = dr["lock_desc"].ToString(),
                                LockedLevels = "",
                                LockedSujects = dr["lock_subjects"].ToString(),
                                LockedSujectsDescription = dr["lock_subjectsDesc"].ToString(),
                                LockedForm = Convert.ToInt32(dr["lock_frm"].ToString()),
                                LockedFrom = Convert.ToDateTime(dr["lock_fr"].ToString()),
                                LockedTo = Convert.ToDateTime(dr["lock_to"].ToString()),
                                LockedUsers = dr["lock_users"].ToString(),
                                SY = dr["sy"].ToString(),
                                FlagStatus = Convert.ToBoolean(dr["flag_stat"].ToString()),
                                FlagDelete = Convert.ToBoolean(dr["flag_del"].ToString())
                            };
                        }
                    }
                }
            }

            return lockingSchedules;
        }

        public static void InsertData(LockingSchedules lockingSchedules, string userid, bool isSubject)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spTRANS_LOCK_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "I");
                    cmd.Parameters.AddWithValue("@sy", lockingSchedules.SY);
                    cmd.Parameters.AddWithValue("@lock_desc", lockingSchedules.Description);
                   
                    if (isSubject)
                    {
                        cmd.Parameters.AddWithValue("@lock_subjects", lockingSchedules.LockedSujects);
                        cmd.Parameters.AddWithValue("@lock_subjectsDesc", lockingSchedules.LockedSujectsDescription);
                    }
                    else
                        cmd.Parameters.AddWithValue("@lock_levels", lockingSchedules.LockedLevels);

                    cmd.Parameters.AddWithValue("@lock_fr", lockingSchedules.LockedFrom);
                    cmd.Parameters.AddWithValue("@lock_to", lockingSchedules.LockedTo);
                    cmd.Parameters.AddWithValue("@lock_frm", lockingSchedules.LockedForm);
                    cmd.Parameters.AddWithValue("@lock_users", lockingSchedules.LockedUsers);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@isSubject", isSubject);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateData(LockingSchedules lockingSchedules, string userid, bool isSubject)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spTRANS_LOCK_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "U");
                    cmd.Parameters.AddWithValue("@id", lockingSchedules.Id);
                    cmd.Parameters.AddWithValue("@lock_desc", lockingSchedules.Description);

                    if (isSubject)
                    {
                        cmd.Parameters.AddWithValue("@lock_subjects", lockingSchedules.LockedSujects);
                        cmd.Parameters.AddWithValue("@lock_subjectsDesc", lockingSchedules.LockedSujectsDescription);
                    }
                    else
                        cmd.Parameters.AddWithValue("@lock_levels", lockingSchedules.LockedLevels);
                
                    cmd.Parameters.AddWithValue("@lock_fr", lockingSchedules.LockedFrom);
                    cmd.Parameters.AddWithValue("@lock_to", lockingSchedules.LockedTo);
                    cmd.Parameters.AddWithValue("@lock_users", lockingSchedules.LockedUsers);
                    cmd.Parameters.AddWithValue("@flag_stat", lockingSchedules.FlagStatus);
                    cmd.Parameters.AddWithValue("@flag_del", lockingSchedules.FlagDelete);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@isSubject", isSubject);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteData(int Id)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spTRANS_LOCK_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion
    }

    public class ActivityLogs
    {
        public int id { get; set; }
        public string transactionKey { get; set; }
        public LogType logType { get; set; }
        public string flagType { get; set; }
        public string transaction { get; set; }
        public DateTime timestamp { get; set; }
        public string userId { get; set; }
    }

    public class LogType
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }
}//End of NameSpace


