using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       
    public Vector3 offset = new Vector3(0f, 2f, -5f); 
    public float smoothSpeed = 10f; 

    void LateUpdate()
    {
       
        Vector3 targetPos = target.position + offset;

        
        transform.position = Vector3.Lerp(
            transform.position, targetPos, smoothSpeed * Time.deltaTime
        );

        
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
