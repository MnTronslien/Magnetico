using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aura : MonoBehaviour
{


    public List<GameObject> capturedObjects;

    public List<CapturedObject> satelites;
    public float objectSpinSpeed = 3;
    public float objectShrinkSpeed = 0.01f;
    public float releaseForce = 100;


    public float debug_angle = 90;

    public float defaultDist = 2;
    bool clockwize = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDestroy()
    {
        foreach (var item in satelites)
        {
            Release(item);
        }
        satelites.Clear();
    }

    private void Release(CapturedObject item)
    {
        if (item == null) return;
        //item.projectile.captured = false;
        item.projectile.canAge = true;
        item.projectile.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
     
    }

    public void OnExplode(InputAction.CallbackContext _)
    {
        Debug.Log("Explode");
        foreach (var item in satelites)
        {
            Release(item);
            var dir = Vector2.Perpendicular(item.projectile.transform.position - transform.position);
            if (!clockwize) dir *= -1;
            item.projectile.GetComponent<Rigidbody2D>().AddForce(dir * releaseForce * (defaultDist/item.distance), ForceMode2D.Force);
        }
        satelites.Clear();
    }

    public void OnReverse(InputAction.CallbackContext _)
    {
        clockwize = !clockwize;
    }

    private void FixedUpdate()
    {
        //   var count = satelites.Count;
        //   satelites.RemoveAll(x => x.projectile == null);
        //   if (count != satelites.Count) Debug.Log($"Removed {count - satelites.Count}");

        foreach (var item in satelites)
        {
            //item.transform.position = transform.position + (Vector3.up * defaultDist);


            //Find angle from singend angle
            if (clockwize) { item.angle += objectSpinSpeed * Time.deltaTime; }
            else item.angle -= objectSpinSpeed * Time.deltaTime; 
            //Debug.Log(item.angle);

            item.distance -= objectShrinkSpeed * Time.deltaTime;
            //Debug.Log(item.distance);

            item.projectile.transform.position = transform.position + CirclePosFromAngle(item.angle) * item.distance;


            //Add to anglel
            //Get new position




        }



     // foreach (var item in satelites)
     // {
     //
     //         item.projectile.transform.position = transform.position + Vector3.up * defaultDist;
     //         Debug.Log("Moved Knives");
     //
     //
     //
     //
     //
     //     //  item.angle = item.angle + (objectSpeed + Time.deltaTime);
     //     //  transform.position = item.lastPos + CirclePosFromAngle(item.angle) * item.Distance;
     //     //  item.lastPos = transform.position;
     // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"{collision.otherCollider.name} collided with {collision.gameObject.name}");
        if (collision.gameObject.GetComponent<Projectile>())
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
            if (!projectile.captured)
            {

                projectile.captured = true;
                projectile.weaponizedColor = GetComponent<PlayerController>().playerColor;
                projectile.Weaponize(true);
                projectile.canAge = false;
                projectile.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                projectile.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                projectile.GetComponent<Rigidbody2D>().AddTorque(100f);
                satelites.Add(new CapturedObject(90, projectile, defaultDist));


                //capturedObjects.Add(projectile.gameObject);

            }


            //   var otherRB = collision.otherCollider.GetComponent<Rigidbody2D>().simulated = false;
            //   var otherCol = collision.otherCollider.GetComponent<Collider2D>().enabled = false;

        }
    }


    public static Vector3 CirclePosFromAngle(float angle)
    {

        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        return new Vector3(x, y);
    }

    [Serializable]
    public class CapturedObject
    {
        public Projectile projectile;
        public float distance;
        public float angle;

        public CapturedObject(float angle, Projectile projectile, float distance)
        {
            this.angle = angle;
            this.projectile = projectile;
            this.distance = distance;
        }
    }

}


