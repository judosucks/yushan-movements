using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Effect : MonoBehaviour
{
    public static Effect Instance;

    //building
    [SerializeField] private GameObject building;
    private AnimatorStateInfo animatorStateInfo;
    private Animator animator;
    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }
    private void LateUpdate()
    {

    }
    public void ElectricEffectAfterDemonPowerPunch()
    {
        if (animatorStateInfo.IsName(Tags.DemonPowerPunch) && animatorStateInfo.normalizedTime <= 0.5f)
        {
            IsElectric();
            Debug.Log("stateinfo" + animatorStateInfo.IsName("demon power punch"));
        }
        else if (animatorStateInfo.IsName(Tags.DemonPowerPunch) && animatorStateInfo.normalizedTime >= 0.6f)
        {

            IsNotElectric();
        }
    }
    public void DemonGroundShake()
    {
        if (animatorStateInfo.IsName("demon transform") && animatorStateInfo.normalizedTime <= 0.1f)
        {
            Debug.Log("run");
            ShakeCamera.Instance.Shake(10f, 2f);

        }
        if (animatorStateInfo.IsName("demon transform") && animatorStateInfo.normalizedTime >= 0.9f)
        {

            ShakeCamera.Instance.Shake(0f, 0f);

        }
    }
    //effect
    public void IsSatanReturn()
    {
        animator.SetTrigger("isSatanReturn");
    }
    public void IsFeatherEffect()
    {
        animator.SetTrigger("isFeatherEffect");
    }
    public void IsElectric()
    {
        animator.SetBool("isElectricEffect", true);
    }
    public void IsNotElectric()
    {
        animator.SetBool("isElectricEffect", false);
    }
}
