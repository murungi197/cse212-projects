using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several items with distinct priorities where the highest priority
    //   item is at the BACK of the queue (Low:1, Medium:3, High:5). Dequeue once.
    // Expected Result: "High" is returned because it has the highest priority (5).
    // Defect(s) Found: The Dequeue loop stopped at "_queue.Count - 1", so it never
    //   examined the last item. The highest priority item at the back was missed and the
    //   wrong value ("Medium") was returned.
    public void TestPriorityQueue_HighestPriorityAtBack()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("High", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items and dequeue them all. Priorities are mixed and the
    //   highest priority item is in the middle (A:2, B:9, C:4, D:1). Dequeue repeatedly.
    // Expected Result: Items come out in priority order: B (9), C (4), A (2), D (1).
    // Defect(s) Found: Dequeue never removed the selected item from the internal list, so
    //   the same highest priority item was returned every time instead of the next one.
    public void TestPriorityQueue_RemovesInPriorityOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 9);
        priorityQueue.Enqueue("C", 4);
        priorityQueue.Enqueue("D", 1);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue several items where more than one share the highest priority
    //   (First:5, Second:5, Third:2, Fourth:5). Dequeue the high priority items.
    // Expected Result: Among equal priorities, FIFO order is preserved: First, then
    //   Second, then Fourth are returned before Third.
    // Defect(s) Found: The comparison used ">=" which selected the LAST tied item instead
    //   of the first, breaking the FIFO tie-breaking rule.
    public void TestPriorityQueue_FifoTieBreaking()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 2);
        priorityQueue.Enqueue("Fourth", 5);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Fourth", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException is thrown with the message
    //   "The queue is empty."
    // Defect(s) Found: None - the empty-queue check worked correctly.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                               e.GetType(), e.Message)
            );
        }
    }

    // Add more test cases as needed below.
}