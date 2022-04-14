using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.3f;
    private float alphaMultiplier = 0.85f;

    private Transform player;

    private SpriteRenderer sr;
    private SpriteRenderer playerSr;

    [SerializeField] private Color color;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSr = player.GetComponentInChildren<SpriteRenderer>();

        alpha = alphaSet;
        sr.sprite = playerSr.sprite;
        transform.position = (player.position - new Vector3(.5f, .5f, 0f));
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(71f, 0f, 0f, alpha);
        sr.color = color;
        if (Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
