using System;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace YinzerHangman
{
    namespace YinzerHangman
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Welcome to Yinzer Hangman, the game where we take everyday words and spell them in Pittsburghese");
                Console.WriteLine();
                Console.WriteLine("This is like traditional hangman, guess the word before you're hanged!");
                Console.WriteLine();

                string[] yinzWords = {
                "shahr",
                "yinz",
                "clahdy",
                "jagoff",
                "dahntahn",
                "grahnd bees",
                "rahndabaht",
                "slippy",
                "gumband",
                "seent",
                "cousint",
                "friggin",
                "crick",
                "arn",
                "worsh",
                "sweeper",
                "spickett",
                "prolly",
                "zak same",
                "lahsy",
                "jagger bush",
                "up air",
                "blesshew",
                "bo da yous"
            };
                int incorrect = 0;
                int correct = 0;
                bool hasLetter = false;
                string playAgain;
                string answer = "";

                do
                {

                    var random = new Random();
                    var word = random.Next(yinzWords.Length);
                    answer = yinzWords[word];
                    string hidden = new string('*', answer.Length);

                    Console.WriteLine($"Your word is {hidden}!");
                    Console.WriteLine($"And it is {answer.Length} letters");
                    Console.WriteLine();

                    while (answer.Length != correct && incorrect < 5)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Guess a letter, or if you know the answer, spell out the whole word.");

                        string guess = Console.ReadLine();


                        if (guess.Length > 1 && guess.Length == answer.Length)
                        {
                            if (guess.ToLower() != answer.ToLower())
                            {
                                Console.WriteLine("I'm sorry, that was an incorrect guess!");
                            }
                            // They guessed the correct word and win the game.
                            else if (correct == answer.Length)
                            {
                                Console.WriteLine($"The answer is {answer}");
                                Console.WriteLine("You win the game!");
                            }
                        }
                        //user only guessed a single letter
                        else if (guess.Length == 1)
                        {
                            //if they guess incorrectly respond and increment the incorrect variable
                            if (!answer.Contains(guess))
                            {
                                Console.WriteLine("The word doesn't contain that letter!");
                                incorrect++;
                                Console.WriteLine();
                                Console.WriteLine($"You have made {incorrect} attempts, {5 - incorrect} attempts remain");
                                Console.WriteLine($"{hidden}");
                            }
                            else
                            {
                                //number of times a letter appears in a word, in case more than once
                                int numberOfTimes = 0;

                                //create a CharArray to hold hidden
                                char[] reveal = hidden.ToCharArray();
                                for (int i = 0; i < answer.Length; i++)
                                {
                                    if (guess[0] == answer[i])
                                    {
                                        hasLetter = true;
                                        correct++;
                                        numberOfTimes++;
                                        reveal[i] = guess[0];
                                        Console.WriteLine();
                                        if (answer.Length != correct && numberOfTimes >= 1)
                                        {
                                            Console.WriteLine("Correct guess!");
                                            Console.WriteLine();
                                            hidden = new string(reveal);
                                            Console.WriteLine($"{hidden}!");
                                        }
                                    }
                                }
                                if (correct == answer.Length)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("You win!");
                                    Console.WriteLine();
                                    Console.WriteLine($"The word is {answer}");
                                    Console.WriteLine();
                                }
                                
                               

                            }
                        }

                    }

                    Console.WriteLine("Would you like to play again?");
                    Console.WriteLine();
                    Console.WriteLine("'Y' to play again, 'N' to exit");
                    Console.WriteLine();
                    playAgain = Console.ReadLine();

                    // User will now select 
                    if (playAgain.ToLower() == "y")
                    {
                        playAgain = "y";
                        correct = 0;
                        incorrect = 0;
                        hasLetter = false;
                        answer = "";
                    }
                    // User chose not to play again, exit the screen.
                    else if (playAgain.ToLower() == "n")
                    {
                        playAgain = "n";
                        Console.WriteLine("Thanks for playing, bye!");
                    }
                    // They made an incorrect choice
                    else
                    {
                        Console.WriteLine("I'm sorry, please enter Y to play again or N to exit");
                        Console.WriteLine();
                        playAgain = Console.ReadLine();

                    }
                } while (playAgain.ToLower() == "y");

            }
        }
    }
}