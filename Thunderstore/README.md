<img src="https://thunderstore.io/thumbnail-serve/repository/icons/Frogger-RepairKit-0.1.1.png/?width=128&height=128" align="right" alt="Project Logo" style="border-radius: 10px;">

# RepairKit

Portable repair kits for Valheim — fix your gear in the field without running back to the workbench.

## About

RepairKit adds two consumable toolbox items that restore durability to your equipment on the go. One kit handles weapons and tools, the other handles armor and capes. Both are craftable at a level 2 Workbench.

If all relevant items are already at full durability, the kit won't be consumed.

## How It Works

When you consume a repair kit, the mod iterates over your inventory and restores a configurable percentage of max durability to each applicable item:

- **Items Repair Kit** — affects weapons, tools, and everything that isn't armor
- **Armor Repair Kit** — affects helmets, chestplates, leggings, capes (including cosmetic slots)

Durability is clamped to the item's maximum — you can't overheal.

### Crafting

| Kit | Materials | Result |
|-----|-----------|--------|
| Items Repair Kit | 5 Fine Wood + 1 Ruby | ×2 |
| Armor Repair Kit | 5 Fine Wood + 1 Ruby | ×2 |

### Configuration

Configuration values can be changed while in-game using a Configuration Manager mod. Otherwise, please
open the config file created in the ``BepInEx/config`` folder to manually modify configurable values.

```cfg
## Settings file was created by plugin RepairKit v0.1.0
## Plugin GUID: com.Frogger.RepairKit

[General]

## How much percent of durability is repaired for items
# Setting type: Single
# Default value: 20
Items kit repair percent = 20

## How much percent of durability is repaired for armor
# Setting type: Single
# Default value: 20
Armor kit repair percent = 20
```

### Install Notes

The mod runs entirely on the client. Server installation is not required, but recommended for configuration synchronization.


### Credits

<img alt="Discord Logo" src="https://freelogopng.com/images/all_img/1691730813discord-icon-png.png" width="16"> Discord — `justafrogger`<br>
<img alt="GitHub Logo" src="https://github.githubassets.com/assets/pinned-octocat-093da3e6fa40.svg" width="16"/> <a href="https://github.com/JFHeim/RepairKit">Source Code</a><br>

<ins> If something does not work for you, you have found any bugs, there are any suggestions, then be sure to write to me!</ins>

This mod was commissioned. <br>
Also, many thanks to Opus 4.6 for the help with writing this text


## Screenshots

![screenshot 1](https://github.com/JFHeim/RepairKit/blob/main/Thunderstore/img/screenshot_1.jpeg?raw=true)<br>
![screenshot 2](https://github.com/JFHeim/RepairKit/blob/main/Thunderstore/img/screenshot_2.jpeg?raw=true)<br>
![screenshot 3](https://github.com/JFHeim/RepairKit/blob/main/Thunderstore/img/screenshot_3.jpeg?raw=true)<br>