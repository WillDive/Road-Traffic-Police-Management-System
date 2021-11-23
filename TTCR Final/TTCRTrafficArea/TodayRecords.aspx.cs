﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

public partial class TTCRTrafficArea_TodayRecords : System.Web.UI.Page
{
    string CS = ConfigurationManager.ConnectionStrings["TTCRConnectionString1"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["USERNAME"] != null)
        {
            gertTodaysRecordsPerTraffic();
        }

        else
        {
            Response.Redirect("../Loginpage.aspx");
        }
    }

    private void gertTodaysRecordsPerTraffic()
    {
        string loggeduser = Session["USERNAME"] as string;
        string tody = DateTime.Now.ToShortDateString();
        spn123.InnerText = tody.ToString();
        using (SqlConnection con = new SqlConnection(CS))
        {
            SqlCommand cmd_TodayRecoPerTrfc = new SqlCommand("SELECT * FROM Records WHERE Traffi_Phone ='"+loggeduser+"' AND Date ='"+ tody + "'", con);
            SqlDataAdapter sda_TodayRecoPerTrfc = new SqlDataAdapter(cmd_TodayRecoPerTrfc);
            DataTable dt_TodayRecoPerTrfc = new DataTable();
            sda_TodayRecoPerTrfc.Fill(dt_TodayRecoPerTrfc);

            int TodayRecoPerTrfc = dt_TodayRecoPerTrfc.Rows.Count;
            spnTodayRecordsPerTraffic.InnerText = TodayRecoPerTrfc.ToString();

            float TodayRecoTotalIncomePerTraf = 0;
            for(int i = 0; i < TodayRecoPerTrfc; i++)
            {
                //float takevalue = 
                TodayRecoTotalIncomePerTraf = TodayRecoTotalIncomePerTraf + float.Parse(dt_TodayRecoPerTrfc.Rows[i][9].ToString());
            }
            spnTodayRecoTotalIncomePerTraf.InnerText = TodayRecoTotalIncomePerTraf.ToString();


            rptrtodaysRecordesPertraffic.DataSource = dt_TodayRecoPerTrfc;
            rptrtodaysRecordesPertraffic.DataBind();

        }
    }
}