using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 5;

    private Rigidbody2D rb;
    [SerializeField]
    private bool isOnConveyor;
    private Vector2 conveyorDirection;
    private float conveyorXPosition;
    [SerializeField]private bool isHeld;
    public Vector2 OGPosotion;
    [SerializeField]
    private bool Pear, Apple, Banana;
    private GameObject PointChekerGameObject;
    private PointChecker PointCheckerScript;
    private GameObject Audio;
    private AudioSource Basket;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PointChekerGameObject = GameObject.FindGameObjectWithTag("PointManager");
        PointCheckerScript = PointChekerGameObject.GetComponent<PointChecker>();
        Audio = GameObject.FindGameObjectWithTag("Audio");
        Basket = Audio.GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Up"))
        {
            SetConveyorMovement(Vector2.up, collision.transform.position.x);
        }
        else if (collision.CompareTag("Down"))
        {
            SetConveyorMovement(Vector2.down, collision.transform.position.x);
        }
        else if (collision.CompareTag("PearBasket"))
        {
            if (Pear)
            {
                Basket.Play();
                PointCheckerScript.Points++;
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("BananaBasket"))
        {
            if (Banana)
            {
                Basket.Play();
                PointCheckerScript.Points++;

            }
            Destroy(gameObject);

        }
        else if (collision.CompareTag("AppleBasket"))
        {
            if (Apple)
            {
                Basket.Play();
                PointCheckerScript.Points++;

            }
            Destroy(gameObject);

        }
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Up") || collision.CompareTag("Down"))
        {
            isOnConveyor = false;
        }
        else if (collision.CompareTag("Holder"))
        {
            isHeld = false;
        }
    }

    private void SetConveyorMovement(Vector2 direction, float xPosition)
    {
        isOnConveyor = true;
        conveyorDirection = direction;
        conveyorXPosition = xPosition;

        Vector2 currentPos = transform.position;
        transform.position = new Vector2(xPosition, currentPos.y);
    }

    private void Update()
    {
        HandleMovement();

    }

    private void HandleMovement()
    {
        if (isHeld)
        {
            rb.velocity = Vector2.zero;
            if (transform.parent != null)
            {
                transform.position = transform.parent.position;
            }
            return;
        }

        if (isOnConveyor)
        {

                rb.velocity = conveyorDirection * moveSpeed * Time.deltaTime;

                Vector2 currentPos = transform.position;
                transform.position = new Vector2(conveyorXPosition, currentPos.y);
            
        }
        else if (!isHeld)
        {
            //transform.position = OGPosotion;
            if (!isOnConveyor)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 1;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTransformParentChanged()
    {
        isHeld = transform.parent != null && transform.parent.CompareTag("Holder");
    }
}