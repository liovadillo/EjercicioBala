using EjercicioBala.Models;

namespace EjercicioBala.Services
{
    public interface IDoctorService
    {
        public IEnumerable<Doctor> GetAll();
        public Doctor? GetById(int id);
        public Doctor Insert(Doctor obj);
        public Doctor? Update(int id, Doctor obj);
        public bool Delete(int id);
        public IEnumerable<Doctor> GetAllAvailable();
        public IEnumerable<Doctor> GetAllSpeciality(string speciality);

    }
}
