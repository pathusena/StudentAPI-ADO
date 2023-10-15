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

        public async Task<List<Student>> GetStudents(int flag)
        {
            try {
                using (var conn = new SqlConnection(_connectionString)) {
                    using (var cmd = new SqlCommand("USP_Student_GetStudents", conn)) {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("pInt_Flag", flag);
                        conn.Open();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();
                        return await GetStudents(reader);
                    }
                }
            
            }catch
            {
                throw;
            }
        }

        public async Task<List<Student>> GetStudents(SqlDataReader reader) {
            List<Student> students = new List<Student>();
            while (await reader.ReadAsync())
            {
                students.Add(GetStudent(reader));
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
    }
}
