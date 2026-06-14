namespace ItalianFiscalKit.Exceptions;

/// <summary>
/// The exception that is thrown when some of the data that compones the Italian Fiscal Code string are not valid.
/// </summary>
[Serializable]
public class InvalidFiscalCodeDataException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="InvalidFiscalCodeDataException"/> with a descriptive message.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    public InvalidFiscalCodeDataException(string message)
        : base(message)
    { }
}
