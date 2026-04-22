using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace SIMSBDAL
{
    public class Student : cBase
    {

        #region "PROPERTY-FIELDS"


        public string STUDNO { get; set; }
        public string APPNUM { get; set; }
        public string LASTNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string FULLNAME { get; set; }
        public string FULLNAME2 { get; set; }
        public string MI { get; set; }
        public string SUFFIX { get; set; }
        public DateTime DOB { get; set; }
        public string POB { get; set; }
        public string GENDERCODE { get; set; }
        public string TELNO { get; set; }
        public string MOBILENO { get; set; }
        public string EMAIL { get; set; }
        public string STREET { get; set; }
        public string CITYCODE { get; set; }
        public string BARANGAYCODE { get; set; }
        public string INITIALCONTACT { get; set; }
        public bool STATUS { get; set; }
        public bool SSICHILD { get; set; }
        public string ENTRY_LEVELCODE { get; set; }
        public DateTime? ENTRY_DATE { get; set; }
        public string CURRENT_LEVELCODE { get; set; }
        public string CURRENT_LEVELDESC { get; set; }
        public string RESERVE_LEVELCODE { get; set; }
        public string RESERVE_LEVELDESC { get; set; }




        public string SECTION { get; set; }
        public string CITIZENSHIPCODE { get; set; }
        public string CITIZENSHIPDESC { get; set; }
        public string RELIGIONCODE { get; set; }
        public string RELIGIONDESC { get; set; }

        //For Display Info purposes of applicant stage entry
        public string DATEAPPLIED { get; set; }
        public string ENTRYSY { get; set; }

        public string LRN { get; set; }
        public string STRANDCODE { get; set; }
        public string STRANDDESC { get; set; }
        public string PHOTOLOC { get; set; }

        public string STUDTYPE { get; set; }

        public string MOTCODE { get; set; }
        public string MOTDESC { get; set; }
        public string MOTCODEIN { get; set; }

        //PRIMARY CONTACT PERSON - INFO
        public string COMPLETE_ADDRESS { get; set; }
        public string PRIMARY_CONTACT_PERSON { get; set; }
        public string CONTACT_RELATION { get; set; }
        public string CONTACT_NUMBERS { get; set; }

        public string BARCODE { get; set; }


        //FROM STUDENT_MF
        public bool TOENROLL { get; set; }
        public bool MOVINGUP { get; set; } // APPLICABLE ONLY FOR NURSERY 2
        public string STATCODEE { get; set; } // STATUS OF ENROLLED STUDENTS
        public string STATCODER { get; set; } // STATUS FOR RESERVATION FOR NXT SY
        public DateTime DATEPAYR { get; set; } // DATE OF PAYMENT FOR RESERVATION / RESERVED
        public string STATCODE { get; set; }  // STATUS FOR ENROLLMENT FOR NXT SY
        public DateTime? StatCodeForLeavingEffectivityDate { get; private set; }
        public DateTime? StatCodeSystemDate { get; private set; }
        public DateTime DATEPAYE { get; set; } // DATE OF PAYMENT FOR ENROLLMENT / ENROLLED
        public bool BO_R { get; set; } // STATUS FOR BACKOUT (RESERVATION)
        public bool BO_E { get; set; } // STATUS FOR BACKOUT (ENROLLMENT)
        public string STATCODEBO_E { get; set; }
        public bool SPC_ARR { get; set; }
        public bool BL { get; set; }

        /* 
         FIELDS FOR FAMILY
         
         */
        public string FLASTNAME { get; set; }
        public string FFIRSTNAME { get; set; }
        public string FMIDDLENAME { get; set; }
        public string FOCCUPATION { get; set; }
        public string FCITIZENSHIP { get; set; }
        public string FEDUCATION { get; set; }
        public string FCOMPANY { get; set; }
        public string FCOMPADDRESS { get; set; }
        public string FTELEPHONE { get; set; }
        public string FMOBILE { get; set; }
        public string FEMAIL { get; set; }

        public string MLASTNAME { get; set; }
        public string MFIRSTNAME { get; set; }
        public string MMIDDLENAME { get; set; }
        public string MAIDENLASTNAME { get; set; }
        public string MAIDENFIRSTNAME { get; set; }
        public string MAIDENMIDDLENAME { get; set; }
        public string MOCCUPATION { get; set; }
        public string MCITIZENSHIP { get; set; }
        public string MEDUCATION { get; set; }
        public string MCOMPANY { get; set; }
        public string MCOMPADDRESS { get; set; }
        public string MTELEPHONE { get; set; }
        public string MMOBILE { get; set; }
        public string MEMAIL { get; set; }

        public string GLASTNAME { get; set; }
        public string GFIRSTNAME { get; set; }
        public string GMIDDLENAME { get; set; }
        public string GADDRESS { get; set; }
        public string GTELEPHONE { get; set; }
        public string GMOBILE { get; set; }
        public string GRELATION { get; set; }
        public string GEMAIL { get; set; }
        public int PRIMARYCONTACT { get; set; }

        //additional fields in relatives
        public string FCIVILSTAT { get; set; }
        public string MCIVILSTAT { get; set; }
        public string FHOMEADDR { get; set; }
        public string MHOMEADDR { get; set; }
        public string FHOMETEL { get; set; }
        public string MHOMETEL { get; set; }
        public string GCITIZENSHIP { get; set; }
        public string GEDUCATION { get; set; }
        public string GOCCUPATION { get; set; }
        public string GCOMPANY { get; set; }
        public string GCOMPANYTEL { get; set; }
        public string GCOMPANYADDRESS { get; set; }
        //------------------------------


        //CREDENTIALS
        public bool FORM138 { get; set; }
        public bool BC { get; set; }
        public bool BC_PSA { get; set; }
        public bool BC_NSO { get; set; }
        public bool BC_CR { get; set; }
        public bool BC_NSO_ORIG { get; set; }
        public bool BC_NSO_PCOPY { get; set; }
        public bool BC_CR_ORIG { get; set; }
        public bool BC_CR_PCOPY { get; set; }
        public bool PASSPORT { get; set; }
        public bool VISA { get; set; }
        public bool COLORED1X1 { get; set; }
        public bool BROWNENVELOPE { get; set; }
        public bool GM { get; set; }
        public bool RECOMMENDATION { get; set; }
        public bool FORM137 { get; set; }
        public bool NCAE { get; set; }
        public bool INTERVIEW { get; set; }
        public string OTHER { get; set; }
        public bool MEDCERT { get; set; }

        /*Fields for Sibling */
        public string SIBLINGCODE { get; set; }

        #endregion

        public DataTable GetStudentBasicInformation(string studNum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_STUDENT_INFORMATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataTable GetStudentFamilyInformation(string studNum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_STUDENT_FAMILY_INFORMATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetStudentAddressInformation(string studNum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_STUDENT_ADDRESS_INFORMATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetStudentIDInformation(string studNum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_STUDENT_ID_INFORMATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        #region "UPDATE STUDENT ENROLLMENT STATUS FOR CURRENT SY"
        public bool UPDATE_STUDENT_ENRSTAT_CURRENTSY(string _studnum, string _statcode, DateTime? _statCodeForLeavingEffectivityDate)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_STUDENT_ENRSTAT_CURRENTSY", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_studnum", _studnum);
                        cmd.Parameters.AddWithValue("@p_statcode", _statcode);
                        cmd.Parameters.AddWithValue("@StatCodeForLeavingEffectivityDate", _statCodeForLeavingEffectivityDate);
                        switch (_statcode)
                        {
                            case "DP": // DROPPED
                                cmd.Parameters.AddWithValue("@p_enrollstat", '3');
                                break;
                            case "WD": // WITHDRAWN
                                cmd.Parameters.AddWithValue("@p_enrollstat", '4');
                                break;
                            case "UD": // UNOFFICIALLY DROPPED
                                cmd.Parameters.AddWithValue("@p_enrollstat", '5');
                                break;
                            case "EN": // ENROLLED
                                cmd.Parameters.AddWithValue("@p_enrollstat", '2');
                                break;
                            default:
                                cmd.Parameters.AddWithValue("@p_enrollstat", null);
                                break;
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.GetType().ToString() + "; " + exc.Message);
                LogException(exc);
                return false;
            }
        }
        #endregion


        public DataTable GET_STUD_ID_FOR_UPDATES(string searchKey = "", int pageIndex = 0)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[STUDENT].[spGET_STUD_ID_FOR_UPDATES]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 360;
                    cmd.Parameters.AddWithValue("@searchKey", searchKey);
                    cmd.Parameters.AddWithValue("@pageIndex", pageIndex);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public int GET_TOTAL_STUD_ID_UPDATES()
        {
            int r = 0;
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[STUDENT].[spCOUNT_TOTAL_STUD_ID_UPDATES]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 360;
                    r = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }
            }

            return r;
        }

        public DataTable GET_STUD_ID_INFO(string studNum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spGET_STUD_ID_INFO]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public void UPDATE_STUD_ID_INFO(string studNum)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[STUDENT].[spUPDATE_STUD_ID_INFO]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    cmd.CommandTimeout = 360;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void REVERT_STUD_ID_INFO(string studNum)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[STUDENT].[spREVERT_STUD_ID_INFO]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 360;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool CHECK_STUD_ID_HAS_CHANGES(string studNum)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[STUDENT].[spCHECK_STUD_FOR_UPDATE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 360;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        #region "INSERT-UPDATE-DELETE FUNCTIONS"


        //INSERT RECORD OF APPLICANT TO REGISTRATION OF STUDENTS
        public void INSERT_APP_TO_STUDENT(string _appnum, string _studnum, string _levelcode, string _levelcatcode, string _usercode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spINSERT_APPLICANT_TO_STUDENT]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@LEVELCODE", _levelcode);
                    cmd.Parameters.AddWithValue("@LEVELCATCODE", _levelcatcode);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    cmd.ExecuteNonQuery();



                }
            }


        }

        public void UPDATE_IS_APPROVED_BY_REGISTRAR(string studNum, bool isApprovedByRegistrar = false)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE [Registration].[Student_MF] SET IsApprovedByRegistrar = @isApprovedByRegistrar WHERE studNum = @studNum", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@isApprovedByRegistrar", isApprovedByRegistrar);
                    cmd.Parameters.AddWithValue("@studNum", studNum);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Get list of students by section
        /// </summary>
        /// <param name="levelCode"></param>
        /// <param name="section"></param>
        /// <returns>DataTable</returns>
        public DataTable GET_STUDENTS(string levelCode, string section)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_STUDENTS_BY_SECTION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@levelCode", levelCode);
                    cmd.Parameters.AddWithValue("@section", section);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        //INSERT spInsertStudentEducBack
        public void INSERT_EDUCBACK(string _studnum, string _edutype, string _schoolname, string _address, string _yeargrad, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spINSERT_STUDENT_EDUCATION_BACKGROUND]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@EDUTYPE", _edutype);
                    cmd.Parameters.AddWithValue("@SCHOOLNAME", _schoolname);
                    cmd.Parameters.AddWithValue("@ADDRESS", _address);
                    cmd.Parameters.AddWithValue("@YEARGRAD", _yeargrad);
                    cmd.Parameters.AddWithValue("@USERID", _userid);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void DELETE_EDUBACK(int id)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spDELETE_STUDENT_EDUBACK", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //INSERT RELATIVE
        public void INSERT_STUDENT_RELATIVE(string _studnum, string _flastname, string _ffirstname, string _fmiddlename,
                                    string _foccupation, string _fcitizenship, string _feducation,
                                    string _fcompany, string _fcompaddress, string _ftelephone, string _fmobile,
                                    string _mlastname, string _mfirstname, string _mmiddlename,
                                    string _moccupation, string _mcitizenship, string _meducation,
                                    string _mcompany, string _mcompaddress, string _mtelephone, string _mmobile,
                                    string _glastname, string _gfirstname, string _gmiddlename,
                                    string _gaddress, string _gtelephone, string _gmobile, string _grelation, int _primarycontactid, string _memail, string _femail, string _gemail
                                    // additional fields
                                    , string _fcivilstat, string _mcivilstat, string _fhomeaddr, string _mhomeaddr, string _fhometel, string _mhometel,
                                    string _gcitizenship, string _geducation, string _goccupation, string _gcompany, string _gcompanytel, string _gcompaddress
                                    //------------------
                                    , string _maidenLname, string _maidenFname, string _maidenMname
            )

        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spINSERT_STUDENT_RELATIVE]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@FLASTNAME", _flastname);
                    cmd.Parameters.AddWithValue("@FFIRSTNAME", _ffirstname);
                    cmd.Parameters.AddWithValue("@FMIDDLENAME", _fmiddlename);
                    cmd.Parameters.AddWithValue("@FOCCUPATION", _foccupation);
                    cmd.Parameters.AddWithValue("@FCITIZENSHIP", _fcitizenship);
                    cmd.Parameters.AddWithValue("@FEDUCATION", _feducation);
                    cmd.Parameters.AddWithValue("@FCOMPANY", _fcompany);
                    cmd.Parameters.AddWithValue("@FCOMPADDRESS", _fcompaddress);
                    cmd.Parameters.AddWithValue("@FTELEPHONE", _ftelephone);
                    cmd.Parameters.AddWithValue("@FMOBILE", _fmobile);
                    cmd.Parameters.AddWithValue("@FEMAIL", _femail);

                    cmd.Parameters.AddWithValue("@MLASTNAME", _mlastname);
                    cmd.Parameters.AddWithValue("@MFIRSTNAME", _mfirstname);
                    cmd.Parameters.AddWithValue("@MMIDDLENAME", _mmiddlename);
                    cmd.Parameters.AddWithValue("@MOCCUPATION", _moccupation);
                    cmd.Parameters.AddWithValue("@MCITIZENSHIP", _mcitizenship);
                    cmd.Parameters.AddWithValue("@MEDUCATION", _meducation);
                    cmd.Parameters.AddWithValue("@MCOMPANY", _mcompany);
                    cmd.Parameters.AddWithValue("@MCOMPADDRESS", _mcompaddress);
                    cmd.Parameters.AddWithValue("@MTELEPHONE", _mtelephone);
                    cmd.Parameters.AddWithValue("@MMOBILE", _mmobile);
                    cmd.Parameters.AddWithValue("@MEMAIL", _memail);

                    cmd.Parameters.AddWithValue("@GLASTNAME", _glastname);
                    cmd.Parameters.AddWithValue("@GFIRSTNAME", _gfirstname);
                    cmd.Parameters.AddWithValue("@GMIDDLENAME", _gmiddlename);
                    cmd.Parameters.AddWithValue("@GADDRESS", _gaddress);
                    cmd.Parameters.AddWithValue("@GTELEPHONE", _gtelephone);
                    cmd.Parameters.AddWithValue("@GMOBILE", _gmobile);
                    cmd.Parameters.AddWithValue("@GRELATION", _grelation);
                    cmd.Parameters.AddWithValue("@GEMAIL", _gemail);

                    cmd.Parameters.AddWithValue("@PRIMARYCONTACTID", _primarycontactid);

                    // additional fields
                    cmd.Parameters.AddWithValue("@FCIVILSTAT", _fcivilstat);
                    cmd.Parameters.AddWithValue("@MCIVILSTAT", _mcivilstat);
                    cmd.Parameters.AddWithValue("@FHOMEADDR", _fhomeaddr);
                    cmd.Parameters.AddWithValue("@MHOMEADDR", _mhomeaddr);
                    cmd.Parameters.AddWithValue("@FHOMETEL", _fhometel);
                    cmd.Parameters.AddWithValue("@MHOMETEL", _mhometel);
                    cmd.Parameters.AddWithValue("@GCITIZENSHIP", _gcitizenship);
                    cmd.Parameters.AddWithValue("@GEDUCATION", _geducation);
                    cmd.Parameters.AddWithValue("@GOCCUPATION", _goccupation);
                    cmd.Parameters.AddWithValue("@GCOMPANY", _gcompany);
                    cmd.Parameters.AddWithValue("@GCOMPANYTEL", _gcompanytel);
                    cmd.Parameters.AddWithValue("@GCOMPADDRESS", _gcompaddress);
                    //------------------
                    cmd.Parameters.AddWithValue("@MAIDENLNAME", _maidenLname);
                    cmd.Parameters.AddWithValue("@MAIDENFNAME", _maidenFname);
                    cmd.Parameters.AddWithValue("@MAIDENMNAME", _maidenMname);

                    cmd.ExecuteNonQuery();

                }
            }
        }



        //INSERT CREDENTIALS
        public void INSERT_STUDENTCREDENTIALS(string _studnum, bool _form138, bool _bc, bool _colored1x1,
                                           bool _brownenvelope, bool _gm, bool _recommendation,
                                           bool _form137, bool _ncae, bool _interview, string _other)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spINSERT_STUDENT_CREDENTIALS]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@SNUM", _studnum);
                    cmd.Parameters.AddWithValue("@Form138", _form138);
                    cmd.Parameters.AddWithValue("@BC", _bc);
                    cmd.Parameters.AddWithValue("@Colored1x1", _colored1x1);
                    cmd.Parameters.AddWithValue("@BrownEnvelope", _brownenvelope);
                    cmd.Parameters.AddWithValue("@GM", _gm);
                    cmd.Parameters.AddWithValue("@Recommendation", _gm);
                    cmd.Parameters.AddWithValue("@Form137", _form138);
                    cmd.Parameters.AddWithValue("@NCAE", _ncae);
                    cmd.Parameters.AddWithValue("@Interview", _interview);
                    cmd.Parameters.AddWithValue("@Other", _other);


                    cmd.ExecuteNonQuery();



                }
            }
        }

        //INSERT SIBLINGS
        public void INSERT_SIBLINGS(string _studnum, string _siblingcode, int _siblingorder, string _userid)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spINSERT_STUDENT_SIBLING]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@SIBLINGCODE", _siblingcode);
                    cmd.Parameters.AddWithValue("@SIBLINGORDER", _siblingorder);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();



                }
            }
        }




        //UPDATE RELATIVE
        public void UPDATE_STUDENT_RELATIVE(string _studnum, string _flastname, string _ffirstname, string _fmiddlename,
                                   string _foccupation, string _fcitizenship, string _feducation,
                                   string _fcompany, string _fcompaddress, string _ftelephone, string _fmobile,
                                   string _mlastname, string _mfirstname, string _mmiddlename,
                                   string _moccupation, string _mcitizenship, string _meducation,
                                   string _mcompany, string _mcompaddress, string _mtelephone, string _mmobile,
                                   string _glastname, string _gfirstname, string _gmiddlename,
                                   string _gaddress, string _gtelephone, string _gmobile, string _grelation, int _primarycontactid, string _memail, string _femail, string _gemail
                                    // additional fields
                                    , string _fcivilstat, string _mcivilstat, string _fhomeaddr, string _mhomeaddr, string _fhometel, string _mhometel,
                                    string _gcitizenship, string _geducation, string _goccupation, string _gcompany, string _gcompanytel, string _gcompaddress
                                    //------------------
                                    , string _maidenLname, string _maidenFname, string _maidenMname
            )
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_RELATIVE]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@FLASTNAME", _flastname);
                    cmd.Parameters.AddWithValue("@FFIRSTNAME", _ffirstname);
                    cmd.Parameters.AddWithValue("@FMIDDLENAME", _fmiddlename);
                    cmd.Parameters.AddWithValue("@FOCCUPATION", _foccupation);
                    cmd.Parameters.AddWithValue("@FCITIZENSHIP", _fcitizenship);
                    cmd.Parameters.AddWithValue("@FEDUCATION", _feducation);
                    cmd.Parameters.AddWithValue("@FCOMPANY", _fcompany);
                    cmd.Parameters.AddWithValue("@FCOMPADDRESS", _fcompaddress);
                    cmd.Parameters.AddWithValue("@FTELEPHONE", _ftelephone);
                    cmd.Parameters.AddWithValue("@FMOBILE", _fmobile);
                    cmd.Parameters.AddWithValue("@FEMAIL", _femail);

                    cmd.Parameters.AddWithValue("@MLASTNAME", _mlastname);
                    cmd.Parameters.AddWithValue("@MFIRSTNAME", _mfirstname);
                    cmd.Parameters.AddWithValue("@MMIDDLENAME", _mmiddlename);
                    cmd.Parameters.AddWithValue("@MOCCUPATION", _moccupation);
                    cmd.Parameters.AddWithValue("@MCITIZENSHIP", _mcitizenship);
                    cmd.Parameters.AddWithValue("@MEDUCATION", _meducation);
                    cmd.Parameters.AddWithValue("@MCOMPANY", _mcompany);
                    cmd.Parameters.AddWithValue("@MCOMPADDRESS", _mcompaddress);
                    cmd.Parameters.AddWithValue("@MTELEPHONE", _mtelephone);
                    cmd.Parameters.AddWithValue("@MMOBILE", _mmobile);
                    cmd.Parameters.AddWithValue("@MEMAIL", _memail);

                    cmd.Parameters.AddWithValue("@GLASTNAME", _glastname);
                    cmd.Parameters.AddWithValue("@GFIRSTNAME", _gfirstname);
                    cmd.Parameters.AddWithValue("@GMIDDLENAME", _gmiddlename);
                    cmd.Parameters.AddWithValue("@GADDRESS", _gaddress);
                    cmd.Parameters.AddWithValue("@GTELEPHONE", _gtelephone);
                    cmd.Parameters.AddWithValue("@GMOBILE", _gmobile);
                    cmd.Parameters.AddWithValue("@GRELATION", _grelation);
                    cmd.Parameters.AddWithValue("@GEMAIL", _gemail);

                    cmd.Parameters.AddWithValue("@PRIMARYCONTACTID", _primarycontactid);

                    // additional fields
                    cmd.Parameters.AddWithValue("@FCIVILSTAT", _fcivilstat);
                    cmd.Parameters.AddWithValue("@MCIVILSTAT", _mcivilstat);
                    cmd.Parameters.AddWithValue("@FHOMEADDR", _fhomeaddr);
                    cmd.Parameters.AddWithValue("@MHOMEADDR", _mhomeaddr);
                    cmd.Parameters.AddWithValue("@FHOMETEL", _fhometel);
                    cmd.Parameters.AddWithValue("@MHOMETEL", _mhometel);
                    cmd.Parameters.AddWithValue("@GCITIZENSHIP", _gcitizenship);
                    cmd.Parameters.AddWithValue("@GEDUCATION", _geducation);
                    cmd.Parameters.AddWithValue("@GOCCUPATION", _goccupation);
                    cmd.Parameters.AddWithValue("@GCOMPANY", _gcompany);
                    cmd.Parameters.AddWithValue("@GCOMPANYTEL", _gcompanytel);
                    cmd.Parameters.AddWithValue("@GCOMPADDRESS", _gcompaddress);
                    //------------------
                    cmd.Parameters.AddWithValue("@MAIDENLNAME", _maidenLname);
                    cmd.Parameters.AddWithValue("@MAIDENFNAME", _maidenFname);
                    cmd.Parameters.AddWithValue("@MAIDENMNAME", _maidenMname);

                    cmd.ExecuteNonQuery();

                }
            }
        }




        //UPDATE EDUCATIONAL BACKGROUND
        public void UPDATE_EDUCBACK(int _id, string _edutype, string _schoolname, string _address, string _yeargrad, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_EDUCATION_BACKGROUND]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@EDUTYPE", _edutype);
                    cmd.Parameters.AddWithValue("@SCHOOLNAME", _schoolname);
                    cmd.Parameters.AddWithValue("@ADDRESS", _address);
                    cmd.Parameters.AddWithValue("@YEARGRAD", _yeargrad);
                    cmd.Parameters.AddWithValue("@USERID", _userid);
                    cmd.ExecuteNonQuery();



                }
            }
        }



        //UPDATE STUDENT VITAL INFORMATION
        public void UPDATE_STUDENTINFORMATION(string _studnum, string _lastname, string _firstname, string _middlename,
                                               string _mi, string _suffix, string _fullname, string _gendercode, DateTime _dob,
                                               string _pob, double _age, string _citizenshipcode, string _religioncode, string _telno, string _mobileno, string _street,
                                               string _barangaycode, string _citycode, string _email,
                                               string _initialContact, string _photopath, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_INFO]", cn))
                {
                    cn.Open();

                    cmd.CommandTimeout = 60;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@LASTNAME", _lastname);
                    cmd.Parameters.AddWithValue("@FIRSTNAME", _firstname);
                    cmd.Parameters.AddWithValue("@MIDDLENAME", _middlename);
                    cmd.Parameters.AddWithValue("@MI", _mi);
                    cmd.Parameters.AddWithValue("@SUFFIX", _suffix);
                    cmd.Parameters.AddWithValue("@FULLNAME", _fullname);
                    cmd.Parameters.AddWithValue("@GENDERCODE", _gendercode);
                    cmd.Parameters.AddWithValue("@DOB", _dob);
                    cmd.Parameters.AddWithValue("@POB", _pob);
                    cmd.Parameters.AddWithValue("@AGE", _age);
                    cmd.Parameters.AddWithValue("@CITIZENSHIPCODE", _citizenshipcode);
                    cmd.Parameters.AddWithValue("@RELIGIONCODE", _religioncode);
                    cmd.Parameters.AddWithValue("@TELNO", _telno);
                    cmd.Parameters.AddWithValue("@MOBILENO", _mobileno);
                    cmd.Parameters.AddWithValue("@STREET", _street);
                    cmd.Parameters.AddWithValue("@BARANGAYCODE", _barangaycode);
                    cmd.Parameters.AddWithValue("@CITYCODE", _citycode);
                    cmd.Parameters.AddWithValue("@EMAIL", _email);
                    cmd.Parameters.AddWithValue("@INITIALCONTACT", _initialContact);
                    cmd.Parameters.AddWithValue("@PHOTOPATH", _photopath);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*UPDATE STUDENT ADDRESS TO ONLINE-SSIMS*/
        public void UPDATE_STUDENT_ADDRESS_TO_ONLINE_SSIMS(string studentNumber)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[Student].[spPOST_STUDENT_ID_INFO_ADDR_TO_ONLINE]", cn))
                {
                    cn.Open();
                    cmd.CommandTimeout = 100;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studentNumber);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*UPDATE STUDENT RELATIVES TO ONLINE-SSIMS*/
        public void UPDATE_STUDENT_RELATIVES_TO_ONLINE_SSIMS(string studentNumber, string userId)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[Student].[spPOST_STUDENT_ID_INFO_RELATIVES_TO_ONLINE]", cn))
                {
                    cn.Open();
                    cmd.CommandTimeout = 100;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studentNumber);
                    cmd.Parameters.AddWithValue("@respondUser", userId);

                    cmd.ExecuteNonQuery();
                }
            }
        }   
        
        /*UPDATE STUDENT STATUS TO ONLINE-SSIMS*/
        public void UPDATE_STUDENT_STATUS_TO_ONLINE_SSIMS(string studentNumber, string status)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[Student].[spPOST_STUDENT_ID_INFO_STATUS_TO_ONLINE]", cn))
                {
                    cn.Open();
                    cmd.CommandTimeout = 100;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studentNumber);
                    cmd.Parameters.AddWithValue("@status", status);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*UPDATE STUDENT RESERVATION TO ONLINE-SSIMS*/
        public void POST_STUDENT_RESERVATION_STATUS_TO_ONLINE(string studentNumber)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[Student].[spPOST_STUDENT_RESERVATION_STATUS_TO_ONLINE]", cn))
                {
                    cn.Open();
                    cmd.CommandTimeout = 100;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studentNumber);
 
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /*UPDATE STUDENT CREDENTIALS*/
        public void UPDATE_STUDENTCREDENTIALS(string _studnum, bool _form138, bool _bc, bool _colored1x1,
                                            bool _brownenvelope, bool _gm, bool _recommendation,
                                            bool _form137, bool _ncae, bool _interview, string _other)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_CREDENTIALS]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@Form138", _form138);
                    cmd.Parameters.AddWithValue("@BC", _bc);
                    cmd.Parameters.AddWithValue("@Colored1x1", _colored1x1);
                    cmd.Parameters.AddWithValue("@BrownEnvelope", _brownenvelope);
                    cmd.Parameters.AddWithValue("@GM", _gm);
                    cmd.Parameters.AddWithValue("@Recommendation", _gm);
                    cmd.Parameters.AddWithValue("@Form137", _form138);
                    cmd.Parameters.AddWithValue("@NCAE", _ncae);
                    cmd.Parameters.AddWithValue("@Interview", _interview);
                    cmd.Parameters.AddWithValue("@Other", _other);


                    cmd.ExecuteNonQuery();



                }
            }
        }
        //REMOVE SELECTED SIBLINGS
        public void DELETE_SIBLING(int _id)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[sp_DELETE_STUDENT_SIBLING]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", _id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //UPDATE SIBLING ORDER
        public void UPDATE_SIBLING_ORDER(int _id, int _siblingorder)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_SIBLINGS_ORDER]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", _id);
                    cmd.Parameters.AddWithValue("@SIBLINGORDER", _siblingorder);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        //UPDATE MODE OF STUDENT TRANSPORTATION
        public void UPDATE_STUDENT_MOT(string _studnum, string _motcode, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_MOT]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                     
                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@MOTCODE", _motcode);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //UPDATE MODE OF STUDENT TRANSPORTATION TO ONLINE-SSIMS
        public void UPDATE_STUDENT_MOT_TO_ONLINE(string _studnum, string _motcode)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {

                using (SqlCommand cmd = new SqlCommand("[STUDENT].[spPOST_STUDENT_ID_INFO_MOT_TO_ONLINE]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@MOTCODE", _motcode);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //UPDATE MODE OF STUDENT STRAND TO ONLINE-SSIMS
        public void UPDATE_STUDENT_STRAND_TO_ONLINE(string studNum, string strandCode)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {

                using (SqlCommand cmd = new SqlCommand("[STUDENT].[spPOST_STUDENT_ID_INFO_STRAND_ONLINE]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    cmd.Parameters.AddWithValue("@strandCode", strandCode);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #region UPDATE EMPLOYEE CHILD
        //UPDATE EMPLOYEE CHILD
        public void UPDATE_STUDENT_EMPLOYEE_CHILD(string _studnum, bool _isempchild, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_EMPLOYEE_CHILD]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@EMPLOYEECHILD", _isempchild);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region UPDATE STRANDCODE
        public void UPDATE_STRANDCODE(string _studnum, string _strandcode, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spUPDATE_STUDENT_STRAND", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@STRANDCODE", _strandcode);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region UPDATE STUDENT STATUS
        //UPDATE STUDENT STATUS
        public void UPDATE_STATUS(string _studnum, bool _status, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spUPDATE_STUDENT_STATUS", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@STATUS", _status);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        public bool CHECK_DUPLICATE_BARCODE(string studNum, string barcode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                var sql = @"SELECT * FROM Registration.Student_Info_MF 
                            WHERE StudNum <> @studNum 
                            AND ISNULL(Barcode, '') = @barcode";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    cmd.Parameters.AddWithValue("@barcode", barcode);
                
                    var dr = cmd.ExecuteReader();// cmd.ExecuteNonQuery();

                    return dr.HasRows;
                }
            }
        }

        #region UPDATE BARCODE
        public void UPDATE_BARCODE(string _studnum, string _barcode, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_BARCODE]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@BARCODE", _barcode);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region UPDATE LRN
        public void UPDATE_LRN(string _studnum, string _lrn, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_LRN]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@LRN", _lrn);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region UPDATE STUDENT SERIALS
        public void UPDATE_SERIALS(string studnum, string barcode, string lrn, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_STUDENT_BARCODE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STUDNUM", studnum);
                    cmd.Parameters.AddWithValue("@BARCODE", barcode);
                    cmd.Parameters.AddWithValue("@USERID", userid);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_STUDENT_LRN", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STUDNUM", studnum);
                    cmd.Parameters.AddWithValue("@LRN", lrn);
                    cmd.Parameters.AddWithValue("@USERID", userid);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region UPDATE RESERVATION STATUS
        public bool UPDATE_RESERVATION_STATUS(string _studnum, string _statcoder, string _userid)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {

                    using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_RESERVATION_STATUS]", cn))
                    {
                        cn.Open();

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                        cmd.Parameters.AddWithValue("@STATCODER", _statcoder);
                        cmd.Parameters.AddWithValue("@USERID", _userid);

                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                LogException(exc);
                return false;
            }
        }
        #endregion

        #region UPDATE ENROLLMENT STATUS
        public bool UPDATE_ENROLLMENT_STATUS(string _studnum, string _statcode, string _userid)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {

                    using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_ENROLLMENT_STATUS]", cn))
                    {
                        cn.Open();

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                        cmd.Parameters.AddWithValue("@STATCODE", _statcode);
                        cmd.Parameters.AddWithValue("@USERID", _userid);

                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                LogException(exc);
                return false;
            }
        }
        #endregion

        #region UPDATE TO ENROLL
        public bool UPDATE_TOENROLL(bool _toEnroll, string _studnum, string _userid, bool _spcArr)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {

                    using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_TO_ENROLL]", cn))
                    {
                        cn.Open();

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TOENROLL", _toEnroll);
                        cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                        cmd.Parameters.AddWithValue("@USERID", _userid);
                        cmd.Parameters.AddWithValue("@SPCARR", _spcArr);

                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                LogException(exc);
                return false;
            }
        }
        #endregion

        #region UPDATE MOVING UP
        public bool UPDATE_STUDENT_MOVING_UP(bool _movingup, string _userid, string _studnum)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_STUDENT_MOVING_UP", cn))
                    {
                        cn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@P_STUDNUM", _studnum);
                        cmd.Parameters.AddWithValue("@P_MOVINGUP", _movingup);
                        cmd.Parameters.AddWithValue("@P_USERID", _userid);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                LogException(exc);
                return false;
            }
        }
        #endregion

        #region "UPDATE TAGGING FOR PRINTING OF ID CARD"
        public void UPDATE_READY_FOR_PRINTIN_IDCARD(string StudNum, bool isReady, string UserId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_STUD_PRINT_IDCARD", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", StudNum);
                    cmd.Parameters.AddWithValue("@readyToPrint", isReady);
                    cmd.Parameters.AddWithValue("@userId", UserId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion


        #endregion



        #region "GET-DISPLAY / VALIDATE FUNCTION AREA"


        public bool GETINFO(string _studnum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spGetStudentInformation", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);


                    SqlDataReader dr = cmd.ExecuteReader();// cmd.ExecuteNonQuery();
                    if (dr.HasRows)
                    {
                        x = true;

                        while (dr.Read())
                        {
                            STUDNO = dr["StudNum"].ToString();
                            APPNUM = dr["AppNum"].ToString();
                            LASTNAME = dr["LastName"].ToString();
                            FIRSTNAME = dr["FirstName"].ToString();
                            MIDDLENAME = dr["MiddleName"].ToString();
                            MI = dr["MI"].ToString();
                            ENTRY_LEVELCODE = dr["Entry_LevelCode"].ToString();
                            CURRENT_LEVELCODE = dr["Current_LevelCode"].ToString();
                            CURRENT_LEVELDESC = dr["LevelTypeDesc"].ToString();
                            SUFFIX = dr["Suffix"].ToString();
                            DOB = (DateTime)dr["DOB"];
                            POB = dr["POB"].ToString();
                            GENDERCODE = dr["GenderCode"].ToString();

                            TELNO = dr["Telno"].ToString();
                            MOBILENO = dr["MobileNo"].ToString();
                            EMAIL = dr["Email"].ToString();
                            STREET = dr["Street"].ToString();
                            CITYCODE = dr["CityCode"].ToString();
                            BARANGAYCODE = dr["BarangayCode"].ToString();
                            INITIALCONTACT = dr["InitialContact"].ToString();
                            STATUS = (bool)dr["Status"];
                            SSICHILD = (bool)dr["SSIChild"];

                            SECTION = dr["current_section"].ToString();
                            CITIZENSHIPCODE = dr["CitizenshipCode"].ToString();
                            RELIGIONCODE = dr["ReligionCode"].ToString();

                            LRN = dr["LRN"].ToString();
                            BARCODE = dr["Barcode"].ToString();
                            STRANDCODE = dr["Strandcode"].ToString();
                            STRANDDESC = dr["StrandName"].ToString();

                            //MODE OF TRANSPORTATION
                            MOTCODE = dr["motCode"].ToString();

                            PHOTOLOC = dr["PhotoPath"].ToString();

                            DATEAPPLIED = dr["Entry_Date"].ToString();
                            ENTRYSY = dr["Entry_Sy"].ToString();

                            //FROM STUDENT MF DATA
                            STATCODER = dr["statcoder"].ToString();
                            STATCODE = dr["statcode"].ToString();

                        }
                    }
                    else
                    {
                        x = false;
                    }


                }
            }

            return x;
        }

        public bool GET_STUDENT_HEALTH_CONCERN(string _studnum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Health].[spGET_HEALTH_CONCERN]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SNUM", _studnum);
                    cmd.Parameters.AddWithValue("@htype", "STUDENT");

                    var dr = cmd.ExecuteReader();// cmd.ExecuteNonQuery();

                    return dr.HasRows;
                }
            }
        }     
        
        public bool GETINFO_HEALTH(string _studnum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spGET_HEALTH_INFO", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);


                    SqlDataReader dr = cmd.ExecuteReader();// cmd.ExecuteNonQuery();
                    if (dr.HasRows)
                    {
                        x = true;

                        while (dr.Read())
                        {
                            STUDNO = dr["StudNum"].ToString();
                            FULLNAME = dr["FullName"].ToString();
                            CURRENT_LEVELDESC = dr["LevelTypeDesc"].ToString();
                            SECTION = dr["section"].ToString();
                            PRIMARY_CONTACT_PERSON = dr["ContactName"].ToString();
                            CONTACT_RELATION = dr["Relation"].ToString();
                            CONTACT_NUMBERS = dr["Contact"].ToString();
                            COMPLETE_ADDRESS = dr["ContactAddress"].ToString();
                            MOTDESC = dr["motDescription"].ToString();
                            PHOTOLOC = dr["PhotoPath"].ToString();
                            //PHOTOLOC = dr["Picture"].ToString();

                        }
                    }
                    else
                    {
                        x = false;
                    }


                }
            }

            return x;
        }

        public bool GETCREDENTIALS(string _studnum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spDisplayStudentCredentials", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);


                    SqlDataReader dr = cmd.ExecuteReader();// cmd.ExecuteNonQuery();
                    if (dr.HasRows)
                    {
                        x = true;

                        while (dr.Read())
                        {
                            //GET THE PUBLIC PROPERTY
                            FORM138 = (bool)dr["FORM138"];
                            BC = (bool)dr["BC"];
                            COLORED1X1 = (bool)dr["COLORED1X1"];
                            BROWNENVELOPE = (bool)dr["BROWNENVELOPE"];
                            GM = (bool)dr["GM"];
                            RECOMMENDATION = (bool)dr["RECOMMENDATION"];
                            FORM137 = (bool)dr["FORM137"];
                            NCAE = (bool)dr["NCAE"];
                            INTERVIEW = (bool)dr["INTERVIEW"];
                            OTHER = dr["OTHER"].ToString();
                        }
                    }
                    else
                    {
                        x = false;
                    }
                }
            }

            return x;

        }

        public DataTable GET_EDUBACK(string _studnum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spGetStudentEduBack", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }


        public DataTable GET_SIBLINGLIST(string _siblingcode)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select * FROM [Registration].[V_Sibling_List] WHERE siblingcode=@SIBLINGCODE and R_levelCode is not null", cn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@SIBLINGCODE", _siblingcode);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;

        }

        //public class StudentSiblings
        //{
        //    public string sib_code { get; set; }
        //    public string stud_num { get; set; }
        //    public int sib_order { get; set; }
        //    public string stat_code { get; set; }
        //    public bool temp_stat { get; set; }
        //    public string lvl_code { get; set; }
        //    public int lvl_order { get; set; }
        //}

        public void BATCH_UPDATE_SIBLING_ORDER()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                cn.Open();
                //using (SqlCommand cmd = new SqlCommand("xSystem.spUPDATE_BATCH_SIBLINGS", cn))
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.ExecuteNonQuery();
                //}

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Registration.V_Sibling_List ORDER BY SiblingCode ASC, ordr DESC", cn))
                {
                    //cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            int x = 1;
                            string temp_sib_code = "";
                            while (dr.Read())
                            {
                                int id = Convert.ToInt32(dr["ID"].ToString());
                                string sib_code = dr["SiblingCode"].ToString();
                                int c_sib = Convert.ToInt32((Utilities.RValue("sp_CountSib", "@v_sibcode", sib_code)).ToString());
                                if (temp_sib_code != sib_code)
                                {
                                    x = 1;
                                    if (c_sib <= 1)
                                    {
                                        UPDATE_SIBLING_TEMP_STAT(id);
                                    }
                                }
                                UPDATE_SIBLING_ORDER(id, x);
                                temp_sib_code = sib_code;
                                x++;
                            }
                        }
                    }
                }
            }
        }

        public void UPDATE_SIBLING_TEMP_STAT(int id)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Registration.Student_Siblings_MF SET SiblingOrder = 0, temp_stat = 0 WHERE ID = @ID", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GET_SELECTED_SIBLINGS(string _lastname)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spGetStudentSibling", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LASTNAME", _lastname);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        //Default without parameter
        public DataTable GET_SELECTED_SIBLINGS()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spGetStudentSibling_Default", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public bool CHECK_FAMILY(string _studnum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spCheckStudentExist", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);


                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        x = true;

                        while (dr.Read())
                        {
                            FLASTNAME = dr["FLASTNAME"].ToString();
                            FFIRSTNAME = dr["FFIRSTNAME"].ToString();
                            FMIDDLENAME = dr["FMIDDLENAME"].ToString();
                            FOCCUPATION = dr["FOCCUPATION"].ToString();
                            FCITIZENSHIP = dr["FCITIZENSHIP"].ToString();
                            FEDUCATION = dr["FEDUCATION"].ToString();
                            FCOMPANY = dr["FCOMPANY"].ToString();
                            FCOMPADDRESS = dr["FCOMPADDRESS"].ToString();
                            FTELEPHONE = dr["FTELEPHONE"].ToString();
                            FMOBILE = dr["FMOBILE"].ToString();
                            FHOMEADDR = dr["FHOMEADDR"].ToString();

                            MLASTNAME = dr["MLASTNAME"].ToString();
                            MFIRSTNAME = dr["MFIRSTNAME"].ToString();
                            MMIDDLENAME = dr["MMIDDLENAME"].ToString();
                            MOCCUPATION = dr["MOCCUPATION"].ToString();
                            MCITIZENSHIP = dr["MCITIZENSHIP"].ToString();
                            MEDUCATION = dr["MEDUCATION"].ToString();
                            MCOMPANY = dr["MCOMPANY"].ToString();
                            MCOMPADDRESS = dr["MCOMPADDRESS"].ToString();
                            MTELEPHONE = dr["MTELEPHONE"].ToString();
                            MMOBILE = dr["MMOBILE"].ToString();
                            MHOMEADDR = dr["MHOMEADDR"].ToString();

                            GLASTNAME = dr["GLASTNAME"].ToString();
                            GFIRSTNAME = dr["GFIRSTNAME"].ToString();
                            GMIDDLENAME = dr["GMIDDLENAME"].ToString();
                            GADDRESS = dr["GADDRESS"].ToString();
                            GTELEPHONE = dr["GTELEPHONE"].ToString();
                            GMOBILE = dr["GMOBILE"].ToString();
                            GRELATION = dr["GRELATION"].ToString();

                            PRIMARYCONTACT = Convert.ToInt32(dr["PrimaryContactID"].ToString());

                        }
                    }

                    else
                    {
                        x = false;
                    }
                }
            }

            return x;

        }

        //Will check and get if exist
        public bool CHECK_SIBLING_EXIST(string _studnum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spCHECK_STUDENT_SIBLINGS]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        x = true;

                        while (dr.Read())
                        {
                            SIBLINGCODE = dr["SiblingCode"].ToString();
                        }

                    }
                    else
                    {
                        x = false;
                    }
                }

                return x;
            }

        }


        public bool CHECK_SIBLING_ORDER_EXIST(string _siblingcode, int _siblingorder)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spCHECK_STUDENT_SIBLING_ORDER]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SIBLINGCODE", _siblingcode);
                    cmd.Parameters.AddWithValue("@SIBLINGORDER", _siblingorder);


                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        x = true;

                    }
                    else
                    {
                        x = false;
                    }
                }

                return x;
            }

        }

        public void POST_SIBLINGS_TO_ONLINE(string siblingCode, string studNum, string order)
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[Asmt].[spPOST_SIBLINGS_TO_ONLINE]", cn))
                {
                    cn.Open();
                    cmd.CommandTimeout = 100;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@siblingCode", siblingCode);
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    cmd.Parameters.AddWithValue("@siblingOrder", order);
                    cmd.ExecuteNonQuery();
                }
            }
        }




        //Display List of Student With Balance-Deliquent

        public DataTable GETDELIQUENTLIST()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayDeliquentAccount", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }


        public string GET_STUDENT_GENDER(string studnum)
        {
            string gender = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT GenderCode FROM Registration.Student_Info_MF WHERE StudNum = @p_studnum", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@p_studnum", studnum);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            gender = dr["GenderCode"].ToString();
                        }
                    }
                }
            }

            return gender;
        }

        public bool GET_STUDENT_INFORMATION(string _studnum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("Select * from Registration.V_GET_STUDENT_INFO WHERE studnum=@STUDNUM", cn))
                {
                    cn.Open();

                    //cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);


                    SqlDataReader dr = cmd.ExecuteReader();// cmd.ExecuteNonQuery();
                    if (dr.HasRows)
                    {
                        x = true;

                        while (dr.Read())
                        {
                            STUDNO = dr["StudNum"].ToString();
                            APPNUM = dr["AppNum"].ToString();
                            LASTNAME = dr["LastName"].ToString();
                            FIRSTNAME = dr["FirstName"].ToString();
                            MIDDLENAME = dr["MiddleName"].ToString();
                            MI = dr["MI"].ToString();
                            FULLNAME = dr["Fullname"].ToString();
                            FULLNAME2 = dr["StudName"].ToString();
                            ENTRY_LEVELCODE = dr["Entry_LevelCode"].ToString();
                            CURRENT_LEVELCODE = dr["Current_LevelCode"].ToString();
                            CURRENT_LEVELDESC = dr["LevelTypeDesc"].ToString();
                            SUFFIX = dr["Suffix"].ToString();
                            DOB = (DateTime)dr["DOB"];
                            POB = dr["POB"].ToString();
                            GENDERCODE = dr["GenderCode"].ToString();

                            RESERVE_LEVELCODE = dr["R_levelCode"].ToString();
                            RESERVE_LEVELDESC = dr["R_levelDesc"].ToString();

                            TELNO = dr["Telno"].ToString();
                            MOBILENO = dr["MobileNo"].ToString();
                            EMAIL = dr["Email"].ToString();
                            STREET = dr["Street"].ToString();
                            CITYCODE = dr["CityCode"].ToString();
                            BARANGAYCODE = dr["BarangayCode"].ToString();
                            INITIALCONTACT = dr["InitialContact"].ToString();


                            STATUS = (bool)dr["Status"];
                            SSICHILD = (bool)dr["SSIChild"];

                            SECTION = dr["current_section"].ToString();
                            CITIZENSHIPCODE = dr["CitizenshipCode"].ToString();
                            RELIGIONCODE = dr["ReligionCode"].ToString();

                            LRN = dr["LRN"].ToString();
                            BARCODE = dr["Barcode"].ToString();
                            STRANDCODE = dr["Strandcode"].ToString();
                            STRANDDESC = dr["StrandName"].ToString();
                            STUDTYPE = dr["StudTypeCode"].ToString();

                            //MODE OF TRANSPORTATION
                            MOTCODE = dr["motCode"].ToString();
                            MOTDESC = dr["motDescription"].ToString();
                            MOTCODEIN = dr["MotCodeIn"].ToString();

                            PHOTOLOC = dr["PhotoPath"].ToString();

                            DATEAPPLIED = dr["Entry_Date"].ToString();
                            ENTRYSY = dr["Entry_Sy"].ToString();

                            //FROM STUDENT MF DATA
                            if (string.IsNullOrEmpty(dr["ToEnroll"].ToString()))
                            {
                                TOENROLL = false;
                            }
                            else
                            {
                                TOENROLL = (bool)dr["ToEnroll"];
                            }

                            STATCODER = dr["statcoder"].ToString();
                            STATCODE = dr["statcode"].ToString();
                            if (dr["StatCodeForLeavingEffectivityDate"].ToString() == "")
                                StatCodeForLeavingEffectivityDate = null;
                            else
                                StatCodeForLeavingEffectivityDate = Convert.ToDateTime(dr["StatCodeForLeavingEffectivityDate"]);

                            if (dr["StatCodeSystemDate"].ToString() == "")
                                StatCodeSystemDate = null;
                            else
                                StatCodeSystemDate = Convert.ToDateTime(dr["StatCodeSystemDate"].ToString());

                            STATCODEE = dr["StatCodeE"].ToString();
                            SPC_ARR = (bool)dr["Spc_Arr"];
                            BL = (bool)dr["BL"];

                            // =========================================================================================
                            // ADDED THIS TO GET THE STATUS OF MOVING UP STUDENTS AND BACKOUT (RESERVATION & ENROLLMENT)
                            // 2017.12.14 -- Marlowe Escaros
                            MOVINGUP = (bool)dr["M_UP"];
                            BO_R = (bool)dr["BO_R"];
                            BO_E = (bool)dr["BO_E"];
                            STATCODEBO_E = dr["StatCodeBO_E"].ToString();
                            // =========================================================================================


                            /*
                             RELATIVES
                             */

                            //FATHER INFORMATION
                            FLASTNAME = dr["FLastName"].ToString();
                            FFIRSTNAME = dr["FFirstName"].ToString();
                            FMIDDLENAME = dr["FMiddleName"].ToString();
                            FEDUCATION = dr["FEducation"].ToString();
                            FCOMPANY = dr["FCompany"].ToString();
                            FTELEPHONE = dr["FTELEPHONE"].ToString();
                            FMOBILE = dr["FMOBILE"].ToString();
                            FCOMPADDRESS = dr["FCompAddress"].ToString();
                            FCITIZENSHIP = dr["FCitizenship"].ToString();
                            FOCCUPATION = dr["FOccupation"].ToString();
                            FEMAIL = dr["FEmail"].ToString();

                            //MOTHER INFORMATION
                            MLASTNAME = dr["MLastName"].ToString();
                            MFIRSTNAME = dr["MFirstName"].ToString();
                            MMIDDLENAME = dr["MMiddleName"].ToString();
                            MAIDENLASTNAME = dr["MaidenLname"].ToString();
                            MAIDENFIRSTNAME = dr["MaidenFname"].ToString();
                            MAIDENMIDDLENAME = dr["MaidenMname"].ToString();
                            MEDUCATION = dr["MEducation"].ToString();
                            MCOMPANY = dr["MCompany"].ToString();
                            MTELEPHONE = dr["MTELEPHONE"].ToString();
                            MMOBILE = dr["MMOBILE"].ToString();
                            MCOMPADDRESS = dr["MCompAddress"].ToString();
                            MCITIZENSHIP = dr["MCitizenship"].ToString();
                            MOCCUPATION = dr["MOccupation"].ToString();
                            MEMAIL = dr["MEmail"].ToString();

                            //GUARDIAN INFORMATION
                            GLASTNAME = dr["GLastName"].ToString();
                            GFIRSTNAME = dr["GFirstname"].ToString();
                            GMIDDLENAME = dr["GMiddleName"].ToString();
                            GADDRESS = dr["GAddress"].ToString();
                            GRELATION = dr["GRelation"].ToString();
                            GTELEPHONE = dr["GTelephone"].ToString();
                            GMOBILE = dr["GMobile"].ToString();
                            GEMAIL = dr["GEmail"].ToString();

                            //additional fields
                            FCIVILSTAT = dr["FCivilStat"].ToString();
                            MCIVILSTAT = dr["MCivilStat"].ToString();
                            FHOMEADDR = dr["FHomeAddr"].ToString();
                            MHOMEADDR = dr["MHomeAddr"].ToString();
                            FHOMETEL = dr["FHomeTel"].ToString();
                            MHOMETEL = dr["MHomeTel"].ToString();
                            GCITIZENSHIP = dr["GCitizenship"].ToString();
                            GEDUCATION = dr["GEducation"].ToString();
                            GOCCUPATION = dr["GOccupation"].ToString();
                            GCOMPANY = dr["GCompany"].ToString();
                            GCOMPANYTEL = dr["GCompanyTel"].ToString();
                            GCOMPANYADDRESS = dr["GCompAddress"].ToString();
                            //-----------------

                            //Validate if student already have record in relative table
                            if (string.IsNullOrEmpty((dr["PrimaryContactID"].ToString())))
                            {
                                PRIMARYCONTACT = 1;
                            }
                            else
                            {
                                PRIMARYCONTACT = Convert.ToInt32(dr["PrimaryContactID"]);
                            }

                            //CREDENTIALS OF STUDENT 03/04/2017
                            if (string.IsNullOrEmpty(dr["FORM138"].ToString()))
                            { FORM138 = false; }
                            else { FORM138 = (bool)dr["FORM138"]; }

                            if (string.IsNullOrEmpty(dr["BC"].ToString()))
                            {
                                BC = false;
                            }
                            else { BC = (bool)dr["BC"]; }

                            if (string.IsNullOrEmpty(dr["COLORED1X1"].ToString()))
                            { COLORED1X1 = false; }
                            else { COLORED1X1 = (bool)dr["COLORED1X1"]; }

                            if (string.IsNullOrEmpty(dr["BROWNENVELOPE"].ToString()))
                            { BROWNENVELOPE = false; }
                            else { BROWNENVELOPE = (bool)dr["BROWNENVELOPE"]; }

                            if (string.IsNullOrEmpty(dr["GM"].ToString()))
                            {
                                GM = false;
                            }
                            else
                            {
                                GM = (bool)dr["GM"];
                            }

                            if (string.IsNullOrEmpty(dr["RECOMMENDATION"].ToString()))
                            {
                                RECOMMENDATION = false;
                            }
                            else
                            {
                                RECOMMENDATION = (bool)dr["RECOMMENDATION"];
                            }

                            if (string.IsNullOrEmpty(dr["FORM137"].ToString()))
                            {
                                FORM137 = false;
                            }
                            else
                            {
                                FORM137 = (bool)dr["FORM137"];
                            }

                            if (string.IsNullOrEmpty(dr["NCAE"].ToString()))
                            { NCAE = false; }
                            else { NCAE = (bool)dr["NCAE"]; }

                            if (string.IsNullOrEmpty(dr["INTERVIEW"].ToString()))
                            { INTERVIEW = false; }
                            else
                            { INTERVIEW = (bool)dr["INTERVIEW"]; }

                            OTHER = dr["OTHER"].ToString();


                        }
                    }
                    else
                    {
                        x = false;
                    }


                }
            }

            return x;
        }

        //REVISION 2.0
        //02/22/2017
        public Student GetStudentInformation(string _studnum)
        {
            Student s = new Student();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Registration.V_GET_STUDENT_INFO WHERE studnum=@STUDNUM", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    SqlDataReader dr = cmd.ExecuteReader();// cmd.ExecuteNonQuery();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            s.STUDNO = dr["StudNum"].ToString();
                            s.APPNUM = dr["AppNum"].ToString();
                            s.LASTNAME = dr["LastName"].ToString();
                            s.FIRSTNAME = dr["FirstName"].ToString();
                            s.MIDDLENAME = dr["MiddleName"].ToString();
                            s.MI = dr["MI"].ToString();
                            s.FULLNAME = dr["Fullname"].ToString();
                            s.FULLNAME2 = dr["StudName"].ToString();
                            s.ENTRY_LEVELCODE = dr["Entry_LevelCode"].ToString();
                            s.CURRENT_LEVELCODE = dr["Current_LevelCode"].ToString();
                            s.CURRENT_LEVELDESC = dr["LevelTypeDesc"].ToString();
                            s.SUFFIX = dr["Suffix"].ToString();
                            s.DOB = (DateTime)dr["DOB"];
                            s.POB = dr["POB"].ToString();
                            s.GENDERCODE = dr["GenderCode"].ToString();

                            s.RESERVE_LEVELCODE = dr["R_levelCode"].ToString();
                            s.RESERVE_LEVELDESC = dr["R_levelDesc"].ToString();

                            s.TELNO = dr["Telno"].ToString();
                            s.MOBILENO = dr["MobileNo"].ToString();
                            s.EMAIL = dr["Email"].ToString();
                            s.STREET = dr["Street"].ToString();
                            s.CITYCODE = dr["CityCode"].ToString();
                            s.BARANGAYCODE = dr["BarangayCode"].ToString();
                            s.INITIALCONTACT = dr["InitialContact"].ToString();

                            s.STATUS = (bool)dr["Status"];
                            s.SSICHILD = (bool)dr["SSIChild"];

                            s.SECTION = dr["current_section"].ToString();
                            s.CITIZENSHIPCODE = dr["CitizenshipCode"].ToString();
                            s.RELIGIONCODE = dr["ReligionCode"].ToString();

                            s.LRN = dr["LRN"].ToString();
                            s.BARCODE = dr["Barcode"].ToString();
                            s.STRANDCODE = dr["Strandcode"].ToString();
                            s.STRANDDESC = dr["StrandName"].ToString();
                            s.STUDTYPE = dr["StudTypeCode"].ToString();

                            //MODE OF TRANSPORTATION
                            s.MOTCODE = dr["motCode"].ToString();
                            s.MOTDESC = dr["motDescription"].ToString();
                            s.MOTCODEIN = dr["motCodeIN"].ToString();

                            s.PHOTOLOC = dr["PhotoPath"].ToString();

                            s.DATEAPPLIED = dr["Entry_Date"].ToString();
                            s.ENTRYSY = dr["Entry_Sy"].ToString();

                            //FROM STUDENT MF DATA
                            if (string.IsNullOrEmpty(dr["ToEnroll"].ToString()))
                            {
                                s.TOENROLL = false;
                            }
                            else
                            {
                                s.TOENROLL = (bool)dr["ToEnroll"];
                            }

                            s.STATCODER = dr["statcoder"].ToString();
                            s.STATCODE = dr["statcode"].ToString();
                            s.STATCODEE = dr["StatCodeE"].ToString();
                            s.SPC_ARR = (bool)dr["Spc_Arr"];
                            s.BL = (bool)dr["BL"];

                            // =========================================================================================
                            // ADDED THIS TO GET THE STATUS OF MOVING UP STUDENTS AND BACKOUT (RESERVATION & ENROLLMENT)
                            // 2017.12.14 -- Marlowe Escaros
                            s.MOVINGUP = (bool)dr["M_UP"];
                            s.BO_R = (bool)dr["BO_R"];
                            s.BO_E = (bool)dr["BO_E"];
                            s.STATCODEBO_E = dr["StatCodeBO_E"].ToString();
                            // =========================================================================================

                            /*
                             RELATIVES
                             */

                            //FATHER INFORMATION
                            s.FLASTNAME = dr["FLastName"].ToString();
                            s.FFIRSTNAME = dr["FFirstName"].ToString();
                            s.FMIDDLENAME = dr["FMiddleName"].ToString();
                            s.FEDUCATION = dr["FEducation"].ToString();
                            s.FCOMPANY = dr["FCompany"].ToString();
                            s.FTELEPHONE = dr["FTELEPHONE"].ToString();
                            s.FMOBILE = dr["FMOBILE"].ToString();
                            s.FCOMPADDRESS = dr["FCompAddress"].ToString();
                            s.FCITIZENSHIP = dr["FCitizenship"].ToString();
                            s.FOCCUPATION = dr["FOccupation"].ToString();
                            s.FEMAIL = dr["FEmail"].ToString();

                            //MOTHER INFORMATION
                            s.MLASTNAME = dr["MLastName"].ToString();
                            s.MFIRSTNAME = dr["MFirstName"].ToString();
                            s.MMIDDLENAME = dr["MMiddleName"].ToString();
                            s.MAIDENLASTNAME = dr["MaidenLname"].ToString();
                            s.MAIDENFIRSTNAME = dr["MaidenFname"].ToString();
                            s.MAIDENMIDDLENAME = dr["MaidenMname"].ToString();
                            s.MEDUCATION = dr["MEducation"].ToString();
                            s.MCOMPANY = dr["MCompany"].ToString();
                            s.MTELEPHONE = dr["MTELEPHONE"].ToString();
                            s.MMOBILE = dr["MMOBILE"].ToString();
                            s.MCOMPADDRESS = dr["MCompAddress"].ToString();
                            s.MCITIZENSHIP = dr["MCitizenship"].ToString();
                            s.MOCCUPATION = dr["MOccupation"].ToString();
                            s.MEMAIL = dr["MEmail"].ToString();

                            //GUARDIAN INFORMATION
                            s.GLASTNAME = dr["GLastName"].ToString();
                            s.GFIRSTNAME = dr["GFirstname"].ToString();
                            s.GMIDDLENAME = dr["GMiddleName"].ToString();
                            s.GADDRESS = dr["GAddress"].ToString();
                            s.GRELATION = dr["GRelation"].ToString();
                            s.GTELEPHONE = dr["GTelephone"].ToString();
                            s.GMOBILE = dr["GMobile"].ToString();
                            s.GEMAIL = dr["GEmail"].ToString();

                            //additional fields
                            s.FCIVILSTAT = dr["FCivilStat"].ToString();
                            s.MCIVILSTAT = dr["MCivilStat"].ToString();
                            s.FHOMEADDR = dr["FHomeAddr"].ToString();
                            s.MHOMEADDR = dr["MHomeAddr"].ToString();
                            s.FHOMETEL = dr["FHomeTel"].ToString();
                            s.MHOMETEL = dr["MHomeTel"].ToString();
                            s.GCITIZENSHIP = dr["GCitizenship"].ToString();
                            s.GEDUCATION = dr["GEducation"].ToString();
                            s.GOCCUPATION = dr["GOccupation"].ToString();
                            s.GCOMPANY = dr["GCompany"].ToString();
                            s.GCOMPANYTEL = dr["GCompanyTel"].ToString();
                            s.GCOMPANYADDRESS = dr["GCompAddress"].ToString();
                            //-----------------

                            //Validate if student already have record in relative table
                            if (string.IsNullOrEmpty((dr["PrimaryContactID"].ToString())))
                            {
                                s.PRIMARYCONTACT = 1;
                            }
                            else
                            {
                                s.PRIMARYCONTACT = Convert.ToInt32(dr["PrimaryContactID"]);
                            }

                            //CREDENTIALS OF STUDENT 03/04/2017
                            if (!string.IsNullOrEmpty(dr["CredId"].ToString()))
                            {
                                s.FORM138 = (bool)dr["FORM138"];
                                s.BC = (bool)dr["BC"];
                                s.BC_PSA = (bool)dr["BC_PSA"];
                                s.BC_NSO = (bool)dr["BC_NSO"];
                                s.BC_CR = (bool)dr["BC_CR"];
                                s.BC_NSO_ORIG = (bool)dr["BC_NSO_ORIG"];
                                s.BC_NSO_PCOPY = (bool)dr["BC_NSO_PCOPY"];
                                s.BC_CR_ORIG = (bool)dr["BC_CR_ORIG"];
                                s.BC_CR_PCOPY = (bool)dr["BC_CR_PCOPY"];
                                s.COLORED1X1 = (bool)dr["COLORED1X1"];
                                s.BROWNENVELOPE = (bool)dr["BROWNENVELOPE"];
                                s.GM = (bool)dr["GM"];
                                s.RECOMMENDATION = (bool)dr["RECOMMENDATION"];
                                s.FORM137 = (bool)dr["FORM137"];
                                s.NCAE = (bool)dr["NCAE"];
                                s.INTERVIEW = (bool)dr["INTERVIEW"];
                                s.PASSPORT = (bool)dr["Passport"];
                                s.VISA = (bool)dr["Visa"];
                                s.MEDCERT = (bool)dr["medCert"];
                            }

                            s.OTHER = dr["OTHER"].ToString();
                        }
                    }
                }
            }

            return s;
        }

        public bool SavePersonalInformation(Student student, string userId)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_STUDENT_INFORMATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "PI");
                    cmd.Parameters.AddWithValue("@studNum", student.STUDNO);
                    cmd.Parameters.AddWithValue("@studLname", student.LASTNAME);
                    cmd.Parameters.AddWithValue("@studFname", student.FIRSTNAME);
                    cmd.Parameters.AddWithValue("@studMname", student.MIDDLENAME);
                    cmd.Parameters.AddWithValue("@studSuffix", student.SUFFIX);
                    cmd.Parameters.AddWithValue("@fullname", student.FULLNAME);
                    cmd.Parameters.AddWithValue("@gender", student.GENDERCODE);
                    cmd.Parameters.AddWithValue("@dob", student.DOB);
                    cmd.Parameters.AddWithValue("@pob", student.POB);
                    cmd.Parameters.AddWithValue("@studCitizen", student.CITIZENSHIPCODE);
                    cmd.Parameters.AddWithValue("@religion", student.RELIGIONCODE);
                    cmd.Parameters.AddWithValue("@street", student.STREET);
                    cmd.Parameters.AddWithValue("@city", student.CITYCODE);
                    cmd.Parameters.AddWithValue("@brgy", student.BARANGAYCODE);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        r = true;
                    }
                }
            }
            return r;
        }

        public bool SaveContactInformation(Student student, string userId)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_STUDENT_INFORMATION", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "CI");
                    cmd.Parameters.AddWithValue("@studNum", student.STUDNO);
                    cmd.Parameters.AddWithValue("@fLname", student.FLASTNAME);
                    cmd.Parameters.AddWithValue("@fFname", student.FFIRSTNAME);
                    cmd.Parameters.AddWithValue("@fMname", student.FMIDDLENAME);
                    cmd.Parameters.AddWithValue("@fCitizen", student.FCITIZENSHIP);
                    cmd.Parameters.AddWithValue("@fCivil", student.FCIVILSTAT);
                    cmd.Parameters.AddWithValue("@fEmail", student.FEMAIL);
                    cmd.Parameters.AddWithValue("@fMobile", student.FMOBILE);
                    cmd.Parameters.AddWithValue("@fTelephone", student.FHOMETEL);
                    cmd.Parameters.AddWithValue("@fHomeAddr", student.FHOMEADDR);
                    cmd.Parameters.AddWithValue("@fEduc", student.FEDUCATION);
                    cmd.Parameters.AddWithValue("@fOccu", student.FOCCUPATION);
                    cmd.Parameters.AddWithValue("@fCpyName", student.FCOMPANY);
                    cmd.Parameters.AddWithValue("@fCpyNum", student.FTELEPHONE);
                    cmd.Parameters.AddWithValue("@fCpyAddr", student.FCOMPADDRESS);

                    cmd.Parameters.AddWithValue("@mLname", student.MLASTNAME);
                    cmd.Parameters.AddWithValue("@mFname", student.MFIRSTNAME);
                    cmd.Parameters.AddWithValue("@mMname", student.MMIDDLENAME);
                    cmd.Parameters.AddWithValue("@mCitizen", student.MCITIZENSHIP);
                    cmd.Parameters.AddWithValue("@mCivil", student.MCIVILSTAT);
                    cmd.Parameters.AddWithValue("@mEmail", student.MEMAIL);
                    cmd.Parameters.AddWithValue("@mMobile", student.MMOBILE);
                    cmd.Parameters.AddWithValue("@mTelephone", student.MHOMETEL);
                    cmd.Parameters.AddWithValue("@mHomeAddr", student.MHOMEADDR);
                    cmd.Parameters.AddWithValue("@mEduc", student.MEDUCATION);
                    cmd.Parameters.AddWithValue("@mOccu", student.MOCCUPATION);
                    cmd.Parameters.AddWithValue("@mCpyName", student.MCOMPANY);
                    cmd.Parameters.AddWithValue("@mCpyNum", student.MTELEPHONE);
                    cmd.Parameters.AddWithValue("@mCpyAddr", student.MCOMPADDRESS);
                    cmd.Parameters.AddWithValue("@maidenLname", student.MAIDENLASTNAME);
                    cmd.Parameters.AddWithValue("@maidenFname", student.MAIDENFIRSTNAME);
                    cmd.Parameters.AddWithValue("@maidenMname", student.MAIDENMIDDLENAME);

                    cmd.Parameters.AddWithValue("@gLname", student.GLASTNAME);
                    cmd.Parameters.AddWithValue("@gFname", student.GFIRSTNAME);
                    cmd.Parameters.AddWithValue("@gMname", student.GMIDDLENAME);
                    cmd.Parameters.AddWithValue("@gCitizen", student.GCITIZENSHIP);
                    cmd.Parameters.AddWithValue("@gRelation", student.GRELATION);
                    cmd.Parameters.AddWithValue("@gEmail", student.GEMAIL);
                    cmd.Parameters.AddWithValue("@gMobile", student.GMOBILE);
                    cmd.Parameters.AddWithValue("@gTelephone", student.GTELEPHONE);
                    cmd.Parameters.AddWithValue("@gHomeAddr", student.GADDRESS);
                    cmd.Parameters.AddWithValue("@gEduc", student.GEDUCATION);
                    cmd.Parameters.AddWithValue("@gOccu", student.GOCCUPATION);
                    cmd.Parameters.AddWithValue("@gCpyName", student.GCOMPANY);
                    cmd.Parameters.AddWithValue("@gCpyNum", student.GCOMPANYTEL);
                    cmd.Parameters.AddWithValue("@gCpyAddr", student.GCOMPANYADDRESS);
                    cmd.Parameters.AddWithValue("@primaryContact", student.PRIMARYCONTACT);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        r = true;
                    }
                }
            }

            return r;
        }

        public bool SaveCredentials(Student student)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_INFORMATION]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "CR");
                    cmd.Parameters.AddWithValue("@studNum", student.STUDNO);
                    cmd.Parameters.AddWithValue("@form138", student.FORM138);
                    cmd.Parameters.AddWithValue("@BC", student.BC);
                    cmd.Parameters.AddWithValue("@BC_PSA", student.BC_PSA);
                    cmd.Parameters.AddWithValue("@BC_NSO", student.BC_NSO);
                    cmd.Parameters.AddWithValue("@BC_NSO_ORIG", student.BC_NSO_ORIG);
                    cmd.Parameters.AddWithValue("@BC_NSO_PCOPY", student.BC_NSO_PCOPY);
                    cmd.Parameters.AddWithValue("@BC_CR", student.BC_CR);
                    cmd.Parameters.AddWithValue("@BC_CR_ORIG", student.BC_CR_ORIG);
                    cmd.Parameters.AddWithValue("@BC_CR_PCOPY", student.BC_CR_PCOPY);
                    cmd.Parameters.AddWithValue("@colored1x1", student.COLORED1X1);
                    cmd.Parameters.AddWithValue("@brownEnv", student.BROWNENVELOPE);
                    cmd.Parameters.AddWithValue("@goodMoral", student.GM);
                    cmd.Parameters.AddWithValue("@recommendation", student.RECOMMENDATION);
                    cmd.Parameters.AddWithValue("@form137", student.FORM137);
                    cmd.Parameters.AddWithValue("@ncae", student.NCAE);
                    cmd.Parameters.AddWithValue("@interview", student.INTERVIEW);
                    cmd.Parameters.AddWithValue("@passport", student.PASSPORT);
                    cmd.Parameters.AddWithValue("@visa", student.VISA);
                    cmd.Parameters.AddWithValue("@other", student.OTHER);
                    cmd.Parameters.AddWithValue("@medCert", student.MEDCERT);
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        r = true;
                    }
                }
            }

            return r;
        }

        public bool SaveRegistration(Student student, string userId)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_INFORMATION]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "RI");
                    cmd.Parameters.AddWithValue("@studNum", student.STUDNO);
                    cmd.Parameters.AddWithValue("@strand", student.STRANDCODE);
                    cmd.Parameters.AddWithValue("@lrn", student.LRN);
                    cmd.Parameters.AddWithValue("@barcode", student.BARCODE);
                    cmd.Parameters.AddWithValue("@ssiChild", student.SSICHILD);
                    cmd.Parameters.AddWithValue("@toEnroll", student.TOENROLL);
                    cmd.Parameters.AddWithValue("@spcArr", student.SPC_ARR);
                    cmd.Parameters.AddWithValue("@movingUp", student.MOVINGUP);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    if (cmd.ExecuteNonQuery() != 0)
                        r = true;
                }
            }

            return r;
        }

        //List of Teacher
        public DataTable GET_TEACHER_LIST()
        {
            DataTable dt = new DataTable();
            dt = queryCommandDT_StoredProc("xSystem.spGET_TEACHER_LIST");

            return dt;
        }


        /*
         
         */

        //RELATIVE EXIST
        public bool CHECK_RELATIVE_EXIST(string _studnum)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spCHECK_STUDENT_RELATIVE_EXIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);

                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;

        }

        //CREDENTIAL EXIST
        public bool CHECK_CREDENTIAL_EXIST(string _studnum)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spCHECK_STUDENT_CREDENTIAL_EXIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);

                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;

        }

        //CHECK EXIST SECTION AND LEVEL
        public bool CHECK_LEVEL_SECTION_EXIST(string _levelcode, string _sectioncode, string _sy)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCHECK_LEVEL_SECTION_SCHEDULING", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LEVELCODE", _levelcode);
                    cmd.Parameters.AddWithValue("@SECTIONCODE", _sectioncode);
                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;
        }

        //CHECK IF TEACHER ALREADY HAVE ASSIGN ADVISORY
        public bool CHECK_TEACHER_EXIST(string _teacherID, string _sy)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCHECK_TEACHER", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TEACHERID", _teacherID);
                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;
        }

        //CHECK IF ROOM ALREADY OCCUPIED / ASSIGNED
        public bool CHECK_ROOM_EXIST(string _roomID, string _sy)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCHECK_ROOM", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ROOMID", _roomID);
                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;
        }

        #endregion


        public DataTable GET_STUDENT_GENDER_STATISTICS(string sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string sqlquery = string.Format("Select b.GenderCode,COUNT(b.GenderCode) as gendercnt, case when b.GenderCode='B' then '~/images/infograph/Boy.png' else '~/images/infograph/Girl.png' end as imgsrc, " +
                " c.GenderDesc " +
                " from Registration.Student_MF a " +
                " inner join Registration.Student_Info_MF b on b.StudNum = a.StudNum " +
                " inner join Utilities.Gender_RF c on c.GenderCode = b.GenderCode " +
                " where a.SY ='{0}' and a.StatCode = 'EN' and a.BO_E = 0 " +
                " group by b.GenderCode, c.GenderDesc, c.GenderCode " +
                " order by c.GenderCode", sy);
                using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }





        #region "INSERT/UPDATE/DELETE FUNCTIONS"
        //INSERT SECTION

        public void INSERT_STUDENTSTATUS(string _studnum, string _sy, string _leveltypecode, string _statcode, string _userid)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spInsertStudentStatus", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cmd.Parameters.AddWithValue("@LEVELTYPECODE", _leveltypecode);
                    cmd.Parameters.AddWithValue("@STATCODE", _statcode);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();



                }
            }
        }

        public void UPDATE_STUDENTSTATUS(string _studnum, string _sy, string _statcode, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spUpdateStudentStatus", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cmd.Parameters.AddWithValue("@STATCODE", _statcode);
                    cmd.Parameters.AddWithValue("@USERID", _userid);

                    cmd.ExecuteNonQuery();

                }
            }
        }

        //Update Section - 05/13/2016
        public void UPDATE_STUDENTSECTION(string _studnum, string _section, int _roomid, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_STUDENT_SECTION]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@SECTION", _section);
                    cmd.Parameters.AddWithValue("@ROOMID", _roomid);
                    cmd.Parameters.AddWithValue("@USERID", _userid);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void UPDATE_SECTION_PORTAL()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_SECTION_PORTAL]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UPLOAD_ADVISERS_TO_PORTAL()
        {
            using (SqlConnection cn = new SqlConnection(SSIDB))
            {
                using (SqlCommand cmd = new SqlCommand("[SYSTEM].[spUPLOAD_ADVISERS_TO_ONLINE]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void DOWNLOAD_MISSING_STUDENT_FOR_SECTIONING()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spINSERT_STUDENT_IN_SECTIONING]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }



        //05-14-2016
        //INSERT NEW RECORD ON TEACHER SECTION
        public void INSERT_TEACHERSECTION(string _sy, string _levelcode, string _sectioncode, int _roomid, string _teacherid,
                                        string _scheddesc, string _buildingdedesc, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spInsert_TEACHER_SECTION_LIST]", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cmd.Parameters.AddWithValue("@LEVELCODE", _levelcode);
                    cmd.Parameters.AddWithValue("@SECTIONCODE", _sectioncode);
                    cmd.Parameters.AddWithValue("@ROOMID", _roomid);
                    cmd.Parameters.AddWithValue("@TEACHERID", _teacherid);
                    cmd.Parameters.AddWithValue("@SCHEDDESC", _scheddesc);
                    cmd.Parameters.AddWithValue("@BUILDINGDESC", _buildingdedesc);
                    cmd.Parameters.AddWithValue("@USERID", _userid);


                    cmd.ExecuteNonQuery();
                }
            }

        }

        //05-23-2016
        //UPDATE TEACHER SECTION
        public void UPDATE_TEACHERSECTION(string _sy, string _levelcode, string _sectioncode, int _roomid, string _teacherid,
                                        string _scheddesc, string _buildingdedesc, string _userid, int _id)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spUPDATE_TEACHER_SECTION_LIST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cmd.Parameters.AddWithValue("@LEVELCODE", _levelcode);
                    cmd.Parameters.AddWithValue("@SECTIONCODE", _sectioncode);
                    cmd.Parameters.AddWithValue("@ROOMID", _roomid);
                    cmd.Parameters.AddWithValue("@TEACHERID", _teacherid);
                    cmd.Parameters.AddWithValue("@SCHEDDESC", _scheddesc);
                    cmd.Parameters.AddWithValue("@BUILDINGDESC", _buildingdedesc);
                    cmd.Parameters.AddWithValue("@USERID", _userid);
                    cmd.Parameters.AddWithValue("@ID", _id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //UPDATE CLASS SCHEDULE TEACHER

        public void UPDATE_SCHED_DESC_TEACHER(int _schedID, string _teacherID, string _schedDesc, string _userID)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spUPDATE_SCHED_DESC_TEACHER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SCHEDID", _schedID);
                    cmd.Parameters.AddWithValue("@TEACHERID", _teacherID);
                    cmd.Parameters.AddWithValue("@SCHED_DESC", _schedDesc);
                    cmd.Parameters.AddWithValue("@USERID", _userID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion



        #region "GET/DISPLAY/CHECK FUNCTION AREA"


        public bool CHECK_STUDENTSTATUSEXIST(string _studnum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spCheckStudentStatus", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        x = true;

                        while (dr.Read())
                        {
                            STATCODE = dr["Statcode"].ToString();
                        }

                    }
                    else
                    {
                        x = false;
                    }
                }

                return x;
            }

        }

        //GET STUDENTS FROM STUDENT INFORMATION FOR STATUS ASSIGNMENT
        public DataTable GET_STUDENTS_NO_SECTION()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spGET_STUDENT_NO_SECTION_YET]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GET_STUDENTS_WITH_SECTION(string addlcond = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spGET_STUDENT_WITH_SECTION]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@addlcond", addlcond);
                    cmd.Parameters.AddWithValue("@order", order);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GET_TEACHER_SECTION_LIST(string _sy)
        {

            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spGET_TEACHER_SECTION_LIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SY", _sy);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GET_TEACHER_SECTION_LIST(string select = null, string filter = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spGET_TEACHER_SECTION_LIST]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@filter", filter);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }


        #endregion


        public bool CHECK_EXIST_TAG_ADDL_INFO(string sy, string studnum, string typecode, string statcode)
        {
            bool res = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCHECK_EXIST_TAG_ADDL_INFO", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_sy", sy);
                    cmd.Parameters.AddWithValue("@p_studnum", studnum);
                    cmd.Parameters.AddWithValue("@p_typecode", typecode);
                    cmd.Parameters.AddWithValue("@p_statcode", statcode);
                    res = (bool)cmd.ExecuteScalar();
                }
            }

            return res;
        }

        public void SaveTaggingStatus(string sy, string studNum, string typeCode, string statCode, DateTime datePref, DateTime dateFile, string reason, string desc, int status, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spSAVE_STUD_TAGGING_STATUS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    cmd.Parameters.AddWithValue("@typeCode", typeCode);
                    cmd.Parameters.AddWithValue("@statCode", statCode);
                    cmd.Parameters.AddWithValue("@datePref", datePref);
                    cmd.Parameters.AddWithValue("@dateFile", dateFile);
                    cmd.Parameters.AddWithValue("@reason", reason);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public class StudentSubjectClass : cBase
    {
        public DataTable GET_SSC_SUBJECT_TEACHER()
        {
            DataTable dt = new DataTable();
            string strSQL = "Registration.spGET_SSC_SUBJECT_TEACHER";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;

        }

        public bool CHECK_STUDENT_SUBJECT_CLASS_EXIST(string _studnum, string _classcode)
        {

            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spCHECK_STUDENT_SUBJECT_CLASS]", cn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@CLASSCODE", _classcode);

                    cn.Open();

                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;

        }

        public bool CHECK_STUDENT_CLASS_EXIST(string _studnum)
        {

            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spCHECK_STUDENT_CLASS_EXIST]", cn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);


                    cn.Open();

                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;

        }

        public DataTable GET_SSC_STUDENT_LEVEL_SECTION_LISTED(string _levelcode, string _section)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Registration].[spGET_STUDENT_LEVEL_SECTION_LISTED]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LEVELCODE", _levelcode);
                    cmd.Parameters.AddWithValue("@SECTION", _section);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }


        public void DELETE_STUDENT_SUBJECT_CLASS(string _studnum, string _classCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("Registration.spDELETE_STUDENT_SUBJECT_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@CLASSCODE", _classCode);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DELETE_STUDENT_SUBJECT_CLASS_ALL(string _studnum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("Registration.spDELETE_STUDENT_SUBJECT_CLASS_ALL", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void INSERT_STUDENT_SUBJECT_CLASS(string _studnum, string _levelcode, string _section, string _classcode, string _subjcode, string _teachercode, string _sy, int _sem, int _term, string _usercode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Registration].[spINSERT_STUDENT_SUBJECT_CLASS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@STUDNUM", _studnum);
                    cmd.Parameters.AddWithValue("@LEVELCODE", _levelcode);
                    cmd.Parameters.AddWithValue("@SECTION", _section);
                    cmd.Parameters.AddWithValue("@CLASSCODE", _classcode);
                    cmd.Parameters.AddWithValue("@SUBJCODE", _subjcode);
                    cmd.Parameters.AddWithValue("@TEACHERCODE", _teachercode);
                    cmd.Parameters.AddWithValue("@SY", _sy);
                    cmd.Parameters.AddWithValue("@SEM", _sem);
                    cmd.Parameters.AddWithValue("@TERM", _term);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);


                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public class SubjectClass : cBase
    {
        #region "PROPERTIES"
        public int Id { get; set; }
        public string ClassCode { get; set; }
        public string ClassDesc { get; set; }
        public string SY { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public string StudentNum { get; set; }
        public int TotalEnrolled { get; set; }
        #endregion

        public static DataTable GetStudentList(string select, int subj_id = 0, string where = null, string order = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_SUBJ_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subj_id", subj_id);
                    cmd.Parameters.AddWithValue("@select", select);
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

        public static DataTable GetStudentList2(string classCode)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_SUBJ_CLASS2", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classCode", classCode);
     
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static DataTable GetSubjectClass(string select, int subj_id, string where = null, string group = null, string order = null, string where_2 = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_SUBJ_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subj_id", subj_id);
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@where_2", where_2);
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

        public static DataTable GetNotEnrolledList(int subj_id, string select, string where = null, string order = null, string where_2 = null, string semsql = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_SUBJ_CLASS_NOT_ENROLLED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subj_id", subj_id);
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@where_2", where_2);
                    cmd.Parameters.AddWithValue("@order", order);
                    cmd.Parameters.AddWithValue("@semsql", semsql);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static void EnrollStudent(SubjectClass subjclass)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spINSERT_SUBJ_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@class_code", subjclass.ClassCode);
                    cmd.Parameters.AddWithValue("@class_desc", subjclass.ClassDesc);
                    cmd.Parameters.AddWithValue("@sy", subjclass.SY);
                    cmd.Parameters.AddWithValue("@subj_id", subjclass.SubjectId);
                    cmd.Parameters.AddWithValue("@tchr_id", subjclass.TeacherId);
                    cmd.Parameters.AddWithValue("@stud_no", subjclass.StudentNum);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UnenrollStudent(SubjectClass subjclass)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spDELETE_SUBJ_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "STUD");
                    cmd.Parameters.AddWithValue("@class_code", subjclass.ClassCode);
                    cmd.Parameters.AddWithValue("@stud_num", subjclass.StudentNum);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteClass(string class_code)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spDELETE_SUBJ_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@type", "CLASS");
                    cmd.Parameters.AddWithValue("@class_code", class_code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void LockClass(string class_code)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Registration.Subj_Class_RF SET flag_lock = 1, sys_dt_upd = GETDATE() WHERE class_code = @class_code", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@class_code", class_code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UnLockClass(string class_code)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Registration.Subj_Class_RF SET flag_lock = 0, sys_dt_upd = GETDATE() WHERE class_code = @class_code", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@class_Code", class_code);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static SubjectClass GetClassDescription(SubjectClass sclass)
        {
            SubjectClass subjclass = new SubjectClass();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spGET_SUBJ_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subj_id", sclass.SubjectId);
                    cmd.Parameters.AddWithValue("@select", "class.class_code, class.class_desc, class.subj_id, COUNT(stud.StudNum) AS count_stud");
                    cmd.Parameters.AddWithValue("@where", "WHERE class.class_code = '" + sclass.ClassCode + "'");
                    cmd.Parameters.AddWithValue("@order", "ORDER BY class.class_code");
                    cmd.Parameters.AddWithValue("@group", "GROUP BY class.class_code, class.class_desc, class.subj_id");
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            subjclass = new SubjectClass
                            {
                                ClassCode = sclass.ClassCode,
                                ClassDesc = dr["class_desc"].ToString(),
                                SubjectId = Convert.ToInt32(dr["subj_id"].ToString()),
                                TotalEnrolled = Convert.ToInt32(dr["count_stud"].ToString())
                            };
                        }
                    }
                }
            }

            return subjclass;
        }

        public static int SectionsCount(string classCode)
        {
            var sql = @"SELECT ISNULL(COUNT(DISTINCT section), 0) as Count 
	                    FROM Registration.Subj_Class_RF  as Class
	                    INNER JOIN Registration.StudentSectioning_MF as Student on Student.Studnum = Class.stud_no  
	                    WHERE class_code = @classCode and student.sy IN (SELECT SYDesc FROM xSystem.SchoolYear_RF WHERE Status = 1 AND isGrade = 1);";

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            return int.Parse(dr["Count"].ToString());
                        }
                        else
                            return 0;
                    }
                }
            }
        }

        public static void TransferClass(string studNum, string classCode, string refClassCode, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spTRANSFER_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    cmd.Parameters.AddWithValue("@refClassCode", refClassCode);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateClassDesc(string class_code, string class_desc)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spUPDATE_SUBJ_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@class_code", class_code);
                    cmd.Parameters.AddWithValue("@class_desc", class_desc);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool CheckSubjectClass(SubjectClass subjclass)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spCHECK_SUBJ_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@subj_id", subjclass.SubjectId);
                    cmd.Parameters.AddWithValue("@stud_no", subjclass.StudentNum);
                    r = (bool)cmd.ExecuteScalar();
                }
            }

            return r;
        }

        public static void DropClass(string classCode, string studNum, string userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Registration.spDROP_CLASS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classCode", classCode);
                    cmd.Parameters.AddWithValue("@studNum", studNum);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
    }
}
