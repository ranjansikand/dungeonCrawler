using UnityEngine;

public class Door : MonoBehaviour, IInteractive
{
    [SerializeField] GameObject disappearEffect;
    [SerializeField] GameObject lockEffect;

    public void Interact()
    {
        if (gameObject.activeSelf) {
            // if object is active, set to not active
            if (disappearEffect != null) {
                Instantiate(disappearEffect, transform.position + Vector3.up, transform.rotation);
            }
            gameObject.SetActive(false);
        } else {
            // if object not active, set active
            if (lockEffect != null) {
                Instantiate(lockEffect, transform.position + Vector3.up, transform.rotation);
            }
            gameObject.SetActive(true);
        }
    }
}
