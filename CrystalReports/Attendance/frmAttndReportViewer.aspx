<script src="/crystalreportviewers13/js/crviewer/crv.js"></script>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAttndReportViewer.aspx.cs"
    Inherits="frmAttndReportViewer" Title="Report Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <fieldset style="text-align: left; background-color: White">
            <CR:CrystalReportViewer ID="CRV" runat="server" EnableDatabaseLogonPrompt="False"
                OnBeforeRender="CRV_BeforeRender" OnUnload="CRV_Unload" BorderColor="White" BorderWidth="0px"
                GroupTreeStyle-BackColor="White" GroupTreeStyle-BorderColor="White" GroupTreeStyle-BorderStyle="None"
                Height="50px" ToolbarStyle-BackColor="White" ToolbarStyle-BorderColor="White"
                Width="350px" HasToggleGroupTreeButton="False" ToolPanelView="None" />
        </fieldset>
    </div>
    </form>
</body>
</html>