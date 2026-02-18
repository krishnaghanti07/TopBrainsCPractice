// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

[Serializable]
public class Applicant
{
    public string ApplicantId { get; set; }
    public string Name { get; set; }
    public string CurrentLocation { get; set; }
    public string PreferredLocation { get; set; }
    public string CoreCompetency { get; set; }
    public int PassingYear { get; set; }
}

class Program
{
    static string filePath = "applicants.json";
    static List<Applicant> applicants = new List<Applicant>();

    static void Main()
    {
        LoadFromFile();

        while (true)
        {
            Console.WriteLine("\n==== CampusHire Applicant Management ====");
            Console.WriteLine("1. Add Applicant");
            Console.WriteLine("2. Display All Applicants");
            Console.WriteLine("3. Search Applicant by ID");
            Console.WriteLine("4. Update Applicant");
            Console.WriteLine("5. Delete Applicant");
            Console.WriteLine("6. Exit");
            Console.Write("Select Option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddApplicant(); break;
                case "2": DisplayAll(); break;
                case "3": SearchApplicant(); break;
                case "4": UpdateApplicant(); break;
                case "5": DeleteApplicant(); break;
                case "6": SaveToFile(); return;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    static void AddApplicant()
    {
        Applicant app = new Applicant();

        Console.Write("Applicant ID: ");
        app.ApplicantId = Console.ReadLine();

        Console.Write("Applicant Name: ");
        app.Name = Console.ReadLine();

        Console.Write("Current Location (Mumbai/Pune/Chennai): ");
        app.CurrentLocation = Console.ReadLine();

        Console.Write("Preferred Location (Mumbai/Pune/Chennai/Delhi/Kolkata/Bangalore): ");
        app.PreferredLocation = Console.ReadLine();

        Console.Write("Core Competency (.NET/JAVA/ORACLE/Testing): ");
        app.CoreCompetency = Console.ReadLine();

        Console.Write("Passing Year: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Console.WriteLine("Invalid year.");
            return;
        }
        app.PassingYear = year;

        if (!ValidateApplicant(app))
            return;

        if (applicants.Any(a => a.ApplicantId == app.ApplicantId))
        {
            Console.WriteLine("Applicant ID already exists.");
            return;
        }

        applicants.Add(app);
        SaveToFile();
        Console.WriteLine("Applicant added successfully.");
    }

    static void DisplayAll()
    {
        if (!applicants.Any())
        {
            Console.WriteLine("No applicants found.");
            return;
        }

        foreach (var a in applicants)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"ID: {a.ApplicantId}");
            Console.WriteLine($"Name: {a.Name}");
            Console.WriteLine($"Current Location: {a.CurrentLocation}");
            Console.WriteLine($"Preferred Location: {a.PreferredLocation}");
            Console.WriteLine($"Core Competency: {a.CoreCompetency}");
            Console.WriteLine($"Passing Year: {a.PassingYear}");
        }
    }

    static void SearchApplicant()
    {
        Console.Write("Enter Applicant ID: ");
        string id = Console.ReadLine();

        var applicant = applicants.FirstOrDefault(a => a.ApplicantId == id);

        if (applicant == null)
        {
            Console.WriteLine("Applicant not found.");
            return;
        }

        Console.WriteLine($"Name: {applicant.Name}");
        Console.WriteLine($"Current Location: {applicant.CurrentLocation}");
        Console.WriteLine($"Preferred Location: {applicant.PreferredLocation}");
        Console.WriteLine($"Core Competency: {applicant.CoreCompetency}");
        Console.WriteLine($"Passing Year: {applicant.PassingYear}");
    }

    static void UpdateApplicant()
    {
        Console.Write("Enter Applicant ID to update: ");
        string id = Console.ReadLine();

        var applicant = applicants.FirstOrDefault(a => a.ApplicantId == id);

        if (applicant == null)
        {
            Console.WriteLine("Applicant not found.");
            return;
        }

        Console.Write("New Name: ");
        applicant.Name = Console.ReadLine();

        Console.Write("New Current Location: ");
        applicant.CurrentLocation = Console.ReadLine();

        Console.Write("New Preferred Location: ");
        applicant.PreferredLocation = Console.ReadLine();

        Console.Write("New Core Competency: ");
        applicant.CoreCompetency = Console.ReadLine();

        Console.Write("New Passing Year: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Console.WriteLine("Invalid year.");
            return;
        }
        applicant.PassingYear = year;

        if (!ValidateApplicant(applicant))
            return;

        SaveToFile();
        Console.WriteLine("Applicant updated successfully.");
    }

    static void DeleteApplicant()
    {
        Console.Write("Enter Applicant ID to delete: ");
        string id = Console.ReadLine();

        var applicant = applicants.FirstOrDefault(a => a.ApplicantId == id);

        if (applicant == null)
        {
            Console.WriteLine("Applicant not found.");
            return;
        }

        applicants.Remove(applicant);
        SaveToFile();
        Console.WriteLine("Applicant deleted successfully.");
    }

    static bool ValidateApplicant(Applicant app)
    {
        if (string.IsNullOrWhiteSpace(app.ApplicantId) ||
            string.IsNullOrWhiteSpace(app.Name) ||
            string.IsNullOrWhiteSpace(app.CurrentLocation) ||
            string.IsNullOrWhiteSpace(app.PreferredLocation) ||
            string.IsNullOrWhiteSpace(app.CoreCompetency))
        {
            Console.WriteLine("All fields are mandatory.");
            return false;
        }

        if (app.ApplicantId.Length != 8 || !app.ApplicantId.StartsWith("CH"))
        {
            Console.WriteLine("Applicant ID must be 8 characters and start with 'CH'.");
            return false;
        }

        if (app.Name.Length < 4 || app.Name.Length > 15)
        {
            Console.WriteLine("Name must be between 4 and 15 characters.");
            return false;
        }

        if (app.PassingYear > DateTime.Now.Year)
        {
            Console.WriteLine("Passing year cannot be in the future.");
            return false;
        }

        return true;
    }

    static void SaveToFile()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(applicants, options);
        File.WriteAllText(filePath, json);
    }

    static void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            applicants = JsonSerializer.Deserialize<List<Applicant>>(json) ?? new List<Applicant>();
        }
    }
}
