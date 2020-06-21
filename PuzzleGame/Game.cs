using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGame
{
    class Game
    {
        public int[,] game = new int[3, 3];
        public int[,] ideal = new int[3, 3];
        private Form2 form2;

        public Game(Form2 f2, string diff)
        {
            form2 = f2;
            initialize();
            //scramble(diff);
            //up();
            smartScramble(diff);
            printGame();
            updatePositions();
        }


        public void updatePositions()
        {
            int[,] positions = new int[9, 2];
            for (int i = 0; i < 9; i++)
            {
                int[] thisPos = new int[2];
                thisPos = find2d(game, i);
                positions[i, 0] = thisPos[1];
                positions[i, 1] = thisPos[0];
            }
            form2.updatePictures(positions);
        }


        private void initialize()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ideal[i, j] = i * 3 + j;
                    game[i, j] = i * 3 + j;
                }
            }
        }


        private void scramble(string diff)
        {
            int MOVES = 1;
            if (diff == "very easy")
            {
                MOVES = 2;
            }
            else if (diff == "easy")
            {
                MOVES = 7;
            }
            else if (diff == "medium")
            {
                MOVES = 15;
            }
            else if (diff == "difficult")
            {
                MOVES = 30;
            }
            else if (diff == "very difficult")
            {
                MOVES = 60;
            }
            int counter = 0;
            int i_previous = 100;
            Random rand = new Random();
            while (counter < MOVES)
            {
                int i = rand.Next(0, 4);
                if (i == 0)
                {
                    if (i_previous == 1)
                    {
                        continue; //don't make the opposite move that was made before
                    }
                    else if (find(row(game, 2), 0) == -1)
                    {
                        up();
                        counter++;
                        continue;
                    }
                }
                if (i == 1)
                {
                    if (i_previous == 0)
                    {
                        continue; //don't make the opposite move that was made before
                    }
                    else if (find(row(game, 0), 0) == -1)
                    {
                        down();
                        counter++;
                        continue;
                    }
                }
                if (i == 2)
                {
                    if (i_previous == 3)
                    {
                        continue; //don't make the opposite move that was made before
                    }
                    else if (find(column(game, 2), 0) == -1)
                    {
                        left();
                        counter++;
                        continue;
                    }
                }
                if (i == 3)
                {
                    if (i_previous == 2)
                    {
                        continue; //don't make the opposite move that was made before
                    }
                    else if (find(column(game, 0), 0) == -1)
                    {
                        right();
                        counter++;
                        continue;
                    }
                }
                i_previous = i;
            }
        }


        private void smartScramble(string diff)
        {
            int MOVES = 100;
            if (diff == "very easy")
            {
                MOVES = 2;
            }
            else if (diff == "easy")
            {
                MOVES = 3;
            }
            else if (diff == "medium")
            {
                MOVES = 5;
            }
            else if (diff == "difficult")
            {
                MOVES = 10;
            }
            else if (diff == "very difficult")
            {
                MOVES = 20;
            }
            scramble(diff);
            Ai ai = new Ai(game, ideal);
            string[] sol = ai.getSolution();
            try
            {
                if (sol[0] == "none")
                {
                    return;
                }
            }
            catch
            {
                int apple = 0;
            }
            int len = sol.Length;
            while (len < MOVES)
            {
                scramble("very easy");
                ai = new Ai(game, ideal);
                sol = ai.getSolution();
                try
                {
                    if (sol[0] == "none")
                    {
                        return;
                    }
                }
                catch
                {
                    int apple = 0;
                }
            }
        }


        public void up()
        {
            for (int i=0; i<3; i++)
            {
                //just for demonstration purposes, use Array.Exists instead of the find method
                if (Array.Exists(row(game, 2), element => element == 0)) {
                    Console.WriteLine("Tried to move up from bottom row");
                    break;
                }
                else
                {
                    int j = find(row(game, i), 0);
                    if (j != -1)
                    {
                        game[i, j] = game[i + 1, j];
                        game[i + 1, j] = 0;
                        break;
                    }
                }
            }
        }


        public void down()
        {
            for (int i = 0; i < 3; i++)
            {
                if (find(row(game, 0), 0) != -1)
                {
                    Console.WriteLine("Tried to move down from top row");
                    break;
                }
                else
                {
                    int j = find(row(game, i), 0);
                    if (j != -1)
                    {
                        game[i, j] = game[i - 1, j];
                        game[i - 1, j] = 0;
                        break;
                    }
                }
            }
        }


        public void left()
        {
            for (int i = 0; i < 3; i++)
            {
                if (find(column(game, 2), 0) != -1)
                {
                    Console.WriteLine("Tried to move left from right column");
                    break;
                }
                else
                {
                    int j = find(row(game, i), 0);
                    if (j != -1)
                    {
                        game[i, j] = game[i, j + 1];
                        game[i, j + 1] = 0;
                        break;
                    }
                }
            }
        }


        public void right()
        {
            for (int i = 0; i < 3; i++)
            {
                if (find(column(game, 0), 0) != -1)
                {
                    Console.WriteLine("Tried to move right from left column");
                    break;
                }
                else
                {
                    int j = find(row(game, i), 0);
                    if (j != -1)
                    {
                        game[i, j] = game[i, j - 1];
                        game[i, j - 1] = 0;
                        break;
                    }
                }
            }
        }


        public void printGame()
        {
            Console.Write("[");
            for (int i=0; i<3; i++)
            {
                Console.Write(game[i, 0] + " " + game[i, 1] + " " + game[i, 2]);
                if (i!=2)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("]");
        }


        private int find(int[] arr, int element)
        {
            int result = -1;
            for (int i=0; i<arr.Length; i++)
            {
                if (arr[i] == element)
                {
                    result = i;
                }
            }
            return result;
        }


        private int[] find2d(int[,] arr, int element)
        {
            int[] result = new int[2] { -1, -1 };
            for (int i=0; i<3; i++)
            {
                for (int j=0; j<3; j++)
                {
                    if (arr[i, j] == element)
                    {
                        result[0] = i;
                        result[1] = j;
                    }
                }
            }
            return result;
        }


        public bool endGame()
        {
            bool result = true;
            for (int i=0; i<3; i++)
            {
                for (int j=0; j<3; j++)
                {
                    if (game[i, j] != ideal[i, j])
                    {
                        result = false;
                    }
                }
            }
            return result;
        }


        public static T[] row<T>(T[,] arr, int rowWanted)
        {
            T[] row = new T[3];
            for (int i = 0; i < 3; i++)
            {
                row[i] = arr[rowWanted, i];
            }
            return row;
        }


        public static T[] column<T>(T[,] arr, int columnWanted)
        {
            T[] col = new T[3];
            for (int i=0; i<3; i++)
            {
                col[i] = arr[i, columnWanted];
            }
            return col;
        }
    }
}

