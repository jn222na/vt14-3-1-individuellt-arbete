<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumEdit.aspx.cs" Inherits="AlbumSamling.Pages.AlbumEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
         <h1>
        Redigera Album
    </h1>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validation-summary-errors" />
    <asp:FormView ID="AlbumFormView" runat="server"
        ItemType="AlbumSamling.Model.AlbumProp"
        DataKeyNames="AlbumId"
        DefaultMode="Edit"
        RenderOuterTable="false"
        SelectMethod="AlbumFormView_GetData"
        UpdateMethod="AlbumFormView_UpdateItem">
        <EditItemTemplate>
            <div>
                    <label for="AlbumTitel">AlbumTitel</label>
                </div>
                <div>
                    <asp:TextBox ID="AlbumTitel" runat="server" Text='<%# BindItem.AlbumTitel %>' />
                </div>
                <div>
                    <label for="ArtistTitel">ArtistTitel</label>
                </div>
                <div>
                    <asp:TextBox ID="ArtistTitel" runat="server" Text='<%# BindItem.ArtistTitel %>' />
                </div>
                <div>
                    <label for="Utgivningsår">Utgivningsår</label>
                </div>
                <div>
                    <asp:TextBox ID="Utgivningsår" runat="server" Text='<%# BindItem.Utgivningsår %>' />
                </div>


                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Visible="false" runat="server" ErrorMessage="AlbumTitel : Får inte vara tomt" ControlToValidate="AlbumTitel"></asp:RequiredFieldValidator>
            
            
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Visible="false" runat="server" ErrorMessage="ArtistTitel : Får inte vara tomt" ControlToValidate="ArtistTitel"></asp:RequiredFieldValidator>
            
          
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Visible="false" runat="server" ErrorMessage="Utgivningsår : Får inte vara tomt" ControlToValidate="Utgivningsår"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <div>
                <asp:LinkButton ID="LinkButton1" runat="server" Text="Spara" CommandName="Update" />
                <asp:HyperLink ID="HyperLink1" runat="server" Text="Avbryt" NavigateUrl='<%# GetRouteUrl("Album", new { id = Item.AlbumID }) %>' />
            </div>
        </EditItemTemplate>
    </asp:FormView>
    </div>
    </form>
</body>
</html>
