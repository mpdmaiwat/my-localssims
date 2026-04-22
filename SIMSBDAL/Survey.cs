using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMSBDAL
{
    public class Survey : cBase
    {
        public DataTable GET_QUESTIONS(string swhere=null, string sorder =null, string sgroup=null, string sselect = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spGET_QUESTIONS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    cmd.Parameters.AddWithValue("@sgroup", sgroup);
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_EVALUEE(string swhere = null, string sorder = null, string sselect = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spGET_EVALUEE]", cn))
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

        public DataTable GET_RESPONDENT(string swhere = null, string sorder = null, string sselect = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spGET_RESPONDENTS]", cn))
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

        public void INSERT_QUESTION(string question, int arr, int evaluee, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_QUESTION]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@question", question);
                    cmd.Parameters.AddWithValue("@arr", arr);
                    cmd.Parameters.AddWithValue("@evaluee", evaluee);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_QUESTION(int qid, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_QUESTION]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@qid", qid);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_QUESTION(string question, int arr, int evaluee, string userid, int qid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_QUESTION]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@question", question);
                    cmd.Parameters.AddWithValue("@arr", arr);
                    cmd.Parameters.AddWithValue("@evaluee", evaluee);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@qid", qid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_QUESTION_ARR(int qid, int evaluee, int arr)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spCHECK_QUESTION_ARR]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@qid", qid);
                    cmd.Parameters.AddWithValue("@evaluee", evaluee);
                    cmd.Parameters.AddWithValue("@arr", arr);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public bool CHECK_ANSWER_ARR(int aid, int questid, int arr)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spCHECK_ANSWER_ARR]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@aid", aid);
                    cmd.Parameters.AddWithValue("@questid", questid);
                    cmd.Parameters.AddWithValue("@arr", arr);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public DataTable GET_ANSWERS(string swhere = null, string sorder = null, string sgroup = null, string sselect = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spGET_ANSWERS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    cmd.Parameters.AddWithValue("@sgroup", sgroup);
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_ANSWER(int questid, string atext, string avalue, int arr, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_ANSWER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@questid", questid);
                    cmd.Parameters.AddWithValue("@atext", atext);
                    cmd.Parameters.AddWithValue("@avalue", avalue);
                    cmd.Parameters.AddWithValue("@arr", arr);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_ANSWER(int aid, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_ANSWER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@aid", aid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void COPY_ANSWERS(int questid, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_ANSWER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "C");
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@questid", questid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_ANSWER_EXIST(int questid)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spCHECK_ANSWER_EXIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@questid", questid);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public string INSERT_SURVEY(string stitle, int respid, int evalid,string svtype, string userid, int evaldet)
        {
            string x;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_SURVEY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@stitle", stitle);
                    cmd.Parameters.AddWithValue("@respid", respid);
                    cmd.Parameters.AddWithValue("@evalid", evalid);
                    cmd.Parameters.AddWithValue("@svtype", svtype);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@evaldet", evaldet);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void DELETE_SURVEY(int svid, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_SURVEY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@svid", svid);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_SURVEY(string stitle, int respid, int evalid, string svtype, string userid, int svid, int evaldet)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_SURVEY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@stitle", stitle);
                    cmd.Parameters.AddWithValue("@respid", respid);
                    cmd.Parameters.AddWithValue("@evalid", evalid);
                    cmd.Parameters.AddWithValue("@svtype", svtype);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@svid", svid);
                    cmd.Parameters.AddWithValue("@evaldet", evaldet);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_SURVEY_HDR(string swhere = null, string sorder = null, string sselect = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spGET_SURVEYS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gtype", "HDR");
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_SURVEY_DTL(string swhere = null, string sorder = null, string sselect = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spGET_SURVEYS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gtype", "DTL");
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_SURVEY_DTL(int svid, int qid, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_SURVEY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "NQ");
                    cmd.Parameters.AddWithValue("@svid", svid);
                    cmd.Parameters.AddWithValue("@qid", qid);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_SURVEY_DTL(int sdid, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spTRANS_SURVEY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "DQ");
                    cmd.Parameters.AddWithValue("@sdid", sdid);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public string START_SURVEY(int facil, int roomid, int surveyid, int numrespo, string lvlcode, string section, int deptid, int sem, int term)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spSETUP_SURVEY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "ST");
                    cmd.Parameters.AddWithValue("@facil", facil);
                    cmd.Parameters.AddWithValue("@roomid", roomid);
                    cmd.Parameters.AddWithValue("@surveyid", surveyid);
                    cmd.Parameters.AddWithValue("@numrespo", numrespo);
                    if (lvlcode == "0")
                    {
                        cmd.Parameters.AddWithValue("@lvlcode", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    }
                    if (section == "0")
                    {
                        cmd.Parameters.AddWithValue("@section", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@section", section);
                    }
                    if (deptid == 0)
                    {
                        cmd.Parameters.AddWithValue("@deptid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@deptid", deptid);
                    }
                    if (sem == 0)
                    {
                        cmd.Parameters.AddWithValue("@sem", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@sem", sem);
                    }
                    if (term == 0)
                    {
                        cmd.Parameters.AddWithValue("@term", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@term", 0);
                    }
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void STOP_SURVEY(string scode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spSETUP_SURVEY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "SP");
                    cmd.Parameters.AddWithValue("@scode", scode);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_SURVEY_SETUP(string swhere = null, string sorder = null, string sselect = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spGET_SURVEY_SETUP]", cn))
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

        public DataTable GET_STUDENTS_NOT_ANSWERED(int stid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Survey].[spSETUP_SURVEY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "NS");
                    cmd.Parameters.AddWithValue("@stid", stid);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}
