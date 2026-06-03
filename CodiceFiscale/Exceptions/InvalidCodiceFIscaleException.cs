namespace ItalianFiscalKit.Exceptions;

/// <summary>
/// The exception that is thrown when an Italian Codice Fiscale string fails validation.
/// </summary>
[Serializable]
public class InvalidCodiceFiscaleException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="InvalidCodiceFiscaleException"/> with no message.
    /// </summary>
    public InvalidCodiceFiscaleException()
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="InvalidCodiceFiscaleException"/> with a descriptive message.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    public InvalidCodiceFiscaleException(string message)
        : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="InvalidCodiceFiscaleException"/> with a descriptive message
    /// and a reference to the inner exception that caused this exception.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public InvalidCodiceFiscaleException(string message, Exception innerException)
        : base(message, innerException)
    { }
}