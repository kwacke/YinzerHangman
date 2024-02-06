using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
namespace YinzerHangman
{
    public class Program
    {
        public void Main(string[] args)
        {

            Console.WriteLine("Hey yinz, gather 'round! We got ourselves a real n'at hangman game goin' on.");
            Console.WriteLine("It's a proper Pittsburgh puzzler, so grab yer pop, kick back, and let's see if yinz can guess the words.");
            Console.WriteLine("Ready to tackle this Pittsburgh hangman, ya jagoffs?");
            Console.WriteLine();
            Console.WriteLine("Yinz only got five chances to crack the code.Grab yer Terrible Towel, sit back, and let's see if yinz can unravel the mystery.");
            Console.WriteLine();

            // Originally had an array, I decided on the dictionary so that the key could hold the hints for the word
            Dictionary<string, string> yinzWords = new Dictionary<string, string>
            {
            {"Yinz", "Pittsburghese pronoun"},
            {"Clahdy", "Weather-related term"},
            {"Jagoff", "Pittsburgh insult"},
            {"Dahntahn", "Downtown"},
            {"Grahnd bees", "Yellow jacket bees in the ground"},
            {"Rahn-da-baht", "Song by the group yes or slang for a long way of getting to your destination"},
            {"Slippy", "Icy"},
            {"Gumband", "Rubber band"},
            {"Seent", "Seen it"},
            {"Cousint", "Cousin"},
            {"Friggin", "Vulgar slang"},
            {"Crick", "Small stream or creek"},
            {"Irn", "Short for Iron City Beer"},
            {"Worsh", "Wash"},
            {"Sweeper", "Vacuum cleaner"},
            {"Spickett", "Outdoor water faucet"},
            {"Prolly", "Probably"},
            {"Zak same", "Exactly the same"},
            {"Lahsy", "Lousy"},
            {"Jagger bush", "Thorny bush or plant"},
            {"Up air", "Up there"},
            {"Da-Boat-a-ya", "The both of you"},
            {"Nebby", "Nosy"},
            {"N'at", "And that"},
            {"Gutchies", "Underwear"},
            {"Buggy", "Shopping cart"},
            {"Jumbo", "Bologna"},
            {"Chipped-Chopped ham", "Isaly's was famous for this!"},
            {"Red Up", "Clean up"},
            {"Jeet Jet?", "Did you eat yet?"},
            {"Dippy Eggs", "Sunny-side-up eggs"},
            {"Pop", "Soda"},
            {"Canipshun", "Conniption"},
            {"Sammitches", "Sandwiches"},
            {"Ahnno-dat", "I don't know that"},
            {"A whole nother", "A whole other"},
            { "Airyago", "There you go"},
            {"Apost tu", "Supposed too"},
            {"Back'air", "Back there"},
            {"Bo fuss", "Both of us"},
            {"Bowchyins", "Both of you"},
            {"Buy Sam a drink and get his dog one too!", "Mike Lang Slang"},
            {"Can a corn", "Easy catch in baseball"},
            {"Choobinuptoo", "What have you been up to?"},
            {"Cole-daht-dare", "It's cold outside"},
            {"Come mere", "Come here"},
            {"Dahn nair", "Down there"},
            {"Daht'et", "That it"},
            {"Don't go err wit me", "Don't argue with me"},
            {"Dooder Jobs", "Do their jobs"},
            {"Elvis has left the building", "The show is over"},
            {"Hafta", "Have to"},
            {"Hauscome", "How has it come"},
            {"Hay Bir Here", "Hey beer here"},
            {"How's come?", "Why is that?"},
            {"Ize", "I am"},
            {"Ja Wanna", "Do you want to"},
            {"Jano?", "Do you know?"},
            {"Jeez-o-man", "Expression of surprise"},
            {"Kennywood's Open", "The zipper on your pants is open"},
            {"Lassnite", "Last night"},
            {"Like iss", "Like this"},
            {"Like 'at", "Like that"},
            {"Nuh-uh!!", "No"},
            {"Oh call Arnold Slick from Turtle Crick!", "Expression of surprise"},
            {"Oh mah gersh!", "Oh my gosh!"},
            {"Ovaderr", "Over there"},
            {"Same difference", "No difference"},
            {"Scratch my back with a hacksaw!", "Mike Lang Slang"},
            {"Sick'n tard", "Sick and tired"},
            {"Sposda", "Supposed to"},
            {"bungals", "Slang for Cincinatti's football team"},
            {"brahns", "Brown shoes"},
            {"Ya Gatta Regatta!", "You have to attend the regatta"}
            };

            int incorrect = 0;
            int correct = 0;
            bool hasLetter = false;
            string playAgain;
            string answer = "";

            do 
            {
                //Selects random KVP from our Dictionary and take the key and turn it into string answer
                var random = new Random();
                var selectedWord = yinzWords.ElementAt(random.Next(yinzWords.Count));
                answer = selectedWord.Key;

                //I asked the internet how to do this, a ternary that checks if its a letter to replace with an asteriks or leaves the symbol or space if not a letter
                string hidden = new string(answer.Select(c => Char.IsLetter(c) ? '*' : c == ' ' || c == '\'' || c == '-' || c == ',' || c == '?' || c == '.' ? c : ' ').ToArray());

                //Console.WriteLine($"Your word is {hidden}!");
                Console.WriteLine($"Hint: {selectedWord.Value}: {hidden}");
                Console.WriteLine();
                Console.WriteLine($"The word is {answer.Length} letters");
                Console.WriteLine();

                // makes sure that you haven't guessed the word or run out of guesses
                while (hidden.Contains('*') && incorrect < 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Take a shot at guessin' a letter, 'er if yinz already know it, spell out the whole word, 'er whatever");

                    string guess = Console.ReadLine();


                    if (guess.Length > 1 && guess.Length == answer.Length)
                    {
                        string filteredGuess = new string(guess.ToLower().Where(c => Char.IsLetter(c) || c == ' ' || c == '\\' || c == '-' || c == ',' || c == '?' || c == '.' || c == '!').ToArray());
                        string filteredAnswer = new string(answer.ToLower().Where(c => Char.IsLetter(c) || c == ' ' || c == '\\' || c == '-' || c == ',' || c == '?' || c == '.' || c == '!').ToArray());
                        if (filteredGuess == filteredAnswer)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Da answer is {answer}");
                            Console.WriteLine();
                            Console.WriteLine("You's won the game!");
                            Console.WriteLine();
                            break; // Exit since they have won and the game is over.
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, that was an incorrect guess!");
                            Console.WriteLine($"{hidden}");
                            DrawHangman(incorrect);
                        }

                    }
                    //user only guessed a single letter
                    else if (guess.Length == 1)
                    {
                        //if they guess incorrectly respond and increment the incorrect variable
                        if (!answer.ToLower().Contains(char.ToLower(guess[0])))
                        {
                            Console.WriteLine();
                            Console.WriteLine("The word ain't gat that letter!");
                            incorrect++;
                            DrawHangman(incorrect);
                            Console.WriteLine();
                            Console.WriteLine($"Yinz made {incorrect} tries, 'n'at. Still got {5 - incorrect} tries left, y'know.");
                            Console.WriteLine();
                            Console.WriteLine($"{hidden}");
                            if (incorrect == 5)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Game over.");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            //number of times a letter appears in a word, in case more than once
                            int numberOfTimes = 0;

                            /* create a CharArray to hold letters asterisk and reveal them 
                             as we loop through the length of answer checking if the guesses are in each index
                            position of answer*/

                            char[] reveal = hidden.ToCharArray();

                            for (int i = 0; i < answer.Length; i++)
                            {
                                // this checks if your guess is in the word
                                // then reveals that letter as many times as it appears in the word
                                if ((char.ToLower(guess[0]) == char.ToLower(answer[i])))
                                {
                                    hasLetter = true;
                                    numberOfTimes++;

                                    if (!hidden.Contains(char.ToLower(guess[0])))
                                    {
                                        reveal[i] = answer[i];
                                    }
                                }
                            }
                            Console.WriteLine();
                            // if you guess correctly and haven't guessed all of the letters

                            /* I added the number of times so that if the letter appears multiple
                            in the word, it will only Console.WriteLine one time */
                            if (hasLetter)
                            {
                                Console.WriteLine("Spot on, n'at!");
                                Console.WriteLine();
                                hidden = new string(reveal);
                                Console.WriteLine($"{hidden}!");
                            }
                            //correct = hidden.Count(c => c != '*' && char.IsLetter(c));

                            //if (correct == answer.Where(char.IsLetter).Distinct().Count())
                            if (!hidden.Contains("*"))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Yinz win!");
                                Console.WriteLine();
                                Console.WriteLine($"Yer word is {answer}");
                                break;
                            }
                        }
                    }
                }

                if (hidden.Contains('*') && incorrect == 5)
                {
                    Console.WriteLine();
                    Console.WriteLine("Game over, ya Jag.");
                    Console.WriteLine();
                }

                // This runs after you have guessed the word or have ran out of guesses

                Console.WriteLine("Yinz wanna play again?");
                Console.WriteLine();
                Console.WriteLine("Jus' type in Y or Yes if yinz wanna play again, 'n if yinz had enough, type N or No to exit, y'know");
                Console.WriteLine();
                playAgain = Console.ReadLine();
                Console.WriteLine();

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
                    Console.WriteLine("Thanks for playing, see yinz next time!");
                }
                // They made an incorrect choice
                else
                {
                    Console.WriteLine("I'm sorry 'n'at, but could yinz please enter a Y to play again or an N to exit, jagoff");
                    Console.WriteLine();
                    playAgain = Console.ReadLine();

                }
            } // this loop condition is needed to start the game over again after selecting yes to play again
            while (playAgain.ToLower() == "y" || playAgain.ToLower() == "yes");

        }
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

    }

}