using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SIMSBDAL
{

    public class MOTRequest : cBase
    {
        public int id { get; set; }
        public string transId { get; set; }
        public string SY { get; set; }
        public Student student { get; set; }
        public string motWay { get; set; }
        public string motFrom { get; set; }
        public string motTo { get; set; }
        public DateTime dateFiled { get; set; }
        public string durationType { get; set; }
        public DateTime durationFrom { get; set; }
        public DateTime durationTo { get; set; }
        public string status { get; set; }
        public DataTable toTable { get; set; }
        public MOTRequest[] toList { get; set; }
        public string __key { get; set; }

        public MOTRequest()
        {
            
        }

        public MOTRequest(string _studNum)
        {
            toTable = GetRequest(where: string.Format("a.studNum = '{0}'", _studNum));
        }

        public MOTRequest(int _id)
        {
            DataTable dt = GetRequest(where: string.Format("a.Id = {0}", _id));
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.id = _id;
                this.transId = dr["transId"].ToString();
                this.SY = dr["sy"].ToString();
                this.student = new Student().GetStudentInformation(dr["studNum"].ToString());
                this.motWay = dr["motWay"].ToString();
                this.motFrom = dr["motFrom"].ToString();
                this.motTo = dr["motTo"].ToString();
                this.dateFiled = Convert.ToDateTime(dr["dateFiled"].ToString());
                this.durationType = dr["durationType"].ToString();
                this.durationFrom = Convert.ToDateTime(dr["dateFrom"].ToString());
                this.durationTo = Convert.ToDateTime(dr["dateTo"].ToString());
                this.status = dr["flagStat"].ToString();
            }
        }

        public void GetData(string key = "", int pageIndex = 0, int pageSize = 0)
        {
            DataTable dt = GetRequest(where: string.Format("a.studNum LIKE '%{0}%' OR b.StudName LIKE '%{0}%'", key), order: "a.dateFiled DESC", pageIndex: pageIndex, pageSize: pageSize);
            List<MOTRequest> list = new List<MOTRequest>();
            toTable = dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new MOTRequest
                    {
                        id = Convert.ToInt32(dr["Id"].ToString()),
                        transId = dr["transId"].ToString(),
                        SY = dr["sy"].ToString(),
                        student = new Student().GetStudentInformation(dr["studNum"].ToString()),
                        motWay = dr["motWay"].ToString(),
                        motFrom = dr["motFrom"].ToString(),
                        motTo = dr["motTo"].ToString(),
                        dateFiled = Convert.ToDateTime(dr["dateFiled"].ToString()),
                        durationType = dr["durationType"].ToString(),
                        durationFrom = Convert.ToDateTime(dr["dateFrom"].ToString()),
                        durationTo = Convert.ToDateTime(dr["dateTo"].ToString()),
                        status = dr["flagStat"].ToString()
                    });
                }
            }
            toList = list.ToArray();
        }

        private DataTable GetRequest(string select = null, string where = null, string order = null, int pageIndex = 0, int pageSize = 0)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_MOTREQUEST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@order", order);
                    cmd.Parameters.AddWithValue("@currentPage", pageIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public int Count(string key = "")
        {
            int r = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCOUNT_MOTREQUEST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@key", key);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        r = Convert.ToInt32(dr[0]);
                    }
                }
            }
            return r;
        }

        public int Save(MOTRequest motrequest)
        {
            int r = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spINSERT_MOTREQUEST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", motrequest.student.STUDNO);
                    cmd.Parameters.AddWithValue("@motWay", motrequest.motWay);
                    cmd.Parameters.AddWithValue("@motFrom", motrequest.motFrom);
                    cmd.Parameters.AddWithValue("@motTo", motrequest.motTo);
                    cmd.Parameters.AddWithValue("@dateFiled", motrequest.dateFiled);
                    cmd.Parameters.AddWithValue("@durationType", motrequest.durationType);
                    cmd.Parameters.AddWithValue("@dateFrom", motrequest.durationFrom);
                    cmd.Parameters.AddWithValue("@dateTo", motrequest.durationTo);
                    r = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return r;
        }

        public int Save(string _studNum, string _motWay, string _motFrom, string _motTo, DateTime _dateFiled, string _durationType, DateTime _durationFrom, DateTime _durationTo)
        {
            int r = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spINSERT_MOTREQUEST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", _studNum);
                    cmd.Parameters.AddWithValue("@motWay", _motWay);
                    cmd.Parameters.AddWithValue("@motFrom", _motFrom);
                    cmd.Parameters.AddWithValue("@motTo", _motTo);
                    cmd.Parameters.AddWithValue("@dateFiled", _dateFiled);
                    cmd.Parameters.AddWithValue("@durationType", _durationType);
                    cmd.Parameters.AddWithValue("@dateFrom", _durationFrom);
                    cmd.Parameters.AddWithValue("@dateTo", _durationTo);
                    r = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return r;
        }

        public int Update(int _Id, string _transId, string _motWay, string _motTo, string _durationType, DateTime _durationFrom, DateTime _durationTo, string _status)
        {
            int r = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_MOTREQUEST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", _Id);
                    cmd.Parameters.AddWithValue("@transId", _transId);
                    cmd.Parameters.AddWithValue("@motWay", _motWay);
                    cmd.Parameters.AddWithValue("@motTo", _motTo);
                    cmd.Parameters.AddWithValue("@durationType", _durationType);
                    cmd.Parameters.AddWithValue("@dateFrom", _durationFrom);
                    cmd.Parameters.AddWithValue("@dateTo", _durationTo);
                    cmd.Parameters.AddWithValue("@status", _status);
                    r = cmd.ExecuteNonQuery();
                }
            }
            return r;
        }

        public int Post(int _id, string _transId, string _userId)
        {
            int r = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spPOST_MOTREQUEST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", _id);
                    cmd.Parameters.AddWithValue("@transId", _transId);
                    cmd.Parameters.AddWithValue("@userId", _userId);
                    r = cmd.ExecuteNonQuery();
                }
            }
            return r;
        }

        public int Cancel(int _id, string _transId, string _userId)
        {
            int r = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCANCEL_MOTREQUEST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", _id);
                    cmd.Parameters.AddWithValue("@transId", _transId);
                    cmd.Parameters.AddWithValue("@userId", _userId);
                    r = cmd.ExecuteNonQuery();
                }
            }
            return r;
        }
    }
}
