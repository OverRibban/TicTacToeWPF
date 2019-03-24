using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<String> gameBoard = new List<String>(); //Initialize list as 3x3 board in order to right and down.
        static int currentPlayer = 0; // Initialize variable for deciding starting player
        static List<String> gameButtons = new List<String>();
        public MainWindow()
        {
            InitializeComponent();
            buttonPlay0.Tag = "0";
            buttonPlay1.Tag = "1";
            buttonPlay2.Tag = "2";
            buttonPlay3.Tag = "3";
            buttonPlay4.Tag = "4";
            buttonPlay5.Tag = "5";
            buttonPlay6.Tag = "6";
            buttonPlay7.Tag = "7";
            buttonPlay8.Tag = "8";
            // Randomly select integer 1 or 2. 
            Random rand = new Random();
            currentPlayer = rand.Next(0, 2);
            ClearBoard();

        }
        private void ClearBoard()
        {
            // Clear whole list in case of restart
            gameBoard.Clear();
            // Fill all indexes with numbers from 1 to 9 in ascending order, to make every index unlike each other and match their button's tag.
            for (int i = 0; i < 10; i++)
            {
                gameBoard.Add((i.ToString()));
                // Null every form that has a tag to reset board buttons
            }
            // Change current player image to their corresponding player
            if (currentPlayer % 2 != 0)
            {
                CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(247, 54, 54));
                CurrentPlayerIcon.Content = "X";
            }
            else
            {
                CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(101, 162, 247));
                CurrentPlayerIcon.Content = "O";
            }
        }
        private void MatchResultInput(string CurrentWinner)
        {
            // Display winner and restart game if either player clicks on "OK" in the end message.
            MessageBoxResult result = MessageBox.Show("Do you want to play again?",
                                          CurrentWinner + " Won!",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearBoard();
            }
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            // Use button as object in further functions
            Button b = (sender as Button);
            // Get the button's tag and store it in a variable
            int buttonTag = int.Parse(b.Tag.ToString());
            DecidePlayerTurn(buttonTag, b);
        }
        private void DecidePlayerTurn(int tag, Button b)
        {
            // If button's matching index in list has not been changed
            if (gameBoard[tag] == tag.ToString())
            {
                // Switch player every turn and use random int to choose starting player in 1st game
                // Change current player image to their corresponding player
                if (currentPlayer % 2 == 0)
                {
                    CheckBoard("O", tag, b);
                    CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(247, 54, 54));
                    CurrentPlayerIcon.Content = "X";
                }
                else
                {
                    CheckBoard("X", tag, b);
                    CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(101, 162, 247));
                    CurrentPlayerIcon.Content = "O";
                }
            }
            else
            {
                MessageBox.Show("Taken!");
            }
        }
        private void CheckBoard(string v, int tag, Button b)
        {
            // Change button's index in board array to the current player
            gameBoard[tag] = v;
            // If player is O or X
            if (v == "O")
            {
                b.Foreground = new SolidColorBrush(Color.FromRgb(101, 162, 247));
                b.Content = "O";
            }
            else
            {
                b.Foreground = new SolidColorBrush(Color.FromRgb(247, 54, 54));
                b.Content = "X";
            }
            // Check various combinations horizontally and vertically to see if a player achived 3 in a row
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[0 + (3 * i)] == gameBoard[1 + (3 * i)] && gameBoard[1 + (3 * i)] == gameBoard[2 + (3 * i)])
                {
                    MatchResultInput(gameBoard[0 + (3 * i)]);
                }
                if (gameBoard[0 + (1 * i)] == gameBoard[3 + (1 * i)] && gameBoard[3 + (1 * i)] == gameBoard[6 + (1 * i)])
                {
                    MatchResultInput(gameBoard[3 + (1 * i)]);
                }
            }
            // Check both diagonal combinations to see if a player achived 3 in a row
            if (gameBoard[0] == gameBoard[4] && gameBoard[4] == gameBoard[8])
            {
                MatchResultInput(gameBoard[0]);
            }
            if (gameBoard[2] == gameBoard[4] && gameBoard[4] == gameBoard[6])
            {
                MatchResultInput(gameBoard[2]);
            }
            // Up the variable by one to change player next turn
            currentPlayer++;
            // Check if all slots are taken
            if (
                    (gameBoard[0] == "X" || gameBoard[0] == "O")
                && (gameBoard[1] == "X" || gameBoard[1] == "O")
                && (gameBoard[2] == "X" || gameBoard[2] == "O")
                && (gameBoard[3] == "X" || gameBoard[3] == "O")
                && (gameBoard[4] == "X" || gameBoard[4] == "O")
                && (gameBoard[5] == "X" || gameBoard[5] == "O")
                && (gameBoard[6] == "X" || gameBoard[6] == "O")
                && (gameBoard[7] == "X" || gameBoard[7] == "O")
                && (gameBoard[8] == "X" || gameBoard[8] == "O")
                )
            {
                // Restarts game if either player clicks on "OK" in end message
                MessageBoxResult result = MessageBox.Show("Do you want to play again?",
                                              "DRAW!",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ClearBoard();
                }
            }
        }
        // Restarts the game if the player manually clicks on the restart button
        private void button11_Click(object sender, EventArgs e)
        {
            ClearBoard();
        }

        private void TimeLeftLabel_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ClearBoard();
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // Use button as object in further functions
            Button b = (sender as Button);
            // Get the button's tag and store it in a variable
            int buttonTag = int.Parse(b.Tag.ToString());
            DecidePlayerTurn(buttonTag, b);
        }
        // Display manual for the player
        private void HelpButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Rules\n\n1. The board/playing area is a grid that is 3 squares wide and 3 squares tall. \n2. There are two players, 'X' or 'O', and you will need to assign yourself to one of them. \n3. The player who is first to get 3 of their own marks in a row in a diagional, horizontal or vertical order will win. \n4. If all the slots are taken on the board then the game will result in a draw. \n\nHow to win? \n\nYour goal is to get 3 marks in a row, when you first set out your first mark you will then need to look ahead and make decisions that will benefit you in the future. This would for example be that if you as a player places a mark across the board that might give the incentive of the other player to block your path. The trick is to have the player not knowing what your next motive will be after you have placed your first mark.");
        }
    }
}
