using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMSBDAL
{
    public class Attendance : cBase
    {
       
        //Getting List of Current Students
        public DataTable GET_CUSTOMER_LIST()
        {
            DataTable dt = new DataTable();
            string strSQL = "Attendance.spGET_CUSTOMERS";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;
        }


        //GET the official Time
        public DataTable GET_OFFICIAL_TIME()
        {
            DataTable dt = new DataTable();
            string strSQL = "Attendance.spGET_OFFICIAL_TIME";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;
        }

        public DataTable GET_OFFICIAL_TIME_TABLE(string select, string groupby=null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Attendance.spGET_OFFICIAL_TIME", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@group", groupby);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DateTime GET_OFFICIAL_TIME_LEVEL(string lvlcode, DateTime attdate, string type,string section)
        {
            DateTime dt;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spGET_OFFICIAL_TIME_LEVEL]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@attdate", attdate);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@section", section);
                    cn.Open();
                    if (cmd.ExecuteScalar() == DBNull.Value)
                    {
                        dt = Convert.ToDateTime("1/1/1900");
                    }
                    else
                    {
                        dt = (DateTime)cmd.ExecuteScalar();
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        //GET LIST OF ATTENDANCE TODAY
        public DataTable GET_ATTENDANCE_TODAY()
          {
              DataTable dt = new DataTable();
              string strSQL = "[Attendance].[spGET_ATTENDANCE_TODAY]";
              dt = queryCommandDT_StoredProc(strSQL);
              return dt;
          }

        public DataTable GET_ATTENDANCE_NOT_LOG_TODAY()
          {
              DataTable dt = new DataTable();
              string strSQL = "[Attendance].[spGET_ATTENDANCE_NOT_LOG_TODAY]";
              dt = queryCommandDT_StoredProc(strSQL);
              return dt;
          }

        public DataTable GET_ATTENDANCE_NOT_LOG_TODAY_RANDOM()
          {
              DataTable dt = new DataTable();
              string strSQL = "[Attendance].[spGET_ATTENDANCE_NOT_LOG_TODAY_RANDOM]";
              dt = queryCommandDT_StoredProc(strSQL);
              return dt;
          }

        public DataTable GET_ATTENDANCE_ALL_TODAY()
          { 
               DataTable dt = new DataTable();
               string strSQL = "[Attendance].[spGET_ATTENDANCE_ALL_TODAY]";
              dt = queryCommandDT_StoredProc(strSQL);
              return dt;
         
          }

        public DataTable GET_ATTENDANCE_SUMMARY()
        {
            DataTable dt = new DataTable();
            string strSQL = "[Attendance].[spGET_ATTENDANCE_SUMMARY]";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;

        }

        public DataTable GET_ATTENDANCE_ALL_DATE(DateTime _dateLog)
          {
              DataTable dt = new DataTable();
              using (SqlConnection cn = new SqlConnection(CS))
              {
                  using (SqlCommand cmd = new SqlCommand("[Attendance].[spGET_ATTENDANCE_ALL_DATE]", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.Parameters.AddWithValue("@DATELOG", _dateLog);
                     
                      SqlDataAdapter da = new SqlDataAdapter();
                      da.SelectCommand = cmd;
                      da.Fill(dt);
                  }
              }

              return dt;

          }

        public DataTable GET_SPECIAL_DAYS(string swhere = null, string sorder = null,string sselect=null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spGET_SPECIAL_DAYS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_LVL_SECTION_PER_ADVISER(int tchrid, string whattype, string lvlcode=null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spGET_LVL_SECTION_PER_ADVISER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tchrid", tchrid);
                    cmd.Parameters.AddWithValue("@whattype", whattype);
                    if(lvlcode == "L")
                    {
                        cmd.Parameters.AddWithValue("@lvlcode", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void UPDATE_OFFICIAL_TIME(int id, DateTime timein, DateTime timeout, DateTime hdtime, Boolean status, int graceper , string attsched)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spTRANS_OFFICIAL_TIME]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@timein", timein);
                    cmd.Parameters.AddWithValue("@timeout", timeout);
                    cmd.Parameters.AddWithValue("@hdtime", hdtime);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@graceper", graceper);
                    cmd.Parameters.AddWithValue("@attsched", attsched);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DELETE_SPECIAL_DAYS(int spdid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spTRANS_SPECIAL_DAYS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@spdid", spdid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void INSERT_SPECIAL_DAYS(string type, DateTime date, string title, string lvl, string username, Nullable<DateTime> timeout, Nullable<DateTime> timein, string sched,string section,string sy)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spTRANS_SPECIAL_DAYS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@lvl", lvl);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@timeout", timeout ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@timein", timein ?? Convert.DBNull);
                    cmd.Parameters.AddWithValue("@sched", sched);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_DATE_TYPES()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spTRANS_SPECIAL_DAYS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "T");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_ATTENDANCE_FOR_MANAGE(DateTime DATELOG, string lvlcode,string section)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spGET_ATTENDANCE_FOR_MANAGE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DATELOG", DATELOG);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@section", section);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_STUDENT_NO_ATTENDANCE_PER_DATE(DateTime date, string lvlcode, string section)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spGET_STUDENT_NO_ATTENDANCE_PER_DATE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@section", section);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_CUST_ATTENDANCE_ABSENT(string customerid, string barcode,DateTime date, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spINSERT_CUST_ATTENDANCE_ABSENT_DATE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CUSTOMERID", customerid);
                    cmd.Parameters.AddWithValue("@BARCODE", barcode);
                    cmd.Parameters.AddWithValue("@DATE", date);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_SCHOOL_DAYS_LVL(string lvlcode, int sem, int term , string sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spGET_SCHOOL_DAYS_PER_LVL]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_ATTENDANCE_SUMMARY(string lvlcode , string section, int mnum, int sem, int term ,string sy, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spINSERT_ATTENDANCE_SUMMARY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 36000;
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@mnum", mnum);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@username", username);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_SUSPENSION_LIST()
          {
              DataTable dt = new DataTable();
              string strSQL = "[Attendance].[spGET_SUSPENSION_ATTENDANCE]";
              dt = queryCommandDT_StoredProc(strSQL);
              return dt;
          }

        //GET COUNT
          public int GET_TOTAL_LATE()
          {
              int iCount = 0;

              using (SqlConnection cn = new SqlConnection(CS))
              {
                  using (SqlCommand cmd = new SqlCommand("Attendance.spGET_LATE_COUNT", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;
                    
                      cn.Open();
                      iCount= (int)cmd.ExecuteScalar();
                  }
              }

              return iCount;
          }

          public int GET_TOTAL_PRESENT()
          {
              int iCount = 0;

              using (SqlConnection cn = new SqlConnection(CS))
              {
                  using (SqlCommand cmd = new SqlCommand("Attendance.spGET_PRESENT_COUNT", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;

                      cn.Open();
                      iCount = (int)cmd.ExecuteScalar();
                  }
              }

              return iCount;
          }


        //CHECK IF THE CUSTOMER ALREADY TODAY

          public bool CHECK_LOG_TODAY(string _barcode)
          {
              bool x = false;
              using (SqlConnection cn = new SqlConnection(CS))
              {
                  using (SqlCommand cmd = new SqlCommand("Attendance.spCHECK_LOG_TODAY", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.Parameters.AddWithValue("@BARCODE", _barcode);
                      cn.Open();
                      x = (bool)cmd.ExecuteScalar();
                  }
              }

              return x;
          }
        
          public void INSERT_CUSTOMER_TIME_IN(string _customerID, string _barcode, DateTime _timeIn)
          {
              using (SqlConnection cn = new SqlConnection(CS))
              {

                  using (SqlCommand cmd = new SqlCommand("Attendance.[spINSERT_CUST_ATTENDANCE]", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.Parameters.AddWithValue("@CUSTOMERID", _customerID);
                      cmd.Parameters.AddWithValue("@BARCODE", _barcode);
                      cmd.Parameters.AddWithValue("@TIME_IN", _timeIn);
                     
                      cn.Open();

                      cmd.ExecuteNonQuery();
                  }
              }
          
          }

          public void INSERT_SUSPENSION(DateTime _dateApplied, string _remarks, string _usercode)
          {
              using (SqlConnection cn = new SqlConnection(CS))
              {

                  using (SqlCommand cmd = new SqlCommand("Attendance.[spINSERT_SUSPENSION_ATTENDANCE]", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.Parameters.AddWithValue("@DATEAPPLIED", _dateApplied);
                      cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                      cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                      cn.Open();

                      cmd.ExecuteNonQuery();
                  }
              }
          }

        //UPDATE CUSTOMER TIME OUT
          public void UPDATE_CUSTOMER_TIME_OUT(string _customerID, DateTime _timeOut, bool _isComplete)
          {
              using (SqlConnection cn = new SqlConnection(CS))
              {

                  using (SqlCommand cmd = new SqlCommand("[Attendance].[spUPDATE_CUST_ATTENDANCE]", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;
                      cmd.Parameters.AddWithValue("@CUSTOMERID", _customerID);
                      cmd.Parameters.AddWithValue("@TIME_OUT", _timeOut);
                      cmd.Parameters.AddWithValue("@ISCOMPLETE", _isComplete);

                      cn.Open();

                      cmd.ExecuteNonQuery();
                  }
              }
          }

          public void UPDATE_ATTENDANCE_MANAGE(string _barcode, string _customerID, DateTime _dateLog, Nullable<DateTime> _timeIn, Nullable<DateTime> _timeOut, bool _isUnderTime, bool _isComplete, bool _isAbsent, bool _isHalfday, bool _isOverWrite, string _usercode,bool _islate)
          {
              using (SqlConnection cn = new SqlConnection(CS))
              {

                  using (SqlCommand cmd = new SqlCommand("Attendance.[spUPDATE_CUST_ATTENDANCE_MANAGE]", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;

                      cmd.Parameters.AddWithValue("@CUSTOMERID", _customerID);
                      cmd.Parameters.AddWithValue("@BARCODE", _barcode);
                      cmd.Parameters.AddWithValue("@DATELOG", _dateLog);
                      cmd.Parameters.AddWithValue("@TIMEIN", _timeIn ?? Convert.DBNull);
                      cmd.Parameters.AddWithValue("@TIMEOUT", _timeOut ?? Convert.DBNull);
                      cmd.Parameters.AddWithValue("@ISUNDERTIME", _isUnderTime);
                      cmd.Parameters.AddWithValue("@ISCOMPLETE", _isComplete);
                      cmd.Parameters.AddWithValue("@ISABSENT", _isAbsent);
                      cmd.Parameters.AddWithValue("@ISHALFDAY", _isHalfday);
                      cmd.Parameters.AddWithValue("@ISOVERWRITE", _isOverWrite);
                      cmd.Parameters.AddWithValue("@USERCODE", _usercode);
                      cmd.Parameters.AddWithValue("@ISLATE", _islate);
                      cn.Open();
                      cmd.ExecuteNonQuery();
                  }
              }
          }

          public void UPDATE_OFFICIAL_TIME(int _id, DateTime _timeIn, DateTime _timeOut, string _sy, string _usercode)
          {
              using (SqlConnection cn = new SqlConnection(CS))
              {

                  using (SqlCommand cmd = new SqlCommand("Attendance.[spUPDATE_OFFICIAL_TIME]", cn))
                  {
                      cmd.CommandType = CommandType.StoredProcedure;

                      cmd.Parameters.AddWithValue("@ID", _id);
                      cmd.Parameters.AddWithValue("@TIMEIN", _timeIn);
                      cmd.Parameters.AddWithValue("@TIMEOUT", _timeOut);
                      cmd.Parameters.AddWithValue("@SY", _sy);
                      cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                      cn.Open();

                      cmd.ExecuteNonQuery();
                  }
              }
          }

        public DataTable GET_ATT_SUMMARY(string select = null, string where = null, string group = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spGET_ATT_SUMMARY_PER_SEM]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public bool CHECK_ATTENDANCE_SUMMARY_HAS_UPDATE(string lvlcode,string section, int sem, int term, string sy)
        {
            bool x;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Attendance].[spCHECK_ATTENDANCE_HAS_UPDATE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

    }
}
