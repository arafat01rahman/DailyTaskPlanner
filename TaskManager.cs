using System;
using System.Collections.Generic;

public class TaskManager
{
    private List<Task> tasks = new List<Task>();
    private int nextId = 1;

    public void AddTask(string title, string description, DateTime dueDate, PriorityLevel priority)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            if (dueDate < DateTime.Now)
                throw new ArgumentException("Deadline cannot be in past time");

            var Task = new PriorityTask(nextId++, title, description, dueDate, priority);
            tasks.Add(Task);
            Console.WriteLine("Task added Successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Occurred: {ex.Message}");
        }
    }

    public void ShowAllTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No Task found, Please add task to proceed");
            Console.ReadKey();  
            return;
        }

        Console.WriteLine("============= ALL TASKS =================");

        List<Task> SortedTasks = new List<Task>(tasks);
        SortedTasks.Sort(TaskComparison);

        foreach (var item in SortedTasks)
        {
            Console.WriteLine(item.GetTaskDetail());
        }
    }

    private int TaskComparison(Task x, Task y)
    {
        if (x.DueDate < y.DueDate) return -1;
        if (x.DueDate > y.DueDate) return 1;

        int priorityX = GetPriorityNumber(x);
        int priorityY = GetPriorityNumber(y);

        if (priorityX < priorityY) return -1;
        if (priorityX > priorityY) return 1;
        return 0;
    }

    private int GetPriorityNumber(Task task)
    {
        if (task is PriorityTask pt)
        {
            if (pt.Priority == PriorityLevel.High) return 1;
            if (pt.Priority == PriorityLevel.Medium) return 2;
            return 3;
        }
        return 3;
    }

    public void CompleteTask(int id)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("ID must be a positive number.");

            Task FoundTask = null;

            foreach (var item in tasks)
            {
                if (item.Id == id)
                {
                    FoundTask = item;
                    break;
                }
            }

            if (FoundTask == null)
            {
                Console.WriteLine($"Task with ID {id} not found");
                return;
            }

            if (FoundTask.Status == TaskStatus.Completed)
            {
                Console.WriteLine("That task is already completed!");
            }
            else
            {
                FoundTask.Status = TaskStatus.Completed;
                Console.WriteLine($"Task '{FoundTask.Title}' (ID: {FoundTask.Id}) marked as completed!");
                Console.WriteLine($"Completed on: {DateTime.Now}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Occurred: {ex.Message}");
        }
    }

    public void GenerateReport()
    {
        try
        {
            int total = 0;
            int completed = 0;
            int inPending = 0;
            int inProgress = 0;
            int highPriority = 0;

            foreach (var item in tasks)
            {
                total++;
                if (item.Status == TaskStatus.Completed)
                {
                    completed++;
                }
                if (item.Status == TaskStatus.InProgress)
                {
                    inProgress++;
                }
                if (item.Status == TaskStatus.Pending)
                {
                    inPending++;
                }

                if (item is PriorityTask pt)
                {
                    if (pt.Priority == PriorityLevel.High)
                    {
                        highPriority++;
                    }
                }
            }

            Console.WriteLine("\n===== COMPLETION REPORT =======");
            Console.WriteLine($"Total Tasks: {total}");
            Console.WriteLine($"Completed: {completed}");
            Console.WriteLine($"Pending: {inPending}");
            Console.WriteLine($"In Progress: {inProgress}");
            Console.WriteLine($"High Priority Tasks: {highPriority}");

            if (total > 0)
            {
                double completionRate = (double)completed / total * 100;
                Console.WriteLine($"Completion Rate: {completionRate:F1}%");
            }
            else
            {
                Console.WriteLine("No tasks available.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Generating report: {ex.Message}");
        }
    }

    public void ClearAllTasks()
    {
        try
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks to clear.");
                return;
            }

            Console.Write($"Are you sure you want to delete all {tasks.Count} tasks? (yes/no): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "yes")
            {
                tasks.Clear();
                nextId = 1;
                Console.WriteLine("All tasks cleared successfully!");
            }
            else
            {
                Console.WriteLine("Clear operation cancelled.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing tasks: {ex.Message}");
        }
    }
}