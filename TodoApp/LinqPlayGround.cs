using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp;

public class Student
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Class { get; set; }
    public int Income { get; set; }
    public int CourseId { get; set; }
}


public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Student> CourseStudents { get; set; }
}
public class PlayGround
{
    public IEnumerable<Student> data;
    public IEnumerable<Course> courses;

    
    public PlayGround()
    {
        data = new List<Student>
        {
            new Student() { Name = "zehra", LastName = "koca", Class = "A", Income= 1000 },
            new Student() { Name = "zekeriya", LastName = "koca", Class = "B" , Income = 800},
            new Student() { Name = "isa", LastName = "girgin", Class = "A", Income = 1100},
        };
        courses = new List<Course>();
    }

    public void Test()
    {
       var a =  data.Where(x => x.Name == "zehra");

       var b = data.Where(x => x.Name.StartsWith("z")).Skip(1).Take(2);
       
       var onlyNames = data.Where(x => x.Name.EndsWith("z")).Select(x=>  x.Name);

       var c = data.Where(x => x.Name.EndsWith("z")).Select(x =>
       {
           return x.Name == "zehra";
       });

       var d = data.OrderBy(x => x.LastName).ThenByDescending(x=> x.Class); // isa , zekeirya, zehra

       var totalIncomesOfStudents = data.Select(x => x.Income).Sum();

       var nameOfRichestStudent = data.MaxBy(x => x.Income)?.Name;

       var isExist = data.Any(x => x.Name.StartsWith("c")); //returns true or false
       
       var e = data.All(x => x.Income>1000); //returns true or false

       var f = data.Select(s => s.LastName).Distinct(); // List<string>
       
       var g = data.DistinctBy(s => s.LastName); // List<Student>

       _ = data.Where(x => x.Name == "zehra").Where(x => x.LastName == "koca").ToList();
       _ =  data.Where(x => x.Name == "zehra").First();
       _ =  data.Where(x => x.Name == "zehra").FirstOrDefault();
       _ =  data.Where(x => x.Name == "zehra").LastOrDefault();

       _ = data.ElementAt(1);

       var myData = data.ToList();
       // add a new record
       myData.Add(new Student());
       // delete a record
       myData.Remove(data.ElementAt(1));
       // check if exist
       myData.Exists(x=>x.Name == "zehra"); // same with Any(....)
       // check if exist. not so useful
       myData.Select(x=>x.Name).Contains(data.ElementAt(1).Name);
       // update record 
       var firstElement = myData.ElementAt(0);
       firstElement.Name = "zehra-new"; 
       
       
       // JOIN
       var onlyCourses = courses.ToList(); // course.Students are always null
       var coursesIncludingStudents = courses.AsQueryable().Include(x => x.CourseStudents).ToList(); // course.Students are full now

    }

    private void NullableSample()
    {
        var x = 12;
        int y = 12;
        int? z = 12;
        //y = null; // cannot assign
        z = null;
        int? k = 3;
        k = z;
        y = z.HasValue ? z.Value : 0;
        y = z.GetValueOrDefault(0);
    }

}