namespace AlkemyChallenge.Model.ViewModel
{
    public class SendEmailViewModel
    {
        private string _emailSender = "testalkemy2021@gmail.com";



        public string From { get { return _emailSender; } }
        public string Subject { get { return "Welcome to Alkemy"; } }
        public string To { get; set; }
        public string PlainTextContent { get { return "Welcome " + To + " , enjoy our services. Start " +
                    " using this link: "+ "https://localhost:44384/api/auth/login"; } }
        public string htmlContent { get { return "<h1><strong> " + PlainTextContent + " </strong></h1>"; } }
    }
}
