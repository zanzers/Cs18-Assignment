namespace VacuumBot
{
       public class Map
    {
        private enum CellType { Empty, Dirt, Obstacle, Cleaned }
        private CellType[,] _grid;
        public int Width { get; private set; }
        public int Height { get; private set; }


        public Map(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            _grid = new CellType[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    _grid[x, y] = CellType.Empty;
                }
            }
        }


        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < this.Width && y >= 0 && y < this.Height;
        }


        public bool IsDirt(int x, int y)
        {
            return IsInBounds(x, y) && _grid[x, y] == CellType.Dirt;
        }

        public bool IsObstacle(int x, int y)
        {
            return IsInBounds(x, y) && _grid[x, y] == CellType.Obstacle;
        }


        public void AddObstacle(int x, int y)
        {
            _grid[x, y] = CellType.Obstacle;
        }

        public void AddDirt(int x, int y)
        {
            _grid[x, y] = CellType.Dirt;
        }

        public void Clean(int x, int y)

        {
            if (IsInBounds(x, y))
            {
                _grid[x, y] = CellType.Cleaned;
            }
        }



        public void Display(int robotX, int robotY)
        {
            // display  2d grid, accepts location x and y


            Console.Clear();
            Console.WriteLine("vacuum cleaner robot simulation:");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Legends: #=obstacle, D=Dirt, .=Empty, R=Robot, C=Cleaned");

            // display loop using grid

            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (x == robotX && y == robotY)
                    {
                        Console.Write("R ");
                    }
                    else
                    {
                        switch (_grid[x, y])
                        {
                            case CellType.Empty: Console.Write(". "); break;
                            case CellType.Dirt: Console.Write("D "); break;
                            case CellType.Obstacle: Console.Write("# "); break;
                            case CellType.Cleaned: Console.Write("C "); break;
                        }
                    }
                }
                Console.WriteLine();
            }
            Thread.Sleep(200);
        }

    }

}