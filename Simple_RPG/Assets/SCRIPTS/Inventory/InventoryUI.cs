using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onitemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Inventory")) // You can create custom Inputs
        {
            // Code below SetsActive to the opposite bool state of the current one
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        //Debug.Log("UPDATING UI"); // DELETE later

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

    }
}
