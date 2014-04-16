<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexEdit.aspx.cs" ViewStateMode="Disabled" Inherits="AlbumSamling.Pages.IndexEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:Label runat="Server" ID="Label1" Visible="false" Text=""></asp:Label>
    <form id="form1" runat="server">
        <h1>Redigera kund
        </h1>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validation-summary-errors" />
        <asp:FormView ID="CustomerFormView" runat="server"
            ItemType="AlbumSamling.Model.CustomerProp"
            DataKeyNames="CustomerId"
            DefaultMode="Edit"
            RenderOuterTable="false"
            SelectMethod="CustomerFormView_GetData"
            UpdateMethod="CustomerFormView_UpdateItem">
            <EditItemTemplate>
                <div>
                    <label for="Förnamn">Förnamn</label>
                </div>
                <div>
                    <asp:TextBox ID="Förnamn" runat="server" Text='<%# BindItem.Förnamn %>' />
                </div>
                <div>
                    <label for="Efternamn">Efternamn</label>
                </div>
                <div>
                    <asp:TextBox ID="Efternamn" runat="server" Text='<%# BindItem.Efternamn %>' />
                </div>
                <div>
                    <label for="Ort">Ort</label>
                </div>
                <div>
                    <asp:TextBox ID="Ort" runat="server" Text='<%# BindItem.Ort %>' />
                </div>
                <div>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Spara" CommandName="Update" />
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Tillbaka" NavigateUrl='<%# GetRouteUrl("Index") %>' />
                </div>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="Kund"
                    ControlToValidate="Förnamn" ErrorMessage="Förnamn: Text är enbart tillåtet"
                    ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Visible="false" runat="server" ErrorMessage="Förnamn : Får inte vara tomt" ControlToValidate="Förnamn"></asp:RequiredFieldValidator>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="Kund"
                    ControlToValidate="Efternamn" ErrorMessage="Efternman: Text är enbart tillåtet"
                    ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="EfternamnRequiredFieldValidator" Visible="false" runat="server" ErrorMessage="Efternamn : Får inte vara tomt" ControlToValidate="Efternamn"></asp:RequiredFieldValidator>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Kund"
                    ControlToValidate="Ort" ErrorMessage="Ort: Text är enbart tillåtet"
                    ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="OrtRequiredFieldValidator2" Visible="false" runat="server" ErrorMessage="Ort : Får inte vara tomt" ControlToValidate="Ort"></asp:RequiredFieldValidator>
               
            </EditItemTemplate>
        </asp:FormView>
    </form>
</body>
</html>
