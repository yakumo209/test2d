using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum State
{
    Stand,
    Move,
    Pose,
}


public class KoishiController : MonoBehaviour
{
    public float moveSpeed=5f;
    public float moveDistance;
    public State currentState;
    private float standTimer;
    private float poseTimer;
    private float shootTimer;
    private Animator am;
    private Vector3 targetPos;
    public GameObject bullet;
    public GameObject bullet1;
    private int bulletNum;
    float shootTimer2 = .1f;
    public Transform target;
    private AudioSource audioSource;
    public AudioClip[] clips;
    public float lastSurviveTime;
    private float surviveTime;
    float rand1= Random.Range(5f, 10f);
    float rand2 =Random.Range(10f, 40f);
    void Start()
    {
        am = GetComponent<Animator>();
        currentState = State.Stand;
        poseTimer = 5;
        standTimer = 5;
        bulletNum = 30;
        shootTimer = 0;
        shootTimer2 = .1f;
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        surviveTime += Time.deltaTime;
        CurrentTime.instance.UpdateText(surviveTime);
        switch (currentState)
        {
            case State.Stand:
                // shootTimer2 -= Time.deltaTime;
                //     for (int i = 0; i < 20; i++)
                //     {
                //         if (shootTimer2<0)
                //         {
                //             GameObject bulletObj = Instantiate(bullet1);
                //             bulletObj.GetComponent<Bullet>().SetBulletType(0,BulletCase.two);
                //             bulletObj.SetActive(true);
                //             bulletObj.transform.position = transform.position + Vector3.down * 1f;
                //             Vector3 dir = target.position - bulletObj.transform.position;
                //             float angle = -90+Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //             print(angle);
                //             Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
                //             bulletObj.transform.rotation = quaternion;
                //             shootTimer2 = .1f;
                //         }
                //         
                //     }
                
                am.SetBool("stand",true);
                standTimer -= Time.deltaTime;
                if (standTimer<0)
                {
                    currentState = State.Move;
                    am.SetBool("stand",false);
                    standTimer = 5;
                    while (true)
                    {
                        targetPos = transform.position +
                                    new Vector3(moveDistance *Random.Range(-1f, 1f),
                                        moveDistance* Random.Range(-1f, 1f), transform.position.z);
                        float minX=Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
                        float minY=Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y;
                        float maxX= Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;
                        float maxY= Camera.main.ViewportToWorldPoint(new Vector3(1, 1)).y;
                        if (targetPos.x<maxX&& targetPos.x>minX && targetPos.y> minY&& targetPos.y< maxY)
                        {
                            break;
                        }
                    }
                }
                break;
            case State.Move:
                am.SetBool("move",true);
                if (targetPos.x>=transform.position.x)
                {
                    //right
                    transform.localScale = new Vector3(-1, 1, 1);

                }
                else
                {
                    //left

                    transform.localScale = new Vector3(1, 1, 1);
                }
                transform.Translate((targetPos-transform.position).normalized*Time.deltaTime*moveSpeed,Space.Self);
                if (Vector3.Distance(targetPos,transform.position)<0.1f)
                {
                    float randNum = Random.Range(0, 2);
                    if (randNum==0)
                    {
                        currentState = State.Pose;
                        rand1= Random.Range(5f, 10f);
                        rand2 =Random.Range(10f, 40f);
                    }
                    else
                    {
                        currentState = State.Stand;
                    }
                    am.SetBool("move",false);
                }
                break;
            case State.Pose:
                
                am.SetBool("pose",true);
                this.shootTimer -= Time.deltaTime;
                if (this.shootTimer<0)
                {
                    for (int i = 0; i < bulletNum; i++)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(clips[0]);
                        Quaternion q = Quaternion.AngleAxis(i*360/bulletNum,Vector3.forward);
                        Vector3 pos = transform.position + q * Vector3.up * 2f;
                        GameObject bulletObj = ObjPool.instance.Get();
                        bulletObj.transform.position = pos;
                        bulletObj.transform.rotation = transform.rotation * q;
                        bulletObj.GetComponent<Bullet>().SetBulletType(i,BulletCase.one);
                        bulletObj.GetComponent<Bullet>().SetTarget(this.transform);
                        bulletObj.GetComponent<Bullet>().moveSpeed = rand1 ;
                        bulletObj.GetComponent<Bullet>().rotateSpeed = rand2;
                        bulletObj.SetActive(true);
                        
                    }
                    this.shootTimer = 0.4f;
                }
                
                poseTimer -= Time.deltaTime;
                if (poseTimer<0)
                {
                    am.SetBool("pose",false);
                    poseTimer = 5;
                    currentState = State.Move;
                    while (true)
                    {
                        targetPos = transform.position +
                                    new Vector3(moveDistance *Random.Range(-1f, 1f),
                                        moveDistance* Random.Range(-1f, 1f), transform.position.z);
                        float minX=Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x+1;
                        float minY=Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).y+1;
                        float maxX= Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x-1;
                        float maxY= Camera.main.ViewportToWorldPoint(new Vector3(1, 1)).y-1;
                        if (targetPos.x<maxX&& targetPos.x>minX && targetPos.y> minY&& targetPos.y< maxY)
                        {
                            break;
                        }
                    }
                    
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void UpdateTime()
    {
        if (surviveTime>lastSurviveTime)
        {
            lastSurviveTime = surviveTime;
            PlayerPrefs.SetFloat("time",lastSurviveTime);
        }
        surviveTime = 0;
    }
}
