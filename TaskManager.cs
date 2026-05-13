public class TaskManager
{
    private List<Task> tasks = new List<Task>();
    private int nextId = 1;
    public void AddTask(string title, string description,DateTime dueDate,PriorityLevel priority)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title Cant be empty");

            if(dueDate < DateTime.Now)
                throw new ArgumentException("Deadline cannot be in past time");

            var Task = new PriorityTask(nextId++,title,description,dueDate,priority);
            tasks.Add(Task);
            System.Console.WriteLine("Task added Successfully!!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Error Occured : {ex.Message}");
        }
    }

    public void ShowAllTasks()
{
    if(tasks.Count == 0)
    {
        System.Console.WriteLine("No Task found, Please add task to proceed");
        return;
    }
    
    System.Console.WriteLine("============= ALL TASKS =================");
    
    List<Task> SortedTasks = new List<Task>(tasks);
    
    SortedTasks.Sort(TaskComparison);
    
    foreach(var item in SortedTasks)
    {
        System.Console.WriteLine(item.GetTaskDetail());
    }
}

    private int TaskComparison(Task x, Task y)
    {
        // Compare by date 
        if (x.DueDate < y.DueDate) return -1;
        if (x.DueDate > y.DueDate) return 1;
        
        // date same but  by priority
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
            if(id <= 0)
                throw new ArgumentException("ID must be a positive number.");

            Task FoundTask = null;

            foreach(var item in tasks)
            {
                if(item.Id == id)
                {
                    FoundTask = item;
                    break;
                }
            }

            if(FoundTask == null)
            {
                Console.WriteLine($"Task with ID {id} not found");
                return;
            }

            if(FoundTask.Status == TaskStatus.Completed)
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
        catch(Exception ex)
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

            foreach(var item in tasks)
            {
                total ++;
                if(item.Status == TaskStatus.Completed)
                {
                    completed++;
                }
                if(item.Status == TaskStatus.InProgress)
                {
                    inProgress++;
                }
                if(item.Status == TaskStatus.Pending)
                {
                    inPending++;
                }
                //
                //
                //Priority Task++ Implement kora baki ase .
                //
                //


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
        catch(Exception ex)
        {
            System.Console.WriteLine($"Error Generating report:{ex.Message}");
        }
        
        
    }



    public void EditTask(int id)
    {
        try
        {
            if(id <= 0)
                throw new ArgumentException("ID must be a positive number.");

            Task FoundTask = null;

            foreach(var item in tasks)
            {
                if(item.Id == id)
                {
                    FoundTask = item;
                    break;
                }
            }

            if(FoundTask == null)
            {
                Console.WriteLine($"Task with ID {id} not found");
                return;
            }

            Console.WriteLine($"Editing Task: {FoundTask.Title}");            
            Console.Write($"New title ({FoundTask.Title}): ");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                FoundTask.Title = newTitle;
            }
            
            Console.Write($"New description ({FoundTask.Description}): ");
            string newDesc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDesc))
            {
                FoundTask.Description = newDesc;
            }
            
            Console.Write($"New due date ({FoundTask.DueDate:yyyy-mm-dd}): ");
            string newDate = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDate))
            {
                DateTime dueDate;
                if (DateTime.TryParse(newDate, out dueDate))
                {
                    FoundTask.DueDate = dueDate;
                }
            }
            
            if (FoundTask is PriorityTask priorityTask)
            {
                Console.Write($"New priority ({priorityTask.Priority}): ");
                string newPriority = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPriority))
                {
                    PriorityLevel priority;
                    if (Enum.TryParse<PriorityLevel>(newPriority, true, out priority))
                    {
                        priorityTask.Priority = priority;
                    }
                }
            }
            
            Console.WriteLine("Task updated successfully!");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error Occurred: {ex.Message}");
        }
    }

}