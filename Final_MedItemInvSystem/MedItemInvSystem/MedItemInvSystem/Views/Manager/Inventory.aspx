<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Manager.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 List of Items
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>List of Items Report:</h2>

    <% if (TempData["notice"] != null) { %>
        <p><%= Html.Encode(TempData["notice"]) %></p>
    <% } %>

    <% using(Html.BeginForm("ListOfItemsForm","Manager")) %>
    <% { %>
        <p>
            Item Name: <%= Html.TextBox("itemName") %>
        </p>
        <p>
            Item Type: <%= Html.TextBox("itemType") %>
        </p>
        <p>
            Quantity: <%= Html.TextBox("quantity") %>
        </p>
        <p>
            Allergens:<br />
            Latex <%= Html.CheckBox("latex") %><br />
            Plastic <%= Html.CheckBox("plastic") %><br />
            Penicillin <%= Html.CheckBox("penicillin") %><br />
        </p>

        <p>
            Miscellaneous:<br />
            Disposable <%= Html.CheckBox("disposable") %> <br />
        
            Refrigerated <%= Html.CheckBox("refrigerated") %> <br />
        
            Inflammable <%= Html.CheckBox("inflammable") %> <br />
        </p>
        <input type="submit" value="Submit" />
    <% } %>

    <% using(Html.BeginForm("EmptyListOfItemsForm","Manager")) %>
    <% { %>
        <input type="submit" value="Get All Items" />
    <% } %>

    <% using(Html.BeginForm("AllergenListOfItemsForm","Manager")) %>
    <% { %>
        <input type="submit" value="Get All Items with Allergens" />
    <% } %>

    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="MySQLData" BackColor="#DEBA84" 
            BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" 
            CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="Item Name" HeaderText="Item Name" 
                    SortExpression="Item Name" />
                <asp:BoundField DataField="Item Type" HeaderText="Item Type" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Disposable" HeaderText="Disposable" />
                <asp:BoundField DataField="Item ID" HeaderText="Item ID" />
                <asp:BoundField DataField="Allergen" HeaderText="Allergen" />
                <asp:BoundField DataField="Size" HeaderText="Size" />
                <asp:BoundField DataField="Item Use" HeaderText="Item Use" />
                <asp:BoundField DataField="Refrigerated" HeaderText="Refrigerated" />
                <asp:BoundField DataField="Inflammable" HeaderText="Inflammable" />
                <asp:BoundField DataField="Other Info" HeaderText="Other Info" />

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
            SelectCommand="SELECT Inventory.ItemName AS 'Item Name', Inventory.ItemType AS 'Item Type', Inventory.QuantityAvailable AS 'Quantity', Inventory.Disposable AS 'Disposable', Inventory.ItemID AS 'Item ID', Inventory.Allergen AS 'Allergen', ItemDescription.Size AS 'Size', ItemDescription.ItemUse AS 'Item Use',
            ItemDescription.Refrigerated AS 'Refrigerated', ItemDescription.Inflammable AS 'Inflammable', ItemDescription.OtherInfo AS 'Other Info'
                            FROM Inventory INNER JOIN ItemDescription
                            ON Inventory.ItemName = ItemDescription.Inventory_ItemName
                            WHERE Inventory.ItemName LIKE @SessionVar AND Inventory.ItemType LIKE @SessionVar2 AND Inventory.QuantityAvailable &lt; @SessionVar3 
                            AND Inventory.QuantityAvailable LIKE @SessionVar4 AND Inventory.QuantityAvailable &gt; @SessionVar5 AND Inventory.QuantityAvailable &lt;= @SessionVar6 
                            AND Inventory.QuantityAvailable &gt;= @SessionVar7 AND Inventory.Disposable LIKE @SessionVar8 AND ItemDescription.Refrigerated LIKE @SessionVar9 AND ItemDescription.Inflammable LIKE @SessionVar10
                            AND IFNULL(Inventory.Allergen, '') LIKE @SessionVar11 OR Inventory.Allergen LIKE @SessionVar12 AND Inventory.ItemName LIKE @SessionVar AND Inventory.ItemType LIKE @SessionVar2 AND Inventory.QuantityAvailable &lt; @SessionVar3 
                            AND Inventory.QuantityAvailable LIKE @SessionVar4 AND Inventory.QuantityAvailable &gt; @SessionVar5 AND Inventory.QuantityAvailable &lt;= @SessionVar6 
                            AND Inventory.QuantityAvailable &gt;= @SessionVar7 AND Inventory.Disposable LIKE @SessionVar8 AND ItemDescription.Refrigerated LIKE @SessionVar9 AND ItemDescription.Inflammable LIKE @SessionVar10
                            ORDER BY Inventory.ItemName asc">
         <SelectParameters>
                <asp:SessionParameter Name="SessionVar" SessionField="ItemNameSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar2" SessionField="ItemTypeSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar3" SessionField="QuantityLessSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar4" SessionField="QuantitySession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar5" SessionField="QuantityGreaterSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar6" SessionField="QuantityLessThanSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar7" SessionField="QuantityGreaterThanSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar8" SessionField="DisposableSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar9" SessionField="RefrigeratedSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar10" SessionField="InflammableSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar11" SessionField="AllergenSession" ConvertEmptyStringToNull="true" />
                <asp:SessionParameter Name="SessionVar12" SessionField="Allergen2Session" ConvertEmptyStringToNull="true" />
         </SelectParameters>                   
         </asp:SqlDataSource>
    </div>
    </form>

</asp:Content>
