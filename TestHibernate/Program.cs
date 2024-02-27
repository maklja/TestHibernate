using NHibernate;
using NHibernate.Linq;
using NHibernate.Util;
using System;
using System.Linq;
using TestHibernate.Models;

namespace TestHibernate
{
    internal class Program
    {
        private static Company TestNHibernateSession()
        {
            var address = new Location();
            address.City = "Novi Sad";
            address.Street = "Novosadskog sajma 2";

            var company = new Company();
            company.Name = "VegaIT";
            company.Address = address;

            var car = new Car();
            car.BrandName = "Mazda";
            car.RegistrationNumber = "NS999-WW";

            var employee1 = new Employee();
            employee1.FirstName = "FirstName1";
            employee1.LastName = "LastName1";
            employee1.Designation = "Senior";

            var employee2 = new Employee();
            employee2.FirstName = "FirstName2";
            employee2.LastName = "LastName2";
            employee2.Designation = "Junior";

            using (var session = NHibertnateSession.OpenSession())
            {
                company.AddEmployee(employee1);
                company.AddEmployee(employee2);
                company.AddCar(car);
                session.Save(company);
                session.Flush();

                //session.Delete(company);
                //session.Flush();
            }

            return company;
        }

        private static Company TestNHibernateTransaction()
        {
            var address = new Location();
            address.City = "Novi Sad";
            address.Street = "Novosadskog sajma 2";

            var company = new Company();
            company.Name = "VegaIT";
            company.Address = address;

            var car = new Car();
            car.BrandName = "Mazda";
            car.RegistrationNumber = "NS999-" + DateTime.Now; // registration number needs to be unique, duplicates will validate database constraint

            var employee1 = new Employee();
            employee1.FirstName = "FirstName1";
            employee1.LastName = "LastName1";
            employee1.Designation = "Senior";

            var employee2 = new Employee();
            employee2.FirstName = "FirstName2";
            employee2.LastName = "LastName2";
            employee2.Designation = "Junior";
            using (var session = NHibertnateSession.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    company.AddEmployee(employee1);
                    company.AddEmployee(employee2);
                    company.AddCar(car);
                    session.Save(company);

                    transaction.Commit();

                    return company;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }

        static void Main(string[] args)
        {
            //TestNHibernateSession();
            var company = TestNHibernateTransaction();
            using (var session = NHibertnateSession.OpenSession())
            {
                var savedCompany = session.Query<Company>().Where(curCompany => curCompany.Id == company.Id).First();
                Console.WriteLine($"Retrieved company with name {savedCompany.Name}");
            }
        }
    }
}
