﻿using AssessRite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssessRite_1._3.Admin
{
    public partial class ReviewQuestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
        }
        [System.Web.Services.WebMethod]
        public static string DeleteQuestion(int questionid)
        {
            dbLibrary.idUpdateTable("Questions",
                "QuestionId=" + questionid,
                "IsDeleted", "1");
            return "Question Deleted Successfully";
        }

        [System.Web.Services.WebMethod]
        public static string RejectQuestion(int questionid, string comment)
        {
            var regexItem = new Regex("^[a-zA-Z0-9\' ]*$");
            if (!regexItem.IsMatch(comment))
            {
                return "Comment Cannot Have Special Characters";
            }
            if (!(Regex.IsMatch(comment, "[^0-9]")))
            {
                return "Comment Cannot Have Just Numbers";
            }
            dbLibrary.idUpdateTable("Questions",
                "QuestionId=" + questionid,
                "Comment", comment,
                "StatusId", "2");
            return "Question Sent Back to DE for Correction";
        }

        [System.Web.Services.WebMethod]
        public static string ApproveQuestion(int questionid)
        {
            dbLibrary.idUpdateTable("Questions",
                "QuestionId=" + questionid,
                "Comment", "",
                "AddedDateTime", DateTime.Now.ToString(),
                "StatusId", "3");
            return "Question Approved";
        }
    }
}