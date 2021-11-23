using System.Collections.Generic;
using UnityEngine;

public class Widget : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();

    List<IInteractive> boundObjects = new List<IInteractive>();

    void Start() {
        for (int i = 0; i < objects.Count; i++)
        {
            boundObjects.Add(objects[i].GetComponent<IInteractive>());
        }
    }

    public void Interact() {
        for (int i = 0; i < boundObjects.Count; i++) {
            boundObjects[i].Interact();
        }
    }
}
