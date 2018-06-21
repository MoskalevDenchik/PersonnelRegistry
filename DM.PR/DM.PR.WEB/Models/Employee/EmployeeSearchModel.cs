using System.Collections.Generic;

namespace DM.PR.WEB.Models.Employee
{
    public class EmployeeSearchModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int FromYear { get; set; }

        public int ToYear { get; set; }

        public string WorkStatusId { get; set; }

       public IReadOnlyCollection<Common.Entities.WorkStatus> WorkStatusLst { get; set; }
    }
}