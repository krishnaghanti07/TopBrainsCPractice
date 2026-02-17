// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Linq;

// Base constraints
public interface IStudent
{
    int StudentId { get; }
    string Name { get; }
    int Semester { get; }
}

public interface ICourse
{
    string CourseCode { get; }
    string Title { get; }
    int MaxCapacity { get; }
    int Credits { get; }
}

// 1. Generic enrollment system
public class EnrollmentSystem<TStudent, TCourse>
    where TStudent : IStudent
    where TCourse : ICourse
{
    private Dictionary<TCourse, List<TStudent>> _enrollments = new();
    
    // TODO: Enroll student with constraints
    public bool EnrollStudent(TStudent student, TCourse course)
    {
        if (student == null || course == null)
        {
            Console.WriteLine("Invalid student or course.");
            return false;
        }

        if (!_enrollments.ContainsKey(course))
            _enrollments[course] = new List<TStudent>();

        var students = _enrollments[course];

        // - Course not at capacity
        if (students.Count >= course.MaxCapacity)
        {
            Console.WriteLine($"Enrollment failed: {course.Title} is full.");
            return false;
        }

        // - Student not already enrolled
        if (students.Any(s => s.StudentId == student.StudentId))
        {
            Console.WriteLine("Enrollment failed: Student already enrolled.");
            return false;
        }

        // - Student semester >= course prerequisite (if any)
        if (course is LabCourse lab)
        {
            if (student.Semester < lab.RequiredSemester)
            {
                Console.WriteLine("Enrollment failed: Semester prerequisite not met.");
                return false;
            }
        }

        students.Add(student);
        Console.WriteLine($"Enrollment successful: {student.Name} -> {course.Title}");
        return true;
    }
    
    // TODO: Get students for course
    public IReadOnlyList<TStudent> GetEnrolledStudents(TCourse course)
    {
        if (_enrollments.ContainsKey(course))
            return _enrollments[course].AsReadOnly();

        return new List<TStudent>().AsReadOnly();
    }
    
    // TODO: Get courses for student
    public IEnumerable<TCourse> GetStudentCourses(TStudent student)
    {
        foreach (var entry in _enrollments)
        {
            if (entry.Value.Any(s => s.StudentId == student.StudentId))
                yield return entry.Key;
        }
    }
    
    // TODO: Calculate student workload
    public int CalculateStudentWorkload(TStudent student)
    {
        return GetStudentCourses(student).Sum(c => c.Credits);
    }
}

// 2. Specialized implementations
public class EngineeringStudent : IStudent
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public int Semester { get; set; }
    public string Specialization { get; set; }
}

public class LabCourse : ICourse
{
    public string CourseCode { get; set; }
    public string Title { get; set; }
    public int MaxCapacity { get; set; }
    public int Credits { get; set; }
    public string LabEquipment { get; set; }
    public int RequiredSemester { get; set; } // Prerequisite
}

// 3. Generic gradebook
public class GradeBook<TStudent, TCourse>
    where TStudent : IStudent
    where TCourse : ICourse
{
    private Dictionary<(TStudent, TCourse), double> _grades = new();
    private EnrollmentSystem<TStudent, TCourse> _enrollmentSystem;

    public GradeBook(EnrollmentSystem<TStudent, TCourse> enrollmentSystem)
    {
        _enrollmentSystem = enrollmentSystem;
    }
    
    // TODO: Add grade with validation
    public void AddGrade(TStudent student, TCourse course, double grade)
    {
        // Grade must be between 0 and 100
        if (grade < 0 || grade > 100)
        {
            Console.WriteLine("Invalid grade.");
            return;
        }

        // Student must be enrolled in course
        if (!_enrollmentSystem.GetStudentCourses(student).Contains(course))
        {
            Console.WriteLine("Cannot assign grade. Student not enrolled.");
            return;
        }

        _grades[(student, course)] = grade;
        Console.WriteLine($"Grade added: {student.Name} - {course.Title} = {grade}");
    }
    
    // TODO: Calculate GPA for student
    public double? CalculateGPA(TStudent student)
    {
        var studentGrades = _grades
            .Where(g => g.Key.Item1.StudentId == student.StudentId)
            .ToList();

        if (!studentGrades.Any())
            return null;

        double totalPoints = 0;
        int totalCredits = 0;

        foreach (var entry in studentGrades)
        {
            var course = entry.Key.Item2;
            totalPoints += entry.Value * course.Credits;
            totalCredits += course.Credits;
        }

        return totalPoints / totalCredits;
    }
    
    // TODO: Find top student in course
    public (TStudent student, double grade)? GetTopStudent(TCourse course)
    {
        var courseGrades = _grades
            .Where(g => EqualityComparer<TCourse>.Default.Equals(g.Key.Item2, course))
            .ToList();

        if (!courseGrades.Any())
            return null;

        var top = courseGrades.OrderByDescending(g => g.Value).First();
        return (top.Key.Item1, top.Value);
    }
}

// 4. TEST SCENARIO: Create a simulation
// a) Create 3 EngineeringStudent instances
// b) Create 2 LabCourse instances with prerequisites
// c) Demonstrate:
//    - Successful enrollment
//    - Failed enrollment (capacity, prerequisite)
//    - Grade assignment
//    - GPA calculation
//    - Top student per course

class Program
{
    static void Main()
    {
        var enrollment = new EnrollmentSystem<EngineeringStudent, LabCourse>();
        var gradeBook = new GradeBook<EngineeringStudent, LabCourse>(enrollment);

        // a) Create 3 EngineeringStudent instances
        var s1 = new EngineeringStudent { StudentId = 1, Name = "Rahul", Semester = 3, Specialization = "CSE" };
        var s2 = new EngineeringStudent { StudentId = 2, Name = "Anjali", Semester = 2, Specialization = "CSE" };
        var s3 = new EngineeringStudent { StudentId = 3, Name = "Vikram", Semester = 4, Specialization = "ECE" };

        // b) Create 2 LabCourse instances with prerequisites
        var c1 = new LabCourse { CourseCode = "CS301", Title = "Data Structures Lab", MaxCapacity = 2, Credits = 4, RequiredSemester = 3 };
        var c2 = new LabCourse { CourseCode = "CS201", Title = "Basic Electronics Lab", MaxCapacity = 1, Credits = 3, RequiredSemester = 2 };

        Console.WriteLine("\n--- Enrollment ---");

        // Successful enrollment
        enrollment.EnrollStudent(s1, c1);
        enrollment.EnrollStudent(s3, c1);

        // Failed (capacity)
        enrollment.EnrollStudent(s2, c1);

        // Failed (prerequisite)
        enrollment.EnrollStudent(s2, c1);

        // Successful
        enrollment.EnrollStudent(s2, c2);

        Console.WriteLine("\n--- Grades ---");

        gradeBook.AddGrade(s1, c1, 85);
        gradeBook.AddGrade(s3, c1, 92);
        gradeBook.AddGrade(s2, c2, 78);

        Console.WriteLine("\n--- GPA ---");
        var gpa = gradeBook.CalculateGPA(s1);
        if (gpa != null)
            Console.WriteLine($"GPA of {s1.Name}: {gpa:F2}");

        Console.WriteLine("\n--- Top Student ---");
        var top = gradeBook.GetTopStudent(c1);
        if (top != null)
            Console.WriteLine($"Top student in {c1.Title}: {top?.student.Name} ({top?.grade})");

        Console.WriteLine("\nSimulation Complete.");
    }
}
