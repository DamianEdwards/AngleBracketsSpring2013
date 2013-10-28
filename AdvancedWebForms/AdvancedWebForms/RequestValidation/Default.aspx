<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdvancedWebForms.RequestValidation.Default" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Some stuff from the query string</h1>

    <div>
        <asp:Label runat="server" ID="output" />
    </div>
</asp:Content>
