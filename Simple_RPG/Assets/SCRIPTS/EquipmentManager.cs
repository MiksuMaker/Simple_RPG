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


    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;

        // Code below gets the .Length of Enum EquipmentSlot    (from Equipment.cs)
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        //Equipment oldItem = currentEquipment[slotIndex];  // HOW I did it first (worked too)
        //Equipment oldItem = null;   // HOW Brackeys did it  (1/2)
        Equipment oldItem = Unequip(slotIndex);   // HOW Sebastian Lague modified it

        // Check if there is already an item equipped
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];    //    (2/2)
            //inventory.Add(oldItem);
            Unequip(slotIndex);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        SetEquipmentBlendShapes(newItem, 100);  // Sets the correct BlendShapes for armor in question

        currentEquipment[slotIndex] = newItem;
        // HUOM!!   Alla oleva koodi (+ alusteet t‰m‰n luokan alussa)
        //          lis‰‰ pukujen meshit, transformit ja luut oikein
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject); // Poistaa nykyisen meshin
            }

            // Add the old item back to inventory
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            // Sets the Blendshape back to normal
            SetEquipmentBlendShapes(oldItem, 0);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    public void UnequipEverything()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendshape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendshape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    private void Update()   // Checks for Unequip button prompt
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipEverything();
    }
}
