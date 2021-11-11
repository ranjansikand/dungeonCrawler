using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    Equipment _item;

    public void AddItem(Equipment newItem)
    {
        _item = newItem;

        icon.sprite = _item.icon;
        icon.enabled = true;
        removeButton.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        _item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.gameObject.SetActive(false);
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Unequip(_item);
    }
}
