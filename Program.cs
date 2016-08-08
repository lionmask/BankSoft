using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankSoft
{
    class Program
    {
        static void Main(string[] args)
        {
            //Hardcoded user name
            string userFirstName = "John";
            string userLastName = "Dole";
            char transactionChar = '\b';


            //instantiate accounts
            Checking_Account checking1 = new Checking_Account(userFirstName, userLastName, 101);
            Reserve_Account reserve1 = new Reserve_Account(userFirstName, userLastName, 102);
            Savings_Account savings1 = new Savings_Account(userFirstName, userLastName, 103);

            //instantiate the streamwriter for transactions reports
            StreamWriter checkingTransactions = new StreamWriter(Path.GetFullPath("Checking_Transactions.txt"));
            StreamWriter reserveTransactions = new StreamWriter(Path.GetFullPath("Reserve_Transactions.txt"));
            StreamWriter savingsTransactions = new StreamWriter(Path.GetFullPath("Savings_Transactions.txt"));

            //Main while loop
            while (true)
            {
                Bank_Account.SpecialMessage = "Welcome to your BankSoft v1.0 accounts " +userFirstName+ " " +userLastName+ "!\nPlease select a menu option by entering the corresponding number.";
                Console.Clear();
                showTitle();
                Bank_Account.showSpecialMessage();
                Bank_Account.showMenu();
                string input = getUserInput();

                //User wants to see their account information
                if (input == "1")
                {
                    Console.Clear();
                    showTitle();
                    Bank_Account.SpecialMessage = "Here is your information Mr." + userLastName;
                    Bank_Account.showSpecialMessage();
                    Console.WriteLine("Customer: " + userFirstName + " " + userLastName + "\n");
                    Console.WriteLine("Accounts:\n");
                    checking1.showAccountInfo();
                    reserve1.showAccountInfo();
                    savings1.showAccountInfo();
                    Console.WriteLine("Press any key to return to main menu.");
                    Console.ReadKey();
                }

                //User wants to see balances
                else if (input == "2")
                {
                    Console.Clear();
                    showTitle();
                    Bank_Account.SpecialMessage = "Here are your account balances Mr." + userLastName;
                    Bank_Account.showSpecialMessage();
                    Console.Write("Checking: "); checking1.ViewBalance();
                    Console.Write("Reserve: "); reserve1.ViewBalance();
                    Console.Write("Savings: "); savings1.ViewBalance();
                    Console.WriteLine("\nPress any key to return to main menu.");
                    Console.ReadKey();
                }

                //User wants to make a withdrawl or deposit -- Handle together to reduce code
                string transactionType = "";
                while (input == "3" || input == "4")
                {
                    //change vars based on selection
                    if (input == "3") //Deposit
                    {
                        Bank_Account.SpecialMessage = "Ammount you wish to deposit:";
                        transactionType = "deposit";
                        transactionChar = '+';
                        //bool isDeposit remains true
                    }
                    else if (input == "4") //Withdrawl
                    {
                        Bank_Account.SpecialMessage = "Ammount you wish to withdraw:";
                        transactionType = "withdrawl";
                        transactionChar = '\b';
                    }

                    //Ask for dollar ammount for user to transaction
                    while (true)
                    {
                        showTitle();
                        Bank_Account.showSpecialMessage();
                        string input3 = getUserInput();

                        //remove dollar signs using regex
                        Regex rgx = new Regex(@"\$");
                        input3 = rgx.Replace(input3, "");
                        //try to parse the input to make sure it's a decimal and if not ask for a new input
                        decimal dollarAmmount;
                        bool isDecimal = decimal.TryParse(input3, out dollarAmmount);
                        if (dollarAmmount < 0) //ask for a positive number if the user input a negative
                        {
                            Console.WriteLine("Please enter a positive number, even if you are making a withdrawl.");
                            Console.ReadKey();
                        }
                        else if (!isDecimal)//to do if not a decimal
                        {
                            Console.WriteLine("Please enter a dollar amount.");
                            Console.ReadKey();
                        }
                        else if (isDecimal) //if it is a non-negative decimal, run the transaction
                        {
                            while (true) //loop to get transaction account choice
                            {
                                if (input == "4") { dollarAmmount *= -1; } // change dollarAmmount to negative if withdrawl
                                showTitle();
                                Bank_Account.SpecialMessage = "Please choose an account for your transaction:";
                                Bank_Account.showSpecialMessage();
                                showAccountsMenu();
                                string input2 = getUserInput();
                                if (input2 == "1") //checking
                                {

                                    //This is a lot of repeated code because I could not figure out how to refer to a
                                    //object (in this case a specific type of account) in a variable
                                    //Also did not have time to handle negative balances
                                    showTitle();
                                    //add or subtract the amount from the account
                                    checking1.Balance += dollarAmmount;
                                    //write the transaction information in the established text file
                                    checkingTransactions.WriteLine( userLastName + ", " + userFirstName + "on " + DateTime.Now + ": " + transactionType + 
                                        " of " + transactionType + " of " + transactionChar +  dollarAmmount.ToString("C2") + "from Checking Account " + 
                                        checking1.AccountNumber + "\tNew Balance: " + checking1.Balance);
                                    //use Math.Abs to always display the dollar amount positive to the user
                                    Bank_Account.SpecialMessage = "You made a " + transactionType + " of " + Math.Abs(dollarAmmount).ToString("C2") + "\n";
                                    Bank_Account.showSpecialMessage();
                                    Console.WriteLine("Press any key to return to the main menu.");
                                    Console.ReadKey();
                                    break;
                                }
                                else if (input2 == "2") //reserve uses same code routine as above
                                {
                                    showTitle();
                                    reserve1.Balance += dollarAmmount;
                                    reserveTransactions.WriteLine(userLastName + ", " + userFirstName + "on " + DateTime.Now + ": " + transactionType + 
                                        " of " + transactionType + " of " + transactionChar + dollarAmmount.ToString("C2") + "from Reserve Account " + 
                                        reserve1.AccountNumber + "\tNew Balance: " + reserve1.Balance);
                                    Bank_Account.SpecialMessage = "You made a " + transactionType + " of " + Math.Abs(dollarAmmount).ToString("C2") + "\n";
                                    Bank_Account.showSpecialMessage();
                                    Console.WriteLine("Press any key to return to the main menu.");
                                    Console.ReadKey();
                                    break;
                                }
                                else if (input2 == "3") //savings
                                {
                                    showTitle();
                                    savings1.Balance += dollarAmmount;
                                    savingsTransactions.WriteLine(userLastName + ", " + userFirstName + "on " + DateTime.Now + ": " + transactionType + 
                                        " of " + transactionChar + dollarAmmount.ToString("C2") + "from Savings Account " + 
                                        savings1.AccountNumber + "\tNew Balance: " + savings1.Balance);
                                    //use Math.Abs to always display the dollar amount positive to the user
                                    Bank_Account.SpecialMessage = "You made a " + transactionType + " of " + Math.Abs(dollarAmmount).ToString("C2") + "\n";
                                    Bank_Account.showSpecialMessage();
                                    Console.WriteLine("Press any key to return to the main menu.");
                                    Console.ReadKey();
                                    break;
                                }
                                Console.WriteLine("Please select a number from the menu");
                            }
                            break;
                        }
                        break;
                    }
                    break;
                }
                if (input == "5")
                {
                    checkingTransactions.Close();
                    reserveTransactions.Close();
                    savingsTransactions.Close();
                    Console.WriteLine("Thank you for banking with BankSoft v1.0");
                    System.Threading.Thread.Sleep(2000);

                    Environment.Exit(0);
                }
                else { Console.WriteLine("Please select a number from the menu"); }
  

            } //end main while looop
            
        }// end main args
    


        //static methods to support user interface

        public static string getUserInput()
        {
            Console.Write("\n> ");
            string input = Console.ReadLine();
            Console.Clear();
            input = input.ToUpper(); //note: will ignore numbers and can parse in main args
            //handle "quit"
            return input;
        }
        public static void showTitle()
        {
            Console.WriteLine("-----------------------------------BankSoft v1.0--------------------------------\n\n");
        }
        public static void showAccountsMenu()
        {
            Console.WriteLine("1 - Checking\n2 - Reserve\n3 - Savings\n");
        }

    }// end class program
}//end namespace
