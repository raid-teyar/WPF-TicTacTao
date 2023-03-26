using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacTao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int gameCounter = 0;
        bool gameEnded;
        MarkedType[] inputs;

        public MainWindow()
        {
            InitializeComponent();
            newGame();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            int column = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            int index = (row * 3) + column;

            if (gameEnded)
            {
                MessageBoxResult result = MessageBox.Show("Again?", "Game Finished!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    newGame();
                    return;
                }
                else
                {
                    Application.Current.Shutdown();
                }
                
            }

            if (inputs[index] != MarkedType.none)
            {
                return;
            }

            inputs[index] = MarkedType.X;
            button.Content = "X";
            button.Foreground = Brushes.Blue;
            gameCounter++;

            botPlay();

            checkGrid();
        }

        public void newGame()
        {
            gameCounter = 0;

            inputs = new MarkedType[9];

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = MarkedType.none;
            }

            myGrid.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
            });

            gameEnded = false;

            botPlay();
        }


        void botPlay()
        {
            switch (gameCounter)
            {
                case 0:
                    play(4, btn5);
                    break;

                case 2:
                    {
                        if (inputs[5] == MarkedType.none)
                            play(5, btn6);
                        else
                            play(3, btn4);
                    }
                    break;

                case 4:
                    {
                        if (inputs[0] == MarkedType.X && inputs[3] == MarkedType.X)
                        {
                            play(6, btn7);
                        }
                        else if (inputs[6] == MarkedType.X && inputs[3] == MarkedType.X)
                        {
                            play(0, btn1);
                        }
                        else
                        {
                            if (inputs[5] == MarkedType.none)
                                play(5, btn6);
                            else if (inputs[3] == MarkedType.none)
                                play(3, btn4);
                            else
                            {
                                if (inputs[2] == MarkedType.none)
                                {
                                    play(2, btn3);
                                }
                                else if (inputs[1] == MarkedType.none)
                                {
                                    play(1, btn2);
                                }
                                else
                                {
                                    play(0, btn1);
                                }
                            }
                        }
                    }
                    break;
                case 6:
                    {
                        if (inputs[2] == MarkedType.none)
                        {
                            play(2, btn3);
                        }
                        else
                        {
                            if (inputs[6] == MarkedType.none)
                            {
                                play(6, btn7);
                            }
                            else if (inputs[8] == MarkedType.none)
                            {
                                play(8, btn9);
                            }
                            else
                            {
                                play(7, btn8);
                            }
                        }
                        
                    }
                    break;
                case 8:
                    {
                        Button tempBtn = new Button();
                        int ndex = 0;
                        for (int i = 0; i < inputs.Length; i++)
                        {
                            if (inputs[i].Equals(MarkedType.none))
                            {
                                ndex = i;
                            }
                        }
                        myGrid.Children.Cast<Button>().ToList().ForEach(button =>
                        {
                            if (button.Content.ToString() == string.Empty)
                            {
                                tempBtn = button;
                            }
                        });
                        play(ndex, tempBtn);
                    }
                    break;
                default:
                    break;
            }


            void play(int dex, Button button)
            {
                inputs[dex] = MarkedType.O;
                button.Content = "O";
                button.Foreground = Brushes.Red;
                gameCounter++;
            }
        }

        public void checkGrid()
        {

            if (inputs[0] != MarkedType.none && inputs[0] == inputs[1] && inputs[1] == inputs[2])
            {

                gameEnded = true;

                btn1.Background = btn2.Background = btn3.Background = Brushes.Green;
            }
            if (inputs[3] != MarkedType.none && inputs[3] == inputs[4] && inputs[4] == inputs[5])
            {
                gameEnded = true;

                btn4.Background = btn5.Background = btn6.Background = Brushes.Green;
            }
            if (inputs[6] != MarkedType.none && inputs[6] == inputs[7] && inputs[7] == inputs[8])
            {
                gameEnded = true;

                btn7.Background = btn8.Background = btn9.Background = Brushes.Green;
            }


            if (inputs[0] != MarkedType.none && inputs[0] == inputs[3] && inputs[3] == inputs[6])
            {
                gameEnded = true;

                btn1.Background = btn4.Background = btn7.Background = Brushes.Green;
            }
            if (inputs[1] != MarkedType.none && inputs[1] == inputs[4] && inputs[4] == inputs[7])
            {
                gameEnded = true;

                btn2.Background = btn5.Background = btn8.Background = Brushes.Green;
            }
            if (inputs[2] != MarkedType.none && inputs[2] == inputs[5] && inputs[5] == inputs[8])
            {
                gameEnded = true;

                btn3.Background = btn6.Background = btn9.Background = Brushes.Green;
            }



            if (inputs[0] != MarkedType.none && inputs[0] == inputs[4] && inputs[4] == inputs[8])
            {
                gameEnded = true;

                btn1.Background = btn5.Background = btn9.Background = Brushes.Green;
            }
            if (inputs[2] != MarkedType.none && inputs[2] == inputs[4] && inputs[4] == inputs[6])
            {
                gameEnded = true;

                btn3.Background = btn5.Background = btn7.Background = Brushes.Green;
            }


            if (!inputs.Any(type => type == MarkedType.none))
            {
                gameEnded = true;

                myGrid.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    
                        button.Background = Brushes.Orange;
                    
                });
            }


        }

    }
}
