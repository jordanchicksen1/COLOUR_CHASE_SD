using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlick : MonoBehaviour
{
    public float lifetime = 5f;       
    public float slowAmount = 0.5f;    
    public float slowDuration = 10f;  

    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            car.StartCoroutine(SlowCar(car));
        }
    }

    private IEnumerator SlowCar(CarController car)
    {
        float originalAccel = car.acceleration;
        car.acceleration *= slowAmount;  
        yield return new WaitForSeconds(slowDuration); 
        car.acceleration = originalAccel; 
    }
}
