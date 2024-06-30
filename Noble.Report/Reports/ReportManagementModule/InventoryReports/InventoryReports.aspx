<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="InventoryReports.aspx.cs" Inherits="Noble.Report.Reports.ReportManagementModule.InventoryReports.InventoryReports" %>
<%@ Register  Assembly="DevExpress.XtraReports.v22.2.Web.WebForms, Version=22.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%"  BorderBottom-BorderStyle="None" Border-BorderStyle="None" BorderLeft-BorderStyle="None" BorderRight-BorderStyle="None" BorderTop-BorderStyle="None" Theme="SoftOrange">
        <SettingsContextMenu>
            <ColumnMenuItemVisibility SortAscending="False" SortDescending="False" />
        </SettingsContextMenu>
        <SettingsCustomizationDialog ShowSortingPage="False" />
        <Templates>
            <DetailRow>
            </DetailRow>
        </Templates>
        <SettingsPager PageSize="30">
        </SettingsPager>
        <Settings HorizontalScrollBarMode="Auto" ShowFooter="True" ShowGroupPanel="True" GroupFormat="{1}" GroupFormatForMergedGroup="" GroupFormatForMergedGroupRow="" ShowColumnHeaders="true" />
        <SettingsBehavior AllowSort="False" />
        <SettingsResizing ColumnResizeMode="Control" />
        <SettingsCookies StoreGroupingAndSorting="False" />
<SettingsPopup>
<FilterControl AutoUpdatePosition="False"></FilterControl>
</SettingsPopup>
        <SettingsSearchPanel Visible="True" />
        <Styles>
            <GroupRow BackColor="White" Font-Bold="True">
            </GroupRow>
            <Row BackColor="White">
            </Row>
            <AlternatingRow BackColor="#F5F5F5">
            </AlternatingRow>
            <Footer BackColor="#999999" HorizontalAlign="Right" Font-Bold="True" ForeColor="Black">
            </Footer>
            <GroupFooter BackColor="#CCCCCC" Font-Bold="True" HorizontalAlign="Right">
            </GroupFooter>
            <GroupPanel BackColor="White">
            </GroupPanel>
        </Styles>

<Border BorderStyle="None"></Border>

<BorderLeft BorderStyle="None"></BorderLeft>

<BorderTop BorderStyle="None"></BorderTop>

<BorderRight BorderStyle="None"></BorderRight>

<BorderBottom BorderStyle="None"></BorderBottom>
    </dx:ASPxGridView>
    <dx:ASPxWebDocumentViewer Visible="false"  ID="ASPxWebDocumentViewer1" runat="server" style="margin-top: 10px" Height="1500px">
    </dx:ASPxWebDocumentViewer>
</asp:Content>
