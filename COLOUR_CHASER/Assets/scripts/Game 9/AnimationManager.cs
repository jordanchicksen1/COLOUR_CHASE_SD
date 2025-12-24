using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    public Animator PlayerAnimator;
    [SerializeField]
    private List<string> Animations;
    private string Punch;
    private BoxingController boxingControllerScript;

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        boxingControllerScript = GetComponent<BoxingController>();
    }

    private void Update()
    {
        
        if (boxingControllerScript.isBlockingRight)
        {
            PlayerAnimator.SetBool("RBlock", true);

        }
        else if (!boxingControllerScript.isBlockingRight)
        {
            PlayerAnimator.SetBool("RBlock", false);
        }

        if (boxingControllerScript.isBlockingLeft)
        {
            PlayerAnimator.SetBool("LBlock", true);
        }
        else if (!boxingControllerScript.isBlockingLeft)
        {
            PlayerAnimator.SetBool("LBlock", false);

        }
        
        if (boxingControllerScript.isBlockingLeft && boxingControllerScript.isBlockingRight)
        {
            PlayerAnimator.SetBool("LBlock", false);
            PlayerAnimator.SetBool("RBlock", false);
            PlayerAnimator.SetBool("DBlock", true);

        }
        else if (!boxingControllerScript.isBlockingLeft && !boxingControllerScript.isBlockingRight)
        {
            PlayerAnimator.SetBool("DBlock", false);

        }
    }
    public void Player1()
    {
        PlayerAnimator.SetBool("P1", true);

    }
    public void Player2() 
    {
        PlayerAnimator.SetBool("P2", true);

    }
    public void LeftPunch()
    {
        Punch = "LPunch";
        StartCoroutine(PunchDuration());
    }

    public void RightPunch()
    {
        Punch = "RPunch";
        StartCoroutine(PunchDuration());
    }
    
    public void DoubleBlock()
    {
        PlayerAnimator.SetBool("DBlock", true);

    }

    public void Knock()
    {
        for (int i = 0; i < Animations.Count; i++)
        {
            PlayerAnimator.SetBool(Animations[i], false);
        }
        StartCoroutine(Knocked());
    }

    IEnumerator Knocked()
    {
        PlayerAnimator.SetBool("Knock", true);
        yield return new WaitForSeconds(0.1f);
        PlayerAnimator.SetBool("Knock", false);
    }
    IEnumerator PunchDuration()
    {
        PlayerAnimator.SetBool(Punch, true);
        yield return new WaitForSeconds(0.1f);
        PlayerAnimator.SetBool(Punch, false);

    }

}
