using StudentAPI_ADO.Models;
using System.Data.SqlClient;

namespace StudentAPI_ADO.Repositories.StudentRepository
{
    public class StudentService : IStudentService
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;

        public StudentService(IConfiguration configuration)
        {
                _configuration = configuration;
                _connectionString = _configuration.GetSection("ConnectionStrings")["DefaultConnectionString"];
        }

        public async Task<List<Student>> GetStudents(int flag, int id)
        {
            try {
                using (var conn = new SqlConnection(_connectionString)) {
                    using (var cmd = new SqlCommand("USP_Student_GetStudents", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("pInt_Flag", flag);
                        cmd.Parameters.AddWithValue("pInt_Id", id);
                        conn.Open();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        return GetStudents(reader);
                    }
                }
            
            }catch
            {
                throw;
            }
        }

        public List<Student> GetStudents(SqlDataReader reader) {
            List<Student> students = new List<Student>();
            if (reader.HasRows) {
                while (reader.Read())
                {
                    students.Add(GetStudent(reader));
                }
            }
            return students;
        }

        public Student GetStudent(SqlDataReader reader) {
            return new Student
            {
                Id = Convert.ToInt32(reader["Id"]),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Email = reader["Email"].ToString(),
                Age = Convert.ToInt16(reader["Age"])
            };
        }

        public async Task<Student?> SaveStudent(int flag, Student student)
        {
            try {
                using (var conn = new SqlConnection(_connectionString)) {
                    using (var cmd = new SqlCommand("USP_Student_SaveStudent", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("pInt_Flag", flag);
                        cmd.Parameters.AddWithValue("pInt_Id", student.Id);
                        cmd.Parameters.AddWithValue("pStr_FirstName", student.FirstName);
                        cmd.Parameters.AddWithValue("pStr_LastName", student.LastName);
                        cmd.Parameters.AddWithValue("pStr_Email", student.Email);
                        cmd.Parameters.AddWithValue("pInt_Age", student.Age);
                        conn.Open();
                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null) { 
                            student.Id = Convert.ToInt32(result);
                            return student;
                        }
                        return null;
                    }    
                }
            } catch {
                throw;
            }
        }

        public async Task<bool> DeleteStudent(int flag, int id)
        {
            try {
                using (var conn = new SqlConnection(_connectionString)) {
                    using (var cmd = new SqlCommand("USP_Student_DeleteStudent", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("pInt_Flag", flag);
                        cmd.Parameters.AddWithValue("pInt_Id", id);
                        conn.Open();
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            } catch {
                throw;
            }
        }
    }
}
