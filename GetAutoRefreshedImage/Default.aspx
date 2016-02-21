<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GetAutoRefreshedImage._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Auto Refreshed Image Mails</h1>
    </div>
    <div class="row">
        <div>
            <h3>Select date and time for event</h3>
            <asp:TextBox runat="server" id="txt_Date" CssClass="datepicker"/>
            <button class="btn btn-primary">Save Date</button>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('.datepicker').datetimepicker({ dateFormat: "dd/mm/yy" });
        });
    </script>
</asp:Content>
