<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SimpleSMSWebpage._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Text to Phone</h1>
        <p class="lead">Enter your message and phone number below and we'll send the message to you via SMS!</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <p>
                Message:
                <asp:TextBox ID="TextBoxMsg" runat="server" Height="50px" TextMode="MultiLine" Width="250px">Enter your message here</asp:TextBox>
            </p>
            <p>
                Phone number:
                <asp:TextBox ID="TextBoxPhone" runat="server" TextMode="Phone" ></asp:TextBox>
            </p>
            <p>
                <asp:Button ID="ButtonSend" runat="server" Text="Send" OnClick="ButtonSend_Click" />
            </p>
        </div>
    </div>

</asp:Content>
