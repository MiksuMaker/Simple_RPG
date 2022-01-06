using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;

    private void Start()
    {
        // Code below gets the .Length of Enum EquipmentSlot    (from Equipment.cs)
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        /// CONTINUE FROM BRACKEYS - EQUIPMENT  9:08  -->

        currentEquipment[slotIndex] = newItem;
    }
}
