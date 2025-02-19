using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private float smoothSpeed = 2f;


    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x + posOffset.x, target.position.y + posOffset.y, target.position.z + posOffset.z);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);

        Quaternion desiredRotation = target.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
        transform.rotation = smoothedRotation;
    }
}
