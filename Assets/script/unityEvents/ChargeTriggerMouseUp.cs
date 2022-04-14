using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChargeTriggerMouseUp : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public Animator animator;
    public AnimatorStateInfo animatorStateInfo;
    [SerializeField] private GameObject followCamera;
    [SerializeField] private GameObject closeCamera;
    //demon stance
    private bool isDemonPowerCharge;
    private bool isDemonPunch;
    private bool isDemonPowerPunch;
    private bool isDemonIdle;
    private bool isDemon;

    private void OnEnable()
    {

    }
    private void Awake()
    {



    }
    private void LateUpdate()
    {

    }
    private void Start()
    {

    }
    public void ChargeAttackAndInputMouseUpFull()
    {


    }
    public void ChargeAttackAndInputMouseUp()
    {


        //if (Input.GetMouseButtonUp(1) && isDemon && (demonPowerChargeTimer > demonPowerChargeDuration) && animatorStateInfo.IsName(Tags.DemonPowerCharge) && isDemonPowerCharge)
        //{
        //    isDemonPowerCharge = false;
        //    click = false;

        //}





    }
}
