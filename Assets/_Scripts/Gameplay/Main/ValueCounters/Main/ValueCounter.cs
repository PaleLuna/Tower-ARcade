using PaleLuna.Architecture.Services;

public abstract class ValueCounter : IService
{
    protected int m_currentValue = 0;

    public int currentValue => m_currentValue;


    public void Add(int value) => SetCurrentValue(m_currentValue + value);

    public bool TryTake(int value) 
    {
        int upcomingBalance = m_currentValue - value;
        bool isPositiveBalance = upcomingBalance >= 0;

        if(isPositiveBalance) SetCurrentValue(upcomingBalance);

        return isPositiveBalance;
    }

    public void SetCurrentValue(int value)
    {
        m_currentValue = value;

        InvokeEventOnChange();
    }

    protected abstract void InvokeEventOnChange();
}
