using BepInEx.Configuration;

namespace RepairKit.Config;

public partial class ConfigsContainer
{
    public static float RepairPercentItems => Instance._repairPercentItems.Value;
    public static float RepairPercentArmor => Instance._repairPercentArmor.Value;

    internal readonly ConfigEntry<float> _repairPercentItems;
    internal readonly ConfigEntry<float> _repairPercentArmor;


    private ConfigsContainer()
    {
        _repairPercentItems = config("General", "Items kit repair percent", 20f, "How much percent of durability is repaired for items");
        _repairPercentArmor = config("General", "Armor kit repair percent", 20f, "How much percent of durability is repaired for armor");
    }

    private void ApplyConfiguration()
    {
    }
}