<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AsyncUI.aspx.cs" Inherits="AdvancedWebForms.Async.AsyncUI" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        iframe {
            display: none;
            border: none;
        }
 
            iframe.loaded {
                display: block;
            }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Async UI Loading</h1>
    
    <hr />

    <h3>Static Content</h3>
    <p>I am static content and will load immediately!</p>

    <h3>Peons (2s query)</h3>
    <div>
        <p class="loading">loading peons...</p>
        <iframe src="AsyncUI_Content?view=0"></iframe>
    </div>

    <h3>Manager Types (5s query)</h3>
    <div>
        <p class="loading">loading managers...</p>
        <iframe src="AsyncUI_Content?view=1"></iframe>
    </div>

    <script>
        var iframes = document.querySelectorAll("iframe"),
            iframe;

        for (var i = 0; i < iframes.length; i++) {
            iframe = iframes[i];
            iframe.addEventListener("load", (function (f) {
                return function () {
                    // Remove the loading message <p>
                    f.parentNode.removeChild(f.previousElementSibling);

                    // Show the iframe
                    f.className = "loaded";
                };
            })(iframe), false);
        }
    </script>
</asp:Content>
