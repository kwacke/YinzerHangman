using System;
using System.ComponentModel.Design;
using System.Text;
namespace YinzerHangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Yinzer Hangman, the game where we take everyday words, spell them in Pittsburghese, and have you guess them before you're hanged!");

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

            Console.WriteLine();
            var random = new Random();
            var word = random.Next(yinzWords.Length);
            string answer = yinzWords[word];
            string playAgain = "";
            //StringBuilder newWord = new StringBuilder(word);

            //loop while ? while
            while (answer.Length > correct)
            { 
                Console.WriteLine($"Your word is {answer.Length} letters long!");
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
                    else
                    {
                        Console.WriteLine("You win the game!");
                        correct = guess.Length;
                    }
                }
                //user only guessed a single lette
                else if (guess.Length == 1)
                {
                    foreach (char singleAnswer in answer)
                    {
                        if (guess.Contains(singleAnswer))
                        {
                            Console.WriteLine($"{guess} was found in the word! Make your next letter guess or enter word!");
                            correct++;
                            if (correct == answer.Length)
                            {
                                Console.WriteLine($"The word is {answer}. You win!");
                            }
                        }
                        else if(!guess.Contains(singleAnswer)){
                            Console.WriteLine("The word doesn't contain that letter!");
                            incorrect++;
                        }

                    }
                }

            }
            while (answer.Length == correct || incorrect == 5)
                
                {
                    Console.WriteLine("Would you like to play again?");
                    Console.WriteLine();
                    Console.WriteLine("'Y' to play again, 'N' to exit");
                    Console.WriteLine();
                    playAgain = Console.ReadLine();

                    // User will now select 
                    if (playAgain.ToLower() == "y")
                    {
                        playAgain = "y";
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
                }

        }
    }
}