using JetBrains.Annotations;
using RepairKit.Config;
using static ItemDrop.ItemData;

namespace RepairKit.Patch;

[HarmonyWrapSafe]
[HarmonyPatch(typeof(Player), nameof(Player.ConsumeItem))]
file static class ApplyRepairPatch
{
    public const ItemType COSMETIC_CHEST = (ItemType)30;
    public const ItemType COSMETIC_HELMET = (ItemType)31;
    public const ItemType COSMETIC_LEGS = (ItemType)32;
    public const ItemType COSMETIC_CAPE = (ItemType)33;

    [HarmonyPrefix]
    [UsedImplicitly]
    private static bool PreventUsingKitWhenAllItemsAreOkay(Player __instance, ref bool __result, ItemDrop.ItemData item)
    {
        var repairMode = Utils.GetPrefabName(item.m_dropPrefab) switch
        {
            "JF_ItemsRepairKit" => RepairMode.Items,
            "JF_ArmorRepairKit" => RepairMode.Armor,
            _ => RepairMode.None
        };
        if (repairMode == RepairMode.None) return true;

        foreach (var itemData in __instance.GetInventory().GetAllItems())
        {
            var itemType = itemData.m_shared.m_itemType;
            var isArmor = itemType is ItemType.Helmet or ItemType.Chest or ItemType.Legs or ItemType.Shoulder;
            if (repairMode == RepairMode.Armor && !isArmor) continue;
            if (repairMode == RepairMode.Items && isArmor) continue;
            if (itemData.m_shared.m_useDurability && itemData.m_durability < itemData.GetMaxDurability()) return true;
        }

        __instance.Message(MessageHud.MessageType.Center, "$noItemsToRepair");
        __result = false;
        return false;
    }

    [HarmonyPostfix]
    [UsedImplicitly]
    private static void Repair(Player __instance, bool __result, ItemDrop.ItemData item)
    {
        if (!__result) return;
        var repairMode = Utils.GetPrefabName(item.m_dropPrefab) switch
        {
            "JF_ItemsRepairKit" => RepairMode.Items,
            "JF_ArmorRepairKit" => RepairMode.Armor,
            _ => RepairMode.None
        };

        if (repairMode == RepairMode.None) return;
        foreach (var itemData in __instance.GetInventory().GetAllItems())
        {
            var shared = itemData.m_shared;
            var itemType = shared.m_itemType;

            if (!shared.m_useDurability) continue;
            var isArmor = itemType is ItemType.Helmet or ItemType.Chest or ItemType.Legs or ItemType.Shoulder or COSMETIC_CHEST or COSMETIC_HELMET or COSMETIC_LEGS or COSMETIC_CAPE;
            var isOther = !isArmor;
            float repairPercent = 0;
            if (isArmor && repairMode == RepairMode.Armor) repairPercent = ConfigsContainer.RepairPercentArmor;
            else if (isOther && repairMode == RepairMode.Items) repairPercent = ConfigsContainer.RepairPercentItems;
            else continue;

            itemData.m_durability += itemData.GetMaxDurability() * repairPercent / 100;
            itemData.m_durability = Mathf.Clamp(itemData.m_durability, 0, itemData.GetMaxDurability());
        }
    }
}