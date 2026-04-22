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

          public void UPDATE_ATTENDANCE_MANAGE(string _barcode, string _customerID, DateTime _dateLog, Nullable<DateTime> _timeIn, Nullable<DateTime> _timeOut, bool _isUnderTime, bool _isComplete, bool _isAbsent, bool _isHalfday, bool _isOverWrite, string _usercode)
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

                      cn.Open();

                      cmd.ExecuteNonQuery();
                  }
              }
          }

        // 2018.01.13
        // ======================================================================================================================
          //public void UPDATE_ATTENDANCE_MANAGE(string _barcode, string _customerID, DateTime _dateLog, DateTime _timeIn, DateTime _timeOut, bool _isUnderTime, bool _isComplete, bool _isAbsent, bool _isHalfday, bool _isOverWrite, string _usercode)
          //{
          //    using (SqlConnection cn = new SqlConnection(CS))
          //    {

          //        using (SqlCommand cmd = new SqlCommand("Attendance.[spUPDATE_CUST_ATTENDANCE_MANAGE]", cn))
          //        {
          //            cmd.CommandType = CommandType.StoredProcedure;
                      
          //            cmd.Parameters.AddWithValue("@CUSTOMERID", _customerID);
          //            cmd.Parameters.AddWithValue("@BARCODE", _barcode);
          //            cmd.Parameters.AddWithValue("@DATELOG", _dateLog);
          //            cmd.Parameters.AddWithValue("@TIMEIN", _timeIn);
          //            cmd.Parameters.AddWithValue("@TIMEOUT", _timeOut);
          //            cmd.Parameters.AddWithValue("@ISUNDERTIME", _isUnderTime);
          //            cmd.Parameters.AddWithValue("@ISCOMPLETE", _isComplete);
          //            cmd.Parameters.AddWithValue("@ISABSENT", _isAbsent);
          //            cmd.Parameters.AddWithValue("@ISHALFDAY", _isHalfday);
          //            cmd.Parameters.AddWithValue("@ISOVERWRITE", _isOverWrite);
          //            cmd.Parameters.AddWithValue("@USERCODE", _usercode);

          //            cn.Open();

          //            cmd.ExecuteNonQuery();
          //        }
          //    }
          //}
          // ======================================================================================================================


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
    }
}
