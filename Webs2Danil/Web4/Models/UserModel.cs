namespace Web4.Models
{
    public class UserModel
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string birthday { get; set; }

        public string gender { get; set; }

        public string email {get;set;}

        public string password {get;set;}

        public string remember {get;set;}

        public int currentRegisterStage { get; set; }

        public UserModel()
        {
            firstName = "";
            lastName = "";
            birthday = "";
            gender = "";
            email = "";
            password = "";
            remember = "";
            currentRegisterStage = 1;
        }
    }
}
