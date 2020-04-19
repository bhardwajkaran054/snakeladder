using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LadderAndSnakeVersio1
{
    public class SnakeAndLadder
    {
        public SnakeAndLadder() //Constructor by default generate snakes and ladder
        {
            Snake();
            Ladder();
        }

        protected int[] snakeStart = new int[9];
        protected int[] snakeEnd = new int[9];

        protected int[] ladderStart = new int[9];
        protected int[] ladderEnd = new int[9];

        Random random = new Random();
        private int _range;
        

        private void Snake()
        {
            _range = 91;//for snake 

            for (int i = 0; i < 9; i++)
            {
                /*Snake Head(Starting Point) Generator*/
                snakeStart[i] = random.Next(_range, _range + 9);//initialy, Range Between (91, 100), then (81, 90), (71, 80), ... (11, 20).

                /*Check Random Generated Snake Start Value Cant Be 100*/
                if (snakeStart[i] == 100)
                    snakeStart[i] -= 1;

                /*check the starting and ending point are not same*/
                for (int j = 0; j < i; j++)
                {
                    if (snakeStart[i] == snakeEnd[j])
                    {
                        Console.WriteLine("snakeStart({0}) and snakeEnd({1}) are same", i + 1, j + 1);
                        snakeStart[i] -= 1;
                    }
                }

                /*snake tail (ending point) generator*/
                snakeEnd[i] = random.Next(9, snakeStart[i] - 1);//Exceptional Handling: if all the random generated end point of snake will be same then from 9, all the point will be greater than 1.
         
                /*check the ends are not same*/
                for (int j = 0; j < i; j++)
                {
                    if (snakeEnd[i] == snakeEnd[j])
                    {
                        Console.WriteLine("snakeEnd({0}) and snakeEnd({1}) are same", i + 1, j + 1);
                        snakeEnd[i] -= 1;
                    }
                }

                _range -= 10;//decreament range by 10, for geting this range sequence (91,100),(81,90),(71,80)...(11,20)

            }

            //SnakeValuesDisplay();
        }

        private void Ladder()
        {
            _range = 1;//for ladder

            for (int i = 0; i < 9; i++)
            {
                /*ladder starting point generator*/
                ladderStart[i] = random.Next(_range, _range + 9);//initialy, Range Between (1, 10), then (11, 20), (21, 30), ... (81, 90).

                /*Here, checking the generated ladder point should not be conflict with snake i.e.(snakeStart != ladderStart AND snakeEnd != ladderStart)*/
                for (int j = 0; j < 9; j++)
                {
                    if (ladderStart[i] == snakeStart[j])
                    {
                        Console.WriteLine("#ladderStart({0}) and ~snakeStart({1}) are same", i + 1, j + 1);
                        ladderStart[i] += 1;
                    }
                    if (ladderStart[i] == snakeEnd[j])
                    {
                        Console.WriteLine("#ladderStart({0}) and ~snakeEnd({1}) are same", i + 1, j + 1);
                        ladderStart[i] += 1;
                    }
                }

                /*check the starting and ending point should not be same*/
                for (int j = 0; j < i; j++)
                {
                    if (ladderStart[i] == ladderEnd[j])
                    {
                        Console.WriteLine("ladderStart({0}) and ladderEnd({1}) are same", i + 1, j + 1);
                        ladderStart[i] += 1;
                    }
                }

                /*ladder ending point generator*/
                ladderEnd[i] = random.Next(ladderStart[i] + 1, 91);//exceptional handling: if all the ladder end point will be same then upto 91 all the ladder occupy number < 100

                /*Generated end point should not be conflict be snake points (ladderEnd != snakeStart)*/
                for (int j = 0; j < 9; j++)
                {
                    if (ladderEnd[i] == snakeStart[j])
                    {
                        Console.WriteLine("#ladderEnd({0}) and ~snakeStart({1}) are same", i + 1, j + 1);
                        ladderEnd[i] += 1;
                    }
                }

                /*check the ends should not be same*/
                for (int j = 0; j < i; j++)
                {
                    if (ladderEnd[i] == ladderEnd[j])
                    {
                        Console.WriteLine("ladderEnd({0}) and ladderEnd({1}) are same", i + 1, j + 1);
                        ladderEnd[i] += 1;
                    }
                }

                _range += 10;

            }
            //LadderValuesDisplay();
        }

        public void SnakeValuesDisplay()
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("Snake-{0}: from {1} to {2}", i + 1, snakeStart[i], snakeEnd[i]);
            }
        }

        public void LadderValuesDisplay()
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("Ladder{0}: from {1} to {2}", i + 1, ladderStart[i], ladderEnd[i]);
            }
        }
    }

    public class Board : SnakeAndLadder
    {
        public void PlayerAndLogic(int totalPlayers)
        {
            Console.WriteLine("Snake and Ladder generation LOG:-\n");
            SnakeAndLadder snakeAndLadder = new SnakeAndLadder(); // snake and ladder object.

            const int MaxValue = 100;//Maximum Snake & Ladder Board Value
            int playerTurn = 1;//initally set player1 to play game
            int diceValue;
            bool stopGame = false;
            int winner = 0;//initially set winner is nobody



            int[] playerScore = new int[totalPlayers];

            for (int i = 0; i < totalPlayers; i++)//initially set playerscore value to 0
            {
                playerScore[i] = 0;
            }

            while (true)
            {
                SnakeValuesDisplay();
                LadderValuesDisplay();

                Console.WriteLine("\nPlayer{0}:-\nEnter Dice Value(1 to 6)", playerTurn);//loophole-1: user can enter more than 6, follow solution-1
                diceValue = Convert.ToInt32(Console.ReadLine());

                if ((diceValue > (MaxValue - playerScore[playerTurn - 1])) || (diceValue > 6)) //solution-1: if user enter dice value more than required value to win OR enter more than 6 then skip that value
                {
                    Console.WriteLine("Enter Valid value! or Required Value! You missed chance");
                    diceValue = 0; //reset user entered value to 0 because entered value is not acceptable
                }

                playerScore[playerTurn - 1] += diceValue;

                /*logic to add Snake and Ladder in the game*/
                for (int i = 0; i < 9; i++)
                {
                    if (playerScore[playerTurn - 1] == snakeStart[i])
                    {
                        Console.WriteLine("Playe{0} bitten by Snake: {2} <<< {1}", playerTurn, snakeStart[i], snakeEnd[i]);
                        playerScore[playerTurn - 1] = snakeEnd[i];
                    }

                    if (playerScore[playerTurn - 1] == ladderStart[i])
                    {
                        Console.WriteLine("Playe{0} got Ladder: {1} >>> {2}", playerTurn, ladderStart[i], ladderEnd[i]);
                        playerScore[playerTurn - 1] = ladderEnd[i];
                    }
                }
                /*End of logic*/

                for (int i = 0; i < totalPlayers; i++)
                {
                    Console.WriteLine("Player{0} Score: {1}", i + 1, playerScore[i]);

                    if (playerScore[i] == MaxValue)//loophole-2: if user entered more than required value, follow solution-1
                    {
                        winner = i + 1;

                        stopGame = true;
                    }
                }
                if (stopGame == true)
                {
                    Console.WriteLine("\n***** Player{0} Won the game. *****\n", winner);
                    break;
                }

                if (playerTurn == totalPlayers) //reset the player turn(1-2-3-1-2..). example, totalPlayer = 3 then playerTurn 1-2-3-4-1-2-3....
                    playerTurn = 1;
                else
                    playerTurn++;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();

            Console.WriteLine("\n***** SNAKE LADDER GAME *****\n");

            Console.WriteLine("Enter number of players");
            var totalPlayers = Convert.ToInt32(Console.ReadLine());
            board.PlayerAndLogic(totalPlayers);

        }



    }
}

