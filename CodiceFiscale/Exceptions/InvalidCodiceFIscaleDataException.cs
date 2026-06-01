namespace CodiceFiscale.Exceptions;

/// <summary>
/// The exception that is thrown when some of the data that compones the Italian Codice Fiscale string are not valid.
/// </summary>
[Serializable]
public class InvalidCodiceFiscaleDataException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="InvalidCodiceFiscaleDataException"/> with no message.
    /// </summary>
    public InvalidCodiceFiscaleDataException()
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="InvalidCodiceFiscaleDataException"/> with a descriptive message.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    public InvalidCodiceFiscaleDataException(string message)
        : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="InvalidCodiceFiscaleDataException"/> with a descriptive message
    /// and a reference to the inner exception that caused this exception.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public InvalidCodiceFiscaleDataException(string message, Exception innerException)
        : base(message, innerException)
    { }
}