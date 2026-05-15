using System;

public class TaskNotFoundException : Exception
{
    public TaskNotFoundException() : base() { }

    public TaskNotFoundException(string message) : base(message) { }

    public TaskNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}