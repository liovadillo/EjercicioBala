using EjercicioBala.Models;

namespace EjercicioBala.Services
{
    public class DoctorService : IDoctorService
    {
        private static List<Doctor> addedDoctors = new List<Doctor>
        {
            new Doctor{Id=1, Name="Jose", Specialty="diabetes", IsAvailable=true, YearsOfExperience = 7},
            new Doctor{Id=2, Name="Mario", Specialty="allopecy", IsAvailable=true, YearsOfExperience = 5},
            new Doctor{Id=3, Name="Carolina", Specialty="diabetes", IsAvailable=false, YearsOfExperience = 10}
        };

        public bool Delete(int id)
        {
            var doctor = addedDoctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null)
                return false;

            addedDoctors.Remove(doctor);
            return true;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return addedDoctors;
        }

        public IEnumerable<Doctor> GetAllAvailable()
        {
            return addedDoctors.Where(d => d.IsAvailable).ToList();
        }

        public IEnumerable<Doctor> GetAllSpeciality(string speciality)
        {
            return addedDoctors.Where(d => d.Specialty.Contains(speciality)).ToList();
        }

        public Doctor? GetById(int id)
        {
            var doctor = addedDoctors.FirstOrDefault(d => d.Id == id);
            return doctor;
        }

        public Doctor Insert(Doctor obj)
        {
            var nextID = addedDoctors.Select(p => p.Id).DefaultIfEmpty(0).Max() + 1;
            var doctor = new Doctor { 
                Id = nextID, 
                Name = obj.Name, 
                Specialty = obj.Specialty, 
                IsAvailable = obj.IsAvailable, 
                YearsOfExperience = obj.YearsOfExperience 
            };
            
            addedDoctors.Add(doctor);

            return doctor;
        }

        public Doctor? Update(int id, Doctor obj)
        {
            var doctor = addedDoctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null)
                return null;

            doctor.Name = obj.Name;
            doctor.Specialty = obj.Specialty;
            doctor.YearsOfExperience = obj.YearsOfExperience;
            doctor.IsAvailable = obj.IsAvailable;

            return doctor;
        }
    }
}
