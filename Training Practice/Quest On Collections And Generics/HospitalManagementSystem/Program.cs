// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


// Task 1: Implement Patient class with proper encapsulation
using System ;

public class Patient
{
    // TODO: Add properties with get/set accessors
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Condition { get; set; }

    // TODO: Add constructor
    public Patient(int id, string name, int age, string condition)
    {
        Id = id ;
        Name = name ;
        Age = age ;
        Condition = condition ;
    }
}

// Task 2: Implement HospitalManager class
public class HospitalManager
{
    private Dictionary<int, Patient> _patients = new Dictionary<int, Patient>();
    private Queue<Patient> _appointmentQueue = new Queue<Patient>();
    
    // Add a new patient to the system
    public void RegisterPatient(int id, string name, int age, string condition)
    {
        // TODO: Create patient and add to dictionary
        Patient patient = new Patient(id, name, age, condition) ;
        
        _patients.Add(patient.Id, patient) ;
    }
    
    // Add patient to appointment queue
    public void ScheduleAppointment(int patientId)
    {
        // TODO: Find patient and add to queue
        if (_patients.ContainsKey(patientId))
        {
            _appointmentQueue.Enqueue(_patients[patientId]) ;
        } else
        {
            Console.WriteLine($"No Patient Found with The Id : {patientId}") ;
        }
    }
    
    // Process next appointment (remove from queue)
    public Patient ProcessNextAppointment()
    {
        // TODO: Return and remove next patient from queue
        if (_appointmentQueue.Count > 0)
        {
            return _appointmentQueue.Dequeue() ;
            // return _appointmentQueue.Peek() ;
        } else
        {
            Console.WriteLine("The Patient Queue is already Empty..!");
            return null ;
        }
    }
    
    // Find patients with specific condition using LINQ
    public List<Patient> FindPatientsByCondition(string condition)
    {
        // TODO: Use LINQ to filter patients

        // return _patients.Values.Where(ele => ele.Condition == condition).ToList();


        return _patients.Where(ele => ele.Value.Condition == condition)
        .Select(ele => ele.Value)
        .ToList() ;

        // List<Patient> patientList = new List<Patient>() ;
        // foreach (var ele in _patients)
        // {
        //     if (ele.Value.Condition == condition)
        //     {
        //         patientList.Add(_patients[ele.Value.Id]);
        //     }
        // }
        // return patientList ;
    }
}


// Main / Program Class
public class Program
{
    public static void Main(string[] args)
    {
        HospitalManager manager = new HospitalManager();

        manager.RegisterPatient(1, "John Doe", 45, "Hypertension");
        manager.RegisterPatient(2, "Jane Smith", 32, "Diabetes");
        manager.ScheduleAppointment(1);
        manager.ScheduleAppointment(2);

        var nextPatient = manager.ProcessNextAppointment();
        Console.WriteLine(nextPatient.Name); // Should output: John Doe

        var diabeticPatients = manager.FindPatientsByCondition("Diabetes");
        Console.WriteLine(diabeticPatients.Count); // Should output: 1

    }
}
