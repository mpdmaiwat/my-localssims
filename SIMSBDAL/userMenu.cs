using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace SIMSBDAL
{
    public class userMenu: cBase 
    {

        public int MENUID { get; set; }
        public string MENUTEXT { get; set; }
        public string URL { get; set; }
        public int? PARENTID { get; set; }

        public List<userMenu> LIST { get; set; }



        public DataSet displayUserMenu(string _userid)
        {
            //DataSet ds = new DataSet();
            //string strSQL = "spUserMenuList";            
            //ds = queryCommandDS(strSQL);
            //return ds;

            // 2018.01.13
            // ==========================================================================
            //DataSet ds = new DataSet();
            //using (SqlConnection cn = new SqlConnection(CS))
            //{
            //    using (SqlCommand cmd = new SqlCommand("spGET_USER_MENU", cn))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@USERID", _userid);
                

            //        SqlDataAdapter da = new SqlDataAdapter();
            //        da.SelectCommand = cmd;
            //        da.Fill(ds);
            //    }
            //}
            // ==========================================================================
            
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSystem].[spGetUserMenu]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserCode", _userid);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    cn.Close();
                }
            }
            return ds;
        }
        
        public DataSet displayGeneralMenu()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from xSystem.Menus_test order by Arr", cn))
                //using (SqlCommand cmd = new SqlCommand("Select * from xSystem.Menus", cn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@UserCode", _UserCode);
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    cn.Close();
                }
            }
            return ds;
        }
        
        public DataTable displayMenuParent(int _parentID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from xSystem.Menus_Test where parentId='" + _parentID +"'", cn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@parentID", _parentID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GET_MENUS_FOR_CHANGELOGS()
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("select a.MenuID, a.URL from xSystem.Menus_test a inner join (Select MenuID,ParentID from xSystem.Menus_test where Pos='MID') b on b.MenuID=a.ParentID inner join (Select MenuID,ParentID,Arr from xSystem.Menus_test where Pos='TOP') c on c.MenuID=b.ParentID where a.Pos='BOT' and a.ParentID >0 order by c.Arr asc,a.Arr asc", cn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public string GET_MENU_INFORMATION(int menuid)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Select menu_info from xSystem.Menus_test where MenuID=" + menuid.ToString() + " ", cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            x = dr["menu_info"].ToString();
                        }
                    }
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_USER_MENU(string _uname, int _mid, int _cid, int _sid, string _ftrans = null)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Insert into xSystem.UserMenu_Access values(@userid, @menuid, @conid, @subid, @ftrans)";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.Parameters.AddWithValue("@userid", _uname);
                    cmd.Parameters.AddWithValue("@menuid", _mid);
                    cmd.Parameters.AddWithValue("@conid", _cid);
                    cmd.Parameters.AddWithValue("@subid", _sid);
                    cmd.Parameters.AddWithValue("@ftrans", _ftrans);

                    cn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DELETE_USER_MENU(string _uname, int _mid, int _cid, int _sid, string _ftrans = null)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM xSystem.UserMenu_Access WHERE UserCode = @userid AND MenuID = @mid AND ConID = @cid AND SubID = @sid AND Trans = @ftrans", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@userid", _uname);
                    cmd.Parameters.AddWithValue("@mid", _mid);
                    cmd.Parameters.AddWithValue("@cid", _cid);
                    cmd.Parameters.AddWithValue("@sid", _sid);
                    cmd.Parameters.AddWithValue("@ftrans", _ftrans);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DELETE_USERMENU(string _uname)
        {

            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Delete from xSystem.UserMenu_Access where UserCode= @userid";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@userid", _uname);
                        cn.Open();

                        cmd.ExecuteNonQuery();
                    }
                    catch { }//Message HERE 
                }
            }

        }

        public void DELETE_ATTRPTACSS(string _uname)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Delete from xSystem.UserAttendance_Access where UserID= @userid";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@userid", _uname);
                        cn.Open();

                        cmd.ExecuteNonQuery();
                    }
                    catch { }//Message HERE 
                }
            }
        }

        public void INSERT_ATTRPTACSS(string _uname, int _glid, int _secid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Insert into xSystem.UserAttendance_Access values(@userid, @lvlid, @secid)";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@userid", _uname);
                        cmd.Parameters.AddWithValue("@lvlid", _glid);
                        cmd.Parameters.AddWithValue("@secid", _secid);

                        cn.Open();

                        cmd.ExecuteNonQuery();
                    }
                    catch { }//Message HERE 
                }
            }
        }

        public static bool IsUserAccessExist(string userid, int sid, string trans)
        {
            bool r = false;
            using (SqlConnection cn = new SqlConnection(cBase.CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM xSystem.UserMenu_Access WHERE UserCode = @userid AND SubID = @sid AND Trans = @ftrans", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@userid", userid);
                    //cmd.Parameters.AddWithValue("@mid", mid);
                    //cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Parameters.AddWithValue("@sid", sid);
                    cmd.Parameters.AddWithValue("@ftrans", trans);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            r = true;
                        }
                        else
                        {
                            r = false;
                        }
                    }
                }
            }
            return r;
        }

        public static int GetMenuId(string url)
        {
            int i = 0;
            using(SqlConnection cn = new SqlConnection(CS))
            {
                using(SqlCommand cmd = new SqlCommand("SELECT MenuID FROM xSystem.Menus_test WHERE [URL] LIKE @url", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@url", "%" + url + "%");
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            i = Convert.ToInt32(dr["MenuID"].ToString());
                        }
                    }
                }
            }

            return i;
        }

        public bool GET_MENU_STATUS(int menuid)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT isActive FROM xSystem.Menus_test WHERE [MenuID]= @menuid", cn))
                {
                    cn.Open();
                    cmd.Parameters.AddWithValue("@menuid", menuid);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            x = Convert.ToBoolean(dr["isActive"].ToString());
                        }
                    }
                }
            }
            return x;
        }

        public void UPDATE_STATUS(int menuid, bool stat, string userid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Update xSystem.Menus_test set isActive=@stat, userid=@userid where MenuID=@menuid ";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.Parameters.AddWithValue("@menuid", menuid);
                    cmd.Parameters.AddWithValue("@stat", stat);
                    if(userid == "0")
                    {
                        cmd.Parameters.AddWithValue("@userid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@userid", userid);
                    }

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
