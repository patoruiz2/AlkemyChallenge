using AlkemyChallenge.Model;
using AlkemyChallenge.Model.ViewModel;
using System;
using System.Linq;

namespace AlkemyChallenge.BL
{
    public class LoginLogic
    {
        public RegisterUser LogIn( LoginViewModel model, DbContextModel context)
        {
            RegisterUser registerUser = context.RegisterUsers.Where(x => x.UserName == model.UserName)
               .Where(t => t.Token != null)
               .Where(p => p.Password == model.Password)
               .FirstOrDefault();
            registerUser.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return registerUser;
        } 
    }
}
