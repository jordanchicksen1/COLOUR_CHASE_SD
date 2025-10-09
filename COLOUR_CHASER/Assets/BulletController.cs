using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject Explotion;
    private CircleCollider2D CircleCollider;
    private bool CheckforBool;

    private void Start()
    {
        StartCoroutine(ShootCheck());
        CircleCollider = GetComponent<CircleCollider2D>();
    }

    IEnumerator ShootCheck()
    {
        yield return new WaitForSeconds(0.3f);
        CheckforBool = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (CheckforBool)
            {
                GameObject ExplotiongParticle = Instantiate(Explotion, transform.position, Quaternion.identity);
                CircleCollider.radius = 0.41f;
                Destroy(ExplotiongParticle, 2);
                Destroy(gameObject);
            }
        }
    }
}
