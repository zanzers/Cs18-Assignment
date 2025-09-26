using System;
using System.Collections.Generic;




namespace RobotCleaner
{
    
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Initialize CleaningBot");
            
            Map myMap = new Map(20, 10);

            myMap.AddDirt(5, 3);
            myMap.AddDirt(10, 8);
            myMap.AddObstacle(2, 5);
            myMap.AddObstacle(12, 1);

            myMap.Display(10, 9);


            Robot robot  = new Robot(myMap);
            robot.StartCleaning();

            Console.WriteLine("Cleaning completed!");
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
            if( IsInBounds(x, y))
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

            for (int y= 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if( x == robotX && y == robotY)
                    {
                        Console.Write("R ");
                    }
                    else
                    {
                        switch(_grid[x,y])
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

        public int X { get; private set; }
        public int Y { get; private set; }


        public Robot(Map map)
        {
            this._map = map;
            this.X = 0;
            this.Y = 0;
        }

        public bool Move( int newX, int newY)
        {
            // Set new location and display map in the grid


            if(_map.IsInBounds(newX, newY) && !_map.IsObstacle(newX,  newY))
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
            if(_map.IsDirt(this.X, this.Y))
            {
                _map.Clean(this.X, this.Y);
                _map.Display(this.X, this.Y);
            }
        }

        public void StartCleaning()
        {
            Console.WriteLine("Starting cleaning The room...");
            // flag the directions


            int direction = 1; 
            

            for(int y = 0; y < _map.Height; y++)
            {
                int startX = (direction == 1) ? 0 : _map.Width - 1;
                int endX  = (direction == 1) ? _map.Width : -1;

               for (int x = startX; (direction == 1) ? x < endX : x > endX; x += direction)
                {
                //    if(!Move(x, y))
                //    {
                //     // obstacle detected, skip to next cell
                //     continue;
                //    }
                    Move(x, y);
                   CleanCurrentSpot();
                }

                direction *= -1; 
            }
        }

    }
} 