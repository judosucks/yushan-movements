using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAnimationControl : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        EventHandler.DarkComboEvent += SetAnimationParameters;
    }
    private void OnDisable()
    {
        EventHandler.DarkComboEvent -= SetAnimationParameters;
    }

    private void SetAnimationParameters(bool isDarkSpinKick, bool isDarkWhirlWindKick, bool isDarkSweepKick, bool isDarkDoubleSweepKick, bool isDarkSpinHeadKick)
    {
        if (isDarkSpinKick)
        {
            animator.SetTrigger(Settings.isDarkSpinKick);
        }
        if (isDarkWhirlWindKick)
        {
            animator.SetTrigger(Settings.isDarkWhirlWindKick);
        }
        if (isDarkSweepKick)
        {
            animator.SetTrigger(Settings.isDarkSweepKick);
        }
        if (isDarkDoubleSweepKick)
        {
            animator.SetTrigger(Settings.isDarkDoubleSweepKick);
        }
        if (isDarkSpinHeadKick)
        {
            animator.SetTrigger(Settings.isDarkSpinHeadKick);
        }
    }
}
