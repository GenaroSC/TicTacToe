using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// Current results of the game
        /// </summary>

        private MarkerType[] mResults;

        private bool mPlayer1Turn;  // True if it's player 1 (X) turn and false for player 2 (O)
        private bool mGameOver;     //True if game is over

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        /// <summary>
        /// Starts a new game and resets all values
        /// </summary>
        private void NewGame()
        {
            mResults = new MarkerType[9];

            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkerType.Free;
            }

            mPlayer1Turn = true; // Make sure the game starts with player 1

            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Set content, background and foreground to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameOver = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender"> The button clicked </param>
        /// <param name="e">The events of the click </param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Start a new game if game is over and user presses any button
            if (mGameOver)
            {
                NewGame();
                return;
            }

            //Cast the sender to a button
            var button = (Button)sender;
            
            //Find the column and row of the button
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //Convert column and row to position in array
            var index = column + (row * 3);

            if (mResults[index] != MarkerType.Free)
            {
                return; //Dont do anything because button is not free
            }

            //Set cell value depending on player turn
            mResults[index] = mPlayer1Turn ? MarkerType.Cross : MarkerType.Nought;
            
            //Set button text on screen 
            button.Content = mPlayer1Turn ? "X" : "O";

            //Change button to green
            if (!mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //Toggle the player turn
            mPlayer1Turn ^= true;

            //Check for winner
            CheckForWinner();

            CheckForDraw();
        }

        /// <summary>
        /// Check if game ends in a draw
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CheckForDraw()
        {
            if (!mResults.Any(r => r == MarkerType.Free))
            {
                mGameOver = true;

                //Iterate every button on the grid
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
        }

        /// <summary>
        /// Checks if there is a winner of a 3 line straight
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void CheckForWinner()
        {
            #region HORIZONTAL WINS
            //Row 0
            if (mResults[0] != MarkerType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameOver = true;

                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //Row 1
            if (mResults[3] != MarkerType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameOver = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //Row 2
            if (mResults[6] != MarkerType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameOver = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region VERTICAL WINS
            //Column 0
            if (mResults[0] != MarkerType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameOver = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Column 1
            if (mResults[1] != MarkerType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameOver = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Column 2
            if (mResults[2] != MarkerType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameOver = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region DIAGONAL WINS
            //Left to Bottom Right
            if (mResults[0] != MarkerType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameOver = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Right to Bottom Left
            if (mResults[2] != MarkerType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameOver = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion
        }
    }
}