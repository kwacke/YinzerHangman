using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
namespace YinzerHangman
{
    namespace YinzerHangman
    {
        internal class Program
        {
            static void Main(string[] args)
            {   
                // Method to call for drawing a hangman as you make incorrect guesses in the game
                static void DrawHangman(int step)
                {
                    Console.WriteLine();
                    
                    switch (step)
                    {   //Case number = incorrect guess amount
                        case 1:
                            Console.WriteLine("  ____ ");
                            Console.WriteLine(" |    |");
                            Console.WriteLine(" |    O");
                            Console.WriteLine(" |    | ");
                            break;
                        case 2:
                            Console.WriteLine("  ____ ");
                            Console.WriteLine(" |    |");
                            Console.WriteLine(" |    O");
                            Console.WriteLine(" |   /|  ");
                            break;
                        case 3:
                            Console.WriteLine("  ____ ");
                            Console.WriteLine(" |    |");
                            Console.WriteLine(" |    O");
                            Console.WriteLine(" |   /|\\ ");
                            break;
                        case 4:
                            Console.WriteLine("  ____ ");
                            Console.WriteLine(" |    |");
                            Console.WriteLine(" |    O");
                            Console.WriteLine(" |   /|\\");
                            Console.WriteLine(" |    |");
                            Console.WriteLine(" |   / ");
                            Console.WriteLine(" |");
                            break;
                        case 5:
                            Console.WriteLine("  ____ ");
                            Console.WriteLine(" |    |");
                            Console.WriteLine(" |    O");
                            Console.WriteLine(" |   /|\\");
                            Console.WriteLine(" |    |");
                            Console.WriteLine(" |   / \\");
                            Console.WriteLine(" |");

                            break;
                        default:
                            Console.WriteLine("Invalid step number.");
                            break;
                    }
                }
                Console.WriteLine("Hey yinz, gather 'round! We got ourselves a real n'at hangman game goin' on. It's a proper Pittsburgh puzzler, so grab yer pop, kick back, and let's see if yinz can guess the words. No need to fret 'bout them empty spaces between words - we already got 'em figured aht. Give it a go and may the Stillers' luck be with yinz! Ready to tackle this Pittsburgh hangman, jagoffs?");
                Console.WriteLine();
                Console.WriteLine("Alright, listen up yinz! We're divin' into this hangman game, but this time, yinz only got five chances to crack the code.Grab yer Terrible Towel, sit back, and let's see if yinz can unravel the mystery.");
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
                    string hidden = new string(answer.Select(c => Char.IsLetter(c) ? '*' : c).ToArray());

                    Console.WriteLine($"Your word is {hidden}!");
                    Console.WriteLine($"And it is {answer.Length} letters");
                    Console.WriteLine();

                    while (correct < 5 && incorrect < 5)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Guess a letter, or if you know the answer, spell out the whole word.");

                        string guess = Console.ReadLine();


                        if (guess.Length > 1 && guess.Length == answer.Length)
                        {
                            if (guess.ToLower() != answer.ToLower())
                            {
                                Console.WriteLine("I'm sorry, that was an incorrect guess!");
                                DrawHangman(incorrect);
                            }
                            // They guessed the correct word and win the game.
                            else if (guess.ToLower() == answer.ToLower())
                            {
                                correct = 5;
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
                                Console.WriteLine();
                                Console.WriteLine("The word doesn't contain that letter!");
                                incorrect++;
                                DrawHangman(incorrect);
                                Console.WriteLine();
                                Console.WriteLine($"You have made {incorrect} attempts, {5 - incorrect} attempts remain");
                                Console.WriteLine();
                                Console.WriteLine($"{hidden}");
                                if (incorrect == 5)
                                {
                                    Console.WriteLine("Game over.");
                                }
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
                                            hidden = new string(answer.Select((c, i) => Char.IsLetter(c) && reveal[i] == '*' ? '*' : reveal[i]).ToArray());
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
                    Console.WriteLine("Enter Y or Yes to play again, N or No to exit");
                    Console.WriteLine();
                    playAgain = Console.ReadLine();

                    // User will now select 
                    if (playAgain.ToLower() == "y" || playAgain.ToLower() == "yes")
                    {
                        playAgain = "y";
                        correct = 0;
                        incorrect = 0;
                        hasLetter = false;
                        answer = "";
                    }
                    // User chose not to play again, exit the screen.
                    else if (playAgain.ToLower() == "n" || playAgain.ToLower() == "no")
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
                } while (playAgain.ToLower() == "y" || playAgain.ToLower() == "yes");

            }
        }
    }
}