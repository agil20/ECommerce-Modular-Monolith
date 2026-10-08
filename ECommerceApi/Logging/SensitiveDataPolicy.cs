using System.Reflection;
using Serilog.Core;
using Serilog.Events;

namespace ECommerceApi.Logging;

public sealed class SensitiveDataPolicy : IDestructuringPolicy
{
    private static readonly string[] Forbidden =
    [
        "Password", "PasswordHash", "Token", "RefreshToken", "AccessToken",
        "SecurityStamp", "Email", "PhoneNumber", "Otp"
    ];

    private static bool IsSensitive(string name) =>
        Forbidden.Any(f => name.Contains(f, StringComparison.OrdinalIgnoreCase));

    // Email-in iki rolu var: sexsiyyet (qorunmali) ve identifikator (loga lazim).
    // Tam silsek ikinci rolu da oldururuk - ona gore qismen maskalayiriq.
    private static string Mask(string name, object? value) =>
        name.Contains("Email", StringComparison.OrdinalIgnoreCase)
            ? MaskEmail(value as string)
            : "***";

    private static string MaskEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return "***";
        var at = email.IndexOf('@');
        if (at <= 0) return "***";
        return $"{email[0]}***{email[at..]}";
    }

    public bool TryDestructure(object value, ILogEventPropertyValueFactory factory, out LogEventPropertyValue result)
    {
        result = null!;

        var properties = value.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
            .ToArray();

        // Bu tipde hessas sahe yoxdursa qarismiriq - Serilog oz normal isini gorsun
        if (!properties.Any(p => IsSensitive(p.Name)))
            return false;

        var masked = properties.Select(p => new LogEventProperty(
            p.Name,
            IsSensitive(p.Name)
                ? new ScalarValue(Mask(p.Name, p.GetValue(value)))
                : factory.CreatePropertyValue(p.GetValue(value), destructureObjects: true)));

        result = new StructureValue(masked, value.GetType().Name);
        return true;
    }
}