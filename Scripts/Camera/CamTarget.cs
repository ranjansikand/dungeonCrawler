// Controls the position of the lock-on camera

using UnityEngine;

public class CamTarget : MonoBehaviour
{
    public GameObject player;
    public float cameraDistance = 10.0f;

    void LateUpdate ()
    {
        transform.position = player.transform.position - player.transform.forward * cameraDistance;
        transform.LookAt (player.transform.position);
        transform.position = new Vector3 (transform.position.x, transform.position.y + 5, transform.position.z);
    }
}
