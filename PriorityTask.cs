public class PriorityTask : Task
{
    public PriorityLevel Priority { get; set; }

    public PriorityTask(int id, string title, string description, DateTime dueDate, PriorityLevel priority)
        : base(id, title, description, dueDate)
    {
        Priority = priority;
    }

    public override string GetTaskDetail()
    {
        return $"{base.GetTaskDetail()} | Priority:  {Priority}";
    }
}
