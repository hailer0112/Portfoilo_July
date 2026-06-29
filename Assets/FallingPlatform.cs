using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float delay = 1.5f;
    public float shakeAmount = 0.05f;

    private bool isTriggered = false;
    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            StartCoroutine(FallSequence());
        }
    }

    IEnumerator FallSequence()
    {
        float elapsed = 0f;
        while (elapsed < delay)
        {
            float shakeX = Random.Range(-shakeAmount, shakeAmount);
            transform.position = originalPos + new Vector3(shakeX, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}