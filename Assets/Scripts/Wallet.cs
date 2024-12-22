using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Wallet : MonoBehaviour
{
    public event Action<int> MoneyChanged;

    public int Coins { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            Coins += coin.Cost;
            Destroy(coin.gameObject);
            MoneyChanged?.Invoke(Coins);
        }
    }
}
