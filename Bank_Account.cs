using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoft
{
    public abstract class Bank_Account
    {
        //Fields/Properties
        public string CustomerFirstName { get; set; }
        public static string SpecialMessage { get; set; } = "";
        public string CustomerLastName { get; set; }
        public double AccountNumber { get; set; }
        public decimal Balance {get; set; }
        public double InterestRate { get; set; }
        public string Type { get; set; } = "";


        //Constructor
        public Bank_Account(string CustomerFirstName, string CustomerLastName)
        {
            this.CustomerFirstName = CustomerFirstName;
            this.CustomerLastName = CustomerLastName;
            this.Balance = 0;
        }


        //Methods
            //Change Value
        public virtual void changeValue(decimal ammount)
        {
            Balance += ammount;
        }

            //Exit
        public static void Exit()
        {
            Console.WriteLine("Logging off.\n\nThank you for your business. Good bye.");
            System.Threading.Thread.Sleep(2000);
            Environment.Exit(0);
        }

            //View Balance
        public void ViewBalance()
        {
            Console.WriteLine("Your Balance is " + this.Balance.ToString("C2"));
        }

            //Show Menu
        public static void showMenu()
        {
            Console.WriteLine("1 - View Your Account Information\n2 - View Your Account Balances\n3 - Make a Deposit" +
                "\n4 - Make a Withdrawl\n5 - Exit");
        }
        public void showAccountInfo()
        {
            Console.WriteLine("Your " + this.Type + " Account \n  Account #: " + this.AccountNumber +
                "\t  Balance: " + this.Balance.ToString("C2") + "\t  Interest Rate: " + this.InterestRate + "%\n");
        }
        public static void showSpecialMessage()
        {
            if(SpecialMessage == "") { /*do nothing*/ } else
            Console.WriteLine(Bank_Account.SpecialMessage + "\n\n");
        }
    }
}
