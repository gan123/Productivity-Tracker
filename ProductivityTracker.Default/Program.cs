using System;
using Raven.Client.Document;

namespace ProductivityTracker.Default
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadDefaultUser();
            LoadIndustries();
            LoadStatuses();
            Console.ReadLine();
        }

        static void LoadDefaultUser()
        {
            var store = new DocumentStore { Url = "http://localhost:8080" };
            store.Initialize();

            Console.WriteLine("Start loading user");
            using (var session = store.OpenSession())
            {
                var karthik = new Domain.Model.Recruiter
                                  {
                                      FullName = "Karthik", 
                                      Contact = "454654",
                                      DateOfJoining = new DateTime(2007, 7, 15),
                                      Designation = "Test",
                                      Email = "test@gmail.com",
                                      IsAdmin = true,
                                      Login = "karthik",
                                      Password = "password"
                                  };
                session.Store(karthik);

                session.SaveChanges();
            }
            Console.WriteLine("End loading industries");
        }

        static void LoadIndustries()
        {
            var store = new DocumentStore {Url = "http://localhost:8080"};
            store.Initialize();

            Console.WriteLine("Start loading industries");
            using(var session = store.OpenSession())
            {
                var ad = new Domain.Model.Industry {Name = "Advertising"};
                var it = new Domain.Model.Industry { Name = "IT Service" };
                var sw = new Domain.Model.Industry { Name = "Software" };
                var ind = new Domain.Model.Industry { Name = "Industrial" };
                var hc = new Domain.Model.Industry { Name = "HealthCare" };
                var ins = new Domain.Model.Industry { Name = "Insurance" };

                session.Store(ad);
                session.Store(it);
                session.Store(sw);
                session.Store(ind);
                session.Store(hc);
                session.Store(ins);

                session.SaveChanges();
            }
            Console.WriteLine("End loading industries");
        }

        static void LoadStatuses()
        {
            var store = new DocumentStore { Url = "http://localhost:8080" };
            store.Initialize();

            Console.WriteLine("Start loading statuses");
            using (var session = store.OpenSession())
            {
                var pending = new Domain.Model.Status {Name = "Pending Feedback"};
                var shortListed = new Domain.Model.Status { Name = "Shortlisted" };
                var rejected = new Domain.Model.Status {Name = "Rejected"};
                var intrv = new Domain.Model.Status { Name = "Interview" };
                var intrv1 = new Domain.Model.Status { Name = "Interview 1" };
                var intrv2 = new Domain.Model.Status { Name = "Interview 2" };

                session.Store(pending);
                session.Store(shortListed);
                session.Store(rejected);
                session.Store(intrv);
                session.Store(intrv1);
                session.Store(intrv2);

                session.SaveChanges();
            }
            Console.WriteLine("End loading statuses");
        }
    }
}
