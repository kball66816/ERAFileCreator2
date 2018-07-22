namespace PatientManagement.DAL.Interfaces
{
    internal interface IPerson
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string MiddleInitial { get; set; }

        string Prefix { get; set; }

        string Suffix { get; set; }
    }
}