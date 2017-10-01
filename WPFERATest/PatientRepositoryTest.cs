﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using EFC.BL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace WPFERATest
{
    [TestClass]
    public class PatientRepositoryTest
    {
        [TestMethod]
        public void AddTest()
        {
            //Arrange
            PatientRepository pr = new PatientRepository();

            //Act
            var patient = new Patient()
            {
                FirstName = "John"
            };
            pr.Add(patient);

            var expected = "John";
            var actual = pr.GetAllPatients().First().FirstName;
            //Assert
            Assert.AreEqual(expected, actual);


        }
        [TestMethod]
        public void DeleteTest()
        {
            //Arrange
            PatientRepository pr = new PatientRepository();

            //Act
            var patient = new Patient()
            {
                FirstName = "John"
            };
            pr.Add(patient);
            pr.Delete(patient);

            var expected = 0;
            var actual = pr.GetAllPatients().Count;
            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
