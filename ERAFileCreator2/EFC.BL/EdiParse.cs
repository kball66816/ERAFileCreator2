using System;
using System.Globalization;
using PatientManagement.DAL;
using PatientManagement.Model;
using System.Linq;
using System.Text;

namespace EFC.BL
{
    public static class EdiParse
    {
        private static readonly char[] SegmentDelimiter = { '*', ':' };
        private static readonly StringBuilder Sb = new StringBuilder();
        private static PrimaryCharge Charge { get; set; }
        private static Patient Patient { get; set; }
        private static readonly IPrimaryChargeRepository ChargeRepository = new PrimaryChargeRepository();
        private static readonly IPatientRepository PatientRepository = new PatientRepository();
        private static PrimaryCharge LastCharge = new PrimaryCharge();

        public static void Parse837Loop(this string loop)
        {
            ParsePatientDetails(loop);
            AppendNm1EnvelopInformation(loop);
            ResetAtEndOfFile(loop);
        }

        private static void ParsePatientDetails(string loop)
        {
            if (loop.StartsWith("NM1*IL"))
            {
                var patientDetails = loop.Split(SegmentDelimiter);
                IdentifyPatient(patientDetails);
                DeleteEmptyLastPatient();
                PatientRepository.Add(Patient);
            }

            if (loop.StartsWith("CLM"))
            {
                AddNewChargeToList();
                var segments = loop.Split(SegmentDelimiter);
                {
                    CheckIfCurrentChargeShouldBeGroupedWithLastEncounter(segments);
                    IdentifyCharge(segments);
                }
            }
            if (loop.StartsWith("SV1"))
            {
                var segments = loop.Split(SegmentDelimiter);
                {
                    MatchServiceDetailsBasedOnModifiers(segments);
                }
            }

            if (loop.StartsWith("DTP"))
            {
                var segments = loop.Split(SegmentDelimiter);
                {
                    var date = DateTime.ParseExact(segments[3], "yyyyMMdd", CultureInfo.InvariantCulture);
                    Charge.DateOfService = date;
                }
            }

            if (loop.StartsWith("REF*6R"))
            {
                var segments = loop.Split(SegmentDelimiter);
                {
                    Charge.ReferenceId = segments[2];
                }
                LastCharge = new PrimaryCharge(Charge);
            }
        }

        private static void MatchServiceDetailsBasedOnModifiers(string[] segments)
        {
            Charge.ProcedureCode = segments[2];
            switch (segments.Length)
            {
                case 13:
                    {
                        Charge.Modifier.ModifierOne = segments[3];
                        Charge.Modifier.ModifierTwo = segments[4];
                        Charge.Modifier.ModifierThree = segments[5];
                        Charge.Modifier.ModifierFour = segments[6];

                        decimal.TryParse(segments[7], out var result);
                        {
                            Charge.ChargeCost = result;
                            Charge.PaymentAmount = result;
                        }
                        break;
                    }
                case 12:
                    {
                        Charge.Modifier.ModifierOne = segments[3];
                        Charge.Modifier.ModifierTwo = segments[4];
                        Charge.Modifier.ModifierThree = segments[5];
                        decimal.TryParse(segments[6], out var result);
                        {
                            Charge.ChargeCost = result;
                            Charge.PaymentAmount = result;
                        }
                        break;
                    }
                case 11:
                    {
                        Charge.Modifier.ModifierOne = segments[3];
                        Charge.Modifier.ModifierTwo = segments[4];
                        decimal.TryParse(segments[5], out var result);
                        {
                            Charge.ChargeCost = result;
                            Charge.PaymentAmount = result;
                        }
                        break;
                    }
                case 10:
                    {
                        Charge.Modifier.ModifierOne = segments[3];
                        decimal.TryParse(segments[4], out var result);
                        {
                            Charge.ChargeCost = result;
                            Charge.PaymentAmount = result;
                        }
                        break;
                    }
            }
        }

        private static void CheckIfCurrentChargeShouldBeGroupedWithLastEncounter(string[] segments)
        {

            if (segments[1] != LastCharge.BillId)
            {
                var clone = PatientRepository.GetSelectedPatient(Patient.Id).CopyPatient();
                Patient = clone;
                PatientRepository.Add(Patient);
            }
        }

        private static void IdentifyCharge(string[] segments)
        {
            Charge.PatientId = Patient.Id;
            Charge.BillId = segments[1];
            Charge.PlaceOfService.ServiceLocation = segments[5];
        }

        private static void AddNewChargeToList()
        {
            Charge = new PrimaryCharge();
            ChargeRepository.Add(Charge);
        }

        private static void DeleteEmptyLastPatient()
        {
            if (string.IsNullOrEmpty(PatientRepository.GetAllPatients().LastOrDefault()?.FirstName))
            {
                PatientRepository.Delete(PatientRepository.GetAllPatients().LastOrDefault());
            }
        }

        private static void IdentifyPatient(string[] patientDetails)
        {
            Patient = new Patient
            {
                LastName = patientDetails[3],
                FirstName = patientDetails[4],
                MemberId = patientDetails[9]
            };
        }

        private static void AppendNm1EnvelopInformation(string loop)
        {
            if (!loop.StartsWith("NM1*IL"))
            {
                Sb.Append(loop);
            }
        }

        private static void ResetAtEndOfFile(string loop)
        {
            if (loop.StartsWith("IEA"))
            {
                Sb.Clear();
            }
        }
    }
}
