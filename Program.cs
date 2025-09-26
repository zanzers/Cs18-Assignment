using System;
using System.Collections.Generic;
using System.Diagnostics;




namespace VacuumBot
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
            // robot.SetStrategy(new SPatternStrategy());
            robot.SetStrategy(new PlanedRouteStrategy());
            robot.StartCleaning();

            
        }
    }


 
} 