using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        struct SignsAndPriorities
        {
            public SignsAndPriorities(char sign, byte priority, bool isLeftSided)
            {
                Sign = sign;
                Priority = priority;
                IsLeftSided = isLeftSided;
            }
            public char Sign { get; set; }
            public byte Priority { get; set; }
            public bool IsLeftSided { get; set; }
        }

        public static string InfixToRPN(string result)
        {
            List<SignsAndPriorities> listOfSigns = new List<SignsAndPriorities>
            {
                new SignsAndPriorities('+', 1, true),
                new SignsAndPriorities('-', 1, true),
                new SignsAndPriorities('x', 2, true),
                new SignsAndPriorities('/', 2, true),
                new SignsAndPriorities('%', 2, true),
                new SignsAndPriorities('^', 3, false),
            };

            SignsAndPriorities leftBracket = new SignsAndPriorities('(', 0, true);

            SignsAndPriorities rightBrcket = new SignsAndPriorities(')', 0, true);

            List<char> listForNumbers = new List<char>
            {
                '0','1','2','3','4','5','6','7','8','9',','
            };

            Stack<SignsAndPriorities> stackOfSigns = new Stack<SignsAndPriorities>();

            Stack<string> stackOut = new Stack<string>();

            string tmpNumber = "";

            string newResult = "";

            for (int i = 0; i < result.Length; i++)
            {
                if (listForNumbers.Contains(result[i]))
                {
                    tmpNumber += result[i];
                }
                else if(!listForNumbers.Contains(result[i]))
                {
                    stackOut.Push(tmpNumber);
                }
                else if (result[i] == leftBracket.Sign)
                {
                    stackOfSigns.Push(leftBracket);
                }
                else if (result[i] == rightBrcket.Sign)
                {
                    foreach (var item in stackOfSigns)
                    {
                        if (item.Sign != leftBracket.Sign)
                        {
                            stackOut.Push(stackOfSigns.Pop().Sign.ToString());
                        }
                        if (item.Sign == leftBracket.Sign)
                        {
                            stackOfSigns.Pop();
                            break;
                        }
                    }
                }
                foreach (var item in listOfSigns)
                {
                    if (item.Sign == result[i])
                    {
                        foreach (var cell in stackOfSigns)
                        {
                            if(cell.Priority <= item.Priority)
                            {
                                stackOut.Push(stackOfSigns.Pop().Sign.ToString());
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            for (int z = 0; z < stackOut.Count; z++)
            {
                newResult += $"{stackOut.Pop()}";
            }
            return newResult;
        }

        #region BUttonHandlers
        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "7";
            }
            else
            {
                labelResult.Content += "7";
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)

        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "4";
            }
            else
            {
                labelResult.Content += "4";
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "1";
            }
            else
            {
                labelResult.Content += "1";
            }
        }

        private void ButtonDot_Click(object sender, RoutedEventArgs e)
        {
            labelResult.Content += ",";
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "8";
            }
            else
            {
                labelResult.Content += "8";
            }
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "5";
            }
            else
            {
                labelResult.Content += "5";
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "2";
            }
            else
            {
                labelResult.Content += "2";
            }
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "0";
            }
            else
            {
                labelResult.Content += "0";
            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var result = labelResult.Content.ToString();
            if (result.Length < 2)
            {
                labelResult.Content = 0;
            }
            else
                labelResult.Content = result.Remove(result.Length - 1);
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "9";
            }
            else
            {
                labelResult.Content += "9";
            }
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "6";
            }
            else
            {
                labelResult.Content += "6";
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "3";
            }
            else
            {
                labelResult.Content += "3";
            }
        }

        private void ButtonEqual_Click(object sender, RoutedEventArgs e)
        {
            labelResult.Content = InfixToRPN(labelResult.Content.ToString());
        }

        private void buttonDivide_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "/";
            }
            else
            {
                labelResult.Content += "/";
            }
        }

        private void ButtonTimes_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "x";
            }
            else
            {
                labelResult.Content += "x";
            }
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "-";
            }
            else
            {
                labelResult.Content += "-";
            }
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "+";
            }
            else
            {
                labelResult.Content += "+";
            }
        }

        private void ButtonAC_Click(object sender, RoutedEventArgs e)
        {

            labelResult.Content = "0";

        }

        private void buttonRepo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/KacperCedro/PublicProjects",
                UseShellExecute = true
            });
        }
    }
    #endregion
}
