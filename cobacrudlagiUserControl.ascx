<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cobacrudlagiUserControl.ascx.cs" Inherits="cobacrudlagi.cobacrudlagi.cobacrudlagiUserControl" %>
<script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.3.1.min.js"></script>
<style type="text/css">
    .auto-style1 {
        width: 269px;
    }  

    .menuread {
        position: relative;
        margin: auto;
    }

        .menuread table {
            position: relative;
            margin: auto;
            margin-top: 5px;
            width: 100%;
            height: 100%;
            text-align: center;
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
        }

        .menuread td, .menuread tr {
            border: 1px solid #ddd;
            padding: 8px;
        }

            .menuread tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .menuread tr:hover {
                background-color: #ddd;
            }

        .menuread th {
            padding-top: 12px;
            padding-bottom: 12px;
            background-color: #4CAF50;
            color: white;
        }

        .menuread input, .menuadd input, .menudelete input {
            position: relative;
            margin-left: 25%;
            background-color: #4CAF50; /* Green */
            border: none;
            color: white;
            padding: 8px 16px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            -webkit-transition-duration: 0.4s; /* Safari */
            transition-duration: 0.4s;
            cursor: pointer;
        }

            .menuadd input[value=Submit], .menudelete input[value=Delete] {
                margin-left: 25%;
                background-color: white;
                color: black;
                border: 2px solid #4CAF50;
            }

            .menuread input[value=Create],.menuread input[value=Edit], .menuupdate input[value=Update] {
                background-color: white;
                color: black;
                border: 2px solid #4CAF50;
            }

                .menuread input[value=Create]:hover, .menuadd input[value=Submit]:hover, .menudelete input[value=Delete]:hover,.menuread input[value=Edit]:hover, .menuupdate input[value=Update]:hover {
                    background-color: #4CAF50;
                    color: white;
                }

            .menuread input[value=Delete] {
                background-color: white;
                color: black;
                border: 2px solid #f44336;
            }

            .menuadd input[value=Cancel], .menudelete input[value=Cancel], .menuupdate input[value=Cancel] {
                margin-left: 33%;
                background-color: white;
                color: black;
                border: 2px solid #f44336;
            }

            .menuadd input[value=Cancel], .menuadd input[value=Submit], .menudelete input[value=Cancel], .menudelete input[value=Delete],.menuupdate input[value=Cancel] {
                margin-left: 0%;
            }

                .menuread input[value=Delete]:hover, .menuadd input[value=Cancel]:hover, .menudelete input[value=Cancel]:hover,.menuupdate input[value=Cancel]:hover {
                    background-color: #f44336;
                    color: white;
                }

    .menuadd table, .menuupdate table {
        margin: auto;
        border-radius: 5px;
        background-color: #f2f2f2;
        padding: 20px;
    }

    .menuadd input[type=text], .menuupdate input[type=text] {
        width: 100%;
        background-color: lightgrey;
        padding: 12px 20px;
        margin: 8px 0;
        display: inline-block;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
    }
    .erroradd{
        color:red;        
    }
    .erroredit{
        color:red;        
    }
</style>
<script>
    function panggilAdd() {
        $(".menuread").hide();
        $(".menuadd").show();
    }
    //function panggilDelete() {
    //    $(".menudelete").show();
    //    $(".menuread").hide();
    //}
    function panggilCancel() {
        $(".menuread").show();
        $(".menudelete").hide();
        $(".menuadd").hide();
        $(".menuupdate").hide();
        $(".pilih").val("");
        $(".textboxadd").val("");
        $(".erroredit").val("");
        $(".erroradd").val("");
        $(".coba").val("");
    }
    function panggilEdit() {
        $(".menuupdate").show();
        $(".menuread").hide();
        $(".pilih").val("edit");
    }
    $(document).ready(function () {
        if ($(".pilih").val() == "") {
            $(".menuadd").hide();
            $(".menuupdate").hide();
            $(".menudelete").hide();
        } else if($(".pilih").val()=="edit") {            
            $(".menuupdate").show();
            $(".menuread").hide();
            $(".menuadd").hide();
        }                
        if ($(".coba").val() == "addsalah") {
            panggilAdd();
        }
        else if ($(".coba").val() == "editsalah") {
            panggilEdit;
        }
        //if ($(".coba").val() == "delete") {
        //    panggilDelete();
        //}
    });
</script>
<%--create--%>
<div class="menuadd">
    <h2>Masukkan Data</h2>
    <table>
        <tr>
            <td class="auto-style1">Title:</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="textboxadd"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style1">Nama:</td>
            <td>
                <asp:TextBox ID="txtNama" runat="server" CssClass="textboxadd"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style1">ID Karyawan:
            </td>
            <td>
                <asp:TextBox ID="txtID" runat="server" CssClass="textboxadd"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Tipe Karyawan ID:</td>
            <td>
                <asp:TextBox ID="txtTipeKID" runat="server" CssClass="textboxadd"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td>
                <input type="button" onclick="panggilCancel()" value="Cancel" id="Button1" runat="server" />
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /><br />
                <asp:Label ID="lblResult" runat="server" Text="" CssClass="erroradd"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<%--read--%>
<div class="menuread">
    <h2>Display SharePoint List Items</h2>
    <input type="button" onclick="panggilAdd()" value="Create" id="tombolAdd" runat="server" />
<%--    <asp:Button runat="server" Text="Export to Excel" OnClick="Unnamed1_Click"  OnClientClick="_spFormOnSubmitCalled = false;_spSuppressFormOnSubmitWrapper=true;"  />--%>
    <%--    <input type="button" onclick="panggilDelete()" value="Delete" id="tombolDelete" runat="server" />--%>
    <asp:GridView ID="gvSelectedColumnListData" OnRowCommand="rowcommand" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false" AllowPaging="True" AllowCustomPaging="True" DataKeyNames="ID_Karyawan" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="ID_Karyawan" HeaderText="ID" />
            <asp:BoundField DataField="Nama" HeaderText="Nama" />
            <asp:BoundField DataField="Tipe_Karyawan" HeaderText="Tipe Karyawan" />
            <%--<asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <input type="button" onclick="panggilEdit()" value="Edit" id="tombolEdit" runat="server"/>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:ButtonField CommandName="editRecord" ButtonType="Button" Text="Edit" HeaderText="Edit Record">
            </asp:ButtonField>
            <asp:ButtonField CommandName="deleteRecord" ButtonType="Button" Text="Delete" HeaderText="Delete Record"></asp:ButtonField>
        </Columns>
    </asp:GridView>   
    <asp:DataList CellPadding="5" RepeatDirection="Horizontal" runat="server" ID="dlPager" OnItemCommand="dlPager_ItemCommand">
                <ItemTemplate>
                    <asp:LinkButton Enabled='<%#Eval("Enabled") %>' runat="server" ID="lnkPageNo" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>' CommandName="PageNo"></asp:LinkButton>
                </ItemTemplate>
            </asp:DataList>
</div>
<%--update--%>
<div class="menuupdate">
    <h2>Masukkan Data</h2>
    <table>
        <tr>
            <td class="auto-style1">Title:</td>
            <td>
                <asp:TextBox ID="titleUpdate" runat="server" CssClass="textboxedit"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style1">Nama:</td>
            <td>
                <asp:TextBox ID="namaUpdate" runat="server" CssClass="textboxedit"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style1">ID Karyawan:</td>
            <td>
                <asp:TextBox ID="IDUpdate" runat="server" CssClass="textboxedit"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style1">Tipe Karyawan</td>
            <td>
                <asp:TextBox ID="tipeIDUpdate" runat="server" CssClass="textboxedit"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="auto-style1"></td>
            <td>
                <input type="button" onclick="panggilCancel()" value="Cancel" id="Button2" runat="server" />
                <asp:Button ID="Button3" runat="server" Text="Update" OnClick="btnUpdate_Click" /><br />
                <asp:Label ID="lblResultEdit" runat="server" Text="" CssClass="erroredit"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<%--delete--%>
<%--<div class="menudelete">
    <h2>Apakah anda ingin menghapus file ini?</h2>
    <br />
    <table>
        <tr>
            <td></td>
            <td>
                <input type="button" onclick="panggilCancel()" value="Cancel" id="Button2" runat="server" />
                <asp:Button ID="btnDeleteListItem" runat="server" Text="Delete" OnClick="btnDeleteListItem_Click" />
                <asp:Label ID="lblDeleteStatus" runat="server" Text=""></asp:Label>
                
            </td>
        </tr>
    </table>
</div>--%>
<input type="hidden" value="" id="lblHasil" class="coba" runat="server" />
<input type="hidden" value="" id="pilih" class="pilih" runat="server" />
<input type="hidden" value="" id="lblIndex" class="pilih1" runat="server" />
<input type="hidden" value="" id="lblhal" class="pilih2" runat="server" />

