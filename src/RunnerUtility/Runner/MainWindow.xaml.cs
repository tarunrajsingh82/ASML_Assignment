using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using SequenceAnalysis;
using SumOfMultiple;

namespace Runner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void exBtn_Click(object sender, RoutedEventArgs e)
        {
            TxtOutput.Text = "";            

            if ((bool)RdBtnSum.IsChecked)
            {

                SumUtil SumUtil = new SumUtil();

                int limit = int.MinValue;
                if(!String.IsNullOrEmpty(TxtBoxLimit.Text) &&
                    int.TryParse(TxtBoxLimit.Text.ToString(),out limit))
                {
                    if(limit<0)
                    {
                        TxtOutput.Text = "Please enter a positive limit.";
                    }
                    else
                    {
                        var technique = Technique.USING_MATHEMATICAL_FORMULA;

                        if ((bool)RdBtnForLoop.IsChecked)
                            technique = Technique.USING_FORLOOP;
                        if ((bool)RdBtnLinq.IsChecked)
                            technique = Technique.USING_LINQ;

                        string time= "";
                        long sum =SumUtil.CalculateSum(limit, (int)technique, out time);

                        if (sum != (int)ERRORCODE.INVALID_OPEARTION)
                        {
                            TxtOutput.Text = "Sum of multiples of 3 and 5 below " + limit + " is : " + sum + "\n"
                                + "Execution Time: " + time + " ms";
                        }
                        else
                        {
                            TxtOutput.Text= "Error occured with errorcode: " + (int)ERRORCODE.INVALID_OPEARTION ;
                        }
                    } 
                }
                else
                {
                    TxtOutput.Text = "Please enter a valid limit.";
                }
            }

            if ((bool)RdBtnSeq.IsChecked)
            {

                var seqFinder = new SequenceAnalysisUtil();

                //check if input only contains letters
                if (!String.IsNullOrEmpty(InputSeqTxtBox.Text) &&
                    Regex.IsMatch(InputSeqTxtBox.Text, @"^[a-zA-Z]+$"))
                {
                    string inputStr = InputSeqTxtBox.Text;

                    var technique = SeqTechnique.USING_HASHING;

                    if ((bool)RdBtnMerge.IsChecked)
                        technique = SeqTechnique.USING_MERGESORT;
                    if ((bool)RdBtnLINQSeq.IsChecked)
                        technique = SeqTechnique.USING_LINQ;

                    string time = "";
                     var result= seqFinder.FindCapSequence(inputStr, (int)technique, out time);

                        if (result != "ERROR")
                        {
                            TxtOutput.Text = result + "\n"
                                + "Execution Time: " + time + " ms";
                        }
                        else
                        {
                            TxtOutput.Text = "Error occured";
                        }
                    
                }
                else
                {
                    TxtOutput.Text = "Please enter a character only sequence.";
                }
            }
        }
    }
}
