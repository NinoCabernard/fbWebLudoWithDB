<%@ Page Title="Ausleihen Übersicht" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AusleihenÜbersicht.aspx.cs" Inherits="fbLudoWebFinal.AusleihenÜbersicht" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Ausgeliehene Spiele</h3>
    <asp:Table class="table table-striped table-dark" ID="tblAusleihenAktiv" runat="server" width="100%"></asp:Table>
    <h3>Zurückgegebene Spiele</h3>
    <asp:Table class="table table-striped table-dark" ID="tblAusleihenIaktiv" runat="server" width="100%"></asp:Table>
</asp:Content>
