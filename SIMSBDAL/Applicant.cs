using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace SIMSBDAL
{

    public class ApplicantBASE : cBase
    {
        //Fields
        public bool _STATUS { get; set; }
        public string _APPNUM { get; set; }
        public string _APPLASTNAME { get; set; }
        public string _APPFIRSTNAME { get; set; }
        public string _APPMIDDLENAME { get; set; }
        public string _APPMI { get; set; }
        public string _APPSUFFIX { get; set; }
        public string _FULLNAME { get; set; }
        public string _FULLNAME2 { get; set; }

        public string _APPTYPE { get; set; }
        public string _LEVELTYPECODE { get; set; }
        public string _APPSTRANDCODE { get; set; }
        public string _LEVELDESC { get; set; }

        public DateTime _APPDATE { get; set; }
        public DateTime _DOB { get; set; }
        public double _AGE { get; set; }

        public string _RELIGIONCODE { get; set; }

        public string _APPPLACEOFBIRTH { get; set; }

        public string _CONTACTPERSON { get; set; }
        public string _HOMEPHONE { get; set; }
        public string _OFFICEPHONE { get; set; }
        public string _MOBILEPHONE { get; set; }
        public string _ADDRESS { get; set; }
        public string _CITYCODE { get; set; }
        public string _BARANGGAYCODE { get; set; }
        public string _EMAIL { get; set; }
        public string _REMARKS { get; set; }

        public bool _WAITLISTED { get; set; }
        public bool _EMPLOYEECHILD { get; set; }
        public int _EMPLOYEEID { get; set; }
        public string _EMPLOYEENUM { get; set; }
        public string _EMPLOYEENAME { get; set; }
        public bool _NOT_INTERESTED { get; set; }
        public bool _NOT_ACCEPTED { get; set; }
        public bool _BACKOUT { get; set; }
        public string _VOUCHER_CODE { get; set; }
        public bool _FROM_FOREIGN_SCHOOL { get; set; }
        public string _PREV_SCHOOL { get; set; }
        public string _LOCAL_SCHOOL_TYPE { get; set; }
 

        //CREDENTIALS
        public bool _FORM138 { get; set; }
        public bool _BIRTHCERTIFICATE { get; set; }
        public bool _PSA { get; set; }
        public bool _NSO { get; set; }
        public bool _NSO_Orig { get; set; }
        public bool _NSO_Pcopy { get; set; }
        public bool _CR { get; set; }
        public bool _CR_Orig { get; set; }
        public bool _CR_Pcopy { get; set; }
        public bool _COLORED1X1 { get; set; }
        public bool _BROWNENVELOPE { get; set; }
        public bool _GOODMORAL { get; set; }
        public bool _RECOMMENDATION { get; set; }
        public bool _FORM137 { get; set; }
        public bool _NCAE { get; set; }
        public bool _INTERVIEW { get; set; }
        public bool _WITH_REQUEST_LETTER { get; set; }
        public string _OTHERCREDENTIALS { get; set; }
        public bool _PASSPORT { get; set; }
        public bool _STUDENT_VISA { get; set; }
        public bool _MED_CERT { get; set; }

        //PREVIOUS GRADE DETAILS
        public double _ENG1 { get; set; }
        public double _ENG2 { get; set; }
        public double _ENG3 { get; set; }
        public double _ENG4 { get; set; }
        public double _ENGTOTAL { get; set; }

        public double _MAT1 { get; set; }
        public double _MAT2 { get; set; }
        public double _MAT3 { get; set; }
        public double _MAT4 { get; set; }
        public double _MATTOTAL { get; set; }

        public double _SCI1 { get; set; }
        public double _SCI2 { get; set; }
        public double _SCI3 { get; set; }
        public double _SCI4 { get; set; }
        public double _SCITOTAL { get; set; }

        public double _Q1 { get; set; }
        public double _Q2 { get; set; }
        public double _Q3 { get; set; }
        public double _Q4 { get; set; }
        public double _PREVFINALGRADE { get; set; }

        public double _OVERRIDEFINALGRADE { get; set; }

        public bool _ISLOWENG { get; set; }
        public bool _ISLOWSCI { get; set; }
        public bool _ISLOWMATH { get; set; }

        //SHORT MONTH
        public double _MONTHSHORT { get; set; }
        public bool _ISMONTHSHORT { get; set; }
        
        //OVERRIDE PREVIOUS GRADE
        public double _OVERRIDEPREVGRADE { get; set; }
        public bool _ISOVERRIDEPREVGRADE { get; set; }


        public double _PREVGRADE { get; set; }
        public string _EXAMSCHED { get; set; }
        public string _INTSCHED { get; set; }
        public string _AGEBYJUNE { get; set; }
        public string _GENDERCODE { get; set; }
        public bool _RETESTSTATUS { get; set; }

        //FOR SAP STUDENT NAME COMBINATION
        public string _SAP_FN_FORMAT { get; set; }

        public string _LEVELCATCODE { get; set; }
        //For Retrieval of Testing Record
        public string _SCORES { get; set; }
        public string _RESULT { get; set; }

        //SUMMARY Retrieval
        public string _PREV { get; set; }
        public string _PREVRESULT { get; set; }
        public string _ASSESSMENT { get; set; }
        public string _OVERALL { get; set; }
        public string _OBSERVATION { get; set; }
        public string _STATCODE { get; set; }
        public bool _RETEST { get; set; }


        //RESULT SLIP Retrieval 02/16/2016
        public DateTime _DATECREATED { get; set; }
        public string _ADDRESSTO { get; set; }
        public DateTime _DATEEXPIRED { get; set; }
        public int _RESULTTYPE { get; set; }
    
    }

    public class Applicant: ApplicantBASE
    {
        public static DataTable GetData(string select = null, string where = null, string group = null, string order = null, string limit = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_APPLICANTS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", select);
                    cmd.Parameters.AddWithValue("@where", where);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", order);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    using(SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public static Applicant GetInfo(string appNum)
        {
            Applicant appl = new Applicant();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Admission.spGET_APPLICANTS", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", "appl.*,cred.Form138,cred.BC,cred.BC_PSA,cred.BC_NSO,cred.BC_NSO_Orig,cred.BC_NSO_Pcopy,cred.BC_CR,cred.BC_CR_Orig,BC_CR_Pcopy,cred.Colored1x1,cred.BrownEnvelope,cred.GM,cred.Recommendation,cred.Form137,cred.NCAE,cred.Interview,cred.WithReqLetter,cred.Other,cred.Passport,cred.StudVisa,cred.medCert," +
                        "lvl.LevelTypeDesc,strand.StrandName,city.CityDesc,brgy.BarangayDesc,test.STATCODE,gender.GenderDesc,parent.emp_id,emp.EmpNum,emp.LastName AS empLname,emp.FirstName AS empFname,emp.MiddleName AS empMname"+
                        ", grd.EngTotal,grd.SciTotal,grd.MathTotal,grd.FirstTotal,grd.SecondTotal,grd.ThirdTotal,grd.FourthTotal,grd.Eng1,grd.Eng2,grd.Eng3,grd.Eng4,grd.Sci1,grd.Sci2,grd.Sci3,grd.Sci4,grd.Math1,grd.Math2,grd.Math3,grd.Math4,grd.LowerEng,grd.LowerSci,grd.LowerMath,grd.FinalAverage,grd.FinalAverage2,grd.OverrideAverage");
                    cmd.Parameters.AddWithValue("@where", string.Format("WHERE appl.AppNum = '{0}'", appNum));
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            appl = new Applicant();
                            //{
                            appl._APPNUM = dr["AppNum"].ToString();
                            appl._APPDATE = Convert.ToDateTime(dr["AppDOA"].ToString());
                            appl._APPTYPE = dr["AppTypeCode"].ToString();
                            appl._LEVELTYPECODE = dr["LevelTypeCode"].ToString();
                            appl._APPSTRANDCODE = dr["StrandCode"].ToString();
                            appl._APPLASTNAME = dr["LastName"].ToString();
                            appl._APPFIRSTNAME = dr["FirstName"].ToString();
                            appl._APPMIDDLENAME = dr["MiddleName"].ToString();
                            appl._FULLNAME = dr["LastName"].ToString() + ", " + dr["FirstName"].ToString() + (string.IsNullOrEmpty(dr["Middlename"].ToString()) ? "" : " " + dr["MiddleName"].ToString().Substring(0, 1) + ".");
                            appl._GENDERCODE = dr["GenderCode"].ToString();
                            appl._DOB = Convert.ToDateTime(dr["DOB"].ToString());
                            appl._APPPLACEOFBIRTH = dr["POB"].ToString();
                            appl._RELIGIONCODE = dr["ReligionCode"].ToString();
                            appl._CONTACTPERSON = dr["ContactPerson"].ToString();
                            appl._EMAIL = dr["Email"].ToString();
                            appl._HOMEPHONE = dr["TelNo"].ToString();
                            appl._OFFICEPHONE = dr["OfficeNo"].ToString();
                            appl._MOBILEPHONE = dr["MobileNo"].ToString();
                            appl._ADDRESS = dr["AddInfo"].ToString();
                            appl._CITYCODE = dr["CityCode"].ToString();
                            appl._BARANGGAYCODE = dr["BarangayCode"].ToString();
                            appl._WAITLISTED = Convert.ToBoolean(dr["WLStatus"]);
                            appl._EMPLOYEECHILD = Convert.ToBoolean(dr["SSIChild"]);
                            appl._EMPLOYEEID = (!Convert.ToBoolean(dr["SSIChild"]) ? 0 : Convert.ToInt32(dr["emp_id"]));
                            appl._EMPLOYEENUM = dr["EmpNum"].ToString();
                            appl._EMPLOYEENAME = dr["empLname"].ToString() + ", " + dr["empFname"].ToString() + (string.IsNullOrEmpty(dr["empMname"].ToString()) ? "" : " " + dr["empMname"].ToString().Substring(0, 1) + ".");
                            appl._NOT_ACCEPTED = Convert.ToBoolean(dr["AppNotAccepted"]);
                            appl._NOT_INTERESTED = Convert.ToBoolean(dr["AppNotInterested"]);
                            appl._BACKOUT = Convert.ToBoolean(dr["AppBackOut"]);
                            appl._STATUS = Convert.ToBoolean(dr["Status"]);
                            appl._ISMONTHSHORT = Convert.ToBoolean(dr["ShortByJune"]);
                            appl._MONTHSHORT = Convert.ToDouble(dr["ShortMonth"]);
                            appl._STATCODE = dr["STATCODE"].ToString();
                            appl._REMARKS = dr["Remarks"].ToString();
                            appl._VOUCHER_CODE = dr["VouCode"].ToString();
                            appl._FROM_FOREIGN_SCHOOL = Convert.ToBoolean(dr["IsFromForeignSch"]);
                            appl._PREV_SCHOOL = dr["prevSch"].ToString();
                            appl._LOCAL_SCHOOL_TYPE = dr["localSchType"].ToString();
                            //CREDENTIALS
                            appl._FORM138 = Convert.ToBoolean(dr["FORM138"]);
                            appl._BIRTHCERTIFICATE = Convert.ToBoolean(dr["BC"]);
                            appl._NSO = Convert.ToBoolean(dr["BC_NSO"]);
                            appl._PSA = Convert.ToBoolean(dr["BC_PSA"]);
                            appl._NSO_Orig = Convert.ToBoolean(dr["BC_NSO_Orig"]);
                            appl._NSO_Pcopy = Convert.ToBoolean(dr["BC_NSO_Pcopy"]);
                            appl._CR = Convert.ToBoolean(dr["BC_CR"]);
                            appl._CR_Orig = Convert.ToBoolean(dr["BC_CR_Orig"]);
                            appl._CR_Pcopy = Convert.ToBoolean(dr["BC_CR_Pcopy"]);
                            appl._COLORED1X1 = Convert.ToBoolean(dr["Colored1x1"]);
                            appl._BROWNENVELOPE = Convert.ToBoolean(dr["BrownEnvelope"]);
                            appl._GOODMORAL = Convert.ToBoolean(dr["GM"]);
                            appl._RECOMMENDATION = Convert.ToBoolean(dr["Recommendation"]);
                            appl._FORM137 = Convert.ToBoolean(dr["FORM137"]);
                            appl._NCAE = Convert.ToBoolean(dr["NCAE"]);
                            appl._INTERVIEW = Convert.ToBoolean(dr["Interview"]);
                            appl._WITH_REQUEST_LETTER = Convert.ToBoolean(dr["WithReqLetter"]);
                            appl._OTHERCREDENTIALS = dr["Other"].ToString();
                            appl._PASSPORT = Convert.ToBoolean(dr["Passport"]);
                            appl._STUDENT_VISA = Convert.ToBoolean(dr["StudVisa"]);
                            appl._MED_CERT = Convert.ToBoolean(dr["medCert"]);
                            //PREVIOUS GRADES
                            appl._ENG1 = Convert.ToDouble(dr["ENG1"]);
                            appl._ENG2 = Convert.ToDouble(dr["ENG2"]);
                            appl._ENG3 = Convert.ToDouble(dr["ENG3"]);
                            appl._ENG4 = Convert.ToDouble(dr["ENG4"]);
                            appl._ENGTOTAL = Convert.ToDouble(dr["EngTotal"]);
                            appl._MAT1 = Convert.ToDouble(dr["Math1"]);
                            appl._MAT2 = Convert.ToDouble(dr["Math2"]);
                            appl._MAT3 = Convert.ToDouble(dr["Math3"]);
                            appl._MAT4 = Convert.ToDouble(dr["Math4"]);
                            appl._MATTOTAL = Convert.ToDouble(dr["MathTotal"]);
                            appl._SCI1 = Convert.ToDouble(dr["Sci1"]);
                            appl._SCI2 = Convert.ToDouble(dr["Sci2"]);
                            appl._SCI3 = Convert.ToDouble(dr["Sci3"]);
                            appl._SCI4 = Convert.ToDouble(dr["Sci4"]);
                            appl._SCITOTAL = Convert.ToDouble(dr["SciTotal"]);
                            appl._Q1 = Convert.ToDouble(dr["FirstTotal"]);
                            appl._Q2 = Convert.ToDouble(dr["SecondTotal"]);
                            appl._Q3 = Convert.ToDouble(dr["ThirdTotal"]);
                            appl._Q4 = Convert.ToDouble(dr["FourthTotal"]);
                            appl._PREVFINALGRADE = Convert.ToDouble(dr["FinalAverage"]);
                            appl._ISLOWENG = Convert.ToBoolean(dr["LowerEng"]);
                            appl._ISLOWSCI = Convert.ToBoolean(dr["LowerSci"]);
                            appl._ISLOWMATH = Convert.ToBoolean(dr["LowerMath"]);
                            appl._ISOVERRIDEPREVGRADE = Convert.ToBoolean(dr["OverrideAverage"]);
                            appl._OVERRIDEFINALGRADE = Convert.ToDouble(dr["FinalAverage2"]);
                            //};
                        }
                    }
                }
            }
            return appl;
        }








        public int retAppInfo { get; set; }
        public int retAppPrevGrade { get; set; }


        //Stored New Applicant Information
        public static void InsertData(Applicant appl, string user_id, string sy)
        {
            appl.NEW_APPLICANT(sy, appl._APPTYPE, appl._LEVELTYPECODE, appl._APPSTRANDCODE, appl._APPDATE, appl._APPNUM, appl._WAITLISTED, appl._EMPLOYEECHILD, appl._APPLASTNAME, appl._APPFIRSTNAME, appl._APPMIDDLENAME
                , appl._APPMI, appl._APPSUFFIX, appl._FULLNAME, appl._FULLNAME2, appl._GENDERCODE, appl._DOB, appl._APPPLACEOFBIRTH, appl._AGE, Convert.ToDouble(appl._AGEBYJUNE), false, 0
                , appl._RELIGIONCODE, appl._HOMEPHONE, appl._OFFICEPHONE, appl._MOBILEPHONE, appl._CONTACTPERSON, appl._ADDRESS, appl._BARANGGAYCODE, appl._CITYCODE, appl._EMAIL, appl._REMARKS, true, false, DateTime.Now, DateTime.Now, user_id, appl._VOUCHER_CODE, appl._FROM_FOREIGN_SCHOOL, appl._PREV_SCHOOL, appl._LOCAL_SCHOOL_TYPE, appl._FORM138
                , appl._BIRTHCERTIFICATE, appl._PSA, appl._NSO, appl._NSO_Orig, appl._NSO_Pcopy, appl._CR, appl._CR_Orig, appl._CR_Pcopy, appl._COLORED1X1, appl._BROWNENVELOPE, appl._GOODMORAL, appl._RECOMMENDATION, appl._FORM137, appl._NCAE, appl._WITH_REQUEST_LETTER, appl._OTHERCREDENTIALS, appl._PASSPORT, appl._STUDENT_VISA, appl._MED_CERT, appl._ENGTOTAL, appl._SCITOTAL, appl._MATTOTAL
                , appl._Q1, appl._Q2, appl._Q3, appl._Q4, appl._ENG1, appl._ENG2, appl._ENG3, appl._ENG4, appl._SCI1, appl._SCI2, appl._SCI3, appl._SCI4, appl._MAT1, appl._MAT2, appl._MAT3, appl._MAT4, appl._ISLOWENG, appl._ISLOWSCI, appl._ISLOWMATH, appl._PREVFINALGRADE, appl._OVERRIDEFINALGRADE, appl._ISOVERRIDEPREVGRADE
                , appl._EMPLOYEEID);
        }

        public static void UpdateData(Applicant appl, string user_id, string sy)
        {
            appl.UPDATE_APPLICANT(appl._APPTYPE, appl._LEVELTYPECODE, appl._APPSTRANDCODE, appl._APPDATE, appl._APPNUM, appl._WAITLISTED, appl._EMPLOYEECHILD, appl._APPLASTNAME, appl._APPFIRSTNAME, appl._APPMIDDLENAME, appl._APPMI
                , appl._APPSUFFIX, appl._FULLNAME, appl._FULLNAME2, appl._GENDERCODE, appl._DOB, appl._APPPLACEOFBIRTH, appl._AGE, Convert.ToDouble(appl._AGEBYJUNE), false, 0, appl._RELIGIONCODE, appl._HOMEPHONE, appl._OFFICEPHONE, appl._MOBILEPHONE, appl._CONTACTPERSON
                , appl._ADDRESS, appl._BARANGGAYCODE, appl._CITYCODE, appl._EMAIL, appl._REMARKS, appl._STATUS, appl._BACKOUT, DateTime.Now, user_id, appl._VOUCHER_CODE, appl._FROM_FOREIGN_SCHOOL, appl._PREV_SCHOOL, appl._LOCAL_SCHOOL_TYPE, appl._FORM138, appl._BIRTHCERTIFICATE, appl._NSO, appl._PSA, appl._NSO_Orig, appl._NSO_Pcopy
                , appl._CR, appl._CR_Orig, appl._CR_Pcopy, appl._COLORED1X1, appl._BROWNENVELOPE, appl._GOODMORAL, appl._RECOMMENDATION, appl._FORM137, appl._NCAE, appl._WITH_REQUEST_LETTER, appl._OTHERCREDENTIALS, appl._PASSPORT, appl._STUDENT_VISA, appl._MED_CERT, appl._ENGTOTAL, appl._SCITOTAL, appl._MATTOTAL
                , appl._Q1, appl._Q2, appl._Q3, appl._Q4, appl._ENG1, appl._ENG2, appl._ENG3, appl._ENG4, appl._SCI1, appl._SCI2, appl._SCI3, appl._SCI4, appl._MAT1, appl._MAT2, appl._MAT3, appl._MAT4
                , appl._ISLOWENG, appl._ISLOWSCI, appl._ISLOWMATH, appl._PREVFINALGRADE, appl._OVERRIDEFINALGRADE, appl._ISOVERRIDEPREVGRADE, appl._EMPLOYEEID, appl._NOT_INTERESTED, appl._NOT_ACCEPTED);
        }

        public void NEW_APPLICANT(string SY, string APPTYPECODE, string LEVELTYPECODE, string STRANDCODE, DateTime APPDOA, string APPNUM,
                                 bool WLSTATUS, bool SSICHILD, string LASTNAME, string FIRSTNAME, string MIDDLENAME,
                                 string MI, string SUFFIX, string FULLNAME, string FULLNAME2, string GENDERCODE, DateTime DOB, string POB,
                                 double AGE, double AGEONJUNE, bool SHORTBYJUNE, int SHORTMONTH, string RELIGIONCODE, string TELNO, string OFFICENO,
                                 string MOBILENO, string CONTACTPERSON, string ADDINFO, string BARANGAYCODE, string CITYCODE, string EMAIL,
                                 string REMARKS, bool STATUS, bool _APPBACKOUT,
                                 DateTime DATEENCODE, DateTime DATEUPDATE, string USERCODE, string VouCode, bool IsFromForeignSch, string prevSch, string localSchType,
                                 bool FORM138, bool BC, bool BC_PSA, bool BC_NSO, bool BC_NSO_Orig, bool BC_NSO_Pcopy, bool BC_CR, bool BC_CR_Orig, bool BC_CR_Pcopy, bool COLORED1X1, bool BROWNENVELOPE, bool GM, bool RECOMMENDATION, bool FORM137, bool NCAE, bool WithReqLetter, string OTHER, bool Passport, bool StudVisa, bool medCert,
                                 double ENGTOTAL, double SCITOTAL, double MATHTOTAL, double FIRSTTOTAL, double SECONDTOTAL, double THIRDTOTAL, double FOURTHTOTAL,
                                 double ENG1, double ENG2, double ENG3, double ENG4, double SCI1, double SCI2, double SCI3, double SCI4,
                                 double MATH1, double MATH2, double MATH3, double MATH4,   
                                 bool LOWERENG, bool LOWERSCI, bool LOWERMATH, double FINALAVERAGE, double FINALAVERAGE2, bool OVERRIDEAVERAGE,
                                 int emp_id = 0)
       
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {



                using (SqlCommand cmd = new SqlCommand("spInsert_APPLICANT_INFORMATION", cn))
                {


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EMP_ID", emp_id);
                    cmd.Parameters.AddWithValue("@AppNum", APPNUM);

                    cmd.Parameters.AddWithValue("@SY", SY);
                    cmd.Parameters.AddWithValue("@AppTypeCode", APPTYPECODE);
                    cmd.Parameters.AddWithValue("@LevelTypeCode", LEVELTYPECODE);
                    cmd.Parameters.AddWithValue("@StrandCode", STRANDCODE);
                    cmd.Parameters.AddWithValue("@AppDOA", APPDOA);
                    cmd.Parameters.AddWithValue("@WLStatus", WLSTATUS);
                    cmd.Parameters.AddWithValue("@SSIChild", SSICHILD);
                    cmd.Parameters.AddWithValue("@LastName", LASTNAME);
                    cmd.Parameters.AddWithValue("@FirstName", FIRSTNAME);
                    cmd.Parameters.AddWithValue("@MiddleName", MIDDLENAME);
                    cmd.Parameters.AddWithValue("@MI", MI);
                    cmd.Parameters.AddWithValue("@Suffix", SUFFIX);
                    cmd.Parameters.AddWithValue("@FullName", FULLNAME);
                    cmd.Parameters.AddWithValue("@FullName2", FULLNAME2);
                    cmd.Parameters.AddWithValue("@GenderCode", GENDERCODE);
                    cmd.Parameters.AddWithValue("@DOB", DOB);
                    cmd.Parameters.AddWithValue("@POB", POB);
                    cmd.Parameters.AddWithValue("@Age", AGE);
                    cmd.Parameters.AddWithValue("@AgeOnJune", AGEONJUNE);
                    cmd.Parameters.AddWithValue("@ShortByJune", SHORTBYJUNE);
                    cmd.Parameters.AddWithValue("@ShortMonth", SHORTMONTH);
                    cmd.Parameters.AddWithValue("@religionCode", RELIGIONCODE);
                    cmd.Parameters.AddWithValue("@TelNo", TELNO);
                    cmd.Parameters.AddWithValue("@OfficeNo", OFFICENO);
                    cmd.Parameters.AddWithValue("@MobileNo", MOBILENO);
                    cmd.Parameters.AddWithValue("@ContactPerson", CONTACTPERSON);
                    cmd.Parameters.AddWithValue("@AddInfo", ADDINFO);
                    cmd.Parameters.AddWithValue("@BarangayCode", BARANGAYCODE);
                    cmd.Parameters.AddWithValue("@CityCode", CITYCODE);
                    cmd.Parameters.AddWithValue("@Email", EMAIL);
                    cmd.Parameters.AddWithValue("@Remarks", REMARKS);
                    cmd.Parameters.AddWithValue("@Status", STATUS);
                    cmd.Parameters.AddWithValue("@APPBACKOUT", _APPBACKOUT);
                    cmd.Parameters.AddWithValue("@DateEncode", DATEENCODE);
                    cmd.Parameters.AddWithValue("@DateUpdate", DATEUPDATE);
                    cmd.Parameters.AddWithValue("@UserCode", USERCODE);
                    cmd.Parameters.AddWithValue("@VouCode", VouCode);
                    cmd.Parameters.AddWithValue("@IsFromForeignSch", IsFromForeignSch);
                    cmd.Parameters.AddWithValue("@prevSch", prevSch);
                    cmd.Parameters.AddWithValue("@localSchType", localSchType);

                    //CREDENTIALS
                    //cmd.Parameters.AddWithValue("@APPNUM_CREDENTIAL", APPNUM);
                    cmd.Parameters.AddWithValue("@Form138", FORM138);
                    cmd.Parameters.AddWithValue("@BC", BC);
                    cmd.Parameters.AddWithValue("@BC_PSA", BC_PSA);
                    cmd.Parameters.AddWithValue("@BC_NSO", BC_NSO);
                    cmd.Parameters.AddWithValue("@BC_NSO_Orig", BC_NSO_Orig);
                    cmd.Parameters.AddWithValue("@BC_NSO_Pcopy", BC_NSO_Pcopy);
                    cmd.Parameters.AddWithValue("@BC_CR", BC_CR);
                    cmd.Parameters.AddWithValue("@BC_CR_Orig", BC_CR_Orig);
                    cmd.Parameters.AddWithValue("@BC_CR_Pcopy", BC_CR_Pcopy);
                    cmd.Parameters.AddWithValue("@Colored1x1", COLORED1X1);
                    cmd.Parameters.AddWithValue("@BrownEnvelope", BROWNENVELOPE);
                    cmd.Parameters.AddWithValue("@GM", GM);
                    cmd.Parameters.AddWithValue("@Recommendation", RECOMMENDATION);
                    cmd.Parameters.AddWithValue("@Form137", FORM137);
                    cmd.Parameters.AddWithValue("@NCAE", NCAE);
                    cmd.Parameters.AddWithValue("@WithReqLetter", WithReqLetter);
                    cmd.Parameters.AddWithValue("@Other", OTHER);
                    cmd.Parameters.AddWithValue("@Passport", Passport);
                    cmd.Parameters.AddWithValue("@StudVisa", StudVisa);
                    cmd.Parameters.AddWithValue("@medCert", medCert);

                    ////PREVIOUS GRADE
                    ////cmd.Parameters.AddWithValue("@SNUM", SNUM);
                    cmd.Parameters.AddWithValue("@ENGTOTAL", ENGTOTAL);
                    cmd.Parameters.AddWithValue("@SCITOTAL", SCITOTAL);
                    cmd.Parameters.AddWithValue("@MATHTOTAL", MATHTOTAL);
                    cmd.Parameters.AddWithValue("@FIRSTTOTAL", FIRSTTOTAL);
                    cmd.Parameters.AddWithValue("@SECONDTOTAL", SECONDTOTAL);
                    cmd.Parameters.AddWithValue("@THIRDTOTAL", THIRDTOTAL);
                    cmd.Parameters.AddWithValue("@FOURTHTOTAL", FOURTHTOTAL);
                    cmd.Parameters.AddWithValue("@ENG1", ENG1);
                    cmd.Parameters.AddWithValue("@ENG2", ENG2);
                    cmd.Parameters.AddWithValue("@ENG3", ENG3);
                    cmd.Parameters.AddWithValue("@ENG4", ENG4);
                    cmd.Parameters.AddWithValue("@SCI1", SCI1);
                    cmd.Parameters.AddWithValue("@SCI2", SCI2);
                    cmd.Parameters.AddWithValue("@SCI3", SCI3);
                    cmd.Parameters.AddWithValue("@SCI4", SCI4);
                    cmd.Parameters.AddWithValue("@MATH1", MATH1);
                    cmd.Parameters.AddWithValue("@MATH2", MATH2);
                    cmd.Parameters.AddWithValue("@MATH3", MATH3);
                    cmd.Parameters.AddWithValue("@MATH4", MATH4);
                    cmd.Parameters.AddWithValue("@LOWERENG", LOWERENG);
                    cmd.Parameters.AddWithValue("@LOWERSCI", LOWERSCI);
                    cmd.Parameters.AddWithValue("@LOWERMATH", LOWERMATH);
                    cmd.Parameters.AddWithValue("@FINALAVERAGE", FINALAVERAGE);
                    cmd.Parameters.AddWithValue("@FINALAVERAGE2", FINALAVERAGE2);
                    cmd.Parameters.AddWithValue("@OVERRIDEAVERAGE", OVERRIDEAVERAGE);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    

                }
            }
         
        
        }

        //Update Applicant
        public void UPDATE_APPLICANT(string APPTYPECODE, string LEVELTYPECODE, string STRANDCODE, DateTime APPDOA, string APPNUM,
                                 bool WLSTATUS, bool SSICHILD, string LASTNAME, string FIRSTNAME, string MIDDLENAME,
                                 string MI, string SUFFIX, string FULLNAME, string FULLNAME2, string GENDERCODE, DateTime DOB, string POB,
                                 double AGE, double AGEONJUNE, bool SHORTBYJUNE, int SHORTMONTH, string RELIGIONCODE, string TELNO, string OFFICENO,
                                 string MOBILENO, string CONTACTPERSON, string ADDINFO, string BARANGAYCODE, string CITYCODE, string EMAIL,
                                 string REMARKS, bool STATUS, bool _APPBACKOUT,
                                 DateTime DATEUPDATE, string USERCODE, string VouCode, bool IsFromForeignSch, string prevSch, string localSchType,
                                 bool FORM138, bool BC, bool BC_PSA, bool BC_NSO, bool BC_NSO_Orig, bool BC_NSO_Pcopy, bool BC_CR, bool BC_CR_Orig, bool BC_CR_Pcopy, bool COLORED1X1, bool BROWNENVELOPE, bool GM, bool RECOMMENDATION, bool FORM137, bool NCAE, bool WithReqLetter, string OTHER, bool Passport, bool StudVisa, bool medCert,
                                 double ENGTOTAL, double SCITOTAL, double MATHTOTAL, double FIRSTTOTAL, double SECONDTOTAL, double THIRDTOTAL, double FOURTHTOTAL,
                                 double ENG1, double ENG2, double ENG3, double ENG4, double SCI1, double SCI2, double SCI3, double SCI4,
                                 double MATH1, double MATH2, double MATH3, double MATH4,
                                 bool LOWERENG, bool LOWERSCI, bool LOWERMATH, double FINALAVERAGE, double FINALAVERAGE2, bool OVERRIDEAVERAGE,
                                 int emp_id = 0, bool NOT_INTERESTED = false, bool NOT_ACCEPTED = false)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {



                using (SqlCommand cmd = new SqlCommand("spUPDATE_APPLICANT_INFORMATION", cn))
                {


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EMP_ID", emp_id);
                    cmd.Parameters.AddWithValue("@AppNum", APPNUM);

                    cmd.Parameters.AddWithValue("@AppTypeCode", APPTYPECODE);
                    cmd.Parameters.AddWithValue("@LevelTypeCode", LEVELTYPECODE);
                    cmd.Parameters.AddWithValue("@StrandCode", STRANDCODE);
                    cmd.Parameters.AddWithValue("@AppDOA", APPDOA);
                    cmd.Parameters.AddWithValue("@WLStatus", WLSTATUS);
                    cmd.Parameters.AddWithValue("@SSIChild", SSICHILD);
                    cmd.Parameters.AddWithValue("@LastName", LASTNAME);
                    cmd.Parameters.AddWithValue("@FirstName", FIRSTNAME);
                    cmd.Parameters.AddWithValue("@MiddleName", MIDDLENAME);
                    cmd.Parameters.AddWithValue("@MI", MI);
                    cmd.Parameters.AddWithValue("@Suffix", SUFFIX);
                    cmd.Parameters.AddWithValue("@FullName", FULLNAME);
                    cmd.Parameters.AddWithValue("@FullName2", FULLNAME2);
                    cmd.Parameters.AddWithValue("@GenderCode", GENDERCODE);
                    cmd.Parameters.AddWithValue("@DOB", DOB);
                    cmd.Parameters.AddWithValue("@POB", POB);
                    cmd.Parameters.AddWithValue("@Age", AGE);
                    cmd.Parameters.AddWithValue("@AgeOnJune", AGEONJUNE);
                    cmd.Parameters.AddWithValue("@ShortByJune", SHORTBYJUNE);
                    cmd.Parameters.AddWithValue("@ShortMonth", SHORTMONTH);
                    cmd.Parameters.AddWithValue("@religionCode", RELIGIONCODE);
                    cmd.Parameters.AddWithValue("@TelNo", TELNO);
                    cmd.Parameters.AddWithValue("@OfficeNo", OFFICENO);
                    cmd.Parameters.AddWithValue("@MobileNo", MOBILENO);
                    cmd.Parameters.AddWithValue("@ContactPerson", CONTACTPERSON);
                    cmd.Parameters.AddWithValue("@AddInfo", ADDINFO);
                    cmd.Parameters.AddWithValue("@BarangayCode", BARANGAYCODE);
                    cmd.Parameters.AddWithValue("@CityCode", CITYCODE);
                    cmd.Parameters.AddWithValue("@Email", EMAIL);
                    cmd.Parameters.AddWithValue("@Remarks", REMARKS);
                    cmd.Parameters.AddWithValue("@Status", STATUS);
                    cmd.Parameters.AddWithValue("@APPBACKOUT", _APPBACKOUT);
                    cmd.Parameters.AddWithValue("@DateUpdate", DATEUPDATE);
                    cmd.Parameters.AddWithValue("@UserCode", USERCODE);
                    cmd.Parameters.AddWithValue("@VouCode", VouCode);
                    cmd.Parameters.AddWithValue("@IsFromForeignSch", IsFromForeignSch);
                    cmd.Parameters.AddWithValue("@prevSch", prevSch);
                    cmd.Parameters.AddWithValue("@localSchType", localSchType);

                    //CREDENTIALS
                    
                    cmd.Parameters.AddWithValue("@Form138", FORM138);
                    cmd.Parameters.AddWithValue("@BC", BC);
                    cmd.Parameters.AddWithValue("@BC_PSA", BC_PSA);
                    cmd.Parameters.AddWithValue("@BC_NSO", BC_NSO);
                    cmd.Parameters.AddWithValue("@BC_NSO_Orig", BC_NSO_Orig);
                    cmd.Parameters.AddWithValue("@BC_NSO_Pcopy", BC_NSO_Pcopy);
                    cmd.Parameters.AddWithValue("@BC_CR", BC_CR);
                    cmd.Parameters.AddWithValue("@BC_CR_Orig", BC_CR_Orig);
                    cmd.Parameters.AddWithValue("@BC_CR_Pcopy", BC_CR_Pcopy);
                    cmd.Parameters.AddWithValue("@Colored1x1", COLORED1X1);
                    cmd.Parameters.AddWithValue("@BrownEnvelope", BROWNENVELOPE);
                    cmd.Parameters.AddWithValue("@GM", GM);
                    cmd.Parameters.AddWithValue("@Recommendation", RECOMMENDATION);
                    cmd.Parameters.AddWithValue("@Form137", FORM137);
                    cmd.Parameters.AddWithValue("@NCAE", NCAE);
                    cmd.Parameters.AddWithValue("@WithReqLetter", WithReqLetter);
                    cmd.Parameters.AddWithValue("@Other", OTHER);
                    cmd.Parameters.AddWithValue("@Passport", Passport);
                    cmd.Parameters.AddWithValue("@StudVisa", StudVisa);
                    cmd.Parameters.AddWithValue("@medCert", medCert);

                    //PREVIOUS GRADE
                    cmd.Parameters.AddWithValue("@ENGTOTAL", ENGTOTAL);
                    cmd.Parameters.AddWithValue("@SCITOTAL", SCITOTAL);
                    cmd.Parameters.AddWithValue("@MATHTOTAL", MATHTOTAL);
                    cmd.Parameters.AddWithValue("@FIRSTTOTAL", FIRSTTOTAL);
                    cmd.Parameters.AddWithValue("@SECONDTOTAL", SECONDTOTAL);
                    cmd.Parameters.AddWithValue("@THIRDTOTAL", THIRDTOTAL);
                    cmd.Parameters.AddWithValue("@FOURTHTOTAL", FOURTHTOTAL);
                    cmd.Parameters.AddWithValue("@ENG1", ENG1);
                    cmd.Parameters.AddWithValue("@ENG2", ENG2);
                    cmd.Parameters.AddWithValue("@ENG3", ENG3);
                    cmd.Parameters.AddWithValue("@ENG4", ENG4);
                    cmd.Parameters.AddWithValue("@SCI1", SCI1);
                    cmd.Parameters.AddWithValue("@SCI2", SCI2);
                    cmd.Parameters.AddWithValue("@SCI3", SCI3);
                    cmd.Parameters.AddWithValue("@SCI4", SCI4);
                    cmd.Parameters.AddWithValue("@MATH1", MATH1);
                    cmd.Parameters.AddWithValue("@MATH2", MATH2);
                    cmd.Parameters.AddWithValue("@MATH3", MATH3);
                    cmd.Parameters.AddWithValue("@MATH4", MATH4);
                    cmd.Parameters.AddWithValue("@LOWERENG", LOWERENG);
                    cmd.Parameters.AddWithValue("@LOWERSCI", LOWERSCI);
                    cmd.Parameters.AddWithValue("@LOWERMATH", LOWERMATH);
                    cmd.Parameters.AddWithValue("@FINALAVERAGE", FINALAVERAGE);
                    cmd.Parameters.AddWithValue("@FINALAVERAGE2", FINALAVERAGE2);
                    cmd.Parameters.AddWithValue("@OVERRIDEAVERAGE", OVERRIDEAVERAGE);

                    // ### 2019.06.18 ###
                    // ### added parameters ####
                    cmd.Parameters.AddWithValue("@NOTINTERESTED", NOT_INTERESTED);
                    cmd.Parameters.AddWithValue("@NOTACCEPTED", NOT_ACCEPTED);
                    // #########################

                    cn.Open();
                    cmd.ExecuteNonQuery();


                }
            }
         
        
        }


        //CHECK APPLICANT ENTRY IF EXIST
        public bool EXIST_APPLICANT(string LASTNAME, string FIRSTNAME, string MI)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spCHECK_APPLICANT_EXIST", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LASTNAME", LASTNAME);
                    cmd.Parameters.AddWithValue("@FIRSTNAME", FIRSTNAME);
                    cmd.Parameters.AddWithValue("@MI", MI);
                    
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;

        }


        public void newApplicantISAMs(string _appno, string _appdate, string _apptype,
                                       string _lname, string _fname, string _mname,
                                       string _entrylevelcode, string _entryleveltype,
                                       string _entryYear, string _mail1, string _mail2,
                                       string _mail3, string _bday, 
                                       string _natCode, string _civilStatus, string _gender, double _prevgrade, string _dateEncode, string _usercode)
        {
            using (SqlConnection cn = new SqlConnection(ISAMSCS))
            {
                // string strSQL = "spInsertApplicant";
                string strSQL = "Insert Into Appl_Info_MF(appl_no,appl_date,appl_type,appl_lname,appl_fname,appl_mname, " +
                                "entry_level_code,entry_level_type,entry_year,mail_addr1, mail_addr2, mail_addr3, bday, natl_Code, civil_stat, sex, final_grade, date_time_sys,log_user_name) " +
                                "Values(@AppNo,@AppDate,@AppType,@Applname,@Appfname,@Appmname,@EntryLevelCode,@EntryLevelType, " +
                                "@EntryYear,@Mail1,@Mail2,@Mail3,@Bday,@natCode, @civilStatus, @gender,@finalgrade, @dateEncode, @userCode)";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@AppNo",_appno);
                    cmd.Parameters.AddWithValue("@AppDate", _appdate);
                    cmd.Parameters.AddWithValue("@AppType", _apptype);
                    cmd.Parameters.AddWithValue("@Applname", _lname);
                    cmd.Parameters.AddWithValue("@Appfname", _fname);
                    cmd.Parameters.AddWithValue("@Appmname", _mname);
                    cmd.Parameters.AddWithValue("@EntryLevelCode", _entrylevelcode);
                    cmd.Parameters.AddWithValue("@EntryLevelType", _entryleveltype);
                    cmd.Parameters.AddWithValue("@EntryYear", _entryYear);
                    cmd.Parameters.AddWithValue("@Mail1", _mail1);
                    cmd.Parameters.AddWithValue("@Mail2", _mail2);
                    cmd.Parameters.AddWithValue("@Mail3", _mail3);
                    cmd.Parameters.AddWithValue("@Bday", _bday);
                    cmd.Parameters.AddWithValue("@natCode", _natCode);
                    cmd.Parameters.AddWithValue("@civilStatus", _civilStatus);
                    cmd.Parameters.AddWithValue("@gender", _gender);
                    cmd.Parameters.AddWithValue("@finalgrade", _prevgrade);
                    cmd.Parameters.AddWithValue("@dateEncode", _dateEncode);
                    cmd.Parameters.AddWithValue("@userCode", _usercode);

                    cmd.ExecuteNonQuery();
                }
            }
        
        }

        // Update ISAMS Applicant Information
        // Marlowe Escaros
        // 2018.01.15
        // Add function to update Applicant information from ISAMSDB
        // ===========================================================================================================================
        public void UpdateApplicantISAMS(string _appno, string _apptype, string _lname, string _fname, string _mname, string _entrylevelcode, string _entryleveltype,
                                       string _mail1, string _mail2, string _mail3, string _bday, string _gender)
        {
            using (SqlConnection cn = new SqlConnection(ISAMSCS))
            {
                using (SqlCommand cmd = new SqlCommand(QryUpdateApplInfoISAMS(), cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@ApplNo", _appno);
                    cmd.Parameters.AddWithValue("@ApplType", _apptype);
                    cmd.Parameters.AddWithValue("@ApplLname", _lname);
                    cmd.Parameters.AddWithValue("@ApplFname", _fname);
                    cmd.Parameters.AddWithValue("@ApplMname", _mname);
                    cmd.Parameters.AddWithValue("@EntryLevelCode", _entrylevelcode);
                    cmd.Parameters.AddWithValue("@EntryLevelType", _entryleveltype);
                    cmd.Parameters.AddWithValue("@Mail1", _mail1);
                    cmd.Parameters.AddWithValue("@Mail2", _mail2);
                    cmd.Parameters.AddWithValue("@Mail3", _mail3);
                    cmd.Parameters.AddWithValue("@Bday", _bday);
                    cmd.Parameters.AddWithValue("@Sex", _gender);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #region "QUERY UPDATE FOR UPDATING APPLICANT INFORMATION FROM ISAMSDB"
        
        private string QryUpdateApplInfoISAMS()
        {
            string qry = "";
            qry = "UPDATE Appl_Info_MF ";
            qry += "SET ";
            qry += "    appl_type = @ApplType,";
            qry += "    appl_lname = @ApplLname,";
            qry += "    appl_fname = @ApplFname,";
            qry += "    appl_mname = @ApplMname,";
            qry += "    entry_level_code = @EntryLevelCode,";
            qry += "    entry_level_type = @EntryLevelType,";
            qry += "    mail_addr1 = @Mail1,";
            qry += "    mail_addr2 = @Mail2,";
            qry += "    mail_addr3 = @Mail3,";
            qry += "    bday = @Bday,";
            //qry += "    natl_code = @NatlCode,";
            //qry += "    civil_stat = @CivilStat,";
            qry += "    sex = @Sex ";
            qry += "WHERE appl_no = @ApplNo ";
            return qry;
        }

        #endregion
        // ===========================================================================================================================

        public void updateApplicant(string APPTYPECODE, string LEVELTYPECODE, string STRANDCODE, DateTime APPDOA, string APPNUM,
                                 bool WLSTATUS, bool SSICHILD, string LASTNAME, string FIRSTNAME, string MIDDLENAME,
                                 string MI, string SUFFIX, string FULLNAME, string GENDERCODE, DateTime DOB,string POB, double AGE, string AGEONJUNE, bool SHORTBYJUNE, int SHORTMONTH, string TELNO,
                                 string MOBILENO, string CONTACTPERSON, string ADDINFO, string BARANGAYCODE, string CITYCODE, string EMAIL, string REMARKS, bool STAT, bool _appbackout,
                                 DateTime DATEENCODE, DateTime DATEUPDATE, string USERCODE,
                                 bool FORM138, bool BC, bool COLORED1X1, bool BROWNENVELOPE, bool GM, bool RECOMMENDATION, bool FORM137, bool NCAE, string OTHER)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                // string strSQL = "spInsertApplicant";

                using (SqlCommand cmd = new SqlCommand("spUpdateApplicantINFO", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AppTypeCode", APPTYPECODE);
                    cmd.Parameters.AddWithValue("@LevelTypeCode", LEVELTYPECODE);
                    cmd.Parameters.AddWithValue("@StrandCode", STRANDCODE);
                    cmd.Parameters.AddWithValue("@AppDOA", APPDOA);
                    cmd.Parameters.AddWithValue("@AppNum", APPNUM);
                    cmd.Parameters.AddWithValue("@WLStatus", WLSTATUS);
                    cmd.Parameters.AddWithValue("@SSIChild", SSICHILD);
                    cmd.Parameters.AddWithValue("@LastName", LASTNAME);
                    cmd.Parameters.AddWithValue("@FirstName", FIRSTNAME);
                    cmd.Parameters.AddWithValue("@MiddleName", MIDDLENAME);
                    cmd.Parameters.AddWithValue("@MI", MI);
                    cmd.Parameters.AddWithValue("@Suffix", SUFFIX);
                    cmd.Parameters.AddWithValue("@FullName", FULLNAME);
                    cmd.Parameters.AddWithValue("@GenderCode", GENDERCODE);
                    cmd.Parameters.AddWithValue("@DOB", DOB);
                    cmd.Parameters.AddWithValue("@POB", POB);
                    cmd.Parameters.AddWithValue("@Age", AGE);
                    cmd.Parameters.AddWithValue("@AgeOnJune", AGEONJUNE);
                    cmd.Parameters.AddWithValue("@ShortByJune", SHORTBYJUNE);
                    cmd.Parameters.AddWithValue("@ShortMonth", SHORTMONTH);
                    cmd.Parameters.AddWithValue("@TelNo", TELNO);
                    cmd.Parameters.AddWithValue("@MobileNo", MOBILENO);
                    cmd.Parameters.AddWithValue("@ContactPerson", CONTACTPERSON);
                    cmd.Parameters.AddWithValue("@AddInfo", ADDINFO);
                    cmd.Parameters.AddWithValue("@BarangayCode", BARANGAYCODE);
                    cmd.Parameters.AddWithValue("@CityCode", CITYCODE);
                    cmd.Parameters.AddWithValue("@Email", EMAIL);
                    cmd.Parameters.AddWithValue("@Remarks", REMARKS);
                    cmd.Parameters.AddWithValue("@Status", STAT);
                    cmd.Parameters.AddWithValue("@APPBACKOUT", _appbackout);
                    cmd.Parameters.AddWithValue("@DateUpdate", DATEUPDATE);
                    cmd.Parameters.AddWithValue("@UserCode", USERCODE);

                    //CREDENTIALS
                    cmd.Parameters.AddWithValue("@Form138", FORM138);
                    cmd.Parameters.AddWithValue("@BC", BC);
                    cmd.Parameters.AddWithValue("@Colored1x1", COLORED1X1);
                    cmd.Parameters.AddWithValue("@BrownEnvelope", BROWNENVELOPE);
                    cmd.Parameters.AddWithValue("@GM", GM);
                    cmd.Parameters.AddWithValue("@Recommendation", RECOMMENDATION);
                    cmd.Parameters.AddWithValue("@Form137", FORM137);
                    cmd.Parameters.AddWithValue("@NCAE", NCAE);
                    cmd.Parameters.AddWithValue("@Other", OTHER);

                    cn.Open();
                    cmd.ExecuteNonQuery();


                    

                }

            }
        
        }

     


        public void PreviousGrade(string SNUM, double ENGTOTAL, double SCITOTAL, double MATHTOTAL, double FIRSTTOTAL, double SECONDTOTAL, double THIRDTOTAL,
                                  double FOURTHTOTAL, double ENG1, double ENG2, double ENG3, double ENG4, double SCI1, double SCI2, double SCI3, double SCI4,
                                  double MATH1, double MATH2, double MATH3, double MATH4, bool LOWERENG, bool LOWERSCI, bool LOWERMATH, double FINALAVERAGE, double FINALAVERAGE2,
                                  DateTime DATENCODE, DateTime DATEUPDATE, string USERCODE)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spInsertPreviousGrade", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SNUM", SNUM);
                    cmd.Parameters.AddWithValue("@ENGTOTAL", ENGTOTAL);
                    cmd.Parameters.AddWithValue("@SCITOTAL", SCITOTAL);
                    cmd.Parameters.AddWithValue("@MATHTOTAL", MATHTOTAL);
                    cmd.Parameters.AddWithValue("@FIRSTTOTAL", FIRSTTOTAL);
                    cmd.Parameters.AddWithValue("@SECONDTOTAL", SECONDTOTAL);
                    cmd.Parameters.AddWithValue("@THIRDTOTAL", THIRDTOTAL);
                    cmd.Parameters.AddWithValue("@FOURTHTOTAL", FOURTHTOTAL);
                    cmd.Parameters.AddWithValue("@ENG1", ENG1);
                    cmd.Parameters.AddWithValue("@ENG2", ENG2);
                    cmd.Parameters.AddWithValue("@ENG3", ENG3);
                    cmd.Parameters.AddWithValue("@ENG4", ENG4);
                    cmd.Parameters.AddWithValue("@SCI1", SCI1);
                    cmd.Parameters.AddWithValue("@SCI2", SCI2);
                    cmd.Parameters.AddWithValue("@SCI3", SCI3);
                    cmd.Parameters.AddWithValue("@SCI4", SCI4);
                    cmd.Parameters.AddWithValue("@MATH1", MATH1);
                    cmd.Parameters.AddWithValue("@MATH2", MATH2);
                    cmd.Parameters.AddWithValue("@MATH3", MATH3);
                    cmd.Parameters.AddWithValue("@MATH4", MATH4);
                    cmd.Parameters.AddWithValue("@LOWERENG", LOWERENG);
                    cmd.Parameters.AddWithValue("@LOWERSCI", LOWERSCI);
                    cmd.Parameters.AddWithValue("@LOWERMATH", LOWERMATH);
                    cmd.Parameters.AddWithValue("@FINALAVERAGE", FINALAVERAGE);
                    cmd.Parameters.AddWithValue("@FINALAVERAGE2", FINALAVERAGE2);
                    cmd.Parameters.AddWithValue("@DATEENCODE", DATENCODE);
                    cmd.Parameters.AddWithValue("@DATEUPDATE", DATEUPDATE);
                    cmd.Parameters.AddWithValue("@USERCODE", USERCODE);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                    

                }
            }
            
        }


        public void updatePreviousGrade(string SNUM, double ENGTOTAL, double SCITOTAL, double MATHTOTAL, double FIRSTTOTAL, double SECONDTOTAL, double THIRDTOTAL,
                                  double FOURTHTOTAL, double ENG1, double ENG2, double ENG3, double ENG4, double SCI1, double SCI2, double SCI3, double SCI4,
                                  double MATH1, double MATH2, double MATH3, double MATH4, bool LOWERENG, bool LOWERSCI, bool LOWERMATH, double FINALAVERAGE, double FINALAVERAGE2,
                                  DateTime DATEUPDATE, string USERCODE)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spUpdatePreviousGrade", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SNUM", SNUM);
                    cmd.Parameters.AddWithValue("@ENGTOTAL", ENGTOTAL);
                    cmd.Parameters.AddWithValue("@SCITOTAL", SCITOTAL);
                    cmd.Parameters.AddWithValue("@MATHTOTAL", MATHTOTAL);
                    cmd.Parameters.AddWithValue("@FIRSTTOTAL", FIRSTTOTAL);
                    cmd.Parameters.AddWithValue("@SECONDTOTAL", SECONDTOTAL);
                    cmd.Parameters.AddWithValue("@THIRDTOTAL", THIRDTOTAL);
                    cmd.Parameters.AddWithValue("@FOURTHTOTAL", FOURTHTOTAL);
                    cmd.Parameters.AddWithValue("@ENG1", ENG1);
                    cmd.Parameters.AddWithValue("@ENG2", ENG2);
                    cmd.Parameters.AddWithValue("@ENG3", ENG3);
                    cmd.Parameters.AddWithValue("@ENG4", ENG4);
                    cmd.Parameters.AddWithValue("@SCI1", SCI1);
                    cmd.Parameters.AddWithValue("@SCI2", SCI2);
                    cmd.Parameters.AddWithValue("@SCI3", SCI3);
                    cmd.Parameters.AddWithValue("@SCI4", SCI4);
                    cmd.Parameters.AddWithValue("@MATH1", MATH1);
                    cmd.Parameters.AddWithValue("@MATH2", MATH2);
                    cmd.Parameters.AddWithValue("@MATH3", MATH3);
                    cmd.Parameters.AddWithValue("@MATH4", MATH4);
                    cmd.Parameters.AddWithValue("@LOWERENG", LOWERENG);
                    cmd.Parameters.AddWithValue("@LOWERSCI", LOWERSCI);
                    cmd.Parameters.AddWithValue("@LOWERMATH", LOWERMATH);
                    cmd.Parameters.AddWithValue("@FINALAVERAGE", FINALAVERAGE);
                    cmd.Parameters.AddWithValue("@FINALAVERAGE2", FINALAVERAGE2);
                    cmd.Parameters.AddWithValue("@DATEUPDATE", DATEUPDATE);
                    cmd.Parameters.AddWithValue("@USERCODE", USERCODE);

                    cn.Open();

                    cmd.ExecuteNonQuery();


                }
            }
        
        }



       

        /// <summary>
        /// GETTING DATA SECTION
        /// </summary>
        /// <returns></returns>

        public DataTable getApplicant()
        { 
            DataTable dt = new DataTable();
            //string strSQL = "Select AppNum, Fullname, levelTypeCode, WLStatus, SSIChild, LevelTypeCode, GenderCode from Admission.App_Info_MF order by FirstName";
            //string strSQL = "SELECT a.AppNum, a.Fullname, a.levelTypeCode, a.WLStatus, a.SSIChild, a.LevelTypeCode, a.GenderCode, b.Remarks as statRemarks " + 
            //                "From Admission.App_Info_MF a INNER JOIN xSystem.ApplicantStatusTrail_RF b " +
            //                "ON a.StatCode = b.statCode Order by Fullname";
            string strSQL = "sp_ApplicantListTrail";
            dt = queryCommandDT_StoredProc(strSQL);
            return dt;
      
        }

        public DataSet GET_APPLICANT_COUNTS(string _sy)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Admission].[spCOUNT_APPLICANTS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SY", _sy);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }

            return ds;
        }


        //Get Only list of Applicant base on system default school year
        //Russel 11/21/2016
        public DataTable GET_APPLICANT_LIST(string _sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spGET_APPLICANT_LIST", cn))
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

        //CREATED 02/17/2017
        public DataTable GET_ALL_APPLICANT(string _sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Admission].[spGET_ALL_APPLICANT]", cn))
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

        //Get Individual Applicant Information
        public void GET_APPLICANT_INFO(string _appnum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spGET_APPLICANT_INFO", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);


                    SqlDataReader dr = cmd.ExecuteReader();// cmd.ExecuteNonQuery();
                    
                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {

                            _APPDATE = Convert.ToDateTime(dr["AppDOA"]);
                            _APPTYPE = dr["AppTypeCode"].ToString();
                            _DOB = Convert.ToDateTime(dr["DOB"]);
                            
                            _APPNUM = dr["AppNum"].ToString();
                            _APPLASTNAME = dr["LastName"].ToString();
                            _APPFIRSTNAME = dr["FirstName"].ToString();
                            _APPMIDDLENAME = dr["MiddleName"].ToString();
                            _APPMI = dr["MI"].ToString();
                            _APPSUFFIX = dr["Suffix"].ToString();
                            _GENDERCODE = dr["GenderCode"].ToString();
                            _APPPLACEOFBIRTH = dr["POB"].ToString();

                            _LEVELDESC= dr["LevelTypeDesc"].ToString();
                            _LEVELTYPECODE = dr["LeveltypeCode"].ToString();
                            _APPSTRANDCODE = dr["StrandCode"].ToString();

                            _CONTACTPERSON = dr["ContactPerson"].ToString();
                            _HOMEPHONE = dr["TelNo"].ToString();
                            _OFFICEPHONE = dr["Telno"].ToString();
                            _MOBILEPHONE = dr["MobileNo"].ToString();
                            _ADDRESS = dr["AddInfo"].ToString();
                            _BARANGGAYCODE = dr["BarangayCode"].ToString();
                            _CITYCODE = dr["CityCode"].ToString();
                            _EMAIL = dr["Email"].ToString();

                            _WAITLISTED = Convert.ToBoolean(dr["WLStatus"]);
                            _EMPLOYEECHILD = Convert.ToBoolean(dr["SSIChild"]);
                            _NOT_ACCEPTED = Convert.ToBoolean(dr["AppNotAccepted"]);
                            _NOT_INTERESTED = Convert.ToBoolean(dr["AppNotInterested"]);
                            _BACKOUT = Convert.ToBoolean(dr["AppBackOut"]);

                            _ISMONTHSHORT = Convert.ToBoolean(dr["ShortByJune"]);
                            _MONTHSHORT = Convert.ToDouble(dr["ShortMonth"]);

                            //CREDENTIALS
                            _FORM138 = Convert.ToBoolean(dr["FORM138"]);
                            _BIRTHCERTIFICATE = Convert.ToBoolean(dr["BC"]);
                            _NSO = Convert.ToBoolean(dr["BC_NSO"]);
                            _NSO_Orig = Convert.ToBoolean(dr["BC_NSO_Orig"]);
                            _NSO_Pcopy = Convert.ToBoolean(dr["BC_NSO_Pcopy"]);
                            _CR = Convert.ToBoolean(dr["BC_CR"]);
                            _CR_Orig = Convert.ToBoolean(dr["BC_CR_Orig"]);
                            _CR_Pcopy = Convert.ToBoolean(dr["BC_CR_Pcopy"]);
                            _COLORED1X1 = Convert.ToBoolean(dr["Colored1x1"]);
                            _BROWNENVELOPE = Convert.ToBoolean(dr["BrownEnvelope"]);                                                                                                                                                                                                  
                            _GOODMORAL = Convert.ToBoolean(dr["GM"]);                                                                                                                                                                                                                                                           
                            _RECOMMENDATION = Convert.ToBoolean(dr["Recommendation"]);
                            _FORM137 = Convert.ToBoolean(dr["FORM137"]);
                            _NCAE = Convert.ToBoolean(dr["NCAE"]);
                            _INTERVIEW = Convert.ToBoolean(dr["Interview"]);
                            _OTHERCREDENTIALS = dr["Other"].ToString();


                            //PREVIOUS GRADES
                            _ENG1 = Convert.ToDouble(dr["ENG1"]);
                            _ENG2 = Convert.ToDouble(dr["ENG2"]);
                            _ENG3= Convert.ToDouble(dr["ENG3"]);
                            _ENG4 = Convert.ToDouble(dr["ENG4"]);
                            _ENGTOTAL = Convert.ToDouble(dr["EngTotal"]);

                            _MAT1 = Convert.ToDouble(dr["Math1"]);
                            _MAT2 = Convert.ToDouble(dr["Math2"]);
                            _MAT3 = Convert.ToDouble(dr["Math3"]);
                            _MAT4 = Convert.ToDouble(dr["Math4"]);
                            _MATTOTAL = Convert.ToDouble(dr["MathTotal"]);

                            _SCI1 = Convert.ToDouble(dr["Sci1"]);
                            _SCI2 = Convert.ToDouble(dr["Sci2"]);
                            _SCI3 = Convert.ToDouble(dr["Sci3"]);
                            _SCI4 = Convert.ToDouble(dr["Sci4"]);
                            _SCITOTAL = Convert.ToDouble(dr["SciTotal"]);

                            _Q1 = Convert.ToDouble(dr["FirstTotal"]);
                            _Q2 = Convert.ToDouble(dr["SecondTotal"]);
                            _Q3 = Convert.ToDouble(dr["ThirdTotal"]);
                            _Q4 = Convert.ToDouble(dr["FourthTotal"]);
                            _PREVFINALGRADE = Convert.ToDouble(dr["FinalAverage"]);

                            _ISLOWENG = Convert.ToBoolean(dr["LowerEng"]);
                            _ISLOWSCI = Convert.ToBoolean(dr["LowerSci"]);
                            _ISLOWMATH = Convert.ToBoolean(dr["LowerMath"]);

                            _ISOVERRIDEPREVGRADE = Convert.ToBoolean(dr["OverrideAverage"]);
                            _OVERRIDEFINALGRADE = Convert.ToDouble(dr["FinalAverage2"]);
                            //PERIOD TOTAL


                        }

                    }
                }
            }
           
        }

        public string GetApplicantOnlineReferenceId(string appNum)
        {
            string refId = null;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT refOnline FROM Admission.App_Info_MF WHERE AppNum = @appNum", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("appNum", appNum);
                    refId = cmd.ExecuteScalar().ToString();
                }
            }

            return refId;
        }

        public void UpdateApplicantTagging(string appnum, bool ni, bool na, bool bo)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spUPDATE_APPL_TAGGING", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ni", ni);
                    cmd.Parameters.AddWithValue("@na", na);
                    cmd.Parameters.AddWithValue("@bo", bo);
                    cmd.Parameters.AddWithValue("@appnum", appnum);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable getDetailsApplicant()
        {
         DataTable dt = new DataTable();
         string strSQL = "SELECT * from vAppInfoCredentials";
         dt = queryCommandDT(strSQL);
         return dt;
        }

        public DataTable getPrevGrade()
        {
            DataTable dt = new DataTable();
            string strSQL = "SELECT SNUM, Eng1,Eng2,Eng3,Eng4,Sci1,Sci2,Sci3,Sci4,Math1,Math2,Math3,Math4 from Admission.PreviousGrade_MF";
            dt = queryCommandDT(strSQL);
            return dt;
        }


        //ISAMS SOURCE - RESERVATION LIST OLD
        //11-5-2015
        //modify SIMS SOURCE - RESERVATION LIST OLD
        //04-19-2016
        public DataTable getStudentReserveList()
        {
            DataTable dt = new DataTable();
            //string strSQL = "Sims_CountReservedStudentOLD";
            string strSQL = "spCountReserveStudent_OLD";
            //string strSQL = "Select Count(*) from Student_MF where studtype = 'O' and statcode = 'EN'";
            //dt = ISAMSqueryCommandDT_StoredProc(strSQL);
            dt = queryCommandDT(strSQL);
            return dt;
        }

        //ISAMS SOURCE - RESERVATION LIST NEW
        //modify SIMS SOURCE - RESERVATION LIST NEW
        //04-19-2016
        public DataTable getStudentReserveNewList()
        {
            DataTable dt = new DataTable();
            //string strSQL = "Sims_CountReservedStudentNEW";
            //dt = ISAMSqueryCommandDT_StoredProc(strSQL);

            string strSQL = "spCountReserveStudent_NEW";
            dt = queryCommandDT(strSQL);
            return dt;
        }

        //SIMS SOURCE - TOTAL RESERVED
        //04-19-2016
        public DataTable getStudentTOTALReservedList()
        {
            DataTable dt = new DataTable();
            //string strSQL = "Sims_CountReservedStudentNEW";
            //dt = ISAMSqueryCommandDT_StoredProc(strSQL);

            string strSQL = "spCountTOTALReserveStudent";
            dt = queryCommandDT(strSQL);
            return dt;
        }

        public DataTable getORTestingList()
        { 
            DataTable dt = new DataTable();
            string strSQL = "Sims_DisplayTestingOR";
            dt = ISAMSqueryCommandDT_StoredProc(strSQL);
            return dt;
        }


        //CHECKING TESTING receipt no in ISAMS
        public bool CHECKING_OR_INPUT(string _input)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(ISAMSCS))
            {
                using (SqlCommand cmd = new SqlCommand("Sims_CheckTestingOR", cn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RECEIPTNO",  _input);

                    cn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        x = true;
                    }
                    else
                    {
                        x = false;
                    }

                }
            }

            return x;
        }


        public int getStudentTotalReservedCount()
        {
            int iCount = 0;
            using (SqlConnection cn = new SqlConnection(ISAMSCS))
            {
                string strSQL = "Sims_CountReservedStudent";

                SqlCommand cmd = new SqlCommand(strSQL, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                iCount = (int)cmd.ExecuteScalar();
            }

            return iCount;
        }

    }


    //CLASS FOR SCHEDULING AND ENCODING OF APPLICANT SCREENING RESULT

    public class ApplicantSchedule: cBase
    {
        
        //FIELDS
        public string _OR { get; set; }
        public int _EXAMID { get; set; }
        public int _INTID { get; set; }
        public int _SCHEDSTAT { get; set; }
        public bool _ISFREE { get; set; }
        public bool _ISPASSED { get; set; }

        //2017.11.17
        //Marlowe Escaros
        //GET APPLICANT SCHEDULE INFO
        public DataTable GET_APPLICANT_SCHEDULE_INFO(string sched_appnum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_APPLICANT_SCHEDULE_INFO", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", sched_appnum);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // GET ALL AVAILABLE SLOT EXAMINATION SCHEDULE
        // 2017.11.24 -- Marlowe Escaros
        public DataTable GET_AVAILABLE_EXAMINATION_SCHED(string _levelcode, string _currentsy, int _currentexamid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_AVAILABLE_EXAM_SCHED", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_levelcode", _levelcode);
                    cmd.Parameters.AddWithValue("@p_currentsy", _currentsy);
                    cmd.Parameters.AddWithValue("@p_examid", _currentexamid);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // GET ALL AVAILABLE SLOT INTERVIEW SCHEDULE
        // 2017.11.24 -- Marlowe Escaros
        public DataTable GET_AVAILABLE_INTERVIEW_SCHED(string _levelcode, string _currentsy, int _currentintid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_AVAILABLE_INT_SCHED", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_levelcode", _levelcode);
                    cmd.Parameters.AddWithValue("@p_currentsy", _currentsy);
                    cmd.Parameters.AddWithValue("@p_intid", _currentintid);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

      
        //GET LIST OF EXAMINATION DATA
        public DataTable GET_EXAMINATION_LIST(string _levelTypeCode,string _sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_APPLICANT_EXAM_SCHEDULE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LEVELTYPECODE", _levelTypeCode);
                    cmd.Parameters.AddWithValue("@SY", _sy);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        //GET LIST OF INTERVIEW DATA
        public DataTable GET_INTERVIEW_LIST(string _levelTypeCode, string _sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_APPLICANT_INTERVIEW_SCHEDULE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LEVELTYPECODE", _levelTypeCode);
                    cmd.Parameters.AddWithValue("@SY", _sy);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GET_LIST_APPLICANT_FOR_SCHEDULING(string _levelTypeCode, string _sy)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_APPLICANT_LIST_FOR_SCHEDULE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LEVELTYPECODE", _levelTypeCode);
                    cmd.Parameters.AddWithValue("@SY", _sy);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public static DataTable GetApplicantForScheduling(string lvl, string sy, string search = null, string limit = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_APPL_FOR_SCHED", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@lvl", lvl);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@search", search);
                    cmd.Parameters.AddWithValue("@limit", limit);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        //VALIDATE OR OF APPLICANT ON SAP
        public bool EXIST_SAP_APP_OR_NUMBER(string _appnum, string _ornumber)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spCHECK_SAP_OR", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cmd.Parameters.AddWithValue("@OR", _ornumber);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;
        }

        public bool EXIST_ISAMS_OR_NUMBER(string _orNumber)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spCHECK_ISAMS_OR", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RECEIPTNUM", _orNumber);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;
        }

        //CHECK IF OR EXIST
        public bool EXIST_OR_NUMBER(string _orNumber)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spCHECK_OR", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OR_NUMBER", _orNumber);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;

        }

        public bool EXIST_STUDENT(string _appnum)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.CHECK_APPLICANT_STUDENT", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;
        }

        //Inserting Appicant Schedule
        public void INSERT_APPLICANT_SCHEDULE(string _appNum, string _orNum, int _examID, int _intID, 
                                            int _schedStat, bool _isFree, string _userCode, DateTime _releaseDate)
        {
            {
                using (SqlConnection cn = new SqlConnection(CS))
                {

                    using (SqlCommand cmd = new SqlCommand("Admission.spINSERT_APPLICANT_SCHEDULE", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@APPNUM", _appNum);
                        cmd.Parameters.AddWithValue("@OR", _orNum);
                        cmd.Parameters.AddWithValue("@EXAMID", _examID);
                        cmd.Parameters.AddWithValue("@INTID", _intID);
                        cmd.Parameters.AddWithValue("@SCHEDSTAT", _schedStat);
                        cmd.Parameters.AddWithValue("@ISFREE", _isFree);
                        cmd.Parameters.AddWithValue("@USERCODE", _userCode);
                        cmd.Parameters.AddWithValue("@RELEASEDATE", _releaseDate);

                        cn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        // Updating Applicant Schedule
        // 2017.11.19
        // Marlowe Escaros
        public void UPDATE_APPLICANT_SCHEDULE(string _appnum, /*string _ornum,*/ int _examid_new, int _intid_new, DateTime _releasedate,
                                              bool _isexamchanged, bool _isintchanged, int _examid_old, int _intid_old)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spUPDATE_APPLICANT_SCHEDULE", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    //cmd.Parameters.AddWithValue("@OR", _ornum);
                    cmd.Parameters.AddWithValue("@EXAMID", _examid_new);
                    cmd.Parameters.AddWithValue("@INTID", _intid_new);
                    cmd.Parameters.AddWithValue("@RELEASEDATE", _releasedate);
                    cmd.Parameters.AddWithValue("@ISEXAMIDCHANGED", _isexamchanged);
                    cmd.Parameters.AddWithValue("@ISINTIDCHANGED", _isintchanged);
                    cmd.Parameters.AddWithValue("@OLD_EXAMID", _examid_old);
                    cmd.Parameters.AddWithValue("@OLD_INTID", _intid_old);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void UpdateApplicantSchedule(string _appNum, string _orNum, int _examID, int _intID,
                                            int _schedStat, bool _isfree, DateTime _du, string _userCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
              
                using (SqlCommand cmd = new SqlCommand("spUpdateApplicantSchedule", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APPNUM", _appNum);
                    cmd.Parameters.AddWithValue("@OR", _orNum);
                    cmd.Parameters.AddWithValue("@EXAMID", _examID);
                    cmd.Parameters.AddWithValue("@INTID", _intID);
                    cmd.Parameters.AddWithValue("@SCHEDSTAT", _schedStat);
                    cmd.Parameters.AddWithValue("@ISFREE", _isfree);
                    cmd.Parameters.AddWithValue("@DU", _du);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }


        
        //Decrease available slot of scheduled by one in Screening Schedule setup
        public void updateScreeingSetupSlot(int _id)
        { 
             using (SqlConnection cn = new SqlConnection(CS))
            {
              
                using (SqlCommand cmd = new SqlCommand("spUpdateScreeningSetupSlot", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", _id);
                     cn.Open();

                    cmd.ExecuteNonQuery();
                }
             }
        }

        // Check applicant if exist in Applicant Schedule and Permit
        // 2017.11.22
        // Marlowe Escaros
        public bool CHECK_APPLICANT_SCHED_PERMIT(string _appnum)
        {
            bool ret = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spCHECK_APP_SCHED_PERMIT", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _appnum);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                    dr.Close();
                }
            }

            return ret;
        }
        // ----------------------------------------------------------

        

        public bool checkExistAppInSchedule(string _appNum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select AppNum,AppOR,ExamID,IntID,SchedStat,isfree,ispassed from [Admission].[App_ScreeningSchedule_MF] where AppNum ='" + _appNum + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        x = true;
                        while (dr.Read())
                        {
                            _OR = dr["AppOR"].ToString();
                            _EXAMID = (int)dr["ExamID"];
                            _INTID = (int)dr["IntID"];
                            _SCHEDSTAT = (int)dr["SchedStat"];
                            _ISFREE = (bool)dr["ISFREE"];
                            _ISPASSED = (bool)dr["IsPassed"];
                        }

                    }

                    else
                    {
                        x = false;
                    }

                    return x;
                }
            }

        }

        //check if receiptno in use
        public bool CHECK_EXIST_OR(string _or)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select AppOR from [Admission].[App_ScreeningSchedule_MF] where AppOR ='" + _or + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        x = true;
                    }

                    else
                    {
                        x = false;
                    }

                    return x;
                }
            }
        }
   
        //Displaying List of Applicant for Scheduling
        public DataTable getApplicantForScheduling()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayApplicantForScheduling", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }


    

        //Display on each row inside gridview base on exam type parameter I- Interview / E-Examination
        //Stored Procedure
        //11-12-2015 Russel Vasquez
        
        
        public DataTable getScheduleList(string _ExamType)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spDisplayExamScheduleList", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ScreeningCode", _ExamType);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }


        //UPDATE APPLICANT STATUS TRAIL TABLE
        public void UPDATE_ApplicantStatusTrail(string _snum, int _statusCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "spApplicantClearSchedule";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APPNUM", _snum);
                    cmd.Parameters.AddWithValue("@SCHEDSTAT", _statusCode);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }

 

    }// End of Scheduling Class


    public class ApplicantScreeningData : ApplicantBASE
    {
     

        //--------------------------------------------------------

        //Display Applicant Information Briefly
        //11-23-2015
        public bool GET_APPLICANT_INFO_SCREENING(string _appnum)
        {
            bool x = false;

          using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_APPLICANT_INFO_FOR_SCREENING_ENTRY", cn))
                {
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);

                    cn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        x = true;
                        while (rdr.Read())
                        {
                            _APPNUM = rdr["AppNum"].ToString();
                            _FULLNAME = rdr["Fullname"].ToString();
                            _SAP_FN_FORMAT = rdr["StandardFullName"].ToString();
                            _LEVELCATCODE = rdr["LevelCatCode"].ToString();
                            _LEVELTYPECODE = rdr["LevelTypeCode"].ToString();
                            _DOB = (DateTime)rdr["DOB"];

                            if (Convert.ToDouble(rdr["FinalAverage"]) == 0 || string.IsNullOrEmpty(rdr["FinalAverage"].ToString()))
                            {
                                _PREVGRADE = (double)rdr["FinalAverage2"];
                            }
                            else
                            {
                                _PREVGRADE = (double)rdr["FinalAverage"];
                            }

                            //GET EXAM AND INTERVIEW SCHEDULE
                            _EXAMSCHED = rdr["ExamSched"].ToString();
                            _INTSCHED = rdr["IntSched"].ToString();

                            //DATE APPLIED
                            _APPDATE = (DateTime)rdr["AppDOA"];


                            _APPLASTNAME = rdr["LastName"].ToString();
                            _APPFIRSTNAME = rdr["FirstName"].ToString();
                            _APPMIDDLENAME = rdr["MiddleName"].ToString();
                            _GENDERCODE = rdr["GenderCode"].ToString();

                            ////THIS CONDITION WILL IDENTIFY HOW TESTING 
                            ////PROCEDURE WILL ACT.
                            ////RUSSEL VASQUEZ 01/29/2016
                            //if (Convert.ToInt32(rdr["SchedStat"].ToString()) == 4)
                            //{
                            //    _RETESTSTATUS = true;
                            //}
                            //else
                            //{
                            //    _RETESTSTATUS = false;
                            //}


                           
                            //_AGEBYJUNE = rdr["AgeOnJune"].ToString();
                            //_GENDERCODE = rdr["GenderCode"].ToString();
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

        //DISPLAY LIST OF CONTENT/COMPONENT
        public DataTable GET_SCREENING_CONTENT_LIST(string _levelType)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Admission].[spGET_SCREENING_CONTENT_LIST]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LEVELTYPE", _levelType);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }
        
        
        
        
        
        /*Retrieve Record
         * Changes Entry if applicant not yet and need for re-evaluating Passed
         * 11/27/2015
         * Russel Vasquez
        */




        public bool CHECK_EXIST_EXAM(string _appnum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spCHECK_APPLICANT_EXIST_EXAM", cn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);

                    cn.Open();

                    x = (bool)cmd.ExecuteScalar();
                }
            }

            return x;
        }

        //Will check if applicant already exist on student information
        public bool CHECKAPPEXISTSTUDENT(string _appnum)
        {  
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spCheckApplicantInRegistrationExist", cn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);

                    cn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {

                        x = true;
                    }
                    else
                    {
                        x = false;
                    }
                }
            }

            return x;
        
        }

        //Check applicant record on result slip table
        public bool CHECK_APPRESULTSLIP(string _appnum)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spCheckAppResultSlip", cn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);

                    cn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {

                        while (rdr.Read())
                        {
                            _DATECREATED = Convert.ToDateTime(rdr["DateCreated"]);
                            _ADDRESSTO = rdr["AddressTo"].ToString();
                            _DATEEXPIRED = Convert.ToDateTime(rdr["DateExpired"]);
                            _RESULTTYPE = Convert.ToInt16(rdr["ResultType"]);
                        }

                        x = true;
                    }
                    else
                    {
                        x = false;
                    }
                }
            }

            return x;
        }



        public void UPDATE_APPLICANT_SCHEDULE_STATUS(string _appnum)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateApplicantScheduleStatusToDisable", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

      

        //RETRIEVE SCORES OF APPLICANT
        public bool GET_TEST_RECORD(string _appnum, string _ttcode)
        {
            bool x = false;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Admission].[spGET_APPLICANT_TEST_RESULT]", cn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cmd.Parameters.AddWithValue("@TTCODE", _ttcode);

                    cn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        x = true;

                        while (rdr.Read())
                        {
                            _SCORES = rdr["Scores"].ToString();
                            _RESULT = rdr["Result"].ToString();

                            _PREV = rdr["PREV"].ToString();
                            _PREVRESULT = rdr["PREVRESULT"].ToString();
                             _ASSESSMENT = rdr["ASSESSMENTRESULT"].ToString();
                            _OVERALL = rdr["OVERALL"].ToString();
                            _OBSERVATION = rdr["OBSERVATION"].ToString();
                            _STATCODE = rdr["STATCODE"].ToString();
                            _RETEST = (bool)rdr["RETEST"];
                        }

                      
                    }

                }
            }

            return x;
            

        }
        
        //DISPLAY LIST OF APPLICANT THAT PASSED 
        public DataTable DISPLAY_APPLICANTPASSED()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT APPNUM, Name, LeveltypeCode FROM vr_ListPassedApp order by Name", cn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        //Display List of Applicant That passed with Parameter
        public DataTable DISPLAY_APPLICANTPASSED(string _levelType)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT APPNUM, Name, LeveltypeCode FROM vr_ListPassedApp where LevelTypeCode=@LEVELTYPE order by Name", cn))
                {
                    cmd.Parameters.AddWithValue("@LEVELTYPE", _levelType);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable DISPLAY_APPLICANTFAILED()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT APPNUM, Name, LeveltypeCode FROM vr_ListFailedApp order by Name", cn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }
        //Display List of Applicant That passed with Parameter
        public DataTable DISPLAY_APPLICANTFAILED(string _levelType)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT APPNUM, Name, LeveltypeCode FROM vr_ListFailedApp where LevelTypeCode=@LEVELTYPE order by Name", cn))
                {
                    cmd.Parameters.AddWithValue("@LEVELTYPE", _levelType);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;
        }


        /* TRANSACTION AREA */

        //INSERT APPLICANT TEST RESULT

        public void INSERT_RESULT_DETAILS(string _appnum, string _ttcode, double _scores, double _result, bool _retest, string _usercode)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("[Admission].[spINSERT_TEST_DETAILS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cmd.Parameters.AddWithValue("@TTCODE", _ttcode);
                    cmd.Parameters.AddWithValue("@SCORES", _scores);
                    cmd.Parameters.AddWithValue("@RESULT", _result);
                    cmd.Parameters.AddWithValue("@RETEST", _retest);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        public void INSERT_SUMMARY_RESULT(string _appnum, double _prev, double _prevresult, double _assessmentresult,
                                        double _overall, string _observation, string _statcode, bool _retest, string _usercode)

        {

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("Admission.spINSERT_TEST_SUMMARY", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cmd.Parameters.AddWithValue("@PREV",_prev);
                    cmd.Parameters.AddWithValue("@PREVRESULT",_prevresult);
                    cmd.Parameters.AddWithValue("@ASSESSMENTRESULT", _assessmentresult);
                    cmd.Parameters.AddWithValue("@OVERALL", _overall);
                    cmd.Parameters.AddWithValue("@OBSERVATION", _observation);
                    cmd.Parameters.AddWithValue("@STATCODE", _statcode);
                    cmd.Parameters.AddWithValue("@RETEST", _retest);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        
        }

       
        
        //INSERT INTO Result Slip Table 02-16-2016
        public void INSERT_RESULTSLIP(string _appnum, DateTime _dateCreated, string _addressTo, DateTime _dateExpired, int _resultType, string _userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spInsertApplicantResultSlip", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cmd.Parameters.AddWithValue("@DATECREATED", _dateCreated);
                    cmd.Parameters.AddWithValue("@ADDRESSTO", _addressTo);
                    cmd.Parameters.AddWithValue("@DATEEXPIRED", _dateExpired);
                    cmd.Parameters.AddWithValue("@RESULTTYPE", _resultType);
                    cmd.Parameters.AddWithValue("@USERID", _userId);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        //UPDATE APPLICANT TEST RESULT

        public void UPDATE_DETAILS_RESULT(string _appnum, string _ttcode, double _scores, double _result, string _usercode)
        {
         using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spUpdateApplicantTestResult", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cmd.Parameters.AddWithValue("@TTCODE", _ttcode);
                    cmd.Parameters.AddWithValue("@SCORES", _scores);
                    cmd.Parameters.AddWithValue("@RESULT", _result);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void UPDATE_SUMMARY_RESULT(string _appnum, double _prev, double _prevresult, double _assessmentresult,
                                        double _overall, string _observation, string _statcode, string _usercode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateApplicantTestSummary", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cmd.Parameters.AddWithValue("@PREV", _prev);
                    cmd.Parameters.AddWithValue("@PREVRESULT", _prevresult);
                    cmd.Parameters.AddWithValue("@ASSESSMENTRESULT", _assessmentresult);
                    cmd.Parameters.AddWithValue("@OVERALL", _overall);
                    cmd.Parameters.AddWithValue("@OBSERVATION", _observation);
                    cmd.Parameters.AddWithValue("@STATCODE", _statcode);
                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }
        
        //UPDATE APPLICANT RESULT SLIP
        public void UPDATE_RESULTSLIP(string _appnum, DateTime _dateCreated, string _addressTo, DateTime _dateExpired, int _resultType, string _userId)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("spUpdateApplicantResultSlip", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APPNUM", _appnum);
                    cmd.Parameters.AddWithValue("@DATECREATED", _dateCreated);
                    cmd.Parameters.AddWithValue("@ADDRESSTO", _addressTo);
                    cmd.Parameters.AddWithValue("@DATEEXPIRED", _dateExpired);
                    cmd.Parameters.AddWithValue("@RESULTTYPE", _resultType);
                    cmd.Parameters.AddWithValue("@USERID", _userId);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }
        //


        // UPDATE RETEST TAGGING IN APPLICANT SCREENING RESULT
        public void UPDATE_RETEST_TAGGING(string _appnum, bool _retest)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                //using (SqlCommand cmd = new SqlCommand("UPDATE Admission.App_TestSummary_MF SET RETEST = @p_retest WHERE APPNUM = @p_appnum", cn))
                using (SqlCommand cmd = new SqlCommand("Admission.spUPDATE_APPL_RETEST_TAGGING", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _appnum);
                    cmd.Parameters.AddWithValue("@p_retest", _retest);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        // end.UPDATE_RETEST_TAGGING

        public void UPDATE_ETP_TAGGING(string appNum, bool etpchk)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spUPDATE_TAGGING", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "ETP");
                    cmd.Parameters.AddWithValue("@app_num", appNum);
                    cmd.Parameters.AddWithValue("@tagging", etpchk);
                    cmd.ExecuteNonQuery();
                }
                
            }
        }

        public void UPDATE_REASSESS_TAGGING(string appNum, bool reasses)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spUPDATE_TAGGING", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "RAS");
                    cmd.Parameters.AddWithValue("@app_num", appNum);
                    cmd.Parameters.AddWithValue("@tagging", reasses);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public sealed class TaggingStatus
        {
            public bool Etp { get; set; }
            public bool Reassess { get; set; }
        }

        public TaggingStatus GET_TAGGING_FROM_TRAIL(string appNum)
        {
            var result = new TaggingStatus { Etp = false, Reassess = false };

            using (var cn = new SqlConnection(CS))
            using (var cmd = new SqlCommand(
                "SELECT ETPStat, ReAsmntStat FROM Admission.App_Trail_TF WHERE AppNum=@app", cn))
            {
                cmd.Parameters.Add("@app", SqlDbType.NVarChar, 8).Value = appNum;
                cn.Open();
                using (var r = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (r.Read())
                    {
                        result.Etp = r["ETPStat"] != DBNull.Value && (bool)r["ETPStat"];
                        result.Reassess = r["ReAsmntStat"] != DBNull.Value && (bool)r["ReAsmntStat"];
                    }
                }
            }

            return result;
        }

        public DataTable DISPLAY_APPLICANT_RETEST_TYPES(string _appnum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_APPL_RETEST_TYPES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _appnum);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
        // end.DISPLAY_APPLICANT_RETEST_TYPES

        public DateTime GET_RETEST_SCHED(string _appnum)
        {
            DateTime d = DateTime.Now;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string qry = "SELECT TOP 1 DOR, TOR FROM Admission.App_Retest_TestTypes_RF WHERE AppNum = @p_appnum";
                using (SqlCommand cmd = new SqlCommand(qry, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@p_appnum", _appnum);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            DateTime d1 = Convert.ToDateTime(dr["DOR"].ToString());
                            DateTime d2 = Convert.ToDateTime(dr["TOR"].ToString());
                            d = new DateTime(d1.Year, d1.Month, d1.Day, d2.Hour, d2.Minute, d2.Second);
                        }
                    }
                }
            }

            return d;
        } // end.GET_RETEST_SCHED

        public bool CHECK_RETEST_EXIST(string _appnum, int _taken)
        {
            bool res = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("Admission.spCHECK_APPL_RETEST_RESULT_EXIST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _appnum);
                    cmd.Parameters.AddWithValue("@p_taken", _taken);
                    res = (bool)cmd.ExecuteScalar();
                }
            }
            return res;
        } // end.CHECK_RETEST_EXIST

        public RetestRecord GET_RETEST_RECORD(string _appnum, string _ttcode)
        {
            RetestRecord retestData = new RetestRecord();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spGET_APPL_RETEST_RECORD", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _appnum);
                    cmd.Parameters.AddWithValue("@p_ttcode", _ttcode);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            retestData = new RetestRecord
                            {
                                AppNum = dr["AppNum"].ToString(),
                                TTCODE = dr["TTCODE"].ToString(),
                                Score = Convert.ToDouble(dr["Score"].ToString()),
                                Result = Convert.ToDouble(dr["Result"].ToString()),
                                PrevGrades = Convert.ToDouble(dr["PrevGrades"].ToString()),
                                PrevResult = Convert.ToDouble(dr["PrevResult"].ToString()),
                                Assessment = Convert.ToDouble(dr["Assessment"].ToString()),
                                Overall = Convert.ToDouble(dr["Overall"].ToString())
                            };
                        }
                    }
                }
            }

            return retestData;
        } // end.GET_RETEST_RECORD


        public int GET_RETEST_TYPE_ID(RetestRecord _retestData)
        {
            int i = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string qry = "SELECT Id FROM Admission.App_Retest_TestTypes_RF WHERE AppNum = @p_appnum AND TTCODE = @p_ttcode AND Taken = 1";
                using (SqlCommand cmd = new SqlCommand(qry, cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@p_appnum", _retestData.AppNum);
                    cmd.Parameters.AddWithValue("@p_ttcode", _retestData.TTCODE);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            i = Convert.ToInt32(dr["Id"].ToString());
                        }
                    }
                }
            }

            return i;
        }

        public void INSERT_APPL_RETEST_SCORES(RetestRecord _retestData, string _userid)
        {
            int idRe_type = this.GET_RETEST_TYPE_ID(_retestData);
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spINSERT_APPL_RETEST_SCORES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _retestData.AppNum);
                    cmd.Parameters.AddWithValue("@p_typeid", idRe_type);
                    cmd.Parameters.AddWithValue("@p_score", _retestData.Score);
                    cmd.Parameters.AddWithValue("@p_result", _retestData.Result);
                    cmd.Parameters.AddWithValue("@p_userid", _userid);
                    cmd.ExecuteNonQuery();
                }
            }
        } // end.INSERT_APPL_RETEST_SCORES

        public void UPDATE_APPL_RETEST_SCORES(RetestRecord _retestData, string _userid)
        {
            int idRe_type = this.GET_RETEST_TYPE_ID(_retestData);
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spUPDATE_APPL_RETEST_SCORES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _retestData.AppNum);
                    cmd.Parameters.AddWithValue("@p_typeid", idRe_type);
                    cmd.Parameters.AddWithValue("@p_score", _retestData.Score);
                    cmd.Parameters.AddWithValue("@p_result", _retestData.Result);
                    cmd.Parameters.AddWithValue("@p_userid", _userid);
                    cmd.ExecuteNonQuery();
                }
            }
        } // end.UPDATE_APPL_RETEST_SCORES

        public void INSERT_APPL_RETEST_SUMMARY(RetestRecord _retestData, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spINSERT_APPL_RETEST_SUMMARY", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _retestData.AppNum);
                    cmd.Parameters.AddWithValue("@p_prevgrades", _retestData.PrevGrades);
                    cmd.Parameters.AddWithValue("@p_prevresult", _retestData.PrevResult);
                    cmd.Parameters.AddWithValue("@p_assessment", _retestData.Assessment);
                    cmd.Parameters.AddWithValue("@p_overall", _retestData.Overall);
                    cmd.Parameters.AddWithValue("@p_userid", _userid);
                    cmd.ExecuteNonQuery();
                }
            }
        } // end.INSERT_APPL_RETEST_SUMMARY

        public void UPDATE_APPL_RETEST_SUMMARY(RetestRecord _retestData, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spUPDATE_APPL_RETEST_SUMMARY", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _retestData.AppNum);
                    cmd.Parameters.AddWithValue("@p_prevgrades", _retestData.PrevGrades);
                    cmd.Parameters.AddWithValue("@p_prevresult", _retestData.PrevResult);
                    cmd.Parameters.AddWithValue("@p_assessment", _retestData.Assessment);
                    cmd.Parameters.AddWithValue("@p_overall", _retestData.Overall);
                    cmd.Parameters.AddWithValue("@p_userid", _userid);
                    cmd.ExecuteNonQuery();
                }
            }
        } // end.UPDATE_APPL_RETEST_SUMMARY

        public void UPDATE_APPL_TESTSUMMARY_BASED_RETEST(string _appnum, string _recommendation, string _remarks, bool _isUpdRetest, string _userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                //string qry = "UPDATE Admission.App_TestSummary_MF SET [OBSERVATION] = @p_remarks, [STATCODE] = @p_recommendation, [RETEST] = @p_retest, [DU] = GETDATE(), [USERCODE] = @p_userid WHERE [APPNUM] = @p_appnum";
                using(SqlCommand cmd = new SqlCommand("Admission.spUPDATE_APPL_TESTSUMMARY_BASED_RETEST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _appnum);
                    cmd.Parameters.AddWithValue("@p_remarks", _remarks);
                    cmd.Parameters.AddWithValue("@p_recommendation", _recommendation);
                    cmd.Parameters.AddWithValue("@p_updretest", _isUpdRetest);
                    cmd.Parameters.AddWithValue("@p_userid", _userid);
                    cmd.ExecuteNonQuery();
                }
            }
        } // end.UPDATE_APPL_TESTSUMMARY_BASED_RETEST

        public bool CHECK_APPL_RETEST_TYPES_EXIST(RetestRecord _retestData)
        {
            bool res = false;
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spCHECK_APPL_RETEST_TYPES_EXIST", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _retestData.AppNum);
                    cmd.Parameters.AddWithValue("@p_ttcode", _retestData.TTCODE);
                    res = (bool)cmd.ExecuteScalar();
                }
            }
            return res;
        } // end.CHECK_APPL_RETEST_TYPES_EXIST

        public void INSERT_APPL_RETEST_TYPES(RetestRecord _retestData, string _sy, string _userid, DateTime _dor, DateTime _tor)
        {
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Admission.spINSERT_APPL_RETEST_TYPES", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_appnum", _retestData.AppNum);
                    cmd.Parameters.AddWithValue("@p_ttcode", _retestData.TTCODE);
                    cmd.Parameters.AddWithValue("@p_sy", _sy);
                    cmd.Parameters.AddWithValue("@p_userid", _userid);
                    cmd.Parameters.AddWithValue("@p_dor", _dor);
                    cmd.Parameters.AddWithValue("@p_tor", _tor);
                    cmd.ExecuteNonQuery();
                }
            }
        } // end.INSERT_APPL_RETEST_TYPES

    }//END OF CLASS

    public class RetestRecord
    {
        public RetestRecord() { }
        public string AppNum { get; set; }
        public string TTCODE { get; set; }
        public double Score { get; set; }
        public double Result { get; set; }
        public double PrevGrades { get; set; }
        public double PrevResult { get; set; }
        public double Assessment { get; set; }
        public double Overall { get; set; }
    }


    public class TourSchedule: Applicant
    {
        public void DOWNLOAD_TOUR_SCHEDULES()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "[Admission].[spTRANS_TOUR_SCHED]";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "DWL");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_TOUR_SCHED_HEADER(DateTime dtstart, DateTime dtend,string vcode, string srccode, string tsstat)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Admission].[spTRANS_TOUR_SCHED]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "HDR");
                    cmd.Parameters.AddWithValue("@dtstart", dtstart);
                    cmd.Parameters.AddWithValue("@dtend", dtend);
                    cmd.Parameters.AddWithValue("@vcode", vcode);
                    cmd.Parameters.AddWithValue("@srccode", srccode);
                    cmd.Parameters.AddWithValue("@tsstat", tsstat);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_TOUR_SCHED_ATTENDEE(int hid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Admission].[spTRANS_TOUR_SCHED]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "ATD");
                    cmd.Parameters.AddWithValue("@hid", hid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }

        public DataTable GET_TOUR_SCHED_APPLICANT(int hid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Admission].[spTRANS_TOUR_SCHED]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "APD");
                    cmd.Parameters.AddWithValue("@hid", hid);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            return dt;
        }
    }




}//End of Namespace
