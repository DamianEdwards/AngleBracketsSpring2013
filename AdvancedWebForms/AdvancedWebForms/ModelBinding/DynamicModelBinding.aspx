<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DynamicModelBinding.aspx.cs" Inherits="AdvancedWebForms.ModelBinding.DynamicModelBinding" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ad-hoc Dynamic Model Binding Anonymous Magic</h2>
    <hr />

    <h3>Widget from query string</h3>

    <ul>
        <li>Name: <%: Model.Name %></li>
        <li>Description: <%: Model.Description %></li>
        <li>Price: <%: Model.Price %></li>
    </ul>

    <h4>Model State Errors</h4>
    <asp:ValidationSummary runat="server" ShowModelStateErrors="true" ShowValidationErrors="false" />
</asp:Content>
