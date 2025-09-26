Take-Home Activity: The Strategy-Powered Vacuum Bot
Objective: Refactor your vacuum robot code by implementing the Strategy Design Pattern to
make its cleaning algorithm swappable. You will then present your solution in a short video.
Part 1: Implementation Steps
1. Create the Strategy Interface:
o Define a new public interface, ICleaningStrategy.
o It must contain one method signature: void Clean(Robot robot, Map map).
2. Implement Concrete Strategies:
o Create at least two public classes that implement ICleaningStrategy.
o Strategy 1 (Refactor): Make a class for your original "S-pattern" logic (e.g.,
S_PatternStrategy). Move your existing cleaning code into its Clean method.
o Strategy 2 (New): Create a class for a new algorithm (e.g.,
RandomPathStrategy). Implement logic for random movement.

3. Update the Robot Class:
o Add a private ICleaningStrategy field to hold the current strategy.
o Add a public SetStrategy() method to assign a strategy to that field.
o Replace all logic in your StartCleaning() method with a single line that calls
the Clean method on the current strategy object.

4. Update the Main Method:
o In your Main method, demonstrate the system by:
1. Setting and running the first strategy.
2. Setting and running the second strategy to show the robot's behavior
changes.

Part 2: Video Explainer (3-5 Minutes)
Record a video explaining your work.
• Requirements: Your face must be visible (picture-in-picture) while you screen-share
your code and application.
• Content to Cover:
1. Demonstrate: Run your program and show both cleaning strategies working.
2. Explain: Briefly explain the Strategy Pattern while showing your interface,
strategy classes, and the modified Robot class.
3. Analyze: Conclude by explaining why this new design is more flexible than the
original hard-coded version.

Part 3: Submission
Submit the following:
• All of your updated C# source code files (or link to a Github repository).
• A link to your video (e.g., an unlisted YouTube link, Loom link, or a shared cloud file).
