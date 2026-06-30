using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float delay = 1.5f;
    public float shakeAmount = 0.05f;
    public float respawnTime = 3f;

    private bool isTriggered = false;
    private bool playerOnPlatform = false;
    private Vector3 originalPos;
    private Transform playerTransform;
    private CharacterController playerCC;
    private Rigidbody rb;
    private MeshRenderer rend;
    private Collider[] cols;

    void Start()
    {
        originalPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<MeshRenderer>();
        cols = GetComponents<Collider>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            playerTransform = col.transform;
            playerCC = col.GetComponent<CharacterController>();
            StartCoroutine(FallSequence());
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    IEnumerator FallSequence()
    {
        playerOnPlatform = true;

        float elapsed = 0f;
        while (elapsed < delay)
        {
            float shakeX = Random.Range(-shakeAmount, shakeAmount);
            transform.position = originalPos + new Vector3(shakeX, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;

        if (!playerOnPlatform)
        {
            rb.isKinematic = false;
        }
        else
        {
            if (playerCC != null) playerCC.enabled = false;
            if (playerTransform != null) playerTransform.SetParent(transform);

            rb.isKinematic = false;

            yield return new WaitForSeconds(1f);
            if (playerTransform != null)
            {
                playerTransform.SetParent(null);
                if (playerCC != null) playerCC.enabled = true;
            }
        }

        yield return new WaitForSeconds(2f);
        rend.enabled = false;
        foreach (var c in cols) c.enabled = false;

        yield return new WaitForSeconds(respawnTime);
        ResetPlatform();
    }

    void ResetPlatform()
    {
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.position = originalPos;
        transform.position = originalPos;
        transform.rotation = Quaternion.identity;

        rb.isKinematic = true;

        rend.enabled = true;
        foreach (var c in cols) c.enabled = true;

        // 다시 밟을 수 있도록 초기화
        isTriggered = false;
        playerOnPlatform = false;
        playerTransform = null;
        playerCC = null;
    }
}