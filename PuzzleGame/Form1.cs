using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string getDifficulty()
        {
            string diff = "undefined";
            if (radioButton3.Checked)
            {
                diff = "very easy";
            }
            else if (radioButton4.Checked)
            {
                diff = "easy";
            }
            else if (radioButton5.Checked)
            {
                diff = "medium";
            }
            else if (radioButton6.Checked)
            {
                diff = "difficult";
            }
            else if (radioButton7.Checked)
            {
                diff = "very difficult";
            }
            return diff;
        }

        private bool getAI()
        {
            bool ai = false;
            if (radioButton1.Checked)
            {
                ai = true;
            }
            return ai;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            this.Hide();
            form3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string diff = getDifficulty();
            bool ai = getAI();
            Form2 form2 = new Form2(this, diff, ai);
            this.Hide();
            form2.Show();
        }
    }
}
