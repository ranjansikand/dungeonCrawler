using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple inventories found!");
            return;
        }

        instance = this;
    }
    #endregion


    // event that tracks item changes
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public OnItemChanged onEquipmentChangedCallback;

    // inventory control stats
    [Header("---Item inventory---")]
    public GameObject inventory;
    public List<Item> items = new List<Item>();
    public int space = 20;

    [Header("---Equipment Inventory---")]
    Equipment bubbleSlot;
    Equipment amulet, chest, rune, boots;
    public Equipment[] equipped = new Equipment[4];

    bool inventoryEnabled;


    // called by input
    public void ToggleInventory()
    {
        inventoryEnabled = !inventoryEnabled;
        inventory.SetActive(inventoryEnabled);

        if (inventoryEnabled) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    // adding and removing objects from ITEM LIST
    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("No room!");
            return false;
        }

        items.Add(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }

    // adding and removing items from equipment list
    public void Equip(Equipment equipment)
    {
        switch (equipment.type) {
            case EquipmentType.amulet:
                ReplaceEquipment(equipment, 0);
                break;
            case EquipmentType.chest:
                ReplaceEquipment(equipment, 1);
                break;
            case EquipmentType.rune:
                ReplaceEquipment(equipment, 2);
                break;
            case EquipmentType.boots:
                ReplaceEquipment(equipment, 3);
                break;
            default:
                Debug.Log("Could not equip " + equipment);
                break;
        }

        if (onEquipmentChangedCallback != null) onEquipmentChangedCallback.Invoke();
    }

    void ReplaceEquipment(Equipment item, int slot)
    {
        if (equipped[slot] != null) {
            bubbleSlot = equipped[slot];
            Add(bubbleSlot);
        }

        equipped[slot] = item;
    }

    public void Unequip(Equipment equipment)
    {
        bool returnToInventory = Add(equipment);

        if (returnToInventory == false) return;

        switch (equipment.type) {
            case EquipmentType.amulet:
                equipped[0] = null;
                break;
            case EquipmentType.chest:
                equipped[1] = null;
                break;
            case EquipmentType.rune:
                equipped[2] = null;
                break;
            case EquipmentType.boots:
                equipped[3] = null;
                break;
            default:
                Debug.Log("Could not unequip " + equipment);
                break;
        }
        if (onEquipmentChangedCallback != null) onEquipmentChangedCallback.Invoke();
    }

    public int RunicAttack()
    {
        if (equipped[2] != null) {
            return equipped[2].animation;
        } else {
            return 0;
        }
    }
}
