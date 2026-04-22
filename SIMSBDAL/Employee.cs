using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMSBDAL
{
    public class TeacherSubjects
    {
        public int Id { get; set; }
        public int TchrId { get; set; }
        public int SubjId { get; set; }
        public bool FlagDel { get; set; }
    }

    public class Employee : cBase
    {
        #region "PROPERTIES"
        public int Id { get; set; }
        public string EmpNum { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int PostnId { get; set; }
        public string Position { get; set; }
        public int DeptId { get; set; }
        public string Department { get; set; }
        public bool FlagActive { get; set; }
        public string DeptCatCode { get; set; }
        public string ephotopath { get; set; }
        public string nickname { get; set; }

        public string homesite { get; set; }
        // user info
        public string UserType { get; set; }
        public string AccessType { get; set; }
        #endregion

        public static DataTable GetEmployees(string limit = null, string where = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spGET_EMPLOYEES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@limit", limit);
                    cmd.Parameters.AddWithValue("@where", where);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }



        public static Employee GetInfo(int empId)
        {
            Employee emp = new Employee();
            DataTable dt = GetEmployees();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "USERID = " + empId.ToString();
            if (dv.Count > 0)
            {
                emp = new Employee
                {
                    Id = Convert.ToInt32(dv[0].Row["USERID"].ToString()),
                    EmpNum = dv[0].Row["EmpNum"].ToString(),
                    Firstname = dv[0].Row["FirstName"].ToString(),
                    Lastname = dv[0].Row["LastName"].ToString(),
                    Email = dv[0].Row["Emailaddress"].ToString(),
                    PostnId = Convert.ToInt32(dv[0].Row["PosID"].ToString()),
                    Position = dv[0].Row["Position"].ToString(),
                    DeptId = Convert.ToInt32(dv[0].Row["DeptID"].ToString()),
                    Department = dv[0].Row["Dept"].ToString(),
                    DeptCatCode = dv[0].Row["LevelCatCode"].ToString(),
                    ephotopath = dv[0].Row["ephotopath"].ToString()
                };
            }

            return emp;
        }

        public static Employee GetInfo(string usercode)
        {
            Employee emp = new Employee();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spGET_USER_INFO", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usercode", usercode);
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            emp = new Employee
                            {
                                Id = Convert.ToInt32(dr["EmpID"].ToString()),
                                EmpNum = dr["EmpNum"].ToString(),
                                Firstname = dr["Firstname"].ToString(),
                                Lastname = dr["LastName"].ToString(),
                                MiddleName = dr["MiddleName"].ToString(),
                                Email = dr["Emailaddress"].ToString(),
                                PostnId = Convert.ToInt32(dr["PosID"].ToString()),
                                Position = dr["Position"].ToString(),
                                DeptId = Convert.ToInt32(dr["DeptID"].ToString()),
                                Department = dr["Dept"].ToString(),
                                DeptCatCode = dr["LevelCatCode"].ToString(),
                                UserType = dr["UType"].ToString(),
                                ephotopath = dr["ephotopath"].ToString(),
                                AccessType = dr["AType"].ToString(),
                                nickname= dr["NickName"].ToString(),
                                homesite = dr["HomeSite"].ToString()
                            };
                        }
                    }
                }
            }

            return emp;
        }

        public DataTable GET_DEPARTMENT(string sselect , string swhere, string sorder)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_DEPARTMENT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_POSITION(string sselect, string swhere, string sorder)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_EMP_POSITION]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public bool CHECK_EMPLOYEE(string swhere)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spCHECK_EMPLOYEE]", cn))
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

        public DataTable GET_EMPLOYEE_SQL(string sselect, string swhere, string sgroup, string sorder)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_EMPLOYEE_SQL]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sgroup", sgroup);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_USER_ACCESS(string sselect, string swhere, string sgroup, string sorder)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_USER_ACCESS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sgroup", sgroup);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_LIST_OF_EMPLOYEES_FOR_DEPARTMENT(int deptid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_EMPLOYEE_MF]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "L");
                    cmd.Parameters.AddWithValue("@deptid", deptid);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public void INSERT_EMPLOYEE(string empnum, string fname, string lname , string mname, string emailad, int posid, int deptid, string barcode,string nickname)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_EMPLOYEE_MF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empnum", empnum);
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@lname", lname);
                    cmd.Parameters.AddWithValue("@mname", mname);
                    cmd.Parameters.AddWithValue("@emailad", emailad);
                    cmd.Parameters.AddWithValue("@posid", posid);
                    cmd.Parameters.AddWithValue("@deptid", deptid);
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                    cmd.Parameters.AddWithValue("@nickname", nickname);
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_EMPLOYEE(int empid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_EMPLOYEE_MF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", empid);
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void ACTIVATE_EMPLOYEE(int empid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_EMPLOYEE_MF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", empid);
                    cmd.Parameters.AddWithValue("@transtype", "A");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_EMPLOYEE(string empnum, string fname, string lname, string mname, string emailad, int posid, int deptid, string barcode, int empid,string nickname)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_EMPLOYEE_MF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empnum", empnum);
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@lname", lname);
                    cmd.Parameters.AddWithValue("@mname", mname);
                    cmd.Parameters.AddWithValue("@emailad", emailad);
                    cmd.Parameters.AddWithValue("@posid", posid);
                    cmd.Parameters.AddWithValue("@deptid", deptid);
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@empid", empid);
                    cmd.Parameters.AddWithValue("@nickname", nickname);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public string GET_PASSWORD_BY_EMPID(int empid)
        {
            string passwo = "";

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select Password from xSystem.UserCredentials_RF where EmpID = " + empid + "";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        
                        while (dr.Read())
                        {
                            passwo = dr["Password"].ToString();
                        }

                    }
                }
            }

            return passwo;
        }

        public string GET_ATYPE(string userid)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[xSystem].[spTRANS_User_Credentials_RF]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "T");
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_USER_ACCOUNT(int empid, string userid, string password, string uname, string atype, string passwordDescript)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_User_Credentials_RF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@empid", empid);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@uname", uname);
                    cmd.Parameters.AddWithValue("@atype", atype);
                    cmd.Parameters.AddWithValue("@passDecrypt", passwordDescript);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_USER_ACCOUNT(int empid, string userid, string password, Boolean status, string atype, string passwordDescript)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_User_Credentials_RF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@empid", empid);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@atype", atype);
                    cmd.Parameters.AddWithValue("@passDecrypt", passwordDescript);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_USER_ACCOUNT(int empid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_User_Credentials_RF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@empid", empid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void ACTIVATE_USER_ACCOUNT(int empid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_User_Credentials_RF]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "A");
                    cmd.Parameters.AddWithValue("@empid", empid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_USER_ACCESS(int accessid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_USER_ACCESS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accessid", accessid);
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_MENU_LIST(string sselect, string swhere, string sgroup, string sorder)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_MENUS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sgroup", sgroup);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_USER_ACCESS(string usercode, int menuid, int conid, int subid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_USER_ACCESS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usercode", usercode);
                    cmd.Parameters.AddWithValue("@menuid", menuid);
                    if (conid == 0)
                    {
                        cmd.Parameters.AddWithValue("@conid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@conid", conid);
                    }
                    if (subid == 0)
                    {
                        cmd.Parameters.AddWithValue("@subid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@subid", subid);
                    }
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool GET_STUDENT_HEALTH_CONCERN(string _studnum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Health].[spGET_HEALTH_CONCERN]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ANUM", _studnum);
                    cmd.Parameters.AddWithValue("@htype", "EMPLOYEE");

                    var dr = cmd.ExecuteReader();// cmd.ExecuteNonQuery();

                    return dr.HasRows;
                }
            }
        }

        public class Teacher
        {
            public int Id { get; set; }
            public int AssignId { get; set; }
            public string SY { get; set; }
            public string Level { get; set; }
            public string Section { get; set; }
            public int RoomId { get; set; }
            public string BldgDescription { get; set; }

            public static void InsertAssignedSection(Teacher tchr, string userid = null)
            {
                using(SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd= new SqlCommand("Registration.spINSERT_ADVISER_SECTION", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SY", tchr.SY);
                        cmd.Parameters.AddWithValue("@LEVELCODE", tchr.Level);
                        cmd.Parameters.AddWithValue("@SECTIONCODE", tchr.Section);
                        cmd.Parameters.AddWithValue("@ROOMID", tchr.RoomId);
                        cmd.Parameters.AddWithValue("@TEACHERID", tchr.Id);
                        cmd.Parameters.AddWithValue("@USERID", userid);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public static void UpdateAssignedSection(Teacher tchr, string userid = null)
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_ADVISER_SECTION", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", tchr.AssignId);
                        cmd.Parameters.AddWithValue("@ROOMID", tchr.RoomId);
                        cmd.Parameters.AddWithValue("@TEACHERID", tchr.Id);
                        cmd.Parameters.AddWithValue("@USERID", userid);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public static void DeleteAssignedSection(int asgn_id, string userid = null)
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Registration.spDELETE_ADVISER_SECTION", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", asgn_id);
                        cmd.Parameters.AddWithValue("@USERID", userid);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public static DataTable GetAssignedTeachers(string sy)
            {
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Registration.spGET_TEACHER_SECTION_LIST", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SY", sy);
                        using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }

            public static DataTable GetAssignedSubjects(string where = null, string order = null, string select = null)
            {
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Evltn.spGET_MFE005", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@where", where);
                        cmd.Parameters.AddWithValue("@order", order);
                        cmd.Parameters.AddWithValue("@select", select);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }

            public static DataTable GetBySY_GRADE_SECTION(string sy, string gradeLevel, string section)
            {
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand(
                                                @"SELECT Teacher.*, Employee.FullName 
                                                  FROM Registration.AdviserSection_MF as Teacher
                                                  LEFT JOIN  xSystem.Employee_MF as Employee ON Teacher.tchr_id = Employee.USERID

                                                  WHERE Teacher.sy=@sy 
                                                  AND Teacher.lvl_code=@lvl_code 
                                                  AND Teacher.section=@section
                                                  AND Teacher.flag_del = 0", 
                                            cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@sy", sy);
                        cmd.Parameters.AddWithValue("@lvl_code", gradeLevel);
                        cmd.Parameters.AddWithValue("@section", section);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                return dt;
            }

            public static bool CheckAssignedSubjects(TeacherSubjects subjs, string sy)
            {
                bool r = false;
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Evltn.spCHECK_TCHR_SUBJ", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tchr_id", subjs.TchrId);
                        cmd.Parameters.AddWithValue("@subj_id", subjs.SubjId);
                        cmd.Parameters.AddWithValue("@sy", sy);
                        r = (bool)cmd.ExecuteScalar();
                    }
                }

                return r;
            }

            public static void InsertAssignedSubjects(TeacherSubjects subjs, string sy, string userid = null)
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Evltn.spINSERT_TCHR_SUBJ", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tchr_id", subjs.TchrId);
                        cmd.Parameters.AddWithValue("@subj_id", subjs.SubjId);
                        cmd.Parameters.AddWithValue("@user_id", userid);
                        cmd.Parameters.AddWithValue("@sy", sy);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public static void DeleteAssignedSubject(int tchrsubj_id, string userid = null)
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Evltn.spDELETE_TCHR_SUBJ", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tchrsubj_id", tchrsubj_id);
                        cmd.Parameters.AddWithValue("@userid", userid);
                        cmd.ExecuteNonQuery();
                    }
                }
            }


        }
    }

    public class Users : cBase
    {
        #region "PROPERTIES"
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public int EmpId { get; set; }
        #endregion

        public static DataTable GetData()
        {
            DataTable dt = new DataTable();
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("xSystem.spGET_ALL_USER", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}
