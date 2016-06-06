<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%if (Session["Id"] == null)
  {%>
      <%: Html.ActionLink("Log In", "LogOn2", "Home")%>
<%}
  else if (Session["Classification"] == "Nurse")
  {
      %>
        <%: Html.ActionLink(Session["Id"] + ", Log Off", "LogOff", "Home")%>
<% }
   else if (Session["Classification"] == "Supply")
  {
      %>
        <%: Html.ActionLink(Session["Id"] + ", Log Off", "LogOff", "Home")%>
<% }
  else if (Session["Classification"] == "Doctor")
  {
      %>
        <%: Html.ActionLink(Session["Id"] + ", Log Off", "LogOff", "Home")%>
<% } 
   else if (Session["Classification"] == "Manager")
  {
      %>
        <%: Html.ActionLink(Session["Id"] + ", Log Off", "LogOff", "Home") %>
<% }
  else {
      %>
      <%: Html.ActionLink("Log In", "LogOn2", "Home")%>
<%}     %>