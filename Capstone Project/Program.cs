using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Console
{
    class Program
    {
        // ****************************************************
        // Title: Tic Tac Toe
        // Description: Play a game of Tic Tac Toe in the console with a friend
        // Author: Zach Van Nes
        // Date Created: 4/22/2019
        // Last Modified: 4/28/2019
        // ****************************************************

        static void Main(string[] args)
        {
            // variables-arrays-lists
            bool keepGoing = true;

            // the gameboard 
            string[,] gameboard = new string[3, 3];

            // player info arrays
            string[] playersInfo = new string[4];
            string[] playerOne = new string[2];
            string[] playerTwo = new string[2];

            // start of code
            OpeningScreen();
            RulesScreen();
            while (keepGoing == true)
            {


                playersInfo = InitiateTwoPlayerGame();
                playerOne = BuildPlayerOneArray(playersInfo);
                playerTwo = BuildPlayerTwoArray(playersInfo);

                AnnouncePlayers(playerOne, playerTwo);
                ReadyToStart();

                PlayGame(playerOne, playerTwo, gameboard);
                Pause();
                keepGoing = KeepGoingCheck();

            }

        }

        private static void PlayGame(string[] playerOne, string[] playerTwo, string[,] gameboard)
        {
            // design of gameboard
            // found at https://codegolf.stackexchange.com/questions/95629/print-this-tic-tac-toe-board
            // I made small edits to make it work better for me
            //                
            //            number for board           array space for board
            //
            //                |     |                       |     |
            //             1  |  2  | 3                0,0  | 1,0 | 2,0
            //           _____|_____|_____             _____|_____|_____
            //                |     |                       |     |
            //             4  |  5  | 6                0,1  | 1,1 | 2,1
            //           _____|_____|_____             _____|_____|_____
            //                |     |                       |     |
            //             7  |  8  | 9                0,2  | 1,2 | 2,2
            //                |     |                       |     |
            //
            //

            // get the gameboard set for the game
            gameboard[0, 0] = "1";
            gameboard[1, 0] = "2";
            gameboard[2, 0] = "3";
            gameboard[0, 1] = "4";
            gameboard[1, 1] = "5";
            gameboard[2, 1] = "6";
            gameboard[0, 2] = "7";
            gameboard[1, 2] = "8";
            gameboard[2, 2] = "9";

            bool playerOneWin = false;
            bool playerTwoWin = false;
            bool catsGame = false;
            bool gameRunning = true;

            int turnCount = 0;

            Console.Clear();

            do
            {

                DisplayGameBoard(gameboard);
                PlayerOneMove(playerOne, gameboard);

                // after 9 turns the game will end becuase it has to be a cats game at that point and it will always be 9 turns after player one moves for a 5th time
                turnCount = turnCount + 1;
                switch (turnCount)
                {
                    case 9:
                        catsGame = true;
                        break;
                    default:
                        catsGame = false;
                        break;

                }

                playerOneWin = WinCheck(gameboard);

                Console.Clear();

                DisplayGameBoard(gameboard);

                if (catsGame == false)
                {
                    PlayerTwoMove(playerTwo, gameboard);
                    turnCount = turnCount + 1;
                }
                
                playerTwoWin = WinCheck(gameboard);


                if (playerOneWin == true)
                {
                    gameRunning = false;
                }
                else if (playerTwoWin == true)
                {
                    gameRunning = false;
                }
                else if (catsGame == true)
                {
                    gameRunning = false;
                }
                else
                {
                    gameRunning = true;
                }


                Console.Clear();

            } while (gameRunning == true);

            if (catsGame == true)
            {
                DisplayGameBoard(gameboard);
                Console.WriteLine();
                Console.WriteLine("CATS GAME");
            }
            else if (playerOneWin == true)
            {
                DisplayGameBoard(gameboard);
                Console.WriteLine();
                Console.WriteLine("CONGRADULATIONS " + playerOne[0] + " YOU WIN");
            }
            else if (playerTwoWin == true)
            {
                DisplayGameBoard(gameboard);
                Console.WriteLine();
                Console.WriteLine("CONGRADULATIONS " + playerTwo[0] + " YOU WIN");
            }

            Console.WriteLine();
           
        }

        static void AnnouncePlayers(string[] playerOne, string[] playerTwo)
        {
            Console.Clear();

            Console.WriteLine("Here are the players");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Player One: " + playerOne[0] + " Symbol: " + playerOne[1]);
            Console.WriteLine();
            Console.WriteLine("AND");
            Console.WriteLine();
            Console.WriteLine("Player Two: " + playerTwo[0] + " Symbol: " + playerTwo[1]);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Here is your gameboard, on your turn please enter the number of the space where you want to play");
            Console.WriteLine("If the space doesn't have a number but instead has an X or an O that means the space has already been played in and you can not play there");
        }

        static void DisplayGameBoard (string[,] gameboard)
        {
            // its the gameboard
            Console.WriteLine();
            Console.WriteLine("      |     |     ");
            Console.WriteLine("   " + gameboard[0, 0] + "  |  " + gameboard[1, 0] + "  |  " + gameboard[2, 0] + "  ");
            Console.WriteLine(" _____|_____|_____");
            Console.WriteLine("      |     |     ");
            Console.WriteLine("   " + gameboard[0, 1] + "  |  " + gameboard[1, 1] + "  |  " + gameboard[2, 1] + "  ");
            Console.WriteLine(" _____|_____|_____");
            Console.WriteLine("      |     |     ");
            Console.WriteLine("   " + gameboard[0, 2] + "  |  " + gameboard[1, 2] + "  |  " + gameboard[2, 2] + "  ");
            Console.WriteLine("      |     |     ");
            Console.WriteLine();
        }

        static void PlayerOneMove(string[] playerOneInfo, string[,] gameboard)
        {
            string playerOneMove;

            Console.WriteLine(playerOneInfo[0] + ", What is your move?");
            playerOneMove = Console.ReadLine();
            while (!ValidMoveCheck(gameboard, playerOneMove))
            {
                Console.WriteLine("Invalid move entered, please enter a valid move");
                playerOneMove = Console.ReadLine();
            }

            switch (playerOneMove)
            {
                case "1":
                    gameboard[0, 0] = playerOneInfo[1];
                    break;
                case "2":
                    gameboard[1, 0] = playerOneInfo[1];
                    break;
                case "3":
                    gameboard[2, 0] = playerOneInfo[1];
                    break;
                case "4":
                    gameboard[0, 1] = playerOneInfo[1];
                    break;
                case "5":
                    gameboard[1, 1] = playerOneInfo[1];
                    break;
                case "6":
                    gameboard[2, 1] = playerOneInfo[1];
                    break;
                case "7":
                    gameboard[0, 2] = playerOneInfo[1];
                    break;
                case "8":
                    gameboard[1, 2] = playerOneInfo[1];
                    break;
                case "9":
                    gameboard[2, 2] = playerOneInfo[1];
                    break;
                default:
                    break;
            }


        }

        static void PlayerTwoMove(string[] playerTwoInfo, string[,] gameboard)
        {
            string playerTwoMove;

            Console.WriteLine(playerTwoInfo[0] + ", What is your move?");
            playerTwoMove = Console.ReadLine();
            while (!ValidMoveCheck(gameboard, playerTwoMove))
            {
                Console.WriteLine("Invalid move entered, please enter a valid move");
                playerTwoMove = Console.ReadLine();
            }

            switch (playerTwoMove)
            {
                case "1":
                    gameboard[0, 0] = playerTwoInfo[1];
                    break;
                case "2":
                    gameboard[1, 0] = playerTwoInfo[1];
                    break;
                case "3":
                    gameboard[2, 0] = playerTwoInfo[1];
                    break;
                case "4":
                    gameboard[0, 1] = playerTwoInfo[1];
                    break;
                case "5":
                    gameboard[1, 1] = playerTwoInfo[1];
                    break;
                case "6":
                    gameboard[2, 1] = playerTwoInfo[1];
                    break;
                case "7":
                    gameboard[0, 2] = playerTwoInfo[1];
                    break;
                case "8":
                    gameboard[1, 2] = playerTwoInfo[1];
                    break;
                case "9":
                    gameboard[2, 2] = playerTwoInfo[1];
                    break;
                default:
                    break;
            }
        }

        //static string ComputerMove(string[] playerTwoInfo, string[,] gameboard)
        //{

        //}

        //static string[] InitiateOnePlayerGame()
        //{
        //    // player one info
        //    string playerOneName;
        //    string playerOneSymbol;

        //    // player two (computer) info
        //    string playerTwoName = "The Computer";
        //    string playerTwoSymbol;

        //    // return array
        //    string[] playersInfo = new string[4];

        //    // get player info
        //    playerOneName = GetPlayerName();
        //    playerOneSymbol = GetPlayerSymbol(playerTwoName);

        //    switch (playerOneSymbol)
        //    {
        //        case "X":
        //            playerTwoSymbol = "O";
        //            break;
        //        case "O":
        //            playerTwoSymbol = "X";
        //            break;
        //        default:
        //            Console.WriteLine("An unknown error has occured");
        //            Console.WriteLine(playerOneName + "'s smbol will be set to X");
        //            Console.WriteLine(playerTwoName + "'s symbol will be set to O");
        //            playerOneSymbol = "X";
        //            playerTwoSymbol = "O";
        //            break;
        //    }

        //    playersInfo[0] = playerOneName;
        //    playersInfo[1] = playerOneSymbol;
        //    playersInfo[2] = playerTwoName;
        //    playersInfo[3] = playerTwoSymbol;

        //    return playersInfo;
        //}

        static string[] InitiateTwoPlayerGame()
        {
            // player one info
            string playerOneName;
            string playerOneSymbol;

            // player two info
            string playerTwoName;
            string playerTwoSymbol;

            // return array
            string[] playersInfo = new string[4];

            // get player info
            playerOneName = GetPlayerName();
            playerTwoName = GetPlayerName();
            playerOneSymbol = GetPlayerSymbol(playerOneName, playerTwoName);

            switch (playerOneSymbol)
            {
                case "X":
                    playerTwoSymbol = "O";
                    break;
                case "O":
                    playerTwoSymbol = "X";
                    break;
                default:
                    Console.WriteLine("An unknown error has occured");
                    Console.WriteLine(playerOneName + "'s smbol will be set to X");
                    Console.WriteLine(playerTwoName + "'s symbol will be set to O");
                    playerOneSymbol = "X";
                    playerTwoSymbol = "O";
                    break;
            }

            playersInfo[0] = playerOneName;
            playersInfo[1] = playerOneSymbol;
            playersInfo[2] = playerTwoName;
            playersInfo[3] = playerTwoSymbol;

            return playersInfo;
        }

        static string GetPlayerName()
        {
            string playerName;

            Console.WriteLine("Please enter a name for yourself");
            playerName = Console.ReadLine();

            Console.WriteLine();

            return playerName;

        }

        static string GetPlayerSymbol(string playerOneName, string playerTwoName)
        {
            string playerSymbol;
            bool validSymbol = false;

            Console.WriteLine(playerOneName + " please choose either X's or O's");
            Console.WriteLine(playerTwoName + " will automatically be set to the other symbol");
            playerSymbol = Console.ReadLine();

            playerSymbol = playerSymbol.ToUpper();

            while (validSymbol == false)
            {
                switch (playerSymbol)
                {
                    case "X":
                        validSymbol = true;
                        break;
                    case "O":
                        validSymbol = true;
                        break;
                    default:
                        validSymbol = false;
                        Console.WriteLine("An invalid symbol was entered");
                        Console.WriteLine("Please enter either X or O");
                        playerSymbol = Console.ReadLine();
                        playerSymbol = playerSymbol.ToUpper();

                        break;
                }
            }

            Pause();

            return playerSymbol;
        }

        static string[] BuildPlayerOneArray(string[] playersInfo)
        {
            string[] playerOneInfo = new string[2];

            playerOneInfo[0] = playersInfo[0];
            playerOneInfo[1] = playersInfo[1];

            return playerOneInfo;
        }

        static string[] BuildPlayerTwoArray(string[] playersInfo)
        {
            string[] playerTwoInfo = new string[2];

            playerTwoInfo[0] = playersInfo[2];
            playerTwoInfo[1] = playersInfo[3];

            return playerTwoInfo;
        }

        static bool ValidMoveCheck(string[,] gameboard, string playerMove)
        {
            switch (playerMove)
            {
                case "1":
                    if (gameboard[0,0] == "X")
                    {
                        return false;
                    }
                    else if (gameboard[0, 0] == "O")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "2":
                    if (gameboard[1, 0] == "X")
                    {
                        return false;
                    }
                    else if (gameboard[1, 0] == "O")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "3":
                    if (gameboard[2, 0] == "X")
                    {
                        return false;
                    }
                    else if (gameboard[2, 0] == "O")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "4":
                    if (gameboard[0, 1] == "X")
                    {
                        return false;
                    }
                    else if (gameboard[0, 1] == "O")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "5":
                    if (gameboard[1, 1] == "X")
                    {
                        return false;
                    }
                    else if (gameboard[1, 1] == "O")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "6":
                    if (gameboard[2, 1] == "X")
                    {
                        return false;
                    }
                    else if (gameboard[2, 1] == "O")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "7":
                    if (gameboard[0, 2] == "X")
                    {
                        return false;
                    }
                    else if (gameboard[0, 2] == "O")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "8":
                    if (gameboard[1, 2] == "X")
                    {
                        return false;
                    }
                    else if (gameboard[1, 2] == "O")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case "9":
                    if (gameboard[2, 2] == "X")
                    {
                        return false;
                    }
                    else if (gameboard[2, 2] == "O")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                default:
                    return false;


                    
            }
        }

        static bool WinCheck(string[,] gameboard)
        {
            //                
            //            number for board           array space for board
            //
            //                |     |                       |     |
            //             1  |  2  | 3                0,0  | 1,0 | 2,0
            //           _____|_____|_____             _____|_____|_____
            //                |     |                       |     |
            //             4  |  5  | 6                0,1  | 1,1 | 2,1
            //           _____|_____|_____             _____|_____|_____
            //                |     |                       |     |
            //             7  |  8  | 9                0,2  | 1,2 | 2,2
            //                |     |                       |     |
            //
            //


            // check for win
            if (gameboard[0,0] == "X" && gameboard[0,1] == "X" && gameboard[0,2] == "X")
            {
                return true;
            }
            else if (gameboard[1, 0] == "X" && gameboard[1, 1] == "X" && gameboard[1, 2] == "X")
            {
                return true;
            }
            else if (gameboard[2, 0] == "X" && gameboard[2, 1] == "X" && gameboard[2, 2] == "X")
            {
                return true;
            }

            else if (gameboard[0, 0] == "X" && gameboard[1, 0] == "X" && gameboard[2, 0] == "X")
            {
                return true;
            }
            else if (gameboard[0, 1] == "X" && gameboard[1, 1] == "X" && gameboard[2, 1] == "X")
            {
                return true;
            }
            else if (gameboard[0, 2] == "X" && gameboard[1, 2] == "X" && gameboard[2, 2] == "X")
            {
                return true;
            }

            else if (gameboard[0, 0] == "X" && gameboard[1, 1] == "X" && gameboard[2, 2] == "X")
            {
                return true;
            }
            else if (gameboard[2, 0] == "X" && gameboard[1, 1] == "X" && gameboard[0, 2] == "X")
            {
                return true;
            }

            else if (gameboard[0, 0] == "O" && gameboard[0, 1] == "O" && gameboard[0, 2] == "O")
            {
                return true;
            }
            else if (gameboard[1, 0] == "O" && gameboard[1, 1] == "O" && gameboard[1, 2] == "O")
            {
                return true;
            }
            else if (gameboard[2, 0] == "O" && gameboard[2, 1] == "O" && gameboard[2, 2] == "O")
            {
                return true;
            }

            else if (gameboard[0, 0] == "O" && gameboard[1, 0] == "O" && gameboard[2, 0] == "O")
            {
                return true;
            }
            else if (gameboard[0, 1] == "O" && gameboard[1, 1] == "O" && gameboard[2, 1] == "O")
            {
                return true;
            }
            else if (gameboard[0, 2] == "O" && gameboard[1, 2] == "O" && gameboard[2, 2] == "O")
            {
                return true;
            }

            else if (gameboard[0, 0] == "O" && gameboard[1, 1] == "O" && gameboard[2, 2] == "O")
            {
                return true;
            }
            else if (gameboard[2, 0] == "O" && gameboard[1, 1] == "O" && gameboard[0, 2] == "O")
            {
                return true;
            }
            else
            {
                return false;
            }
            


        }

        //static bool CatsGameCheck(string[,] gameboard, int turnCount)
        //{
           
        //    // after 9 turns the game will end becuase it has to be a cats game at that point
        //    turnCount = turnCount + 1;

        //    switch (turnCount)
        //    {
        //        case 9:
        //            return true;
        //        default:
        //            return false;
                    
        //    }
        //}

        static bool KeepGoingCheck()
        {
            string userResponse;
            Console.WriteLine();
            Console.WriteLine("Would you like to play again?");
            Console.WriteLine("Enter Y to play again and N to not play again");
            userResponse = Console.ReadLine();
            userResponse = userResponse.ToUpper();

            if (userResponse == "Y")
            {
                Console.WriteLine("A new game will now start");
                Pause();
                Console.Clear();
                return true;
            }
            else if (userResponse == "N")
            {
                Console.WriteLine("Thank you for using my app");
                Pause();
                return false;
            }
            else
            {
                Console.WriteLine("Invalid response entered, game will not play again");
                Pause();
                Console.Clear();
                Console.WriteLine("Thank you for using my app");
                Pause();
                return false;
            }
       
        }

        //static int NumberOfPlayersScreen()
        //{
        //    int numberOfPlayers;
        //    bool validNumberOfPlayers = false;
        //    Console.Clear();

        //    Console.WriteLine("Player Select");
        //    Console.WriteLine();

        //    Console.WriteLine("Will one or two people be playing?");

        //    do
        //    {
        //        while (!int.TryParse(Console.ReadLine(), out numberOfPlayers))
        //        {
        //            Console.WriteLine("Invalid player number entered");
        //            Console.WriteLine("Please enter a valid number of players");
        //        }

        //        switch (numberOfPlayers)
        //        {
        //            case 1:
        //                validNumberOfPlayers = true;
        //                break;
        //            case 2:
        //                validNumberOfPlayers = true;
        //                break;
        //            default:
        //                validNumberOfPlayers = false;
        //                Console.WriteLine("Invalid player number entered");
        //                Console.WriteLine("Please enter a valid number of players");
        //                break;
        //        }

        //    } while (validNumberOfPlayers == false);
            

        //    Pause();

        //    return numberOfPlayers;

        //}

        static void OpeningScreen()
        {
            Console.WriteLine("Welcome to my Capstone project");
            Console.WriteLine("You will be able to play Tic Tac Toe in the console with another person");
            Console.WriteLine();
            Console.WriteLine("I hope you enjoy");
            Console.WriteLine();
            Pause();
        }
        static void RulesScreen()
        {
            Console.Clear();

            Console.WriteLine("Rules");
            Console.WriteLine();

            Console.WriteLine("1. Enter a name for each player");
            Console.WriteLine("2. Decide what player is X's and which player is O's");
            Console.WriteLine("3. Try to get three in a row, just like any other game of Tic Tac Toe");
            Console.WriteLine();

            Console.WriteLine("IMPORTANT NOTES");
            Console.WriteLine();
            Console.WriteLine("Player one will move first");
            Console.WriteLine();
            Console.WriteLine("In the event player one gets three in a row, player two will get one more move");
            Console.WriteLine("This extra move does not affect the game and player one will still win even if player two gets three in a row with this move");
            Console.WriteLine();
            Console.WriteLine("Player one will not get this extra move in the event player two gets three in a row first");
            Console.WriteLine();

            Pause();
            Console.Clear();
        }

        static void ReadyToStart()
        {
            Console.Clear();

            Console.WriteLine("Press any key to start the game");
            Console.ReadKey();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();
        }
    }
}
