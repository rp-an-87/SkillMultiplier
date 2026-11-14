using SPTarkov.Server.Core.Models.Spt.Config;

namespace SkillMultiplier;

/// <summary>
/// The two hideout-config numbers SPT 4.1.x pays crafting XP from, exposed in the units the page scales.
/// <para>
/// When a finished craft is collected, the server adds <c>craftingExpAmount</c> if the recipe differs from the
/// station's last one, and one point for every <c>hoursForSkillCrafting</c> seconds of recipe time (the
/// remainder carries over). Both are plain config values read on every collection, so changing them on the
/// live config object takes effect immediately and never touches the file on disk.
/// </para>
/// <para>
/// The time rule is expressed as XP per hour rather than seconds per point, so that a multiplier of 2 means
/// twice the XP, the same as every other row: <c>seconds = 3600 / xpPerHour</c>.
/// </para>
/// </summary>
internal static class HideoutXpTarget
{
    private const double SecondsPerHour = 3600.0;

    /// <summary>Set once at startup from the injected config, before base values are captured.</summary>
    internal static HideoutConfig? Config { get; set; }

    public static double AlternatingBonus
    {
        get => Config?.CraftingExpAmount ?? 0;
        set
        {
            if (Config is not null)
            {
                Config.CraftingExpAmount = value;
            }
        }
    }

    public static double XpPerHourOfCrafting
    {
        get => Config is { HoursForSkillCrafting: > 0 } config ? SecondsPerHour / config.HoursForSkillCrafting : 0;
        set
        {
            if (Config is not null)
            {
                Config.HoursForSkillCrafting = SecondsPerPoint(value);
            }
        }
    }

    /// <summary>
    /// Seconds of recipe time per point for a given XP-per-hour. Zero or less means "pay nothing", which the
    /// server expresses as a span so long no craft reaches it. Never below one second: the server divides by it.
    /// </summary>
    internal static int SecondsPerPoint(double xpPerHour)
    {
        if (double.IsNaN(xpPerHour) || xpPerHour <= 0)
        {
            return int.MaxValue;
        }

        return (int)Math.Clamp(Math.Round(SecondsPerHour / xpPerHour), 1.0, int.MaxValue);
    }
}
