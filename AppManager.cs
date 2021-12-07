using System;
using System.Collections.Generic;
using MovieLibraryDatabase.Context;
using MovieLibraryDatabase.DataModels;

namespace MovieLibraryDatabase
{
    public class AppManager
    {
        public void Search(string title)
        {
            using (var db = new MovieContext())
            {
                int returnedMovies = 0;
                var movies = db.Movies.Where(x => x.Title.ToLower().Contains(title.ToLower()));
                foreach (var m in movies)
                {
                    Console.WriteLine($"Movie {m.Id}: {m.Title}, {m.ReleaseDate}");
                    returnedMovies++;
                }

                if (returnedMovies == 0)
                {
                    Console.WriteLine("There are no movies with term -" + title + "- in the title");
                }

            }
        }

        public void AddMovie(string title)
        {
            using (var db = new MovieContext())
            {
                DateTime release = DateTime.Now;
                var movie = new Movie
                {
                    Title = title,
                    ReleaseDate = release
                };
                db.Movies.Add(movie);
                db.SaveChanges();

                Console.WriteLine(movie.Title + " has been added");
            }
        }

        public void AddUser(long age, string gender, string zip, string occ){
            using (var db = new MovieContext())
            {
                var user = new User
                {
                    Age = age,
                    Gender = gender,
                    ZipCode = zip,
                    Occupation = db.Occupations.FirstOrDefault(x => x.Name == occ)
                };
                db.Users.Add(user);
                db.SaveChanges();

                Console.WriteLine($"User added successfully: #{user.Id}, Age:{user.Age}, {user.Gender}, Zip:{user.ZipCode}, Occ:{user.Occupation.Name}");
            }
        }

        public void UpdateMovie(string title, string updateTo)
        {
            using (var db = new MovieContext())
            {
                var updateMovie = db.Movies.FirstOrDefault(x => x.Title == title);

                try
                {
                    updateMovie.Title = updateTo;
                    db.Movies.Update(updateMovie);
                    db.SaveChanges();

                    Console.WriteLine("Title " + title + " changed to: " + updateTo);
                }
                catch (System.NullReferenceException)
                {
                    Console.WriteLine("No movies with name: " + title);
                }

            }
        }

        public void DeleteMovie(string title)
        {
            using (var db = new MovieContext())
            {
                var deleteMovie = db.Movies.FirstOrDefault(x => x.Title == title);
                
                try
                {
                    
                    System.Console.WriteLine($"Are you sure you want to delete the following movie?(Y = Yes, Anything Else = No) {deleteMovie.Id}: {deleteMovie.Title}, {deleteMovie.ReleaseDate}");
                    string yesNo = Console.ReadLine();

                    if(yesNo == "y"){
                    Console.WriteLine("Deleting...");
                    db.Movies.Remove(deleteMovie);
                    db.SaveChanges();
                    Console.WriteLine("Title: " + title + " has been deleted :(");
                    }

                }
                catch (System.NullReferenceException)
                {
                    Console.WriteLine("No movies with name: " + title);
                }
                
            }
        }
    }
}