using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SIMSBDAL
{
    public class Inventory : cBase
    {
        public DataTable GET_ORDERS_MF(string sselect=null, string swhere = null, string group = null, string sorder = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_ORDER_MF]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", sselect);
                    cmd.Parameters.AddWithValue("@where", swhere);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_ITEMS_MF(string sselect = null, string swhere = null, string group = null, string sorder = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_ITEMS_MF]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@select", sselect);
                    cmd.Parameters.AddWithValue("@where", swhere);
                    cmd.Parameters.AddWithValue("@group", group);
                    cmd.Parameters.AddWithValue("@order", sorder);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void CANCEL_ORDERS(string ordernum,string remarks, DateTime dtcancel)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_CANCEL_ORDER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ordernum", ordernum);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@dtcancel", dtcancel);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void SAVE_ORDERS(string ordernum,string sy,string pymtmode, double amtdue, int empid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "OS");
                    cmd.Parameters.AddWithValue("@ordernum", ordernum);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@pymtmode", pymtmode);
                    cmd.Parameters.AddWithValue("@amtdue", amtdue);
                    if (empid == 0)
                    {
                        cmd.Parameters.AddWithValue("@empid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@empid", empid);
                    }
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DEDUCT_ITEM_QUANTITY(string itemCode, string qty, string location)
        {
            //var sql = @"UPDATE Item 
            //        SET Item.Qty = ISNULL(Item.Qty, 0) - ISNULL(@Qty, 0)
            //        FROM Inventory.Order_MF as OrderMF
            //        INNER JOIN Inventory.Order_Item_MF as OrderItem on OrderMF.Id = OrderItem.order_id
            //        INNER JOIN Inventory.Item_Master_MF as Item on OrderItem.item_id = Item.ID
            //        WHERE OrderMF.order_num = @order_num and Item.Id = @ItemId";

            //var sql = @"UPDATE Inventory.Item_Master_MF SET Qty = ISNULL(Qty, 0) - ISNULL(@Qty, 0)
            //           WHERE ItemCode = @ItemCode";  
            //using (SqlConnection cn = new SqlConnection(CS))
            //{
            //    using (SqlCommand cmd = new SqlCommand(sql, cn))
            //    {
            //        cn.Open();
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Parameters.AddWithValue("@ItemCode", itemCode);
            //        cmd.Parameters.AddWithValue("@Qty", qty);
            //        cmd.ExecuteNonQuery();
            //        cn.Close();
            //    }
            //}


            var sql = @" UPDATE Inventory.Item_Warehouse SET Qty = ISNULL(Qty, 0) - ISNULL(@Qty, 0)
                        WHERE ItemCode = @ItemCode AND Location=@Location";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ItemCode", itemCode);
                    cmd.Parameters.AddWithValue("@Qty", qty);
                    cmd.Parameters.AddWithValue("@Location", location);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }

            /* This is temporary */
            //sql = @"update  [Inventory].[Item_Master_MF] set qty = 500";
            //using (SqlConnection cn = new SqlConnection(CS))
            //{
            //    using (SqlCommand cmd = new SqlCommand(sql, cn))
            //    {
            //        cn.Open();
            //        cmd.CommandType = CommandType.Text;
            //        cmd.ExecuteNonQuery();
            //        cn.Close();
            //    }
            //}
        }

        public int GET_ITEM_QTY_IN_SAP(string itemCode, string whCode)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_ITEM_QTY_IN_SAP]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@itemCode", itemCode);
                    cmd.Parameters.AddWithValue("@whCode", whCode);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }

            return x;
        }

        public DataTable GET_ITEM_WAREHOUSE(string itemCode, string location)
        {
            DataTable dt = new DataTable();
            var sql = @"SELECT * FROM Inventory.Item_Warehouse 
                        WHERE ItemCode = @ItemCode AND Location = @Location";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ItemCode", itemCode);
                    cmd.Parameters.AddWithValue("@Location", location);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void SAVE_NEW_ITEM_ORDER(string ordernum,int itemid,int qty, double totalprice, string itemstat, string itemremarks)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "IN");
                    cmd.Parameters.AddWithValue("@ordernum", ordernum);
                    cmd.Parameters.AddWithValue("@itemid", itemid);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@totalprice", totalprice);
                    cmd.Parameters.AddWithValue("@itemstat", itemstat);
                    cmd.Parameters.AddWithValue("@itemremarks", itemremarks);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_ITEM_ORDER(int id, int qty, double totalprice, string itemstat, string itemremarks)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "IU");
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@totalprice", totalprice);
                    cmd.Parameters.AddWithValue("@itemstat", itemstat);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@itemremarks", itemremarks);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_OLD_ITEM_ORDER(int id, string itemstat, string itemremarks, int ciqty, DateTime dtreturn)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "IA");
                    cmd.Parameters.AddWithValue("@itemstat", itemstat);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@itemremarks", itemremarks);
                    cmd.Parameters.AddWithValue("@ciqty", ciqty);
                    cmd.Parameters.AddWithValue("@dtreturn", dtreturn);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_ORDER_AMOUNT(string ordernum, double amtdue)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "OA");
                    cmd.Parameters.AddWithValue("@ordernum", ordernum);
                    cmd.Parameters.AddWithValue("@amtdue", amtdue);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public string INSERT_NEW_ORDER(string studnum, string locip,string sy, string pymtmode, double amtdue, int empid,string orderstat, string consolordernum)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "OI");
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cmd.Parameters.AddWithValue("@locip", locip);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@pymtmode", pymtmode);
                    cmd.Parameters.AddWithValue("@amtdue", amtdue);
                    if (empid == 0)
                    {
                        cmd.Parameters.AddWithValue("@empid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@empid", empid);
                    }
                    cmd.Parameters.AddWithValue("@orderstat", orderstat);
                    cmd.Parameters.AddWithValue("@consolordernum", consolordernum);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public string INSERT_ORDER_SAP(string ordernum,string schyear)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spINSERT_ORDER_SAP]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ordernum", ordernum);
                    cmd.Parameters.AddWithValue("@schyear", schyear);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void CHECK_QTY_SAP_ORDERNUM(string ordernum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spCHECK_QTY_SAP_ORDERNUM]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ordernum", ordernum);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void CHECK_QTY_LOCAL_ORDERNUM(string ordernum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spCHECK_QTY_LOCAL_ORDERNUM]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ordernum", ordernum);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_ORDER(string ordernum, string sy, string pymtmode, double amtdue, int empid, string orderstat,string consolordernum)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "OU");
                    cmd.Parameters.AddWithValue("@ordernum", ordernum);
                    cmd.Parameters.AddWithValue("@sy", sy);
                    cmd.Parameters.AddWithValue("@pymtmode", pymtmode);
                    cmd.Parameters.AddWithValue("@amtdue", amtdue);
                    if (empid == 0)
                    {
                        cmd.Parameters.AddWithValue("@empid", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@empid", empid);
                    }
                    cmd.Parameters.AddWithValue("@orderstat", orderstat);
                    cmd.Parameters.AddWithValue("@consolordernum", consolordernum);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_ITEM_QTY_STAT(int itemid, int qtyneed)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spCHECK_ITEM_QTY_STAT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@itemid", itemid);
                    cmd.Parameters.AddWithValue("@qtyneed", qtyneed);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public DataTable GET_PACKAGES(string where)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_PACKAGE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_ITEM_PACKAGES(string where)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_PACKAGE_ITEMS]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_ITEM_TYPE(string where)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_ITEM_TYPE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GET_FOR_ENCODE_INVENTORY(DateTime FromDate, int itype, string location)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_FOR_ENCODE_INVENTORY]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", FromDate);
                    cmd.Parameters.AddWithValue("@itype", itype);
                    cmd.Parameters.AddWithValue("@location", location);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public string INSERT_NEW_PACKAGE(string packname, string lvlcode, string strand, string isCLVE)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_PACKAGE]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@qtype", "NP");
                    cmd.Parameters.AddWithValue("@packname", packname);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    if (strand == "0")
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    if (isCLVE == "NONE")
                    {
                        cmd.Parameters.AddWithValue("@isCLVE", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isCLVE", isCLVE);
                    }
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_NEW_ITEM_PACKAGE(string packcode, int itemid,string packname,string lvlcode, string strand, string isCLVE)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_PACKAGE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@qtype", "NI");
                    cmd.Parameters.AddWithValue("@packcode", packcode);
                    cmd.Parameters.AddWithValue("@itemid", itemid);
                    cmd.Parameters.AddWithValue("@packname", packname);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    if (strand == "0")
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    if (isCLVE == "NONE")
                    {
                        cmd.Parameters.AddWithValue("@isCLVE", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isCLVE", isCLVE);
                    }
                    cmd.Parameters.AddWithValue("@maxQty", 1);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_PACKAGE(string packcode, string packname, string lvlcode, string strand, string isCLVE)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_PACKAGE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@qtype", "UP");
                    cmd.Parameters.AddWithValue("@packcode", packcode);
                    cmd.Parameters.AddWithValue("@packname", packname);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    if (strand == "0")
                    {
                        cmd.Parameters.AddWithValue("@strand", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@strand", strand);
                    }
                    if (isCLVE == "NONE")
                    {
                        cmd.Parameters.AddWithValue("@isCLVE", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isCLVE", isCLVE);
                    }
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void DELETE_ITEM_PACKAGE(string packcode, int itemid)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_PACKAGE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@qtype", "RI");
                    cmd.Parameters.AddWithValue("@packcode", packcode);
                    cmd.Parameters.AddWithValue("@itemid", itemid);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_MAX_QTY(string packcode, int itemid, int maxQty)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_PACKAGE]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@qtype", "UQ");
                    cmd.Parameters.AddWithValue("@packcode", packcode);
                    cmd.Parameters.AddWithValue("@itemid", itemid);
                    cmd.Parameters.AddWithValue("@maxQty", maxQty);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public int GET_AVAILABLE_QTY_ORDER(string itemcode,int qtyneed,string lvlcode, string studnum)
        {
            int x = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spAVAILABLE_QTY_ORDER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Parameters.AddWithValue("@qtyneed", qtyneed);
                    cmd.Parameters.AddWithValue("@lvlcode", lvlcode);
                    cmd.Parameters.AddWithValue("@studnum", studnum);
                    cn.Open();
                    x = (int)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void CHECK_CONNECTION_ORDER()
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CC");
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public bool CHECK_ONLINE_ORDER_AVAILABLE(string refnum)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "CO");
                    cmd.Parameters.AddWithValue("@refnum", refnum);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public string DOWNLOAD_DATA_ONLINE(string refnum, string locip)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "DL");
                    cmd.Parameters.AddWithValue("@refnum", refnum);
                    cmd.Parameters.AddWithValue("@locip", locip);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public bool CHECK_ASSESS_STUDNUM_ONLINE(string refnum)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GS");
                    cmd.Parameters.AddWithValue("@refnum", refnum);
                    cn.Open();
                    x = (bool)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public string GET_STUDNUM_ONLINE(string refnum)
        {
            string x = "";
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_TRANS_ORDER]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "GN");
                    cmd.Parameters.AddWithValue("@refnum", refnum);
                    cn.Open();
                    x = (string)cmd.ExecuteScalar();
                    cn.Close();
                }
            }
            return x;
        }

        public void INSERT_ITEM(string itemcode, string desc, string shortd, int type, bool forsale, bool isactive, int critstock, bool ismonitoring)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_ITEMS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "N");
                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@shortd", shortd);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@forsale", forsale);
                    cmd.Parameters.AddWithValue("@isactive", isactive);
                    cmd.Parameters.AddWithValue("@critstock", critstock);
                    cmd.Parameters.AddWithValue("@ismonitoring", ismonitoring);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void UPDATE_ITEM(string itemcode, string desc, string shortd, int type, bool forsale, bool isactive, int id, int critstock, bool ismonitoring)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_ITEMS]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transtype", "U");
                    cmd.Parameters.AddWithValue("@itemcode", itemcode);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@shortd", shortd);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@forsale", forsale);
                    cmd.Parameters.AddWithValue("@isactive", isactive);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@critstock", critstock);
                    cmd.Parameters.AddWithValue("@ismonitoring", ismonitoring);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public DataTable GET_INV_STAT(string where)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spGET_INV_STAT]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@where", where);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public void INSERT_DAILY_REPORT(int itemid, DateTime invdate,int unrelease, int actual, string userid, string location)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_INVENTORY_REPORT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intype", "IN");
                    cmd.Parameters.AddWithValue("@itemid", itemid);
                    cmd.Parameters.AddWithValue("@invdate", invdate);
                    cmd.Parameters.AddWithValue("@unrelease", unrelease);
                    cmd.Parameters.AddWithValue("@actual", actual);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        //public void UPDATE_DAILY_REPORT(int id, int unrelease, int actual, string userid)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_INVENTORY_REPORT]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@intype", "U");
        //            cmd.Parameters.AddWithValue("@unrelease", unrelease);
        //            cmd.Parameters.AddWithValue("@actual", actual);
        //            cmd.Parameters.AddWithValue("@userid", userid);
        //            cmd.Parameters.AddWithValue("@id", id);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}

        public void UPDATE_GW_ACTUAL_INV(int itemid, DateTime invdate, int actualgw, string userid, string location)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_INVENTORY_REPORT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intype", "GW");
                    cmd.Parameters.AddWithValue("@itemid", itemid);
                    cmd.Parameters.AddWithValue("@invdate", invdate);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@actualgw", actualgw);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        public void INSERT_INVENTORY_REMARKS(int itemid, DateTime invdate, string userid, string invremarks, string location)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_INVENTORY_REPORT]", cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@intype", "RM");
                    cmd.Parameters.AddWithValue("@itemid", itemid);
                    cmd.Parameters.AddWithValue("@invdate", invdate);
                    cmd.Parameters.AddWithValue("@invremarks", invremarks);
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        //di na to gagamitin
        //public void UPDATE_INVENTORY_REMARKS(int id, string userid, string invremarks)
        //{
        //    using (SqlConnection cn = new SqlConnection(CS))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("[Inventory].[spTRANS_INVENTORY_REPORT]", cn))
        //        {
        //            cn.Open();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@intype", "RU");
        //            cmd.Parameters.AddWithValue("@invremarks", invremarks);
        //            cmd.Parameters.AddWithValue("@userid", userid);
        //            cmd.Parameters.AddWithValue("@id", id);
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}


    }
}
