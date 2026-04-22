using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace SIMSBDAL
{

    //Base Class for Health
    public class baseHealth : cBase
    {
        //Properties
        #region "Property Fields"
       
            public string MEDCODE { get; set; }
            public string MEDDESC { get; set; }
            public string MEDGENERICNAME { get; set; }
            public string MEDTYPE { get; set; }
            public string MEDLEVEL { get; set; }

            //will use for portion medicine level
            public int BATCHID { get; set; }
            public bool POR_MED_STAT { get; set; }

        #endregion

        //Methods
        #region "Methods Area"
        
        public DataTable GET_MEDICINE_LIST()
            {
                DataTable dt = new DataTable();
                string strSQL = "[Health].[spGET_MEDICINE_LIST]";
                dt = queryCommandDT_StoredProc(strSQL);
                return dt;
            }
        
        public DataTable GET_MEDICINE_TYPE()
            {
                DataTable dt = new DataTable();
                string strSQL = "spGET_MEDICINE_TYPE";
                dt = queryCommandDT_StoredProc(strSQL);
                return dt;

            }
        
        public DataTable GET_MEDICINE_LEVEL()
            {
                DataTable dt = new DataTable();
                string strSQL = "spGET_MEDICINE_LEVEL";
                dt = queryCommandDT_StoredProc(strSQL);
                return dt;

            }

        public string GET_MEDICINE_LEVEL(string _medCode)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spGET_MEDICINE_QUERY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", " medLevelCode" );
                    cmd.Parameters.AddWithValue("@where", " where medCode ='" + _medCode + "'");
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

         
        public DataTable GET_MEDICINE_EXPIRATION_LIST(string _medCode)
        {
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("[Health].[spGET_MEDICINE_BATCH]", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MEDCODE", _medCode);


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);
                    }
                }

                return dt;
        }


        #endregion

        public DataTable GET_NON_COUNTABLES(string swhere)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_NON_COUNTABLES]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "G");
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public void INSERT_NON_COUNTABLES(string nccode,string ncdesc)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_NON_COUNTABLES]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@transtype", "I");
                    cmd.Parameters.AddWithValue("@nccode", nccode);
                    cmd.Parameters.AddWithValue("@ncdesc", ncdesc);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_NON_COUNTABLES(string nccode, string ncdesc, bool flagdel, int ncid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_NON_COUNTABLES]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "U");
                    cmd.Parameters.AddWithValue("@nccode", nccode);
                    cmd.Parameters.AddWithValue("@ncdesc", ncdesc);
                    cmd.Parameters.AddWithValue("@flagdel", flagdel);
                    cmd.Parameters.AddWithValue("@ncid", ncid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_EXISTS_NC_INSERT(string nccode)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_NON_COUNTABLES]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CI");
                    cmd.Parameters.AddWithValue("@nccode", nccode);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public bool CHECK_EXISTS_NC_UPDATE(string nccode, int ncid)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_NON_COUNTABLES]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CU");
                    cmd.Parameters.AddWithValue("@nccode", nccode);
                    cmd.Parameters.AddWithValue("@ncid", ncid);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public DataTable GET_MEDICINE_BATCH(string swhere)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_BATCH_MEDICINE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "G");
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_MEDICINES(string transtype)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_BATCH_MEDICINE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", transtype);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public string INSERT_MEDICINE_BATCH(int medid,DateTime medexp,int begbal, int closebal,string medstat, string remarks, bool wportion, int whid)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "I");
                    cmd.Parameters.AddWithValue("@medid", medid);
                    cmd.Parameters.AddWithValue("@medexp", medexp);
                    cmd.Parameters.AddWithValue("@begbal", begbal);
                    cmd.Parameters.AddWithValue("@closebal", closebal);
                    cmd.Parameters.AddWithValue("@medstat", medstat);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@wportion", wportion);
                    cmd.Parameters.AddWithValue("@whid", whid);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void UPDATE_MEDICINE_BATCH(int msid, DateTime medexp, string remarks, bool wportion)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "U");
                    cmd.Parameters.AddWithValue("@msid", msid);
                    cmd.Parameters.AddWithValue("@medexp", medexp);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@wportion", wportion);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void ADD_STOCK(int addstock, int msid,string remarks, string userid, bool isportion)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "AB");
                    cmd.Parameters.AddWithValue("@msid", msid);
                    cmd.Parameters.AddWithValue("@addstock", addstock);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@isportion", isportion);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void RELEASE_STOCK(int remstock, int msid, string remarks,string userid, DateTime daterelease, bool isportion)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "AR");
                    cmd.Parameters.AddWithValue("@msid", msid);
                    cmd.Parameters.AddWithValue("@remstock", remstock);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@daterelease", daterelease);
                    cmd.Parameters.AddWithValue("@isportion", isportion);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_BATCH_TRANSACTIONS(int msid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_BATCH_MEDICINE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GT");
                    cmd.Parameters.AddWithValue("@msid", msid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_CLINIC_WAREHOUSE(bool condition, bool isforrelease)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_BATCH_MEDICINE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (condition == true)
                    {
                        cmd.Parameters.AddWithValue("@transtype", "WW");
                        cmd.Parameters.AddWithValue("@isforrelease", isforrelease);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@transtype", "GW");
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_ITEM_TYPE_CLINIC()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_BATCH_MEDICINE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GY");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_CLINIC_WAREHOUSE_FOR_TRANSFER(int whid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_BATCH_MEDICINE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "WH");
                    cmd.Parameters.AddWithValue("@whid", whid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public int GET_CLOSING_BAL_RECENT(int msid)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GB");
                    cmd.Parameters.AddWithValue("@msid", msid);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public bool CHECK_EXISTS_BATCH(int medid,DateTime medexp, int whid)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CE");
                    cmd.Parameters.AddWithValue("@medid", medid);
                    cmd.Parameters.AddWithValue("@medexp", medexp);
                    cmd.Parameters.AddWithValue("@whid", whid);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_TRANSACTION_HISTORY(int msid, bool isportion, int remstock, DateTime daterelease, string remarks,string userid, string trans)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "VA");
                    cmd.Parameters.AddWithValue("@msid", msid);
                    cmd.Parameters.AddWithValue("@remstock", remstock);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@daterelease", daterelease);
                    cmd.Parameters.AddWithValue("@isportion", isportion);
                    cmd.Parameters.AddWithValue("@trans", trans);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public int GET_MSID(string batchcode)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GI");
                    cmd.Parameters.AddWithValue("@batchcode", batchcode);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public int GET_EXISTS_MSID(int medid, DateTime medexp, int whid)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CI");
                    cmd.Parameters.AddWithValue("@medid", medid);
                    cmd.Parameters.AddWithValue("@medexp", medexp);
                    cmd.Parameters.AddWithValue("@whid", whid);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public string GET_BATCHCODE_VIA_MSID(int msid)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_BATCH_MEDICINE]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "BC");
                    cmd.Parameters.AddWithValue("@msid", msid);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }
    }

    

    public class PatientInfo: baseHealth
    {
        public DataTable GET_STUDENT_PATIENT_RECORDS(string studnum, string sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GRS");
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_EMPLOYEE_PATIENT_RECORDS(int empid, string sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GRE");
                    cmd.Parameters.AddWithValue("@empid", empid);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_COMPLAINT_LIST()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GC");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_PATIENT_COMPLAINTS(int pcid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GPC");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_PATIENT_SUMMARY(int pcid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GPS");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public void DELETE_PATIENT_COMPLAINT(int cid, int pcid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "DPC");
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_PATIENT_NC(int ncid, int pcid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "DPN");
                    cmd.Parameters.AddWithValue("@ncid", ncid);
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_PATIENT_MEDICINE(int pmid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "DPM");
                    cmd.Parameters.AddWithValue("@pmid", pmid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_TIME_INCIDENTS()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GTI");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_PLACE_INCIDENTS()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GPI");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_PATIENT_MEDICINES(int pcid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GPM");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_PATIENT_AVAILABLE_MEDS(int whid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GMW");
                    cmd.Parameters.AddWithValue("@whid", whid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_PATIENT_NON_COUNTABLES(int pcid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GPN");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_PATIENT_INCIDENT(int pcid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GPD");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_PATIENT_EXAMINE(int pcid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GPE");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public int INSERT_PATIENT_SUMMARY(string transnum, string sy, string ptype, DateTime compdate, DateTime comptime, Boolean senthome, Boolean senthospi, string remarks, string userid, string studnum,int empid, string complaintRemarks = "", bool isSentClassroom = false)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "IPS");
                    cmd.Parameters.AddWithValue("@transnum", transnum);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@ptype", ptype);
                    cmd.Parameters.AddWithValue("@compdate", compdate);
                    cmd.Parameters.AddWithValue("@comptime", comptime);
                    cmd.Parameters.AddWithValue("@senthome", senthome);
                    cmd.Parameters.AddWithValue("@senthospi", senthospi);
                    cmd.Parameters.AddWithValue("@isSentClassroom", isSentClassroom);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@empid", empid);
                    cmd.Parameters.AddWithValue("@complaintRemarks", complaintRemarks);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_PATIENT_COMPLAINT(int cid, int pcid, string remarks = "")
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "IPC");
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cmd.Parameters.AddWithValue("@remarks", remarks);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_PATIENT_INCIDENT(int tid, int pcid, int pid, string incphysician, string incremarks)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "IPI");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cmd.Parameters.AddWithValue("@tid", tid);
                    cmd.Parameters.AddWithValue("@pid", pid);
                    cmd.Parameters.AddWithValue("@incphysician", incphysician);
                    cmd.Parameters.AddWithValue("@incremarks", incremarks);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_PATIENT_SEEN_EXAMINE(int pcid, string seenphysician, string seendiagnosis, string seenorder)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "IPE");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cmd.Parameters.AddWithValue("@seenphysician", seenphysician);
                    cmd.Parameters.AddWithValue("@seendiagnosis", seendiagnosis);
                    cmd.Parameters.AddWithValue("@seenorder", seenorder);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_PATIENT_NC(int ncid, int pcid, string remarks = "")
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "IPN");
                    cmd.Parameters.AddWithValue("@ncid", ncid);
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_PATIENT_SUMMARY(DateTime compdate, DateTime comptime, Boolean senthome, Boolean senthospi, string remarks, string userid, int pcid, string complaintRemarks="", bool isSentClassroom = false)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "UPS");
                    cmd.Parameters.AddWithValue("@compdate", compdate);
                    cmd.Parameters.AddWithValue("@comptime", comptime);
                    cmd.Parameters.AddWithValue("@senthome", senthome);
                    cmd.Parameters.AddWithValue("@senthospi", senthospi);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cmd.Parameters.AddWithValue("@complaintRemarks", complaintRemarks);
                    cmd.Parameters.AddWithValue("@isSentClassroom", isSentClassroom);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_PATIENT_SEEN_EXAMINE(int pcid, string seenphysician, string seendiagnosis, string seenorder)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "UPE");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cmd.Parameters.AddWithValue("@seenphysician", seenphysician);
                    cmd.Parameters.AddWithValue("@seendiagnosis", seendiagnosis);
                    cmd.Parameters.AddWithValue("@seenorder", seenorder);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_PATIENT_INCIDENT(int tid, int pcid, int pid, string incphysician, string incremarks)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "UPI");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cmd.Parameters.AddWithValue("@tid", tid);
                    cmd.Parameters.AddWithValue("@pid", pid);
                    cmd.Parameters.AddWithValue("@incphysician", incphysician);
                    cmd.Parameters.AddWithValue("@incremarks", incremarks);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_PATIENT_MEDICINE(int pcid, int bmid, bool isportion, int qty)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "IPM");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cmd.Parameters.AddWithValue("@bmid", bmid);
                    cmd.Parameters.AddWithValue("@isportion", isportion);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_PATIENT_RECORD_LOGS(string transnum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_PATIENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GPL");
                    cmd.Parameters.AddWithValue("@transnum", transnum);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public bool CHECK_EXISTS_PATIENT_INCIDENT(int pcid)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spTRANS_PATIENT]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CEI");
                    cmd.Parameters.AddWithValue("@pcid", pcid);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }
    }
    

    /*Inherit from Base Class
     *Module Class of Health Information 
     */
    public class HealthInformation: baseHealth
    {
       
        #region "GET-SELECT FUNCTIONS"

        public DataSet RET_STUDENT_HEALTH_DETAILS(string _SNUM)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Health.spGET_STUDENT_HEALTH", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SNUM", _SNUM);
                    cmd.Parameters.AddWithValue("@htype", "STUDENT");

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }
            return ds;
        }

        public DataSet RET_APPLICANT_HEALTH_DETAILS(string _ANUM)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Health.spGET_STUDENT_HEALTH", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ANUM", _ANUM);
                    cmd.Parameters.AddWithValue("@htype", "APPLICANT");

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }
            return ds;
        }

        public DataSet GET_HEALTH_INFO(string _ANUM)
        {

            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Health.Stud_Health_Info_MF WHERE ANUM = @ANUM", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ANUM", _ANUM);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

            return ds;
        }

        public DataTable GET_APPLICANT_CLINIC_LIST()
        {

            DataTable dt = new DataTable();
            string strSQL = "Health.spGET_ApplicantClinicList";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;

        }

        public DataTable GET_STUDENT_ILLNESS()
        {
            DataTable dt = new DataTable();
            string strSQL = "SELECT * from xSystem.Health_Illness_RF Order by Arr";
            dt = queryCommandDT(strSQL);
            return dt;
        }

        public DataTable GET_STUDENT_MEDICINE_GIVEN()
        {
            DataTable dt = new DataTable();
            string strSQL = "SELECT * from xSystem.Health_GivenMed_RF WHERE IsActive = 1 Order by Arr";
            dt = queryCommandDT(strSQL);
            return dt;
        }

        public DataTable GET_STUDENT_VACCINE()
        {
            DataTable dt = new DataTable();
            string strSQL = "SELECT * from xSystem.Health_Vaccine_RF Order by Vac_Arr";
            dt = queryCommandDT(strSQL);
            return dt;
        }
        
        public bool GET_EXIST_HEALTH_RECORD(string _SNUM)
        {

            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select snum from Health.Stud_Health_Info_MF where SNUM= '" + _SNUM + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
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

                    return x;
                }
            }

        }
        
        public bool GET_EXIST_HEALTH_RECORD_APPLICANT(string _ANUM)
        {

            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select anum from Health.Stud_Health_Info_MF where ANUM= '" + _ANUM + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
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

                    return x;
                }
            }

        }

        public bool GET_EXIST_HEALTH_RECORD_STUDENT(string _SNUM)
        {

            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select snum from Health.Stud_Health_Info_MF where SNUM= '" + _SNUM + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
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

                    return x;
                }
            }

        }

        #endregion


        #region "INSERT-UPDATE-DELETE FUNCTIONS"


        //STUDENT HEALTH
        public void INSERT_STUDENT_ILLNESS(string _snum, string _illnesscode, bool _ischecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "INSERT INTO Health.Stud_Illness_MF(SNUM,IllnessCode,IsChecked) " +
                                "VALUES(@SNUM, @IllnessCode, @IsChecked)";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@SNUM", _snum);
                    cmd.Parameters.AddWithValue("@IllnessCode", _illnesscode);
                    cmd.Parameters.AddWithValue("@IsChecked", _ischecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }



        }

        public void INSERT_STUDENT_MEDICINE_GIVEN(string _snum, string _medCode, bool _ischecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "INSERT INTO Health.Stud_GivenMed_MF(SNUM,MedCode,IsChecked) " +
                                "VALUES(@SNUM, @MedCode, @IsChecked)";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@SNUM", _snum);
                    cmd.Parameters.AddWithValue("@MedCode", _medCode);
                    cmd.Parameters.AddWithValue("@IsChecked", _ischecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }

        }

        public void INSERT_STUDENT_VACCINE(string _snum, string _vaccode, bool _ischecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "INSERT INTO Health.Stud_Vaccine_MF(SNUM,VacCode,isChecked) " +
                                "VALUES(@SNUM, @VacCode, @IsChecked)";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@SNUM", _snum);
                    cmd.Parameters.AddWithValue("@VacCode", _vaccode);
                    cmd.Parameters.AddWithValue("@IsChecked", _ischecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }

        }

        public void INSERT_STUDENT_HEALTH_DETAILS(string _snum, bool _iscongenital, string _congenital,
                                        bool _ishospitalized, string _dateHospitalized, string _reasonhospitalized,
                                        bool _isminormajor, string _minormajor, string _dateminormajor,
                                        bool _isaccident, string _accident, string _dateaccident, string _parentRemarks,
                                        string _nurserRemarks, string _illOthers, string _HEALTHSTATUSCODE, string _USERID,string vacothers)
        {
            
            using (SqlConnection cn = new SqlConnection(CS))
            {
                
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_HEALTH_INFO]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SNUM", _snum);
                    cmd.Parameters.AddWithValue("@htype", "STUDENT");
                    cmd.Parameters.AddWithValue("@savetype", "N");
                    cmd.Parameters.AddWithValue("@IsCongenital", _iscongenital);
                    cmd.Parameters.AddWithValue("@CongenitalDesc", _congenital);
                    cmd.Parameters.AddWithValue("@IsHospitalized", _ishospitalized);
                    cmd.Parameters.AddWithValue("@DateHospitalized", _dateHospitalized);
                    
                    cmd.Parameters.AddWithValue("@ReasonHospitalized", _reasonhospitalized);
                    cmd.Parameters.AddWithValue("@IsMinorMajor", _isminormajor);
                    cmd.Parameters.AddWithValue("@MinorMajorDesc", _minormajor);
                    cmd.Parameters.AddWithValue("@MinorMajorDate", _dateminormajor);

                    cmd.Parameters.AddWithValue("@IsSeriousAccident", _isaccident);
                    cmd.Parameters.AddWithValue("@SeriousAccidentDesc", _accident);
                    cmd.Parameters.AddWithValue("@SeriousAccidentDate", _dateaccident);

                    cmd.Parameters.AddWithValue("@ParentRemarks", _parentRemarks);
                    cmd.Parameters.AddWithValue("@NurseRemarks", _nurserRemarks);
                    cmd.Parameters.AddWithValue("@illOthers", _illOthers);
                    cmd.Parameters.AddWithValue("@HEALTHSTATUSCODE", _HEALTHSTATUSCODE);
                    
                    cmd.Parameters.AddWithValue("@UserID", _USERID);
                    cmd.Parameters.AddWithValue("@VACOTHERS", vacothers);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        
        public void UPDATE_STUDENT_ILLNESS(string _snum, string _illnesscode, bool _isChecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "UPDATE Health.Stud_Illness_MF SET IsChecked=@isChecked " +
                                "WHERE SNUM=@snum and IllnessCode=@IllnessCode ";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@snum", _snum);
                    cmd.Parameters.AddWithValue("@IllnessCode", _illnesscode);
                    cmd.Parameters.AddWithValue("@isChecked", _isChecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_STUDENT_MEDICINE_GIVEN(string _snum, string _medCode, bool _isChecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "UPDATE Health.Stud_GivenMed_MF SET IsChecked=@isChecked " +
                                "WHERE SNUM=@snum and MedCode=@MedCode";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@snum", _snum);
                    cmd.Parameters.AddWithValue("@MedCode", _medCode);
                    cmd.Parameters.AddWithValue("@isChecked", _isChecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_STUDENT_VACCINE(string _snum, string _vaccode, bool _isChecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "UPDATE Health.Stud_Vaccine_MF SET isChecked=@isChecked " +
                                "WHERE SNUM=@snum and VacCode=@VacCode";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@snum", _snum);
                    cmd.Parameters.AddWithValue("@VacCode", _vaccode);
                    cmd.Parameters.AddWithValue("@isChecked", _isChecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_STUDENT_HEALTH_DETAILS(string _snum, bool _iscongenital, string _congenital,
                                        bool _ishospitalized, string _dateHospitalized, string _reasonhospitalized,
                                        bool _isminormajor, string _minormajor, string _dateminormajor,
                                        bool _isaccident, string _accident, string _dateaccident, string _parentRemarks,
                                        string _nurserRemarks, string _illOthers, string _HEALTHSTATUSCODE, string _USERID, string vacothers)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_HEALTH_INFO]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IsCongenital", _iscongenital);
                    cmd.Parameters.AddWithValue("@CongenitalDesc", _congenital);
                    cmd.Parameters.AddWithValue("@IsHospitalized", _ishospitalized);
                    cmd.Parameters.AddWithValue("@DateHospitalized", _dateHospitalized);
                    cmd.Parameters.AddWithValue("@ReasonHospitalized", _reasonhospitalized);
                    cmd.Parameters.AddWithValue("@IsMinorMajor", _isminormajor);
                    cmd.Parameters.AddWithValue("@MinorMajorDesc", _minormajor);
                    cmd.Parameters.AddWithValue("@MinorMajorDate", _dateminormajor);
                    cmd.Parameters.AddWithValue("@IsSeriousAccident", _isaccident);
                    cmd.Parameters.AddWithValue("@SeriousAccidentDesc", _accident);
                    cmd.Parameters.AddWithValue("@SeriousAccidentDate", _dateaccident);
                    cmd.Parameters.AddWithValue("@ParentRemarks", _parentRemarks);
                    cmd.Parameters.AddWithValue("@NurseRemarks", _nurserRemarks);
                    cmd.Parameters.AddWithValue("@illOthers", _illOthers);
                    cmd.Parameters.AddWithValue("@HEALTHSTATUSCODE", _HEALTHSTATUSCODE);
                    cmd.Parameters.AddWithValue("@USERID", _USERID);
                    cmd.Parameters.AddWithValue("@SNUM", _snum);
                    cmd.Parameters.AddWithValue("@htype", "STUDENT");
                    cmd.Parameters.AddWithValue("@savetype", "U");
                    cmd.Parameters.AddWithValue("@VACOTHERS", vacothers);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        //APPLICANT HEALTH
        public void INSERT_APPLICANT_ILLNESS(string _anum, string _illnesscode, bool _ischecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "INSERT INTO Health.Stud_Illness_MF(ANUM,IllnessCode,IsChecked) " +
                                "VALUES(@ANUM, @IllnessCode, @IsChecked)";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@ANUM", _anum);
                    cmd.Parameters.AddWithValue("@IllnessCode", _illnesscode);
                    cmd.Parameters.AddWithValue("@IsChecked", _ischecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_APPLICANT_MEDICINE_GIVEN(string _anum, string _medCode, bool _ischecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "INSERT INTO Health.Stud_GivenMed_MF(ANUM,MedCode,IsChecked) " +
                                "VALUES(@ANUM, @MedCode, @IsChecked)";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@ANUM", _anum);
                    cmd.Parameters.AddWithValue("@MedCode", _medCode);
                    cmd.Parameters.AddWithValue("@IsChecked", _ischecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_APPLICANT_VACCINE(string _anum, string _vaccode, bool _ischecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "INSERT INTO Health.Stud_Vaccine_MF(ANUM,VacCode,isChecked) " +
                                "VALUES(@ANUM, @VacCode, @IsChecked)";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@ANUM", _anum);
                    cmd.Parameters.AddWithValue("@VacCode", _vaccode);
                    cmd.Parameters.AddWithValue("@IsChecked", _ischecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_APPLICANT_HEALTH_DETAILS(string _anum, bool _iscongenital, string _congenital,
                                        bool _ishospitalized, string _dateHospitalized, string _reasonhospitalized,
                                        bool _isminormajor, string _minormajor, string _dateminormajor,
                                        bool _isaccident, string _accident, string _dateaccident, string _parentRemarks,
                                        string _nurserRemarks, string _illOthers, string _HEALTHSTATUSCODE, string _USERID, string vacothers)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_HEALTH_INFO]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ANUM", _anum);
                    cmd.Parameters.AddWithValue("@htype", "APPLICANT");
                    cmd.Parameters.AddWithValue("@savetype", "N");
                    cmd.Parameters.AddWithValue("@IsCongenital", _iscongenital);
                    cmd.Parameters.AddWithValue("@CongenitalDesc", _congenital);
                    cmd.Parameters.AddWithValue("@IsHospitalized", _ishospitalized);
                    cmd.Parameters.AddWithValue("@DateHospitalized", _dateHospitalized);

                    cmd.Parameters.AddWithValue("@ReasonHospitalized", _reasonhospitalized);
                    cmd.Parameters.AddWithValue("@IsMinorMajor", _isminormajor);
                    cmd.Parameters.AddWithValue("@MinorMajorDesc", _minormajor);
                    cmd.Parameters.AddWithValue("@MinorMajorDate", _dateminormajor);

                    cmd.Parameters.AddWithValue("@IsSeriousAccident", _isaccident);
                    cmd.Parameters.AddWithValue("@SeriousAccidentDesc", _accident);
                    cmd.Parameters.AddWithValue("@SeriousAccidentDate", _dateaccident);

                    cmd.Parameters.AddWithValue("@ParentRemarks", _parentRemarks);
                    cmd.Parameters.AddWithValue("@NurseRemarks", _nurserRemarks);
                    cmd.Parameters.AddWithValue("@illOthers", _illOthers);
                    cmd.Parameters.AddWithValue("@HEALTHSTATUSCODE", _HEALTHSTATUSCODE);

                    cmd.Parameters.AddWithValue("@UserID", _USERID);

                    cmd.Parameters.AddWithValue("@VACOTHERS", vacothers);

                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_VACCINE_DETAILS(
            string num, 
            DateTime? dt1stDose, DateTime? dt2ndDose, DateTime? dtBoosterDose,
            string brand1stDose, string brand2ndDose, string brandBoosterDose,  
            string type = "Applicant") /* type = Applicant or Student */
        {
            var sql = @"UPDATE Health.Stud_Health_Info_MF 
                        SET dtVacCovidDose1 = @dtVacCovidDose1, dtVacCovidDose2 = @dtVacCovidDose2, dtVacCovidBooster = @dtVacCovidBooster,
                            brandVacCovidDose1 = @brandVacCovidDose1, brandVacCovidDose2 = @brandVacCovidDose2, brandVacCovidBooster = @brandVacCovidBooster
                        WHERE ";
            if (type == "Applicant")
                sql += " ANUM = @NUM";
            else
                sql += " SNUM = @NUM";

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@NUM", num);

                    if (dt1stDose == null)
                        cmd.Parameters.AddWithValue("@dtVacCovidDose1", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@dtVacCovidDose1", dt1stDose);

                    if (dt2ndDose == null)
                        cmd.Parameters.AddWithValue("@dtVacCovidDose2", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@dtVacCovidDose2", dt2ndDose);

                    if (dtBoosterDose == null)
                        cmd.Parameters.AddWithValue("@dtVacCovidBooster", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@dtVacCovidBooster", dtBoosterDose);

                    cmd.Parameters.AddWithValue("@brandVacCovidDose1", brand1stDose);
                    cmd.Parameters.AddWithValue("@brandVacCovidDose2", brand2ndDose);
                    cmd.Parameters.AddWithValue("@brandVacCovidBooster", brandBoosterDose);

                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }

       
        }

        public void UPDATE_APPLICANT_ILLNESS(string _anum, string _illnesscode, bool _isChecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "UPDATE Health.Stud_Illness_MF SET IsChecked=@isChecked " +
                                "WHERE ANUM=@anum and IllnessCode=@IllnessCode ";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@anum", _anum);
                    cmd.Parameters.AddWithValue("@IllnessCode", _illnesscode);
                    cmd.Parameters.AddWithValue("@isChecked", _isChecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_APPLICANT_MEDICINE_GIVEN(string _anum, string _medCode, bool _isChecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "UPDATE Health.Stud_GivenMed_MF SET IsChecked=@isChecked " +
                                "WHERE ANUM=@anum and MedCode=@MedCode";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@anum", _anum);
                    cmd.Parameters.AddWithValue("@MedCode", _medCode);
                    cmd.Parameters.AddWithValue("@isChecked", _isChecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_APPLICANT_VACCINE(string _anum, string _vaccode, bool _isChecked)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "UPDATE Health.Stud_Vaccine_MF SET isChecked=@isChecked " +
                                "WHERE ANUM=@anum and VacCode=@VacCode";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@anum", _anum);
                    cmd.Parameters.AddWithValue("@VacCode", _vaccode);
                    cmd.Parameters.AddWithValue("@isChecked", _isChecked);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_APPLICANT_HEALTH_DETAILS(string _anum, bool _iscongenital, string _congenital,
                                        bool _ishospitalized, string _dateHospitalized, string _reasonhospitalized,
                                        bool _isminormajor, string _minormajor, string _dateminormajor,
                                        bool _isaccident, string _accident, string _dateaccident, string _parentRemarks,
                                        string _nurserRemarks, string _illOthers, string _HEALTHSTATUSCODE, string _USERID, string vacothers)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spTRANS_HEALTH_INFO]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IsCongenital", _iscongenital);
                    cmd.Parameters.AddWithValue("@CongenitalDesc", _congenital);
                    cmd.Parameters.AddWithValue("@IsHospitalized", _ishospitalized);
                    cmd.Parameters.AddWithValue("@DateHospitalized", _dateHospitalized);
                    cmd.Parameters.AddWithValue("@ReasonHospitalized", _reasonhospitalized);
                    cmd.Parameters.AddWithValue("@IsMinorMajor", _isminormajor);
                    cmd.Parameters.AddWithValue("@MinorMajorDesc", _minormajor);
                    cmd.Parameters.AddWithValue("@MinorMajorDate", _dateminormajor);
                    cmd.Parameters.AddWithValue("@IsSeriousAccident", _isaccident);
                    cmd.Parameters.AddWithValue("@SeriousAccidentDesc", _accident);
                    cmd.Parameters.AddWithValue("@SeriousAccidentDate", _dateaccident);
                    cmd.Parameters.AddWithValue("@ParentRemarks", _parentRemarks);
                    cmd.Parameters.AddWithValue("@NurseRemarks", _nurserRemarks);
                    cmd.Parameters.AddWithValue("@illOthers", _illOthers);
                    cmd.Parameters.AddWithValue("@HEALTHSTATUSCODE", _HEALTHSTATUSCODE);
                    cmd.Parameters.AddWithValue("@USERID", _USERID);
                    cmd.Parameters.AddWithValue("@ANUM", _anum);
                    cmd.Parameters.AddWithValue("@htype", "APPLICANT");
                    cmd.Parameters.AddWithValue("@savetype", "U");
                    cmd.Parameters.AddWithValue("@vacothers", vacothers);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_EXIST_HEALTH_DETAILS(string squery)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(squery, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        

        //UPDATE APPLICANT STATUS TRAIL TABLE
        public void UPDATE_APPLICANT_STATUS_TRAIL(string _snum, int _statusCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "spApplicantClearHealth";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APPNUM", _snum);
                    cmd.Parameters.AddWithValue("@HEALTHSTAT", _statusCode);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        #endregion
    }


    //MAAARING HINDI NA GAMITIN BY KEVIN-----

    /*Inherit from Base Class
     *Module Class of Health Complaint Transactions 
     */

    public class HealthComplaint : baseHealth
    {
        
        /*
         Method Area
         */

        //08.30.2016
        //public bool CHECK_MEDICINE_PORTION_ITEM(string _medCode)
        //{
        //    bool x = false;

        //    string strSQL = "spGET_MEDICINE_PORTION";
        //    //string strSQL = "SELECT * FROM Health.Medicine_Portion_RF WHERE medCode = '" + _medCode + "'";

        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(strSQL, cn))
        //        {

        //            cn.Open();

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@MEDCODE", _medCode);

        //            SqlDataReader dr = cmd.ExecuteReader();
        //            if (dr.HasRows)
        //            {
        //                while (dr.Read())
        //                {
        //                    BATCHID = int.Parse(dr["batchID"].ToString());
        //                    MEDCODE = dr["medCode"].ToString();
        //                    POR_MED_STAT = (bool)dr["medStatus"];
        //                }

        //                x = true;
        //            }

        //            else
        //            {
        //                x = false;
        //            }

        //        }

        //        return x;
        //    }



        //}

        //07.25.2016




        /*08102019 old health complaint
        public DataTable GET_COMPLAINT_LIST()
        {
            DataTable dt = new DataTable();
            string strSQL = "[Health].[spGET_COMPLAINT_LIST]";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;
        }

        public DataTable GET_PATIENT_INFO_STUD()
        {
            DataTable dt = new DataTable();
            string strSQL = "Health.spGET_PATIENT_INFO_STUDENT";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;

        }     
        
        public DataTable GET_PATIENT_INFO_STUD_SEARCH(string studnum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spGET_PATIENT_INFO_STUDENT_SEARCH]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }
            return dt;
        }
        */

        /* 08102019
        public DataTable GET_TIMEINCIDENT_LIST()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGET_TIME_INCIDENT_LIST";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;
        }

        public DataTable GET_PLACEINCIDENT_LIST()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGET_PLACE_INCIDENT_LIST";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;
        }


        public DataTable GET_COMPLAINT_HISTORY(string _PATIENTNUM)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spGET_COMPLAINT_HISTORY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PATIENTNUM", _PATIENTNUM);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public void DELETE_COMPLAINT(int _detid , string _transcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spDELETE_COMPLAINT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@detid", _detid);
                    cmd.Parameters.AddWithValue("@transcode", _transcode);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_COMPLAINT(int _detid, string _transcode, string _compcode, DateTime _compdate, string _comptime, string _notes, bool _senthome, bool _senthosp, string _userID)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Health].[spUPDATE_COMPLAINT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@detid", _detid);
                    cmd.Parameters.AddWithValue("@transcode", _transcode);
                    cmd.Parameters.AddWithValue("@compcode", _compcode);
                    cmd.Parameters.AddWithValue("@compdate", _compdate);
                    cmd.Parameters.AddWithValue("@comptime", _comptime);
                    cmd.Parameters.AddWithValue("@notes", _notes);
                    cmd.Parameters.AddWithValue("@senthome", _senthome);
                    cmd.Parameters.AddWithValue("@senthosp", _senthosp);
                    cmd.Parameters.AddWithValue("@user", _userID);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //08/05/2016
        public DataTable GET_COMPLAINT_PATIENT_LIST(string _TRANSCODE)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spGET_COMPLAINT_LIST_PATIENT", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TRANSCODE", _TRANSCODE);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GET_MEDICINE_PATIENT_LIST(string _TRANSCODE)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spGET_MEDICINE_LIST_PATIENT", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TRANSCODE", _TRANSCODE);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;

        }
     
        
        */

        /*08102019
        public void INSERT_MEDICINE(string _medCode, string _medDesc, string _medGenericName, string _medTypeCode, string _medLevelCode, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "spINSERT_MEDICINE";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MEDCODE", _medCode);
                    cmd.Parameters.AddWithValue("@MEDDESC", _medDesc);
                    cmd.Parameters.AddWithValue("@MEDGENERICNAME", _medGenericName);
                    cmd.Parameters.AddWithValue("@MEDTYPECODE", _medTypeCode);
                    cmd.Parameters.AddWithValue("@MEDLEVELCODE", _medLevelCode);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        */

        /* 
         TRANSACTION CRUD ON COMPLAINT SECTION
         * 07.25.2016
         *INSERT LOGS OF COMPLAINTS
         */

        /* 08102019
        public void INSERT_COMPLAINT_SUMMARY(string _transCode, string _sy, string _patientNum, DateTime _compDate, string _compTime,
                                             string _notes, bool _sentHome, bool _sentHospital, 
                                             bool _patientType, string _userID)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spInsert_COMPLAINT_SUMMARY]";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TRANSCODE", _transCode);
                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cmd.Parameters.AddWithValue("@PATIENTNUM", _patientNum);
                    cmd.Parameters.AddWithValue("@COMPDATE", _compDate);
                    cmd.Parameters.AddWithValue("@COMPTIME", _compTime);
                    cmd.Parameters.AddWithValue("@NOTES", _notes);
                    cmd.Parameters.AddWithValue("@SENTHOME", _sentHome);
                    cmd.Parameters.AddWithValue("@SENTHOSPITAL", _sentHospital);
                    cmd.Parameters.AddWithValue("@PATIENTTYPE", _patientType);
                    cmd.Parameters.AddWithValue("@USERID", _userID);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }
        
        public void INSERT_PATIENT_COMPLAINT_DETAILS(string _transCode, string _complaintCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spInsert_COMPLAINT_DETAILS]";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TRANSCODE", _transCode);
                    cmd.Parameters.AddWithValue("@COMPLAINTCODE", _complaintCode);


                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void INSERT_PATIENT_MEDICINE_DETAILS(string _transCode, string _medCode, int _batchID,int _quantity)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Health].[spINSERT_PATIENT_MEDICINE_DETAILS]";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TRANSCODE", _transCode);
                    cmd.Parameters.AddWithValue("@MEDCODE", _medCode);
                    cmd.Parameters.AddWithValue("@BATCHID", _batchID);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);



                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public void UPDATE_COMPLAINT_SUMMARY(string _transCode, DateTime _compDate, DateTime _compTime,
                                             string _notes, bool _sentHome, bool _sentHospital, string _timeIncidentCode,
                                             string _placeIncidentCode, string _physician, string _amount, string _remarks,
                                             bool _patientType, string _userID)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "spUpdate_COMPLAINT_SUMMARY";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TRANSCODE", _transCode);
                    cmd.Parameters.AddWithValue("@COMPDATE", _compDate);
                    cmd.Parameters.AddWithValue("@COMPTIME", _compTime);
                    cmd.Parameters.AddWithValue("@NOTES", _notes);
                    cmd.Parameters.AddWithValue("@SENTHOME", _sentHome);
                    cmd.Parameters.AddWithValue("@SENTHOSPITAL", _sentHospital);
                    cmd.Parameters.AddWithValue("@TIMEINCIDENTCODE", _timeIncidentCode);
                    cmd.Parameters.AddWithValue("@PLACEINCIDENTCODE", _placeIncidentCode);
                    cmd.Parameters.AddWithValue("@PHYSICIAN", _physician);
                    cmd.Parameters.AddWithValue("@AMOUNT", _amount);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@USERID", _userID);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }
        
        //Make Complaint Record Inactive
        public void DISABLE_COMPLAINT_TRANSACTION(string _transcode, int _batchID, string _medCode, int _quantity, string _userID)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "spINACTIVE_COMPLAINT_TRANSACTION";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TRANSCODE", _transcode);
                    cmd.Parameters.AddWithValue("@BATCHID", _batchID);
                    cmd.Parameters.AddWithValue("@MEDCODE", _medCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
                    cmd.Parameters.AddWithValue("@USERID", _userID);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Transaction of Medicine

        public void UPDATE_MEDICINE_STOCK_DOWN(int _batchID, string _medCode, int _quantity)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "spUPDATE_MEDICINE_STOCK_DOWN";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BATCHID", _batchID);
                    cmd.Parameters.AddWithValue("@MEDCODE", _medCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);


                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }
        */
    }

    //08102019
    //public class HealthMedicineSetUp : baseHealth
    //{
    //    public DataTable GET_MEDICINE_LIST_STOCK()
    //    {
    //        DataTable dt = new DataTable();
    //        string strSQL = "[Health].[spGET_MEDICINE_LIST_STOCK]";
    //        dt = queryCommandDT_StoredProc(strSQL);
    //        return dt;
    //    }

    //    public void INSERT_MEDICINE_STOCK(string _medCode, int _quantity, DateTime _expirationDate, string _remarks,string _userID)
    //    {
    //        using (SqlConnection cn = new SqlConnection(CS))
    //        {
    //            string strSQL = "[Health].[spINSERT_MEDICINE_STOCK]";

    //            using (SqlCommand cmd = new SqlCommand(strSQL, cn))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;

    //                cmd.Parameters.AddWithValue("@MEDCODE", _medCode);
    //                cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
    //                cmd.Parameters.AddWithValue("@EXPIRATION", _expirationDate);
    //                cmd.Parameters.AddWithValue("@REMARKS", _remarks);
    //                cmd.Parameters.AddWithValue("@USERID", _userID);


    //                cn.Open();

    //                cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }

    //    public void INSERT_MEDICINE_STOCK_RELEASE(string _medCode,int _batchID, int _quantity, string _remarks, DateTime _dateRelease, string _userID)
    //    {
    //        using (SqlConnection cn = new SqlConnection(CS))
    //        {
    //            string strSQL = "[Health].[spINSERT_MEDICINE_STOCK_RELEASE]";

    //            using (SqlCommand cmd = new SqlCommand(strSQL, cn))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;

    //                cmd.Parameters.AddWithValue("@MEDCODE", _medCode);
    //                cmd.Parameters.AddWithValue("@BATCHID", _batchID);
    //                cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
    //                cmd.Parameters.AddWithValue("@DATERELEASE", _dateRelease);
    //                cmd.Parameters.AddWithValue("@REMARKS", _remarks);
    //                cmd.Parameters.AddWithValue("@USERCODE", _userID);


    //                cn.Open();

    //                cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }
    
        
    //}
}


