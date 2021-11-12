using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform equipmentParent;

    // player's inventory instance
    Inventory inventory;

    InventorySlot[] slots;
    EquipmentSlot[] equip;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        inventory.onEquipmentChangedCallback += UpdateEquipmentSlots;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        equip = equipmentParent.GetComponentsInChildren<EquipmentSlot>();

        UpdateUI();
        UpdateEquipmentSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else {
                slots[i].ClearSlot();
            }

            if (i >= inventory.space)
            {
                slots[i].HideSlot();
            }
        }
    }

    void UpdateEquipmentSlots()
    {
        for (int i = 0; i < equip.Length; i++)
        {
            if (inventory.equipped[i] != null) {
                equip[i].AddItem(inventory.equipped[i]);
            }
            else {
                equip[i].ClearSlot();
            }
        }
    }
}
