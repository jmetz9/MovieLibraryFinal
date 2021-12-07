using System;
using System.IO;
using MovieLibraryDatabase.Context;
using MovieLibraryDatabase.DataModels;

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
                Console.WriteLine("2) Display all movies");
                Console.WriteLine("3) Add a movie to the database");
                Console.WriteLine("4) Add a user to the database");
                Console.WriteLine("5) Change a Movie's title");
                Console.WriteLine("6) Delete a movie from the database");
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
                    using(var db = new MovieContext()){
                        foreach (var m in db.Movies)
                        {
                            Console.WriteLine($"Movie {m.Id}: {m.Title}, {m.ReleaseDate}");
                        }
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Enter the movie title");
                    string input = Console.ReadLine();
                    appMan.AddMovie(input);
                }
                else if (choice == "4")
                {
                    string gender;
                    string occ;
                    Console.WriteLine("Enter the user's age");
                    //tried doing try/catch block but it would just say "The build failed. Fix the build errors and run again." 
                    //without telling me what the errors were so I'm stumped
                    long age = long.Parse(Console.ReadLine());
                    do
                    {
                        Console.WriteLine("Enter the user's gender. (M)ale, (F)emale");
                        gender = Console.ReadLine().ToUpper();

                    } while (gender != "M" && gender != "F");

                    Console.WriteLine("Enter the user's zipcode");
                    string zip = Console.ReadLine();


                    bool exists = false;
                    do
                    {
                        using (var db = new MovieContext())
                        {
                            foreach (var o in db.Occupations)
                            {
                                Console.WriteLine($"{o.Name}");
                            }

                            Console.WriteLine("From the selection above, choose the user's occupation");
                            occ = Console.ReadLine();
                            foreach (var o in db.Occupations)
                            {
                                if (o.Name.ToLower() == occ.ToLower())
                                {
                                    exists = true;
                                }
                            }
                        }
                    }while(!exists);

                    appMan.AddUser(age, gender, zip, occ);
                }
                else if (choice == "5")
                {
                    Console.WriteLine("Enter movie title to change");
                    string input = Console.ReadLine();
                    Console.WriteLine("What would you like to change it to?");
                    string change = Console.ReadLine();

                    appMan.UpdateMovie(input, change);
                }
                else if (choice == "6")
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