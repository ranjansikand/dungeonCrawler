// This class carries functions that are called by animation events
// This performs no behavior on its own; all behavior is controlled
// by the enemy state machine.

using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] Transform firePoint;
    [SerializeField] Aim firePointAim;
    GameObject currentArrow;

    [SerializeField] GameObject displayArrow;

    // called by animation events
    public void GenerateArrow()
    {
        displayArrow.SetActive(true);
    }

    public void FireArrow()
    {
        displayArrow.SetActive(false);
        
        currentArrow = Instantiate(arrow, firePoint.position, Quaternion.identity);
        currentArrow.transform.LookAt(firePoint.forward);
        currentArrow.GetComponent<Rigidbody>().AddForce(firePoint.forward * 30f, ForceMode.Impulse);
    }
}
