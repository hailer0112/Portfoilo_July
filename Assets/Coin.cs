using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 90f;   // 회전 속도

    void Update()
    {
        // 제자리에서 빙글빙글 회전
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.Instance.CollectCoin();
            Destroy(gameObject);
        }
    }
}
