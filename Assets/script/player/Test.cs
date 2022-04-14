using UnityEngine;

public class Test : MonoBehaviour
{
    public float movX;
    public float movY;
    public bool isRunning;
    public bool isSprint;
    public bool isDashing;
    public bool isDarkSpinBack;
    public bool isJumping;
    public bool isFalling;
    public bool isLanding;
    public bool isSprintJump;
    public bool isSprintFall;
    public bool isRunningJump;
    public bool isRunningFall;
    public bool isWallGrab;
    public bool isWallJumping;
    public bool isWallFall;
    public bool isIdle;
    public bool isDarkPowerUp;

    private void Update()
    {
        EventHandler.CallDarkMovementEvent(movX, movY, isRunning, isSprint, isDashing, isDarkSpinBack, isJumping, isFalling, isLanding, isSprintJump, isSprintFall, isRunningJump, isRunningFall, isWallGrab, isWallJumping, isWallFall, isIdle, isDarkPowerUp);

    }
}