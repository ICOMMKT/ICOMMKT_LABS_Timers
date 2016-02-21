<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GetAutoRefreshedImage._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="ModalPanel" Visible="false">
        <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Image URL</h4>
                </div>
                <div class="modal-body">
                    <asp:Literal Text="" runat="server" id="UrlGenerated"/>
                </div>
            </div>
        </div>
    </div>
    </asp:Panel>
    <div class="jumbotron">
        <h1>Auto Refreshed Image Mails</h1>
    </div>
    <div class="row">
        <div>
            <h3>Select date and time for event</h3>
            <asp:TextBox runat="server" id="txt_Date" CssClass="datepicker"/>
            <asp:Button Text="Save Date" runat="server" CssClass="btn btn-primary" OnClick="SaveDateTime"/>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('.datepicker').datetimepicker({ dateFormat: "dd/mm/yy" });
            if($('.modal.fade').length > 0) {
                $('.modal.fade').modal();
            }
        });
    </script>
</asp:Content>
