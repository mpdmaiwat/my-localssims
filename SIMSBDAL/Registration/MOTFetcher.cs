using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SIMSBDAL
{
    public class MOTFetcher : cBase
    {
        public int id { get; set; }
        public MOTRequest motRequest { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public bool isActive { get; set; }
        public DataTable toTable { get; set; }
        public MOTFetcher[] toList { get; set; }

        public MOTFetcher()
        {

        }

        public MOTFetcher(int transId)
        {
            this.GetData(transId);
        }

        private void GetData(int transId)
        {
            DataTable dt = new DataTable();
            List<MOTFetcher> list = new List<MOTFetcher>();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_MOTFETCHERS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transId", transId);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            this.toTable = dt;
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new MOTFetcher
                {
                    id = int.Parse(dr["Id"].ToString()),
                    motRequest = new MOTRequest(int.Parse(dr["transSrc"].ToString())),
                    lastName = dr["lastName"].ToString(),
                    firstName = dr["firstName"].ToString(),
                    middleName = dr["middleName"].ToString(),
                    isActive = bool.Parse(dr["flagStat"].ToString())
                });
            }
            this.toList = list.ToArray();
        }

        private void Insert(int _transId, string _studNum, string _lastName, string _firstName, string _middleName)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spINSERT_MOTFETCHERS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transId", _transId);
                    cmd.Parameters.AddWithValue("@studNum", _studNum);
                    cmd.Parameters.AddWithValue("@lastName", _lastName);
                    cmd.Parameters.AddWithValue("@firstName", _firstName);
                    cmd.Parameters.AddWithValue("@middleName", _middleName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Update(int _id, string _lastName, string _firstName, string _middleName)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_MOTFETCHERS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", _id);
                    cmd.Parameters.AddWithValue("@lastName", _lastName);
                    cmd.Parameters.AddWithValue("@firstName", _firstName);
                    cmd.Parameters.AddWithValue("@middleName", _middleName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Delete(int _id)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spDELETE_MOTFETCHERS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", _id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Save(MOTFetcher[] fetchers)
        {
            foreach (MOTFetcher fetcher in fetchers)
            {
                int _id = fetcher.id;
                int _transId = fetcher.motRequest.id;
                string _studNum = fetcher.motRequest.student.STUDNO;
                string _lastName = fetcher.lastName;
                string _firstName = fetcher.firstName;
                string _middleName = fetcher.middleName;
                if (_id == 0)
                {
                    this.Insert(_transId, _studNum, _lastName, _firstName, _middleName);
                } else
                {
                    this.Update(_id, _lastName, _firstName, _middleName);
                }
            }
        }

        public void Disable(int[] _ids)
        {
            foreach (int id in _ids)
            {
                this.Delete(id);
            }
        }
    }
}
