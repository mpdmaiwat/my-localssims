using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SIMSBDAL.Evaluation
{
    public class DeliberationFinalGrade : cBase
    {
        public int Id { get; set; }
        public string SY { get; set; }
        public Student Student { get; set; }
        public Awards Award { get; set; }
        public double FinalGrade { get; set; }

        public DeliberationFinalGrade()
        {

        }

        public void Compute(string userId = null)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCOMPUTE_DELIBERATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", this.SY);
                    cmd.Parameters.AddWithValue("@studNum", this.Student.STUDNO);
                    cmd.Parameters.AddWithValue("@awardId", Award.Id);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
