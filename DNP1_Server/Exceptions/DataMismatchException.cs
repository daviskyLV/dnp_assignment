namespace DNP1_Server.Exceptions; 

/// <summary>
/// Thrown when the provided data doesn't match expected data
/// </summary>
public class DataMismatchException : Exception {
	public DataMismatchException() {
	}

	public DataMismatchException(string message)
		: base(message) {
	}

	public DataMismatchException(string message, Exception inner)
		: base(message, inner) {
	}
}