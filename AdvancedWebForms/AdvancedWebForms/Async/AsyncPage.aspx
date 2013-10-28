<%@ Page Title="Async Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsyncPage.aspx.cs" Inherits="AdvancedWebForms.Async.AsyncPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Async Page</h1>

    <hr />

    <h2>Results</h2>

    <asp:Repeater runat="server" ID="results">
        <ItemTemplate>
            <div><%#: Container.DataItem %></div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
