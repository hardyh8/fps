using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody body;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        body.AddForce(transform.forward*200*Time.deltaTime,ForceMode.Impulse);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
