namespace StudentAPI_ADO.Repositories.StudentRepository
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudents(int flag);
    }
}
