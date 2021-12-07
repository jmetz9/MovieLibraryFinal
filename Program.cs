using System;
using System.IO;

namespace MovieLibraryDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            AppManager appMan = new AppManager();
            
            string choice;
            do
            {
                Console.WriteLine("1) Search Movie by title.");
                Console.WriteLine("2) Add a movie to the database");
                Console.WriteLine("3) Change a Movie's title");
                Console.WriteLine("4) Delete a movie from the database");
                Console.WriteLine("Enter any other key to exit.");
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    Console.WriteLine("Enter your search term: ");
                    string input = Console.ReadLine();
                    appMan.Search(input);
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Enter the movie title");
                    string input = Console.ReadLine();
                    appMan.AddMovie(input);
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Enter movie title to change");
                    string input = Console.ReadLine();
                    Console.WriteLine("What would you like to change it to?");
                    string change = Console.ReadLine();

                    appMan.UpdateMovie(input, change);
                }
                else if (choice == "4")
                {
                    Console.WriteLine("Enter movie title to delete");
                    string input = Console.ReadLine();

                    appMan.DeleteMovie(input);
                }
                Console.WriteLine("Anything else? Y/N");
                choice = Console.ReadLine();
            } while (choice.ToLower() == "y");
        }

    }
}