using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public TextMeshProUGUI coinText;
    private int collectedCoins = 0;
    public int totalCoins;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 씬에 있는 코인 개수 자동 계산
        totalCoins = FindObjectsOfType<Coin>().Length;
        UpdateUI();
    }

    public void CollectCoin()
    {
        collectedCoins++;
        UpdateUI();

        if (collectedCoins >= totalCoins)
        {
            Debug.Log("모든 코인 수집 완료!");
            // 여기에 출구 활성화 코드 추가 가능
        }
    }

    void UpdateUI()
    {
        coinText.text = $"코인: {collectedCoins} / {totalCoins}";
    }
}
