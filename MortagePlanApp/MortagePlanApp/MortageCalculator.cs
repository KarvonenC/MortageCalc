using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MortagePlanApp
{
    public partial class MortageCalculator : Form
    {
        //Lists
        public static List<string> textList = new List<string>();
        public static List<string> customerList = new List<string>();
        public static List<string> totalList = new List<string>();
        public static List<string> interestList = new List<string>();
        public static List<string> yearsList = new List<string>();

        //Length of prospectList
        public static int prospectListLength;

        //OpenFileDialog
        OpenFileDialog ofd = new OpenFileDialog();
        public MortageCalculator()
        {
            InitializeComponent();       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog filter
            ofd.Filter = "TXT|*.txt";
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Gets file path and prints it to textBox1
                textBox1.Text = ofd.FileName;
                //Gets file name and prints it to textBox2
                textBox2.Text = ofd.SafeFileName;
                //Reads file content into richTextBox1
                richTextBox1.Text = File.ReadAllText(ofd.FileName);
            }
            //Gets file path
            string path = textBox1.Text = ofd.FileName;
            //Reads file from path 
            using (StreamReader sr = new StreamReader(path))
            {
                //Skips (headers) first line of .txt file
                string headerLine = sr.ReadLine();
                //Rest of the lines
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //Parsing and regexing file content
                    string splitLine;
                    string regex;
                    regex = Regex.Replace(line, @",(?=[^""]*""(?:[^""]*""[^""]*"")*[^""]*$)", " ");
                    splitLine = regex.Replace("\"", "");
                    string[] split = splitLine.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    //Adds each value to a List<String>
                    foreach (string value in split)
                    {
                        textList.Add(value);
                    }
                }
                //Close streamreader
                sr.Close();
            }
            //Removes last extra element of the textList
            textList.Remove(".");
            //Prints textList to richTextBox3
            richTextBox3.Text = "";
            foreach (string item in textList)
            {
                richTextBox3.Text += item + " \n";
            }
            //Goes through the textList and adds each element based on index to it's own list
            for (int i = 0; i < textList.Count; i += 4)
            {
                customerList.Add(textList[i + 0]);
                totalList.Add(textList[i + 1]);
                interestList.Add(textList[i + 2]);
                yearsList.Add(textList[i + 3]);
            }
            
        }
        //Mortage calculator function
        private void MortageCalc(int x, string n, double t, double i, int y)
        {
            //Total Loan
            double U = t;
            //Interest on a monthly basis
            decimal decimalInterest = Convert.ToDecimal(i) / 100;
            double b = (double)(decimalInterest / 12);
            //Number of payments
            int p = y * 12;
            //Fixed monthly payment
            double E;
            //Calculate mortgage | Formula: E = U[b(1 + b) ^ p] /[(1 + b) ^ p - 1]
            double onePlusB = 1 + b;
            double firstPart = U * Exponent(onePlusB, p) * b;
            double secondPart = Exponent(onePlusB, p) - 1;
            E = (firstPart / secondPart);
            //Format fixed monthly payment with 2 decimals
            string m = $"{E:0.00}";
            //Prints prospects and their respective information to richTextBox2
            richTextBox2.AppendText($"Prospect {x}: {n} wants to borrow {t} € for a period {y} years and pay {m} € each month" + "\n");
        }
        //Exponent function
        public static double Exponent(double num, int exponent)
        {
            double result = 1;

            if (exponent > 0)
            {
                for (int i = 1; i <= exponent; ++i)
                {
                    result *= num;
                }
            }
            return result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Changing some list types to double and int
            List<double> totalDoubleList = totalList.Select(i => double.Parse(i, CultureInfo.InvariantCulture)).ToList();
            List<double> interestDoubleList = interestList.Select(i => double.Parse(i, CultureInfo.InvariantCulture)).ToList();
            List<int> yearsIntList = yearsList.ConvertAll(int.Parse);
            //Prospect class list
            List<Prospects> Prospect = new List<Prospects>();
            //Add new prospects
            for (int p = 0; p < customerList.Count; p++)
            {
                Prospect.Add(new Prospects(customerList[p], totalDoubleList[p], interestDoubleList[p], yearsIntList[p]));
            }
            //Counts amount of Prospects
            prospectListLength = Prospect.Count;
            //MortgageCalc function doing calculations for every Prospect
            int x = 0;
            while (x < prospectListLength)
            {
                int xPlusOne = x + 1;
                MortageCalc(xPlusOne, Prospect[x].Name, Prospect[x].Total, Prospect[x].Interest, Prospect[x].Years);
                x++;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
