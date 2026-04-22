using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace SIMSBDAL
{
    public class AutoNumber: cBase
    {

       
        public int SERIESNUMBER { get; set; }


        public string ISAMSAPPNUMBER { get; set; }

        
        /* 
         APPLICANT NUMBER GENERATION
         */ 
        public string appNumber(string PREFIX)
        {
            SERIESNUMBER = 0;
            ISAMSAPPNUMBER = "";
            string PrefixCode = "";
            string AutoNumber = "";

            //try
            //{
                using (SqlConnection cn = new SqlConnection(CS))
                { 
                    string strSQL = "Select CodePrefix, Series from xSystem.AutoNumber_RF where codePrefix = '" + PREFIX + "'";
                    
                        using(SqlCommand cmd = new SqlCommand(strSQL,cn))
                        {
                            cn.Open();
                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {

                                while (dr.Read())
                                {
                                    PrefixCode = dr["CodePrefix"].ToString();

                                    if ((int)dr["Series"] > 0)
                                    {

                                        SERIESNUMBER = (int)dr["Series"] + 1;

                                        //format Applicant AutoNumber
                                        if (SERIESNUMBER > 999)
                                        {
                                            AutoNumber = PrefixCode + "-" + SERIESNUMBER;
                                            ISAMSAPPNUMBER = SERIESNUMBER.ToString();
                                        }

                                        else if (SERIESNUMBER > 99)
                                        {
                                            AutoNumber = PrefixCode + "-" + "0" + SERIESNUMBER;
                                            ISAMSAPPNUMBER = "0" + SERIESNUMBER.ToString();
                                        }

                                        else if (SERIESNUMBER > 9)
                                        {
                                            AutoNumber = PrefixCode + "-" + "00" + SERIESNUMBER;
                                            ISAMSAPPNUMBER = "00" + SERIESNUMBER.ToString();
                                        }

                                        else
                                        {
                                            AutoNumber = PrefixCode + "-" + "000" + SERIESNUMBER;
                                            ISAMSAPPNUMBER = "000" + SERIESNUMBER.ToString();
                                        }

                                    }

                                    else
                                    {
                                        SERIESNUMBER = SERIESNUMBER + 1;
                                        AutoNumber = PrefixCode + "-" + "000" + SERIESNUMBER;
                                        
                                        ISAMSAPPNUMBER = "000" + SERIESNUMBER.ToString();
                                    }
                                }

                            }

                        
                        }
                }

            //}

            //catch { 
            
            //}


            return AutoNumber;
        
        }
      
        private int displayLastSeries(string PREFIX)
        {
            int Series = 0;

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select Series from xSystem.AutoNumber_RF where codePrefix = '" + PREFIX + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        Series = (int)dr["Series"];
                    }
                }
            }

             return Series;
        }
     
        public void updateAppNumber(string PREFIX)
        {
            //Will Add +1 to series to update series number
            //int Series = displayLastSeries(PREFIX) + 1;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("Update xSystem.AutoNumber_RF SET series=" + SERIESNUMBER + " WHERE CodePrefix=@prefix", cn))
            {
                cmd.Parameters.AddWithValue("@prefix", PREFIX);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            
            }
        }


        public string studNumber(string PREFIX)
        {
            SERIESNUMBER = 0;
            string PrefixCode = "";
            string AutoNumber = "";

            //try
            //{
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select CodePrefix, Series from xSystem.AutoNumber_RF where codePrefix = '" + PREFIX + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            PrefixCode = dr["CodePrefix"].ToString();

                            if ((int)dr["Series"] > 0)
                            {

                                SERIESNUMBER = (int)dr["Series"] + 1;

                                //format Applicant AutoNumber
                                if (SERIESNUMBER > 999)
                                {
                                    AutoNumber = PrefixCode + "-" + SERIESNUMBER;
                                    
                                }

                                else if (SERIESNUMBER > 99)
                                {
                                    AutoNumber = PrefixCode + "-" + "0" + SERIESNUMBER;
                                    
                                }

                                else if (SERIESNUMBER > 9)
                                {
                                    AutoNumber = PrefixCode + "-" + "00" + SERIESNUMBER;
                                    
                                }

                                else
                                {
                                    AutoNumber = PrefixCode + "-" + "000" + SERIESNUMBER;
                                    
                                }

                            }

                            else
                            {
                                SERIESNUMBER = SERIESNUMBER + 1;
                                AutoNumber = PrefixCode + "-" + "000" + SERIESNUMBER;

                                
                            }
                        }

                    }


                }
            }

            //}

            //catch { 

            //}


            return AutoNumber;

        }

        public void updateStudNumber(string PREFIX)
        {
            //Will Add +1 to series to update series number
            //int Series = displayLastSeries(PREFIX) + 1;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("Update xSystem.AutoNumber_RF SET series=" + SERIESNUMBER + " WHERE CodePrefix=@prefix", cn))
                {
                    cmd.Parameters.AddWithValue("@prefix", PREFIX);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public string SiblingNumber(string PREFIX)
        {
            SERIESNUMBER = 0;
            string PrefixCode = "";
            string AutoNumber = "";

            //try
            //{
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select CodePrefix, Series from xSystem.AutoNumber_RF where codePrefix = '" + PREFIX + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            PrefixCode = dr["CodePrefix"].ToString();

                            if ((int)dr["Series"] > 0)
                            {

                                SERIESNUMBER = (int)dr["Series"] + 1;

                                //format Applicant AutoNumber
                                if (SERIESNUMBER > 9999)
                                {
                                    AutoNumber = PrefixCode + SERIESNUMBER;

                                }

                                else if (SERIESNUMBER > 999)
                                {
                                    AutoNumber = PrefixCode + "0" + SERIESNUMBER;

                                }

                                else if (SERIESNUMBER > 99)
                                {
                                    AutoNumber = PrefixCode + "00" + SERIESNUMBER;

                                }

                                else if (SERIESNUMBER > 9)
                                {
                                    AutoNumber = PrefixCode + "000" + SERIESNUMBER;

                                }

                                else
                                {
                                    AutoNumber = PrefixCode + "0000" + SERIESNUMBER;

                                }

                            }

                            else
                            {
                                SERIESNUMBER = SERIESNUMBER + 1;
                                AutoNumber = PrefixCode + "0000" + SERIESNUMBER;


                            }
                        }

                    }


                }
            }

            //}

            //catch { 

            //}


            return AutoNumber;

        }

        public void updateSiblingNumber(string PREFIX)
        {
            //Will Add +1 to series to update series number
            //int Series = displayLastSeries(PREFIX) + 1;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("Update xSystem.AutoNumber_RF SET series=" + SERIESNUMBER + " WHERE CodePrefix=@prefix", cn))
                {
                    cmd.Parameters.AddWithValue("@prefix", PREFIX);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
        }



        /*
         ===========================================
         ASSESSMENT - AREA
         ===========================================
        */

        /* 
       Assessment NUMBER GENERATION
        */
        public string assNumber(string PREFIX, string SY)
        {
            SERIESNUMBER = 0;
            ISAMSAPPNUMBER = "";
            string PrefixCode = "";
            string AutoNumber = "";

            //try
            //{
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select CodePrefix, Series from xSystem.AutoNumber_RF where codePrefix = '" + PREFIX + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            PrefixCode = dr["CodePrefix"].ToString();

                            if ((int)dr["Series"] > 0)
                            {

                                SERIESNUMBER = (int)dr["Series"] + 1;

                                //format Assessment Number
                                if (SERIESNUMBER > 999)
                                {
                                    //AutoNumber = PrefixCode + "-" + SERIESNUMBER;
                                    AutoNumber = PrefixCode + "-" + SY + SERIESNUMBER;
                                    // ISAMSAPPNUMBER = SERIESNUMBER.ToString();
                                    // AutoNumber = SY + SERIESNUMBER;
                                }

                                else if (SERIESNUMBER > 99)
                                {
                                    AutoNumber = PrefixCode + "-" + SY + "0" + SERIESNUMBER;
                                    //  ISAMSAPPNUMBER = "0" + SERIESNUMBER.ToString();
                                    //  AutoNumber = SY + SERIESNUMBER;
                                }

                                else if (SERIESNUMBER > 9)
                                {
                                    AutoNumber = PrefixCode + "-" + SY + "00" + SERIESNUMBER;
                                    //  ISAMSAPPNUMBER = "00" + SERIESNUMBER.ToString();
                                    //  AutoNumber = SY + SERIESNUMBER;
                                }

                                else
                                {
                                    AutoNumber = PrefixCode + "-" + SY + "000" + SERIESNUMBER;
                                    //   ISAMSAPPNUMBER = "000" + SERIESNUMBER.ToString();
                                    //AutoNumber = SY + SERIESNUMBER;
                                }

                            }

                            else
                            {
                                SERIESNUMBER = SERIESNUMBER + 1;
                                AutoNumber = PrefixCode + "-" + SY + "000" + SERIESNUMBER;

                                //  ISAMSAPPNUMBER = "000" + SERIESNUMBER.ToString();
                                // AutoNumber = SY + SERIESNUMBER;
                            }
                        }

                    }


                }
            }

            //}

            //catch { 

            //}


            return AutoNumber;

        }
        public void updateAssNumber(string PREFIX)
        {
            //Will Add +1 to series to update series number
            //int Series = displayLastSeries(PREFIX) + 1;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("Update xSystem.AutoNumber_RF SET series=" + SERIESNUMBER + " WHERE CodePrefix=@prefix", cn))
                {
                    cmd.Parameters.AddWithValue("@prefix", PREFIX);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public string GenerateClassCode(string PREFIX)
        {
            string cc = null;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM xSystem.AutoNumber_RF WHERE CodePrefix = @PREFIX", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@PREFIX", PREFIX);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            int last_num = Convert.ToInt32(dr["Series"].ToString());
                            cc = dr["CodePrefix"].ToString() + last_num.ToString("000000");
                        }
                    }
                }
            }

            return cc;
        }

        public void UpdateClassCodeSeries(string PREFIX)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE xSystem.AutoNumber_RF SET Series = Series + 1 WHERE CodePrefix = @PREFIX", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@PREFIX", PREFIX);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        //SAP AUTO INCREMENT - temporary for testing use

        //public string assignBPCode()
        //{
        //    SERIESNUMBER = 0;
        //    string bpcode = "";

        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        string strSQL = "Select CodePrefix, Series from xSystem.AutoNumber_RF where codePrefix = '" + PREFIX + "'";

        //        (string.Format("INSERT INTO [{0}](Code,Name,U_StudentNo,U_Name,U_Type,U_Description,U_Section,U_Dunning,U_Action,U_Processed,U_ForProcess,U_Level) " +
        //                        "VALUES (@code,@name,@stud_num,@u_name,@type,@desc,@sec,@dunning,@action,@process,@forprocess,@level)", "@FT_OCRD"), cn))

        //        using (SqlCommand cmd = new SqlCommand(string.Format("SELECT Code From", cn))
        //        {
        //            cn.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            if (dr.HasRows)
        //            {

        //                while (dr.Read())
        //                {
        //                    PrefixCode = dr["CodePrefix"].ToString();

        //                    if ((int)dr["Series"] > 0)
        //                    {

        //                        SERIESNUMBER = (int)dr["Series"] + 1;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return bpcode;

        //}
    }



}//END OF NAME SPACE
