using RepairKit.Config;
using RepairKit.Managers.ItemManager;
using RepairKit.Managers.LocalizationManager;

namespace RepairKit;

[BepInEx.BepInPlugin(Consts.ModGuid, Consts.ModName, Consts.ModVersion)]
public class Plugin : BepInEx.BaseUnityPlugin
{
    private void Awake()
    {
        Log.InitializeConfiguration(this);
        new Harmony(Consts.ModGuid).PatchAll();
        ConfigsContainer.InitializeConfiguration(this);

        var itemsRepairKit = new Item("repairkit", "JF_ItemsRepairKit");
        itemsRepairKit.Crafting.Add(CraftingTable.Workbench, 2);
        itemsRepairKit.RequiredItems.Add("FineWood", 5);
        itemsRepairKit.RequiredItems.Add("Ruby", 1);
        itemsRepairKit.CraftAmount = 2;

        var armorRepairKit = new Item("repairkit", "JF_ArmorRepairKit");
        armorRepairKit.Crafting.Add(CraftingTable.Workbench, 2);
        armorRepairKit.RequiredItems.Add("FineWood", 5);
        armorRepairKit.RequiredItems.Add("Ruby", 1);
        armorRepairKit.CraftAmount = 2;

        Localizer.Load();
        Localizer.AddPlaceholder("item_desc_ItemsRepairKit", "repairPercent", ConfigsContainer.Instance._repairPercentItems);
        Localizer.AddPlaceholder("item_desc_ArmorRepairKit", "repairPercent", ConfigsContainer.Instance._repairPercentArmor);
    }
}
