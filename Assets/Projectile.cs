using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float maxAge;
    private float currentAge = 0;
    public bool captured = false;
    public bool canAge = true;
    public Color weaponizedColor = Color.red;

    private Rigidbody2D rb;

    public TrailRenderer trail;


    private float restingTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {

        
        if (canAge)
        {
            currentAge += Time.deltaTime;
            if (currentAge > maxAge)
            {
                Destroy(gameObject);
            }
        }
        // item.projectile.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic
        if (captured && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (rb.velocity.magnitude < 0.1f)
            {
                Debug.Log(rb.velocity.magnitude);
                restingTime += Time.deltaTime;
            }
            else restingTime = 0;

            if (restingTime > 0.1f)
            {
                Weaponize(false);
            }
        }


        //   if (rb.velocity.magnitude < 0.1f && captured)
        //   {
        //       restingTime += Time.deltaTime;
        //       if (restingTime > 0.2f)
        //       {
        //           restingTime = 0;
        //           Weaponize(false);
        //       }
        //   }
        //   else restingTime = 0;
    }



    internal void Weaponize(bool v)
    {
        captured = v;
        trail.startColor = v ? weaponizedColor : Color.white;
        trail.endColor = Color.clear;

    }
}
