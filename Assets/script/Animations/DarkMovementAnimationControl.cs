using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMovementAnimationControl : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        EventHandler.DarkMovementEvent += SetDarkAnimationParameter;
    }
    private void OnDisable()
    {
        EventHandler.DarkMovementEvent -= SetDarkAnimationParameter;
    }
    private void SetDarkAnimationParameter(float movX, float movY, bool isRunning,bool isSprint, bool isDashing, bool isDarkSpinBack, bool isJumping, bool isFalling,bool isLanding, bool isSprintJump, bool isSprintFall, bool isRunningJump, bool isRunningFall, bool isWallGrab, bool isWallJumping, bool isWallFall, bool isIdle, bool isDarkPowerUp)
    {
        animator.SetFloat(Settings.movX, movX);
        animator.SetFloat(Settings.movY, movY);
       
        animator.SetBool(Settings.isRunning, isRunning);
        animator.SetBool(Settings.isSprint, isSprint);
        animator.SetBool(Settings.isDashing, isDashing);
        animator.SetBool(Settings.isJumping, isJumping);
        animator.SetBool(Settings.isFalling, isFalling);
        animator.SetBool(Settings.isSprintJump, isSprintJump);
        animator.SetBool(Settings.isSprintFall, isSprintFall);
        animator.SetBool(Settings.isRunningJump, isRunningJump);
        animator.SetBool(Settings.isRunningFall, isRunningFall);
        animator.SetBool(Settings.isWallGrab, isWallGrab);
        animator.SetBool(Settings.isWallFall, isWallFall);
        animator.SetBool(Settings.isWallJumping, isWallJumping);
        animator.SetBool(Settings.isIdle, isIdle);
        animator.SetBool(Settings.isLanding, isLanding);
        if (isDarkSpinBack)
        {
            animator.SetTrigger(Settings.isDarkSpinBack);
        }
     
        if (isDarkPowerUp)
        {
            animator.SetTrigger(Settings.isDarkPowerUp);
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
