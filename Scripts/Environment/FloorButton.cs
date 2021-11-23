using UnityEngine;

public class FloorButton : Widget
{
    void OnTriggerEnter(Collider other)
    {
        base.Interact();
    }
}
