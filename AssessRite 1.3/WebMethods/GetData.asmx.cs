﻿using AssessRite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace AssessRite_1._3.WebMethods
{
    /// <summary>
    /// Summary description for SubjectData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GetData : System.Web.Services.WebService
    {

        //Add/View Subject Page
        [WebMethod(EnableSession = true)]
        public string GetSubjectData()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getSubjects]", HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        //Add/View Class Page
        [WebMethod(EnableSession = true)]
        public string GetClassData()
        {
            string qur = "Select * from Class where IsDeleted='0' and SchoolId='" + HttpContext.Current.Session["SchoolId"].ToString() + "' ORDER BY MasterClassId";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                // DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        //Add/View Concept Page
        [WebMethod]
        public string GetConceptData()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getConcepts]");
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string LoadConceptDropdownsClass()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForConcept]", HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string LoadConceptDropdownsSubject(int classid)
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForConcept]", HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[1];
                var results = (from myRow in dt.AsEnumerable()
                               where (int)myRow["ClassId"] == classid
                               select myRow).CopyToDataTable();
                DataTable dtTemp = (DataTable)results;
                dtTemp.TableName = "Table2";
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dtTemp.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string LoadConceptCheckboxlist()

        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForConcept]", HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                List<CheckBoxItem> chkListClass = new List<CheckBoxItem>();
                chkListClass = (from DataRow dr in ds.Tables[0].Rows
                                select new CheckBoxItem()
                                {
                                    ClassId = Convert.ToInt32(dr["ClassId"].ToString()),
                                    ClassName = dr["ClassName"].ToString(),
                                }).ToList();
                JavaScriptSerializer ser = new JavaScriptSerializer();
                return ser.Serialize(chkListClass);
            }
            else
                return null;
        }
        public class CheckBoxItem
        {
            public string ClassName { get; set; }
            public int ClassId { get; set; }
        }

        [WebMethod]
        public string getAllConceptAppearedinLowerClasses(int conceptid)
        {
            string qur = "Select ClassId from ConceptsRelatedClass where ConceptId=" + conceptid;
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;

        }

        //Add/View Objective Page

        [WebMethod]
        public string GetObjectiveData()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getObjectives]");
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string LoadObjectiveDropdownsClass()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForObjective]", HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string LoadObjectiveDropdownsSubject(int classid)
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForObjective]", HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[1];
                var results = (from myRow in dt.AsEnumerable()
                               where (int)myRow["ClassId"] == classid
                               select myRow).CopyToDataTable();
                DataTable dtTemp = (DataTable)results;
                dtTemp.TableName = "Table2";
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dtTemp.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string LoadObjectiveDropdownsConcept(int subjectid)
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForObjective]", HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[2];
                var results = (from myRow in dt.AsEnumerable()
                               where (int)myRow["SubjectId"] == subjectid
                               select myRow).CopyToDataTable();
                DataTable dtTemp = (DataTable)results;
                dtTemp.TableName = "Table3";
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dtTemp.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }

        //Add/View Questions
        [WebMethod]
        public string LoadQuestionDropdownsAnswerType()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForQuestion]");
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[4].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[4];
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }

        [WebMethod]
        public string LoadQuestionDropdownsClass()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForQuestion]");
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }

        [WebMethod]
        public string LoadQuestionDropdownsSubject(int classid)
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForQuestion]");
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[1];
                var results = (from myRow in dt.AsEnumerable()
                               where (int)myRow["ClassId"] == classid
                               select myRow).CopyToDataTable();
                DataTable dtTemp = (DataTable)results;
                dtTemp.TableName = "Table2";
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dtTemp.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }

        [WebMethod]
        public string LoadQuestionDropdownsConcept(int subjectid)
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForQuestion]");
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[2];
                var results = (from myRow in dt.AsEnumerable()
                               where (int)myRow["SubjectId"] == subjectid
                               select myRow).CopyToDataTable();
                DataTable dtTemp = (DataTable)results;
                dtTemp.TableName = "Table3";
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dtTemp.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }

        [WebMethod]
        public string LoadQuestionDropdownsObjective(int conceptid)
        {
            string qur = dbLibrary.idBuildQuery("[proc_getDataForQuestion]");
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[3].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[3];
                var results = (from myRow in dt.AsEnumerable()
                               where (int)myRow["ConceptId"] == conceptid
                               select myRow).CopyToDataTable();
                DataTable dtTemp = (DataTable)results;
                dtTemp.TableName = "Table4";
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dtTemp.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }


        [WebMethod(EnableSession = true)]
        public string getQuestionsData()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getQuestions]", HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod]
        public string getQuestionAnswerData(int questionid)
        {
            string qur = "SELECT AnswerId, Answer, IsRightAnswer, QuestionId FROM Answers where QuestionId='" + questionid + "'";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string getStudentData()
        {
            string qur = "SELECT Student.StudentId, Student.FirstName, Student.LastName, Student.ParentName, Student.ParentContactNo, Student.ParentEmailId, Class.ClassName, AcedemicYear.AcademicYear, StudentClass.ClassId, StudentClass.AcademicYearId, Login.UserName,Login.Password FROM Login RIGHT OUTER JOIN Student ON Login.StudentId = Student.StudentId LEFT OUTER JOIN Class RIGHT OUTER JOIN StudentClass ON Class.ClassId = StudentClass.ClassId LEFT OUTER JOIN AcedemicYear ON StudentClass.AcademicYearId = AcedemicYear.AcademicYearId ON Student.StudentId = StudentClass.StudentId where StudentClass.IsCurrent='1' and Student.IsDeleted='0' and Student.SchoolId='" + HttpContext.Current.Session["SchoolId"].ToString() + "' and Class.IsDeleted='0'";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string getTeacherData()
        {
            string qur = "SELECT Teacher.TeacherId, Teacher.TeacherFirstName, Teacher.TeacherLastName, Teacher.ContactNo, Teacher.EmailId, Login.UserName,Login.Password FROM Teacher LEFT OUTER JOIN Login ON Teacher.TeacherId = Login.TeacherId where Teacher.IsDeleted='0' and Teacher.SchoolId='" + HttpContext.Current.Session["SchoolId"].ToString() + "'";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string getDEData()
        {
            string qur = "SELECT DE.DEId, DE.DEFirstName, DE.DELastName, DE.DEContactNo, DE.DEEmailId, Login.UserName,Login.Password FROM DE LEFT OUTER JOIN Login ON DE.DEId = Login.DEId where DE.IsDeleted='0' and DE.SchoolId='" + HttpContext.Current.Session["SchoolId"].ToString() + "'";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string getScheduledTests()
        {
            string qur = "SELECT TestSchedule.TestScheduleId, Class_1.ClassName, Subject.SubjectName, Test.TestId, Test.TestCreationDate, Test.TestType, Test.TestKey, Test.NoOfQuestions, Test.TotalQuestions,CONVERT(VARCHAR(10),TestSchedule.TestDate,110) as TestDate, CONVERT(VARCHAR(15), TestSchedule.TestActiveFrom, 100) AS TestActiveFrom, CONVERT(VARCHAR(15),TestSchedule.TestActiveTo, 100) AS TestActiveTo, Class.ClassName AS AssignedTo FROM Class INNER JOIN TestSchedule ON Class.ClassId = TestSchedule.AssignedClassId LEFT OUTER JOIN Test ON TestSchedule.TestId = Test.TestId LEFT OUTER JOIN Class AS Class_1 ON Test.ClassId = Class_1.ClassId LEFT OUTER JOIN Subject ON Test.SubjectId = Subject.SubjectId where Test.CreatedBy='" + HttpContext.Current.Session["UserId"].ToString() + "' and Test.TestType='Online' order by TestSchedule.TestScheduleId DESC";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod]
        public string getConceptsForTests(int testid)
        {
            string qur = "SELECT Concept.ConceptName, TestConcepts.TestKey FROM Concept RIGHT OUTER JOIN TestConcepts ON Concept.ConceptId = TestConcepts.ConceptId where TestConcepts.TestId=" + testid;
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string getQuestionsStatus()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getQuestionsStatusForDE]", HttpContext.Current.Session["DEId"].ToString(), HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string getQuestionsUnderReview()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getQuestionsUnderReview]", HttpContext.Current.Session["SchoolId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod(EnableSession = true)]
        public string getQuestionsUnderReviewForSME()
        {
            string qur = dbLibrary.idBuildQuery("[proc_getQuestionsUnderReviewForSME]", HttpContext.Current.Session["SchoolId"].ToString(), HttpContext.Current.Session["SMEId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod]
        public string GetSchoolData()
        {
            string qur = "SELECT Country.CountryName, State.StateName, SchoolInfo.schoolId, SchoolInfo.SchoolName, SchoolInfo.URL, SchoolInfo.SchoolAddress, SchoolInfo.StateId, " +
                         " SchoolInfo.ZipCode, SchoolInfo.CountryId, SchoolInfo.ContactNo, SchoolInfo.Email, SchoolInfo.NoOfStudents, SchoolInfo.EmergencyContactNo, " +
                          " SchoolInfo.PrincipalName" +
                         " FROM Country INNER JOIN" +
                          " SchoolInfo ON Country.CountryId = SchoolInfo.CountryId INNER JOIN " +
                         " State ON SchoolInfo.StateId = State.StateId where SchoolInfo.IsDeleted='0'";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod]
        public string LoadSchoolDropdownCountry()
        {
            string qur = "Select * from Country";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;

        }

        [WebMethod]
        public string LoadSchoolDropdownState(int countryid)
        {
            string qur = "Select * from State where CountryId=" + countryid;
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }
        [WebMethod]
        public string LoadSchoolDropdownSchool()
        {
            string qur = "Select SchoolId, SchoolName from SchoolInfo";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }
        [WebMethod]
        public string GetAdminDataForSuperAdmin()
        {
            string qur = "SELECT SchoolInfo.SchoolName, Admin.AdminId, Admin.SchoolId, Admin.AdminName, Admin.AdminAddress, Admin.AdminContactNo, Admin.AdminEmailId, Login.UserName,  "+
" Login.Password FROM  Admin INNER JOIN SchoolInfo ON Admin.SchoolId = SchoolInfo.schoolId INNER JOIN Login ON Admin.AdminId = Login.AdminId AND Admin.SchoolId = Login.SchoolId "+
" WHERE(Admin.IsDeleted = '0') AND(SchoolInfo.IsDeleted = '0')";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataSet ds = dbLibrary.idGetCustomResult(qur);
                DataTable dt = ds.Tables[0];
                string JSONresult;
                JSONresult = JsonConvert.SerializeObject(dt);
                return JSONresult;
            }
            else
                return null;
        }

        [WebMethod]
        public string getAllTestTypesForSchool(int schoolid)
        {
            string qur = "Select TestType from SchoolTestType where SchoolId=" + schoolid;
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                string result;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
                return result;
            }
            else
                return null;
        }
    }
}
