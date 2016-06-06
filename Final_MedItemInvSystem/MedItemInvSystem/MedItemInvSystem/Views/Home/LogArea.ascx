<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%if (Session["Id"] == null)
  {%>
      <%: Html.ActionLink("Log In", "LogOn2", "Home")%>
<%}
  else
  {
      Session["Id"] = null;
      Session["Classification"] = null;
      %>
        <%: Html.ActionLink("Log Off", "Index", "Home") %>
<% }%>