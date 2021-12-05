using Project.Business.Framework;
using Project.Business.Middleware;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business
{
    public class UserManager:BaseManager<User>
    {
        private Crypto crypto;
        public UserManager()
        {
            crypto = new Crypto();
        }
        public bool CheckSecurityAnswer(User entity,string answer)
        {
            if (_validation.ValidateSecurityAnswer(entity, answer))
                return true;
            return false;
        }
        public ServiceResult SetNewIpAdress(User user)
        {
            try
            {
                var entity = _repo.GetById(user.Id);
                entity.LastLoginIP = new Helper().GetIPHelper();
                _repo.Update(entity);
                return new ServiceResult<User>(entity);
            }
            catch (Exception ex)
            {
                return new ServiceResult<User>(ServiceResultCode.Generic, ex.Message, user);
            }
        }
        public override ServiceResult Add(User Entity)
        {
            if (_validation.IfUserAlreadyExists(Entity.Mail,Entity.PhoneNumber))
                return new ServiceResult(ServiceResultCode.AlreadyExists, "Eklemeye Çalıştığınız Mail Adresi zaten mevcut!");

            try
            {
                var encryptedPassword = crypto.Encrypt(Entity.Password);
                Entity.Password = encryptedPassword;
                return base.Add(Entity);
            }
            catch (Exception ex)
            {
                return new ServiceResult(ServiceResultCode.Generic, ex.Message);
            }
            
        }
        public ServiceResult<User> Login(string Mail,string Password)
        {
            var encryptedPassword = crypto.Encrypt(Password);
            var entity = _repo.GetByParameter(x => x.Mail == Mail && x.Password == encryptedPassword);
            if (_validation.IsNullOrEmpty(entity))
                return new ServiceResult<User>(ServiceResultCode.RecordNotFound,"Kullanıcı adı veya şifre hatalı.");
            if (_validation.IsAnotherIp(entity))
                return new ServiceResult<User>(ServiceResultCode.DifferentIPException,"Farklı bir IP Adresiyle giriş yapmayı deniyorsunuz. Lütfen hesabınızı doğrulayın.",entity);
            return new ServiceResult<User>(entity);
        }
        public ServiceResult<User> FirstLogin(int id,string password,string questionAnswer)
        {
            try
            {
                var entity = _repo.GetById(id);
                entity.Password = crypto.Encrypt(password);
                entity.SecurityAnswer = crypto.Encrypt(questionAnswer);
                entity.IsFirstLogin = false;
                entity.LastLoginIP = new Helper().GetIPHelper();
                _repo.Update(entity);
                return Login(entity.Mail,password);
            }
            catch (Exception ex)
            {
                return new ServiceResult<User>(ServiceResultCode.Generic, ex.Message);
            }
            
        }
    }
}
