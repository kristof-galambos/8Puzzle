using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGame
{
    class Ai
    {
        private int[,] start;
        private int[,] ideal;
        private bool solution_found = true;

        public Ai(int[,] st, int[,] id)
        {
            start = st;
            ideal = id;
        }

        public string[] getSolution()
        {
            //string[] mockSolution = new string[] { "up", "down" };
            //return mockSolution;

            List<int[,]> solution = new List<int[,]>();
            solution = bfs(start, ideal);
            Console.WriteLine("solution.Count = {0}", solution.Count);
            if (!solution_found)
            {
                string[] route_text = new string[] { "none" };
                return route_text;
            }
            else
            {
                string[] route_text = new string[solution.Count - 1];

                for (int i=0; i<solution.Count-1; i++)
                {
                    int[,] UP = deepcopy(up(solution[i]));
                    if (equals(solution[i+1], UP))
                    {
                        route_text[i] = "up";
                    }
                    int[,] DOWN = deepcopy(down(solution[i]));
                    if (equals(solution[i + 1], DOWN))
                    {
                        route_text[i] = "down";
                    }
                    int[,] LEFT = deepcopy(left(solution[i]));
                    if (equals(solution[i + 1], LEFT))
                    {
                        route_text[i] = "left";
                    }
                    int[,] RIGHT = deepcopy(right(solution[i]));
                    if (equals(solution[i + 1], RIGHT))
                    {
                        route_text[i] = "right";
                    }
                }
                return route_text;
            }

        }

        private List<int[,]> bfs(int[,] initial, int[,] goal)
        {
            List<List<int[,]>> frontier = new List<List<int[,]>>();
            List<int[,]> init = new List<int[,]>();
            init.Add(initial);
            frontier.Add(init);
            List<int[,]> explored = new List<int[,]>();

            int counter = 0;
            while (frontier.Any())
            {
                List<int[,]> route = new List<int[,]>();
                route = frontier[0];
                frontier.Remove(frontier[0]);
                int[,] node = route[route.Count - 1];

                //printState(node);
                //Console.WriteLine(frontier.Any());
                //int[,] test = new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
                //Console.WriteLine(equals(test, goal));
                if (equals(node, goal))
                {
                    Console.WriteLine("solution found!");
                    printRoute(route);
                    return route;
                }
                else if (find(explored, node) == -1)
                {
                    List<int[,]> moves = new List<int[,]>();
                    if (find(row(node, 2), 0) == -1)
                    {
                        moves.Add(up(deepcopy(node)));
                        //Console.WriteLine("adding up");
                    }
                    if (find(row(node, 0), 0) == -1)
                    {
                        moves.Add(down(deepcopy(node)));
                        //Console.WriteLine("adding down");
                    }
                    if (find(column(node, 2), 0) == -1)
                    {
                        moves.Add(left(deepcopy(node)));
                        //Console.WriteLine("adding left");
                    }
                    if (find(column(node, 0), 0) == -1)
                    {
                        moves.Add(right(deepcopy(node)));
                        //Console.WriteLine("adding right");
                    }
                    //printRoute(moves);
                    //Console.WriteLine(moves.Count);

                    ////testing the deepcopy function
                    //List<int[,]> ro = new List<int[,]>();
                    //int[,] st = new int[,] { { 1, 0, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
                    //ro.Add(st);
                    //List<List<int[,]>> fr = new List<List<int[,]>>();
                    //fr.Add(deepcopy(ro));
                    //ro[0][0, 0] = 10;
                    //Console.WriteLine("TESTING COMING:");
                    //printRoute(fr[0]);

                    foreach (int[,] move in moves)
                    {
                        //Console.WriteLine("Move coming:");
                        //printState(move);
                        List<int[,]> newRoute = deepcopy(route);
                        newRoute.Add(deepcopy(move));
                        frontier.Add(deepcopy(newRoute));
                        //Console.WriteLine("frontier[0]=");
                        //printRoute(frontier[0]);
                    }
                    explored.Add(node);
                    //Console.WriteLine(frontier.Count);
                    //foreach (List<int[,]> r in frontier)
                    //{
                    //    printRoute(r);
                    //}
                    //if (frontier.Count == 4)
                    //{
                    //    break;
                    //}
                }
                counter++;
                if (counter == 10000)
                {
                    Console.WriteLine("Couldn't find a solution after 10000 steps.");
                    Console.WriteLine("AI exiting...");
                    solution_found = false;
                    return route;
                }
            }
            return init;
        }


        public int[,] up(int [,] state1)
        {
            int[,] state = new int[3, 3];
            for (int i=0; i<3; i++)
            {
                for (int j=0; j<3; j++)
                {
                    state[i, j] = state1[i, j];
                }
            }
            //Console.WriteLine("BEFORE:");
            //printState(state);
            for (int i = 0; i < 3; i++)
            {
                //just for demonstration purposes, use Array.Exists instead of the find method
                if (Array.Exists(row(state, 2), element => element == 0))
                {
                    Console.WriteLine("Tried to move up from bottom row");
                    break;
                }
                else
                {
                    int j = find(row(state, i), 0);
                    if (j != -1)
                    {
                        state[i, j] = state[i + 1, j];
                        state[i + 1, j] = 0;
                        break;
                    }
                }
            }
            //Console.WriteLine("AFTER:");
            //printState(state);
            return state;
        }


        public int[,] down(int[,] state1)
        {
            int[,] state = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    state[i, j] = state1[i, j];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (find(row(state, 0), 0) != -1)
                {
                    Console.WriteLine("Tried to move down from top row");
                    break;
                }
                else
                {
                    int j = find(row(state, i), 0);
                    if (j != -1)
                    {
                        state[i, j] = state[i - 1, j];
                        state[i - 1, j] = 0;
                        break;
                    }
                }
            }
            return state;
        }


        public int[,] left(int[,] state1)
        {
            int[,] state = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    state[i, j] = state1[i, j];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (find(column(state, 2), 0) != -1)
                {
                    Console.WriteLine("Tried to move left from right column");
                    break;
                }
                else
                {
                    int j = find(row(state, i), 0);
                    if (j != -1)
                    {
                        state[i, j] = state[i, j + 1];
                        state[i, j + 1] = 0;
                        break;
                    }
                }
            }
            return state;
        }


        public int[,] right(int[,] state1)
        {
            int[,] state = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    state[i, j] = state1[i, j];
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (find(column(state, 0), 0) != -1)
                {
                    Console.WriteLine("Tried to move right from left column");
                    break;
                }
                else
                {
                    int j = find(row(state, i), 0);
                    if (j != -1)
                    {
                        state[i, j] = state[i, j - 1];
                        state[i, j - 1] = 0;
                        break;
                    }
                }
            }
            return state;
        }


        private void printState(int[,] state)
        {
            Console.Write("[");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(state[i, 0] + " " + state[i, 1] + " " + state[i, 2]);
                if (i != 2)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("]");
        }


        private void printRoute(List<int[,]> route)
        {
            Console.WriteLine("Route being printed:");
            foreach (int[,] state in route)
            {
                printState(state);
            }
        }


        private int[,] deepcopy(int[,] node)
        {
            int[,] deep = new int[3, 3];
            for (int i=0; i<3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    deep[i, j] = node[i, j];
                }
            }
            return deep;
        }


        private List<int[,]> deepcopy(List<int[,]> route)
        {
            List<int[,]> deep = new List<int[,]>();
            for (int i=0; i<route.Count; i++)
            {
                int[,] node = new int[3, 3];
                for (int j=0; j<3; j++)
                {
                    for (int k=0; k<3; k++)
                    {
                        node[j, k] = route[i][j, k];
                    }
                }
                deep.Add(node);
            }
            return deep;
        }


        private int find(int[] arr, int element)
        {
            int result = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == element)
                {
                    result = i;
                }
            }
            return result;
        }


        private int find(List<int[,]> arr, int[,] element)
        {
            for (int i=0; i<arr.Count; i++)
            {
                if (arr[i] == element)
                {
                    return i;
                }
            }
            return -1;
        }


        private bool equals(int[,] arr1, int[,] arr2)
        {
            bool result = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr1[i, j] != arr2[i, j])
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
            for (int i = 0; i < 3; i++)
            {
                col[i] = arr[i, columnWanted];
            }
            return col;
        }
    }
}
