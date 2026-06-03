namespace ItalianFiscalKit.Exceptions;

/// <summary>
/// The exception that is thrown when some of the data that compones the Italian Fiscal Code string are not valid.
/// </summary>
[Serializable]
public class InvalidFiscalCodeDataException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="InvalidFiscalCodeDataException"/> with no message.
    /// </summary>
    public InvalidFiscalCodeDataException()
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="InvalidFiscalCodeDataException"/> with a descriptive message.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    public InvalidFiscalCodeDataException(string message)
        : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of <see cref="InvalidFiscalCodeDataException"/> with a descriptive message
    /// and a reference to the inner exception that caused this exception.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public InvalidFiscalCodeDataException(string message, Exception innerException)
        : base(message, innerException)
    { }
}