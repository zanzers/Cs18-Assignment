#  Take-Home Activity: The Strategy-Powered Vacuum Bot

## 🎯 Objective
Refactor your vacuum robot code by implementing the **Strategy Design Pattern** to make its cleaning algorithm swappable.  
You will then present your solution in a short video.

---

## 🛠️ Part 1: Implementation Steps

- [ ] **1. Create the Strategy Interface**
  - [ ] Define a new public interface: `ICleaningStrategy`
  - [ ] Add one method signature:
    ```csharp
    void Clean(Robot robot, Map map);
    ```

- [ ] **2. Implement Concrete Strategies**
  - [ ] Create at least **two public classes** that implement `ICleaningStrategy`
  - [ ] **Strategy 1:** `S_PatternStrategy` (refactor original logic)
  - [ ] **Strategy 2:** `RandomPathStrategy` (new random movement)

- [ ] **3. Update the Robot Class**
  - [ ] Add a private `ICleaningStrategy` field
  - [ ] Add a `SetStrategy()` method
  - [ ] Update `StartCleaning()` to call the current strategy’s `Clean()`

- [ ] **4. Update the Main Method**
  - [ ] Demonstrate by setting and running the first strategy
  - [ ] Switch to the second strategy and show behavior change

---

## 🎥 Part 2: Video Explainer (3–5 Minutes)

- [ ] Record a short video explaining your work  
- [ ] Show **both strategies working** in the demo  
- [ ] Explain the Strategy Pattern (interface + strategy classes + Robot refactor)  
- [ ] Analyze why this design is more flexible  

---

## 📦 Part 3: Submission

- [ ] Submit all updated **C# source code files** (or GitHub repo link)  
- [ ] Submit your **video link** (YouTube unlisted, Loom, or cloud share)  
