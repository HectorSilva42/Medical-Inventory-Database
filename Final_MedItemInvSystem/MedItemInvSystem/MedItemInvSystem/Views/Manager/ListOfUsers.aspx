<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
	Team 13 Inventory System - Manager
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <h2>List of Users Report:</h2>
    
    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using(Html.BeginForm("ListOfUsersForm","Manager")) %>
    <% { %>
        <p>
            Employee ID: <%= Html.TextBox("id") %>
        </p>
        <p>
            Classification: <%= Html.TextBox("classification") %>
        </p>
        <p>
            First Name: <%= Html.TextBox("firstName") %>
        </p>
        <p>
            Last Name: <%= Html.TextBox("lastName") %>
        </p>
        <input type="submit" value="Submit" />
    <% } %>

    <% using(Html.BeginForm("EmptyListOfUsersForm","Manager")) %>
    <% { %>
        <input type="submit" value="Get All Users" />
    <% } %>

    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="MySQLData" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" 
            CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="Last Name" HeaderText="Last Name" 
                    SortExpression="Last Name" />
                <asp:BoundField DataField="First Name" HeaderText="First Name" />
                <asp:BoundField DataField="Employee ID" HeaderText="Employee ID" />
                <asp:BoundField DataField="Classification" HeaderText="Classification" />

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
            ConnectionString="server=mydb.cxve5mktqpzu.us-west-2.rds.amazonaws.com;uid=Team13Master;pwd=Medicalitems1;database=mydb" 
            ProviderName="MySql.Data.MySqlClient"
            SelectCommand="SELECT Personnel.LastName AS 'Last Name', Personnel.FirstName AS 'First Name', Personnel.EmployeeID AS 'Employee ID', enumClass.Title AS 'Classification'
                            FROM Personnel INNER JOIN enumClass
                            ON Personnel.Classification = enumClass.Class
                            WHERE Personnel.EmployeeID LIKE @SessionVar AND Personnel.Classification LIKE @SessionVar1 AND Personnel.FirstName LIKE @SessionVar2 AND Personnel.LastName LIKE @SessionVar3
                            ORDER BY Personnel.Classification asc">
            <SelectParameters>
                <asp:SessionParameter Name="SessionVar" SessionField="ListOfUsersSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar1" SessionField="ClassificationSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar2" SessionField="FirstNameSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar3" SessionField="LastNameSession" ConvertEmptyStringToNull="true" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>

</asp:Content>
