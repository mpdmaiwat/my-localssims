using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

//To Get Web Tools
using System.Web.UI;
//using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIMSBDAL
{
    public class xtra:cBase
    {
        //Getting Supplier List
        public DataTable GetSupplierList()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select SupplierCode, SupplierName from XTRA.Supplier_MD order by SupplierName ASC";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        //Method with Parameter  Supplier
        public void GetSupplierList(DropDownList dd)
        {
            DataTable dt = GetSupplierList();

            dd.DataSource = dt;
            dd.DataValueField = dt.Columns["SupplierCode"].ToString();
            dd.DataTextField = dt.Columns["SupplierName"].ToString();
            dd.DataBind();


        }

        //Getting Branch List
        public DataTable GetBranchList()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select BranchCode, BranchName from XTRA.Branch_MD order by BranchName ASC";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        public void GetBranchList(DropDownList dd)
        {
            DataTable dt = GetBranchList();

            dd.DataSource = dt;
            dd.DataValueField = dt.Columns["BranchCode"].ToString();
            dd.DataTextField = dt.Columns["BranchName"].ToString();
            dd.DataBind();

        }

        //Getting Item List
        public DataTable GetItemList()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select ItemCode, ItemName from XTRA.Item_MD order by ItemName ASC";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        //Method with Parameter ITEM
        public void GetItemList(DropDownList dd)
        {
            DataTable dt = GetItemList();

            dd.DataSource = dt;
            dd.DataValueField = dt.Columns["ItemCode"].ToString();
            dd.DataTextField = dt.Columns["ItemName"].ToString();
            dd.DataBind();


        }

         public void GENERIC_DROPDOWN(DropDownList dd, DataTable dt, string colValue, string colText)
        {
            DataTable datatable = dt;

            dd.DataSource = datatable;
            dd.DataTextField = dt.Columns[colValue].ToString();
            dd.DataTextField = dt.Columns[colText].ToString();
            dd.DataBind();
        }


         //INSERT INTO Result Slip Table 02-16-2016
         public void INSERT_INVENTORY_TRANS(string _supplierCode, string _transTypeCode, string _itemCode, string _uom, double _quantity, DateTime _dateTrans, string _userCode)
         {
             using (SqlConnection cn = new SqlConnection(CS))
             {

                 using (SqlCommand cmd = new SqlCommand("INSERT INTO XTRA.Inventory_TF(CustomerCode, TransTypeCode, ItemCode, Quantity, UOM, DateTrans, UserCode) " +
                                                        "VALUES(@CUSTOMERCODE, @TRANSTYPECODE, @ITEMCODE, @QUANTITY, @UOM, @DATETRANS, @USERCODE)", cn))
                 {
                    // cmd.CommandType = CommandType.StoredProcedure;


                     cmd.Parameters.AddWithValue("@CUSTOMERCODE", _supplierCode);
                     cmd.Parameters.AddWithValue("@TRANSTYPECODE", _transTypeCode);
                     cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                     cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
                     cmd.Parameters.AddWithValue("@UOM", _uom);
                     cmd.Parameters.AddWithValue("@DATETRANS", _dateTrans);
                     cmd.Parameters.AddWithValue("@USERCODE", _userCode);


                     cn.Open();

                     cmd.ExecuteNonQuery();

                 }
             }
         }

         public void INSERT_INVENTORY_TRANS_BRANCH(string _branchCode, string _transTypeCode, string _itemCode, string _uom, double _quantity, DateTime _dateTrans, string _userCode)
         {
             using (SqlConnection cn = new SqlConnection(CS))
             {

                 using (SqlCommand cmd = new SqlCommand("INSERT INTO XTRA.Inventory_TF(CustomerCode, TransTypeCode, ItemCode, Quantity, UOM, DateTrans, UserCode) " +
                                                        "VALUES(@CUSTOMERCODE, @TRANSTYPECODE, @ITEMCODE, @QUANTITY, @UOM, @DATETRANS, @USERCODE)", cn))
                 {
                     // cmd.CommandType = CommandType.StoredProcedure;


                     cmd.Parameters.AddWithValue("@CUSTOMERCODE", _branchCode);
                     cmd.Parameters.AddWithValue("@TRANSTYPECODE", _transTypeCode);
                     cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                     cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
                     cmd.Parameters.AddWithValue("@UOM", _uom);
                     cmd.Parameters.AddWithValue("@DATETRANS", _dateTrans);
                     cmd.Parameters.AddWithValue("@USERCODE", _userCode);


                     cn.Open();

                     cmd.ExecuteNonQuery();

                 }
             }
         }

         public void UPDATE_INVENTORY_MD(string _itemCode, double _quantity, DateTime _dateUpdate)
         {
             using (SqlConnection cn = new SqlConnection(CS))
             {

                 using (SqlCommand cmd = new SqlCommand("UPDATE XTRA.Inventory_MD " +
                                                        "SET InStock = InStock + @QUANTITY, RunningStock = InStock +  @QUANTITY, DateUpdate=@DATEUPDATE " +
                                                        "WHERE ItemCode=@ITEMCODE", cn))
                                                      
                 {
                     // cmd.CommandType = CommandType.StoredProcedure;


                     cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                     cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
                     cmd.Parameters.AddWithValue("@DATEUPDATE", _dateUpdate);
                    


                     cn.Open();

                     cmd.ExecuteNonQuery();

                 }
             }
         }

         public void UPDATE_INVENTORY_MD_BRANCH(string _itemCode, double _quantity, DateTime _dateUpdate)
         {
             using (SqlConnection cn = new SqlConnection(CS))
             {

                 using (SqlCommand cmd = new SqlCommand("UPDATE XTRA.Inventory_MD " +
                                                        "SET OutStock = OutStock + @QUANTITY, RunningStock = RunningStock -  @QUANTITY, DateUpdate=@DATEUPDATE " +
                                                        "WHERE ItemCode=@ITEMCODE", cn))
                 {
                     // cmd.CommandType = CommandType.StoredProcedure;


                     cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                     cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
                     cmd.Parameters.AddWithValue("@DATEUPDATE", _dateUpdate);



                     cn.Open();

                     cmd.ExecuteNonQuery();

                 }
             }
         }
    }
}
