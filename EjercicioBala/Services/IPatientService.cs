using EjercicioBala.DTO;
using EjercicioBala.Models;

namespace EjercicioBala.Services
{
    public interface IPatientService
    {
        public IEnumerable<Patient> GetAll();
        public IEnumerable<Patient> GetAllActive();
        public IEnumerable<Patient> GetByDiagnosis(string diagnosis);
        public Patient? GetById(int id);
        public Patient Insert(Patient obj);
        public Patient? Update(int id, Patient obj); //Update full Patient
        public bool Delete(int id);
        public Patient? UpdateStatus(int id, StatusUpdateDto obj);//Update Patient Status

    }
}
