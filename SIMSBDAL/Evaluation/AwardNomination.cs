using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMSBDAL.Evaluation
{
    public class AwardNomination : cBase
    {
        public Student GetNominee(string sy, int awardId, int index)
        {
            string studNum = "";
            DataTable dt = this.GET_NOMINATED_AWARDS(sy, awardId);
            if (dt.Rows.Count > 0 && dt.Rows.Count > index)
            {
                DataRow dr = dt.Rows[index];
                studNum = dr["studno"].ToString();
            }
            return new Student().GetStudentInformation(studNum);
        }

        public DataTable GET_NOMINATED_AWARDS(string sy, int awardid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE027]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GN");
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@awardid", awardid);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_SECTIONS_FOR_AWARDS(string lvlcode,string sy,int awardid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE027]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GS");
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@awardid", awardid);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_NOT_NOMINATED_AWARDS(string lvlcode, string sy, int awardid, string section)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE027]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GD");
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@awardid", awardid);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@section", section);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_NEW_NOMINEE_AWARD(string sy,int awardid, string studno)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE027]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@awardid", awardid);
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@transtype", "IS");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void REMOVE_NOMINEE_AWARD(int nomineeid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE027]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nomineeid", nomineeid);
                    cmd.Parameters.AddWithValue("@transtype", "DS");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
    }
}
