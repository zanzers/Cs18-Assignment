# 🧹 Take-Home Activity: The Strategy-Powered Vacuum Bot

## 🎯 Objective
Refactor your vacuum robot code by implementing the **Strategy Design Pattern** to make its cleaning algorithm swappable.  
You will then present your solution in a short video.

---

## 🛠️ Part 1: Implementation Steps

### 1. Create the Strategy Interface
- Define a new public interface: `ICleaningStrategy`
- It must contain one method signature:
  ```csharp
  void Clean(Robot robot, Map map);
### 2. Implement Concrete Strategies
- Create at least **two public classes** that implement `ICleaningStrategy`.

**Strategy 1 (Refactor):**  
- Class: `S_PatternStrategy`  
- Move your original "S-pattern" cleaning logic into its `Clean()` method.  

**Strategy 2 (New):**  
- Class: `RandomPathStrategy`  
- Implement logic for random movement.  

### 3. Update the Robot Class
- Add a private `ICleaningStrategy` field to hold the current strategy.  
- Add a public `SetStrategy()` method to assign a strategy.  
- In `StartCleaning()`, replace all logic with a single call to the current strategy’s `Clean()` method.  

### 4. Update the Main Method
- In `Main()`, demonstrate:
  1. Setting and running the first strategy.  
  2. Switching to the second strategy and showing the robot’s behavior change.  

---

## 🎥 Part 2: Video Explainer (3–5 Minutes)

Record a short video explaining your work.  

**Requirements:**  
- Your face must be visible (picture-in-picture) while you screen-share your code and application.  

**Content to Cover:**  
1. **Demonstrate:** Run your program and show both cleaning strategies working.  
2. **Explain:** Briefly explain the Strategy Pattern while showing your interface, strategy classes, and the modified Robot class.  
3. **Analyze:** Conclude by explaining why this new design is more flexible than the original hard-coded version.  

---

## 📦 Part 3: Submission

Submit the following:  
- All of your updated **C# source code files** (or a link to your GitHub repository).  
- A link to your **video** (e.g., unlisted YouTube, Loom, or shared cloud file).  
