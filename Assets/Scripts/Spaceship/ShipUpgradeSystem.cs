using UnityEngine;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

/// <summary>
/// Manages ship upgrades and their visual representation
/// Upgrades modify ship appearance and performance
/// </summary>
public class ShipUpgradeSystem : MonoBehaviour
{
    public enum UpgradeType
    {
        Engine,
        Armor,
        Cargo,
        Weapon,
        Shield
    }

    [System.Serializable]
    public class ShipUpgrade
    {
        public UpgradeType type;
        public int level;
        public string upgradeName;
        public float performanceBoost;
        public Color visualModifier = Color.white;
    }

    private ProceduralSpaceshipGenerator.ShipConfig baseConfig;
    private List<ShipUpgrade> installedUpgrades = new List<ShipUpgrade>();
    private GameObject shipMesh;

    public const int MAX_UPGRADE_SLOTS = 2;

    public ShipUpgradeSystem(ProceduralSpaceshipGenerator.ShipConfig config)
    {
        baseConfig = config;
    }

    /// <summary>
    /// Installs an upgrade to the ship (max 2 for starting ships)
    /// </summary>
    public bool InstallUpgrade(ShipUpgrade upgrade)
    {
        if (installedUpgrades.Count >= MAX_UPGRADE_SLOTS)
        {
            Debug.Log("Cannot install more upgrades. Max slots reached.");
            return false;
        }

        installedUpgrades.Add(upgrade);
        ApplyUpgradeVisuals(upgrade);
        return true;
    }

    /// <summary>
    /// Removes an upgrade from the ship
    /// </summary>
    public bool UninstallUpgrade(int upgradeIndex)
    {
        if (upgradeIndex < 0 || upgradeIndex >= installedUpgrades.Count)
            return false;

        installedUpgrades.RemoveAt(upgradeIndex);
        return true;
    }

    /// <summary>
    /// Applies visual changes based on upgrade type
    /// </summary>
    private void ApplyUpgradeVisuals(ShipUpgrade upgrade)
    {
        switch (upgrade.type)
        {
            case UpgradeType.Engine:
                // Larger engine glow and trail
                Debug.Log($"Installed Engine Upgrade: {upgrade.upgradeName}");
                break;
            case UpgradeType.Armor:
                // Darker hull, additional plating visible
                Debug.Log($"Installed Armor Upgrade: {upgrade.upgradeName}");
                break;
            case UpgradeType.Cargo:
                // Larger cargo module visible
                Debug.Log($"Installed Cargo Upgrade: {upgrade.upgradeName}");
                break;
            case UpgradeType.Weapon:
                // Weapon hardpoint visible
                Debug.Log($"Installed Weapon Upgrade: {upgrade.upgradeName}");
                break;
            case UpgradeType.Shield:
                // Shield generator glow
                Debug.Log($"Installed Shield Upgrade: {upgrade.upgradeName}");
                break;
        }
    }

    public List<ShipUpgrade> GetInstalledUpgrades()
    {
        return new List<ShipUpgrade>(installedUpgrades);
    }

    public int GetUpgradeSlotsFilled()
    {
        return installedUpgrades.Count;
    }

    public int GetUpgradeSlotsAvailable()
    {
        return MAX_UPGRADE_SLOTS - installedUpgrades.Count;
    }
}
