﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class KPI_EmpKPIEntry : System.Web.UI.Page
{
    DBConnector objDB = new DBConnector();
    DataTable dtScoring = new DataTable();
    KPIManager objKpi = new KPIManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfIsUpdate.Value = "N";
            dtScoring.Rows.Clear();
            dtScoring.Dispose();
            grEmpKpi.DataSource = null;
            grEmpKpi.DataBind();
            Common.EmptyTextBoxValues(this);
            lblMsg.Text = "";
            Common.FillDropDownList(objKpi.SelectQuarter(0), ddlQuarter, "QuarterName", "QuarterId", false);
            Common.FillDropDownList(objKpi.SelectGroup(0), ddlGroup, "GroupName", "GroupId", false);
            this.EntryMode(false);
            this.OpenRecord();

        }
    }

    protected void EntryMode(bool IsUpdate)
    {
        if (IsUpdate == true)
        {
            btnSave.Text = "Update";
            hfIsUpdate.Value = "Y";
        }
        else
        {
            btnSave.Text = "Save";
            hfIsUpdate.Value = "N";
        }
    }

    private void OpenRecord()
    {
        //dtScoring = objKpi.SelectScoring(0);
        //grEmpKpi.DataSource = dtScoring;
        //grEmpKpi.DataBind();
    }

    private void SaveData(string IsDelete)
    {
        long lngID = 0;
        try
        {
            if (hfIsUpdate.Value == "N")
                lngID = objDB.GerMaxIDNumber("KPIEmpkpiMst", "EKPIID");
            else
                lngID = Convert.ToInt32(hfId.Value);

            
            clsCommonSetup objCommonSetup = new clsCommonSetup(lngID.ToString(), "", (chkInActive.Checked == true ? "N" : "Y"), "N",
                Session["USERID"].ToString(), Common.SetDateTime(DateTime.Now.ToString()), "N", IsDelete);

            //objKpi.InsertEmpKpiData(grEmpKpi, ddlMonth.SelectedValue.Trim(), ddlYear.SelectedValue.Trim(),
            //Common.ReturnDate(txtIssueDate.Text.Trim()), Session["USERID"].ToString().Trim(), Common.SetDateTime(DateTime.Now.ToString()));


            objKpi.InsertEmpKpiData(objCommonSetup, 
                                    grEmpKpi, 
                                    ddlGroup.SelectedValue.ToString(),
                                    ddlQuarter.SelectedValue.ToString(),
                                    txtYear.Text.ToString(),
                                    txtEmpID.Text.ToString().Trim(),
                                    hfIsUpdate.Value,
                                    IsDelete);

            if (hfIsUpdate.Value == "N")
                lblMsg.Text = "Record Saved Successfully";
            else
                lblMsg.Text = "Record Updated Successfully";
            Common.EmptyTextBoxValues(this);
            this.EntryMode(false);
            this.OpenRecord();
        }
        catch (Exception ex)
        {
            lblMsg.Text = "";
            throw (ex);
        }
    }

    public String NullOrEmpty(string s)
    {
        if (String.IsNullOrEmpty(s))
            return "0";
        else
            return s;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
         if (txtEmpID.Text.ToString().Trim()=="" || txtYear.Text.ToString().Trim()=="")
         {
             lblMsg.Text="Employee ID or Year can't be Empty !";
             return;
         }
         else
        this.SaveData("N");
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Common.EmptyTextBoxValues(this);
        this.EntryMode(false);
        this.OpenRecord();
    }

    //protected void grScoring_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grEmpKpi.PageIndex = e.NewPageIndex;
    //    this.OpenRecord();
    //}

    //protected void grScoring_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    GridView _gridView = (GridView)sender;
    //    // Get the selected index and the command name
    //    int _selectedIndex = int.Parse(e.CommandArgument.ToString());
    //    string _commandName = e.CommandName;
    //    _gridView.SelectedIndex = _selectedIndex;
    //    switch (_commandName)
    //    {
    //        case ("DoubleClick"):
    //            //ScoringID,GroupId,QuarterId,MarksF,MarksTo

    //            txtEmpID.Text = grScoring.SelectedRow.Cells[5].Text;
    //            hfId.Value = grScoring.DataKeys[_gridView.SelectedIndex].Values[0].ToString();
    //            ddlGroup.SelectedValue = grScoring.DataKeys[_gridView.SelectedIndex].Values[1].ToString().Trim();
    //            ddlQuarter.SelectedValue = grScoring.DataKeys[_gridView.SelectedIndex].Values[2].ToString().Trim();              
    //            txtYear.Text = grScoring.SelectedRow.Cells[3].Text;

    //            this.EntryMode(true);
    //            break;
    //    }
    //}

   

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hfId.Value) == false)
        {
            this.SaveData("Y");
        }
        else
        {
            lblMsg.Text = "Select a Scoring first from the list then try to delete.";
        }

        this.EntryMode(false);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        // ddlGroup.SelectedValue.ToString(), ddlQuarter.SelectedValue.ToString(), NullOrEmpty(txtYear.Text.ToString())
        grEmpKpi.DataSource = null;
        grEmpKpi.DataBind();

        dtScoring = objKpi.SelectKPIList(ddlGroup.SelectedValue.ToString(), ddlQuarter.SelectedValue.ToString(), NullOrEmpty(txtYear.Text.ToString()),"0");
        grEmpKpi.DataSource = dtScoring;
        grEmpKpi.DataBind();

    }
    protected void cmdFind_Click(object sender, EventArgs e)
    {
        // ddlGroup.SelectedValue.ToString(), ddlQuarter.SelectedValue.ToString(), NullOrEmpty(txtYear.Text.ToString())

        grEmpKpi.DataSource = null;
        grEmpKpi.DataBind();

        dtScoring = objKpi.SelectKPIList(ddlGroup.SelectedValue.ToString(), ddlQuarter.SelectedValue.ToString(), NullOrEmpty(txtYear.Text.ToString()), txtEmpID.Text.ToString());
        grEmpKpi.DataSource = dtScoring;
        grEmpKpi.DataBind();

    }
    

}