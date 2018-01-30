﻿using System.Text;
using PatientManagement.Model;

namespace Edi835._835Segments
{
    public class Nm1 : SegmentBase
    {
        public Nm1(Patient patient)
        {
            SegmentIdentifier = "NM1";
            EntityTypeQualifier = "QC";
            EntityIdCode = "1";
            LastNameOrOrganizationName = patient.LastName;
            FirstName = patient.FirstName;
            MiddleName = patient.MiddleInitial;
            Suffix = patient.Suffix;
            Prefix = patient.Prefix;
            IdCodeQualifier = "MI";
            IdCode = patient.MemberId;
        }

        public Nm1(Provider renderingProvider)
        {
            SegmentIdentifier = "NM1";
            EntityTypeQualifier = "82";
            EntityIdCode = "1";
            LastNameOrOrganizationName = renderingProvider.LastName;
            FirstName = renderingProvider.FirstName;
            MiddleName = renderingProvider.MiddleInitial;
            Suffix = renderingProvider.Suffix;
            Prefix = renderingProvider.Prefix;
            IdCodeQualifier = "XX";
            IdCode = renderingProvider.Npi;
        }

        public Nm1(Subscriber subscriber)
        {
            SegmentIdentifier = "NM1";
            EntityTypeQualifier = "IL";
            LastNameOrOrganizationName = subscriber.LastName;
            FirstName = subscriber.FirstName;
            MiddleName = subscriber.MiddleInitial;
            Suffix = subscriber.Suffix;
            Prefix = subscriber.Prefix;
            IdCodeQualifier = "MI";
            IdCode = subscriber.MemberId;
        }

        private string EntityIdCode { get; }
        private string EntityTypeQualifier { get; }
        private string LastNameOrOrganizationName { get; }
        private string FirstName { get; }
        private string MiddleName { get; }
        private string Suffix { get; }
        private string Prefix { get; }
        private string IdCodeQualifier { get; }
        private string IdCode { get; }

        public string BuildNm1()
        {
            var buildNm1 = new StringBuilder();

            buildNm1.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(EntityTypeQualifier)
                .Append(DataElementTerminator)
                .Append(EntityIdCode)
                .Append(DataElementTerminator)
                .Append(LastNameOrOrganizationName)
                .Append(DataElementTerminator)
                .Append(FirstName)
                .Append(DataElementTerminator)
                .Append(MiddleName)
                .Append(DataElementTerminator)
                .Append(Prefix)
                .Append(DataElementTerminator)
                .Append(Suffix);
            AppendIfIdCodeOrQualifierExists(buildNm1);
            buildNm1.Append(SegmentTerminator);
            return buildNm1.ToString();
        }

        private void AppendIfIdCodeOrQualifierExists(StringBuilder buildNm1)
        {
            if (!string.IsNullOrEmpty(IdCodeQualifier) || string.IsNullOrEmpty(IdCode))
                buildNm1.Append(DataElementTerminator)
                    .Append(IdCodeQualifier)
                    .Append(DataElementTerminator)
                    .Append(IdCode);
        }
    }
}