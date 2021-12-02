using UnityEngine;

public class HeldButton : Widget
{
    void OnTriggerEnter(Collider other)
    {
        base.Interact();
    }

    void OnTriggerExit(Collider other) 
    {
        base.Interact();
    }
}
