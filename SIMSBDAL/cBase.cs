using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.IO;
using System.Web;

namespace SIMSBDAL
{


    public class cBase
    {

        public class _StudentFields
        {
            public string xStudNum { get; set; }
            public string xStudFullName { get; set; }
            public string xStudLastName { get; set; }
            public string xStudFirstName { get; set; }
            public string xStudMiddleName { get; set; }

        }

        public class _ApplicantFields
        {

            public string xAppNum { get; set; }
            public string xAppFullName { get; set; }
            public string xAppLastName { get; set; }
            public string xAppFirstName { get; set; }
            public string xAppMiddleName { get; set; }
        }

        #region ADMISSION SETTINGS
        public class _AdmissionSetup
        {
            public _AdmissionSetup()
            {

            }

            public int __STARTSY { get; set; }
            public string __ADMSY { get; set; }
            public bool __OPENRSV { get; set; }
            public bool __OPENENR { get; set; }
            //public bool __ENDADM { get; set; }
            public bool __OPENSECT { get; set; }

            // GLOBAL PARAMETERS
        }
        #endregion

        public class _Tagging_WAccount_WExtension
        {
            public int __id { get; set; }
            public string __regtagging { get; set; }
            public string __tagcode { get; set; }
            public string __studnum { get; set; }
            public string __SY { get; set; }
            public double __amount { get; set; }
        }

        #region ERROR HANDLING AND LOGGING
        public static void LogException(Exception exc)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/") + "ErrorLog.txt";
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    WriteError(exc, sw);
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    WriteError(exc, sw);
                }
            }
            

        }

        private static void WriteError(Exception exc, StreamWriter sw)
        {
            sw.WriteLine("******************** {0} ********************", DateTime.Now);
            if (exc.InnerException != null)
            {
                sw.Write("INNER EXCEPTION TYPE: ");
                sw.WriteLine(exc.InnerException.GetType());
                sw.Write("INNER EXCEPTION: ");
                sw.WriteLine(exc.InnerException.Message);
                sw.Write("INNER STACK TRACE: ");
                sw.WriteLine(exc.InnerException.StackTrace);
            }
            sw.Write("EXCEPTION TYPE: ");
            sw.WriteLine(exc.GetType());
            sw.Write("EXCEPTION: ");
            sw.WriteLine(exc.Message);
            sw.Write("SOURCE: ");
            sw.WriteLine(exc.Source);
            if (exc.StackTrace != null)
            {
                sw.Write("STACK TRACE: ");
                sw.WriteLine(exc.StackTrace);
                sw.WriteLine();
            }
        }
        #endregion

        public static string CS = ConfigurationManager.ConnectionStrings["CSSIMS"].ToString();

        //ISAMS CONNECTION
        public static string ISAMSCS = ConfigurationManager.ConnectionStrings["CSISAMS"].ToString();

        //SAP CONNECTION
        public static string SAPCS = ConfigurationManager.ConnectionStrings["CSSAP1"].ToString();

        //public static string AMSCS = ConfigurationManager.ConnectionStrings["CSAMS"].ToString();

        public static string SSIDB = ConfigurationManager.ConnectionStrings["SSIDB"].ToString();

        public static DataSet queryCommandDS(string sqlQuery)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }

            return ds;
        }

        public static DataSet queryCommandDS_StoredProc(string sqlQuery)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }

            return ds;
        }
        public static DataTable queryCommandDT(string sqlQuery)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public static DataTable queryCommandDT_StoredProc(string sqlQuery)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public static DataTable ISAMSqueryCommandDT_StoredProc(string sqlQuery)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(ISAMSCS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

       
    }


} //End of NameSpace


