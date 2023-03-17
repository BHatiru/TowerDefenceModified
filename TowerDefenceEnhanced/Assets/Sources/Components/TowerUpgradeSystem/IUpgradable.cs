public interface IUpgradable
{
    void Upgrade();
    float GetUpgradePrice();

    bool HasUpgrade();

    bool IsUpgradable();
}
