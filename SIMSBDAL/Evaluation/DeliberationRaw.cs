using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SIMSBDAL.Evaluation
{
    public class DeliberationRaw : cBase
    {
        public int Id { get; set; }
        public string SY { get; set; }
        public Student Student { get; set; }
        public DeliberationComponent Component { get; set; }
        public double RawEntry { get; set; }
        public bool IsEncoded { get; set; }

        public DeliberationRaw()
        {

        }

        public DeliberationRaw(int _id)
        {
            DataTable dt = this.GetData(null, string.Format("score.Id = {0}", _id), null, null);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.Id = _id;
                this.SY = dr["sy"].ToString();
                this.Student = new Student().GetStudentInformation(dr["studNum"].ToString());
                this.Component = new DeliberationComponent(int.Parse(dr["compId"].ToString()));
                this.RawEntry = double.Parse(dr["rawEntry"].ToString());
                this.IsEncoded = (bool)dr["isEncoded"];
            }
        }

        public DeliberationRaw(string _sy, int _compId, string _studNum)
        {
            DataTable dt = this.GetData(null, string.Format("score.sy = '{0}' AND score.compId = {1} AND score.studNum = '{2}'", _sy, _compId, _studNum), null, null);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.Id = int.Parse(dr["Id"].ToString());
                this.SY = dr["sy"].ToString();
                this.Student = new Student().GetStudentInformation(dr["studNum"].ToString());
                this.Component = new DeliberationComponent(int.Parse(dr["compId"].ToString()));
                this.RawEntry = double.Parse(dr["rawEntry"].ToString());
                this.IsEncoded = (bool)dr["isEncoded"];
            }
        }

        private DataTable GetData(string _select, string _where, string _group, string _order)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_DELIB_RAW", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", _select);
                    cmd.Parameters.AddWithValue("@where", _where);
                    cmd.Parameters.AddWithValue("@group", _group);
                    cmd.Parameters.AddWithValue("@order", _order);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void Save(string userId = null)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spINSERT_DELIB_RAW", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", this.SY);
                    cmd.Parameters.AddWithValue("@studNum", this.Student.STUDNO);
                    cmd.Parameters.AddWithValue("@compId", this.Component.Id);
                    cmd.Parameters.AddWithValue("@rawEntry", this.RawEntry);
                    cmd.Parameters.AddWithValue("@isEncoded", this.IsEncoded);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(string userId = null)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spUPDATE_DELIB_RAW", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", this.Id);
                    cmd.Parameters.AddWithValue("@rawEntry", this.RawEntry);
                    cmd.Parameters.AddWithValue("@isEncoded", this.IsEncoded);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
