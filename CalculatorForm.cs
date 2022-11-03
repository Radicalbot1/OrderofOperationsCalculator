using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ObjectOrientedCalculator
{
    public partial class CalcForm : Form
    {
        char[] totalOperands = { '^', '*', '/', '+', '-'};
        bool finishedCalc = false;
        List<string> history;
        public CalcForm()
        {
            InitializeComponent();
            history = new();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                buttonEnter_Click(sender, e);
            }

            e.Handled = false;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            string equation = textBox1.Text;
            richTextBox1.Text += textBox1.Text + " = ";
            
            Operation operation = new();
            string output = operation.CalcSolve(textBox1.Text);

            double result = 0;
            if (!double.TryParse(output, out result))
            {
                richTextBox1.Text += output + "\n";
                return;
            }

            history.Add($"{textBox1.Text} = {result}");
            richTextBox1.Text += $"{result}\n";
            textBox1.Text = $"{result}";
            finishedCalc = true;
        }

        private void StringButtonClick(object sender, EventArgs e)
        {
            if(sender != null && sender is Button)
            {
                string sent = sender.ToString().Remove(0, sender.ToString().Length - 2).Trim();

                if (totalOperands.Contains(sent[0]))
                {
                    sent = " " + sent + " ";
                }

                textBox1.Text += sent;

                if (finishedCalc)
                {
                    string inText = textBox1.Text.Replace(" ", "");

                    if (!totalOperands.Contains(inText[inText.Length - 1]))
                    {
                        textBox1.Text = sent;
                    }

                    finishedCalc = false;
                }
            }
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            if (finishedCalc)
                textBox1.Text = "-";
            else
                textBox1.Text += "-";

            finishedCalc = false;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length > 0)
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
        }

        private void buttonSQRT_Click(object sender, EventArgs e)
        {
            textBox1.Text += " ^ 0.5 ";
        }

        private void buttonSquare_Click(object sender, EventArgs e)
        {
            textBox1.Text += " ^ 2 ";
        }

        private void buttonPower_Click(object sender, EventArgs e)
        {
            textBox1.Text += " ^ ";
        }

        private void buttonInverse_Click(object sender, EventArgs e)
        {
            textBox1.Text += " ^ -1 ";
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            richTextBox1.Text = "";
        }
    }
}