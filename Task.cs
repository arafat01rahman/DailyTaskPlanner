public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskStatus Status { get; set; }

    public Task(int id, string title, string description, DateTime dueDate)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = TaskStatus.Pending;
    }

    public virtual string GetTaskDetail()
    {
        return $"ID: {Id} | {Title} | Due: {DueDate} | Status: {Status}";
    }
}