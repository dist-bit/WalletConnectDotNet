using System;

public class InvalidHmacException : Exception
{
    public InvalidHmacException() : base("Received and computed HMAC doesn't match")
    {
    }

    public InvalidHmacException(string message) : base(message)
    {
    }

    public InvalidHmacException(string message, Exception innerException) : base(message, innerException)
    {
    }
}