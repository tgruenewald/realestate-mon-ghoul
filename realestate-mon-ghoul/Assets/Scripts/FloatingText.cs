using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime = 2f;  // Duration after which the object will be destroyed

    void Start()
    {
        StartCoroutine(DestroyObjectAfterTime(lifetime));
    }

    private IEnumerator DestroyObjectAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);  // Wait for the specified duration
        Destroy(gameObject);  // Destroy this game object
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 2.5f;
        transform.Translate(0, speed * Time.deltaTime, 0, Space.World);
    }
}
