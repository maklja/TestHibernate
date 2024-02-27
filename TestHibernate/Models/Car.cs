using System.Collections.Generic;

namespace TestHibernate.Models
{
    /// <summary>
    /// Meny to many entity test
    /// </summary>
    public class Car
    {
        public virtual int Id { get; set; }
        public virtual string RegistrationNumber { get; set; }
        public virtual string BrandName { get; set; }
        public virtual IList<Company> Companies { get; set; } = new List<Company>();

        public virtual void addCompany(Company company)
        {
            Companies.Add(company);
            company.Cars.Add(this);
        }
    }
}
