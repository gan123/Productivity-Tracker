using System;
using System.Threading;
using Raven.Client.Document;

namespace ProductivityTracker.Default
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadDefaultUser();
            LoadStatuses();
            Thread.Sleep(3000);
        }

        static void LoadDefaultUser()
        {
            var store = new DocumentStore { Url = "http://scan-2ffd9e1675:8080" };
            store.Initialize();

            Console.WriteLine("Start loading user");
            using (var session = store.OpenSession())
            {
                var deepika = new Domain.Model.Recruiter
                                  {
                                      FullName = "Deepika Ganesh", 
                                      Contact = "+919967659294",
                                      DateOfJoining = new DateTime(2011, 10, 5),
                                      Designation = "Recruiter",
                                      Email = "deeps_1984@yahoo.com",
                                      IsAdmin = true,
                                      Login = "deepika",
                                      Password = "password"
                                  };
                session.Store(deepika);

                session.SaveChanges();
            }
            Console.WriteLine("End loading user");
        }

        static void LoadStatuses()
        {
            var store = new DocumentStore { Url = "http://localhost:8080" };
            store.Initialize();

            Console.WriteLine("Start loading statuses");
            using (var session = store.OpenSession())
            {
                var pending = new Domain.Model.Status {Name = "Feedback pending"};
                var shortListed = new Domain.Model.Status { Name = "Shortlisted" };
                var rejected = new Domain.Model.Status {Name = "Rejected"};
                var intrv = new Domain.Model.Status { Name = "Interview shortlist" };
                var intrReject = new Domain.Model.Status { Name = "Interview Reject" };
                var intrvFeedbackPending = new Domain.Model.Status { Name = "Interview feedback pending" };
                var closure = new Domain.Model.Status { Name = "Prospective closure" };
                var selected = new Domain.Model.Status { Name = "Selected" };

                session.Store(pending);
                session.Store(shortListed);
                session.Store(rejected);
                session.Store(intrv);
                session.Store(intrReject);
                session.Store(intrvFeedbackPending);
                session.Store(closure);
                session.Store(selected);

                session.SaveChanges();
            }
            Console.WriteLine("End loading statuses");
        }
    }
}
