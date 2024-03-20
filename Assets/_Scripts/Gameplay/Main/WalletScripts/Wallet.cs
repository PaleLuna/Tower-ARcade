using PaleLuna.Architecture.Services;

public class Wallet : IService
{
    private int _currentBalance = 0;

    public int currentBalance => _currentBalance;


    public void AddToWallet(int value) => SetCurrentBalance(_currentBalance + value);

    public bool TryTakeFromWallet(int value) 
    {
        int upcomingBalance = _currentBalance - value;
        bool isPositiveBalance = upcomingBalance >= 0;

        if(isPositiveBalance) SetCurrentBalance(upcomingBalance);

        return isPositiveBalance;
    }

    public void SetCurrentBalance(int balance)
    {
        _currentBalance = balance;

        GameEvents.walletBalanceChangedEvent.Invoke(_currentBalance);
    }
}
