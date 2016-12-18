using Prasatec.Cu2Com;
using Prasatec.Cu2Com.Data;
using Prasatec.Raden;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prasatec.Apps.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Connecting to MySql Server...");
            var c = new ConnectionBuilder<Raden.Engines.MysqlEngine>()
                .Server("nctslaws")
                .Database("timetrack")
                .Username("timetrack")
                .Password("timetrack")
                .Build();
            Console.WriteLine("Done");
            Application.SetCompatibleTextRenderingDefault(true);
            Application.EnableVisualStyles();
            
            /*var mgr = new ManageLogsController(c);
            mgr.Show();
            while (mgr.Window.Visible) { Application.DoEvents(); }*/
        }
        static void main5()
        {

            //Form test = new Prasatec.Experience.Windows.BaseWindow();
            Application.Run(new WindowTest());
        }
        static void main4()
        {
            var m = new Prasatec.Cu2Com.Data.User();
            var c = new ConnectionBuilder<Raden.Engines.MysqlEngine>()
                .Server("nctslaws")
                .Database("timetrack")
                .Username("timetrack")
                .Password("timetrack")
                .Build();
            Console.WriteLine("connected");
            //var r = c.Backup();

            Type modelType = typeof(IModel);

            var models = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => modelType.IsAssignableFrom(p) && p != modelType);
            foreach (var model in models)
            {
                Console.WriteLine(model);
            }
        }
        static void main3()
        {
            var asm = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in asm)
            {
                Console.WriteLine(a.FullName);
            }
            Console.WriteLine();
            var asm2 = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            foreach (var a in asm2)
            {
                Console.WriteLine(Assembly.Load(a).FullName);
            }

        }
        static void main2()
        { 
            var m = new Prasatec.Cu2Com.Data.User();
            m.Login = "root";
            m.Email = "root@localhost";
            var b = new ConnectionBuilder<Raden.Engines.MysqlEngine>()
                .Server("nctslaws")
                .Database("timetrack")
                .Username("timetrack")
                .Password("timetrack");
            var c = b.Build();
            var qbuilder = new QueryBuilder<Prasatec.Cu2Com.Data.User>()
                .Where(x => x.ID, 2)
                /*.Where(x => x.ID, 4, QueryLogicalOperators.LogicalOr, QueryComparisonOperators.IsLike, 4, "*")
                .Where<Prasatec.Cu2Com.Data.Role>(x => x.ID, y => y.Role, QueryLogicalOperators.LogicalOr, QueryComparisonOperators.EqualTo, 3)
                .Group(x => x.Role)
                .Having(x => x.ID, 4, QueryLogicalOperators.LogicalOr, QueryComparisonOperators.IsNotNull, 4, "*", QueryColumnActions.Count)
                .Sort(x => x.Name, QuerySortDirections.Descending)
                .SetLimit(100, 13)*/;
            var q = qbuilder.Build();
            var r = c.Retrieve<Prasatec.Cu2Com.Data.User>(q);// m);
            m = r.Records[0];
            c.Update<User>(q, m);
            /*foreach (var p in q.Parameters)
            {
                Console.WriteLine(p);
            }*/
            Console.WriteLine("AffectedRows: {0}", r.AffectedRows);
            Console.WriteLine("InsertId: {0}", r.InsertId);
            Console.WriteLine("CountPages: {0}", r.CountPages);
            Console.WriteLine("CountRecords: {0}", r.CountRecords);
            Console.WriteLine("CountTotal: {0}", r.CountTotal);
            Console.WriteLine("ErrorMessage: {0}", r.ErrorMessage);
            Console.WriteLine("ScalarValue: {0}", r.ScalarValue);
            Console.WriteLine("Successful: {0}", r.Successful);
            if (r.Records != null)
            {
                foreach (var record in r.Records)
                {
                    Console.WriteLine("--> Record ID: {0}", record.ID);
                    Console.WriteLine("--> Record Code: {0}", record.Code);
                    Console.WriteLine("--> Record CreatedAt: {0}", record.CreatedAt);
                    Console.WriteLine("--> Record CreatedBy: {0}", record.CreatedBy);
                    Console.WriteLine("--> Record Email: {0}", record.Email);
                    Console.WriteLine("--> Record Login: {0}", record.Login);
                    Console.WriteLine("--> Record ModifiedAt: {0}", record.ModifiedAt);
                    Console.WriteLine("--> Record ModifiedBy: {0}", record.ModifiedBy);
                    Console.WriteLine("--> Record Name: {0}", record.Name);
                    Console.WriteLine("--> Record Password: {0}", record.Password);
                    Console.WriteLine("--> Record Role: {0}", record.Role);
                }
            }
        }
        static void main1()
        {
            var builder = new QueryBuilder<User>()
                .Column(x => x.Name)
                .Where(x => x.ID, 5);
            var query = builder.Build();

            //var db = new StreamDatabase();

            //db.Count<User>(query);
        }
        static void main0()
        { 
            var test = Prasatec.Plink.Definitions.Retrieve();
            long objects = 0;
            
            foreach (var deployment in test.GetDeployments())
            {
                objects++;
                Console.WriteLine(deployment.Identifier);
                foreach (var module in deployment.GetModules())
                {
                    objects++;
                    Console.WriteLine("--> {0}", module.Identifier);
                    foreach (var method in module?.GetMethods())
                    {
                        objects++;
                        Console.WriteLine("------> {0}", method?.Identifier);
                    }
                    foreach (var property in module?.GetProperties())
                    {
                        objects++;
                        Console.WriteLine("------> {0}", property?.Identifier);
                    }
                    foreach (var field in module?.GetFields())
                    {
                        objects++;
                        Console.WriteLine("------> {0}", field?.Identifier);
                    }
                }
            }
            Console.WriteLine("Total Objects: {0}", objects);
            Console.Read();
            //Prasatec.Raden.
        }
    }
}
