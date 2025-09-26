using System;
using System.Collections.Generic;
using System.Diagnostics;




namespace RobotCleaner
{

    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Initialize CleaningBot");

            Map myMap = new Map(15, 10);

            myMap.AddDirt(5, 3);   
            myMap.AddDirt(10, 8);  
            myMap.AddDirt(7, 2);

            myMap.AddObstacle(6, 2);
            myMap.AddObstacle(6, 3);
            myMap.AddObstacle(6, 4);
            myMap.AddObstacle(6, 5);
            myMap.AddObstacle(6, 6);

            myMap.AddObstacle(6, 8);

            myMap.AddObstacle(11, 7);
            myMap.AddObstacle(12, 7);
            myMap.AddObstacle(12, 8);

            
            myMap.Display(0, 0);



            Robot robot = new Robot(myMap);
            robot.SetStrategy(new SPatternStrategy());
            robot.StartCleaning();

            
        }
    }


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

    public class Robot
    {
        private readonly Map _map;
        private ICleaningStrategy _strategy;

        public int X { get; private set; }
        public int Y { get; private set; }


        public Robot(Map map)
        {
            this._map = map;
            this.X = 0;
            this.Y = 0;
        }


        public void SetStrategy(ICleaningStrategy strategy)
        {
            this._strategy = strategy;
        }



        public void StartCleaning()
        {
            if (_strategy == null)
            {
                Console.WriteLine("No cleaning strategy set!");
            }
            else
            {
                var stopwatch = Stopwatch.StartNew();
                _strategy.Clean(this, _map);
                stopwatch.Stop();
                double seconds = stopwatch.ElapsedMilliseconds / 1000.0;
                Console.WriteLine($"Cleaning finished in {stopwatch.ElapsedMilliseconds} ms ({seconds:F2} seconds).");
            }
        }




        public bool Move(int newX, int newY)
        {
            // Set new location and display map in the grid


            if (_map.IsInBounds(newX, newY) && !_map.IsObstacle(newX, newY))
            {
                this.X = newX;
                this.Y = newY;
                _map.Display(this.X, this.Y);
                return true;
            }
            else
            {
                return false;
                // Cannot move is out of bounds or obstacle!

            }

        }


        public void CleanCurrentSpot()
        {
            if (_map.IsDirt(this.X, this.Y))
            {
                _map.Clean(this.X, this.Y);
                _map.Display(this.X, this.Y);
            }
        }

    }


    public interface ICleaningStrategy
    {
        void Clean(Robot robot, Map map);
    }

    public class SPatternStrategy : ICleaningStrategy
    {
        public void Clean(Robot robot, Map map)
        {

            Console.WriteLine("Starting cleaning The room...");
            int direction = 1;

            for (int y = 0; y < map.Height; y++)
            {
                int startX = (direction == 1) ? 0 : map.Width - 1;
                int endX = (direction == 1) ? map.Width : -1;

                for (int x = startX; (direction == 1) ? x < endX : x > endX; x += direction)
                {
                    //    if(!Move(x, y))
                    //    {
                    //     // obstacle detected, skip to next cell
                    //     continue;
                    //    }
                    robot.Move(x, y);
                    robot.CleanCurrentSpot();
                }

                direction *= -1;
            }
        }
    }


   
} 