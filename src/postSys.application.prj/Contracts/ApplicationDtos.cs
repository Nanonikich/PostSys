namespace PostSys.Application.Contracts;

/// <summary>Данные полного имени.</summary>
/// <param name="Surname">Фамилия.</param>
/// <param name="Name">Имя.</param>
/// <param name="Patronymic">Отчество.</param>
public sealed record FullnameDto(string Surname, string Name, string? Patronymic);

/// <summary>Габариты посылки.</summary>
/// <param name="Length">Длина.</param>
/// <param name="Height">Высота.</param>
/// <param name="Weight">Вес.</param>
public sealed record DimensionsDto(double? Length, double? Height, double Weight);

/// <summary>Географические координаты.</summary>
/// <param name="Longitude">Долгота.</param>
/// <param name="Latitude">Широта.</param>
public sealed record AddressDto(int Longitude, int Latitude);
