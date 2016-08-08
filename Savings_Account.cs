using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoft
{
    public class Savings_Account : Bank_Account
    {
        public string WelcomeMessage { get; set; }

        public Savings_Account(string UserFirstName, string UserLastName, int AccountNumber):
            base(UserFirstName, UserLastName)
        {
            this.InterestRate = 0.005;
            this.Type = "Savings";
            this.AccountNumber = AccountNumber;
        }
    }
}
