<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%
    if (Request.IsAuthenticated) {
%>
        Welcome!
        [ <%: Html.ActionLink("Log Off", "LogOff", "Home") %> ]
<%
    }
    else {
%> 
        [ <%: Html.ActionLink("Log On", "LogOn2", "Home") %> ]
<%
    }
%>
