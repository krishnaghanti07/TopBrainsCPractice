// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleLearningApp
{
    public class Course : IComparable<Course>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MaxCapacity { get; set; }
        public double Rating { get; set; }
        public int InstructorId { get; set; }

        public Course(int id, string title, int capacity, double rating, int instructorId)
        {
            Id = id;
            Title = title;
            MaxCapacity = capacity;
            Rating = rating;
            InstructorId = instructorId;
        }

        public int CompareTo(Course other)
        {
            return this.Title.CompareTo(other.Title);
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Instructor(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public Enrollment(int sid, int cid)
        {
            StudentId = sid;
            CourseId = cid;
        }
    }

    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public DateTime Deadline { get; set; }

        public Assignment(int id, int courseId, DateTime deadline)
        {
            Id = id;
            CourseId = courseId;
            Deadline = deadline;
        }

        public void Submit(DateTime submitDate)
        {
            if (submitDate > Deadline)
                throw new Exception("Submission after deadline not allowed");
        }
    }

    public interface IRepository<T>
    {
        void Add(T item);
        IEnumerable<T> GetAll();
    }

    public class Repository<T> : IRepository<T>
    {
        private List<T> data = new List<T>();

        public void Add(T item)
        {
            data.Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return data;
        }
    }

    class Program
    {
        static void EnrollStudent(List<Enrollment> enrollments, List<Course> courses, int studentId, int courseId)
        {
            if (enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
                throw new Exception("Student already enrolled");

            int count = enrollments.Count(e => e.CourseId == courseId);
            var course = courses.First(c => c.Id == courseId);

            if (count >= course.MaxCapacity)
                throw new Exception("Course capacity full");

            enrollments.Add(new Enrollment(studentId, courseId));
        }

        static void Main(string[] args)
        {
            IRepository<Course> courseRepo = new Repository<Course>();
            IRepository<Student> studentRepo = new Repository<Student>();
            IRepository<Instructor> instructorRepo = new Repository<Instructor>();

            List<Enrollment> enrollments = new List<Enrollment>();

            instructorRepo.Add(new Instructor(1, "Mr. Sharma"));
            instructorRepo.Add(new Instructor(2, "Ms. Rao"));

            courseRepo.Add(new Course(1, "C# Basics", 60, 4.5, 1));
            courseRepo.Add(new Course(2, "Data Structures", 40, 4.7, 1));
            courseRepo.Add(new Course(3, "Web Development", 100, 4.3, 2));

            studentRepo.Add(new Student(1, "Rahul"));
            studentRepo.Add(new Student(2, "Ritika"));
            studentRepo.Add(new Student(3, "Amit"));
            studentRepo.Add(new Student(4, "Sneha"));

            try
            {
                EnrollStudent(enrollments, courseRepo.GetAll().ToList(), 1, 1);
                EnrollStudent(enrollments, courseRepo.GetAll().ToList(), 2, 1);
                EnrollStudent(enrollments, courseRepo.GetAll().ToList(), 3, 2);
                EnrollStudent(enrollments, courseRepo.GetAll().ToList(), 4, 3);
                EnrollStudent(enrollments, courseRepo.GetAll().ToList(), 1, 3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var courses = courseRepo.GetAll().ToList();
            var students = studentRepo.GetAll().ToList();
            var instructors = instructorRepo.GetAll().ToList();

            Console.WriteLine("Courses with more than 2 students:");
            var popularCourses = enrollments
                .GroupBy(e => e.CourseId)
                .Where(g => g.Count() > 2)
                .Select(g => courses.First(c => c.Id == g.Key).Title);

            foreach (var c in popularCourses)
                Console.WriteLine(c);

            Console.WriteLine("Students enrolled in more than 1 course:");
            var activeStudents = enrollments
                .GroupBy(e => e.StudentId)
                .Where(g => g.Count() > 1)
                .Select(g => students.First(s => s.Id == g.Key).Name);

            foreach (var s in activeStudents)
                Console.WriteLine(s);

            Console.WriteLine("Most popular course:");
            var mostPopular = enrollments
                .GroupBy(e => e.CourseId)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (mostPopular != null)
                Console.WriteLine(courses.First(c => c.Id == mostPopular.Key).Title);

            Console.WriteLine("Average course rating:");
            Console.WriteLine(courses.Average(c => c.Rating));

            Console.WriteLine("Instructor with highest enrollments:");
            var joinData = from e in enrollments
                           join c in courses on e.CourseId equals c.Id
                           group e by c.InstructorId into grp
                           orderby grp.Count() descending
                           select new { InstructorId = grp.Key, Count = grp.Count() };

            var topInstructor = joinData.FirstOrDefault();
            if (topInstructor != null)
                Console.WriteLine(instructors.First(i => i.Id == topInstructor.InstructorId).Name);

            Console.WriteLine("Courses sorted by title:");
            courses.Sort();
            foreach (var c in courses)
                Console.WriteLine(c.Title);
        }
    }
}
