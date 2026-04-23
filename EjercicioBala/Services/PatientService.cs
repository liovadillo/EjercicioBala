using EjercicioBala.DTO;
using EjercicioBala.Models;

namespace EjercicioBala.Services
{
    public class PatientService : IPatientService
    {
        private static List<Patient> addedPatients = new List<Patient>
        {
            new Patient{Id=1, Name="Evelio", Age=35, Diagnosis="diabetes", IsActive=true},
            new Patient{Id=2, Name="Antonio", Age=34, Diagnosis="allopecy", IsActive=true},
            new Patient{Id=3, Name="Daniela", Age=27, Diagnosis="diabetes", IsActive=false}
        };

        public bool Delete(int id)
        {
            var patient = addedPatients.FirstOrDefault(p => p.Id == id);
            if (patient == null)
                return false;

            addedPatients.Remove(patient);
            return true;
        }

        public IEnumerable<Patient> GetAll()
        {
            return addedPatients;
        }

        public IEnumerable<Patient> GetAllActive()
        {
            var activePatients = addedPatients.Where(p => p.IsActive).ToList();
            return activePatients;
        }

        public IEnumerable<Patient> GetByDiagnosis(string diagnosis)
        {
            var diagnosedPatients = addedPatients.Where(p => p.Diagnosis.Contains(diagnosis)).ToList();
            return diagnosedPatients;
        }

        public Patient? GetById(int id)
        {
            var patient = addedPatients.FirstOrDefault(p => p.Id == id);
            return patient;
        }

        public Patient Insert(Patient obj)
        {
            var nextID = addedPatients.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1;
            var patient = new Patient
            {
                Id = nextID,
                Name = obj.Name,
                Age = obj.Age,
                Diagnosis = obj.Diagnosis,
                IsActive = obj.IsActive
            };

            addedPatients.Add(patient);

            return patient;
        }

        public Patient? Update(int id, Patient obj)
        {
            var patientIndex = addedPatients.FindIndex(p => p.Id == id);

            if (patientIndex == -1)
                return null;

            var patient = addedPatients[patientIndex];
            patient.Name = obj.Name;
            patient.Age = obj.Age;
            patient.IsActive = obj.IsActive;
            patient.Diagnosis = obj.Diagnosis;

            return patient;
        }

        public Patient? UpdateStatus(int id, StatusUpdateDto obj)
        {
            var patientIndex = addedPatients.FindIndex(p => p.Id == id);

            if (patientIndex == -1)
                return null;

            var patient = addedPatients[patientIndex];
            patient.IsActive = obj.IsActive;

            return patient;
        }
    }
}
