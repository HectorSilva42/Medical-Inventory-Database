<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 - Check Out History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Total Check Out History Report:</h2>

    <% using(Html.BeginForm("TotalForm","Manager")) %>
    <% { %>
        <p>
            Employee ID: <%= Html.TextBox("employeeID") %>
        </p>
        <p>
            Item Name: <%= Html.TextBox("itemName") %>
        </p>

        <input type="submit" value="Submit" />
    <% } %>

    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="MySQLData" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" 
            CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="Item Name" HeaderText="Item Name" />
                <asp:BoundField DataField="Total Checked Out" HeaderText="Total Checked Out" />
                <asp:BoundField DataField="Employee ID" HeaderText="Employee ID" />
                <asp:BoundField DataField="Last Name" HeaderText="Last Name" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="#696969" />
            <PagerStyle ForeColor="#8C4510" 
                HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" ForeColor="White" Font-Bold="True" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
        <asp:SqlDataSource runat="server" ID="MySQLData"
            ConnectionString="server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb;Convert Zero Datetime=True" 
            ProviderName="MySql.Data.MySqlClient"
            SelectCommand="SELECT CheckedOut.Inventory_ItemName AS 'Item Name', SUM(CheckedOut.QuantityCheckedOut) AS 'Total Checked Out', CheckedOut.Personnel_EmployeeID AS 'Employee ID', Personnel.LastName AS 'Last Name'
                            FROM CheckedOut INNER JOIN Personnel
                            ON CheckedOut.Personnel_EmployeeID = Personnel.EmployeeID
                            WHERE CheckedOut.Inventory_ItemName LIKE @SessionVar2 AND CheckedOut.Personnel_EmployeeID LIKE @SessionVar
                            GROUP BY CheckedOut.Inventory_ItemName
                            ORDER BY CheckedOut.IndexKey asc">
            <SelectParameters>
                   <asp:SessionParameter Name="SessionVar" SessionField="EmployeeIDSession" ConvertEmptyStringToNull="true" />
                   <asp:SessionParameter Name="SessionVar2" SessionField="ItemNameSession" ConvertEmptyStringToNull="true" />
                   <%--<asp:SessionParameter Name="SessionVar4" SessionField="QuantitySession" ConvertEmptyStringToNull="true" />
                   <asp:SessionParameter Name="SessionVar6" SessionField="QuantityLessSession" ConvertEmptyStringToNull="true" />
                   <asp:SessionParameter Name="SessionVar7" SessionField="QuantityLessThanSession" ConvertEmptyStringToNull="true" />
                   <asp:SessionParameter Name="SessionVar8" SessionField="QuantityGreaterSession" ConvertEmptyStringToNull="true" />
                   <asp:SessionParameter Name="SessionVar9" SessionField="QuantityGreaterThanSession" ConvertEmptyStringToNull="true" />--%>
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>

</asp:Content>
