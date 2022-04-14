using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SwitchBoundingShape : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SwitchConfinerShape();
    }

    private void SwitchConfinerShape()
    {
        // get the polygon 2d

        PolygonCollider2D polygonCollider2D = GameObject.FindGameObjectWithTag(Tags.BoundsConfiner).GetComponent<PolygonCollider2D>();
        CinemachineConfiner cinemachineConfiner = GetComponent<CinemachineConfiner>();
        cinemachineConfiner.m_BoundingShape2D = polygonCollider2D;
        //clear the cache
        cinemachineConfiner.InvalidatePathCache();
    }

}
