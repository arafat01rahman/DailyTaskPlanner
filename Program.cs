using System;

class Program
{
    static TaskManager manager = new TaskManager();
    
    static void Main(string[] args)
    {
        Console.WriteLine("DAILY TASK PLANNER");
        Console.WriteLine("==================");
        
        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                AddTask();
            }
            else if (choice == "2")
            {
                manager.ShowAllTasks();
            }
            else if (choice == "3")
            {
                CompleteTask();
            }
            else if (choice == "4")
            {
                manager.GenerateReport();
            }
            else if (choice == "5")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice! Press any key to try again...");
                Console.ReadKey();
            }
        }
    }
    
    static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("\nMAIN MENU");
        Console.WriteLine("=========");
        Console.WriteLine("1. Add New Task");
        Console.WriteLine("2. View All Tasks");
        Console.WriteLine("3. Mark Task as Completed");
        Console.WriteLine("4. Generate Report");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option (1-5): ");
    }
    
    static void AddTask()
    {
        Console.Clear();
        Console.WriteLine("ADD NEW TASK");
        Console.WriteLine("============");
        
        Console.Write("Enter title: ");
        string title = Console.ReadLine();
        
        Console.Write("Enter description: ");
        string description = Console.ReadLine();
        
        Console.Write("Enter due date (yyyy-mm-dd): ");
        DateTime dueDate;
        if (!DateTime.TryParse(Console.ReadLine(), out dueDate))
        {
            Console.WriteLine("Invalid date format!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
        Console.Write("Enter priority (Low/Medium/High): ");
        PriorityLevel priority;
        if (!Enum.TryParse<PriorityLevel>(Console.ReadLine(), true, out priority))
        {
            Console.WriteLine("Invalid priority!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
        manager.AddTask(title, description, dueDate, priority);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    
    static void CompleteTask()
    {
        Console.Clear();
        Console.WriteLine("COMPLETE TASK");
        Console.WriteLine("=============");
        
        manager.ShowAllTasks();
        
        Console.Write("Enter task ID to mark as completed: ");
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            manager.CompleteTask(id);
        }
        else
        {
            Console.WriteLine("Invalid ID!");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}