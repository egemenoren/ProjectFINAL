using Project.Data.Repository;
using Project.Entity;

namespace Project.Business.Framework
{
    public class ValidateEntity<T> : IValidateEntity<T> where T:BaseEntity
    {
        private GenericRepository<T> _repo;
        private Helper helper;
        public ValidateEntity()
        {
            _repo = new GenericRepository<T>();
            helper = new Helper();
           
        }
        public bool IsNullOrEmpty(T Entity)
        {
            
            if (Entity == null)
                return true;
            return false;
        }
        public bool IsAnotherIp(User user)
        {
            GenericRepository<User> userRepo = new GenericRepository<User>();
            var entity = userRepo.GetById(user.Id);
            if(helper.GetIPHelper() != entity.LastLoginIP)
            {
                return true;
            }
            return false;
        }
        public bool IfUserAlreadyExists(string mail,string phoneNumber)
        {
            GenericRepository<User> userRepo = new GenericRepository<User>();
            var result = userRepo.GetByParameter(x => x.Mail == mail || x.PhoneNumber == phoneNumber);
            if (result == null)
                return false;
            return true;
        }
        public bool ValidateSecurityAnswer(User user,string answer)
        {
            GenericRepository<User> userRepo = new GenericRepository<User>();
            var result = userRepo.GetById(user.Id);
            var cryptedAnswer = new Crypto().Encrypt(answer);
            if (result.SecurityAnswer == cryptedAnswer)
                return true;
            return false;
        }
    }
}
