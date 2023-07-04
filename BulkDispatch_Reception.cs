using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VIJAYADESKTOP
{
    
    public partial class BulkDispatch_Reception : Form
    {
        String query = String.Empty;
        DataSet ds;
        String gblId;

        public BulkDispatch_Reception()
        {
            InitializeComponent();
        }

        private void BulkDispatch_Reception_Load(object sender, EventArgs e)
        {
            this.MaximumSize = MaxMinSize.getmax();
            this.MinimumSize = MaxMinSize.getMin();
            getLoad();
            GetGrid();
        }
        
        private void btn_Save_Click(object sender, EventArgs e)
        {
            //-------------Saves  data in the Bulk Dispatch Table for further operations(Testing,Sealing)----------------//
            try
            {
                if (txt_Dispatchto.Text != "")
                {
                    int chkEmp = ValidateEmpty();
                    if (chkEmp == 0)
                    {
                        return;
                    }
                    int i = Convert.ToInt32(Common1.Common("SP_INSUP_X_BULK_DISPATCH_Rec", "@value", "@BMILKDISPx_ID", "@BMILKDISPx_LOCATIONm_Id", "@BMILKDISPx_CUSTm_Id", "@BMILKDISPx_WEBRm_VehicleID", "@BMILKDISPx_PRDm_Id", "@BMILKDISPx_SILOm_Id", "@BMILKDISPx_MDISPm_Id", "@BMILKDISPx_CompNo", "@BMILKDISPx_DDate", "@BMILKDISPx_CreatedBy", "@BMILKDISPx_UpdatedBy","", "", "", "", "", "", "", "", "", "", "", "Insert", "0", Convert.ToString(Program.GV.LocId), Convert.ToString(cmb_CCode.SelectedValue), cmb_VehicleId.Text, Convert.ToString(cmb_ProductCode.SelectedValue),Convert .ToString (cmb_Silo .SelectedValue ), Convert.ToString(cmb_ModeofDispatch.SelectedValue),"1", Convert.ToString(dtp1.Value), Convert.ToString(Program.GV.UserId), Convert.ToString(Program.GV.UserId),"", "", "", "", "", "", "", "", "", "", "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", 10, "0", "0"));
                    if (i != -1)
                    {
                        MessageBox.Show("Data Saved Successfully ", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetGrid();
                        getLoad();
                        ClearAll();
                    }
                    else
                    {
                        MessageBox.Show("Duplicate records found ", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please Check This Customer Doesn't Consists City!", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }
        private void btn_Update_Click(object sender, EventArgs e)
        {
            //-------------Update the data of the chosen WB VehicleId----------------//
            try
            {
                int chkEmp = ValidateEmpty();
                if (chkEmp == 0)
                {
                    return;
                }
                int i = Convert.ToInt32(Common1.Common("SP_INSUP_X_BULK_DISPATCH_Rec", "@value", "@BMILKDISPx_ID", "@BMILKDISPx_LOCATIONm_Id", "@BMILKDISPx_CUSTm_Id", "@BMILKDISPx_WEBRm_VehicleID", "@BMILKDISPx_PRDm_Id", "@BMILKDISPx_MDISPm_Id", "@BMILKDISPx_DDate", "@BMILKDISPx_CreatedBy", "@BMILKDISPx_UpdatedBy", "", "", "", "", "", "", "", "", "", "", "", "", "", "Update", "0", Convert.ToString(Program.GV.LocId), Convert.ToString(cmb_CCode.SelectedValue), txt_VehicleId.Text, Convert.ToString(cmb_ProductCode.SelectedValue), Convert.ToString(cmb_ModeofDispatch.SelectedValue), Convert.ToString(dtp1.Value), Convert.ToString(Program.GV.UserId), Convert.ToString(Program.GV.UserId), "", "", "", "", "", "", "", "", "", "", "", "", "0", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "0", 10, "0", "0"));
                if (i != -1)
                {
                    MessageBox.Show("Data Updated Successfully ", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetGrid();
                    getLoad();
                    ClearAll();
                    btn_Update.Visible = false;
                    btn_Save.Visible = true;
                    cmb_VehicleId.Visible = true;
                    txt_VehicleId.Visible = false;
                    gblId = "";
                }
                else
                {
                    //MessageBox.Show("No records found ", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearAll();
            btn_Update.Visible = false;
            btn_Save.Visible = true;
            txt_VehicleId.Visible = false;
            cmb_VehicleId.Visible = true;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_CCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_VehicleId.Text == "System.Data.DataRowView")
            {
                return;
            }
            fillNameAddress(Convert.ToString(cmb_CCode.SelectedValue));   
        }

        private void cmb_ProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_ProductCode.Text == "System.Data.DataRowView")
            {
                return;
            }
            fillProductName(Convert.ToString(cmb_ProductCode.SelectedValue));
        }

        private void cmb_VehicleId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_VehicleId.Text == "System.Data.DataRowView")
            {
                return;
            }
            fillTruckNo(cmb_VehicleId.Text);
            cmb_CCode.DataSource = null;
            txt_CName.Text = "";
            txt_Address.Text = "";
            getCustomerCode(cmb_VehicleId.Text);
        }

        private void dgvBulkDRecep_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //-------------On EDIT Click Get all the values depending on the WB VehicleID----------------//
            if (e.RowIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == 0)
            {
                if (dgvBulkDRecep.Rows[e.RowIndex].Cells[9].Value == System.DBNull.Value || dgvBulkDRecep.Rows[e.RowIndex].Cells[9].Value == "")
                {
                    getBulkDispatchRecById(Convert.ToString(dgvBulkDRecep.Rows[e.RowIndex].Cells[1].Value));
                }
                else
                {
                    MessageBox.Show("This vehicle has left from the Plant!!!", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //getBulkDispatchRecById(Convert.ToString(dgvBulkDRecep.Rows[e.RowIndex].Cells[1].Value));
                btn_Update.Visible = true;
                btn_Save.Visible = false;
                txt_VehicleId.Visible = true;
                cmb_VehicleId.Visible = false;
                gblId = Convert.ToString(dgvBulkDRecep.Rows[e.RowIndex].Cells[1].Value);
            }
        }
        protected void getBulkDispatchRecById(String strId)
        {
            try
            {
                IDataReader dr = DB.GetRS("Sp_GetBulkDispatchRecById  " + strId + "");
                while (dr.Read())
                {
                    getCustomerCode(strId);
                    txt_VehicleId.Text = dr["BMILKDISPx_WEBRm_VehicleID"].ToString();
                    txt_TruckNo.Text = dr["VEHICLEm_Number"].ToString();
                    dtp1.Value = Convert.ToDateTime(dr["BMILKDISPx_DDate"].ToString());
                    cmb_CCode.Text = dr["CUSTm_Code"].ToString();
                    cmb_ProductCode.Text = dr["PRDm_Code"].ToString();
                    cmb_ModeofDispatch.Text = dr["MDISPm_Name"].ToString();
                    txt_CName.Text = dr["CUSTm_Name"].ToString();
                    txt_Address.Text = dr["CUSTm_Address"].ToString();
                    txt_ProductName.Text = dr["PRDm_Name"].ToString();

                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }
        protected void getLoad()
        {
            // CUSTm_Code
            getWBVehicleId();
            //getCustomerCode();
            getMDispatch();
            getProduct();
            getSource();
            
        }
        protected void getMDispatch()
        {
            //-------------Fill the Mode of Dispatch dropdown----------------//
            try
            {
                query = "Select MDISPm_Id ,MDISPm_Name from M_ModeOfDisaptch";// where Based on Location and ";
                ds = new DataSet();
                ds = DB.GetDS(query, "Table", false, DateTime.Now.AddHours(1));
                cmb_ModeofDispatch.DataSource = ds.Tables[0];
                cmb_ModeofDispatch.DisplayMember = "MDISPm_Name";
                cmb_ModeofDispatch.ValueMember = "MDISPm_Id";
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }
        protected void getCustomerCode(String strId)
        {
            //-------------Fill the Customer Code dropdown----------------//
            try
            {
                query = "Select CUSTm_Id,(CUSTm_Code +':'+ CUSTm_Name) as CUSTm_Code  from M_Customer";// Where CUSTm_ROUTm_Id = (Select WEBRm_ROUTm_Id From M_Weighbridge Where WEBrm_VehicleId = '" + strId + "')";// where Based on Location and ";
                ds = new DataSet();
                ds = DB.GetDS(query, "Table", false, DateTime.Now.AddHours(1));
                cmb_CCode.DataSource = ds.Tables[0];
                cmb_CCode.DisplayMember = "CUSTm_Code";
                cmb_CCode.ValueMember = "CUSTm_Id";
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }
        protected void getProduct()
        {
            //-------------Fill the Product Code dropdown----------------//
            try
            {
                string sql = "Select PRDm_Id,PRDm_Code from M_Product Where PRDm_PRDGm_Id = (Select PRDGm_Id From M_ProductGroup Where PRDGm_Name='Dairy Products')";
                //string sql = "Select PRDm_Id,PRDm_Code from M_Product Where PRDm_PRDGm_Id = (Select PRDGm_Id From M_ProductGroup Where PRDGm_Name='Type Of Milk')";
                DataSet ds = DB.GetDS(sql, "Table", false, DateTime.Now.AddHours(1));
                cmb_ProductCode.DataSource = ds.Tables[0];
                cmb_ProductCode.ValueMember = "PRDm_Id";
                cmb_ProductCode.DisplayMember = "PRDm_Code";
                ds.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR:" + e.Message);
            }
        }
        protected void getWBVehicleId()
        {
            //-------------Fill the WB VehicleId DropDown----------------//
            try
            {
                query = "Select WEBRm_VehicleID from M_WeighBridge Where WEBRm_Purpose = 'Loading' And (WEBRm_NetWt is null Or WEBRm_NetWt = '') And WEBRm_VehicleID Not In (Select BMILKDISPx_WEBRm_VehicleID from X_BULK_DISPATCH) And WEBRm_VehicleID Not In (Select DISPxp_WEIGHxp_Id from X_Dispatch_P) AND WEBRm_LOCATIONID = '" + Convert.ToString(Program.GV.LocId) + "' ";// where Based on Location and ";
                ds = new DataSet();
                ds = DB.GetDS(query, "Table", false, DateTime.Now.AddHours(1));
                cmb_VehicleId.DataSource = ds.Tables[0];
                cmb_VehicleId.DisplayMember = "WEBRm_VehicleID";
                cmb_VehicleId.ValueMember = "WEBRm_VehicleID";
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }
        private void GetGrid()
        {
            //--------------Fills the Grid with data of Vehicles who are still in the Dairy----------------//
            try
            {
                query = "SELECT Top(100) ";
                query = query + "BMILKDISPx_WEBRm_VehicleID, ";
                query = query + "(Select VEHICLEm_Number From M_Vehicle as V Where VEHICLEm_Id = (Select WEBRm_VEHICLEm_Id From M_WeighBridge as WB Where WB.WEBRm_VehicleID = BD.BMILKDISPx_WEBRm_VehicleID)) as VEHICLEm_Number, ";
                query = query + "(Select CUSTm_Code from M_Customer as C Where C.CUSTm_Id = BD.BMILKDISPx_CUSTm_Id) as CUSTm_Code, ";
                query = query + "(Select CUSTm_Name from M_Customer as C Where C.CUSTm_Id = BD.BMILKDISPx_CUSTm_Id) as CUSTm_Name, ";
                query = query + "(Select PRDm_Code from M_Product as P Where P.PRDm_Id = BD.BMILKDISPx_PRDm_Id) as PRDm_Code, ";
                query = query + "(Select PRDm_Name from M_Product as P Where P.PRDm_Id = BD.BMILKDISPx_PRDm_Id) as PRDm_Name, ";
                query = query + "(Select MDISPm_Name from M_ModeOfDisaptch as MD Where MD.MDISPm_Id = BD.BMILKDISPx_MDISPm_Id) as MDISPm_Name, ";
                query = query + "BMILKDISPx_DDate, ";
                query = query + "(Select WEBRm_NetWt From M_Weighbridge as WB Where WB.WEBRm_VehicleId = BD.BMILKDISPx_WEBRm_VehicleID) As NetWt ";
                query = query + "FROM X_BULK_DISPATCH as BD ";
                query = query + "WHERE 1=1 ";
                //query = query + "AND BMILKDISPx_WEBRm_VehicleID In (Select WEBRm_VehicleID From M_WeighBridge Where WEBRm_NetWt Is Null) ";
                //query = query + "AND IsNull(BMILKDISPx_Status,0) <> 1 ";
                query = query + " AND BMILKDISPx_LOCATIONm_Id = '" + Convert.ToString(Program.GV.LocId) + "' ";
                query = query + "ORDER BY BMILKDISPx_WEBRm_VehicleID DESC ";
        
                DataSet ds = DB.GetDS(query, "Table", false, DateTime.Now.AddHours(1));
                dgvBulkDRecep.DataSource = ds.Tables[0];

                dgvBulkDRecep.Columns[1].HeaderText = "Vehicle Id";
                dgvBulkDRecep.Columns[2].HeaderText = "Vehicle Number";
                dgvBulkDRecep.Columns[3].HeaderText = "Customer Code";
                dgvBulkDRecep.Columns[4].HeaderText = "Customer Name";
                dgvBulkDRecep.Columns[5].HeaderText = "Product Code";
                dgvBulkDRecep.Columns[6].HeaderText = "Product Name";
                dgvBulkDRecep.Columns[7].HeaderText = "Mode of Dispatch";
                dgvBulkDRecep.Columns[8].HeaderText = "Dispatch Date";

                dgvBulkDRecep.Columns[9].Visible = false;
                ds.Dispose();
                if (Program.GV.IsRCEPTIONEdit == true)
                {
                    dgvBulkDRecep.Columns[0].Visible = true;
                }
                else
                {
                    dgvBulkDRecep.Columns[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }
        protected void getSource()
        {
            //-------------Fill the Source(Silo) DropDown----------------//
            try
            {
                string sql = "SELECT SILOm_Id,(SILOm_Tag+' : '+SILOm_Description) as SILOm_Tag FROM M_Silo ";
                DataSet ds = DB.GetDS(sql, "Table", false, DateTime.Now.AddHours(1));
                cmb_Silo.DataSource = ds.Tables[0];
                cmb_Silo.DisplayMember = "SILOm_Tag";
                cmb_Silo.ValueMember = "SILOm_Id";
                ds.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR:" + e.Message);
            }
        }
        protected void fillNameAddress(String Code)
        {
            //-------------Gets the Customer Name and Address depending on the Customer Code Selected----------------//
            try
            {
                if (cmb_CCode.SelectedValue == null)
                {
                    return;
                }
                string cmbDataType = Convert.ToString(cmb_CCode.SelectedValue.GetType());
                if (cmbDataType == "System.Data.DataRowView")
                {
                    return;
                }
                query = "Select CUSTm_Name,CUSTm_Address,CUSTm_City from M_Customer where CUSTm_Id='" + Code + "'";
                IDataReader dr = DB.GetRS(query);
                while (dr.Read())
                {
                    txt_Address.Text = dr[1].ToString();
                    txt_CName.Text = dr[0].ToString();
                    txt_Dispatchto.Text  = dr[2].ToString();
                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }
        protected void fillProductName(String Code)
        {
            //-------------Gets the Product Name depending on the Product Code Selected----------------//
            try
            {
                if (cmb_ProductCode.SelectedValue == null)
                {
                    return;
                }
                string cmbDataType = Convert.ToString(cmb_ProductCode.SelectedValue.GetType());
                if (cmbDataType == "System.Data.DataRowView")
                {
                    return;
                }
                query = "Select PRDm_Name from M_Product Where PRDm_Id='" + Code + "'";
                IDataReader dr = DB.GetRS(query);
                while (dr.Read())
                {
                    txt_ProductName.Text = dr["PRDm_Name"].ToString();
                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }
        protected void fillTruckNo(String Id)
        {
            //-------------Gets the Vehicle Number depending on the VehicleID Selected----------------//
            try
            {
                query = "Select VEHICLEm_Number From M_Vehicle Where VEHICLEm_Id = (Select WEBRm_VEHICLEm_Id From M_WeighBridge Where WEBRm_VehicleID = '" + Id + "')";
                IDataReader dr = DB.GetRS(query);
                while (dr.Read())
                {
                    txt_TruckNo.Text = dr["VEHICLEm_Number"].ToString();
                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                Console.Write("ERROR" + ex.Message);
            }
        }
        protected void ClearAll()
        {
            //-------------Clear all the Data----------------//
            cmb_CCode.SelectedIndex = -1;
            txt_CName.Text = "";
            txt_Address.Text = "";
            dtp1.Value = System.DateTime.Now;
            cmb_ModeofDispatch.SelectedIndex = -1;
            cmb_ProductCode.SelectedIndex = -1;
            txt_ProductName.Text = "";
            cmb_VehicleId.SelectedIndex = -1;
            txt_TruckNo.Text = "";
            txt_VehicleId.Text = "";
            cmb_VehicleId.SelectedIndex = -1;
        }

        public int ValidateEmpty()
        {
            //-------------Validate Compulsory Fields----------------//
            if (txt_VehicleId.Text == "")
            {
                if (cmb_VehicleId.Text == "")
                {
                    MessageBox.Show("All fields are compulsory", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }
            }
            if (cmb_CCode.Text == "")
            {
                MessageBox.Show("All fields are compulsory", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            if (cmb_ProductCode.Text == "")
            {
                MessageBox.Show("All fields are compulsory", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            if (dtp1.Text == "")
            {
                MessageBox.Show("All fields are compulsory", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            if (cmb_ModeofDispatch.Text == "")
            {
                MessageBox.Show("All fields are compulsory", "Vijaya Dairy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            return 1;
        }

       

       

    }
}