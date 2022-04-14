using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yushan.Enums;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {


        Item item = collision.GetComponent<Item>();

        if (item != null)
        {
            ItemDetails itemDetails = InventeryManager.Instance.GetItemDetails(item.ItemCode);

            if (itemDetails.canBePickedUp == true)
            {
                //add item to inventory
                InventeryManager.Instance.AddItem(InventoryLocation.player, item, collision.gameObject);
            }
        }
    }
}
