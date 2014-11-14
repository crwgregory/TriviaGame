using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //declare variables
            int questionAnswerIndexNumber;
            Trivia questionAndAnswer;
            string userInput;
            bool tryAgain = true;
            bool anotherQuestion = true;
            bool pickingCategory = true;
            //List<string> category = new List<string>();
            //string category;
            List<Trivia> category = new List<Trivia>();
            
            //The logic for your trivia game happens here
            List<Trivia> AllQuestions = GetTriviaList();

            Console.WriteLine("What category would you like? \n1.Geography 2.Lyrics 3.Entertainment 4.Science");
            userInput = Console.ReadLine();

            while (pickingCategory)
            {
                
                Console.Clear();
                if (userInput.ToLower() == "1")
                {
                    userInput = "geography";
                    pickingCategory = false;
                }
                else if (userInput == "2")
                {
                    userInput = "lyrics";
                    pickingCategory = false;
                }
                else if (userInput == "3")
                {
                    userInput = "entertainment";
                    pickingCategory = false;
                }
                else if (userInput == "4")
                {
                    userInput = "science";
                    pickingCategory = false;
                }
                else
                {
                    Console.WriteLine("Try again");
                    Console.WriteLine("1 2 3 or 4");
                    userInput = Console.ReadLine();

                }

            }
            category.Clear();

            //used a foreach loop insted of a lambda for choosing the category. I'm not sure why the lambda didn't work and the foreach did.
            foreach (var item in AllQuestions)
            {

                if (item.Question.ToLower().StartsWith(userInput.ToLower()))
                {
                    category.Add(item);
                }

            }
            //hint system, picks 30% of letters within answer and prints them out, every time hint is used 30% more letters are printed out.

            //select a random quesion from the all quesitions list
            Random rng = new Random();
            while (anotherQuestion)
            {
                questionAnswerIndexNumber = rng.Next(0, category.Count());

                //get a question and answer from the list and set to a variable
                questionAndAnswer = category[questionAnswerIndexNumber];


                tryAgain = true;
                while (tryAgain)
                {
                    //ask user question
                    Console.WriteLine(questionAndAnswer.Question);
                    Console.WriteLine(printDashes(questionAndAnswer.Answer, false));
                    Console.WriteLine("\nWould you like a hint?\n'Yes'.");
                    bool hint = true;
                    while (hint)
                    {
                        userInput = Console.ReadLine();
                        if (userInput.ToLower() == "yes")
                        {
                            Console.Clear();
                            Console.WriteLine(questionAndAnswer.Question);
                            Console.WriteLine(printDashes(questionAndAnswer.Answer, true));
                            hint = false;
                            userInput = Console.ReadLine();
                        }
                        else
                        {
                            break;
                        }
                    }

                    


                    //check if input is correct

                    if (userInput.ToLower() == questionAndAnswer.Answer.ToLower())
                    {
                        Console.WriteLine("Congrats bro.");
                        tryAgain = false;
                        Console.WriteLine("\nWould you like to play again?\n'Yes' or 'No'");
                        bool asking = true;
                        while (asking)
                        {
                            userInput = Console.ReadLine();
                            if (userInput.ToLower() == "yes")
                            {
                                Console.Clear();
                                anotherQuestion = true;
                                asking = false;
                            }
                            else if (userInput.ToLower() == "no")
                            {
                                anotherQuestion = false;
                                asking = false;
                            }
                            else
                            {
                                Console.WriteLine("Enter a yes or no please");
                            }
                        }
                    }

                    else
                    {
                        bool tryAgainChoose = true;
                        Console.WriteLine("Try again, another question, or see answer? \nEnter '1' or '2' or '3'");
                        userInput = Console.ReadLine();
                        while (tryAgainChoose)
                        {
                            Console.Clear();
                            if (userInput == "1")
                            {
                                tryAgain = true;
                                tryAgainChoose = false;
                            }
                            else if (userInput == "2")
                            {
                                tryAgain = false;
                                tryAgainChoose = false;
                            }
                            else if (userInput == "3")
                            {
                                Console.WriteLine(questionAndAnswer.Answer);
                                Console.WriteLine("Another question? If yes enter '2'");
                                userInput = Console.ReadLine();
                                Console.Clear();
                                if (userInput != "2")
                                {
                                    tryAgainChoose = false;
                                    anotherQuestion = false;
                                    tryAgain = false;
                                    break;
                                }

                            }
                            else
                            {
                                Console.WriteLine("Enter correct selection 1 2 or 3");
                            } 
                        }
                    }

                }
            }
        }


        //This functions gets the full list of trivia questions from the Trivia.txt document
        static List<Trivia> GetTriviaList()
        {
            //Get Contents from the file.  Remove the special char "\r".  Split on each line.  Convert to a list.
            List<string> contents = File.ReadAllText("trivia.txt").Replace("\r", "").Split('\n').ToList();

            //Each item in list "contents" is now one line of the Trivia.txt document.
            
            //make a new list to return all trivia questions
            List<Trivia> returnList = new List<Trivia>();
            
            // TODO: go through each line in contents of the trivia file and make a trivia object.
            //       add it to our return list.
            // Example: Trivia newTrivia = new Trivia("returnList[0]");
            //Return the full list of trivia questions

            foreach (var item in contents)
            {
                //calls constructor to set this item to an answer and question
                returnList.Add(new Trivia(item));
            }

            return returnList;
        }

        static string printDashes(string question, bool wantsHint)
        {
            string outPut = "";
            int i = 0;
            Random rng = new Random();
            int randomI;


                foreach (char item in question)
                {
                    randomI = rng.Next(0, question.Length);
                    if (char.IsLetter(item))
                    {
                        if (randomI % 2 == 0 && wantsHint)
                        {
                            outPut += question[i];
                        }
                        else
                        {
                            outPut += "_ "; 
                        }
                    }
                    else if (char.IsNumber(item))
                    {
                        if (randomI % 2 == 0 && wantsHint)
                        {
                            outPut += question[i];
                        }
                        else
                        {
                            outPut += "_ ";
                        }
                    }
                    else if (char.IsSeparator(item))
                    {
                        outPut += " ";
                    }
                    else
                    {
                        outPut += question[i];
                    }
                    i++;
                } 
            
            return outPut;
        }


    }

    class Trivia
    {
        //TODO: Fill out the Trivia Object

        //The Trivia Object will have 2 properties
        // at a minimum, Question and Answer

        //The Constructor for the Trivia object will
        // accept only 1 string parameter.  You will
        // split the question from the answer in your
        // constructor and assign them to the Question
        // and Answer properties

        private string _question;
        public string Question
        {
            get { return _question; }
            set { _question = value; }
        }

        private string _answer;
        public string Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }


        public Trivia(string questionAndAnswer)
        {

            Question = questionAndAnswer.Split('*').First();
            Answer = questionAndAnswer.Split('*').Last();
        }
    }
}
