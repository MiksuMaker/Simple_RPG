using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found! You did something WROOONG");
            return;
        }
        instance = this;
    }
    #endregion // Singleton

    // DELEGATE Stuff
    public delegate void OnItemChanged();
    public OnItemChanged onitemChangedCallback;


    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if (!item.isDefaultItem) // Disqualifies default Items
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room in inventory.");
                return false;
            }

            items.Add(item);

            if (onitemChangedCallback != null)
                onitemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if (onitemChangedCallback != null)
            onitemChangedCallback.Invoke();
    }

}
