using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.Model;

namespace PatientManagement.DAL
{
    public interface IInsurance
    {
        void AddInsurance(InsuranceCompany insurance);
        InsuranceCompany GetInsurance();
    }
}
