<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" ViewStateMode="Disabled" Inherits="AlbumSamling.Pages.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:Label runat="server" ID="Statuslabel" Visible="false" Text=""></asp:Label>
            <asp:ListView ID="CustomerListView1" runat="server"
                ItemType="AlbumSamling.Model.CustomerProp"
                SelectMethod="CustomerListView_GetData"
                DeleteMethod="ContactListView_DeleteItem"
                InsertMethod="ContactFormView_InsertItem"
                InsertItemPosition="FirstItem"
                DataKeyNames="KundID">


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
                        <td class="Detaljer">
                            <asp:LinkButton ID="Edit" runat="server"
                                 CommandArgument='<%#: Item.Förnamn +"  "+ Item.Efternamn + "  " + Item.Ort%> '
                                 OnCommand="Edit_Command"
                                 Text="Redigera"
                                ></asp:LinkButton>
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
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ErrorMessage="Förnamn måste anges" 
                                ControlToValidate="Förnamn" ValidationGroup="Insert" Display="None" >

                            </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="Efternamn" runat="server" Text='<%# BindItem.Efternamn %>'  MaxLength="50"/>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                 ErrorMessage="Efternamn måste anges" 
                                ControlToValidate="Efternamn" ValidationGroup="Insert" Display="None" >
                                 </asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="Ort" runat="server" Text='<%# BindItem.Ort %>' MaxLength="50" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ErrorMessage="Ort måste anges" 
                                ControlToValidate="Ort" ValidationGroup="Insert" Display="None"  >
                            </asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:LinkButton ID="Insert" runat="server" CommandName="Insert" Text="Lägg till"  />
                            <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                        </td>
                    </tr>
                </InsertItemTemplate>
            </asp:ListView>

        </div>
    </form>
</body>
</html>
