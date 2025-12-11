using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxingHPManager : MonoBehaviour
{
    private BoxingController BoxingScrpit;
    [SerializeField]
    private int MaxDamage, BlockedDamage;
    [SerializeField]
    public int HP;
    public bool isTakingLeftPunch, isTakingRightPunch;

    private void Start()
    {
        BoxingScrpit = GetComponent<BoxingController>();
        
    }

    private void Update()
    {
        if (isTakingLeftPunch)
        {
            TakeDamageleft();
        }
        else if (isTakingRightPunch)
        {
            TakeDamageRight();
        }

        
    }

    void TakeDamageRight()
    {
        if (BoxingScrpit != null)
        {
            if (!BoxingScrpit.isBlockingRight)
            {
                StartCoroutine(TakeDamage());
            }
            else if(BoxingScrpit.isBlockingRight)
            {
                StartCoroutine(TakeBlockDamage());
            }

        }
    }

    void TakeDamageleft()
    {
        if (BoxingScrpit != null)
        {
            if (!BoxingScrpit.isBlockingLeft)
            {
                StartCoroutine(TakeDamage());
            }
            else if (BoxingScrpit.isBlockingLeft)
            {
                StartCoroutine(TakeBlockDamage());
            }
        }
    }

    IEnumerator TakeDamage()
    {
        isTakingRightPunch = false;
        isTakingLeftPunch = false;
        Debug.Log("Punched");
        HP -= MaxDamage;
        yield return new WaitForSeconds(0.5f);

    }

    IEnumerator TakeBlockDamage()
    {
        isTakingRightPunch = false;
        isTakingLeftPunch = false;
        Debug.Log("Bocked");
        HP -= BlockedDamage;
        yield return new WaitForSeconds(0);
    }

}
