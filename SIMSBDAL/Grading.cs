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
    public class StudentRemarks : cBase
    {
        #region "PROPERTIES"
        public int Id { get; set; }
        public string SY { get; set; }
        public int Semester { get; set; }
        public int Term { get; set; }
        public string StudNum { get; set; }
        public string Remarks { get; set; }
        public Student StudentInfo { get; set; }
        #endregion

        public DataTable Get(string levelCode, string section, int sem, int term)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE025", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@levelCode", levelCode);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public StudentRemarks Get(int Id)
        {
            StudentRemarks rem = new StudentRemarks();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE025", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", Id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            rem = new StudentRemarks
                            {
                                Id = Id,
                                SY = dr["sy"].ToString(),
                                Semester = Convert.ToInt32(dr["sem"].ToString()),
                                Term = Convert.ToInt32(dr["term"].ToString()),
                                StudNum = dr["StudNum"].ToString(),
                                Remarks = dr["remarks"].ToString(),
                                StudentInfo = new Student
                                {
                                    GENDERCODE = dr["genderCode"].ToString(),
                                    FULLNAME = dr["StudName"].ToString(),
                                    PHOTOLOC = dr["PhotoPath"].ToString()
                                }
                            };
                        }
                    }
                }
            }

            return rem;
        }

        public int Insert(int sem, int term, string studNum, string remarks, string userId)
        {
            int insertedId = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE025", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transType", "I");
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    insertedId = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }
            }

            return insertedId;
        }

        public void Update(int id, string remarks, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE025", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transType", "U");
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool Check(int sem, int term, string studNum)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE025", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }
    }

    public class EquivalentLetterGrades : cBase
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string GradeCode { get; set; }
        public double GradeFrom { get; set; }
        public double GradeTo { get; set; }
        public string GradeLetter { get; set; }
        public string Status { get; set; }

        public static DataTable GetData(string select, string join_on, string where = null, string group = null, string order = null)
        {
            DataTable dt = new DataTable();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@join_on", join_on);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static EquivalentLetterGrades GetInfo(int id)
        {
            EquivalentLetterGrades egrd = new EquivalentLetterGrades();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", "equiv.*");
                    cmd.Parameters.AddWithValue("@join_on", "equiv.grd_letter = lgrd.Id");
                    cmd.Parameters.AddWithValue("@where", string.Format("WHERE equiv.Id = {0}", id));
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            egrd = new EquivalentLetterGrades
                            {
                                Id = id,
                                Code = dr["hdr_code"].ToString(),
                                Description = dr["hdr_desc"].ToString(),
                                GradeCode = dr["grd_code"].ToString(),
                                GradeFrom = Convert.ToDouble(dr["grd_fr"].ToString()),
                                GradeTo = Convert.ToDouble(dr["grd_to"].ToString()),
                                GradeLetter = dr["grd_letter"].ToString(),
                                Status = dr["status"].ToString()
                            };
                        }
                    }
                }
            }

            return egrd;
        }

        public static bool CheckHeader(EquivalentLetterGrades egrd, string type)
        {
            bool r = false;
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@hdr_code", egrd.Code);
                    cmd.Parameters.AddWithValue("@hdr_desc", egrd.Description);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public static bool CheckEquivalent(EquivalentLetterGrades egrd)
        {
            bool r = false;
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "F");
                    cmd.Parameters.AddWithValue("@id", egrd.Id);
                    cmd.Parameters.AddWithValue("@grd_code", egrd.GradeCode);
                    cmd.Parameters.AddWithValue("@grd_fr", egrd.GradeFrom);
                    cmd.Parameters.AddWithValue("@grd_to", egrd.GradeTo);
                    cmd.Parameters.AddWithValue("@grd_letter", egrd.GradeLetter);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public static void InsertHeader(EquivalentLetterGrades egrd, string userid = "dbo")
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "I");
                    cmd.Parameters.AddWithValue("@hdr_code", egrd.Code);
                    cmd.Parameters.AddWithValue("@hdr_desc", egrd.Description);
                    cmd.Parameters.AddWithValue("@grd_code", egrd.GradeCode);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateHeader(EquivalentLetterGrades egrd)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd= new SqlCommand("Evltn.spTRANS_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "U");
                    cmd.Parameters.AddWithValue("@hdr_code", egrd.Code);
                    cmd.Parameters.AddWithValue("@hdr_desc", egrd.Description);
                    cmd.Parameters.AddWithValue("@grd_code", egrd.GradeCode);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertLetter(EquivalentLetterGrades egrd, string userid = "dbo")
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd= new SqlCommand("Evltn.spTRANS_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "J");
                    cmd.Parameters.AddWithValue("@hdr_code", egrd.Code);
                    cmd.Parameters.AddWithValue("@hdr_desc", egrd.Description);
                    cmd.Parameters.AddWithValue("@grd_code", egrd.GradeCode);
                    cmd.Parameters.AddWithValue("@grd_fr", egrd.GradeFrom);
                    cmd.Parameters.AddWithValue("@grd_to", egrd.GradeTo);
                    cmd.Parameters.AddWithValue("@grd_letter", egrd.GradeLetter);
                    cmd.Parameters.AddWithValue("@status", egrd.Status);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateLetter(EquivalentLetterGrades egrd, string userid = "dbo")
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "V");
                    cmd.Parameters.AddWithValue("@id", egrd.Id);
                    cmd.Parameters.AddWithValue("@grd_fr", egrd.GradeFrom);
                    cmd.Parameters.AddWithValue("@grd_to", egrd.GradeTo);
                    cmd.Parameters.AddWithValue("@grd_letter", egrd.GradeLetter);
                    cmd.Parameters.AddWithValue("@status", egrd.Status);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteHeader(string code)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "D");
                    cmd.Parameters.AddWithValue("@hdr_code", code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteLetter(int id)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE017", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "X");
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetAssignedLevel(string where = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE021", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@order", order);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static void AssignToLevel(string sy, string hdr_code, string lvl_code, string strand = null)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE021", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "I");
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@hdr_code", hdr_code);
                    cmd.Parameters.AddWithValue("@lvl_code", lvl_code);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UnassignToLevel(int Id)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE021", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "D");
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public class LetterGrades : cBase
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Letter { get; set; }
        public string LetterDescription { get; set; }
        public int LetterRank { get; set; }
        public int GradeType { get; set; }
        public bool FlagStatus { get; set; }
        public bool FlagDelete { get; set; }

        public static DataTable GetData(string select = null, string where = null, string group = null, string order = null)
        {
            DataTable dt = new DataTable();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static LetterGrades GetInfo(int Id)
        {
            LetterGrades grd = new LetterGrades();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd= new SqlCommand("Evltn.spGET_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", "grdlet.*");
                    cmd.Parameters.AddWithValue("@where", "WHERE grdlet.Id = " + Id.ToString());
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                grd = new LetterGrades
                                {
                                    Id = Id,
                                    Code = dr["grd_code"].ToString(),
                                    Description = dr["grd_desc"].ToString(),
                                    GradeType = Convert.ToInt32(dr["grd_type"].ToString()),
                                    Letter = dr["grd_letter"].ToString(),
                                    LetterDescription = dr["letter_desc"].ToString(),
                                    LetterRank = Convert.ToInt32(dr["letter_rank"].ToString()),
                                    FlagStatus = Convert.ToBoolean(dr["flag_stat"].ToString()),
                                    FlagDelete = Convert.ToBoolean(dr["flag_del"].ToString())
                                };
                            }
                        }
                    }
                }
            }

            return grd;
        }

        public static bool CheckHeader(LetterGrades grd, string type)
        {
            bool r = false;
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@grd_code", grd.Code);
                    cmd.Parameters.AddWithValue("@grd_desc", grd.Description);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public static bool CheckLetter(LetterGrades grd)
        {
            bool r = false;
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "F");
                    cmd.Parameters.AddWithValue("@id", grd.Id);
                    cmd.Parameters.AddWithValue("@grd_code", grd.Code);
                    cmd.Parameters.AddWithValue("@grd_letter", grd.Letter);
                    cmd.Parameters.AddWithValue("@letter_desc", grd.LetterDescription);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public static void InsertHeader(LetterGrades grd, string userid = "dbo")
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "I");
                    cmd.Parameters.AddWithValue("@grd_code", grd.Code);
                    cmd.Parameters.AddWithValue("@grd_desc", grd.Description);
                    cmd.Parameters.AddWithValue("@grd_type", grd.GradeType);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateHeader(LetterGrades grd)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "U");
                    cmd.Parameters.AddWithValue("@grd_code", grd.Code);
                    cmd.Parameters.AddWithValue("@grd_desc", grd.Description);
                    cmd.Parameters.AddWithValue("@grd_type", grd.GradeType);
                    cmd.Parameters.AddWithValue("@flag_stat", grd.FlagStatus);
                    cmd.Parameters.AddWithValue("@flag_del", grd.FlagDelete);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void InsertLetter(LetterGrades grd, string userid = "dbo")
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "J");
                    cmd.Parameters.AddWithValue("@grd_code", grd.Code);
                    cmd.Parameters.AddWithValue("@grd_desc", grd.Description);
                    cmd.Parameters.AddWithValue("@grd_type", grd.GradeType);
                    cmd.Parameters.AddWithValue("@grd_letter", grd.Letter);
                    cmd.Parameters.AddWithValue("@letter_desc", grd.LetterDescription);
                    cmd.Parameters.AddWithValue("@letter_rank", grd.LetterRank);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateLetter(LetterGrades grd, string userid = "dbo")
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "V");
                    cmd.Parameters.AddWithValue("@id", grd.Id);
                    cmd.Parameters.AddWithValue("@grd_letter", grd.Letter);
                    cmd.Parameters.AddWithValue("@letter_desc", grd.LetterDescription);
                    cmd.Parameters.AddWithValue("@letter_rank", grd.LetterRank);
                    cmd.Parameters.AddWithValue("@flag_stat", grd.FlagStatus);
                    cmd.Parameters.AddWithValue("@flag_del", grd.FlagDelete);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteHeader(string grd_code)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "D");
                    cmd.Parameters.AddWithValue("@grd_code", grd_code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteLetter(int Id)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE018", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "X");
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public class GradeType : Grading
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string GradeEntry { get; set; }
        public bool HasComponents { get; set; }
        public bool HasLetterGrade { get; set; }

        public static GradeType GetInfo(int id)
        {
            GradeType grd = new GradeType();
            Grading objGrd = new Grading();
            DataTable dt = objGrd.GET_SUBJECT_TYPE("WHERE ID = " + id.ToString());
            if (dt.Rows.Count > 0)
            {
                grd = new GradeType
                {
                    Id = id,
                    Code = dt.Rows[0]["SubTypeCode"].ToString(),
                    Name = dt.Rows[0]["SubType"].ToString(),
                    GradeEntry = dt.Rows[0]["GrdEntry"].ToString(),
                    HasComponents = Convert.ToBoolean(dt.Rows[0]["isReqComp"].ToString()),
                    HasLetterGrade = Convert.ToBoolean(dt.Rows[0]["hasLetterGrd"].ToString())
                };
            }

            return grd;
        }
    }

    public class RawScore : cBase
    {
        public int Id { get; set; } // Raw Score Id
        public string ClassCode { get; set; }
        public string SY { get; set; }
        public string StudNum { get; set; }
        public int Sem { get; set; }
        public int Term { get; set; }
        public int CompId { get; set; }
        public int SubCompId { get; set; }
        public string Score { get; set; }
        public int SumId { get; set; }
        public string Sum { get; set; }
        public string Trans { get; set; }
        public int TotalItemsId { get; set; }
        public string TotalItems { get; set; }
        public bool HasEntry { get; set; }
        public bool IsEncoded { get; set; }
    }
    public class TransmutationTable : cBase
    {
        public int Id { get; set; }
        public string HeaderCode { get; set; }
        public string SY { get; set; }
        public int Item { get; set; }
        public int Score { get; set; }
        public int Grade { get; set; }

        public class Header
        {
            public int Id { get; set; }
            public string SY { get; set; }
            public string HeaderCode { get; set; }
            public string HeaderDescription { get; set; }
            public bool FlagDelete { get; set; }

            public static string GenerateTransmuHeaderCode(string prefix)
            {
                string r = null;
                using(SqlConnection cn = new SqlConnection(CS))
                {
                    using(SqlCommand cmd = new SqlCommand("SELECT * FROM xSystem.AutoNumber_RF WHERE CodePrefix = @PREFIX", cn))
                    {
                        cn.Open();
                        cmd.Parameters.AddWithValue("@PREFIX", prefix);
                        using(SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                int last_num = Convert.ToInt32(dr["Series"].ToString());
                                r = prefix + last_num.ToString("0000000");
                            }
                        }
                    }
                }

                return r;
            }

            public static DataTable GetData(string where = null)
            {
                DataTable dt = new DataTable();
                using(SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE023", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@where", where);
                        using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }

            public static Header GetInfo(int id)
            {
                Header header = new Header();
                using(SqlConnection cn = new SqlConnection(CS))
                {
                    using(SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE023", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@where", "WHERE id = " + id.ToString());
                        using(SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                header = new Header
                                {
                                    Id = id,
                                    HeaderCode = dr["trmu_code"].ToString(),
                                    HeaderDescription = dr["trmu_desc"].ToString(),
                                    FlagDelete = (bool)dr["flag_del"],
                                    SY = dr["sy"].ToString()
                                };
                            }
                        }
                    }
                }

                return header;
            }

            public static void InsertData(Header header, string userid)
            {
                using(SqlConnection cn = new SqlConnection(CS))
                {
                    using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE023", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@type", "I");
                        cmd.Parameters.AddWithValue("@sy", header.SY);
                        cmd.Parameters.AddWithValue("@code", header.HeaderCode);
                        cmd.Parameters.AddWithValue("@desc", header.HeaderDescription);
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public static void UpdateData(Header header, string userid)
            {
                using(SqlConnection cn = new SqlConnection(CS))
                {
                    using(SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE023", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@type", "U");
                        cmd.Parameters.AddWithValue("@id", header.Id);
                        cmd.Parameters.AddWithValue("@desc", header.HeaderDescription);
                        cmd.Parameters.AddWithValue("@flag_del", header.FlagDelete);
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static TransmutationTable[] GetInfo(string select, string where= null, string group = null, string order = null)
        {
            List<TransmutationTable> list = new List<TransmutationTable>();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd =new SqlCommand("Evltn.spGET_MFE008", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                list.Add(new TransmutationTable
                                {
                                    Id = Convert.ToInt32(dr["Id"].ToString()),
                                    SY = dr["sy"].ToString(),
                                    Item = Convert.ToInt32(dr["items"].ToString()),
                                    Score = Convert.ToInt32(dr["score"].ToString()),
                                    Grade = Convert.ToInt32(dr["grd"].ToString())
                                });
                            }
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public static DataTable GetData(string select, string where = null, string group = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE008", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static bool Check(TransmutationTable tbl)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE008", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@item", tbl.Item);
                    cmd.Parameters.AddWithValue("@sy", tbl.SY);
                    cmd.Parameters.AddWithValue("@trmu_code", tbl.HeaderCode);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public static void InsertData(TransmutationTable tbl, string userid = null)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE008", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "I");
                    cmd.Parameters.AddWithValue("@id", tbl.Id);
                    cmd.Parameters.AddWithValue("@sy", tbl.SY);
                    cmd.Parameters.AddWithValue("@trmu_code", tbl.HeaderCode);
                    cmd.Parameters.AddWithValue("@item", tbl.Item);
                    cmd.Parameters.AddWithValue("@score", tbl.Score);
                    cmd.Parameters.AddWithValue("@grade", tbl.Grade);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateData(TransmutationTable tbl, string userid = null)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE008", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "U");
                    cmd.Parameters.AddWithValue("@id", tbl.Id);
                    cmd.Parameters.AddWithValue("@sy", tbl.SY);
                    cmd.Parameters.AddWithValue("@trmu_code", tbl.HeaderCode);
                    cmd.Parameters.AddWithValue("@item", tbl.Item);
                    cmd.Parameters.AddWithValue("@score", tbl.Score);
                    cmd.Parameters.AddWithValue("@grade", tbl.Grade);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteData(TransmutationTable tbl)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE008", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "D");
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@sy", tbl.SY);
                    cmd.Parameters.AddWithValue("@trmu_code", tbl.HeaderCode);
                    cmd.Parameters.AddWithValue("@item", tbl.Item);
                    cmd.Parameters.AddWithValue("@score", 0);
                    cmd.Parameters.AddWithValue("@grade", 0);
                    cmd.Parameters.AddWithValue("@userid", null);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static string GetLevelTransmutationTableCode(string levelCode)
        {
            string trmu_code = "";
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                cn.Open();
                using (SqlCommand cmd_ = new SqlCommand("xSystem.spGET_LEVEL_TRMU_CODE", cn))
                {
                    cmd_.CommandType = CommandType.StoredProcedure;
                    cmd_.Parameters.AddWithValue("@lvl_code", levelCode);
                    using (SqlDataReader dr = cmd_.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            trmu_code = dr["trmu_code"].ToString();
                        }
                    }
                }
            }

            return trmu_code;
        }
    }

    public class Grading : cBase
    {
        public class Subject
        {
            public int Id { get; set; }
            public int ParentId { get; set; }
            public string Code { get; set; }
            public string ShortDesc { get; set; }
            public string SubjDesc { get; set; }
            public string LevelCode { get; set; }
            public string LevelTypeDesc { get; set; }
            public string StrandCode { get; set; }
            public string StrandDesc { get; set; }
            public int SubjAreaId { get; set; }
            public string SubjAreaDesc { get; set; }
            public int SubjTypeId { get; set; }
            public string SubjTypeDesc { get; set; }
            public string SubjClassftn { get; set; }
            public string SubjClassftnDesc { get; set; }
            public int SeqNum { get; set; }
            public bool HasSubSubj { get; set; }
            public bool FlagStat { get; set; }
            public bool FlagDel { get; set; }
            public bool HasComponents { get; set; }
            public bool HasLetterGrade { get; set; }
            public string GradeStyle { get; set; }
        }

        public DataTable GET_SUBJECTS_OFFERED(string sy, int sem, int term, string levelCode)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_SUBJECTS_OFFERED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@level", levelCode);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GET_SUBJECTS_BY_LEVEL(string levelCode)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Evltn.MFE003 WHERE lvl_code = @levelCode AND flag_stat = 1 AND flag_del = 0", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@levelCode", levelCode);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public string GetSubjectGradeEntry(int subjId)
        {
            string r = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE003", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", "MAX(subj.grd_entry)");
                    cmd.Parameters.AddWithValue("@swhere", string.Format("WHERE subj.Id = {0}", subjId));
                    r = cmd.ExecuteScalar().ToString();
                }
            }
            return r;
        }
        public static bool HasUpdateClassRawScores(string classcode, int sem, int term)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_CHANGES_RAW_SCORES]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@class_code", classcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public static bool HasUpdateComputedGrades(string studno, int sem, int subjtype, int term)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_CHANGES_COMP_GRADES]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@stud_no", studno);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@subjtype", subjtype);
                    cmd.Parameters.AddWithValue("@term", term);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public DataTable GET_SUBJECT_TYPE(string swhere = null, string sorder = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE002]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_SUBJECT_TYPE(string subtcode, string subtdesc, string username, string grdentry, Boolean reqcomp, Boolean lettergrd)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE002]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subtcode", subtcode);
                    cmd.Parameters.AddWithValue("@subtdesc", subtdesc);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@stid", DBNull.Value);
                    cmd.Parameters.AddWithValue("@grdentry", grdentry);
                    cmd.Parameters.AddWithValue("@reqcomp", reqcomp);
                    cmd.Parameters.AddWithValue("@lettergrd", lettergrd);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_SUBJECT_TYPE(string subtdesc, string username, int stid, string grdentry, Boolean reqcomp, Boolean lettergrd)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE002]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subtcode", DBNull.Value);
                    cmd.Parameters.AddWithValue("@subtdesc", subtdesc);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@stid", stid);
                    cmd.Parameters.AddWithValue("@grdentry", grdentry);
                    cmd.Parameters.AddWithValue("@reqcomp", reqcomp);
                    cmd.Parameters.AddWithValue("@lettergrd", lettergrd);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_SUBJECT_TYPE(string username, int stid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE002]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subtcode", DBNull.Value);
                    cmd.Parameters.AddWithValue("@subtdesc", DBNull.Value);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@stid", stid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_SUBJECT_AREA(string swhere = null, string sorder = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE001]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_SUBJECT_AREA(string subacode, string subadesc, int coorid, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE001]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subacode", subacode);
                    cmd.Parameters.AddWithValue("@subadesc", subadesc);
                    cmd.Parameters.AddWithValue("@coorid", coorid);
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_SUBJECT_AREA(string subadesc, int coorid, Boolean stat, string username, int stid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE001]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subadesc", subadesc);
                    cmd.Parameters.AddWithValue("@coorid", coorid);
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@stat", stat);
                    cmd.Parameters.AddWithValue("@stid", stid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_SUBJECT_AREA(string username, int stid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE001]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@stid", stid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_SUBJECTS(string select, string swhere = null, string group = null, string sorder = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE003]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sgroup", group);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public Subject GET_SUBJECT_INFO(int subj_id)
        {
            Subject subj = new Subject();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE003", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", "subj.*, area.SubjArea, type.SubType, lvl.LevelTypeDesc, strand.StrandName, classftn.STATDESC");
                    cmd.Parameters.AddWithValue("@swhere", "WHERE subj.Id = " + subj_id.ToString());
                    cmd.Parameters.AddWithValue("@sorder", null);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            subj = new Subject
                            {
                                Id = subj_id,
                                ParentId = Convert.ToInt32(dr["parent_id"].ToString()),
                                Code = dr["subj_code"].ToString(),
                                ShortDesc = dr["subj_title"].ToString(),
                                SubjDesc = dr["subj_desc"].ToString(),
                                LevelCode = dr["lvl_code"].ToString(),
                                LevelTypeDesc = dr["LevelTypeDesc"].ToString(),
                                StrandCode = dr["strand"].ToString(),
                                StrandDesc = dr["StrandName"].ToString(),
                                SubjAreaId = Convert.ToInt32(dr["subj_area"].ToString()),
                                SubjAreaDesc = dr["SubjArea"].ToString(),
                                SubjTypeId = Convert.ToInt32(dr["subj_type"].ToString()),
                                SubjTypeDesc = dr["SubType"].ToString(),
                                SubjClassftn = dr["subj_classftn"].ToString(),
                                SubjClassftnDesc = dr["STATDESC"].ToString(),
                                HasSubSubj = (bool)dr["has_child"],
                                FlagStat = (bool)dr["flag_stat"],
                                FlagDel = (bool)dr["flag_del"]
                            };
                        }
                    }
                }
            }

            return subj;
        }

        public void INSERT_SUBJECT(Subject subj, string userid = null)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE003", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "I");
                    cmd.Parameters.AddWithValue("@parent_id", subj.ParentId);
                    cmd.Parameters.AddWithValue("@subj_area", subj.SubjAreaId);
                    cmd.Parameters.AddWithValue("@subj_type", subj.SubjTypeId);
                    cmd.Parameters.AddWithValue("@code", subj.Code);
                    cmd.Parameters.AddWithValue("@title", subj.ShortDesc);
                    cmd.Parameters.AddWithValue("@desc", subj.SubjDesc);
                    cmd.Parameters.AddWithValue("@lvl_code", subj.LevelCode);
                    cmd.Parameters.AddWithValue("@strand", subj.StrandCode);
                    cmd.Parameters.AddWithValue("@classftn", subj.SubjClassftn);
                    cmd.Parameters.AddWithValue("@seq_num", subj.SeqNum);
                    cmd.Parameters.AddWithValue("@is_req_comp", subj.HasComponents);
                    cmd.Parameters.AddWithValue("@has_letter_grd", subj.HasLetterGrade);
                    cmd.Parameters.AddWithValue("@grd_entry", subj.GradeStyle);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void INSERT_SUBJECTS(string subjcode, string shortdesc, string subjdesc, string lvlcode, int subjarea, int subjtype, string username, string strand, string subjclasf)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE003]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@subjcode", subjcode);
                    cmd.Parameters.AddWithValue("@shortdesc", shortdesc);
                    cmd.Parameters.AddWithValue("@subjdesc", subjdesc);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@subjarea", subjarea);
                    cmd.Parameters.AddWithValue("@subjtype", subjtype);
                    if (lvlcode == "G11" || lvlcode == "G12")
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@subjclasf", subjclasf);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_SUBJECT(Subject subj, string userid = null)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE003", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "U");
                    cmd.Parameters.AddWithValue("@Id", subj.Id);
                    cmd.Parameters.AddWithValue("@parent_id", subj.ParentId);
                    cmd.Parameters.AddWithValue("@subj_area", subj.SubjAreaId);
                    cmd.Parameters.AddWithValue("@subj_type", subj.SubjTypeId);
                    cmd.Parameters.AddWithValue("@title", subj.ShortDesc);
                    cmd.Parameters.AddWithValue("@desc", subj.SubjDesc);
                    cmd.Parameters.AddWithValue("@lvl_code", subj.LevelCode);
                    cmd.Parameters.AddWithValue("@strand", subj.StrandCode);
                    cmd.Parameters.AddWithValue("@classftn", subj.SubjClassftn);
                    cmd.Parameters.AddWithValue("@flag_stat", subj.FlagStat);
                    cmd.Parameters.AddWithValue("@flag_del", subj.FlagDel);
                    cmd.Parameters.AddWithValue("@seq_num", subj.SeqNum);
                    cmd.Parameters.AddWithValue("@is_req_comp", subj.HasComponents);
                    cmd.Parameters.AddWithValue("@has_letter_grd", subj.HasLetterGrade);
                    cmd.Parameters.AddWithValue("@grd_entry", subj.GradeStyle);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_SUBJECTS(string shortdesc, string subjdesc, string lvlcode, int subjarea, int subjtype, Boolean stat, string username, int stid, string strand, string subjclasf)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE003]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@shortdesc", shortdesc);
                    cmd.Parameters.AddWithValue("@subjdesc", subjdesc);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@subjarea", subjarea);
                    cmd.Parameters.AddWithValue("@subjtype", subjtype);
                    cmd.Parameters.AddWithValue("@stat", stat);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@stid", stid);
                    if (lvlcode == "G11" || lvlcode == "G12")
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@subjclasf", subjclasf);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_SUBJECTS(string username, int stid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE003]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@stid", stid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_SUBJECT_EXIST(string subjcode, int stid)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_MFE003]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subjcode", subjcode);
                    cmd.Parameters.AddWithValue("@stid", stid);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public DataTable GET_CURRICULUM(string swhere = null, string sorder = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE004]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_CURRICULUM(string curcode, string curdesc, string lvlcode, string username, string strand, string schyear, int sem, int term,  string sy)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE004]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@curcode", curcode);
                    cmd.Parameters.AddWithValue("@curdesc", curdesc);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    //string swhere,
                    //cmd.Parameters.AddWithValue("@swhere", swhere);
                    if (lvlcode == "G11" || lvlcode == "G12")
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_CURRICULUM_EXIST(string curcode)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_MFE004]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@curcode", curcode);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public bool CHECK_SUBJECT_CURRICULUM_EXIST(int subjid, int ssubjid, string curcode, int spclzid)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_MFE012]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subjid", subjid);
                    //if (ssubjid == 0)
                    //{
                    //    cmd.Parameters.AddWithValue("@ssubjid", DBNull.Value);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@ssubjid", ssubjid);
                    //}
                    cmd.Parameters.AddWithValue("@ssubjid", ssubjid);
                    //if (spclzid == 0)
                    //{
                    //    cmd.Parameters.AddWithValue("@spclzid", DBNull.Value);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@spclzid", spclzid);
                    //}
                    cmd.Parameters.AddWithValue("@spclzid", spclzid);
                    cmd.Parameters.AddWithValue("@curcode", curcode);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void UPDATE_CURRICULUM(string curcode, string curdesc, string lvlcode, string username, string strand, string schyear, int sem, int term)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE004]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@curcode", curcode);
                    cmd.Parameters.AddWithValue("@curdesc", curdesc);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    if (lvlcode == "G11" || lvlcode == "G12")
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_CURRICULUM(string username, string curcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE004]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@curcode", curcode);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_SUBJECT_CURRICULUM(string currcode, int subjid, int ssubjid, int units, string username, int spclztnid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE012]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@currcode", currcode);
                    cmd.Parameters.AddWithValue("@subjid", subjid);
                    if (ssubjid == 0)
                    {
                        cmd.Parameters.AddWithValue("@ssubjid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ssubjid", ssubjid);
                    }
                    if (spclztnid == 0)
                    {
                        cmd.Parameters.AddWithValue("@specializeid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@specializeid", spclztnid);
                    }
                    cmd.Parameters.AddWithValue("@units", units);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_SUBJECT_CURRICULUM(string username, int subjcurid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE012]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@subjcurid", subjcurid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_CURRICULUM_PER_SUBJECT(string swhere = null, string sorder = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE012]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void COPY_CURRICULUM(string excurcode, string userid, int newsem, int newterm, string newcurcode, string newcurdesc, string newsy, bool iscopystud)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCOPY_CURRICULUM]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@excurcode", excurcode);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@newsem", newsem);
                    cmd.Parameters.AddWithValue("@newterm", newterm);
                    cmd.Parameters.AddWithValue("@newcurcode", newcurcode);
                    cmd.Parameters.AddWithValue("@newcurdesc", newcurdesc);
                    cmd.Parameters.AddWithValue("@newsy", newsy);
                    cmd.Parameters.AddWithValue("@iscopystud", iscopystud);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_SUBJECT_TEACHERS(string select, string where = null, string group = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_SUBJECT_LEVEL_TEACHER", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GET_SUBJECT_SUB_TEACHERS(string classCode)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                var sql = @"SELECT * FROM Registration.Subj_Class_SubtiTchr 
                            WHERE class_code = @classCode";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public void SAVE_SUB_TEACHERS(string classCode, List<string> subTeachersId)
        {
            var sql = "DELETE FROM Registration.Subj_Class_SubtiTchr WHERE class_code = @classCode; ";

            var count = 1;
            foreach (var tchrId in subTeachersId)
            {
                var tchrIdParam = "@tchr_id" + count;
                sql = sql + $@"INSERT INTO Registration.Subj_Class_SubtiTchr (class_code, tchr_id) 
                                    VALUES (@classCode, {tchrIdParam}); ";
                count++;
            }
            count = 1;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    foreach (var tchrId in subTeachersId)
                    {
                        var tchrIdParam = "@tchr_id" + count;
                        cmd.Parameters.AddWithValue(tchrIdParam, tchrId);
                        count++;
                    }
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_COMPONENT_TYPE(string swhere = null, string sorder = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE006]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_COMPONENTS(string swhere = null, string sorder = null, string sselect = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE007]", cn))
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

        public DataTable GET_GRADE_COMPONENTS(string swhere = null, string sorder = null, string sselect = null, string sgroup = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE011]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    cmd.Parameters.AddWithValue("@sgroup", sgroup);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_GRADE_COMPONENTS(string sy, string lvlcode, string strand, string sem, string term, int subjid, int ssubjid, int spclzid, int comptypeid, int compid, string action, double perc, Boolean transmute, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE011]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N1");
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    if (lvlcode == "G11" || lvlcode == "G12")
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@subjid", subjid);
                    if (ssubjid == 0)
                    {
                        cmd.Parameters.AddWithValue("@ssubjid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ssubjid", ssubjid);
                    }
                    if (spclzid == 0)
                    {
                        cmd.Parameters.AddWithValue("@spclzid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@spclzid", spclzid);
                    }
                    cmd.Parameters.AddWithValue("@comptypeid", comptypeid);
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@perc", perc);
                    cmd.Parameters.AddWithValue("@transmute", transmute);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_GRADE_COMPONENTS(string action, double perc, Boolean transmute, string username, int compid, string swhere)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE011]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "E1");
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@perc", perc);
                    cmd.Parameters.AddWithValue("@transmute", transmute);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_GRADE_COMPONENTS(string username, int compid, string swhere)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE011]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D1");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void COPY_GRADE_COMPONENTS(string sy, string lvlcode, string strand, string sem, string term, int subjid, int ssubjid, int spclzid, string username, string csy, string clvlcode, string cstrand, string csem, string cterm, int csubjid, int cssubjid, int cspclzid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE011]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "C");
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    if (lvlcode == "G11" || lvlcode == "G12")
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@subjid", subjid);
                    if (ssubjid == 0)
                    {
                        cmd.Parameters.AddWithValue("@ssubjid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ssubjid", ssubjid);
                    }
                    if (spclzid == 0)
                    {
                        cmd.Parameters.AddWithValue("@spclzid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@spclzid", spclzid);
                    }
                    //copy
                    cmd.Parameters.AddWithValue("@csy", csy);
                    cmd.Parameters.AddWithValue("@clvlcode", clvlcode);
                    if (clvlcode == "G11" || clvlcode == "G12")
                    {
                        cmd.Parameters.AddWithValue("@cstrand", cstrand);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@cstrand", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@csem", csem);
                    cmd.Parameters.AddWithValue("@cterm", cterm);
                    cmd.Parameters.AddWithValue("@csubjid", csubjid);
                    if (cssubjid == 0)
                    {
                        cmd.Parameters.AddWithValue("@cssubjid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@cssubjid", cssubjid);
                    }
                    if (cspclzid == 0)
                    {
                        cmd.Parameters.AddWithValue("@cspclzid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@cspclzid", cspclzid);
                    }
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_GRADE_COMPONENTS(string swhere)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_MFE011]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_SUB_COMPONENTS(string sy, string lvlcode, string strand, string sem, string term, int subjid, int ssubjid, int spclzid, int comptypeid, int compid, string action, double perc, Boolean transmute, string username, string swhere, string subcomp, string subcompdesc)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE011]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N2");
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    if (lvlcode == "G11" || lvlcode == "G12")
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@subjid", subjid);
                    if (ssubjid == 0)
                    {
                        cmd.Parameters.AddWithValue("@ssubjid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ssubjid", ssubjid);
                    }
                    if (spclzid == 0)
                    {
                        cmd.Parameters.AddWithValue("@spclzid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@spclzid", spclzid);
                    }
                    cmd.Parameters.AddWithValue("@comptypeid", comptypeid);
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@perc", perc);
                    cmd.Parameters.AddWithValue("@transmute", transmute);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@subcomp", subcomp);
                    cmd.Parameters.AddWithValue("@subcompdesc", subcompdesc);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_SUB_COMPONENTS(string username, int gradecompid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE011]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D2");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@gradecompid", gradecompid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_COMPONENTS(string swhere)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_MFE007]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_COMPONENTS(string compdesc, int comptypeid, int comparr, string username, string compcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE007]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@compdesc", compdesc);
                    cmd.Parameters.AddWithValue("@comptypeid", comptypeid);
                    cmd.Parameters.AddWithValue("@comparr", comparr);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@compcode", compcode);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_COMPONENTS(string username, int compid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE007]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_COMPONENTS(int comptypeid, string compdesc, int comparr, string username, int compid, string compcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE007]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@compdesc", compdesc);
                    cmd.Parameters.AddWithValue("@comptypeid", comptypeid);
                    cmd.Parameters.AddWithValue("@comparr", comparr);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@compcode", compcode);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_STUD_CUR_LIST(string swhere, string swhere2)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE009]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@swhere2", swhere2);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_STUDENT_CURRICULUM(string schyear, string studnum, string curcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE009]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@curcode", curcode);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_STUDENT_CURRICULUM(string scid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE009]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@scid", scid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_STUDENT_CURRICULUM(string curcode, string scid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE009]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@curcode", curcode);
                    cmd.Parameters.AddWithValue("@scid", scid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void COMPUTE_CLASS_GRADES(string classCode, int grdType, int sem, int term, bool computeFinal, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCOMPUTE_CLASS_GRADES", cn))
                {
                    cn.Open();
                    cmd.CommandTimeout = 300;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    cmd.Parameters.AddWithValue("@grdType", grdType);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@isFinal", computeFinal);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void COMPUTE_CLASS_GRADES_MATATAG(string classCode, int grdType, int sem, int term, bool computeFinal, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCOMPUTE_CLASS_GRADES_MATATAG", cn))
                {
                    cn.Open();
                    cmd.CommandTimeout = 300;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    cmd.Parameters.AddWithValue("@grdType", grdType);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@isFinal", computeFinal);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool CHECK_COMPSCORE(string swhere)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_MFE016]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public DataTable GET_TRANSGRADE(string xselect = null, string xwhere = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE013]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sselect", xselect);
                    cmd.Parameters.AddWithValue("@swhere", xwhere);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_STUDENT_GRADE(string studno, string sy, string sem, string lcat, string levelcode, string sec, string ccode, string scode, string subjtype, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE016]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@trans", 'A');
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@levelcat", lcat);
                    cmd.Parameters.AddWithValue("@levelcode", levelcode);
                    cmd.Parameters.AddWithValue("@section", sec);
                    cmd.Parameters.AddWithValue("@classcode", ccode);
                    cmd.Parameters.AddWithValue("@subjcode", scode);
                    cmd.Parameters.AddWithValue("@subjtype", subjtype);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    //cn.Close();
                }
            }
        }

        public void UPDATE_STUDENT_GRADE(string gradefield, string cgrade, int sem, string studno, string classcode, string scode, string subtype, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE016]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@trans", "U");
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@grade_field", gradefield);
                    cmd.Parameters.AddWithValue("@grade_value", cgrade);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@classcode", classcode);
                    cmd.Parameters.AddWithValue("@subjcode", scode);
                    cmd.Parameters.AddWithValue("@subjtype", subtype);

                    cmd.ExecuteNonQuery();

                    //cn.Close();
                }
            }
        }


        //posting query
        public DataTable CHECK_STUDENT_WOUT_GRADES(string sy, string lvlcode, int sem, int term, string posttype, string subjtypecode, string section, string syvalue)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_STUDENT_WOUT_GRADES]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@subjtypecode", subjtypecode);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable CHECK_STUDENT_WITH_INC_GRADES_ACAD(string sy, string syvalue, string lvlcode, int sem, int term, string posttype, string subjtypecode, string section)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_STUDENT_WITH_INC_GRADES_ACAD]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@subjtypecode", subjtypecode);
                    cmd.Parameters.AddWithValue("@section", section);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        //old
        public void INSERT_POSTING_GRADE(string sy, string syvalue, string lvlcode, int sem, int term, string posttype, string username, string section, bool finalpost)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spINSERT_POST_GRADE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@finalpost", finalpost);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        //old
        public void UPDATE_POSTING_GRADE_CONDUCT(string sy, string syvalue, string lvlcode, int sem, int term, string posttype, string username, string section, bool finalpost)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spINSERT_CONDUCT_GRADE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@finalpost", finalpost);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void COMPUTE_AVERAGE_SUBSUBJECT(string sy, string syvalue, string lvlcode, int sem, int term, string posttype, string username, string section, string subtypecode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                //string sqlstoredproc;
                if (posttype == "T")
                {
                    using (SqlCommand cmd = new SqlCommand("[Evltn].[spCOMPUTE_AVERAGE_SUBSUBJECT]", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 3600;
                        cmd.Parameters.AddWithValue("@sy", sy);
                        cmd.Parameters.AddWithValue("@syvalue", syvalue);
                        cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                        cmd.Parameters.AddWithValue("@sem", sem);
                        cmd.Parameters.AddWithValue("@term", term);
                        cmd.Parameters.AddWithValue("@posttype", posttype);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@section", section);
                        cmd.Parameters.AddWithValue("@subtypecode", subtypecode);
                        cmd.ExecuteNonQuery();
                        cn.Close();
                    }
                }
                else if (posttype == "S")
                {
                    using (SqlCommand cmd = new SqlCommand("[Evltn].[spCOMPUTE_AVERAGE_SUBSUBJECT_SEM]", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@sy", sy);
                        cmd.Parameters.AddWithValue("@syvalue", syvalue);
                        cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                        cmd.Parameters.AddWithValue("@sem", sem);
                        cmd.Parameters.AddWithValue("@term", term);
                        cmd.Parameters.AddWithValue("@posttype", posttype);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@section", section);
                        cmd.Parameters.AddWithValue("@subtypecode", subtypecode);
                        cmd.ExecuteNonQuery();
                        cn.Close();
                    }
                }
            }
        }

        public void POST_ACAD(string sy, string syvalue, string lvlcode, int sem, int term, string posttype, string username, string section, bool finalpost)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string sqlstoredproc;
                if (posttype == "T")
                {
                    sqlstoredproc = "[Evltn].[spPOST_ACAD_TERM]";
                }
                else if (posttype == "S")
                {
                    if (lvlcode == "G11" || lvlcode=="G12")
                    {
                        sqlstoredproc = "[Evltn].[spPOST_ACAD_SEM_SHS]";
                    }
                    else
                    {
                        sqlstoredproc = "[Evltn].[spPOST_ACAD_SEM]";
                    }
                }
                else
                {
                    sqlstoredproc = "[Evltn].[spPOST_ACAD_YE]";
                }

                using (SqlCommand cmd = new SqlCommand(sqlstoredproc, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@finalpost", finalpost);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        } 
        
        // @gradeType(1 = Acad, 2 = Conduct)
        public void POST_MATATAG(string sy, string syvalue, string lvlcode, int sem, int term, string posttype, string username, string section, bool finalpost, int gradeType)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string sqlstoredproc = "[Evltn].[spPOST_ACAD_MATATAG]";

                using (SqlCommand cmd = new SqlCommand(sqlstoredproc, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@finalpost", finalpost);
                    cmd.Parameters.AddWithValue("@grdType", gradeType);

                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void POST_CONDUCT(string sy, string syvalue, string lvlcode, int sem, int term, string posttype, string username, string section, bool finalpost)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string sqlstoredproc;
                if (posttype == "T")
                {
                    sqlstoredproc = "[Evltn].[spPOST_CONDUCT_TERM]";
                }
                else if (posttype == "S")
                {
                    sqlstoredproc = "[Evltn].[spPOST_CONDUCT_SEM]";
                }
                else
                {
                    sqlstoredproc = "[Evltn].[spPOST_CONDUCT_YE]";
                }

                using (SqlCommand cmd = new SqlCommand(sqlstoredproc, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@finalpost", finalpost);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_POSTING_GRADE_GENDEPO(string sy, string syvalue, string lvlcode, int sem, int term, string posttype, string username, string section, bool finalpost)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spINSERT_GRADE_GENDEPO]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@finalpost", finalpost);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void POST_MI_KINDER(string sy, string lvlcode, int sem, int term, string posttype, string username, string section)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spPOST_MI_KINDER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3600;
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void POST_MI_NURSERY(string sy, string levelCode, int sem, int term, string section, string username)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spPOST_MI_NURSERY]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3600;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@levelcode", levelCode);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void COMPUTE_FINAL_GRD(string lvlcode,string section,string sy, int subjtype, int sem)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCOMPUTE_FINAL_GRD]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@subjtype", subjtype);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void COMPUTE_FINAL_GRD_PER_CLASS(string classcode, string sy, int subjtype, int sem)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCOMPUTE_FINAL_GRD_PER_CLASS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@classcode", classcode);
                    cmd.Parameters.AddWithValue("@subjtype", subjtype);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_POST_GRADE(string sy, string lvlcode, int sem, int term, string posttype, string section)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_MFE019]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@section", section);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public bool CHECK_EXISTING_CURRICULUM(string schyear, string lvlcode, string strand, int sem, int term)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE004]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    if (strand == "0")
                    {
                        //cmd.Parameters.AddWithValue("@strand", strand);
                        cmd.Parameters.AddWithValue("@transtype", "CR");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                        cmd.Parameters.AddWithValue("@transtype", "CS");
                    }
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public bool CHECK_READY_FOR_HONORS(string sy, string lvlcode, int sem, int term, string posttype, string section)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_READY_FOR_HONORS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@section", section);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void GENERATE_HONORS(string sy, string lvlcode, int sem, int term, string posttype, int honorid, string username, string section)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string storedproc;
                if (posttype == "T")
                {
                    storedproc = "[Evltn].[spGENERATE_HONORS_TERM]";
                }
                else if (posttype == "S")
                {
                    storedproc = "[Evltn].[spGENERATE_HONORS_SEM]";
                }
                else
                {
                    storedproc = "[Evltn].[spGENERATE_HONORS_YE]";
                }
                //"[Evltn].[spGENERATE_HONORS]"
                using (SqlCommand cmd = new SqlCommand(storedproc, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@honorid", honorid);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void ARCHIVE_HONORS(string sy, string lvlcode, int sem, int term, string posttype, string username, string section)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spARCHIVE_HONORS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1000;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@posttype", posttype);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_HONORS(string select = null, string where = null, string group = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE020]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_EXCLUDE_STUDENT(string swhere = null, string sorder = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_EXCLUDE_LIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void TRANS_EXCLUDE_STUD(Boolean flgexclude, string studnum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_EXCLUDE_STUD]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flgexclude", flgexclude);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public string GET_CURRENT_SCHOOL_YEAR()
        {
            string x;
            string sql = "Select gp_Value from Utilities.Global_Parameters where gp_Code='CURSY2'";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public DataTable GET_STATISTIC_HDR(string lvlcode,string strand, int sem, int term, string syvalue, string sy, int grdtype)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_HDR]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@grdtype", grdtype);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        //public DataTable GET_STATISTIC_HDR_SHS(string lvlcode, string strand, int sem, int term, string syvalue, string sy, int grdtype)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_HDR_SHS]", cn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
        //            cmd.Parameters.AddWithValue("@strand", strand);
        //            cmd.Parameters.AddWithValue("@sem", sem);
        //            cmd.Parameters.AddWithValue("@term", term);
        //            cmd.Parameters.AddWithValue("@syvalue", syvalue);
        //            cmd.Parameters.AddWithValue("@sy", sy);
        //            cmd.Parameters.AddWithValue("@grdtype", grdtype);
        //            cn.Open();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt);
        //            cn.Close();
        //        }
        //    }
        //    return dt;
        //}

        public DataTable GET_STATISTIC_HDR_NO_COMPONENT(string lvlcode, int sem, int term, string syvalue, string sy, int grdtype)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_HDR_NO_COMPONENT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@grdtype", grdtype);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_STATISTICS_STUDENTS_NOT_ENCODED(int subjid, string sy, int sem, int term, bool isencoded, int grdtype,int compid, string syvalue,string strand)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_DTL_ENCODE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "SNE");
                    cmd.Parameters.AddWithValue("@subjid", subjid);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@isencoded", isencoded);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@grdtype", grdtype);
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        //public DataTable GET_STATISTICS_STUDENTS_NOT_ENCODED_SHS(int subjid, string sy, int sem, int term, bool isencoded, int grdtype, int compid, string syvalue, string strand)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_DTL_ENCODE_SHS]", cn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@transtype", "SNS");
        //            cmd.Parameters.AddWithValue("@strand", strand);
        //            cmd.Parameters.AddWithValue("@subjid", subjid);
        //            cmd.Parameters.AddWithValue("@sem", sem);
        //            cmd.Parameters.AddWithValue("@term", term);
        //            cmd.Parameters.AddWithValue("@isencoded", isencoded);
        //            cmd.Parameters.AddWithValue("@sy", sy);
        //            cmd.Parameters.AddWithValue("@grdtype", grdtype);
        //            cmd.Parameters.AddWithValue("@compid", compid);
        //            cmd.Parameters.AddWithValue("@syvalue", syvalue);
        //            cn.Open();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt);
        //            cn.Close();
        //        }
        //    }
        //    return dt;
        //}

        public DataTable GET_STATISTICS_STUDENTS_ENCODED(int subjid, string sy, int sem, int term, bool isencoded, int grdtype, int compid, string syvalue,string strand)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_DTL_ENCODE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "SWE");
                    cmd.Parameters.AddWithValue("@subjid", subjid);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@isencoded", isencoded);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@grdtype", grdtype);
                    cmd.Parameters.AddWithValue("@compid", compid);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        //public DataTable GET_STATISTICS_STUDENTS_ENCODED_SHS(int subjid, string sy, int sem, int term, bool isencoded, int grdtype, int compid, string syvalue, string strand)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_DTL_ENCODE_SHS]", cn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@transtype", "SWS");
        //            cmd.Parameters.AddWithValue("@strand", strand);
        //            cmd.Parameters.AddWithValue("@subjid", subjid);
        //            cmd.Parameters.AddWithValue("@sem", sem);
        //            cmd.Parameters.AddWithValue("@term", term);
        //            cmd.Parameters.AddWithValue("@isencoded", isencoded);
        //            cmd.Parameters.AddWithValue("@sy", sy);
        //            cmd.Parameters.AddWithValue("@grdtype", grdtype);
        //            cmd.Parameters.AddWithValue("@compid", compid);
        //            cmd.Parameters.AddWithValue("@syvalue", syvalue);
        //            cn.Open();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt);
        //            cn.Close();
        //        }
        //    }
        //    return dt;
        //}

        public DataTable GET_STATISTICS_STUDENTS_NOT_ENROLLED(int subjid, string sy, int sem, string lvlcode,string strand)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_DTL_ENROLL]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //if (strand == "0")
                    //{
                    //    cmd.Parameters.AddWithValue("@transtype", "RSN");
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@transtype", "RHN");
                    //}
                    cmd.Parameters.AddWithValue("@subjid", subjid);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_COMPONENTS_FOR_STAT(int sem, int term,string sy, int subjid, bool isencoded,int grdtype,string syvalue, string strand)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_DTL_COMPONENT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(isencoded== true)
                    {
                        cmd.Parameters.AddWithValue("@transtype", "SWC");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@transtype", "SNC");
                    }
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@subjid", subjid);
                    cmd.Parameters.AddWithValue("@isencoded", isencoded);
                    cmd.Parameters.AddWithValue("@grdtype", grdtype);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@strand", strand);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        //public DataTable GET_COMPONENTS_FOR_STAT_SHS(int sem, int term, string sy, int subjid, bool isencoded, int grdtype, string syvalue, string strand)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_STATISTICS_DTL_COMPONENT_SHS]", cn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //             if (isencoded == true)
        //            {
        //                cmd.Parameters.AddWithValue("@transtype", "SWCS");
        //                cmd.Parameters.AddWithValue("@strand", strand);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@transtype", "SNCS");
        //                cmd.Parameters.AddWithValue("@strand", strand);
        //            }
        //            cmd.Parameters.AddWithValue("@sem", sem);
        //            cmd.Parameters.AddWithValue("@term", term);
        //            cmd.Parameters.AddWithValue("@sy", sy);
        //            cmd.Parameters.AddWithValue("@subjid", subjid);
        //            cmd.Parameters.AddWithValue("@isencoded", isencoded);
        //            cmd.Parameters.AddWithValue("@grdtype", grdtype);
        //            cmd.Parameters.AddWithValue("@syvalue", syvalue);
        //            cn.Open();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt);
        //            cn.Close();
        //        }
        //    }
        //    return dt;
        //}


        public bool CHECK_COMPUTE_FINAL_GRADE(string lvlcode, string section, string sy, int sem)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_COMPUTE_FINAL_GRADE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@section", section);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }
        
        public void DELETE_COMPUTED_GRADES(string classcode, string stud_no, string sy, int sem)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE016", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@trans", "X");
                    cmd.Parameters.AddWithValue("@studno", stud_no);
                    cmd.Parameters.AddWithValue("@SY", sy);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_COMPGRADE(string xselect = null, string xwhere = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE0161]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sselect", xselect);
                    cmd.Parameters.AddWithValue("@swhere", xwhere);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_FOR_ADDENDUM_VIOLATION(int sem, int term, string sy, string addtype, string studno)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_FOR_ADDENDUM]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@addtype", addtype);
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@gettype", "VL");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void SAVE_STUDENT_VIOLATION_GRADE(string sy,string classcode, string adtype, int sem, int term, string studno,
            string newgrd,string prevgrd, string userid, string remarks,int vid,string prevgendepo, string newgendepo, string syvalue, string lvlcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_STUD_VIOLATION]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sqltype", "SAVE");
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@classcode", classcode);
                    cmd.Parameters.AddWithValue("@adtype", adtype);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@newgrd", newgrd);
                    cmd.Parameters.AddWithValue("@prevgrd", prevgrd);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@vid", vid);
                    cmd.Parameters.AddWithValue("@prevgendepo", prevgendepo);
                    cmd.Parameters.AddWithValue("@newgendepo", newgendepo);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_FOR_ADDENDUM(string classcode, int subjtype, int sem, int term, string sy, string addtype, string studno)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_FOR_ADDENDUM]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classcode", classcode);
                    cmd.Parameters.AddWithValue("@subjtype", subjtype);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@addtype", addtype);
                    cmd.Parameters.AddWithValue("@studno", studno);
                    cmd.Parameters.AddWithValue("@gettype", "AD");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void ADDENDUM_GRADES(int subjtype, int sem, int term, string sy, string syvalue, string addtype, string studnum, int id, string newgrd, string username,string classcode, string lvlcode,string remarks,string prevgrd)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Evltn].[spADDENDUM_GRADE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subjtype", subjtype);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@syvalue", syvalue);
                    cmd.Parameters.AddWithValue("@addtype", addtype);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@newgrd", newgrd);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@classcode", classcode);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@prevgrd", prevgrd);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string GET_CONDUCT_PERCENT()
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select gp_Value from Utilities.Global_Parameters where gp_Code ='CDPERC' ", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    x = (string)cmd.ExecuteScalar();
                }
            }
            return x;
        }

        public string GET_DEPORTMENT_PERCENT()
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select gp_Value from Utilities.Global_Parameters where gp_Code ='DEPPER' ", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    x = (string)cmd.ExecuteScalar();
                }
            }
            return x;
        }

        public DataTable GET_RAW_SCORES(string where = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_RAW_SCORES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@order", order);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GET_TOTAL_ITEMS(string where = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spGET_TOTAL_ITEMS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@order", order);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        private bool CheckTotalItems(string classCode, int sem, int term, int compId, int subCompId, int grdType)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE014", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@class_code", classCode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@comp_id", compId);
                    cmd.Parameters.AddWithValue("@sub_comp_id", subCompId);
                    cmd.Parameters.AddWithValue("@grd_type", grdType);
                    r = (bool)cmd.ExecuteScalar();
                }
            }
            return r;
        }

        public void DeleteTotalItems(string classCode, int sem, int term, int compId, int subCompId, int grdType, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE014", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "D");
                    cmd.Parameters.AddWithValue("@class_code", classCode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@comp_id", compId);
                    cmd.Parameters.AddWithValue("@sub_comp", subCompId);
                    cmd.Parameters.AddWithValue("@grd_type", grdType);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveTotalItems(string classCode, int sem, int term, int compId, int subCompId, int totalItem, int grdType, string userId, bool isRequired)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE014", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", (CheckTotalItems(classCode, sem, term, compId, subCompId, grdType) ? "U" : "I"));
                    cmd.Parameters.AddWithValue("@class_code", classCode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@comp_id", compId);
                    cmd.Parameters.AddWithValue("@sub_comp", subCompId);
                    cmd.Parameters.AddWithValue("@total_items", totalItem);
                    cmd.Parameters.AddWithValue("@grd_type", grdType);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@is_required", isRequired);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveRawScores(string classCode, int sem, int term, string studNo, int compId, int subCompId, string score, int grdType, bool isEncoded, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE015", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "I");
                    cmd.Parameters.AddWithValue("@class_code", classCode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@stud_no", studNo);
                    cmd.Parameters.AddWithValue("@comp_id", compId);
                    cmd.Parameters.AddWithValue("@sub_comp", subCompId);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.Parameters.AddWithValue("@grd_type", grdType);
                    cmd.Parameters.AddWithValue("@is_encoded", isEncoded);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRawScores(int rawId, string score, bool isEncoded, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE015", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "U");
                    cmd.Parameters.AddWithValue("@raw_id", rawId);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.Parameters.AddWithValue("@is_encoded", isEncoded);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRawScore(int rawId, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE015", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "D");
                    cmd.Parameters.AddWithValue("@raw_id", rawId);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int CheckRawScoreTransmutedGrade(string classCode, int sem,int term, string studNo, int compId, int grdType)
        {
            int r = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE013", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@studNo", studNo);
                    cmd.Parameters.AddWithValue("@compId", compId);
                    cmd.Parameters.AddWithValue("@grdType", grdType);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            r = Convert.ToInt32(dr["ID"].ToString());
                        }
                    }
                }
            }
            return r;
        }

        public void SaveRawScoreTransmutedGrade(string classCode, int sem, int term, string studNo, int compId, string rawSum, string transGrade, int grdType, string userId, string sy = null)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE013", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "I");
                    cmd.Parameters.AddWithValue("@class_code", classCode);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@term", term);
                    cmd.Parameters.AddWithValue("@stud_no", studNo);
                    cmd.Parameters.AddWithValue("@comp_id", compId);
                    cmd.Parameters.AddWithValue("@sum", rawSum);
                    cmd.Parameters.AddWithValue("@trans", transGrade);
                    cmd.Parameters.AddWithValue("@grd_type", grdType);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRawScoreTransmutedGrade(int id, string rawSum, string transGrade, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE013", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "U");
                    cmd.Parameters.AddWithValue("@sum_id", id);
                    cmd.Parameters.AddWithValue("@sum", rawSum);
                    cmd.Parameters.AddWithValue("@trans", transGrade);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool CheckRawScore(string classCode)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE015", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", string.Format("ClassCode = '{0}'", classCode));
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public bool CheckRawScore(string studNo, string classCode)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd= new SqlCommand("Evltn.spCHECK_MFE015", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", string.Format("ClassCode = '{0}' AND StudNo = '{1}'", classCode, studNo));
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public bool CheckFinalGrade(string classCode)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE016", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", string.Format("a.classcode = '{0}' " +
                        "AND a.sy = (SELECT SYDesc FROM xSystem.SchoolYear_RF WHERE [Status] = 1)", classCode));
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public bool CheckFinalGrade(string studNo, string classCode)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE016", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", string.Format("a.classcode = '{0}' AND a.studno = '{1}' " +
                        "AND a.sy = (SELECT SYDesc FROM xSystem.SchoolYear_RF WHERE [Status] = 1)", classCode, studNo));
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public bool CheckFinalGrade(string studNo, int sem, string classCode, int subjId, int grdType, string grdField = null)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Evltn.spCHECK_MFE016", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@swhere", string.Format("a.classcode = '{0}' AND a.studno = '{1}' AND " +
                        "a.sy = (SELECT SYDesc FROM xSystem.SchoolYear_RF WHERE [Status] = 1) AND a.sem = {2} AND a.subjcode = {3} AND a.subjtype = {4} " +
                        (string.IsNullOrEmpty(grdField) ? "" : " AND a." + grdField + " IS NULL"), classCode, studNo, sem, subjId, grdType));
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public bool IsCoordinator(int empId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string query = @"
                SELECT COUNT(*) 
                FROM Evltn.MFE001 a
                WHERE a.CoorEmpID = @empId

                UNION ALL

                SELECT COUNT(*) 
                FROM Evltn.MFE001_Multiple_Coor mc
                WHERE mc.CoorEmpID = @empId";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@empId", empId);
                    cn.Open();
                    int total = 0;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            total += reader.GetInt32(0);
                        }
                    }

                    return total > 0;
                }
            }
        }

        public void SaveAsFinalGrade(string studNo, int sem, int term, string classCode, int subjId, int grdType, string finalGrade, string levelCode, string levelcatCode, string userId)
        {
            if (!CheckFinalGrade(studNo, sem, classCode, subjId, grdType))
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Evltn.spINSERT_MFE016", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@transtype", "A");
                        cmd.Parameters.AddWithValue("@studno", studNo);
                        cmd.Parameters.AddWithValue("@sem", sem);
                        cmd.Parameters.AddWithValue("@levelcatcode", levelcatCode);
                        cmd.Parameters.AddWithValue("@levelcode", levelCode);
                        cmd.Parameters.AddWithValue("@classcode", classCode);
                        cmd.Parameters.AddWithValue("@subjcode", subjId);
                        cmd.Parameters.AddWithValue("@subjtype", grdType);
                        cmd.Parameters.AddWithValue("@grade_prd" + term.ToString(), finalGrade);
                        cmd.Parameters.AddWithValue("@username", userId);
                        cmd.ExecuteNonQuery();
                    }
                }
            } else
            {
                using (SqlConnection cn = new SqlConnection(cBase.CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Evltn.spTRANS_MFE016", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@trans", "U");
                        cmd.Parameters.AddWithValue("@studno", studNo);
                        cmd.Parameters.AddWithValue("@sem", sem);
                        cmd.Parameters.AddWithValue("@levelcode", levelCode);
                        cmd.Parameters.AddWithValue("@classcode", classCode);
                        cmd.Parameters.AddWithValue("@subjcode", subjId);
                        cmd.Parameters.AddWithValue("@subjtype", grdType);
                        string grd_field = "grade_prd" + term.ToString();
                        cmd.Parameters.AddWithValue("@grade_field", grd_field);
                        cmd.Parameters.AddWithValue("@grade_value", finalGrade);
                        cmd.Parameters.AddWithValue("@username", userId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        #region comment codes
        //public void UPDATE_SUBJECT_CURRICULUM(int subjcurid, int subjid, int ssubjid, int units, string username)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE012]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@transtype", "E");
        //            cmd.Parameters.AddWithValue("@username", username);
        //            cmd.Parameters.AddWithValue("@subjcurid", subjcurid);
        //            cmd.Parameters.AddWithValue("@subjid", subjid);
        //            if (ssubjid == 0)
        //            {
        //                cmd.Parameters.AddWithValue("@ssubjid", DBNull.Value);
        //            }
        //            else
        //            {
        //                cmd.Parameters.AddWithValue("@ssubjid", ssubjid);
        //            }
        //            cmd.Parameters.AddWithValue("@units", units);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}

        //public DataTable GET_SUBSUBJ(string swhere = null, string sorder = null)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spGET_MFE009]", cn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@swhere", swhere);
        //            cmd.Parameters.AddWithValue("@sorder", sorder);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt);
        //        }
        //    }
        //    return dt;
        //}

        //public void INSERT_SUBSUBJ(string ssubjcode, string ssubjdesc, int subjid, string username)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE009]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@transtype", "N");
        //            cmd.Parameters.AddWithValue("@username", username);
        //            cmd.Parameters.AddWithValue("@ssubjcode", ssubjcode);
        //            cmd.Parameters.AddWithValue("@ssubjdesc", ssubjdesc);
        //            cmd.Parameters.AddWithValue("@subjid", subjid);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}

        //public bool CHECK_SUBSUBJ_EXIST(string ssubjcode)
        //{
        //    bool x = false;
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spCHECK_MFE009]", cn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@ssubjcode", ssubjcode);
        //            cn.Open();
        //            x = (bool)cmd.ExecuteScalar();
        //            cn.Close();
        //        }
        //    }
        //    return x;
        //}

        //public void DELETE_SUBSUBJ(string username, int ssubjid)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE009]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@transtype", "D");
        //            cmd.Parameters.AddWithValue("@username", username);
        //            cmd.Parameters.AddWithValue("@ssubjid", ssubjid);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}

        //public void UPDATE_SUBSUBJ(string ssubjdesc, int subjid, string username, int ssubjid)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Evltn].[spTRANS_MFE009]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@transtype", "E");
        //            cmd.Parameters.AddWithValue("@username", username);
        //            cmd.Parameters.AddWithValue("@ssubjid", ssubjid);
        //            cmd.Parameters.AddWithValue("@ssubjdesc", ssubjdesc);
        //            cmd.Parameters.AddWithValue("@subjid", subjid);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}
        #endregion
    }
}
