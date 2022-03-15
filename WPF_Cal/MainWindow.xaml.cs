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

namespace WPF_Cal
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string tmp;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            //sender btn화
            Button btn = (Button)sender;

            //btn.Content.. ex) 1, 2, 3, +,- ,,, 
            tmp = btn.Content.ToString();

            while (true)
            {
                calResult.Text += tmp;

                break;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                string test = calResult.Text;

                if (test.Length > 0)
                {
                    calResult.Text = test.Substring(0, test.Length - 1);
                    break;
                }
                else
                    break;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            calResult.Text = null;
        }

        private void btnCal_Click(object sender, RoutedEventArgs e)
        {
            char[] textbox = calResult.Text.ToCharArray();
            float[] arrNum = new float[100];
            char[] arrOper = new char[100];
            int numCnt = 0;
            int opCnt = 0;
            int idx = 0;
            float param = 0.01f;
            bool contNum = false;
            bool isCheck = false;
            bool isFloat;


            for (int i = 0; i < textbox.Length; i++)
            {
                string check = arrNum[numCnt].ToString();

                if ((check.Contains(".")) || (isCheck == true))
                    isFloat = true;
                else
                    isFloat = false;

                if ((textbox[i] >= '0' && textbox[i] <= '9'))
                {
                    if (contNum && isFloat == false)
                    {
                        arrNum[numCnt] = (arrNum[numCnt] * 10) + float.Parse(textbox[i].ToString());
                    }

                    else if (contNum && isFloat == true)
                    {
                        arrNum[numCnt] = (arrNum[numCnt]) + (param * float.Parse(textbox[i].ToString()));
                        param = param * 0.1f;
                    }

                    else
                    {
                        arrNum[numCnt] = float.Parse(textbox[i].ToString());
                    }

                    contNum = true;
                }

                else if (textbox[i] == '.')
                {
                    check += '.';
                    if (textbox[i + 1] != '0')
                        arrNum[numCnt] = (arrNum[numCnt]) + (0.1f * float.Parse(textbox[i + 1].ToString()));
                    else
                        isCheck = true;

                    if ((textbox[i + 1] >= '0' && (textbox[i + 1] <= '9')))
                        contNum = true;
                    else
                        contNum = false;

                    i++;
                }

                else
                {
                    param = 0.01f;
                    numCnt++;
                    arrOper[opCnt++] = textbox[i];
                    contNum = false;
                    isCheck = false;
                }
            }

            for (int i = 0; i < arrOper.Length; i++)
            {
                if (arrOper[i] == '*' && arrOper[i + 1] == '-' && calResult.Text.Contains("*-"))
                {
                    arrNum[0] = -(arrNum[0] * arrNum[idx + 2]);

                    idx++;
                    i++;
                }

                else if (arrOper[i] == '/' && arrOper[i + 1] == '-' && calResult.Text.Contains("/-"))
                {
                    arrNum[0] = -(arrNum[0] / arrNum[idx + 2]);

                    idx++;
                    i++;
                }
                else
                {
                    if (arrOper[i] == '+')
                    {
                        arrNum[0] = (arrNum[0] + arrNum[idx + 1]);

                        idx++;
                    }

                    if(arrOper[i] == '-')
                    {
                        arrNum[0] = (arrNum[0] - arrNum[idx + 1]);

                        idx++;
                    }

                    if (arrOper[i] == '*')
                    {
                        arrNum[0] = (arrNum[0] * arrNum[idx + 1]);

                        idx++;
                    }

                    if (arrOper[i] == '/')
                    {
                        arrNum[0] = (arrNum[0] / arrNum[idx + 1]);

                        idx++;
                    }
                }

                calResult.Text = arrNum[0].ToString();
            }
        }
    }
}
