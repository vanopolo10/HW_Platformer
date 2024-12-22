using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    private TextMeshProUGUI  _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _text.text = _wallet.Coins.ToString();
    }

    private void OnEnable()
    {
        _wallet.MoneyChanged += DrawMoney;
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= DrawMoney;
    }

    private void DrawMoney(int money)
    {
        _text.text = money.ToString();
    }
}
