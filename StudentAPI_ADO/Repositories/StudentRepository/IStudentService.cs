namespace StudentAPI_ADO.Repositories.StudentRepository
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudents(int flag, int id);
        Task<Student?> SaveStudent(int flag, Student student);
        Task<bool> DeleteStudent(int flag, int id);
    }
}
