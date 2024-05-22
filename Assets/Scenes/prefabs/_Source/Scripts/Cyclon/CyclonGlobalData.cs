using UnityEngine;

/// <summary>
/// Глобальная информация о циклоне
/// </summary>
public static class CyclonGlobalData
{
    /// <summary>
    /// Тотальное значение давления в системе (атм)
    /// </summary>
    public static float totalPressureInSystems = 0.0f;
    /// <summary>
    /// Давление компрессора (атм);
    /// </summary>
    public static float currentPressure = 0.0f;
    /// <summary>
    /// Расход газа
    /// </summary>
    public static float gasFlow = 0.0f;
    /// <summary>
    /// Значение поворота 1-ого вентеля
    /// </summary>
    public static float valve1Angle = 0.0f;
    /// <summary>
    /// Вентиль емкости
    /// </summary>
    public static bool valve2State = false;
    /// <summary>
    /// Текущее положение ротометра в %
    /// </summary>
    public static float rotameterPosition = 0.0f;
    /// <summary>
    /// Текущее наполнение колбы в гм.
    /// </summary>
    public static float vialValue = 0.0f;
    /// <summary>
    /// Текущее наполнение емкости в гм.
    /// </summary>
    public static float tankValue = 0.0f;
    /// <summary>
    /// Режим отладки
    /// </summary>
    public static bool debugMode = false;
}