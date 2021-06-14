using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace MovieTheatre.Test
{
    [TestFixture]
    public class Class1
    {
        MovieTheatre.Program pass = new MovieTheatre.Program();

        [Test]
        public void ValidUserInputTest()
        {
            string choice1 = "1";
            string choice2 = "2";
            Assert.AreEqual(true, pass.IsValidInput(choice1));
            Assert.AreEqual(true, pass.IsValidInput(choice2));
        }

        [Test]
        public void InValidUserInputTest()
        {
            string choice1 = "a";
            string choice2 = "1a@";
            Assert.AreEqual(false, pass.IsValidInput(choice1));
            Assert.AreEqual(false, pass.IsValidInput(choice2));
        }

        [Test] 
        public void CheckCorrectPasswordTest()
        {
            string password = "qwerty";
            Assert.AreEqual(true, pass.CheckPassword(password));
        }

        [Test]
        public void CheckIncorrectPasswordTest()
        {
            string password = "gsdfhjsfh";
            Assert.AreEqual(false, pass.CheckPassword(password));
        }

        [Test]
        public void CheckCorrectActionEntry()
        {
            string action1 = "y";
            string action2 = "N";
            string action3 = "b";
            Assert.AreEqual(true, pass.CheckAction(action1));
            Assert.AreEqual(true, pass.CheckAction(action2));
            Assert.AreEqual(true, pass.CheckAction(action3));
        }


        [Test]
        public void CheckInCorrectActionEntry()
        {
            string action1 = "1";
            string action2 = "a";
            string action3 = "@";
            Assert.AreEqual(false, pass.CheckAction(action1));
            Assert.AreEqual(false, pass.CheckAction(action2));
            Assert.AreEqual(false, pass.CheckAction(action3));
        }

        [Test]
        public void ValidatingRatingAndAgeTest()
        {
            //Mock Data
            MovieTheatre.Program.MovieRatings[0] = "PG";
            MovieTheatre.Program.MovieRatings[1] = "G";
            MovieTheatre.Program.MovieRatings[2] = "R";
            MovieTheatre.Program.MovieRatings[3] = "NC-17";
            MovieTheatre.Program.MovieRatings[4] = "PG-13";
            MovieTheatre.Program.MovieRatings[5] = "23";
            MovieTheatre.Program.MovieRatings[6] = "120";
            MovieTheatre.Program.MovieRatings[7] = "%";
            MovieTheatre.Program.MovieRatings[8] = "M";
            MovieTheatre.Program.MovieRatings[9] = "-5";

            int SelectedMovie1 = 0;
            int age1 = 20;
            Assert.AreEqual(true, pass.Validation(SelectedMovie1, age1));

            int SelectedMovie2 = 1;
            int age2 = 30;
            Assert.AreEqual(true, pass.Validation(SelectedMovie2, age2));

            int SelectedMovie3 = 2;
            int age3 = 12;
            Assert.AreEqual(false, pass.Validation(SelectedMovie3, age3));

            int SelectedMovie4 = 3;
            int age4 = 20;
            Assert.AreEqual(true, pass.Validation(SelectedMovie4, age4));

            int SelectedMovie5 = 4;
            int age5 = 6;
            Assert.AreEqual(false, pass.Validation(SelectedMovie5, age5));

            int SelectedMovie6 = 5;
            int age6 = 20;
            Assert.AreEqual(false, pass.Validation(SelectedMovie6, age6));

            int SelectedMovie7 = 6;
            int age7 = 120;
            Assert.AreEqual(true, pass.Validation(SelectedMovie7, age7));

            int SelectedMovie8 = 7;
            int age8 = 16;
            Assert.AreEqual(false, pass.Validation(SelectedMovie8, age8));

            int SelectedMovie9 = 8;
            int age9 = 40;
            Assert.AreEqual(false, pass.Validation(SelectedMovie9, age9));

            int SelectedMovie10 = 9;
            int age10 = 120;
            Assert.AreEqual(false, pass.Validation(SelectedMovie10, age10));

        }
    }
}
 