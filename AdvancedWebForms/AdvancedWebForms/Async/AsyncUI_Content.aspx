<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AsyncUI_Content.aspx.cs" Inherits="AdvancedWebForms.Async.AsyncUI_Content" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:MultiView runat="server" ID="views" ActiveViewIndex="0">
            <asp:View runat="server">
                <%--Grid 1 content--%>
                <asp:GridView runat="server" SelectMethod="Grid1_GetData"></asp:GridView>
            </asp:View>

            <asp:View runat="server">
                <%--Grid 2 content--%>
                <asp:GridView runat="server" SelectMethod="Grid2_GetData"></asp:GridView>
            </asp:View>
        </asp:MultiView>
    </div>
    </form>
</body>
</html>
