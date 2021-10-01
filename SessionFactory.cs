using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployerForm
{
    public class SessionFactory
    {
        private static volatile ISessionFactory iSessionFactory;
        private static object syncRoot = new Object();

        public static ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null)
                {
                    lock (syncRoot)
                    {
                        if (iSessionFactory == null)
                        {
                            iSessionFactory = BuildSessionFactory();
                        }
                    }
                }
                return iSessionFactory.OpenSession();
            }
        }

        private static ISessionFactory BuildSessionFactory()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];
                return Fluently.Configure()
                     .Database(MsSqlConfiguration.MsSql7
                     .ConnectionString(x => x.Server("DESKTOP-43EPCI7;MultipleActiveResultSets=True")
                                                            .Database("ALNASR_STEEL")
                                                            .Username("sa")
                                                            .Password("123456")
                                                            ))
                     .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                     .ExposeConfiguration(BuildSchema)
                     .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        //Create Session
        private static AutoPersistenceModel CreateMAppings()
        {
            return AutoMap
                .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(testc => testc.Namespace == "EmployerForm.Model");
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            SchemaUpdate su = new SchemaUpdate(config);
            su.ExecuteAsync(true, true);
        }
    }
}
