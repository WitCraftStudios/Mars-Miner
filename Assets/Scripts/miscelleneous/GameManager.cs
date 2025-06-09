using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coins = 0;
    public Text coinUIText; // UI Text to show coin count

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinUI();
        Debug.Log("Coins earned: " + amount + ", Total coins: " + coins);
    }

    void UpdateCoinUI()
    {
        if (coinUIText != null)
            coinUIText.text = "Coins: " + coins;
    }
}
