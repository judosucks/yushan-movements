using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachRectransform : MonoBehaviour
{
    [SerializeField] private RectTransform inventoryBarRect;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private Camera camera;

    private void Init()
    {

    }
    private void Awake()
    {


    }
    private void Update()
    {
        Vector3 screenPos = camera.ScreenToWorldPoint(this.transform.position);
        Vector2 screenPos2D = new Vector2(screenPos.x, screenPos.y);
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, screenPos2D, camera, out anchoredPos);
        inventoryBarRect.anchoredPosition = anchoredPos;

    }
}
