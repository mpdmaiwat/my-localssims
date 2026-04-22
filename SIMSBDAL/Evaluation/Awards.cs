using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SIMSBDAL.Evaluation
{
    public class Awards : cBase
    {
        public int Id { get; set; }
        public string LevelCode { get; set; }
        public string AwardCode { get; set; }
        public string AwardTitle { get; set; }
        public string AwardDesc { get; set; }

        public Awards()
        {

        }

        public Awards(int _id)
        {
            DataTable dt = this.GetData(string.Format("award_id = {0}", _id), null);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.Id = _id;
                this.LevelCode = dr["Lvlcode"].ToString();
                this.AwardCode = dr["award_code"].ToString();
                this.AwardTitle = dr["award_title"].ToString();
                this.AwardDesc = dr["award_desc"].ToString();
            }
        }

        public IEnumerable<Awards> GetAwards(string _levelCode)
        {
            List<Awards> list = new List<Awards>();
            DataTable dt = this.GetData(string.Format("Lvlcode = '{0}'", _levelCode), "award_title ASC");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Awards
                {
                    Id = int.Parse(dr["award_id"].ToString()),
                    LevelCode = _levelCode,
                    AwardCode = dr["award_code"].ToString(),
                    AwardTitle = dr["award_title"].ToString(),
                    AwardDesc = dr["award_desc"].ToString()
                });
            }
            return list.ToArray();
        }

        public IEnumerable<Awards> Search(int _pageIndex, int _pageSize, string _levelCode, string _key = "")
        {
            List<Awards> list = new List<Awards>();
            DataTable dt = this.GetData(string.Format("Lvlcode = '{0}' AND (award_code LIKE '%{1}%' OR award_title LIKE '%{1}%')", _levelCode, _key), "award_title ASC", _pageIndex, _pageSize);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Awards
                {
                    Id = int.Parse(dr["award_id"].ToString()),
                    LevelCode = _levelCode,
                    AwardCode = dr["award_code"].ToString(),
                    AwardTitle = dr["award_title"].ToString(),
                    AwardDesc = dr["award_desc"].ToString()
                });
            }
            return list.ToArray();
        }

        public int SearchCount(string _levelCode, string _key = "")
        {
            int r = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Utilities.spCOUNT_AWARDS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@levelCode", _levelCode);
                    cmd.Parameters.AddWithValue("@key", _key);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        r = int.Parse(dr[0].ToString());
                    }
                }
            }
            return r;
        }

        private DataTable GetData(string _where = null, string _order = null, int _pageIndex = 0, int _pageSize = 0)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Utilities.spGET_AWARDS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", _where);
                    cmd.Parameters.AddWithValue("@order", _order);
                    cmd.Parameters.AddWithValue("@pageIndex", _pageIndex);
                    cmd.Parameters.AddWithValue("@pageSize", _pageSize);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void INSERT_NEW_AWARDS(string awardcode, string awardtitle, string awarddesc, string lvlcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_AWARDS_RF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "NA");
                    cmd.Parameters.AddWithValue("@awardcode", awardcode);
                    cmd.Parameters.AddWithValue("@awardtitle", awardtitle);
                    cmd.Parameters.AddWithValue("@awarddesc", awarddesc);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_AWARDS(int awardid, string awardtitle, string awarddesc)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_AWARDS_RF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "UA");
                    cmd.Parameters.AddWithValue("@awardid", awardid);
                    cmd.Parameters.AddWithValue("@awardtitle", awardtitle);
                    cmd.Parameters.AddWithValue("@awarddesc", awarddesc);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
    }
    
}
