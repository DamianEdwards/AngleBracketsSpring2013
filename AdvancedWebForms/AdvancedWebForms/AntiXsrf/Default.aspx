<%@ Page Title="" Language="C#" MasterPageFile="~/AntiXsrf/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdvancedWebForms.AntiXsrf.Default" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>Money Bank</h2>
    <asp:Panel runat="server" ID="Success" CssClass="alert alert-success" Visible="false">
        <asp:Literal runat="server" ID="SuccessMessage" />
    </asp:Panel>

    <div class="form-horizontal">
        <h4>Transfer Money</h4>

        <hr />

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="amount" CssClass="col-md-2 control-label">Amount: $</asp:Label>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="amount" CssClass="form-control" MaxLength="9" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="fromAccount" CssClass="col-md-2 control-label">From account:</asp:Label>
            <div class="col-md-4">
                <asp:DropDownList runat="server" ID="fromAccount" CssClass="form-control">
                    <asp:ListItem Value="select">-- select --</asp:ListItem>
                    <asp:ListItem Value="12345">Checking #12345</asp:ListItem>
                    <asp:ListItem Value="45678">Savings #45679</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="toAccountRoutingNumber" CssClass="col-md-2 control-label">To account routing #:</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="toAccountRoutingNumber" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="toAccountNumber" CssClass="col-md-2 control-label">To account number:</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="toAccountNumber" CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="Transfer_Click" Text="Transfer" CssClass="btn btn-primary" />
                <asp:Button runat="server" OnClick="Cancel_Click" Text="Cancel" CssClass="btn btn-default" />
            </div>
        </div>

    </div>
</asp:Content>
