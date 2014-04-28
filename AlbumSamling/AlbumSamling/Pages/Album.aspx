<%@ Page Language="C#" Title="Album" MasterPageFile="~/Pages/Master.Master" AutoEventWireup="true" CodeBehind="Album.aspx.cs" ViewStateMode="Disabled" Inherits="AlbumSamling.Pages.Album" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <asp:Label runat="server" ID="AlbumStatuslabel" Visible="false" Text=""></asp:Label>
        <asp:ListView ID="AlbumListView1" runat="server"
            ItemType="AlbumSamling.Model.AlbumProp"
            SelectMethod="AlbumListView_GetData"
            DeleteMethod="AlbumListView_DeleteItem"
            InsertMethod="AlbumFormView_InsertItem"
            InsertItemPosition="FirstItem"
            DataKeyNames="AlbumId">
            <LayoutTemplate>

                <table>
                    <tr>
                        <th>AlbumTitel
                        </th>
                        <th>ArtistTitel
                        </th>
                        <th>Utgivningsår
                        </th>
                        <th runat="server" id="itemPlaceholder">Ort
                        </th>
                        <td>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Pages/Index.aspx" runat="server">Se Kunder</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#: Item.AlbumTitel %>
                    </td>
                    <td>
                        <%#: Item.ArtistTitel %>
                    </td>
                    <td>
                        <%#: Item.Utgivningsår %>
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# GetRouteUrl("AlbumEditRoute", new {AlbumID = Item.AlbumID}) %>' runat="server">Redigera Album</asp:HyperLink>
                    </td>
                    <td>
                        <asp:LinkButton ID="Delete" CommandName="Delete" runat="server" Text="Ta Bort" OnClientClick='<%# String.Format("return confirm(\"Ta bort Albumet {0}?\")", Item.AlbumTitel) %>'></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>

            <EmptyDataTemplate>
                <%-- Detta visas då kunder saknas i databasen. --%>
                <p>
                    Kunder saknas.
                </p>
            </EmptyDataTemplate>

            <InsertItemTemplate>
                <tr>

                    <td>
                        <asp:TextBox ID="AlbumTitel" runat="server" Text='<%# BindItem.AlbumTitel %>' MaxLength="50" />
                    </td>
                    <td>
                        <asp:TextBox ID="ArtistTitel" runat="server" Text='<%# BindItem.ArtistTitel %>' MaxLength="50" />
                    </td>
                    <td><asp:TextBox ID="Utgivningsår" runat="server" Text='<%# BindItem.Utgivningsår %>' MaxLength="50" />

                    </td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="Album"
                        ControlToValidate="AlbumTitel" ErrorMessage="AlbumTitel: Text är enbart tillåtet"
                        ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Visible="false" runat="server" ErrorMessage="AlbumTitel : Får inte vara tomt" ControlToValidate="AlbumTitel"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationGroup="Album"
                        ControlToValidate="ArtistTitel" ErrorMessage="ArtistTitel: Text är enbart tillåtet"
                        ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="OrtRequiredFieldValidator2" Visible="false" runat="server" ErrorMessage="ArtistTitel : Får inte vara tomt" ControlToValidate="ArtistTitel"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Kund"
                        ControlToValidate="Utgivningsår" ErrorMessage="Utgivningsår: Text är enbart tillåtet"
                        ForeColor="Red" ValidationExpression="[0-9]+"> </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Visible="false" runat="server" ErrorMessage="Utgivningsår : Får inte vara tomt" ControlToValidate="Utgivningsår"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                    <td>
                        <asp:LinkButton ID="Insert" runat="server" CommandName="Insert" Text="Lägg till" />
                        <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                    </td>
                </tr>
            </InsertItemTemplate>
        </asp:ListView>

    </div>
</asp:Content>
