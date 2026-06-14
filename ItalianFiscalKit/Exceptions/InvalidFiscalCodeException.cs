namespace ItalianFiscalKit.Exceptions;

/// <summary>
/// The exception that is thrown when an Italian Fiscal Code string fails validation.
/// </summary>
[Serializable]
public class InvalidFiscalCodeException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="InvalidFiscalCodeException"/> with a descriptive message.
    /// </summary>
    /// <param name="message">A human-readable message that describes the validation failure.</param>
    public InvalidFiscalCodeException(string message)
        : base(message)
    { }
}
