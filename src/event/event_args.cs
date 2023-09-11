using System;

public abstract class EventArgs
{}

public class WhenWhy : EventArgs
{
    public DateTime WhenOccurred { get; }
    public string Description { get; }

    public WhenWhy(string description = "")
    {
        WhenOccurred = DateTime.Now;
        Description = description;
    }
}

public class Value<T> : EventArgs
{
    public T Val { get; }

    public Value(T value)
    {
        Val = value;
    }
}

public class Values<T1, T2> : EventArgs
{
    public T1 Value1 { get; }
    public T2 Value2 { get; }

    public Values(T1 value1, T2 value2)
    {
        Value1 = value1;
        Value2 = value2;
    }
}
