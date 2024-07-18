using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjPool : MonoBehaviour
{
    public static ObjPool instance;
    
    private void Awake()
    {
        instance = this;
    }

    public Queue<GameObject> bulletPool = new Queue<GameObject>();

    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);
            bulletObj.transform.SetParent(transform);
            bulletObj.SetActive(false);
            
            bulletPool.Enqueue(bulletObj);
        }
    }

    public GameObject Get()
    {
        if (bulletPool.Count==0)
        {
            GameObject bulletObj = Instantiate(bullet, transform);
            bulletObj.SetActive(false);
            bulletPool.Enqueue(bulletObj);
        }
        GameObject obj = bulletPool.Dequeue();
        return obj;
    }

    public void Set(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.rotation = Quaternion.identity;
        obj.transform.position=Vector3.zero;
        obj.transform.SetParent(transform);
        bulletPool.Enqueue(obj);
    }

    private void Update()
    {
        print(bulletPool.Count);
    }
}
