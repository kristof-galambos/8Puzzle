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
    public partial class Form2 : Form
    {
        private int XSIZE = 93;
        private int YSIZE = 134;
        private int XOFFSET = 20;
        private int YOFFSET = 20;
        private Game game;
        private bool gameOver = false;
        private Form1 form1;
        private bool want_ai;


        public Form2(Form1 f1, string diff, bool ai)
        {
            form1 = f1;
            want_ai = ai;
            InitializeComponent();
            setPictureBoxSize();
            setPictureBoxMode();
            game = new Game(this, diff);
        }

        //private void handleInput(object sender, EventArgs e)
        //{
        //    if (gameOver)
        //    {
        //        label1.Text = "Congratulations, you have\nsolved the game!";
        //        label1.Visible = true;
        //        if (Input.KeyPress(Keys.Enter))
        //        {
        //            this.Hide();
        //            form1.Show();
        //        }
        //    }
        //    else
        //    {
        //        bool moved = false;
        //        if (Input.KeyPress(Keys.Right))
        //        {
        //            game.right();
        //            moved = true;
        //        }
        //        else if (Input.KeyPress(Keys.Left))
        //        {
        //            game.left();
        //            moved = true;
        //        }
        //        else if (Input.KeyPress(Keys.Up))
        //        {
        //            game.up();
        //            moved = true;
        //        }
        //        else if (Input.KeyPress(Keys.Down))
        //        {
        //            game.down();
        //            moved = true;
        //        }

        //        if (moved)
        //        {
        //            game.printGame();
        //            game.updatePositions();
        //            if (want_ai)
        //            {
        //                updateAILabels();
        //            }
        //        }
        //    }
        //    if (game.endGame())
        //    {
        //        gameOver = true;
        //    }
        //    if (Input.KeyPress(Keys.S))
        //    {
        //        Form3 form3 = new Form3(this);
        //        this.Hide();
        //        form3.Show();
        //    }
        //}

        private void updateAILabels()
        {
            //Console.WriteLine("Before:");
            //game.printGame();
            int[,] newGame = new int[3, 3];
            int[,] newIdeal = new int[3, 3];
            for (int i=0; i<3; i++)
            {
                for (int j=0; j<3; j++)
                {
                    newGame[i, j] = game.game[i, j];
                    newIdeal[i, j] = game.ideal[i, j];
                }
            }
            Ai ai = new Ai(newGame, newIdeal);
            string[] moves = ai.getSolution();
            try
            {
                if (moves[0] == "none")
                {
                    label3.Text = "AI can't solve the game,\nit's too hard.";
                    label4.Visible = false;
                }
                else
                {
                    label3.Text = "AI can solve the game in " + moves.Length.ToString() + " moves:";
                    label4.Text = String.Join("\n", moves);
                    label4.Visible = true;
                }
                label3.Visible = true;
                //Console.WriteLine("After:");
                //game.printGame();
            }
            catch
            {
                int apple = 0;
            }
        }

        private void setPictureBoxMode()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void setPictureBoxSize()
        {
            int SPACING = 2;
            pictureBox1.Size = new System.Drawing.Size(XSIZE - SPACING, YSIZE - SPACING);
            pictureBox2.Size = new System.Drawing.Size(XSIZE - SPACING, YSIZE - SPACING);
            pictureBox3.Size = new System.Drawing.Size(XSIZE - SPACING, YSIZE - SPACING);
            pictureBox4.Size = new System.Drawing.Size(XSIZE - SPACING, YSIZE - SPACING);
            pictureBox5.Size = new System.Drawing.Size(XSIZE - SPACING, YSIZE - SPACING);
            pictureBox6.Size = new System.Drawing.Size(XSIZE - SPACING, YSIZE - SPACING);
            pictureBox7.Size = new System.Drawing.Size(XSIZE - SPACING, YSIZE - SPACING);
            pictureBox8.Size = new System.Drawing.Size(XSIZE - SPACING, YSIZE - SPACING);
            pictureBox9.Size = new System.Drawing.Size(XSIZE - SPACING, YSIZE - SPACING);
        }

        public void updatePictures(int[,] positions)
        {
            pictureBox1.Location = new Point(positions[0, 0] * XSIZE + XOFFSET, positions[0, 1] * YSIZE + YOFFSET);
            pictureBox2.Location = new Point(positions[1, 0] * XSIZE + XOFFSET, positions[1, 1] * YSIZE + YOFFSET);
            pictureBox3.Location = new Point(positions[2, 0] * XSIZE + XOFFSET, positions[2, 1] * YSIZE + YOFFSET);
            pictureBox4.Location = new Point(positions[3, 0] * XSIZE + XOFFSET, positions[3, 1] * YSIZE + YOFFSET);
            pictureBox5.Location = new Point(positions[4, 0] * XSIZE + XOFFSET, positions[4, 1] * YSIZE + YOFFSET);
            pictureBox6.Location = new Point(positions[5, 0] * XSIZE + XOFFSET, positions[5, 1] * YSIZE + YOFFSET);
            pictureBox7.Location = new Point(positions[6, 0] * XSIZE + XOFFSET, positions[6, 1] * YSIZE + YOFFSET);
            pictureBox8.Location = new Point(positions[7, 0] * XSIZE + XOFFSET, positions[7, 1] * YSIZE + YOFFSET);
            pictureBox9.Location = new Point(positions[8, 0] * XSIZE + XOFFSET, positions[8, 1] * YSIZE + YOFFSET);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            this.Hide();
            form3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            game.up();
            game.printGame();
            game.updatePositions();
            if (want_ai)
            {
                updateAILabels();
            }
            if (game.endGame())
            {
                label1.Text = "Congratulations, you have\nsolved the game!";
                label1.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            game.down();
            game.printGame();
            game.updatePositions();
            if (want_ai)
            {
                updateAILabels();
            }
            if (game.endGame())
            {
                label1.Text = "Congratulations, you have\nsolved the game!";
                label1.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            game.left();
            game.printGame();
            game.updatePositions();
            if (want_ai)
            {
                updateAILabels();
            }
            if (game.endGame())
            {
                label1.Text = "Congratulations, you have\nsolved the game!";
                label1.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            game.right();
            game.printGame();
            game.updatePositions();
            if (want_ai)
            {
                updateAILabels();
            }
            if (game.endGame())
            {
                label1.Text = "Congratulations, you have\nsolved the game!";
                label1.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            form1.Show();
            form1.hideLabel();
        }
    }
}
