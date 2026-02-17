// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");


using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleHospitalApp
{
    public interface IBillable
    {
        double CalculateBill();
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Doctor : Person, IBillable
    {
        public string Specialization { get; set; }
        public double ConsultationFee { get; set; }
        public List<Appointment> Appointments { get; set; }

        public Doctor(int id, string name, string spec, double fee) : base(id, name)
        {
            Specialization = spec;
            ConsultationFee = fee;
            Appointments = new List<Appointment>();
        }

        public double CalculateBill()
        {
            return Appointments.Sum(a => a.BillAmount);
        }
    }

    public class Patient : Person
    {
        public string Disease { get; set; }

        public Patient(int id, string name, string disease) : base(id, name)
        {
            Disease = disease;
        }
    }

    public class MedicalRecord
    {
        private string Diagnosis;
        private string Treatment;

        public int RecordId { get; set; }
        public int PatientId { get; set; }

        public MedicalRecord(int id, int patientId, string diag, string treat)
        {
            RecordId = id;
            PatientId = patientId;
            Diagnosis = diag;
            Treatment = treat;
        }

        public string GetDetails()
        {
            return Diagnosis + " - " + Treatment;
        }
    }

    public class Appointment
    {
        public int AppointmentId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }
        public double BillAmount { get; set; }

        public Appointment(int id, Doctor doc, Patient pat, DateTime date)
        {
            AppointmentId = id;
            Doctor = doc;
            Patient = pat;
            Date = date;
            BillAmount = doc.ConsultationFee;
        }
    }

    public class DoctorNotAvailableException : Exception
    {
        public DoctorNotAvailableException(string msg) : base(msg) { }
    }

    public class InvalidAppointmentException : Exception
    {
        public InvalidAppointmentException(string msg) : base(msg) { }
    }

    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException(string msg) : base(msg) { }
    }

    public class DuplicateMedicalRecordException : Exception
    {
        public DuplicateMedicalRecordException(string msg) : base(msg) { }
    }

    class Program
    {
        static void ScheduleAppointment(List<Appointment> appointments, Doctor doc, Patient pat, DateTime date)
        {
            if (pat == null)
                throw new PatientNotFoundException("Patient not found");

            bool overlap = appointments.Any(a => a.Doctor.Id == doc.Id && a.Date == date);
            if (overlap)
                throw new DoctorNotAvailableException("Doctor not available at this time");

            Appointment appt = new Appointment(appointments.Count + 1, doc, pat, date);
            appointments.Add(appt);
            doc.Appointments.Add(appt);
        }

        static void Main(string[] args)
        {
            List<Doctor> doctors = new List<Doctor>();
            List<Patient> patients = new List<Patient>();
            List<Appointment> appointments = new List<Appointment>();
            Dictionary<int, MedicalRecord> records = new Dictionary<int, MedicalRecord>();

            doctors.Add(new Doctor(1, "Dr. Sharma", "Cardiology", 1000));
            doctors.Add(new Doctor(2, "Dr. Mehta", "Orthopedic", 800));
            doctors.Add(new Doctor(3, "Dr. Rao", "Neurology", 1200));

            patients.Add(new Patient(1, "Rahul", "Heart"));
            patients.Add(new Patient(2, "Ritika", "Bone"));
            patients.Add(new Patient(3, "Amit", "Headache"));
            patients.Add(new Patient(4, "Sneha", "Heart"));

            try
            {
                ScheduleAppointment(appointments, doctors[0], patients[0], DateTime.Now.AddDays(-2));
                ScheduleAppointment(appointments, doctors[1], patients[1], DateTime.Now.AddDays(-10));
                ScheduleAppointment(appointments, doctors[2], patients[2], DateTime.Now.AddDays(-5));
                ScheduleAppointment(appointments, doctors[0], patients[3], DateTime.Now.AddDays(-1));

                MedicalRecord rec1 = new MedicalRecord(1, 1, "Heart Issue", "Medication");
                if (records.ContainsKey(rec1.RecordId))
                    throw new DuplicateMedicalRecordException("Duplicate record");
                records[rec1.RecordId] = rec1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Doctors with more than 1 appointment:");
            var busyDoctors = doctors.Where(d => d.Appointments.Count > 1);
            foreach (var d in busyDoctors)
                Console.WriteLine(d.Name);

            Console.WriteLine("Patients treated in last 30 days:");
            var recentPatients = appointments
                .Where(a => a.Date >= DateTime.Now.AddDays(-30))
                .Select(a => a.Patient.Name)
                .Distinct();
            foreach (var p in recentPatients)
                Console.WriteLine(p);

            Console.WriteLine("Appointments grouped by doctor:");
            var grouped = appointments.GroupBy(a => a.Doctor.Name);
            foreach (var g in grouped)
                Console.WriteLine(g.Key + " - " + g.Count());

            Console.WriteLine("Top 3 highest earning doctors:");
            var topDocs = doctors
                .Select(d => new { Name = d.Name, Earned = d.CalculateBill() })
                .OrderByDescending(x => x.Earned)
                .Take(3);
            foreach (var d in topDocs)
                Console.WriteLine(d.Name + " - " + d.Earned);

            Console.WriteLine("Patients by disease (Heart):");
            var heartPatients = patients.Where(p => p.Disease == "Heart");
            foreach (var p in heartPatients)
                Console.WriteLine(p.Name);

            Console.WriteLine("Total Revenue:");
            Console.WriteLine(appointments.Sum(a => a.BillAmount));

            Console.WriteLine("Appointment Report:");
            var report = appointments.Select(a => new
            {
                a.AppointmentId,
                Doctor = a.Doctor.Name,
                Patient = a.Patient.Name,
                a.Date,
                a.BillAmount
            });

            foreach (var r in report)
                Console.WriteLine(r.AppointmentId + " | " + r.Doctor + " | " + r.Patient + " | " + r.Date.ToShortDateString() + " | " + r.BillAmount);
        }
    }
}
