using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager :MonoBehaviour
{
    
    
    public Player player { get; private set; }
    private void Awake()
    {
        
        player = GetComponentInParent<Player>();
        
    }
    public void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    public void AnimatoinFinishTrigger()
    {
       
        player.AnimationFinishTrigger();
        Debug.Log("animationmanager");
    }

    }
