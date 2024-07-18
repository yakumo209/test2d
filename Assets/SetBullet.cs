using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBullet : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (other.GetComponent<Bullet>().bulletcase==BulletCase.one)
            {
                ObjPool.instance.Set(other.gameObject); 
            }
            
        }
    }
}
