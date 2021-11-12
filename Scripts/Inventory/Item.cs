using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public GameObject prefab;
    public Sprite icon = null;
    public Vector3 basePosition = new Vector3(0,1,0);

    public virtual void Use()
    {
        // Use the item if the item is usable

        Debug.Log("Using " + name);
    }
}
