using AssessRite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace AssessRite_1._3
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            // Get the exception object.
            Exception exc = Server.GetLastError();
            string ErrorPage = Request.UrlReferrer.ToString();
            // Handle HTTP errors
            // if (exc.GetType() == typeof(HttpException))
            //  {
            // The Complete Error Handling Example generates
            // some errors using URLs with "NoCatch" in them;
            // ignore these here to simulate what would happen
            // if a global.asax handler were not implemented.
            if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                return;
            string userid;
            try
            {
                userid = Session["UserId"] != null ? Session["UserId"].ToString() : "";
            }
            catch
            {
                userid = string.Empty;
            }
            string errMsg;
            if (exc.InnerException != null)
            {
                errMsg = exc.InnerException.Message.ToString().Length >= 250 ? exc.InnerException.Message.ToString().Substring(0, 249).Replace('\'', '\"').TrimEnd('.') : exc.InnerException.Message.ToString().Replace('\'', '\"').TrimEnd('.');
            }
            else
            {
                errMsg = exc.Message.ToString().Length >= 250 ? exc.Message.ToString().Substring(0, 249).Replace('\'', '\"').TrimEnd('.') : exc.Message.ToString().Replace('\'', '\"').TrimEnd('.');
            }


            // Response.Write(errMsg);
            string qur = dbLibrary.idBuildQuery("proc_ErrorLog", errMsg, ErrorPage, userid);
            dbLibrary.idExecute(qur);
            Server.ClearError();
            //Redirect HTTP errors to HttpError page
            Response.Redirect("~/Error.html");
        }
        void Session_Start(object sender, EventArgs e)
        {
            //if (Session["UserId"] != null)
            //{
            //    //Redirect to Welcome Page if Session is not null  
            //    Response.Redirect("~/login.aspx");
            //}
            //else
            //{
            //    Response.Redirect("~/SessionExpired.html");
            //}
        }
    }
}