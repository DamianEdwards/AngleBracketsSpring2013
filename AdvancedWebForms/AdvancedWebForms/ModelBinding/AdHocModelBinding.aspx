<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdHocModelBinding.aspx.cs" Inherits="AdvancedWebForms.ModelBinding.AdHocModelBinding" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Ad-hoc Model Binding</h1>
    
    <hr />

    <h2>Create a widget</h2>

    <div class="form">
        <div class="form-group">
            <label for="Name">Name:</label>
            <div>
                <input type="text" name="Name" value="" id="Name" class="form-control" />
                <asp:ModelErrorMessage runat="server" ModelStateKey="Name" CssClass="text-danger" />
            </div>
        </div>

        <div class="form-group">
            <label for="Description" class="form-label">Description:</label>
            <input type="text" name="Description" value="" id="Description" class="form-control" />
            <asp:ModelErrorMessage runat="server" ModelStateKey="Description" CssClass="text-danger" />
        </div>

        <asp:Button Text="Submit" runat="server" OnClick="Submit_Click" CssClass="btn btn-primary" />

    </div>
</asp:Content>
