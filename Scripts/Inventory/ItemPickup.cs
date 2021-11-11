using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    [SerializeField] float radius;

    static Collider[] objectsBuffer = new Collider[100];
    int playerLayerMask = 1 << 10;

    void Update()
    {
        int hits = Physics.OverlapSphereNonAlloc(
                transform.position, radius, objectsBuffer, playerLayerMask, QueryTriggerInteraction.Collide
                );

        if (hits > 0)
        {
            Interact();
        }
    }

    public void Interact()
    {
        PickUp();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp) Destroy(gameObject);
    }
}
