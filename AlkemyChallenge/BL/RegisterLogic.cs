using AlkemyChallenge.Model;
using AlkemyChallenge.Model.ViewModel;
using System;
using System.Linq;

namespace AlkemyChallenge.BL
{
    public class RegisterLogic
    {
        private readonly SendEmailViewModel sendVM = new SendEmailViewModel();

        public RegisterUser Register(RegisterUser model, DbContextModel context)
        {
            model.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            sendVM.To = model.EmailAdress;

            Helper.SendGrid.Main2( sendVM );
            context.Add( model );
            context.SaveChanges();

            return model;
        } 
    }
}
