using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yushan.Enums;

using UnityEngine.UI;
public class UiInventoryBar : MonoBehaviour
{
    public Transform playerTransform;
    public RectTransform rectTransform;
    public RectTransform canvasRect;
    [SerializeField] private Sprite transparent64 = null;
    [SerializeField] private UiInventorySlot[] inventorySlot = null;
    public GameObject inventoryBarDraggedItem;
    [SerializeField] private Camera camera;
    public Vector3 screenPos;
    public Vector2 screenPos2D;
    public Vector2 anchoredPos;
    public Vector2 pivotPos;
    public Vector2 anchorPos;
    public Vector2 anchorPos2d;
    public Rect rect = new Rect(0, 0, 0, 0);

    private bool isRectransformShowsUp;
    public bool IsRectransformShowsUp { get => isRectransformShowsUp; set => isRectransformShowsUp = value; }
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        rectTransform = GetComponent<RectTransform>();
        screenPos = camera.WorldToViewportPoint(playerTransform.position);
        screenPos2D = new Vector2(screenPos.x, screenPos.y);
        pivotPos = camera.WorldToViewportPoint(rectTransform.anchoredPosition);


    }
    private void Start()
    {
        
        anchoredPos = rectTransform.anchoredPosition;
        anchorPos2d = new Vector2(anchoredPos.x, anchoredPos.y);
       
    }
    private void OnDisable()
    {
        EventHandler.InventoryUpdatedEvent -= InventoryUpdated;
    }
    private void OnEnable()
    {
        EventHandler.InventoryUpdatedEvent += InventoryUpdated;
    }
    private void Update()
    {
        //rectTransform.sizeDelta = new Vector2(playerTransform.position.x + 10f, playerTransform.position.y + 10f);




    }

    private void ClearInventorySlots()
    {
        if (inventorySlot.Length > 0)
        {
            // loop through inventory slots and update with blank sprite
            for (int i = 0; i < inventorySlot.Length; i++)
            {
                inventorySlot[i].inventorySlotImage.sprite = transparent64;
                inventorySlot[i].textMeshProUGUI.text = "";
                inventorySlot[i].itemDetails = null;
                inventorySlot[i].itemQuantity = 0;
            }
        }
    }
    private void InventoryUpdated(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (inventoryLocation == InventoryLocation.player)
        {
            Debug.Log("inventorylocation == inventorylocation.player" + InventoryLocation.player);
            ClearInventorySlots();

            if (inventorySlot.Length > 0 && inventoryList.Count > 0)
            {
                //loop through inventory slots and update with corresponding inventopry list item
                for (int i = 0; i < inventorySlot.Length; i++)
                {
                    Debug.Log("inventoryslot.length" + inventorySlot.Length);
                    if (i < inventoryList.Count)
                    {
                        int itemCode = inventoryList[i].itemCode;

                        // itemdetails itemdetails = InventoryManager.Instance.

                        ItemDetails itemDetails = InventeryManager.Instance.GetItemDetails(itemCode);
                        if (itemDetails != null)
                        {
                            // add images and details to inventory item slot

                            inventorySlot[i].inventorySlotImage.sprite = itemDetails.itemSprite;
                            inventorySlot[i].textMeshProUGUI.text = inventoryList[i].itemQuantity.ToString();
                            inventorySlot[i].itemDetails = itemDetails;
                            inventorySlot[i].itemQuantity = inventoryList[i].itemQuantity;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
            }
        }





    }
}
