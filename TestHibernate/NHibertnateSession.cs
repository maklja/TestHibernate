using NHibernate;
using NHibernate.Cfg;

namespace TestHibernate
{
    public class NHibertnateSession
    {
        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            configuration.Configure(@"./Mappings/hibernate.cfg.xml");
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
