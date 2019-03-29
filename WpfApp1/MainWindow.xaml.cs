using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
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
        static List<String> GameBoard = new List<String>(); //Initialize list as 3x3 board in order to right and down.
        static int CurrentPlayer = 0; // Initialize variable for deciding starting player.
        // Symbols representing Player1 and Player2 in their respective variables.
        static string Player1 = "X";
        static string Player2 = "O"; //
        static List<Button> GameButtons = new List<Button>(); //List containing every button for core game.
        public MainWindow()
        {
            InitializeComponent();
            GameButtons.AddRange(new List<Button> //Add every button
            {
                buttonPlay0, buttonPlay1, buttonPlay2, buttonPlay3, buttonPlay4, buttonPlay5, buttonPlay6, buttonPlay7, buttonPlay8

            });
            for (int i = 0; i < 9; i++)
            {
                GameButtons[i].Tag = i.ToString();
            }
            // Randomly select integer 1 or 2. 
            Random rand = new Random();
            CurrentPlayer = rand.Next(0, 2);
            ClearBoard();

        }

        /// <summary>
        /// Called when player manually clicks on restart button or clicks on "OK" in the DecidePlayerTurn MessageBox.
        /// Clears the whole list and fills it with numbers from 0-8 again, clears content of all GameButtons and changes them to their default value.
        /// </summary>
        private void ClearBoard()
        {
            // Clear whole list in case of restart
            GameBoard.Clear();
            //Enables naming boxes for both players
            Player1Textbox.IsEnabled = true;
            Player2Textbox.IsEnabled = true;
            // Fill all indexes with numbers from 1 to 9 in ascending order, to make every index unlike each other and match their button's tag.
            for (int i = 0; i < 10; i++)
            {
                GameBoard.Add((i.ToString()));
                {

                }
                // Reset every GameButton to enable keyboard shortcuts and redesign text to default values.
                for (int j = 0; j < 9; j++)
                {
                    GameButtons[j].FontSize = 20.0;
                    GameButtons[j].Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF05C46B"));
                    GameButtons[j].Content = ("_" + (j+1));
                }
            }
            // Change current player image to their corresponding player
            if (CurrentPlayer % 2 != 0)
            {
                CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(247, 54, 54));
                CurrentPlayerIcon.Content = Player1;
            }
            else
            {
                CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(101, 162, 247));
                CurrentPlayerIcon.Content = Player2;
            }
        }

        /// <summary>
        /// Called when winner MessageBox needs to be shown, in case of a win.
        /// </summary>
        /// <param name="CurrentWinner">Uses a GameBoard index as the winning player</param>
        private void MatchResultInput(string CurrentWinner)
        {
           //Change player's default name to Textbox set name for game-over prompt.
            if (CurrentWinner == "X")
            {
                CurrentWinner = Player1;
            }
            else
            {
                CurrentWinner = Player2;
            }
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

        /// <summary>
        /// Called from ButtonClick with the button's tag and the button, used to check if button is taken
        /// Change CurrentPlayerIcon button's content
        /// Decide which player is making the pending decision
        /// </summary>
        /// <param name="tag">tag used to check if the button is taken</param>
        /// <param name="b">b used to pass on into the function CheckBoard</param>
        private void DecidePlayerTurn(int tag, Button b)
        {
            // If button's matching index in list has not been changed
            if (GameBoard[tag] == tag.ToString())
            {
                // Switch player every turn and use random int to choose starting player in 1st game
                // Change current player image to their corresponding player
                if (CurrentPlayer % 2 == 0)
                {
                    CheckBoard("O", tag, b);
                    CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(247, 54, 54));
                    CurrentPlayerIcon.Content = Player1;
                }
                else
                {
                    CheckBoard("X", tag, b);
                    CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(101, 162, 247));
                    CurrentPlayerIcon.Content = Player2;
                }
            }
            else
            {
                MessageBox.Show("Taken!");
            }
        }

        /// <summary>
        /// Called from DecidePlayerTurn where checks for a win or if all slots are taken, changes the clicked button's content unlike DecidePlayerTurn's function to change currenticon player.
        /// Ups the CurrentPlayer variable by one to change player.
        /// </summary>
        /// <param name="v">v used as player making the pending decision</param>
        /// <param name="tag">tag used as number to change GameBoard index to player's decision</param>
        /// <param name="b">button used to it's content, fontsize and color.</param>
        private void CheckBoard(string v, int tag, Button b)
        {
            // Change button's index in board array to the current player
            GameBoard[tag] = v;
            // If player is O or X
            b.FontSize = 70.0;
            if (v == "X")
            {
                b.Foreground = new SolidColorBrush(Color.FromRgb(247, 54, 54));
                b.Content = Player1;
            }
            else
            {
                b.Foreground = new SolidColorBrush(Color.FromRgb(101, 162, 247));
                b.Content = Player2;
            }
            // Check various combinations horizontally and vertically to see if a player achived 3 in a row
            for (int i = 0; i < 3; i++)
            {
                if (GameBoard[0 + (3 * i)] == GameBoard[1 + (3 * i)] && GameBoard[1 + (3 * i)] == GameBoard[2 + (3 * i)])
                {
                    MatchResultInput(GameBoard[0 + (3 * i)]);
                }
                if (GameBoard[0 + (1 * i)] == GameBoard[3 + (1 * i)] && GameBoard[3 + (1 * i)] == GameBoard[6 + (1 * i)])
                {
                    MatchResultInput(GameBoard[3 + (1 * i)]);
                }
            }
            // Check both diagonal combinations to see if a player achived 3 in a row
            if (GameBoard[0] == GameBoard[4] && GameBoard[4] == GameBoard[8])
            {
                MatchResultInput(GameBoard[0]);
            }
            if (GameBoard[2] == GameBoard[4] && GameBoard[4] == GameBoard[6])
            {
                MatchResultInput(GameBoard[2]);
            }
            // Up the variable by one to change player next turn
            CurrentPlayer++;
            // Check if all slots are taken
            if (
                    (GameBoard[0] == "X" || GameBoard[0] == "O")
                && (GameBoard[1] == "X" || GameBoard[1] == "O")
                && (GameBoard[2] == "X" || GameBoard[2] == "O")
                && (GameBoard[3] == "X" || GameBoard[3] == "O")
                && (GameBoard[4] == "X" || GameBoard[4] == "O")
                && (GameBoard[5] == "X" || GameBoard[5] == "O")
                && (GameBoard[6] == "X" || GameBoard[6] == "O")
                && (GameBoard[7] == "X" || GameBoard[7] == "O")
                && (GameBoard[8] == "X" || GameBoard[8] == "O")
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

        /// <summary>
        /// Function is called when 
        /// </summary>
        /// <param name="sender">sender as button is used to change it's content</param>
        /// <param name="e">e not used</param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            //Disables naming boxes if the game has started.
            Player1Textbox.IsEnabled = false;
            Player2Textbox.IsEnabled = false;
            // Use button as object in further functions
            Button b = (sender as Button);
            // Get the button's tag and store it in a variable
            int buttonTag = int.Parse(b.Tag.ToString());
            DecidePlayerTurn(buttonTag, b);
        }

        /// <summary>
        /// Function is called when user clicks on "help" button where it opens a messagebox to the user.
        /// </summary>
        /// <param name="sender">sender not used</param>
        /// <param name="e">e not used</param>
        private void HelpButton(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Rules\n\n1. The board/playing area is a grid that is 3 squares wide and 3 squares tall. \n2. There are two players, 'X' or 'O', and you will need to assign yourself to one of them. \n3. The player who is first to get 3 of their own marks in a row in a diagional, horizontal or vertical order will win. \n4. If all the slots are taken on the board then the game will result in a draw. \n\nHow to win? \n\nYour goal is to get 3 marks in a row, when you first set out your first mark you will then need to look ahead and make decisions that will benefit you in the future. This would for example be that if you as a player places a mark across the board that might give the incentive of the other player to block your path. The trick is to have the player not knowing what your next motive will be after you have placed your first mark.");
        }

        /// <summary>
        /// Function is called when text changes in the Textbox containing the player's symbol.
        /// </summary>
        /// <param name="sender">Textbox</param>
        /// <param name="e">e not used</param>
        private void ChangeNameInput(object sender, TextChangedEventArgs e)
        {
            TextBox t = (sender as TextBox);
            //If selected Textbox belongs to player1 or player2.
            if (t.Name == "Player1Textbox")
            {
                //Make content in textbox into player's symbol.
                Player1 = Player1Textbox.Text;
            }
            else
            {
                //Make content in textbox into player's symbol.
                Player2 = Player2Textbox.Text;
            }
            //Change button containing current player to the correct symbol and color.
            if (CurrentPlayer % 2 != 0)
            {
                CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(247, 54, 54));
                CurrentPlayerIcon.Content = Player1;
            }
            else
            {
                CurrentPlayerIcon.Foreground = new SolidColorBrush(Color.FromRgb(101, 162, 247));
                CurrentPlayerIcon.Content = Player2;
            }
        }

        /// <summary>
        /// Called when user clicks on the Restart Button
        /// </summary>
        /// <param name="sender">sender is not used</param>
        /// <param name="e">e is not used</param>
        private void RestartButton(object sender, RoutedEventArgs e)
        {
            //Restarts the game if the player manually clicks on the restart button
            ClearBoard();
        }
    }
}
