using System.Linq;
using System.Security;

namespace ProductivityTracker.Services.Services.Authroisation
{
    public class AuthorisationService : ServiceBase, IAuthorisationService
    {
        public Domain.Model.Recruiter GetUserProfile(string login, string password)
        {
            var recruiter = CurrentSession.Query<Domain.Model.Recruiter>()
                .SingleOrDefault(r => r.Login == login && r.Password == password);

            if (recruiter == null)
                throw new SecurityException("Invalid user");
            return recruiter;
        }
    }
}