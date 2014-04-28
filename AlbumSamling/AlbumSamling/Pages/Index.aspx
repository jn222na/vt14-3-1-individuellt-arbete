<%@ Page Language="C#" Title="Index" MasterPageFile="~/Pages/Master.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" ViewStateMode="Disabled" Inherits="AlbumSamling.Pages.Index" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>

        <asp:Label runat="server" ID="Statuslabel" Visible="false" Text=""></asp:Label>
        <asp:ListView ID="CustomerListView1" runat="server"
            ItemType="AlbumSamling.Model.CustomerProp"
            SelectMethod="CustomerListView_GetData"
            DeleteMethod="ContactListView_DeleteItem"
            InsertMethod="ContactFormView_InsertItem"
            InsertItemPosition="FirstItem"
            DataKeyNames="CustomerId">

            <LayoutTemplate>
                <table>
                    <tr>
                        <th>Förnamn
                        </th>
                        <th>Efternamn
                        </th>
                        <th>Ort
                        </th>
                        <th runat="server" id="itemPlaceholder">Ort
                        </th>
                        <td>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Pages/Album.aspx" runat="server">Se album</asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Pages/Phone.aspx" runat="server">Se Telefoner</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#: Item.Förnamn %>
                    </td>
                    <td>
                        <%#: Item.Efternamn %>
                    </td>
                    <td>
                        <%#: Item.Ort %>
                    </td>
                    <td>
                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# GetRouteUrl("IndexEditRoute", new {KundID = Item.CustomerId}) %>' runat="server">Redigera Kontakt</asp:HyperLink>
                    </td>
                    <td>
                        <asp:LinkButton ID="Delete" CommandName="Delete" runat="server" Text="Ta Bort" OnClientClick='<%# String.Format("return confirm(\"Ta bort namnet {0} {1}?\")", Item.Förnamn, Item.Efternamn) %>'></asp:LinkButton>
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
                        <asp:TextBox ID="Förnamn" runat="server" Text='<%# BindItem.Förnamn %>' MaxLength="50" />
                    </td>
                    <td>
                        <asp:TextBox ID="Efternamn" runat="server" Text='<%# BindItem.Efternamn %>' MaxLength="50" />

                    </td>
                    <td>
                        <asp:TextBox ID="Ort" runat="server" Text='<%# BindItem.Ort %>' MaxLength="50" />

                    </td>
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
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationGroup="Kund"
                        ControlToValidate="Ort" ErrorMessage="Ort: Text är enbart tillåtet"
                        ForeColor="Red" ValidationExpression="[a-zA-Z]+"> </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="OrtRequiredFieldValidator2" Visible="false" runat="server" ErrorMessage="Ort : Får inte vara tomt" ControlToValidate="Ort"></asp:RequiredFieldValidator>
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
