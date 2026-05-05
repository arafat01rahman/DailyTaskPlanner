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
            return ;
        }
        System.Console.WriteLine("=============  ALL TASKS  =================");
        foreach(var item in tasks)
        {
            System.Console.WriteLine(item.GetTaskDetail());
        }
        
        
    }

    public void CompleteTask(int id)
    {
        try
        {
            if(id<0)
                throw new ArgumentException("ID must be a positive number.You know right?");

            Task FoundTask = null;

            foreach(var item in tasks)
            {
                if(item.Id == id)
                {
                    FoundTask = item;
                    break;
                }
            }

            if(FoundTask.Status == TaskStatus.Completed)
            {
                System.Console.WriteLine("That Task is Already completed boy! Forgot? You gotta sleep");
                FoundTask.Status = TaskStatus.Completed;

                Console.WriteLine($"Task '{FoundTask.Title}' (ID: {FoundTask.Id}) marked as completed!");
                Console.WriteLine($"Completed on: {DateTime.Now}");
            }
            
            else
            {
                System.Console.WriteLine($"Task '{FoundTask.Title}' (ID: {FoundTask.Id}) Not found");
            }


        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Error Occurred: {ex.Message}");
        }
    }

    // GenerateReport Methode
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
}