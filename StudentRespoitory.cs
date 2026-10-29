using Microsoft.Data.Sqlite;

public class StudentRepository
{
    private readonly string _connectionString = "Data Source=students.db";

    public StudentRepository()
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Students (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT NOT NULL,
            Course TEXT NOT NULL,
            Marks INTEGER NOT NULL)";
        cmd.ExecuteNonQuery();
    }

    public void Add(Student s)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "INSERT INTO Students (Name, Course, Marks) VALUES (@name, @course, @marks)";
        cmd.Parameters.AddWithValue("@name", s.Name);
        cmd.Parameters.AddWithValue("@course", s.Course);
        cmd.Parameters.AddWithValue("@marks", s.Marks);
        cmd.ExecuteNonQuery();
    }

    public List<Student> GetAll(string search = "")
    {
        var list = new List<Student>();
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT Id, Name, Course, Marks FROM Students WHERE Name LIKE @search";
        cmd.Parameters.AddWithValue("@search", "%" + search + "%");
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Student
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Course = reader.GetString(2),
                Marks = reader.GetInt32(3)
            });
        }
        return list;
    }

    public bool Update(int id, int marks)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE Students SET Marks = @marks WHERE Id = @id";
        cmd.Parameters.AddWithValue("@marks", marks);
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Delete(int id)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM Students WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        return cmd.ExecuteNonQuery() > 0;
    }
}
