using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxFllow : MonoBehaviour
{
    public UnityEvent onHit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position =new Vector3(mousePos.x,mousePos.y,0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            print("HIT");
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().Play();
            onHit.Invoke();
        }
    }
}
