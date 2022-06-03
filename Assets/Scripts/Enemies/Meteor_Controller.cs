using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Meteor_Controller : MonoBehaviour
{
    // Variables
    public float speed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 20f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speed != 0 && rb != null)
        {
            rb.position += transform.forward * (speed * Time.deltaTime);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Coroutine_LoseScene());
        }
    }

    IEnumerator Coroutine_LoseScene()
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.Instance.EndGame();
    }
}
