using System.Text.Json.Serialization;

namespace Trivo.Application.Utils;

/// <summary>
/// Represents the result of an operation that can be either successful or failed.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets the error information if the operation failed.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Error? Error { get; set; }

    /// <summary>
    /// Gets the explicit HTTP status code to use on success, when set. When <see langword="null"/>, the
    /// API layer defaults to 204 (no value) or 200 (<see cref="ResultT{TValue}"/> with a value).
    /// </summary>
    [JsonIgnore]
    public int? SuccessStatusCode { get; set; }

    /// <summary>
    /// Initializes a successful result.
    /// </summary>
    protected Result()
    {
        IsSuccess = true;
        Error = default;
    }

    /// <summary>
    /// Initializes a successful result with an explicit HTTP status code.
    /// </summary>
    /// <param name="successStatusCode">The HTTP status code the API layer should use for this success.</param>
    protected Result(int successStatusCode)
    {
        IsSuccess = true;
        Error = default;
        SuccessStatusCode = successStatusCode;
    }

    /// <summary>
    /// Initializes a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure.</param>
    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a failed <see cref="Result"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result(Error error) =>
        new(error);

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A new <see cref="Result"/> indicating success.</returns>
    public static Result Success() =>
        new();

    /// <summary>
    /// Creates a successful result without a value, for an async/fire-and-forget operation accepted for
    /// processing (202 Accepted).
    /// </summary>
    /// <returns>A new <see cref="Result"/> indicating success with status code 202.</returns>
    public static Result Accepted() =>
        new(202);

    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure.</param>
    /// <returns>A new <see cref="Result"/> indicating failure.</returns>
    public static Result Failure(Error error) =>
        new(error);
}

/// <summary>
/// Represents the result of an operation that can be successful with a value of type <typeparamref name="TValue"/>
/// or failed with an associated error.
/// </summary>
/// <typeparam name="TValue">The type of the value contained in the result when the operation is successful.</typeparam>
public class ResultT<TValue> : Result
{
    /// <summary>
    /// Contains the result value in case of success.
    /// </summary>
    private readonly TValue? _value;

    /// <summary>
    /// Initializes a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value associated with the successful result.</param>
    private ResultT(TValue value) : base()
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a successful result with the specified value and an explicit HTTP status code.
    /// </summary>
    /// <param name="value">The value associated with the successful result.</param>
    /// <param name="successStatusCode">The HTTP status code the API layer should use for this success.</param>
    private ResultT(TValue value, int successStatusCode) : base(successStatusCode)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a failed result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure.</param>
    private ResultT(Error error) : base(error)
    {
        _value = default;
    }

    /// <summary>
    /// Gets the result value if the operation was successful; otherwise, throws an exception.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the result indicates failure.</exception>
    public TValue Value =>
        IsSuccess ? _value! : throw new InvalidOperationException("The value cannot be accessed when IsSuccess is false.");

    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a failed <see cref="ResultT{TValue}"/>.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator ResultT<TValue>(Error error) =>
        new(error);

    /// <summary>
    /// Implicitly converts a value of type <typeparamref name="TValue"/> to a successful <see cref="ResultT{TValue}"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator ResultT<TValue>(TValue value) =>
        new(value);

    /// <summary>
    /// Creates a successful result with the provided value.
    /// </summary>
    /// <param name="value">The value associated with the successful result.</param>
    /// <returns>A <see cref="ResultT{TValue}"/> instance representing success.</returns>
    public new static ResultT<TValue> Success(TValue value) =>
        new(value);

    /// <summary>
    /// Creates a successful result with the provided value, for an operation that created a resource
    /// (201 Created).
    /// </summary>
    /// <param name="value">The value associated with the successful result.</param>
    /// <returns>A <see cref="ResultT{TValue}"/> instance representing success with status code 201.</returns>
    public static ResultT<TValue> Created(TValue value) =>
        new(value, 201);

    /// <summary>
    /// Creates a successful result with the provided value, for an async operation accepted for processing
    /// (202 Accepted).
    /// </summary>
    /// <param name="value">The value associated with the successful result.</param>
    /// <returns>A <see cref="ResultT{TValue}"/> instance representing success with status code 202.</returns>
    public static ResultT<TValue> Accepted(TValue value) =>
        new(value, 202);

    /// <summary>
    /// Creates a failed result with the provided error.
    /// </summary>
    /// <param name="error">The error associated with the failed result.</param>
    /// <returns>A <see cref="ResultT{TValue}"/> instance representing failure.</returns>
    public new static ResultT<TValue> Failure(Error error) =>
        new(error);
}