namespace DNP1_Server.Exceptions; 

/// <summary>
/// Thrown when there's duplicate data within database
/// </summary>
public class DuplicateDataException : Exception {
	public DuplicateDataException() {
	}

	public DuplicateDataException(string message)
		: base(message) {
	}

	public DuplicateDataException(string message, Exception inner)
		: base(message, inner) {
	}
}