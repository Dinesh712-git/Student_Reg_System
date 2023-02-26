using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentReg.Pages.students
{
    public class AddstudentsModel : PageModel
    {
        public StudentInfo stinfo = new StudentInfo();
        public String errorMessage = "";
        public String successMassage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            stinfo.fname = Request.Form["fname"];
            stinfo.lname = Request.Form["lname"];
            stinfo.dob = Request.Form["dob"];
            stinfo.subject = Request.Form["subject"];
            stinfo.nic = Request.Form["nic"];
            stinfo.mobile = Request.Form["mobile"];
            stinfo.email = Request.Form["email"];

            if (stinfo.fname.Length == 0 || stinfo.lname.Length == 0 || stinfo.dob.Length == 0 || stinfo.subject.Length == 0 || stinfo.nic.Length == 0 || stinfo.mobile.Length == 0 || stinfo.email.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            //save client info to database
            try
            {
                String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\studentdetails.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "insert into StudentInfo" + "(fname,lname,dob,subject,National_ID,mobile,email)values" + "(@fname,@lname,@dob,@subject,@National_ID,@mobile,@email);";
                    using (SqlCommand comand = new SqlCommand(sql, connection))
                    {
                        comand.Parameters.AddWithValue("@fname", stinfo.fname);
                        comand.Parameters.AddWithValue("@lname", stinfo.lname);
                        comand.Parameters.AddWithValue("@dob", stinfo.dob);
                        comand.Parameters.AddWithValue("@subject", stinfo.subject);
                        comand.Parameters.AddWithValue("@National_ID", stinfo.nic);
                        comand.Parameters.AddWithValue("@mobile", stinfo.mobile);
                        comand.Parameters.AddWithValue("@email", stinfo.email);
                        comand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            stinfo.fname = "";
            stinfo.lname = "";
            stinfo.dob = "";
            stinfo.subject = "";
            stinfo.nic = "";
            stinfo.mobile = "";
            stinfo.email = "";
            successMassage = "New Student Added Correctly";

            Response.Redirect("/students/Student");

        }

    }
}
