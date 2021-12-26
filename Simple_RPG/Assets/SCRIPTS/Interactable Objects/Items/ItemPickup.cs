using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;


    public override void Interact()
    {
        base.Interact(); // Executes the base code in Interactable

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        //Add to inventory
        bool wasPickedUp = Inventory.instance.Add(item); // Checks if there is enough space in inventory.

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }



}
