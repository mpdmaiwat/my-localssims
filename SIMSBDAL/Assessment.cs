using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMSBDAL
{
    [Serializable]
    public class DistributionFees
    {
        public DistributionFees() { }
        public int Id { get; set; }
        public string PSchemeCode { get; set; }
        public string SY { get; set; }
        public string LevelCode { get; set; }
        public string Strand { get; set; }
        public string PSchemeSession { get; set; }
        public string FeeCode { get; set; }
        public string FeeType { get; set; }
        public double FeeAmount { get; set; }
        public double Amount { get; set; }
        public string InputAmt { get; set; }
        public bool FlagDelete { get; set; }
        public string FeeDesc { get; set; }
        public string html { get; set; }
    }

    public class Assessment : cBase
    {
        public DataTable GET_LEVELS()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_GRADE_LEVEL_LIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_STRANDS()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spGET_STRAND_LIST", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_FEES(string swhere, string sorder,string lvlcode, string sy, string strand)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_FEE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // ''''''''''''''' SCHEDULE OF FEES '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public DataTable GET_SCHED_OF_FEES(string schyear, string levelcode, string sessions, string strandcode)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spGET_SCHED_FEES]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@session", sessions);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_SCHED_FEES(string desigfee, string schyear, decimal fee, string sessions, string username, string levelcode, string strandcode, int arr)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SCHED_FEES]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@savetype", "N");
                    cmd.Parameters.AddWithValue("@desigfee", desigfee);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@fee", fee);
                    cmd.Parameters.AddWithValue("@sessions", sessions);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    cmd.Parameters.AddWithValue("@arr", arr);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_DESIGFEE_CODE_EXIST(string schyear, string sessions, string levelcode, string strandcode)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spCHECK_DESIGFEE_CODE_EXIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@sessions", sessions);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public string GET_FEE_TYPE(string feecode)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spGET_FEE_TYPE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@feecode", feecode);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void UPDATE_SCHED_FEES(string desigfee, string schyear, decimal fee, string sessions, string username, string levelcode, string strandcode, int arr)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SCHED_FEES]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@savetype", "U");
                    cmd.Parameters.AddWithValue("@desigfee", desigfee);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@fee", fee);
                    cmd.Parameters.AddWithValue("@sessions", sessions);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    cmd.Parameters.AddWithValue("@arr", arr);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_SCHED_FEES(string desigfee, string schyear, string sessions, string username, string levelcode, string strandcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SCHED_FEES]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@savetype", "D");
                    cmd.Parameters.AddWithValue("@desigfee", desigfee);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@sessions", sessions);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        // ''''''''''''''' PAYMENT SCHEME '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public DataTable GET_PYMT_SCHEME(string schyear, string levelcode, string pymtscheme, string strandcode)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spGET_PYMT_SCHEME]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@pymtscheme", pymtscheme);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void DELETE_PYMT_SCHEME(string code, string schyear, string pymtscheme, string username, string levelcode, string strandcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spDELETE_PYMT_SCHEME]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@pymtscheme", pymtscheme);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_PSCHEME_CODE_EXIST(string code, string schyear, string pymtscheme, string levelcode, string strandcode)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spCHECK_PSCHEME_CODE_EXIST]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@pymtscheme", pymtscheme);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    x = (bool)cmd.ExecuteScalar();
                }
            }
            return x;
        }

        public void INSERT_PYMT_SCHEME(string code, string schyear, string levelcode, string pymtscheme, DateTime duedate, DateTime pendate, Double TF, Double MF, Double totalamt, string strandcode, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_PYMT_SCHEME]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@pymtscheme", pymtscheme);
                    cmd.Parameters.AddWithValue("@duedate", duedate);
                    cmd.Parameters.AddWithValue("@pendate", pendate);
                    cmd.Parameters.AddWithValue("@TF", TF);
                    cmd.Parameters.AddWithValue("@MF", MF);
                    cmd.Parameters.AddWithValue("@totalamt", totalamt);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_PYMT_SCHEME(string code, string schyear, string levelcode, string pymtscheme, DateTime duedate, DateTime pendate, Double TF, Double MF, Double totalamt, string strandcode, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spUPDATE_PYMT_SCHEME]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@code", code);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@pymtscheme", pymtscheme);
                    cmd.Parameters.AddWithValue("@duedate", duedate);
                    cmd.Parameters.AddWithValue("@pendate", pendate);
                    cmd.Parameters.AddWithValue("@TF", TF);
                    cmd.Parameters.AddWithValue("@MF", MF);
                    cmd.Parameters.AddWithValue("@totalamt", totalamt);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public string GET_ASMT_PYMT(string studnum, string sy, int insno, string pymt)
        {
            string d = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spGET_ASMT_PYMNT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@insno", insno);
                    cmd.Parameters.AddWithValue("@pymt", pymt);
                    d = (string)cmd.ExecuteScalar();
                }
            }
            return d;
        }

        public double GET_TOTAL_CREATED_FEE(string mop, string sy, string lvl, string strand, string session, string fee_code)
        {
            double d = 0;
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_SUM_CREATED_FEES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_mop", mop);
                    cmd.Parameters.AddWithValue("@p_sy", sy);
                    cmd.Parameters.AddWithValue("@p_lvl", lvl);
                    cmd.Parameters.AddWithValue("@p_strand", strand);
                    cmd.Parameters.AddWithValue("@p_session", session);
                    cmd.Parameters.AddWithValue("@p_feecode", fee_code);
                    d = (double)cmd.ExecuteScalar();
                }
            }

            return d;
        }

        public DataTable GET_DIST_FEES(string filter = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_DIST_FEES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_where", filter);
                    cmd.Parameters.AddWithValue("@p_order", order);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GET_DIST_FEES_2(string pschm_code, string lvl_code, string strand, string sy)
        {
            DataTable dt = new DataTable();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_DIST_FEES_2", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_pschmcode", pschm_code);
                    cmd.Parameters.AddWithValue("@p_lvl", lvl_code);
                    cmd.Parameters.AddWithValue("@p_strand", strand);
                    cmd.Parameters.AddWithValue("@p_sy", sy);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public void INSERT_DIST_FEES(string pschm_code, string sy, string lvl, string strand, string session, string fee_code, double amt, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Assessment.spINSERT_DIST_FEE", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_pschmcode", pschm_code);
                    cmd.Parameters.AddWithValue("@p_sy", sy);
                    cmd.Parameters.AddWithValue("@p_lvl", lvl);
                    cmd.Parameters.AddWithValue("@p_strand", strand);
                    cmd.Parameters.AddWithValue("@p_session", session);
                    cmd.Parameters.AddWithValue("@p_feecode", fee_code);
                    cmd.Parameters.AddWithValue("@p_amt", amt);
                    cmd.Parameters.AddWithValue("@p_userid", userid);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void UPDATE_DIST_FEE(string pschm_code, string sy, string lvl, string strand, string session, string fee_code, double amt, string userid)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spUPDATE_DIST_FEE", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_pschmcode", pschm_code);
                    cmd.Parameters.AddWithValue("@p_sy", sy);
                    cmd.Parameters.AddWithValue("@p_lvl", lvl);
                    cmd.Parameters.AddWithValue("@p_strand", strand);
                    cmd.Parameters.AddWithValue("@p_session", session);
                    cmd.Parameters.AddWithValue("@p_feecode", fee_code);
                    cmd.Parameters.AddWithValue("@p_amt", amt);
                    cmd.Parameters.AddWithValue("@p_userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DELETE_DIST_FEE(string pschm_code, string sy, string lvl, string strand, string session, string fee_code)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spDELETE_DIST_FEE", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_pschmcode", pschm_code);
                    cmd.Parameters.AddWithValue("@p_sy", sy);
                    cmd.Parameters.AddWithValue("@p_lvl", lvl);
                    cmd.Parameters.AddWithValue("@p_strand", strand);
                    cmd.Parameters.AddWithValue("@p_session", session);
                    cmd.Parameters.AddWithValue("@p_feecode", fee_code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool CHECK_EXIST_DIST_FEE(string pschm_code, string sy, string lvl, string strand, string session, string fee_code)
        {
            bool r = false;
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spCHECK_DIST_FEE", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_pschmcode", pschm_code);
                    cmd.Parameters.AddWithValue("@p_sy", sy);
                    cmd.Parameters.AddWithValue("@p_lvl", lvl);
                    cmd.Parameters.AddWithValue("@p_strand", strand);
                    cmd.Parameters.AddWithValue("@p_session", session);
                    cmd.Parameters.AddWithValue("@p_feecode", fee_code);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        // ''''''''''''''' ITEMS '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public class AssessmentItem
        {
            public AssessmentItem() { }

            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Formula { get; set; }
            public double Percent { get; set; }
            public double Amount { get; set; }
            public string Type { get; set; }
            public string Category { get; set; }
        }

        public DataTable GET_ASMT_ITEMS(string itypecode = "", string icatcode = "", string iname = "", string orderby = "Assessment.Items_RF.ItmName ASC")
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_ASMT_ITEMS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_itypecode", itypecode);
                    cmd.Parameters.AddWithValue("@p_icatcode", icatcode);
                    cmd.Parameters.AddWithValue("@p_iname", iname);
                    cmd.Parameters.AddWithValue("@p_orderby", orderby);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public void DELETE_ASMT_ITEMS(string itmcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spUPDATE_ASMT_ITEM_DEL", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_itmcode", itmcode);
                    cmd.Parameters.AddWithValue("@p_flagdel", 1);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool CHECK_ASMT_ITEM(string itmcode, string itmdesc, int itmid = 0)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spCHECK_ASMT_ITEM", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_itmcode", itmcode);
                    cmd.Parameters.AddWithValue("@p_itmdesc", itmdesc);
                    cmd.Parameters.AddWithValue("@p_itmid", itmid);
                    r = (bool)cmd.ExecuteScalar();
                }
            }
            return r;
        }

        public void INSERT_ASMT_ITEM(AssessmentItem asmtitem)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spINSERT_ASMT_ITEMS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_itmcode", asmtitem.Code);
                    cmd.Parameters.AddWithValue("@p_itmname", asmtitem.Name);
                    cmd.Parameters.AddWithValue("@p_itmdesc", asmtitem.Description);
                    cmd.Parameters.AddWithValue("@p_itmfrmu", asmtitem.Formula);
                    cmd.Parameters.AddWithValue("@p_itmpct", asmtitem.Percent);
                    cmd.Parameters.AddWithValue("@p_itmamt", asmtitem.Amount);
                    cmd.Parameters.AddWithValue("@p_itmtype", asmtitem.Type);
                    cmd.Parameters.AddWithValue("@p_itmcat", asmtitem.Category);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_ASMT_ITEM(AssessmentItem asmtitem)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spUPDATE_ASMT_ITEM", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_itmid", asmtitem.Id);
                    cmd.Parameters.AddWithValue("@p_itmname", asmtitem.Name);
                    cmd.Parameters.AddWithValue("@p_itmdesc", asmtitem.Description);
                    cmd.Parameters.AddWithValue("@p_itmtype", asmtitem.Type);
                    cmd.Parameters.AddWithValue("@p_itmcat", asmtitem.Category);
                    cmd.Parameters.AddWithValue("@p_itmfrmu", asmtitem.Formula);
                    cmd.Parameters.AddWithValue("@p_itmpct", asmtitem.Percent);
                    cmd.Parameters.AddWithValue("@p_itmamt", asmtitem.Amount);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ''''''''''''''' OLD ACCOUNTS '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        public class OldAccounts
        {
            //public OldAccounts() { }

            public string StudNum { get; set; }
            public string StudName { get; set; }
            public string StudLevel { get; set; }
            public string StudSection { get; set; }
            public string StudStrand { get; set; }
            public double Balanace { get; set; }
            public string SY { get; set; }
            public bool Status { get; set; }
            public DateTime DatePaid { get; set; }
        }

        public DataTable GET_ASMT_OLD_ACCT(string where = null, string orderby = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_ASMT_OLD_ACCT", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_where", where);
                    cmd.Parameters.AddWithValue("@p_orderby", orderby);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool CHECK_OLD_ACCOUNT_EXIST(string studno)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spCHECK_OLD_ACCOUNT_EXIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_OLD_ACCOUNT(string studno, Double balance, string schyear, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_OLD_ACCOUNTS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_OLD_ACCOUNT(string studno, Double balance, string schyear, string username, Double oldbal)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spUPDATE_OLD_ACCOUNTS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@oldbal", oldbal);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_OLD_ACCOUNT(string studno, string schyear)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spDELETE_OLD_ACCOUNTS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_OLD_ACCOUNTS_SUMMARY(string schyear)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_OLD_ACCOUNT_SUMMARY", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
                return dt;
        }

        public DataTable GET_OLD_ACCOUNTS_SUMMARY_DTL(string schyear)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_OLD_ACCOUNT_SUMMARY_DTL", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void PAY_OLD_ACCOUNT(string studno, double amt, double balance, string schyear, DateTime datepaid,string ornum, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spPAY_OLD_ACCOUNT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@amt", amt);
                    cmd.Parameters.AddWithValue("@balance", balance);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@datepaid", datepaid);
                    cmd.Parameters.AddWithValue("@ornum", ornum);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        //---------------------------------------------------------------------
        public class ass_spfun
        {
            public double dblSibDisc { get; set; }
            //public double SiblingDisc { get; set; }
        }

        public double SiblingDisc { get; set; }

        public int COUNT_SIB_ASSESS(string sibcode)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[sp_CountSib_Assess]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@v_sibcode", sibcode);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public bool SibDisc(string SIBCODE, string CStudno, double CTF, double perc,string syvalue)
        {
            //var instance = new Utilities();
            string studYoungest = null;
            int studYorder = 0;
            int CstudOrder = 0;
            double TtlTuit = 0;
            double studYTuit = 0;

            bool x = false;

            if (SIBCODE == "")
            {
                x = false;
            }
            else
            {
                //studYoungest = (Utilities.RValue("sp_GetYoungest", "@v_SibCde", SIBCODE)).ToString();
                studYoungest = GET_YOUNGEST_SIBLING(SIBCODE, syvalue);
                //studYorder = Convert.ToInt32((Utilities.RValue("sp_GetSiblingOrder", "@v_studno", studYoungest)).ToString());
                studYorder = GET_YOUNG_SIBLING_ORDER(studYoungest, syvalue);
                //TtlTuit = Convert.ToDouble((Utilities.RValue("sp_GetSumTuit", "@v_sibcode", SIBCODE)).ToString());
                TtlTuit = GET_TOTAL_SIBLING_DISCOUNT(SIBCODE, syvalue);
                //studYTuit = Convert.ToDouble((Utilities.RValue("sp_GetSibTuit", "@v_studno", studYoungest)).ToString());
                studYTuit = GET_YOUNG_SIBLING_TF(studYoungest, syvalue);
                //CstudOrder = Convert.ToInt32((Utilities.RValue("sp_GetSiblingOrder_Current", "@v_studno", CStudno)).ToString());
                CstudOrder = GET_STUDENT_SIBCODE_ORDER(CStudno, syvalue, SIBCODE);

                if (studYoungest == Convert.ToString(CStudno))
                {
                    x = true;
                    SiblingDisc = (CTF * (perc / 100)) - TtlTuit;
                }
                else
                {
                    if (studYorder < CstudOrder)
                    {
                        x = true;
                        SiblingDisc = (studYTuit * (perc / 100)) - TtlTuit;
                        //SiblingDisc = CTF * (perc / 100);
                    }
                    else
                    {
                        x = true;
                        //SiblingDisc = (studYTuit * (perc / 100)) - TtlTuit;
                        SiblingDisc = CTF * (perc / 100) - TtlTuit;
                    }

                    x = false;
                }

                if (SiblingDisc < 0)
                {
                    x = false;
                    SiblingDisc = 0;
                }
                else
                {
                    x = true;
                }
            }
            return x;

        }
        
        public DataTable GET_SIBLING_LIST(string sibdcode, string syvalue)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SIBLING ASMT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "LSIB");
                    cmd.Parameters.AddWithValue("@sibcode", sibdcode);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public int COUNT_SIBLING_ASSESSED(string sibcode,string syvalue)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SIBLING ASMT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CASB");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@sibcode", sibcode);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public string GET_YOUNGEST_SIBLING(string sibcode, string syvalue)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SIBLING ASMT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GYNG");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@sibcode", sibcode);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public int GET_YOUNG_SIBLING_ORDER(string studnum, string syvalue)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SIBLING ASMT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "SYOR");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public double GET_YOUNG_SIBLING_TF(string studnum,string syvalue)
        {
            double x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SIBLING ASMT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "YTUF");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cn.Open();
                    x = Convert.ToDouble(cmd.ExecuteScalar());
                    cn.Close();
                }
            }
            return x;
        }

        public int GET_STUDENT_SIBCODE_ORDER(string studnum, string syvalue, string sibcode)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SIBLING ASMT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "SSBO");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@sibcode", sibcode);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public double GET_TOTAL_SIBLING_DISCOUNT(string sibcode, string syvalue)
        {
            double x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_SIBLING ASMT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "DTFS");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@sibcode", sibcode);
                    cn.Open();
                    x = Convert.ToDouble(cmd.ExecuteScalar());
                    cn.Close();
                }
            }
            return x;
        }

        //public bool SibDisc(string SIBCODE, string CStudno, double CTF, double perc)
        //{
        //    //var instance = new Utilities();
        //    string studYoungest = null;
        //    int studYorder = 0;
        //    int CstudOrder = 0;
        //    double TtlTuit = 0;
        //    double studYTuit = 0;

        //    bool x = false;

        //    if (SIBCODE == null)
        //    {
        //        x = false;
        //    }
        //    else
        //    {
        //        studYoungest = (Utilities.RValue("sp_GetYoungest", "@v_SibCde", SIBCODE)).ToString();
        //        studYorder = Convert.ToInt32((Utilities.RValue("sp_GetSiblingOrder", "@v_studno", studYoungest)).ToString());
        //        TtlTuit = Convert.ToDouble((Utilities.RValue("sp_GetSumTuit", "@v_sibcode", SIBCODE)).ToString());
        //        studYTuit = Convert.ToDouble((Utilities.RValue("sp_GetSibTuit", "@v_studno", studYoungest)).ToString());
        //        CstudOrder = Convert.ToInt32((Utilities.RValue("sp_GetSiblingOrder", "@v_studno", CStudno)).ToString());

        //        if (studYoungest == Convert.ToString(CStudno))
        //        {
        //            x = true;
        //            SiblingDisc = (CTF * (perc / 100)) - TtlTuit;
        //        }
        //        else
        //        {
        //            if (studYorder < CstudOrder)
        //            {
        //                x = true;
        //                SiblingDisc = CTF * (perc / 100);
        //            }
        //            else
        //            {
        //                x = true;
        //                SiblingDisc = (studYTuit * (perc / 100)) - TtlTuit;
        //            }

        //            x = false;
        //        }

        //        if (SiblingDisc < 0)
        //        {
        //            x = false;
        //            SiblingDisc = 0;
        //        }
        //        else
        //        {
        //            x = true;
        //        }

        //        return x;
        //    }
        //    return x;

        //}

        public string GET_ASMT_NUMBER(string studno, string schyear, int sem,string type)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spGET_ASMT_NUMBER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studnum", studno);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@type", type);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public DataTable GET_ASMT_HDR(string where=null,string orderby = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_ASMT_HDR", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_where", where);
                    cmd.Parameters.AddWithValue("@p_orderby", orderby);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_TFMF(string lvlcode, string schyear, string mop,string strandcode, string studnum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_TFMF", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@levelcode", lvlcode);
                    cmd.Parameters.AddWithValue("@mop", mop);
                    cmd.Parameters.AddWithValue("@strandcode", strandcode);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable DISPLAY_ITEMS(string where, string orderby)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL="Select * from Assessment.Items_RF " + where + " " + orderby;
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable DISPLAY_ASSESSMENT(string swhere)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "SELECT dItem, dType, dItem, dDesc, dAmt,dCat, b.ItmFrm,b.ItmPerc,b.IDbCr,b.IApp FROM Assessment.AssDtls_MF a inner join Assessment.Items_RF b on b.ItmCode =a.dItem " + swhere + " order by dAmt DESC ";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable DISPLAY_ASMT_SPECIAL_ARRANGEMENT(int aid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select * from Assessment.Special_Arrange_MF where is_cancel=0 and AID=" + aid.ToString() + " order by spc_date asc";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }
       
        public bool CHECK_ASSESSMENT_EXISTS(string studno, string schyear)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spCHECK_ASSESSMENT_EXIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_ASSESSMENT_HDR(DateTime docdate, string studno, string sy, string sem, string mop, double ttltf, double ttlmf, double ttlpmt, double ttldsc , double ttldue,string stat, string username, bool isvoucher, double voucheramt, bool nopenalty,bool is_sd)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_ASSESSMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@savetype", "Header");
                    cmd.Parameters.AddWithValue("@docdate", docdate);
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@mop", mop);
                    cmd.Parameters.AddWithValue("@ttltf", ttltf);
                    cmd.Parameters.AddWithValue("@ttlmf", ttlmf);
                    cmd.Parameters.AddWithValue("@ttlpmt", ttlpmt);
                    cmd.Parameters.AddWithValue("@ttldsc", ttldsc);
                    cmd.Parameters.AddWithValue("@ttldue", ttldue);
                    cmd.Parameters.AddWithValue("@stat", stat);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@isvoucher", isvoucher);
                    cmd.Parameters.AddWithValue("@voucheramt", voucheramt);
                    cmd.Parameters.AddWithValue("@nopenalty", nopenalty);
                    cmd.Parameters.AddWithValue("@is_sd", is_sd);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_STUDPYMT(string assnum, string studno, string sy, string sem, string mop, double ttltf, double ttlmf,  double ttldue, string stat, string levC, string pschm, string insno)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand command1 = new SqlCommand("spInsertActStud", cn))
                {
                    cn.Open();
                    command1.Parameters.AddWithValue("@v_StudNo", studno);
                    command1.Parameters.AddWithValue("@v_SchYear", sy);
                    command1.Parameters.AddWithValue("@v_AssNo", assnum);
                    command1.Parameters.AddWithValue("@v_levCode", levC);
                    command1.Parameters.AddWithValue("@v_pymt_scheme", pschm);
                    command1.Parameters.AddWithValue("@v_InsNo", insno);
                    command1.Parameters.AddWithValue("@v_TF", ttltf);
                    command1.Parameters.AddWithValue("@v_MF", ttlmf);
                    command1.Parameters.AddWithValue("@v_TTLPYB", ttldue);
                    command1.Parameters.AddWithValue("@v_Status", "Active");

                    
                    command1.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_ASSESSMENT_DTLS(string studno, string sy, int aid,string username, string itype,string icode, string icat,string idesc, double perc, double amt )
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_ASSESSMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@savetype", "Details");
                    cmd.Parameters.AddWithValue("@aid", aid);
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@username", username);

                    cmd.Parameters.AddWithValue("@itype", itype);
                    cmd.Parameters.AddWithValue("@icode", icode);
                    cmd.Parameters.AddWithValue("@icat", icat);
                    cmd.Parameters.AddWithValue("@idesc", idesc);
                    cmd.Parameters.AddWithValue("@perc", perc);
                    cmd.Parameters.AddWithValue("@amt", amt);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        //public void INSERT_STUD_PYMNT_SD(string studnum, string schyear, int aid, string mop, string lvlcode, double dsctf, double dscmf, string strand)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_STUD_PYMNT_SD]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@studnum", studnum);
        //            cmd.Parameters.AddWithValue("@schyear", schyear);
        //            cmd.Parameters.AddWithValue("@aid", aid);
        //            cmd.Parameters.AddWithValue("@mop", mop);
        //            cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
        //            cmd.Parameters.AddWithValue("@dsctf", dsctf);
        //            cmd.Parameters.AddWithValue("@dscmf", dscmf);
        //            cmd.Parameters.AddWithValue("@strand", strand);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}

        public void INSERT_STUD_PYMNT(string studnum, string schyear, int aid, string mop, string lvlcode, double dsctf, double dscmf, string strand)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_STUD_PYMNT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@aid", aid);
                    cmd.Parameters.AddWithValue("@mop", mop);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@dsctf", dsctf);
                    cmd.Parameters.AddWithValue("@dscmf", dscmf);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        //public void INSERT_STUD_PYMT_VOUCHER(string studnum, string schyear, string assnum, string mop, string lvlcode, double vouchamt, int aid)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_STUDPYMT_VOUCHER]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@studnum", studnum);
        //            cmd.Parameters.AddWithValue("@schyear", schyear);
        //            if (assnum == "2000000000")
        //            {
        //                cmd.Parameters.AddWithValue("@assnum", DBNull.Value);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@assnum", assnum);
        //            }
        //            cmd.Parameters.AddWithValue("@mop", mop);
        //            cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
        //            cmd.Parameters.AddWithValue("@vouchamt", vouchamt);
        //            cmd.Parameters.AddWithValue("@aid", aid);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}

        public void INSERT_ASMT_ONLINE(int aid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spHTTP_POST_STUD_ASMT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@aid", aid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void CANCEL_ASSESSMENT(int aid, string username, string studnum,string asmremarks)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spCANCEL_ASSESSMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@aid", aid);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@asmremarks", asmremarks);
                    cmd.Parameters.AddWithValue("@type", "SSIMS");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void CANCEL_ASSESSMENT_ONLINE(string tempnum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spCANCEL_ASSESSMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tempnum", tempnum);
                    cmd.Parameters.AddWithValue("@type", "ONLINE");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public string GET_PREV_BALANCE(string studnum)
        {
            string x = "0";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spGET_PREV_BALANCE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_ASMT_ISAMS(string studnum, string schyear,string assno,double ttlpaymnt, string username, double ttldsctf, double ttldscmf, string mop, string lvlcode,string strand)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_ASSESSMENT_ISAMS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@assno", assno);
                    cmd.Parameters.AddWithValue("@ttlpaymnt", ttlpaymnt);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@ttldsctf", ttldsctf);
                    cmd.Parameters.AddWithValue("@ttldscmf", ttldscmf);
                    cmd.Parameters.AddWithValue("@mop", mop);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_ASMT_SAP(string studnum,string schyear,string assno, string mop, string lvlcode,string strand, string studname, int sem, int aid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_ASSESSMENT_SAP]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@assno", assno);
                    cmd.Parameters.AddWithValue("@mop", mop);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cmd.Parameters.AddWithValue("@studname", studname);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@aid", aid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        //public void INSERT_ASMT_SAP_ZERO(string studnum, string schyear, string assno, string mop, string lvlcode, string strand, string studname, int sem, int insno)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_ASSESSMENT_SAP_ZERO]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@studnum", studnum);
        //            cmd.Parameters.AddWithValue("@schyear", schyear);
        //            cmd.Parameters.AddWithValue("@assno", assno);
        //            cmd.Parameters.AddWithValue("@mop", mop);
        //            cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
        //            cmd.Parameters.AddWithValue("@strand", strand);
        //            cmd.Parameters.AddWithValue("@studname", studname);
        //            cmd.Parameters.AddWithValue("@sem", sem);
        //            cmd.Parameters.AddWithValue("@insno", insno);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}

        public void IMPORT_ASMT(int aid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_ASSESSMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@savetype", "Import");
                    cmd.Parameters.AddWithValue("@aid", aid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_ASMTNUM_PORTAL(string tempnum, string assnum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spINSERT_ASSESSMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@savetype", "Online");
                    cmd.Parameters.AddWithValue("@tempnum", tempnum);
                    cmd.Parameters.AddWithValue("@assnum", assnum);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_PAYMT_NUMBER(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        //public void UPDATE_ASSESSMENT_VOUCHER(int aid, Boolean isvoucher, Double voucheramt)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Assessment].[spUPDATE_ASSESSMENT_VOUCHER]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@aid", aid);
        //            cmd.Parameters.AddWithValue("@isvoucher", isvoucher);
        //            cmd.Parameters.AddWithValue("@voucheramt", voucheramt);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}

        //public void DELETE_STUD_PYMT_VOUCHER(int aid)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Assessment].[spDELETE_STUD_PYMT_VOUCHER]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@aid", aid);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}

        public DataTable GET_PURPOSE_FOR_APPOINTMENT()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_APPOINTMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "P");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_REPORT_APPOINTMENT(int purposeid, DateTime sdate, DateTime edate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_APPOINTMENT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "R");
                    cmd.Parameters.AddWithValue("@purposeid", purposeid);
                    cmd.Parameters.AddWithValue("@sdate", sdate);
                    cmd.Parameters.AddWithValue("@edate", edate);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_STUDITEM_LIST(string syvalue,string itemcode,string studnum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_STUDITEM]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GLST");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_ITEMS_LIST()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_STUDITEM]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GITM");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public bool CHECK_STUDITEM_EXISTS(string syvalue, string itemcode,string studnum, bool flagcancel)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_STUDITEM]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CSIM");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@flagcancel", flagcancel);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_STUDITEM(string syvalue, string itemcode, string studnum, double amt, string ttype)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_STUDITEM]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(ttype == "NEW")
                    {
                        cmd.Parameters.AddWithValue("@transtype", "ISIM");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@transtype", "ROSI");
                    }
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@amt", amt);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void REMOVE_STUDITEM(int atid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_STUDITEM]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "RSIM");
                    cmd.Parameters.AddWithValue("@atid", atid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_AMOUNT_STUDITEM(int atid, double amt)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_STUDITEM]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "USIM");
                    cmd.Parameters.AddWithValue("@atid", atid);
                    cmd.Parameters.AddWithValue("@amt", amt);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
    }

    public class Penalty : Assessment
    {
        public DataTable GET_DUE_DATE(string syvalue, string lvlcode, string mop, string strand)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_PENALTY]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (strand == "0")
                    {
                        cmd.Parameters.AddWithValue("@transtype", "GDD");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@transtype", "GDS");
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@mop", mop);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_DUE_DATES(string syval, string level, string strand = null, string mop = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_DUE_DATES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", syval);
                    cmd.Parameters.AddWithValue("@level", level);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cmd.Parameters.AddWithValue("@mop", mop);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    return dt;
                }
            }
        }

        public DataTable GET_PENALTY_EXCLUDED_ASSESSMENT(string sy, string key = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_PENALTY_EXCLUDED_ASMT", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@key", key);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    return dt;
                }
            }
        }

        public void UPDATE_ASSESSMENT_PENALTY_EXCLUSION(string asmtNum, bool isExclude)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Assessment.AssHdr_MF SET isNoPenalty = @excluded WHERE U_DocNum = @asmtNum", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@excluded", isExclude);
                    cmd.Parameters.AddWithValue("@asmtNum", asmtNum);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_ASSESSMENT_PENALTY_EXCLUSION(int asmtId, bool isExclude)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Assessment.AssHdr_MF SET isNoPenalty = @excluded WHERE ID = @asmtId", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@excluded", isExclude);
                    cmd.Parameters.AddWithValue("@asmtId", asmtId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_PYMT_SCHEME(string syval)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string sql = "SELECT pymt_scheme, b.MOPDesc, DueDate FROM Assessment.PymtScheme_RF a INNER JOIN xSystem.MOP_RF b ON a.pymt_scheme = b.MOPCode WHERE SY = @syval AND a.flag_del = 'N' AND b.flag_del = 0 AND b.Status = 1 GROUP BY a.pymt_scheme, b.MOPDesc, a.DueDate";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@syval", syval);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GET_NOT_PAID(string lvlcode,DateTime duedate, string mop, string syvalue, string strand, bool existpen, string sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_PENALTY]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(existpen == true)
                    {
                        cmd.Parameters.AddWithValue("@transtype", "GNE");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@transtype", "GNP");
                    }
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@mop", mop);
                    cmd.Parameters.AddWithValue("@duedate", duedate);
                    cmd.Parameters.AddWithValue("@strand", strand);
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

        public void UPDATE_ASSESSMENT_PAYMENT_FROM_SAP(string syvalue)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Assessment].[spTRANS_PENALTY]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "USP");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_PENALTY_SAP(string studnum, string syvalue , int insno, string userid, string duedate)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Assessment].[spINSERT_PENALTY_SAP]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@insno", insno);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@duedate", duedate);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public string GET_LATEST_BS_NUM()
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_PENALTY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GGP");
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public bool GET_PENALTY_GENERATOR_STATUS()
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select cast(gp_Value as bit) from Utilities.Global_Parameters where gp_Code='PNTGEN' ", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void UPDATE_LATEST_BS_NUM(string gpvalue,string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Assessment].[spTRANS_PENALTY]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "UGP");
                    cmd.Parameters.AddWithValue("@gpvalue", gpvalue);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void EXEMPT_STUDENT_PENALTY_PER_INSTALLMENT(string assno, string studno, int insno)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Assessment].[spTRANS_PENALTY]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "EXP");
                    cmd.Parameters.AddWithValue("@assno", assno);
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@insno", insno);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void ENABLE_PENALTY_GENERATOR()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Assessment].[spTRANS_PENALTY]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "ENB");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_DUEDATE_FOR_PENALTY(string syvalue)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_PENALTY]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GPD");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public void RESET_DUE_DATES()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Assessment].[spTRANS_PENALTY]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "RST");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UNEXEMPT_ALL()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Assessment].[spTRANS_PENALTY]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "UNX");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_DUE_DATE_PEN_GEN(bool ispengen, string syvalue, string lvlcode, string mop, int insno, string strand)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Assessment].[spTRANS_PENALTY]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "ADU");
                    cmd.Parameters.AddWithValue("@ispengen", ispengen);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@mop", mop);
                    cmd.Parameters.AddWithValue("@insno", insno);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable VIEW_ALL_EXEMPTED_PENALTY(string syvalue)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_PENALTY]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "VEX");
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable VIEW_COMPUTED_PENALTY()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Assessment].[spTRANS_PENALTY]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CMP");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }
    }
}
    

