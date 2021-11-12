using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image panel;
    public Button removeButton;

    Item _item;

    public void AddItem(Item newItem)
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

    public void HideSlot()
    {
        ClearSlot();

        panel.color = new Color(0, 0, 0, 0);
    }

    public void OnRemoveButton()
    {
        Instantiate(_item.prefab, 
            Inventory.instance.gameObject.transform.position + (2*Inventory.instance.gameObject.transform.forward) + _item.basePosition, 
            Quaternion.identity);
        Inventory.instance.Remove(_item);
    }

    public void UseItem()
    {
        if (_item != null)
        {
            _item.Use();
            Inventory.instance.Remove(_item);
        }
    }
}
