using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudentReg.Pages.students
{
    
    public class StudentModel : PageModel
    {
        public List<StudentInfo> stulist = new List<StudentInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\studentdetails.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from StudentInfo";
                    using (SqlCommand comand = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = comand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentInfo stInfo = new StudentInfo();
                                stInfo.id = "" + reader.GetInt32(0);
                                stInfo.fname = "" + reader.GetString(1);
                                stInfo.lname = "" + reader.GetString(2);
                                stInfo.dob = "" + reader.GetString(3);
                                stInfo.subject = "" + reader.GetString(4);
                                stInfo.nic = "" + reader.GetString(5);
                                stInfo.mobile = "" + reader.GetString(6);
                                stInfo.email = "" + reader.GetString(7);
                                stulist.Add(stInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class StudentInfo
    {
        public string id;
        public string fname;
        public string lname;
        public string dob;
        public string subject;
        public string nic;
        public string mobile;
        public string email;
    }
}
