using System;
using System.Collections.Generic;

namespace TestHibernate.Models
{
    public class Company
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        // Embeded entity that will result to columns with in company table
        public virtual Location Address { get; set; }

        // Many to one property
        public virtual IList<Employee> Employees { get; set; } = new List<Employee>();

        // Many to many property
        public virtual IList<Car> Cars { get; set; } = new List<Car>();


        public virtual void AddEmployee(Employee employee) { 
            Employees.Add(employee); 
            employee.Company = this; 
        }

        public virtual void AddCar(Car car)
        {
            Cars.Add(car);
            car.Companies.Add(this);
        }
    }
}
