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

        public static Queue<string> InfixToRPN(string token)
        {
            #region valuesForRPN
            List<SignsAndPriorities> listOfSigns = new List<SignsAndPriorities>
            {
                new SignsAndPriorities('+', 1, true),
                new SignsAndPriorities('-', 1, true),
                new SignsAndPriorities('x', 2, true),
                new SignsAndPriorities('/', 2, true),
                new SignsAndPriorities('%', 3, true),
                new SignsAndPriorities('^', 3, false),
            };


            SignsAndPriorities leftBracket = new SignsAndPriorities('(', 0, true);
            SignsAndPriorities rightBrcket = new SignsAndPriorities(')', 1, true);

            List<char> listForNumbers = new List<char>
            {
                '0','1','2','3','4','5','6','7','8','9',','
            };

            Stack<SignsAndPriorities> stackOfSigns = new Stack<SignsAndPriorities>();
            Queue<string> outputQueue = new Queue<string>();

            string tmpNumber = "";

            token += "=";

            #endregion

            for (int i = 0; i < token.Length; i++)
            {

                if (listForNumbers.Contains(token[i]))
                {
                    tmpNumber += token[i];
                }
                if (!listForNumbers.Contains(token[i]) && token[i] != leftBracket.Sign && token[i] != rightBrcket.Sign)
                {
                    outputQueue.Enqueue(tmpNumber);
                    tmpNumber = "";
                }
                if (token[i] == leftBracket.Sign)
                {
                    stackOfSigns.Push(leftBracket);
                }
                if (token[i] == rightBrcket.Sign)
                {
                    while (stackOfSigns.Count > 0)
                    {
                        SignsAndPriorities tmpSign = stackOfSigns.Pop();
                        if (tmpSign.Sign != leftBracket.Sign)
                        {
                            outputQueue.Enqueue(tmpSign.Sign.ToString());
                        }
                        else if (tmpSign.Sign == leftBracket.Sign)
                        {
                            break;
                        }
                    }
                }
                else if (token[i] != 'P')
                {
                    foreach (var item in listOfSigns)
                    {
                        if (item.Sign == token[i])
                        {
                            while (stackOfSigns.Count > 0)
                            {
                                SignsAndPriorities tmpSign = stackOfSigns.Pop();
                                if ((item.Priority <= tmpSign.Priority && tmpSign.IsLeftSided) || (item.Priority < tmpSign.Priority && tmpSign.IsLeftSided))
                                {
                                    outputQueue.Enqueue(tmpSign.Sign.ToString());
                                }
                                else
                                {
                                    stackOfSigns.Push(tmpSign);
                                    break;
                                }
                            }
                            stackOfSigns.Push(item);
                            break;
                        }
                    }
                }
                else
                {
                    outputQueue.Enqueue(token[i].ToString());
                }
                if (token[i] == '=')
                {
                    while (stackOfSigns.Count > 0)
                    {
                        outputQueue.Enqueue(stackOfSigns.Pop().Sign.ToString());
                    }
                }
            }
            return outputQueue;
        }

        public static string CalculateRPN(Queue<string> outputQueue)
        {


            string result = "";

            Stack<string> stackOfNumbers = new Stack<string>();

            float a = 0;
            float b = 0;
            float tmpNumber = 0;
            string tmpString = "";

            try
            {
                foreach (string item in outputQueue)
                {
                    if (float.TryParse(item, out tmpNumber))
                    {
                        stackOfNumbers.Push(item);
                        tmpNumber = 0;
                    }
                    if (item == "P")
                    {
                        stackOfNumbers.Push("3,14");
                    }
                    switch (item)
                    {
                        case "+":
                            a = float.Parse(stackOfNumbers.Pop());
                            b = float.Parse(stackOfNumbers.Pop());
                            tmpNumber = b + a;
                            tmpString = tmpNumber.ToString();
                            stackOfNumbers.Push(tmpString);
                            tmpString = "";
                            break;
                        case "-":
                            a = float.Parse(stackOfNumbers.Pop());
                            b = float.Parse(stackOfNumbers.Pop());
                            tmpNumber = b - a;
                            tmpString = tmpNumber.ToString();
                            stackOfNumbers.Push(tmpString);
                            break;
                        case "x":
                            a = float.Parse(stackOfNumbers.Pop());
                            b = float.Parse(stackOfNumbers.Pop());
                            tmpNumber = b * a;
                            tmpString = tmpNumber.ToString();
                            stackOfNumbers.Push(tmpString);
                            break;
                        case "/":
                            a = float.Parse(stackOfNumbers.Pop());
                            b = float.Parse(stackOfNumbers.Pop());
                            tmpNumber = b / a;
                            tmpString = tmpNumber.ToString();
                            stackOfNumbers.Push(tmpString);
                            break;
                        case "%":
                            a = float.Parse(stackOfNumbers.Pop());
                            b = float.Parse(stackOfNumbers.Pop());
                            tmpNumber = b % a;
                            tmpString = tmpNumber.ToString();
                            stackOfNumbers.Push(tmpString);
                            break;
                        default:
                            break;
                            //return "Error";
                    }
                }
                return stackOfNumbers.Pop();
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
            return result;
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
            labelResult.Content = CalculateRPN(InfixToRPN(labelResult.Content.ToString()));
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

        private void ButtonPi_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "P";
            }
            else
            {
                labelResult.Content += "P";
            }
        }

        private void ButtonModulo_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "%";
            }
            else
            {
                labelResult.Content += "%";
            }
        }

        private void ButtonLeftBracket_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = "(";
            }
            else
            {
                labelResult.Content += "(";
            }
        }

        private void ButtonRightBracket_Click(object sender, RoutedEventArgs e)
        {
            if (labelResult.Content.ToString() == "0")
            {
                labelResult.Content = ")";
            }
            else
            {
                labelResult.Content += ")";
            }
        }

        private void ButtonBinaryConverter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    #endregion
}
