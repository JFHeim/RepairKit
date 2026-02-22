using BepInEx;
using BepInEx.Configuration;
using RepairKit.Managers.ServerSync;

// ReSharper disable InconsistentNaming

namespace RepairKit.Config;

public partial class ConfigsContainer
{
    public static ConfigsContainer Instance
    {
        get
        {
            System.Diagnostics.Debug.Assert(IsInitialized == true);
            return field;
        }
        private set;
    } = null!;
    private static bool IsInitialized = false;
    private static BaseUnityPlugin Plugin = null!;
    private static Action? OnConfigurationChanged;
    private static DateTime LastConfigChange = DateTime.MinValue;
    private static readonly HashSet<string> ConfigFilesToWatch = [];
    private static ConfigSync? _configSync;

    public static void InitializeConfiguration(BaseUnityPlugin plugin)
    {
        Plugin = plugin;
        plugin.Config.SaveOnConfigSet = false;
        _configSync = new ConfigSync(Consts.ModGuid)
            { DisplayName = Consts.ModName, CurrentVersion = Consts.ModVersion, MinimumRequiredVersion = Consts.ModVersion };
        Instance = new ConfigsContainer();
        ConfigFilesToWatch.Add($"{Plugin.Info.Metadata.GUID}.cfg");
        SetupWatcher();
        plugin.Config.SaveOnConfigSet = true;
        plugin.Config.ConfigReloaded += (_, _) => UpdateConfiguration();
        plugin.Config.Save();

        OnConfigurationChanged += () =>
        {
            Log.Info("Configuration Received");
            Instance.ApplyConfiguration();
            Log.Info("Configuration applied");
        };

        IsInitialized = true;
    }

    private static void SetupWatcher()
    {
        foreach (var fileName in ConfigFilesToWatch.Where(x=> !string.IsNullOrEmpty(x)))
        {
            FileSystemWatcher fileSystemWatcher = new(Paths.ConfigPath, fileName);
            fileSystemWatcher.Changed += ConfigChanged;
            fileSystemWatcher.Created += ConfigChanged;
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            fileSystemWatcher.EnableRaisingEvents = true;
        }
    }

    private static void ConfigChanged(object sender, FileSystemEventArgs e)
    {
        if ((DateTime.Now - LastConfigChange).TotalSeconds <= 2) return;
        LastConfigChange = DateTime.Now;

        try
        {
            Plugin.Config.Reload();
        }
        catch
        {
            Log.Error("Unable reload config");
        }
    }

    private static void UpdateConfiguration()
    {
        try
        {
            OnConfigurationChanged?.Invoke();
        }
        catch (Exception e)
        {
            Log.Error(e, "Configuration error", false);
        }
    }

    public static ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description, bool synchronizedSetting = true)
    {
        var configEntry = Plugin.Config.Bind(group, name, value, description);
        if (_configSync == null)
            Log.Error("ConfigsContainer is not initialized properly, configSync found to be null");
        else
        {
            SyncedConfigEntry<T> syncedConfigEntry = _configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;
        }

        return configEntry;
    }

    public static ConfigEntry<T> config<T>(string group, string name, T value, string description, bool synchronizedSetting = true) =>
        config(group, name, value, new ConfigDescription(description), synchronizedSetting);
}