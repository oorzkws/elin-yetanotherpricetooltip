#nullable disable
namespace YetAnotherPriceTooltip.Patches;

[HarmonyPatch(typeof(Thing), "WriteNote")]
public static class WriteNote_Patch {
    private static int GetPrice(Thing thing, bool sell = false) {
        var isIdentified = thing.c_IDTState;
        thing.c_IDTState = 0;
        var returnValue = thing.GetPrice(CurrencyType.Money, sell, PriceType.Default);
        thing.c_IDTState = isIdentified;
        return returnValue;
    }

    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public static void Postfix(Thing __instance, ref UINote n) {
        // Get the open inventories to see if we're shopping
        var isShopping = InvOwner.HasTrader && InvOwner.Trader.currency != CurrencyType.None;
        // Check layers for shopping mode
        if (!isShopping && LayerInventory.listInv.Any(layer => layer.invs.Any(inv => inv.currentTab.mode is UIInventory.Mode.Buy or UIInventory.Mode.Sell or UIInventory.Mode.Identify))) {
            isShopping = true;
        }
        // Either of the above is true = we already have price info displayed
        if (isShopping)
            return;

        // Space and money bag graphic
        n.Space(8);
        var subUIItem = n.AddExtra<UIItem>("costPrice");
        subUIItem.image1.sprite = SpriteSheet.Get("icon_money");
        // Text
        var buyPrice = GetPrice(__instance);
        var sellPrice = GetPrice(__instance, true);
        subUIItem.text1.SetText($"{Lang._currency(buyPrice)} ({Lang._currency(sellPrice)})", FontColor.Good);

    }
}