using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoft
{
    public class Reserve_Account : Bank_Account
    {
        public string WelcomeMessage { get; set; }

        public Reserve_Account(string UserFirstName, string UserLastName, int AccountNumber):
            base(UserFirstName, UserLastName)
        {
            this.InterestRate = 0.002;
            this.Type = "Reserve";
            this.AccountNumber = AccountNumber;
        }
    }
}
