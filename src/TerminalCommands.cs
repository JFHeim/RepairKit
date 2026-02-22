namespace RepairKit;

[HarmonyPatch(typeof(Terminal)), HarmonyWrapSafe]
file static class TerminalCommands
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Terminal.InitTerminal))]
    private static void Postfix()
    {
        _ = new Terminal.ConsoleCommand("break_inventory_items",
            "", isCheat: true, onlyAdmin: true,
            action: args =>
            {
                var inventory = Player.m_localPlayer.GetInventory();
                foreach (var itemData in inventory.GetAllItems())
                {
                    var maxDurability = itemData.GetMaxDurability();
                    itemData.m_durability = Mathf.Clamp(itemData.m_durability - (0.25f * maxDurability), 0, maxDurability);
                }

                inventory.Changed();

                args.Context.AddString("Done");
            });
    }
}