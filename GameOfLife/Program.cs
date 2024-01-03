namespace GameOfLife
{
    internal class Program
    {
        static int _r = 20;
        static int _c = 20;
        static double _comparator = 0.5;

        static bool[,] _bufer = new bool[_r, _c];
        static bool[,] _grid = new bool[_r, _c];

        static void Main(string[] args)
        {
            if(args.Length > 1)
            {
                if (args.Length < 3) {
                    try
                    {
                        _r = Convert.ToInt32(args[0]);
                        _c = Convert.ToInt32(args[1]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else {
                    try
                    {
                        _r = Convert.ToInt32(args[0]);
                        _c = Convert.ToInt32(args[1]);
                        _comparator = Convert.ToDouble(args[2]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                _grid = new bool[_r, _c];
                _bufer = new bool[_r, _c];
            }

            var rand = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < _r; i++)
            {
                for (int j = 0; j < _c; j++)
                {
                    _grid[i, j] = rand.NextDouble() > _comparator;
                }
            }

            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Conway's Game of Life");
            Console.WriteLine("press q to Quit");

            while (true)
            {
                _grid = Update();
                Print();
                if (Console.KeyAvailable)
                {
                    var c = Console.ReadKey();
                    if(c.Key == ConsoleKey.Q)
                    {
                        Console.Clear();
                        Console.CursorVisible = true;
                        break;
                    }
                }
                Thread.Sleep(1000/30);
            }
        }

        static void Print()
        {
            List<(int x, int y, bool v)> diff = GetDiff();

            foreach (var cord in diff)
            {
                Console.SetCursorPosition(cord.x * 2, cord.y + 3);
                Console.Write(cord.v ? "██" : "  ");
            }
        }

        static List<(int, int, bool)> GetDiff()
        {
            List<(int x, int y, bool v)> diff = new();
            for (int i = 0; i < _r; i++)
            {
                for (int j = 0; j < _c; j++)
                {
                    if (_bufer[i, j] != _grid[i, j])
                    {
                        diff.Add((i, j, _grid[i, j]));
                        _bufer[i, j] = _grid[i, j];
                    }
                }
            }
            return diff;
        }

        static bool[,] Update()
        {
            var newGrid = new bool[_r, _c];
            for (int i = 0; i < _r;  i++)
            {
                for (int j = 0; j < _c; j++)
                {
                    newGrid[i, j] = GetConvalution(i, j);
                }
            }
            return newGrid;
        }

        static bool GetConvalution(int x, int y)
        {
            int sLiveCount = 0;
            for (int i = -1; i <= 1; i++)
            {
                for(int j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || y + j < 0 || x + i >= _r || y + j >= _c || (i == 0 && j == 0)) continue;
                    sLiveCount += _grid[x + i, y + j] ? 1 : 0;
                }
            }
            if (_grid[x, y])
            {
                if (sLiveCount < 2) return false;
                if (sLiveCount < 4) return true;
                return false;
            }
            else
            {
                if(sLiveCount == 3) return true;
                return false;
            }
        }
    }
}