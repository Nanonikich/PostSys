using CSharpFunctionalExtensions;

namespace PostSys.Domain.ValueObjects;

/// <summary>Объект значения, описывающий хеш пароля.</summary>
public class PasswordHash : ValueObject
{
    /// <summary>Возвращает хеш пароля.</summary>
    public string Value { get; }

    private PasswordHash(string value)
    {
        Value = value;
    }

    /// <summary>Создаёт хеш пароля из уже вычисленного значения.</summary>
    /// <param name="hashedValue">Хеш пароля.</param>
    public static Result<PasswordHash> Create(string hashedValue)
    {
        if(string.IsNullOrWhiteSpace(hashedValue))
            return Result.Failure<PasswordHash>("Password hash is required.");

        return Result.Success(new PasswordHash(hashedValue));
    }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}