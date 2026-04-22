using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SIMSBDAL.Evaluation
{
    public class DeliberationComponent : cBase
    {
        public int Id { get; set; }
        public string SY { get; set; }
        public Awards Awards { get; set; }
        public string ComponentTitle { get; set; }
        public int Arrangement { get; set; }
        public int ParentId { get; set; }
        public double Percentage { get; set; }
        public bool IsTotal { get; set; }
        public bool IsTransmuted { get; set; }
        public double TotalPoints { get; set; }
        public bool HasChild { get; set; }
        public bool IsEntry { get; set; }
        public string EntryType { get; set; }
        public bool IsComputed { get; set; }
        public bool IsPercentage { get; set; }

        public DataTable GetData(string _select, string _where, string _group, string _order)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE028", cn))
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

        public DeliberationComponent()
        {
            // nothing to construct
        }

        public DeliberationComponent(int _id)
        {
            DataTable dt = this.GetData(null, string.Format("COMP.comp_id = {0}", _id), null, null);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.Id = _id;
                this.SY = dr["sy"].ToString();
                this.Awards = new Awards(int.Parse(dr["award_id"].ToString()));
                this.ComponentTitle = dr["comp_title"].ToString();
                this.Arrangement = int.Parse(dr["arr"].ToString());
                this.ParentId = int.Parse(dr["parent_id"].ToString());
                this.Percentage = double.Parse(dr["percentage"].ToString());
                this.IsTotal = (bool)dr["is_total"];
                this.IsTransmuted = (bool)dr["is_transmu"];
                this.TotalPoints = double.Parse(dr["total_points"].ToString());
                this.HasChild = (bool)dr["has_child"];
                this.IsEntry = (bool)dr["is_DE"];
                this.EntryType = dr["entry_type"].ToString();
                this.IsComputed = (bool)dr["is_computed"];
                this.IsPercentage = (bool)dr["is_pct"];
            }
        }

        public IEnumerable<DeliberationComponent> GetComponents(int _awardId, int _parentId)
        {
            List<DeliberationComponent> list = new List<DeliberationComponent>();
            DataTable dt = this.GetData(null, string.Format("COMP.award_id = {0} AND COMP.parent_id = {1}", _awardId, _parentId), null, "COMP.arr ASC");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new DeliberationComponent
                {
                    Id = int.Parse(dr["comp_id"].ToString()),
                    SY = dr["sy"].ToString(),
                    Awards = new Awards(_awardId),
                    ComponentTitle = dr["comp_title"].ToString(),
                    Arrangement = int.Parse(dr["arr"].ToString()),
                    ParentId = int.Parse(dr["parent_id"].ToString()),
                    Percentage = double.Parse(dr["percentage"].ToString()),
                    IsTotal = (bool)dr["is_total"],
                    IsTransmuted = (bool)dr["is_transmu"],
                    TotalPoints = double.Parse(dr["total_points"].ToString()),
                    HasChild = (bool)dr["has_child"],
                    IsEntry = (bool)dr["is_DE"],
                    EntryType = dr["entry_type"].ToString(),
                    IsComputed = (bool)dr["is_computed"],
                    IsPercentage = (bool)dr["is_pct"]
                });
            }
            return list.ToArray();
        }

        public void INSERT_AWARD_COMPONENTS(string sy,int awardid,string comptitle, int arr, int parentid, decimal percentage, bool istotal, int ttlpoints,bool istransmu,bool isDE, string entrytype, bool iscompu,bool ispct, int childlvl)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE028]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "NW");
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@awardid", awardid);
                    cmd.Parameters.AddWithValue("@comptitle", comptitle);
                    cmd.Parameters.AddWithValue("@arr", arr);
                    cmd.Parameters.AddWithValue("@parentid", parentid);
                    cmd.Parameters.AddWithValue("@percentage", percentage);
                    cmd.Parameters.AddWithValue("@istotal", istotal);
                    cmd.Parameters.AddWithValue("@ttlpoints", ttlpoints);
                    cmd.Parameters.AddWithValue("@istransmu", istransmu);
                    cmd.Parameters.AddWithValue("@isDE", isDE);
                    if (entrytype == "0")
                    {
                        cmd.Parameters.AddWithValue("@entrytype", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@entrytype", entrytype);
                    }
                    cmd.Parameters.AddWithValue("@iscompu", iscompu);
                    cmd.Parameters.AddWithValue("@ispct", ispct);
                    cmd.Parameters.AddWithValue("@childlvl", childlvl);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_AWARD_COMPONENTS(string comptitle, int arr, decimal percentage, bool istotal, int ttlpoints, bool istransmu, bool isDE, string entrytype, bool iscompu, bool ispct,int compid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE028]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "UP");
                    cmd.Parameters.AddWithValue("@comptitle", comptitle);
                    cmd.Parameters.AddWithValue("@arr", arr);
                    cmd.Parameters.AddWithValue("@percentage", percentage);
                    cmd.Parameters.AddWithValue("@istotal", istotal);
                    cmd.Parameters.AddWithValue("@ttlpoints", ttlpoints);
                    cmd.Parameters.AddWithValue("@istransmu", istransmu);
                    cmd.Parameters.AddWithValue("@isDE", isDE);
                    cmd.Parameters.AddWithValue("@entrytype", entrytype);
                    cmd.Parameters.AddWithValue("@iscompu", iscompu);
                    cmd.Parameters.AddWithValue("@ispct", ispct);
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void REMOVE_AWARD_COMPONENTS(int compid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE028]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "DL");
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
