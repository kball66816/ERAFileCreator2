using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace EFC.BL
{
    public static class EdiParse
    {
        private static readonly char[] SegmentDelimiter = new char[] { '*', ':' };
        private static bool _isClaimDetail;
        private static readonly StringBuilder Sb = new StringBuilder();
        private static bool _isClm;
        private static PrimaryCharge Charge { get; set; }
        private static Patient Patient { get; set; }
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
                Patient = new Patient();
                Charge = new PrimaryCharge();
                var patientDetails = loop.Split(SegmentDelimiter);
                Patient.LastName = patientDetails[3];
                Patient.FirstName = patientDetails[4];
                Patient.MemberId = patientDetails[9];
                IPatientRepository patientRepository = new PatientRepository();
                if (string.IsNullOrEmpty(patientRepository.GetAllPatients().First().FirstName))
                {
                    patientRepository.GetAllPatients().RemoveAt(0);
                }

                patientRepository.Add(Patient);
            }
            if (loop.StartsWith("CLM"))
            {
                _isClm = true;
                Charge.PatientId = Patient.Id;
                var segments = loop.Split(SegmentDelimiter);
                {
                    Charge.BillId = segments[1];
                    Charge.PlaceOfService.ServiceLocation = segments[5];
                }
            }
            if (loop.StartsWith("SV1") && _isClm)
            {
                var segments = loop.Split(SegmentDelimiter);
                {
                    Charge.ProcedureCode = segments[2];

                        if (segments.Length == 13)
                        {
                            Charge.Modifier.ModifierOne = segments[3];
                            Charge.Modifier.ModifierTwo = segments[4];
                            Charge.Modifier.ModifierThree = segments[5];
                            Charge.Modifier.ModifierFour = segments[6];

                            decimal.TryParse(segments[7], out decimal result);
                            {
                                Charge.ChargeCost = result;
                                Charge.PaymentAmount = result;
                            }
                        }
                        else if (segments.Length == 12)
                        {
                            Charge.Modifier.ModifierOne = segments[3];
                            Charge.Modifier.ModifierTwo = segments[4];
                            Charge.Modifier.ModifierThree = segments[5];
                            decimal.TryParse(segments[6], out decimal result);
                            {
                                Charge.ChargeCost = result;
                                Charge.PaymentAmount = result;
                            }
                        }

                        else if (segments.Length == 11)
                        {
                            Charge.Modifier.ModifierOne = segments[3];
                            Charge.Modifier.ModifierTwo = segments[4];
                            decimal.TryParse(segments[5], out decimal result);
                            {
                                Charge.ChargeCost = result;
                                Charge.PaymentAmount = result;
                            }
                        }

                        else if (segments.Length == 10)
                        {
                            Charge.Modifier.ModifierOne = segments[3];
                            decimal.TryParse(segments[4], out decimal result);
                            {
                                Charge.ChargeCost = result;
                                Charge.PaymentAmount = result;
                            }
                        }

                        IPrimaryChargeRepository chargeRepository = new PrimaryChargeRepository();
                        chargeRepository.Add(Charge);
                    Charge = new PrimaryCharge();
                }
            }
        }

        private static void AppendNm1EnvelopInformation(string loop)
        {
            if (_isClaimDetail && !loop.StartsWith("NM1*IL"))
            {
                Sb.Append(loop);
            }
        }

        private static void ResetAtEndOfFile(string loop)
        {
            if (loop.StartsWith("IEA"))
            {
                Sb.Clear();
                _isClaimDetail = false;
            }
        }
    }
}
