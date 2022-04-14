using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiInventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera mainCamera;
    private Transform parentItem;
    private GameObject draggedItem;


    public Image inventorySlotHightlight;
    public Image inventorySlotImage;
    public TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private UiInventoryBar inventoryBar = null;
    [SerializeField] private GameObject itemPrefeb = null;
    [HideInInspector] public ItemDetails itemDetails;
    [HideInInspector] public int itemQuantity;

    private void Start()
    {
        mainCamera = Camera.main;
        parentItem = GameObject.FindGameObjectWithTag(Tags.ItemParentTransform).transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
