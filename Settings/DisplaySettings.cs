namespace YetAnotherPriceTooltip;

public partial class Settings {
    // Declare as static for reflection purposes
    internal static Setting<bool> IncludeSellPrice = new(
            section: "Display",
            key: "IncludeSellPrice",
            defaultValue: true,
            description: "Include sell price in brackets."
        );
}