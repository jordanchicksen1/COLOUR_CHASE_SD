using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private float moveUpDistance = 2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stayUpTime = 2f; // time to stay up after all switches released

    private Vector3 originalPos;
    private Vector3 upPos;
    private int activeSwitches = 0; // how many switches are pressed
    private float releaseTimer = 0f;

    private void Start()
    {
        originalPos = transform.position;
        upPos = originalPos + Vector3.up * moveUpDistance;
    }

    private void Update()
    {
        Vector3 targetPos = originalPos;

        if (activeSwitches > 0)
        {
            // go up if any switch is active
            targetPos = upPos;
            releaseTimer = stayUpTime; // reset timer
        }
        else if (releaseTimer > 0f)
        {
            // stay up for a while after release
            targetPos = upPos;
            releaseTimer -= Time.deltaTime;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    // called by switches
    public void PressSwitch()
    {
        activeSwitches++;
    }

    public void ReleaseSwitch()
    {
        activeSwitches = Mathf.Max(0, activeSwitches - 1);
    }
}
