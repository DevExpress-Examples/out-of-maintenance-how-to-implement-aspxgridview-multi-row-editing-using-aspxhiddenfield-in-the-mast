<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="gvCategories" runat="server" AutoGenerateColumns="False" DataSourceID="dsCategories"
            KeyFieldName="CategoryID" OnInit="grid_Init" OnCustomCallback="grid_CustomCallback">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" VisibleIndex="0">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <DetailRow>
                    <dx:ASPxGridView ID="gvProducts" runat="server" AutoGenerateColumns="False" DataSourceID="dsProducts"
                        KeyFieldName="ProductID" Width="100%" OnInit="grid_Init" OnCustomCallback="grid_CustomCallback">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="ProductID" ReadOnly="True" VisibleIndex="0">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ProductName" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="UnitPrice" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataCheckColumn FieldName="Discontinued" VisibleIndex="3">
                            </dx:GridViewDataCheckColumn>
                        </Columns>
                        <Settings ShowTitlePanel="true" />
                    </dx:ASPxGridView>
                    <asp:AccessDataSource ID="dsProducts" runat="server" DataFile="~/App_Data/nwind.mdb"
                        SelectCommand="SELECT [ProductID], [ProductName], [UnitPrice], [Discontinued] FROM [Products] WHERE ([CategoryID] = ?)"
                        OnInit="dsProducts_Init">
                        <SelectParameters>
                            <asp:Parameter Name="CategoryID" Type="Int32" />
                        </SelectParameters>
                    </asp:AccessDataSource>
                </DetailRow>
            </Templates>
            <Settings ShowTitlePanel="true" />
            <SettingsDetail ShowDetailRow="True" />
        </dx:ASPxGridView>
        <asp:AccessDataSource ID="dsCategories" runat="server" DataFile="~/App_Data/nwind.mdb"
            SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]">
        </asp:AccessDataSource>
    </div>
    </form>
</body>
</html>
