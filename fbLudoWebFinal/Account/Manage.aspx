<%@ Page Title="Konto verwalten" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="fbLudoWebFinal.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>Kontoeinstellungen ändern</h4>
                <hr />
                <dl class="dl-horizontal">
                    <dt>Kennwort:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Change]" Visible="false" ID="ChangePassword" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Create]" Visible="false" ID="CreatePassword" runat="server" />
                    </dd>
                </dl>
            </div>
            <div class="form-horizontal">
                <h4>Persönliche Angaben:</h4>
                <hr />
                <dl class="dl-horizontal">
                    <dt>Anrede:</dt>
                    <dd>
                        <asp:TextBox runat="server" ID="Anrede" CssClass="form-control" value=""/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Anrede" CssClass="text-danger" ErrorMessage="Das Feld Anrede ist erforderlich." />
                    </dd>
                    <dt>Vorname:</dt>
                    <dd>
                        <asp:TextBox runat="server" ID="Vorname" CssClass="form-control" value=""/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Vorname" CssClass="text-danger" ErrorMessage="Das Feld Vorname ist erforderlich." />
                    </dd>
                    <dt>Nachname:</dt>
                    <dd>
                        <asp:TextBox runat="server" ID="Nachname" CssClass="form-control" value=""/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Nachname" CssClass="text-danger" ErrorMessage="Das Feld Nachname ist erforderlich." />
                    </dd>
                    <dt>PLZ:</dt>
                    <dd>
                        <asp:TextBox runat="server" ID="PLZ" CssClass="form-control" value=""/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="PLZ" CssClass="text-danger" ErrorMessage="Das Feld PLZ ist erforderlich." />
                        <asp:RangeValidator runat="server" ControlToValidate="PLZ" CssClass="text-danger" MinimumValue="1000" MaximumValue="9999" ErrorMessage="Der Wert dieses Feldes muss zwischen 1000 und 9999 liegen." />
                    </dd>
                    <dt>Ort:</dt>
                    <dd>
                        <asp:TextBox runat="server" ID="Ort" CssClass="form-control" value=""/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Ort" CssClass="text-danger" ErrorMessage="Das Feld Ort ist erforderlich." />
                    </dd>
                    <dt>Adresse:</dt>
                    <dd>
                        <asp:TextBox runat="server" ID="Adresse" CssClass="form-control" value=""/>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="Adresse" CssClass="text-danger" ErrorMessage="Das Feld Adresse ist erforderlich." />
                    </dd>
                
                    <asp:Button ID="SaveData" Text="Submit" OnClick="SaveData_Click" runat="server"/>
                </dl>
            </div>
        </div>
    </div>
</asp:Content>
