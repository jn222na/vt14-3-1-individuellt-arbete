<%@ Page Language="C#" Title="Phone"  MasterPageFile="~/Pages/Master.Master" AutoEventWireup="true" CodeBehind="Phone.aspx.cs" Inherits="AlbumSamling.Pages.Phone" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div>
            <asp:ListView ID="AlbumListView1" runat="server"
                ItemType="AlbumSamling.Model.PhoneProp"
                SelectMethod="PhoneListView_GetData">


                <LayoutTemplate>
                    <table>
                        <tr>
                            <th>
                                Förnamn
                            </th>
                            <th>TelefonNummer
                            </th>
                        </tr>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                              <%#: Item.Förnamn %>
                        </td>
                        <td>
                            <%#: Item.Number %>
                        </td>
                    </tr>
                    <th>
                        <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Pages/Index.aspx" runat="server">Se Kunder</asp:HyperLink>
                    </th>
                </ItemTemplate>

                <EmptyDataTemplate>
                    <%-- Detta visas då Telefoner saknas i databasen. --%>
                    <p>
                        Telefoner saknas.
                    </p>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
 </asp:Content>
