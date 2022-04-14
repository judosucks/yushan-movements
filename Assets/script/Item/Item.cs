using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yushan.Enums;
public class Item : MonoBehaviour
{
    [ItemCodeDescription]
    [SerializeField] private int _itemCode;

    private SpriteRenderer spriteRenderer;

    public int ItemCode { get { return _itemCode; } set { _itemCode = value; } }


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        if (ItemCode != 0)
        {
            Init(ItemCode);
        }
    }
    public void Init(int itemCodeParam)
    {
        if (itemCodeParam != 0)
        {
            ItemCode = itemCodeParam;
            ItemDetails itemDetails = InventeryManager.Instance.GetItemDetails(ItemCode);

            spriteRenderer.sprite = itemDetails.itemSprite;

            // if item type is reapable then add nudge class
            if (itemDetails.itemType == ItemType.Reapable_Scenary)
            {
                gameObject.AddComponent<ItemNudge>();
            }
        }
    }
}
