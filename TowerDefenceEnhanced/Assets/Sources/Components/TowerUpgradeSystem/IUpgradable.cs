public interface IUpgradable
{
    void Upgrade();
    void Sell();
    float GetUpgradePrice();
    float GetSellPrice();

    bool HasUpgrade();

    bool IsUpgradable();
}
