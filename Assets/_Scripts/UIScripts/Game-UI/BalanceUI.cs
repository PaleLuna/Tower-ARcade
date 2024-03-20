using Services;
using TMPro;
using UnityEngine;

public class BalanceUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _balanceValue;

    private void OnEnable() {
        GameEvents.walletBalanceChangedEvent.AddListener(OnUpdateBalance);
        CheckBalance();
    }
    private void OnDisable()
    {
        GameEvents.walletBalanceChangedEvent.RemoveListener(OnUpdateBalance);
    }

    private void OnUpdateBalance(int currentBalance)
    {
        _balanceValue.text = currentBalance.ToString();
    } 

    private void CheckBalance() =>
        OnUpdateBalance(ServiceManager.Instance.SceneLocator.Get<Wallet>().currentBalance);

}
