using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TowerOptionsPanel : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private TowerUpgradeSystem _upgradeSystem;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _upgradeButton.onClick.AddListener(NotyfiEventUpgrade);
        _sellButton.onClick.AddListener(NotyfiEventSell);
        SceneEventSystem.Instance.BalanceChanged += OnBalanceChanged;
    }

    private void OnDestroy()
    {
        SceneEventSystem.Instance.BalanceChanged -= OnBalanceChanged;
    }

    private void OnBalanceChanged(float obj)
    {
        SetButtonsInteractivity();
    }

    private void NotyfiEventUpgrade()
    {
        SceneEventSystem.Instance.NotifyUpgradeButtonPressed();
    }
    private void NotyfiEventSell(){
        SceneEventSystem.Instance.NotifySellButtonPressed();
    }

    public void Show()
    {
        _animator.SetBool("show", true);
        SetButtonsInteractivity();
    }

    public void Hide()
    {
        _animator.SetBool("show", false);
    }

    private void SetButtonsInteractivity()
    {
        _upgradeButton.interactable = _upgradeSystem.SelectedHasUpgrade() && ResourceSystem.HasEnoughMoney(_upgradeSystem.GetSelectedUpgradePrice());
    }
}
