using TMPro;
using UnityEngine;

public abstract class ValueDisplay : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI m_valueTMP;

    protected void OnValueUpdate(int currentValue)
    {
        m_valueTMP.text = currentValue.ToString();
    }

    protected abstract void CheckValue();
}
