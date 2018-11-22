using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.DAL;

namespace EraFileCreator.Mocks
{
    internal class PatientRepositoryMock : IPatientRepository
    {
        private readonly ObservableCollection<Patient> _patientList;
        public PatientRepositoryMock()
        {
            this._patientList = new ObservableCollection<Patient>
            {
                new Patient() {FirstName = "John", LastName = "Smith", MemberId = "1"},
                new Patient() {FirstName = "Erica", LastName = "Johnson", MemberId = "2"},
                new Patient() {FirstName = "Gaston", LastName = "Frerique", MemberId = "3"},
                new Patient() {FirstName = "Bella", LastName = "Donna", MemberId = "4"},
                new Patient() {FirstName = "Jacque", LastName = "Gusteau", MemberId = "5"},
                new Patient() {FirstName = "John", LastName = "Henry", MemberId = "6"},
                new Patient() {FirstName = "Jenna", LastName = "Smith", MemberId = "7"},
                new Patient() {FirstName = "Alexander", LastName = "Hamilton", MemberId = "8"},
                new Patient() {FirstName = "Tiana", LastName = "Guerrera", MemberId = "9"},
                new Patient() {FirstName = "Erin", LastName = "Harding", MemberId = "10"}
            };
        }
        public void Add(Patient patient)
        {
            this._patientList.Add(patient);
        }

        public void Delete(Patient patient)
        {
            this._patientList.Remove(patient);
        }

        public ObservableCollection<Patient> GetAllPatients()
        {
            return this._patientList;
        }

        public Patient GetSelectedPatient(Guid id)
        {
            return this._patientList.SingleOrDefault(p => p.Id == id);
        }
    }
}
