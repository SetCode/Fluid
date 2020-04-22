using Anda.Fluid.Domain.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Anda.Fluid.Domain.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var ctx = new afmdbEntities())
            //{
            //    ctx.tworkpiece.Add(new tworkpiece());
            //    ctx.SaveChanges();
            //}

            //Transaction();

            using (var ctx = new afmdbEntities())
            {
                Query(ctx);
            }

            Console.ReadLine();
        }
        //public static  bool Exits()
        //{     
        //    //return Database.Exists();
        //}
        private static void Query(afmdbEntities ctx)
        {
            var workpieces = from e in ctx.workpiece select e;
            foreach (var w in workpieces)
            {
                Console.WriteLine(w.Id);
                Console.WriteLine(w.Name);
                Console.WriteLine("----------------");
            }
            Console.WriteLine("================");
        }

        private static void Insert(afmdbEntities ctx)
        {
            ctx.workpiece.Add(new workpiece { StartTime = DateTime.Now });
            ctx.SaveChanges();
        }

        public static void Update(afmdbEntities ctx)
        {
            var w = (from e in ctx.workpiece where e.Id == 2 select e).First();
            w.Name = "aaa";
            ctx.SaveChanges();
        }

        public static void Delete(afmdbEntities ctx)
        {
            var w = (from e in ctx.workpiece where e.Id == 2 select e).First();
            ctx.workpiece.Remove(w);
            ctx.SaveChanges();
        }

        public static void Transaction()
        {
            // Create database if not exists
            using (afmdbEntities ctx = new afmdbEntities())
            {
                ctx.Database.CreateIfNotExists();
            }

            using (afmdbEntities ctx = new afmdbEntities())
            {
                // Interception/SQL logging
                ctx.Database.Log = (string message) => { Console.WriteLine(message); };

                using (var transaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        List<workpiece> workpieces = new List<workpiece>();
                        for (int i = 0; i < 5; i++)
                        {
                            workpieces.Add(new workpiece { Name = string.Format("www{0}", i), StartTime = DateTime.Now });
                        }
                        ctx.workpiece.AddRange(workpieces);
                        ctx.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void TransactionScope()
        {
            using (var ctx = new afmdbEntities())
            {
                using (var scope = new TransactionScope())
                {
                    List<workpiece> workpieces = new List<workpiece>();
                    for (int i = 0; i < 5; i++)


                    {
                        workpieces.Add(new workpiece { Name = string.Format("www{0}", i), StartTime = DateTime.Now });
                    }
                    ctx.workpiece.AddRange(workpieces);
                    ctx.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}
