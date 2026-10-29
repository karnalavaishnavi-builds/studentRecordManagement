var repo = new StudentRepository();

while (true)
{
    Console.WriteLine("\n--- Student Record Management ---");
    Console.WriteLine("1. Add  2. View all  3. Search  4. Update  5. Delete  6. Exit");
    Console.Write("Choose: ");
    string choice = Console.ReadLine() ?? "";
    try
    {
        
        switch (choice)
        {
            case "1": AddStudent(); break;
            case "2": Show(repo.GetAll()); break;
            case "3":
                Console.Write("Name to search: ");
                Show(repo.GetAll(Console.ReadLine() ?? ""));
                break;
            case "4": UpdateStudent(); break;
            case "5": DeleteStudent(); break;
            case "6": return;
            default: Console.WriteLine("Invalid choice."); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}

void AddStudent()
{
    Console.Write("Name: ");
    string name = Console.ReadLine() ?? "";
    Console.Write("Course: ");
    string course = Console.ReadLine() ?? "";
    Console.Write("Marks: ");
    if (!int.TryParse(Console.ReadLine(), out int marks))
    {
        Console.WriteLine("Invalid marks.");
        return;
    }
    repo.Add(new Student { Name = name, Course = course, Marks = marks });
    Console.WriteLine("Student added.");
}

void Show(List<Student> list)
{
    if (list.Count == 0) { Console.WriteLine("No records."); return; }
    foreach (var s in list)
        Console.WriteLine($"{s.Id} | {s.Name} | {s.Course} | {s.Marks}");
}

void UpdateStudent()
{
    Console.Write("Student Id: ");
    int.TryParse(Console.ReadLine(), out int id);
    Console.Write("New marks: ");
    if (!int.TryParse(Console.ReadLine(), out int marks)) { Console.WriteLine("Invalid marks."); return; }
    Console.WriteLine(repo.Update(id, marks) ? "Updated." : "Not found.");
}

void DeleteStudent()
{
    Console.Write("Student Id: ");
    int.TryParse(Console.ReadLine(), out int id);
    Console.WriteLine(repo.Delete(id) ? "Deleted." : "Not found.");
}
