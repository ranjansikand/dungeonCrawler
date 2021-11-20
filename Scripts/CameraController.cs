using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float minDistance = 7, maxDistance = 14;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float cameraSpeed = 1.0f;

    void Start()
    {
        if (player == null) player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void LateUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (!(distance > minDistance && distance < maxDistance))
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        Vector3 finalPosition = player.position + cameraOffset;
        Vector3 lerpPosition = Vector3.Lerp (transform.position, finalPosition, cameraSpeed);
        transform.position = lerpPosition;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
