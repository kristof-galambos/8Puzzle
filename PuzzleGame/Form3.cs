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
    public partial class Form3 : Form
    {
        public Form1 form1;
        public Form2 form2;
        public Form3(Form1 f1)
        {
            form1 = f1;
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public Form3(Form2 f2)
        {
            Console.WriteLine("Form3 constructor called");
            form2 = f2;
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

    private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Console.WriteLine("Form3 closed");
            if (form1 != null)
            {
                form1.Show();
                form1.hideLabel();
            }
            else
            {
                form2.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Console.WriteLine("Form3 closed");
            if (form1 != null)
            {
                form1.Show();
                form1.hideLabel();
            }
            else
            {
                form2.Show();
            }
        }
    }
}
