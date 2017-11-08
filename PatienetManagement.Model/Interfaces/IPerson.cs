using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Model.Interfaces
{
    interface IPerson
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string MiddleInitial { get; set; }

        string Prefix { get; set; }

        string Suffix { get; set; }

    }
}
