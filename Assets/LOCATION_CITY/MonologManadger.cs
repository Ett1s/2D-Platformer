using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonologManadger : MonoBehaviour
{

    public static Action<string> displayTipEvent;
    public static Action disableTipEvent;

    [SerializeField] private TMP_Text messageText;

    private Animator anim;

    private int activeTips;

    private void OnEnable()
    {
        displayTipEvent += displayTip;
        disableTipEvent += disableTip;

    }

    private void OnDisable()
    {
        displayTipEvent -= displayTip;
        disableTipEvent -= disableTip;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void displayTip(string message)
    {
        messageText.text = message;
        anim.SetInteger("monostate", ++activeTips);
    }
    
    private void disableTip()
    {
        anim.SetInteger("monostate", --activeTips);
    }
}
