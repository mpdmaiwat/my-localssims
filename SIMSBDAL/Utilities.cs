using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;

//To Get Web Tools
using System.Web.UI;
//using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace SIMSBDAL
{

    public class Utilities : cBase
    {
        //  string CS = ConfigurationManager.ConnectionStrings["CSSIMS"].ToString();

        /*Student Status*/
        public static readonly string StudentEnrolled = "ENROLLED";
        public static readonly string StudentDropped = "DROPPED";
        public static readonly string StudentUnofficallyDropped = "UNOFFICIALLY DROPPED";
        public static readonly string StudentWithdrawn = "WITHDRAWN";
        /**/

        public static object RValue(string StoredProcedure, string PrmName1, object Param1)
        {
            using (SqlConnection conn = new SqlConnection(CS))
            {
                if (Param1 != null)
                {
                    SqlCommand cmd = new SqlCommand(StoredProcedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter(PrmName1, Param1.ToString()));
                    cmd.Connection.Open();
                    object obj = new object();
                    obj = cmd.ExecuteScalar();


                    if ((obj == null) || obj == DBNull.Value)
                    {
                        return 0;
                    }
                    else
                    {
                        return obj;
                    }
                }
                else
                {

                    return 0;
                }

            }
        }

        private DataSet queryCommand(string sqlQuery)

        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }

            return ds;
        }

        public DataSet getTest() {

            DataSet ds = new DataSet();
            string strC = "Select * from Utilities.CityProvince_RF Order by Arr";
            ds = queryCommand(strC);

            return ds;
        }

        public DataTable GetCivilStatus()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Utilities.CivilStatus_RF ORDER BY SeqNum ASC", cn))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


        //Getting Applicant Type list from database
        public DataTable getApplicantType()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select AppTypeCode, AppTypeDesc from xSystem.ApplicantType_RF order by ID";
            dt = queryCommandDT(strSQL);

            return dt;
        }


        //OverWrite Parameter with drop down.
        public void getApplicantType(DropDownList dd)
        {
            DataTable dt = getApplicantType();

            dd.DataSource = dt;
            dd.DataTextField = dt.Columns["AppTypeDesc"].ToString();
            dd.DataValueField = dt.Columns["AppTypeCode"].ToString();
            dd.DataBind();
        }


        //Getting Applicant Level Applying
        public DataTable getApplicantLevel()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select LevelTypeCode, LevelTypeDesc from xSystem.LevelType_RF order by Arr";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        //Overide with Drop Down Parameter Applicant Level
        public void getApplicantLevel(DropDownList dd)
        {
            DataTable dt = getApplicantLevel();

            dd.DataSource = dt;
            dd.DataTextField = dt.Columns["LevelTypeDesc"].ToString();
            dd.DataValueField = dt.Columns["LevelTypeCode"].ToString();
            dd.DataBind();


        }

        /*This will use exclusive only for Dropdown control on entire project
         * 09-05-2016
         * Russel Vasquez
         */
        public void GENERIC_DROPDOWN(DropDownList dd, DataTable dt, string colValue, string colText)
        {
            DataTable datatable = dt;

            dd.DataSource = datatable;
            dd.DataTextField = dt.Columns[colValue].ToString();
            dd.DataTextField = dt.Columns[colText].ToString();
            dd.DataBind();
        }

        //Getting Applicant Strand for Grade 11 to 12
        public DataTable GET_LEVEL_STRAND()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGET_STRAND_LIST";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;
        }

        //Getting Gender 
        public DataTable GET_GENDER()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGET_GENDER_LIST";
            dt = queryCommandDT_StoredProc(strSQL);

            return dt;
        }


        //Get Area/Baranggay
        public DataTable getBarangay(string INPUT)
        {
            DataTable dt = new DataTable();
            string strSQL = "Select BarangayCode,BarangayDesc from Utilities.Barangay_RF WHERE CityCode ='" + INPUT + "' order by BarangayDesc";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        public DataTable GET_BARANGAY()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGET_BARANGAY_LIST";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;
        }


        //Get City/Province
        public DataTable GET_CITY()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGET_CITY_LIST";
            dt = queryCommandDT_StoredProc(strSQL);

            return dt;
        }


        //Get Citizenship
        public DataTable GET_CITIZENSHIP()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGET_CITIZENSHIP_LIST";
            dt = queryCommandDT_StoredProc(strSQL);

            return dt;
        }

        public DataTable GET_RELATION()
        {
            DataTable dt = new DataTable();
            string strSQL = "Utilities.spGET_RELATIONSHIP";
            dt = queryCommandDT_StoredProc(strSQL);

            return dt;
        }

        //Get Religion
        public DataTable GET_RELIGION()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGET_RELIGION_LIST";
            dt = queryCommandDT_StoredProc(strSQL);

            return dt;
        }

        //Get Education Background Type
        public DataTable GET_EDUBACKGROUND()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGetEduTypeList";
            dt = queryCommandDT_StoredProc(strSQL);

            return dt;
        }


        //Get MODE OF TRANSPORTATION
        public DataTable GET_MOT()
        {
            DataTable dt = new DataTable();
            string strSQL = "spGET_MOT_LIST";
            dt = queryCommandDT_StoredProc(strSQL);

            return dt;
        }

        //Grade Level
        public DataTable GET_GRADELEVEL_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("xSystem.spGET_GRADE_LEVEL_LIST");

            return dt;
        }

        public DataTable GET_GRADELEVEL_LIST(string where)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSystem.spGET_GRADE_LEVEL_LIST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataSet GetGradeLevels()
        {
            DataSet ds = new DataSet();

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGET_GRADE_LEVEL_LIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

            return ds;
        }

        public DataSet GetSections()
        {
            DataSet ds = new DataSet();

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_LEVEL]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GS");
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

            return ds;
        }

        public DataTable GetSections(string lvl_code)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_SECTIONS_LEVELBASED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvl_code", lvl_code);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataSet GET_LEVELLISTPERUSER(string _userid)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGetUserAttendAccess]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserCode", _userid);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

            return ds;
        }
        public DataSet GET_SECTIONLISTPERUSER(string _userid, string _levelcode)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGetUserSectionAccess]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserCode", _userid);
                    cmd.Parameters.AddWithValue("@levelcode", _levelcode);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

            return ds;
        }


        //Section List
        public DataTable GET_SECTION_LIST()
        {
            DataTable dt = new DataTable();
            //dt = queryCommandDT_StoredProc("spGetSectionList");
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spTRANS_LEVEL]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();
                    cmd.Parameters.AddWithValue("@transtype", "GS");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }

            return dt;
        }

        //Room List
        public DataTable GET_ROOM_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[Utilities].[spGET_ROOM_LIST]");

            return dt;
        }

        public DataTable GET_BUILDING_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[Utilities].[spGET_BUILDING_LIST]");

            return dt;
        }

        public DataTable GET_ROOM_TYPE()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("[Utilities].[spGET_ROOM_TYPE]");

            return dt;
        }

        public DataTable GET_ROOMS(string swhere = null, string sorder = null, string sselect = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spGET_ROOM_RF]", cn))
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

        public void INSERT_ROOMS_RF(int bldgid, int floor, int roomtype, string roomno, string roomdesc, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_ROOMS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@bldgid", bldgid);
                    cmd.Parameters.AddWithValue("@floor", floor);
                    cmd.Parameters.AddWithValue("@roomtype", roomtype);
                    cmd.Parameters.AddWithValue("@roomno", roomno);
                    cmd.Parameters.AddWithValue("@roomdesc", roomdesc);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_ROOMS(int roomid, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_ROOMS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "D");
                    cmd.Parameters.AddWithValue("@roomid", roomid);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_ROOMS(int roomid, int bldgid, int floor, int roomtype, string roomno, string roomdesc, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_ROOMS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "E");
                    cmd.Parameters.AddWithValue("@roomid", roomid);
                    cmd.Parameters.AddWithValue("@bldgid", bldgid);
                    cmd.Parameters.AddWithValue("@floor", floor);
                    cmd.Parameters.AddWithValue("@roomtype", roomtype);
                    cmd.Parameters.AddWithValue("@roomno", roomno);
                    cmd.Parameters.AddWithValue("@roomdesc", roomdesc);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_VIOLATIONS(string sselect, string swhere, string sorder)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_VIOLATIONS_RF]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sqltype", "GET");
                    cmd.Parameters.AddWithValue("@sselect", sselect);
                    cmd.Parameters.AddWithValue("@swhere", swhere);
                    cmd.Parameters.AddWithValue("@sorder", sorder);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public object GET_GLOBAL_PARAMETER_VALUE(string gpCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT gp_Value FROM Utilities.Global_Parameters WHERE gp_Code = @gpCode", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@gpCode", gpCode);
                    return cmd.ExecuteScalar();
                }
            }
        }

        public DataTable GET_GLOBAL_PARAMETERS(bool pstat)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_GLOBAL_PARAMETER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "G");
                    cmd.Parameters.AddWithValue("@pstat", pstat);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }

        public void CHANGE_STATUS_PARAMETER(bool pstat, int gpid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_GLOBAL_PARAMETER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "S");
                    cmd.Parameters.AddWithValue("@pstat", pstat);
                    cmd.Parameters.AddWithValue("@gpid", gpid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void INSERT_GLOBAL_PARAMETER(string gpcode,string gpdesc, string gpvalue,string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_GLOBAL_PARAMETER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "I");
                    cmd.Parameters.AddWithValue("@gpcode", gpcode);
                    cmd.Parameters.AddWithValue("@gpdesc", gpdesc);
                    cmd.Parameters.AddWithValue("@gpvalue", gpvalue);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_GLOBAL_PARAMETER(string gpdesc, string gpvalue, string userid, int gpid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Utilities].[spTRANS_GLOBAL_PARAMETER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "U");
                    cmd.Parameters.AddWithValue("@gpdesc", gpdesc);
                    cmd.Parameters.AddWithValue("@gpvalue", gpvalue);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@gpid", gpid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_GLOBAL_PARAMETER_VALUE(string gpCode, string gpValue, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Utilities.Global_Parameters SET gp_Value = @gpValue, username = @userId, dt_upd = GETDATE() WHERE gp_Code = @gpCode", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@gpValue", gpValue);
                    cmd.Parameters.AddWithValue("@gpCode", gpCode);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_AWARDS_LIST(string swhere, string sselect=null)
        {
            //string select = "a.*";
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select " + sselect + " from Utilities.Awards_RF a inner join xSystem.LevelType_RF b on b.LevelTypeCode=a.Lvlcode " + swhere, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cn.Close();
                }
            }
            return dt;
        }



        public DataTable GET_SCREENING_STATUS()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("spScreeningStat");
            return dt;
       }
        
        public DataTable GET_APPLICANT_HEALTH_STATUS()
        {

            DataTable dt = new DataTable();
            string strSQL = "Select HealthStatusCode,HealthStatusRemarks from Utilities.Health_Applicant_Status_RF order by Arr";
            dt = queryCommandDT(strSQL);

            return dt; 
        }

        //Guidance Scheduling-Assignment and Setup
        public DataTable GET_SCREENING_TYPE()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select ScreeningCode,ScreeningDesc from Utilities.ScreeningType_RF order by Arr";
            dt = queryCommandDT(strSQL);

            return dt;
        } 

        //FOR TESTING STATUS
        public DataTable getGeneralStatus(string _acode)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayGeneralStatus", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ACODE", _acode);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
           
        }
        
        public string RET_LEVELTYPEDESC(string _leveltypecode)
        {
         string ret_string = "";

         using (SqlConnection cn = new SqlConnection(CS))
         {
             using (SqlCommand cmd = new SqlCommand("Select LevelTypeDesc from xSystem.LevelType_RF where LevelTypeCode='" + _leveltypecode + "'", cn))
             {
                 cn.Open();
                 SqlDataReader dr = cmd.ExecuteReader();

                 if (dr.HasRows)
                 {
                     while (dr.Read())
                     {
                         ret_string = dr["LevelTypeDesc"].ToString();
                     }
                 }

                 
             }
         }
         return ret_string;

        }

        /*INSERT AREA*/

        public void INSERT_ROOM(string _roomdesc, int _buildingID, string _remarks, bool _status, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Utilities].[spInsert_ROOM]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROOMDESC", _roomdesc);
                    cmd.Parameters.AddWithValue("@BUILDINGID", _buildingID);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@STATUS", _status);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();
                }
            
            }

              
        }
    

        /*
         UPDATE AREA
         */

        public void UPDATE_ROOM(int _roomID, string _roomdesc, int _buildingID, string _remarks, bool _status, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Utilities].[spUPDATE_ROOM]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROOMDESC", _roomdesc);
                    cmd.Parameters.AddWithValue("@BUILDINGID", _buildingID);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@STATUS", _status);
                    cmd.Parameters.AddWithValue("@USERID", _userid);
                    cmd.Parameters.AddWithValue("@ROOMID", _roomID);

                    cmd.ExecuteNonQuery();
                }

            }


        }

        public DataTable GET_SUFFIXES()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Utilities.spGET_SUFFIXES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }




        public class AssessmentItemType
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Prefix { get; set; }
            public int DocStart { get; set; }
            public int DocEnd { get; set; }
            public int NextDoc { get; set; }
            public bool FlagDel { get; set; }
            public DateTime DateIns { get; set; }
            public DateTime DateUpd { get; set; }
        }

        public List<AssessmentItemType> GET_ASMT_ITEM_TYPE(string where = null, string order = null)
        {
            List<AssessmentItemType> list = new List<AssessmentItemType>();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_ASMT_ITEM_TYPES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_where", where);
                    cmd.Parameters.AddWithValue("@p_order", order);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                list.Add(new AssessmentItemType
                                {
                                    Id = Convert.ToInt32(dr["Id"].ToString()),
                                    Code = dr["ItmTypCode"].ToString(),
                                    Name = dr["ItmTypName"].ToString(),
                                    Description = dr["ItmTypDesc"].ToString(),
                                    Prefix = dr["ItmTypPref"].ToString(),
                                    DocStart = Convert.ToInt32(dr["DocStart"].ToString()),
                                    DocEnd = Convert.ToInt32(dr["DocEnd"].ToString()),
                                    NextDoc = Convert.ToInt32(dr["NextDoc"].ToString()),
                                    FlagDel = Convert.ToBoolean(dr["FlagDel"].ToString()),
                                    DateIns = Convert.ToDateTime(dr["sys_dt_ins"].ToString()),
                                    DateUpd = (string.IsNullOrEmpty(dr["sys_dt_up"].ToString()) ? Convert.ToDateTime(dr["sys_dt_ins"].ToString()) : Convert.ToDateTime(dr["sys_dt_up"].ToString()))
                                });
                            }
                        }
                    }
                }
            }

            return list;
        }

        public void UPDATE_ASMT_ITEM_TYPE(AssessmentItemType itmtype)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spUPDATE_ASMT_ITEM_TYPE", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id", itmtype.Id);
                    cmd.Parameters.AddWithValue("@p_name", itmtype.Name);
                    cmd.Parameters.AddWithValue("@p_desc", itmtype.Description);
                    cmd.Parameters.AddWithValue("@p_pref", itmtype.Prefix);
                    cmd.Parameters.AddWithValue("@p_docstart", itmtype.DocStart);
                    cmd.Parameters.AddWithValue("@p_docend", itmtype.DocEnd);
                    cmd.Parameters.AddWithValue("@p_nextdoc", itmtype.NextDoc);
                    cmd.Parameters.AddWithValue("@p_flagdel", itmtype.FlagDel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public class AssessmentFees
        {
            public string desigfee_code { get; set; }
            public string desigfee_type { get; set; }
            public string desigfee_desc { get; set; }
            public string flag_dele { get; set; }
            public DateTime sys_dt_ins { get; set; }
            public DateTime sys_dt_upd { get; set; }
            public string isams_desigfee_code { get; set; }
            public string userid { get; set; }
        }

        public DataTable GET_ASMT_FEES(string limit = null, string where = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_ASMT_FEES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_limit", limit);
                    cmd.Parameters.AddWithValue("@p_where", where);
                    cmd.Parameters.AddWithValue("@p_order", order);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public List<AssessmentFees> GET_ASMT_FEES_DATA(string limit = null, string where = null, string order = null)
        {
            List<AssessmentFees> list = new List<AssessmentFees>();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_ASMT_FEES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_limit", limit);
                    cmd.Parameters.AddWithValue("@p_where", where);
                    cmd.Parameters.AddWithValue("@p_order", order);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                list.Add(new AssessmentFees
                                {
                                    desigfee_code = dr["desigfee_code"].ToString(),
                                    desigfee_type = dr["desigfee_type"].ToString(),
                                    desigfee_desc = dr["desigfee_desc"].ToString(),
                                    flag_dele = dr["flag_dele"].ToString(),
                                    sys_dt_ins = Convert.ToDateTime(dr["date_time_sys"].ToString()),
                                    sys_dt_upd = string.IsNullOrEmpty(dr["date_last_upd"].ToString()) ? Convert.ToDateTime(dr["date_time_sys"].ToString()) : Convert.ToDateTime(dr["date_last_upd"].ToString()),
                                    isams_desigfee_code = dr["isamsfeecode"].ToString()
                                });
                            }
                        }
                    }
                }
            }

            return list;
        }

        public bool CHECK_ASMT_FEE(AssessmentFees fees, string savetype = "N")
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spCHECK_ASMT_FEE", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_code", fees.desigfee_code);
                    cmd.Parameters.AddWithValue("@p_type", fees.desigfee_type);
                    cmd.Parameters.AddWithValue("@p_desc", fees.desigfee_desc);
                    cmd.Parameters.AddWithValue("@p_savetype", savetype);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public void INSERT_ASMT_FEE(AssessmentFees fees)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spINSERT_ASMT_FEE", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_code", fees.desigfee_code);
                    cmd.Parameters.AddWithValue("@p_type", fees.desigfee_type);
                    cmd.Parameters.AddWithValue("@p_desc", fees.desigfee_desc);
                    cmd.Parameters.AddWithValue("@p_userid", fees.userid);
                    cmd.Parameters.AddWithValue("@p_isamscode", fees.isams_desigfee_code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_ASMT_FEE(AssessmentFees fees)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spUPDATE_ASMT_FEE", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_code", fees.desigfee_code);
                    cmd.Parameters.AddWithValue("@p_type", fees.desigfee_type);
                    cmd.Parameters.AddWithValue("@p_desc", fees.desigfee_desc);
                    cmd.Parameters.AddWithValue("@p_flagdele", fees.flag_dele);
                    cmd.Parameters.AddWithValue("@p_userid", fees.userid);
                    cmd.Parameters.AddWithValue("@p_isamscode", fees.isams_desigfee_code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public class ModeOfPayment
        {
            public int id { get; set; }
            public string code { get; set; }
            public string desc { get; set; }
            public int arr { get; set; }
            public bool status { get; set; }
            public bool flag_del { get; set; }
            public DateTime date_ins { get; set; }
            public DateTime date_upd { get; set; }
            public string userid { get; set; }
        }

        public DataTable GET_MOP(string where = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_MOP", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_where", where);
                    cmd.Parameters.AddWithValue("@p_order", order);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public List<ModeOfPayment> GET_MOP_DATA(string where = null, string order = null)
        {
            List<ModeOfPayment> list = new List<ModeOfPayment>();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_MOP", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_where", where);
                    cmd.Parameters.AddWithValue("@p_order", order);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                list.Add(new ModeOfPayment
                                {
                                    id = Convert.ToInt32(dr["Id"].ToString()),
                                    code = dr["MOPCode"].ToString(),
                                    desc = dr["MOPDesc"].ToString(),
                                    arr = Convert.ToInt32(dr["Arr"].ToString()),
                                    status = Convert.ToBoolean(dr["Status"].ToString()),
                                    date_ins = Convert.ToDateTime(dr["sys_dt_ins"].ToString()),
                                    date_upd = string.IsNullOrEmpty(dr["sys_dt_upd"].ToString()) ? Convert.ToDateTime(dr["sys_dt_ins"].ToString()) : Convert.ToDateTime(dr["sys_dt_upd"].ToString()),
                                    userid = dr["UserId"].ToString()
                                });
                            }
                        }
                    }
                }
            }

            return list;
        }

        public void INSERT_MOP(ModeOfPayment payment)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spINSERT_MOP", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_code", payment.code);
                    cmd.Parameters.AddWithValue("@p_desc", payment.desc);
                    cmd.Parameters.AddWithValue("@p_arr", payment.arr);
                    cmd.Parameters.AddWithValue("@p_userid", payment.userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPDATE_MOP(ModeOfPayment payment)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spUPDATE_MOP", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id", payment.id);
                    cmd.Parameters.AddWithValue("@p_desc", payment.desc);
                    cmd.Parameters.AddWithValue("@p_arr", payment.arr);
                    cmd.Parameters.AddWithValue("@p_status", payment.status);
                    cmd.Parameters.AddWithValue("@p_flagdel", payment.flag_del);
                    cmd.Parameters.AddWithValue("@p_userid", payment.userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool CHECK_MOP(ModeOfPayment payment)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spCHECK_MOP", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id", payment.id);
                    cmd.Parameters.AddWithValue("@p_code", payment.code);
                    cmd.Parameters.AddWithValue("@p_desc", payment.desc);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public int GET_MOP_LAST_ORDER()
        {
            int i = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_MOP", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_where", null);
                    cmd.Parameters.AddWithValue("@p_order", "ORDER BY Arr DESC");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            i = Convert.ToInt32(dr["Arr"].ToString());
                        }
                    }
                }
            }

            return i;
        }

        public double GET_MOP_INSTL_FEE(string code)
        {
            double d = 0;
            string filter = "WHERE MOPCode = '" + code + "'";
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Assessment.spGET_MOP", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_where", filter);
                    cmd.Parameters.AddWithValue("@p_order", null);
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            d = Convert.ToDouble(dr["InstlFee"].ToString());
                        }
                    }
                }
            }

            return d;
        }





        public DataTable GET_ITEM_TYPE()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string qry = "SELECT * FROM xSystem.ItemType_RF WHERE [FlagDel] = 0 ORDER BY [ItmTypName] ASC";
                using (SqlCommand cmd = new SqlCommand(qry, cn))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GET_ITEM_CATEGORY(string itemtypecode)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string qry = "SELECT * FROM xSystem.ItemCategory_RF WHERE [FlagDel] = 0 AND [ItmTypCode] = @p_itmtypcode ORDER BY ItmCatCode ASC";
                using (SqlCommand cmd = new SqlCommand(qry, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@p_itmtypcode", itemtypecode);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GET_ITEM_NAME()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string qry = "SELECT [ItmName] FROM Assessment.Items_RF GROUP BY ItmName ORDER BY ItmName ASC";
                using (SqlCommand cmd = new SqlCommand(qry, cn))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GET_LEVEL_CATEGORY()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM xSystem.LevelCategory_RF WHERE xFlag = 1 ORDER BY Arr", cn))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public string GET_RELIGION(string religion_code)
        {
            string r = null;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ReligionDesc FROM Utilities.Religion_RF WHERE ReligionCode = @religion_code", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@religion_code", religion_code);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            r = dr["ReligionDesc"].ToString();
                        }
                    }
                }
            }

            return r;
        }

        public DataTable GET_PYMT_METHODS()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM xSYstem.Pymt_Methods_RF", cn))
                {
                    cn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

    }



    public class Quotes : cBase
    { 
    
    }


   

    
}
