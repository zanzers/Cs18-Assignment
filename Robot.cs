using System.Diagnostics;

namespace VacuumBot
{
    
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

     public class PlanedRouteStrategy : ICleaningStrategy
    {
        public void Clean(Robot robot, Map map)
        {
            Console.WriteLine("Starting cleaning with Planed Route Strategy...");
            
            // the Pland step are
            // 1. Find diry cells
            while (true)
            {
                var dirtyCells = FindDirtyCells(map);

                if (dirtyCells.Count == 0)
                {
                    
                    var returnPath = PlandRoute(map, robot, (0, 0));
                    if (returnPath.Count > 0)
                    {
                        // return to the original x, y
                        foreach (var step in returnPath)
                        {
                            robot.Move(step.x, step.y);
                        }
                    }

                    Console.WriteLine("Cleaning complete.");
                    break;
                }

                // 2. plan best routes
                var nearestDirt = FindNearestDirt(robot, dirtyCells);
                var path = PlandRoute(map, robot, nearestDirt);


                          
            // 3.move robot
                foreach (var step in path)
                {
                    robot.Move(step.x, step.y);
                    robot.CleanCurrentSpot();
                }
            }

        }

        private List<(int X, int Y)> FindDirtyCells(Map map)
        {
            var dirtyCells = new List<(int, int)>();

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.IsDirt(x, y))
                    {
                        dirtyCells.Add((x, y));
                    }
                }
            }
            return dirtyCells;
        }

        private (int x, int y) FindNearestDirt(Robot robot, List<(int X, int Y)> dirtyCells)
        {
            (int x, int y) nearest = dirtyCells[0];

            // Manhattan distance
            int bestDir = Math.Abs(nearest.x - robot.X) + Math.Abs(nearest.y - robot.Y);

            foreach (var dirt in dirtyCells)
            {
                int dist = Math.Abs(dirt.X - robot.X) + Math.Abs(dirt.Y - robot.Y);
                if (dist < bestDir)
                {
                    nearest = dirt;
                    bestDir = dist;
                }
            }
            return nearest;
        }

        private List<(int x, int y)> PlandRoute(Map map, Robot robot, (int x, int y) target)
        {


            return FindPath(map, robot.X, robot.Y, target.x, target.y);
        }
    

       
       
    
        public List<(int x, int y)> FindPath(Map map, int startX, int startY, int targetX, int targetY)
        {

            var directions = new (int dx, int dy)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
            var queue = new Queue<(int x, int y)>();
            var visited = new bool[map.Width, map.Height];
            var parent = new Dictionary<(int, int), (int, int)>();

            // Breadth-First Search (BFS) 
            queue.Enqueue((startX, startY));
            visited[startX, startY] = true;

            while (queue.Count > 0)
            {
                var (cx, cy) = queue.Dequeue();

                if (cx == targetX && cy == targetY)
                {
                    var path = new List<(int x, int y)>();
                    var current = (cx, cy);

                    while (current != (startX, startY))
                    {
                        path.Add(current);
                        current = parent[current];
                    }

                    path.Reverse();
                    return path;
                }

                // Neighbors 
                foreach (var (dx, dy) in directions)
                {
                    int nx = cx + dx;
                    int ny = cy + dy;

                    if (nx >= 0 && ny >= 0 && nx < map.Width && ny < map.Height)
                    {
                        if (!visited[nx, ny] && !map.IsObstacle(nx, ny))
                        {
                            queue.Enqueue((nx, ny));
                            visited[nx, ny] = true;
                            parent[(nx, ny)] = (cx, cy);
                        }
                    }
                }
            }
            return new List<(int, int)>();
        }

    }
   
}