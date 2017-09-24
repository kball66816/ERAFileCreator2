using System.Text;
using PatientManagement.Model;

namespace EFC.BL.EDI_Segments
{
    class Nm1
    {
        public string BuildNm1(Patient patient)
        {
            var buildNm1 = new StringBuilder();
            //NM1 Patient Identifier 2100
            buildNm1.Append("NM1" + "*");
           buildNm1.Append("QC" + "*");  //NM101 Patient Identifier Code
           buildNm1.Append("1" + "*"); //NM102 Entity Type Qualifier
           buildNm1.Append(patient.LastName + "*");//NM103 Patient Last Name
           buildNm1.Append(patient.FirstName + "*"); //NM104 Patient First Name
           buildNm1.Append("*");
           buildNm1.Append(patient.MiddleInitial + "*"); //NM105 Patient Middle Initial
           buildNm1.Append("*"); //NM106 Name Prefix
           buildNm1.Append("*"); //NM107 Patient Name Suffix
           buildNm1.Append("MI" + "*");//NM108 Identification Code Qualifier
           buildNm1.Append(patient.MemberId); //NM109 Patient Member Number
           buildNm1.Append("~");
            if (patient.IncludeSubscriber == true)
            {
                //NM1 Insured Identifier 2100
                buildNm1.Append("NM1" + "*");
                buildNm1.Append("IL" + "*"); //NM101 Insured Name
                buildNm1.Append("1" + "*"); //NM102 Insured Entity Type Qualifier
                buildNm1.Append(patient.Subscriber.LastName + "*");//NM103 Insurance Last Name
                buildNm1.Append(patient.Subscriber.FirstName + "*");
                buildNm1.Append(patient.Subscriber.MiddleInitial + "*"); //NM105 Insured Middle Initial
                buildNm1.Append("*"); //NM106 Insured Name Prefix
                buildNm1.Append("*"); //NM107 Insured Name Suffix
                buildNm1.Append("MI" + "*"); //NM108 Identification Code Qualifier
                buildNm1.Append(patient.Subscriber.MemberId); //NM109 Insured Member Number
                buildNm1.Append("~");
            }
            //NM1 Service Provider 2100
            buildNm1.Append("NM1" + "*");
            buildNm1.Append("82" + "*"); //NM101 Entity Identifier Code
            buildNm1.Append("1" + "*"); //NM102 Entity Type Qualifier
            buildNm1.Append(patient.Provider.LastName + "*"); //NM103 Rendering Provider Last Name
            buildNm1.Append(patient.Provider.FirstName + "*");//NM104 Rendering Provider First Name
            buildNm1.Append(patient.Provider.MiddleInitial + "*"); //NM105 Rendering Provider Middle Name
            buildNm1.Append("*"); //NM106 Name Prefix
            buildNm1.Append("*"); //NM107 Rendering Provider Name Suffix
            buildNm1.Append("XX" + "*"); //NM108 Rendering Provider Identification Code Qualifier
            buildNm1.Append(patient.Provider.Npi); //NM109 Rendering Provider Identifier
            buildNm1.Append("~");

            //NM1 Crossover Carrier
            //NM1 Correct Priority Payer
            return buildNm1.ToString();

        }
    }
}
