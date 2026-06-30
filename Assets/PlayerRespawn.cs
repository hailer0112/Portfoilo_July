using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public float fallLimitY = -10f;   // 이 높이 아래로 떨어지면 리스폰
    public Transform respawnPoint;     // 리스폰 위치

    private CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (transform.position.y < fallLimitY)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        cc.enabled = false; // 위치 강제 이동 위해 잠깐 끄기
        transform.position = respawnPoint.position;
        cc.enabled = true;
    }
}
