namespace ArticleManagementApp.Shared.Abstractions;

/// <summary>
/// Represents the result of an operation that can succeed or fail.
/// </summary>
/// <typeparam name="T">The type of the value returned on success.</typeparam>
public sealed class Result<T>
{
	private Result(bool isSuccess, T? value, string error, List<string> errors)
	{
		IsSuccess = isSuccess;
		Value = value;
		Error = error;
		Errors = errors;
	}

	/// <summary>
	/// Gets a value indicating whether the operation was successful.
	/// </summary>
	public bool IsSuccess { get; }

	/// <summary>
	/// Gets a value indicating whether the operation failed.
	/// </summary>
	public bool IsFailure => !IsSuccess;

	/// <summary>
	/// Gets the value returned by the operation if successful.
	/// </summary>
	public T? Value { get; }

	/// <summary>
	/// Gets the primary error message if the operation failed.
	/// </summary>
	public string Error { get; }

	/// <summary>
	/// Gets the list of all error messages if the operation failed.
	/// </summary>
	public List<string> Errors { get; }

	/// <summary>
	/// Creates a successful result with the specified value.
	/// </summary>
	/// <param name="value">The value to return.</param>
	/// <returns>A successful result.</returns>
	public static Result<T> Success(T value) =>
			new(true, value, string.Empty, []);

	/// <summary>
	/// Creates a failed result with a single error message.
	/// </summary>
	/// <param name="error">The error message.</param>
	/// <returns>A failed result.</returns>
	public static Result<T> Failure(string error) =>
			new(false, default, error, [error]);

	/// <summary>
	/// Creates a failed result with multiple error messages.
	/// </summary>
	/// <param name="errors">The list of error messages.</param>
	/// <returns>A failed result.</returns>
	public static Result<T> Failure(List<string> errors) =>
			new(false, default, errors.FirstOrDefault() ?? string.Empty, errors);

	/// <summary>
	/// Executes one of two functions based on the result's success state.
	/// </summary>
	/// <typeparam name="TResult">The type of the result to return.</typeparam>
	/// <param name="onSuccess">Function to execute if successful.</param>
	/// <param name="onFailure">Function to execute if failed.</param>
	/// <returns>The result of the executed function.</returns>
	public TResult Match<TResult>(
			Func<T, TResult> onSuccess,
			Func<string, TResult> onFailure) =>
			IsSuccess ? onSuccess(Value!) : onFailure(Error);
}

/// <summary>
/// Represents the result of an operation that can succeed or fail without returning a value.
/// </summary>
public sealed class Result
{
	private Result(bool isSuccess, string error, List<string> errors)
	{
		IsSuccess = isSuccess;
		Error = error;
		Errors = errors;
	}

	/// <summary>
	/// Gets a value indicating whether the operation was successful.
	/// </summary>
	public bool IsSuccess { get; }

	/// <summary>
	/// Gets a value indicating whether the operation failed.
	/// </summary>
	public bool IsFailure => !IsSuccess;

	/// <summary>
	/// Gets the primary error message if the operation failed.
	/// </summary>
	public string Error { get; }

	/// <summary>
	/// Gets the list of all error messages if the operation failed.
	/// </summary>
	public List<string> Errors { get; }

	/// <summary>
	/// Creates a successful result.
	/// </summary>
	/// <returns>A successful result.</returns>
	public static Result Success() =>
			new(true, string.Empty, []);

	/// <summary>
	/// Creates a failed result with a single error message.
	/// </summary>
	/// <param name="error">The error message.</param>
	/// <returns>A failed result.</returns>
	public static Result Failure(string error) =>
			new(false, error, [error]);

	/// <summary>
	/// Creates a failed result with multiple error messages.
	/// </summary>
	/// <param name="errors">The list of error messages.</param>
	/// <returns>A failed result.</returns>
	public static Result Failure(List<string> errors) =>
			new(false, errors.FirstOrDefault() ?? string.Empty, errors);
}
