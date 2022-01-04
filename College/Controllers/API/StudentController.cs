using College.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace College.Controllers.API
{
    public class StudentController : ApiController
    {
        string collectionString = @"Data Source=laptop-0hsc4h8o;Initial Catalog=College;Integrated Security=True";
        List<Student> studentsList = new List<Student>();
        // GET: api/Student
        public IHttpActionResult Get()
        {
            List<Student> list = returnAllDetailsFromTable(studentsList, collectionString);
            return Ok(new { list });
        }

        // GET: api/Student/5
        public IHttpActionResult Get(int id)
        {
            List<Student> list = returnStudentById(collectionString,id, studentsList);
            return Ok(new { list });

        }

        // POST: api/Student
        public IHttpActionResult Post([FromBody] Student student)
        {
            int numRow=AddStudent(collectionString, student);
            return Ok(numRow);
        }
        
        // PUT: api/Student/5
        public IHttpActionResult Put(int id, [FromBody] string value)
        {
            int row = EditStudent(collectionString, id);
            return Ok(row);
        }

        // DELETE: api/Student/5
        public IHttpActionResult Delete(int id)
        {
           int howManyRowDeleted= DeleteStudent(collectionString, id);
            return Ok(howManyRowDeleted);
        }

        static List<Student> returnAllDetailsFromTable(List<Student> list, string contactString)
        {
            try
            {
                using (SqlConnection conect = new SqlConnection(contactString))
                {
                    string query = @"SELECT * FROM Student";
                    conect.Open();
                    SqlCommand cmd = new SqlCommand(query, conect);
                    SqlDataReader row = cmd.ExecuteReader();
                    if (row.HasRows)
                    {
                        while (row.Read())
                        {
                            list.Add(new Student( row.GetString(1), row.GetString(2), row.GetString(3), row.GetString(4), row.GetInt32(5)));
                            return list;
                        }
                    };
                    conect.Close();
                    return list;

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        static List<Student> returnStudentById(string collectionString,int id, List<Student> list)
        {
            try
            {
                using (SqlConnection conect = new SqlConnection(collectionString))
                {
                    conect.Open();
                    string query = $@"SELECT * FROM Student WHERE Id={id}";
                    SqlCommand cmd = new SqlCommand(query,conect);
                    SqlDataReader row = cmd.ExecuteReader();
                    if (row.HasRows)
                    {
                        while (row.Read())
                        {
                            list.Add(new Student(row.GetString(1), row.GetString(2), row.GetString(3), row.GetString(4), row.GetInt32(5)));
                            return list;
                        }
                    };
                    return list ;

                    conect.Close();
                }
            }
            catch (SqlException ex) { throw; }
            catch (Exception ex) {  throw;}
        }
        static int AddStudent(string contactString,Student student)
        {
            
            using (SqlConnection conect =new SqlConnection(contactString))
            {
                conect.Open();
                string query = $@"INSERT INTO(firstName,lastName,dateOfBorn,email,schoolYear)
                                VALUES('{student.firstName}','{student.lastName}','{student.dateBorn}','{student.email})',{student.schoolYear})";
                SqlCommand cmd = new SqlCommand(query, conect);
                int rows = cmd.ExecuteNonQuery();
                

                conect.Close();
                return rows;
            }
        }
        static int EditStudent(string contactString, int id)
        {
            using (SqlConnection conect = new SqlConnection(contactString))
            {
                conect.Open();
                string query = @"";
                SqlCommand cmd = new SqlCommand(query, conect);
                int rows = cmd.ExecuteNonQuery();
                return rows;
                conect.Close();
            }
        }
        static int DeleteStudent(string contactString,int id)
        {
            using(SqlConnection conect =new SqlConnection(contactString))
            {
                conect.Open();
                string query = $@"DELETE FROM Student WHERE Id={id}";
                SqlCommand cmd = new SqlCommand(query,conect);
                int rows = cmd.ExecuteNonQuery();
                return rows;

                conect.Close();

            }
        }

    }
}
