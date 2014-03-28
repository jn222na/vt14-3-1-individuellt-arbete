<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Album.aspx.cs" Inherits="AlbumSamling.Pages.Album" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Album</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ListView ID="ListView1" runat="server"
                ItemType="AlbumSamling.AlbumProp">
                <LayoutTemplate>
                    <table>
                        <tr>

                            <th>AlbumTitel
                            </th>
                            <th>AlbumTypID
                            </th>
                            <th>ArtistTitel
                            </th>
                        </tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        
                        <td>
                            <%#: Item.AlbumTitel %>
                        </td>
                        <td>
                            <%#: Item.AlbumTypID %>
                        </td>
                        <td>
                            <%#: Item.ArtistTitel %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
