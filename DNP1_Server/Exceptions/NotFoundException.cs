namespace DNP1_Server.Exceptions; 

/// <summary>
/// Thrown when data is not found
/// </summary>
public class NotFoundException : Exception {
	public NotFoundException() {
	}

	public NotFoundException(string message)
		: base(message) {
	}

	public NotFoundException(string message, Exception inner)
		: base(message, inner) {
	}
}