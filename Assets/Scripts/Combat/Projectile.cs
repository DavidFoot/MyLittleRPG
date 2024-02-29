using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] bool isTargetLock = true;
    Health target;
    float damage = 0;


    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargetLock)
        {
            transform.LookAt(GetAimLocation());
        }
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetTarget(Health target, float damage)
    {
        this.target = target;   
        this.damage = damage;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (target == other.GetComponent<Health>())
        {
            target.TakingDamage(damage);
            Destroy(gameObject);
        }
        //
    }
    private Vector3 GetAimLocation()
    {
        if(target.GetComponent<CapsuleCollider>() != null)
        {
            return target.transform.position + Vector3.up * target.GetComponent<CapsuleCollider>().height / 2 ;
        }
        return target.transform.position;
    }
}
