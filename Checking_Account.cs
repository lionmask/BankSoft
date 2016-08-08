using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoft
{
    public class Checking_Account : Bank_Account
    {
        public string WelcomeMessage { get; } = "Your Checking Account\n-----------------\n\n";

        public Checking_Account(string UserFirstName, string UserLastName, int AccountNumber) :
            base(UserFirstName, UserLastName)
        {
            this.InterestRate = 0.001;
            this.Type = "Checking";
            this.AccountNumber = AccountNumber;
        }
    }
}
