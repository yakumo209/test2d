using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletCase
{
    one,
    two,
}
public class Bullet : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer sp;

    public int bulletType=0;
    public BulletCase bulletcase;

    private Transform target;

    public float rotateSpeed;

    public float moveSpeed;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Init(bulletType,bulletcase);
    }

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Init(int i,BulletCase bulletc)
    {
        sp.sprite = sprites[i];
        this.bulletcase = bulletc;
    }
    // Update is called once per frame
    void Update()
    {
        switch (bulletcase)
        {
            case BulletCase.one:
                transform.Translate(Vector3.up*Time.deltaTime*5f);
                transform.RotateAround(target.position,Vector3.back,Time.deltaTime*moveSpeed);
                break;
            case BulletCase.two:
                transform.Translate(Vector3.up*Time.deltaTime*rotateSpeed);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    public void SetBulletType(int num,BulletCase casec)
    {
        bulletType = num;
        bulletcase = casec;//0 1 2 3 4 5 6 7=0
        if (bulletType>sprites.Length-1)
        {
            bulletType=num % sprites.Length;
        }
    }

    public void SetTarget(Transform trans)
    {
        target = trans;
    }

}
