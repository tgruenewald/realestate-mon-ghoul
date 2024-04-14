using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class letterBounce : MonoBehaviour
{
    public Rigidbody2D rb;
    private float time = 0.0f;
    private bool isMoving = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(ExampleCoroutine());
       
        
        
    }
    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
         rb.velocity = new Vector3(Random.Range(-10,10), Random.Range(-10,10), 0);
        //After we have waited 5 seconds print the time again.
    }
    void FixedUpdate()
    {
            // the cube is going to move upwards in 10 units per second

            
    }
}