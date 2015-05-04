using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework; //test classes need to have the using statement

///     REDDIT DAILY PROGRAMMER SOLUTION TEMPLATE 
///                             http://www.reddit.com/r/dailyprogrammer
///     Your Name: Colton Miller
///     Challenge Name: Remembering your lines
///     Challenge #: 204
///     Challenge URL: http://www.reddit.com/r/dailyprogrammer/comments/2xoxum/20150302_challenge_204_easy_remembering_your_lines/ 
///     Brief Description of Challenge:
///     insert a part of a line from macbeth and this app will write out the line
/// 
///
///     What was difficult about this challenge?
///     finding a way to use the Stream reader and sort the dialog
///
///     
///
///     What was easier than expected about this challenge?
///     The actual referencing of the list
///
///
///
///     BE SURE TO CREATE AT LEAST TWO TESTS FOR YOUR CODE IN THE TEST CLASS
///     One test for a valid entry, one test for an invalid entry.

namespace DailyProgrammer_Template
{
    class Program
    {
        //make a string to hold the lines until a blank
        public static List<string> tempString = new List<string> { };
        public static List<string> listMacBeth = new List<string> { };
        public static string userInput = string.Empty;
        public static bool running = true;
        public static List<string> lines = new List<string> { };
        static void Main(string[] args)
        {

            while (running == true)
            {
                Console.WriteLine("Type any part of a line from MacBeth");
                GetMacBethText();
                userInput = Console.ReadLine();
                LineRemember(userInput);
                Console.WriteLine("Press any key to clear and find another line or escape to end");
                Console.ReadKey();
                Console.Clear();
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    running = false;
                }
            }
        }
        /// <summary>
        /// reads MacBeth.txt and sorts by dialog
        /// </summary>
        public static void GetMacBethText()
        {
            using (StreamReader reader = new StreamReader("MacBeth.txt"))
            {
                // Loop through the rest of the lines
                while (!reader.EndOfStream)
                {
                    //adds to a temp list of strings until the line is null or white space then adds to the listMacBeth and clears the temp list
                    if (string.IsNullOrWhiteSpace(reader.ReadLine()))
                    {
                        string line = string.Join("\n", tempString);
                        listMacBeth.Add(line);
                        tempString.Clear();
                    }
                    else
                    {
                        tempString.Add(reader.ReadLine());
                    }
                }
            }
        }
        /// <summary>
        /// writes out all instances of the user's input
        /// </summary>
        /// <param name="userStringInput"></param>
        public static bool LineRemember(string userStringInput)
        {
            lines = Program.listMacBeth.Where(x => x.ToLower().Contains(userStringInput.ToLower())).ToList();
            if (lines.Count > 0)
            {
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
                return true;
            }
            return false;
        }
    }


#region " TEST CLASS "

    //We need to use a Data Annotation [ ] to declare that this class is a Test class
    [TestFixture]
    class Test
    {
        //Test classes are declared with a return type of void.  Test classes also need a data annotation to mark them as a Test function
        [Test]
        public void MyValidTest()
        {
            Program.GetMacBethText();
            //inside of the test, we can declare any variables that we'll need to test.  Typically, we will reference a function in your main program to test.
            bool result = Program.LineRemember("eye of newt");  // this function should return 15 if it is working correctly
            //now we test for the result.
            Assert.IsTrue(result , "this should display the dialog of the witches");
            // The format is:
            // Assert.IsTrue(some boolean condition, "failure message");
        }

        [Test]
        public void MyInvalidTest()
        {
            Program.GetMacBethText();
            bool result = Program.LineRemember(" POOPY DOODLE");
            Assert.IsFalse(result, "there is no poopy doodle");
        }
    }
#endregion
}
