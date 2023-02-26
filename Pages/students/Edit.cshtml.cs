using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace StudentReg.Pages.students
{
    public class EditModel : PageModel
    {
        public StudentInfo stinfo = new StudentInfo();
        public String errorMessage = "";
        public String successMassage = "";
        public void OnGet()
        {
            try
            {
                String id = Request.Query["id"];
                String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\studentdetails.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from StudentInfo where id=@id";
                    using (SqlCommand comand = new SqlCommand(sql, connection))
                    {
                        comand.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = comand.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                stinfo.id = "" + reader.GetInt32(0);
                                stinfo.fname = reader.GetString(1);
                                stinfo.lname = reader.GetString(2);
                                stinfo.dob = reader.GetString(3);
                                stinfo.subject = reader.GetString(4);
                                stinfo.nic = reader.GetString(5);
                                stinfo.mobile = reader.GetString(6);
                                stinfo.email = reader.GetString(7);

                            }
                        }
                    }
                }

                

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            stinfo.id = Request.Form["id"];
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
            //save student info to database
            try
            {
                String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\studentdetails.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "update StudentInfo set fname=@fname,lname=@lname,dob=@dob,subject=@subject,National_ID=@National_ID,mobile=@mobile,email=@email where id=@id";
                    using (SqlCommand comand = new SqlCommand(sql, connection))
                    {
                        comand.Parameters.AddWithValue("@fname", stinfo.fname);
                        comand.Parameters.AddWithValue("@lname", stinfo.lname);
                        comand.Parameters.AddWithValue("@dob", stinfo.dob);
                        comand.Parameters.AddWithValue("@subject", stinfo.subject);
                        comand.Parameters.AddWithValue("@National_ID", stinfo.nic);
                        comand.Parameters.AddWithValue("@mobile", stinfo.mobile);
                        comand.Parameters.AddWithValue("@email", stinfo.email);
                        comand.Parameters.AddWithValue("@id", stinfo.id);
                        comand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

          
            Response.Redirect("/students/Student");

        }
    }
}
