using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCombatAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        EventHandler.DarkCombatEvent += SetDarkCombatAnimationParameter;
    }
    private void OnDisable()
    {
        EventHandler.DarkCombatEvent -= SetDarkCombatAnimationParameter;
    }

    private void SetDarkCombatAnimationParameter(bool isDarkDoubleSpearKick, bool isDarkKneeKick, bool isDarkCrossKick)
    {
        if (isDarkDoubleSpearKick)
        {
            animator.SetTrigger(Settings.isDarkDoubleSpearKick);
        }
        if (isDarkKneeKick)
        {
            animator.SetTrigger(Settings.isDarkKneeKick);
        }
        if (isDarkCrossKick)
        {
            animator.SetTrigger(Settings.isDarkCrossKick);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
