// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Linq;

public interface IPatient
{
    int PatientId { get; }
    string Name { get; }
    DateTime DateOfBirth { get; }
    BloodType BloodType { get; }
}

public enum BloodType { A, B, AB, O }
public enum Condition { Stable, Critical, Recovering }

// 1. Generic patient queue with priority
public class PriorityQueue<T> where T : IPatient
{
    private SortedDictionary<int, Queue<T>> _queues = new();
    
    // TODO: Enqueue patient with priority (1=highest, 5=lowest)
    public void Enqueue(T patient, int priority)
    {
        if (priority < 1 || priority > 5)
            throw new ArgumentException("Priority must be between 1 and 5");

        if (!_queues.ContainsKey(priority))
            _queues[priority] = new Queue<T>();

        _queues[priority].Enqueue(patient);
        Console.WriteLine($"{patient.Name} added with priority {priority}");
    }
    
    // TODO: Dequeue highest priority patient
    public T Dequeue()
    {
        foreach (var queue in _queues.OrderBy(q => q.Key))
        {
            if (queue.Value.Count > 0)
                return queue.Value.Dequeue();
        }

        throw new InvalidOperationException("Queue is empty");
    }
    
    // TODO: Peek without removing
    public T Peek()
    {
        foreach (var queue in _queues.OrderBy(q => q.Key))
        {
            if (queue.Value.Count > 0)
                return queue.Value.Peek();
        }

        throw new InvalidOperationException("Queue is empty");
    }
    
    // TODO: Get count by priority
    public int GetCountByPriority(int priority)
    {
        if (_queues.ContainsKey(priority))
            return _queues[priority].Count;

        return 0;
    }
}

// 2. Generic medical record
public class MedicalRecord<T> where T : IPatient
{
    private T _patient;
    private List<(DateTime date, string diagnosis)> _diagnoses = new();
    private Dictionary<DateTime, string> _treatments = new();
    
    public MedicalRecord(T patient)
    {
        _patient = patient;
    }

    // TODO: Add diagnosis with date
    public void AddDiagnosis(string diagnosis, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(diagnosis))
            return;

        _diagnoses.Add((date, diagnosis));
    }
    
    // TODO: Add treatment
    public void AddTreatment(string treatment, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(treatment))
            return;

        _treatments[date] = treatment;
    }
    
    // TODO: Get treatment history
    public IEnumerable<KeyValuePair<DateTime, string>> GetTreatmentHistory()
    {
        return _treatments.OrderBy(t => t.Key);
    }
}

// 3. Specialized patient types
public class PediatricPatient : IPatient
{
    public int PatientId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public BloodType BloodType { get; set; }
    public string GuardianName { get; set; }
    public double Weight { get; set; } // in kg
}

public class GeriatricPatient : IPatient
{
    public int PatientId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public BloodType BloodType { get; set; }
    public List<string> ChronicConditions { get; } = new();
    public int MobilityScore { get; set; } // 1-10
}

// 4. Generic medication system
public class MedicationSystem<T> where T : IPatient
{
    private Dictionary<T, List<(string medication, DateTime time)>> _medications = new();
    
    // TODO: Prescribe medication with dosage validation
    public void PrescribeMedication(T patient, string medication, 
        Func<T, bool> dosageValidator)
    {
        if (!dosageValidator(patient))
        {
            Console.WriteLine($"Medication {medication} not suitable for {patient.Name}");
            return;
        }

        if (!_medications.ContainsKey(patient))
            _medications[patient] = new List<(string, DateTime)>();

        _medications[patient].Add((medication, DateTime.Now));
        Console.WriteLine($"Prescribed {medication} to {patient.Name}");
    }
    
    // TODO: Check for drug interactions
    public bool CheckInteractions(T patient, string newMedication)
    {
        if (!_medications.ContainsKey(patient))
            return false;

        var existing = _medications[patient].Select(m => m.medication);

        // Simple example interaction rule
        if (existing.Contains("Aspirin") && newMedication == "Ibuprofen")
            return true;

        return false;
    }
}

// 5. TEST SCENARIO: Simulate hospital workflow
// a) Create 2 PediatricPatient and 2 GeriatricPatient
// b) Add them to priority queue with different priorities
// c) Create medical records with diagnoses/treatments
// d) Prescribe medications with type-specific validation
// e) Demonstrate:
//    - Priority-based patient processing
//    - Age-specific medication validation
//    - Treatment history retrieval
//    - Drug interaction checking

class Program
{
    static void Main()
    {
        var queue = new PriorityQueue<IPatient>();

        // a) Create patients
        var p1 = new PediatricPatient { PatientId = 1, Name = "Riya", DateOfBirth = new DateTime(2018, 5, 1), BloodType = BloodType.A, GuardianName = "Mrs. Sharma", Weight = 18 };
        var p2 = new PediatricPatient { PatientId = 2, Name = "Aman", DateOfBirth = new DateTime(2016, 3, 10), BloodType = BloodType.O, GuardianName = "Mr. Khan", Weight = 22 };

        var g1 = new GeriatricPatient { PatientId = 3, Name = "Mr. Verma", DateOfBirth = new DateTime(1950, 2, 2), BloodType = BloodType.B, MobilityScore = 5 };
        var g2 = new GeriatricPatient { PatientId = 4, Name = "Mrs. Rao", DateOfBirth = new DateTime(1945, 7, 7), BloodType = BloodType.AB, MobilityScore = 3 };

        // b) Add to priority queue
        queue.Enqueue(g1, 1);
        queue.Enqueue(p1, 2);
        queue.Enqueue(g2, 1);
        queue.Enqueue(p2, 3);

        Console.WriteLine("\nNext patient: " + queue.Peek().Name);

        Console.WriteLine("\nProcessing patients by priority:");
        while (true)
        {
            try
            {
                var patient = queue.Dequeue();
                Console.WriteLine("Treating: " + patient.Name);
            }
            catch
            {
                break;
            }
        }

        // c) Medical records
        var record = new MedicalRecord<PediatricPatient>(p1);
        record.AddDiagnosis("Flu", DateTime.Today.AddDays(-2));
        record.AddTreatment("Paracetamol", DateTime.Today.AddDays(-1));

        Console.WriteLine("\nTreatment History:");
        foreach (var t in record.GetTreatmentHistory())
        {
            Console.WriteLine($"{t.Key.ToShortDateString()} - {t.Value}");
        }

        // d) Medication system
        var medSystem = new MedicationSystem<PediatricPatient>();

        // Pediatric: weight-based validation
        medSystem.PrescribeMedication(p1, "Paracetamol", patient => patient.Weight > 15);

        // Drug interaction test
        medSystem.PrescribeMedication(p1, "Aspirin", patient => true);
        bool interaction = medSystem.CheckInteractions(p1, "Ibuprofen");
        Console.WriteLine("\nInteraction between Aspirin and Ibuprofen: " + interaction);

        Console.WriteLine("\nSimulation Complete.");
    }
}
