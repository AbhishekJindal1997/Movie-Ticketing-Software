using System;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace MovieTheatre
{
    public class Program
    {
        static Program Home;
        
        public static string[] MovieNames = new string[30];
        public static string[] MovieRatings = new string[30];
        public static int number_of_movies;
        


        //Start of the program
        static void Main(string[] args)
        {
            Home = new Program();
            Home.ReturnHome();
        }

        // Override the Exception.Message property.
        public class NumberException : ApplicationException
        {
            private string MessageDetails = String.Empty;

            public NumberException() { }
            public NumberException(string message)
            {
                MessageDetails = message;
            }
            public override string Message => $"\nAge value entered is incorrect.!!\nKindly enter a numeric value between 1 to 120: {MessageDetails}";

        }
        //Redirects user to the Start page whenever user enters b or B
        public void GoToHomeScreen(string entryValue)
        {
        
            if (entryValue.ToLower() == "b")
            {
                Console.Clear();
                Home.ReturnHome();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("You have entered an invalid value. Please enter b or B!");
                Console.ResetColor();
                
            }
        }

        //Fetches inputs from the admin/guest
        public string FetchInput(string label)
        {
            if ( label != "" ) {
                Console.Write(label);
            }
            string input = (Console.ReadLine());
            return input;
        } 

        //Return to the main screen
        public void ReturnHome()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t\t\t\t\t******************************************");
            Console.WriteLine("\t\t\t\t\t****** Welcome To MoviePlex Theatre ******");
            Console.WriteLine("\t\t\t\t\t******************************************");
            Console.ResetColor();
            Console.WriteLine("\nPlease Select From The Following Options.\n");
            Console.WriteLine("1: Administrator");
            Console.WriteLine("2: Guests");
        Start:
            string user_enter = FetchInput("\nSelection : ");
        
            bool isValidChoice = IsValidInput(user_enter);

            if (isValidChoice) {
                if (user_enter == "1") {
                    Home.Admin();
                }
                else {
                    Home.Guest();
                }
            }
            else {
              
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nYou have entered an incorrect value!!!\nKindly select either 1 or 2.\n");
                Console.ResetColor();
                goto Start;                
            }
        }

        //Checks input from User
        public bool IsValidInput(string choice)
        {
            if (choice == "1" || choice == "2")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Login into Administrator screen
        public void Admin() {

            Console.Clear();
            string passwordInput = FetchInput("\nPlease Enter the Admin Password : ");
            bool isAdmin = CheckPassword(passwordInput);
                        
            if ( isAdmin == true ) {
                Home.LoginAdmin();
            } else {
                Home.RetryPassword();
            }
        }

        //Checks whether Admin password is correct or not
        public bool CheckPassword(string password)
        {
            if (password == "qwerty") {   

               return true;

            } else {

                return false;
            }
        }

        //Login to Admin screen if password is correct        
        public void LoginAdmin()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t\t\t\t\t*******************************************");
            Console.WriteLine("\t\t\t\t\t***** Welcome MoviePLex Administrator *****");
            Console.WriteLine("\t\t\t\t\t*******************************************");
                      
            Home.MovieEntry();
        }

        //Retry if Admin password is incorrect
        public void RetryPassword()
        {            
            int Count = 5;

            while (Count > 0 && Count < 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("\nIncorrect Password!!!");
                Console.WriteLine("You Have {0} attempts to enter the correct password.", Count);

                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("\nEnter B / b to go back to previous screen.");

                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("\nPlease enter the Admin Passowrd : ");

                string retry = Console.ReadLine();
                Count--;

                if (retry == "qwerty")
                {
                    Console.Clear();
                    Home.LoginAdmin();
                    Count = 6;

                } else if (retry.ToLower() == "b") {

                    Home.GoToHomeScreen(retry);
                }
            }
            Console.Clear();
            Home.ReturnHome();
        }


        //Stores movie details into MovieNames Array entered by the Administrator
        public void MovieEntry()
        {
        Start:
            Console.ResetColor();          
            Console.Write("\nHow Many Movies Are Playing today : ");

            //Checking whether the input is an integer value. Loop will continue till the user enter an integer value
            while (!int.TryParse(Console.ReadLine(), out number_of_movies)) {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou have entered a non-integer value. Please enter again!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nHow Many Movies Are Playing today : ");
            }

            try
            {            
                if (number_of_movies >= 1 & number_of_movies <= 10)
                {
                    
                    // Accepting value from admin 
                    for (int i = 0; i < number_of_movies; i++)
                    {
                        int parsedValue;
                        Console.Write("\nEnter the Movie Name : ");
                        //Storing value in an array
                        MovieNames[i] = Console.ReadLine();
                        Start2:
                        Rate:
                        string ratingValue = FetchInput("Enter Age Limit or Movie Rating for the Movie : ");
                        //Storing value in an array
                        if (ratingValue.ToUpper() == "PG-13" || ratingValue.ToUpper() == "R" || ratingValue.ToUpper() == "PG" || ratingValue.ToUpper() == "G" || ratingValue.ToUpper() == "NC-17"  )
                        {
                            MovieRatings[i] = ratingValue.ToUpper();
                        }
                        else if (int.TryParse(ratingValue, out parsedValue))
                        {
                            if(parsedValue > 120 || parsedValue < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\nRating should be between 1 to 120.\n");
                                Console.ResetColor();
                                goto Rate;
                            }
                            else
                            {
                                MovieRatings[i] = ratingValue;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\nThis rating is not valid.\n");
                            Console.ResetColor();
                            goto Start2;    
                        }  
                    }
                    Home.Display_Movie_Entries();
                    Home.Action_After_Entry();
                }else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nKindly Enter Between 1-10");
                    goto Start;
                }
            }

            catch (Exception)
            {
                NumberException message = new NumberException();
                Console.WriteLine(message.Message);
                Home.Display_Movie_Entries();
            }
        }
            

        //Display Movie List and details entered by the Administrator on the console
        public void Display_Movie_Entries()
        {
            int s_no = 0;
            //Printing the value on console
            for (int i = 0; i < number_of_movies; i++)
            {
                s_no++;
                Console.WriteLine();
                Console.WriteLine(s_no + ". " + MovieNames[i] + "  {" + MovieRatings[i] + "}");
            }

        }

        //Action to take after entering movie details
        public void Action_After_Entry()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nAre You Satisfied With your Entry?");

        Start:
            Console.ResetColor();        
            String actionInput = FetchInput("\nEnter Y/y for Yes | N/n for No | B/b to go back to previous screen : ");
            bool IsValidAction = CheckAction(actionInput);

            if (IsValidAction)
            {
                if(actionInput == "n") {
                    Console.Clear();
                    Home.LoginAdmin();
                }
                else if(actionInput == "y") {
                    Console.Clear();
                    Home.ReturnHome();
                }
                else {
                    Home.ReturnHome();
                }
            }
            else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nYou have entered an incorrect value!!!\nKindly enter y/Y or n/N or b/B\n");
                Console.ForegroundColor = ConsoleColor.White;
                goto Start;
            }
        }

        //Checking the Action entry
        public bool CheckAction(string action)
        {
            if (action.ToLower() == "y" || action.ToLower() == "n" || action.ToLower() == "b") {
                return true;
            }
            else {
                return false;
            }
        }

        //Displays the Guest screen
        public void Guest()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\t\t\t\t\t*************************************");
            Console.WriteLine("\t\t\t\t\t****** Welcome MoviePlex Guest ******");
            Console.WriteLine("\t\t\t\t\t*************************************\n");
            Console.ResetColor();

            Console.WriteLine("There is/are {0} movie/s playing today.\n", number_of_movies);
            Home.Display_Movie_Entries();

            if (number_of_movies == 0) {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("There are no movies playing today!! Kindly check with admin or staff.\n");
                Console.ResetColor();
            }
            else {

                Home.MovieSelection();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            string inputSel = FetchInput("\nEnter b/B to go back to the Start Page : ");
            Console.ResetColor();

            Home.GoToHomeScreen(inputSel);
        }

        //Guest Input for selecting a movie
        public void MovieSelection()
        {
        Start:
            try
            {
                int selMovie;
                int userAge;

                Console.Write("\nWhich Movie Would You Like to Watch : ");
                selMovie = Int32.Parse(Console.ReadLine());
                
                if (selMovie <= number_of_movies & selMovie > 0)
                {
                    Console.Write("\nEnter Your Age for verification : ");
                    userAge = Convert.ToInt32(Console.ReadLine());

                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("\nYou have selected movie {0} with rating {1} and " + "your age is {2} \n", MovieNames[selMovie - 1], MovieRatings[selMovie - 1], userAge);
                    Console.ResetColor();

                    bool isEligibleUser = Home.Validation(selMovie - 1, userAge);
                    ResultMessage(isEligibleUser);

                }
                else if (selMovie > number_of_movies)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease Select From The Given List");
                    Console.ResetColor();
                    goto Start;
                }

                else if (selMovie < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nKindly enter a positive value from The Given List");
                    Console.ResetColor();
                    goto Start;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Entry. Please enter integer values only.");
                Console.ResetColor();
                goto Start;
            }            
        }
        
        //Checks whether guest is entering the age as an ineteger and not as a string
        public bool IsNum(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]) == false) {
                    return false;
                }
            }
            return true;
        }

        //Checks whether guest is eligible to watch the movie or not
        public bool Validation(int selectedMovieIndex, int age)

 
        {

            if ((MovieRatings[selectedMovieIndex] == "PG-13") & age >= 13) {
                return true;

            } else if (MovieRatings[selectedMovieIndex] == "PG-13" & age < 13) {
                return false;

            } else if ((MovieRatings[selectedMovieIndex]) == "PG" & age >= 10) {

                return true;
            } else if ((MovieRatings[selectedMovieIndex]) == "PG" & age < 10) {

                return false;
            } else if ((MovieRatings[selectedMovieIndex]) == "R" & age >= 15) {

                return true;
            } else if ((MovieRatings[selectedMovieIndex]) == "R" & age < 15) {

                return false;
            } else if ((MovieRatings[selectedMovieIndex]) == "NC-17" & age >= 17) {

                return true;
            } else if ((MovieRatings[selectedMovieIndex]) == "NC-17" & age < 17) {

                return false;
            } else if ((MovieRatings[selectedMovieIndex]) == "G" && age > 0) {

                return true;
            } else if (IsNum(MovieRatings[selectedMovieIndex]) && age >= Int32.Parse(MovieRatings[selectedMovieIndex])) {

                return true;
            } else if ((IsNum(MovieRatings[selectedMovieIndex]) && age < Int32.Parse(MovieRatings[selectedMovieIndex]))) {

                return false;
            } else
            {
                return false;
            }
        }

        //Checking what message to print if guest is eligible to watch movie or not
        public void ResultMessage(bool isValid)
        {
            if (isValid)
            {
                Home.EnjoyMsg();
                PostMovieMessage();
               
            }
            else
            {
                Home.GrooveMsg();
            }
        }

        //Displays message if guest is eligible to watch the movie
        public void EnjoyMsg()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nYou are an adult to watch this movie!!!");
            Console.WriteLine("\n*****Enjoy your movie*****\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Displays message if guest is not eligible to watch the movie
        public void GrooveMsg()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nSorry!!!You are not eligible to watch this movie!!!\nKindly Select another Movie.\n");
            Console.ForegroundColor = ConsoleColor.White;
            Home.MovieSelection();
        }

        //Guest making selection to go back to Start Page or the Guest Main Page

 
        public void PostMovieMessage()
        {
            Console.WriteLine("Press M to go back to Guest Main Menu.");
            Console.Write("Press S to go back to Start Page: ");

            string mainScreen1 = Console.ReadLine();
            Home.PostMovieSelection(mainScreen1);
             
           
        }

        public void PostMovieSelection(string mainScreen)
        {
            
            if (mainScreen.ToLower() == "s")
            {
                Console.Clear();
                Home.ReturnHome();
               
            }

            else if (mainScreen.ToLower() == "m")
            {
                Console.Clear();
                Home.Guest();
            }

        }
    }
}