using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera_Platform : MonoBehaviour
{
    [SerializeField] [Tooltip("target")]
    Transform target = null;
    [SerializeField] [Tooltip("lerpTime")]
    float lerpTime = 1;

    Vector3 initialPosition;

    public float offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = initialPosition;
        targetPosition.x += target.position.x;
        targetPosition.y += target.position.y;
        targetPosition.z = target.position.z + offsetZ;


        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpTime);
    }
}
