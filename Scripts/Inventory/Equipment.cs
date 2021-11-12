using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentType type;

    public override void Use()
    {
        base.Use();

        Inventory.instance.Equip(this);
    }
}