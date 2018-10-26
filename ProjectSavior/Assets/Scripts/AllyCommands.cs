using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCommands : MonoBehaviour
{

    bool ducked = false;
    bool following = false;
    bool canTakeAllyDamage = true;
    bool goingToTarget = false;
    bool ismoving = false;
    public float speed = 0;
    public float safeRadio = 1.0f;
    public float AwarenessRadio = 2.5f;
    public Transform player;
    public Transform enemy;
    public Transform target;
    float mCS, mWS, ms, roSG;
    public bool detected = false;


    void Start()
    {
        //GameObject Ally = GameObject.Find("Ally1"); //Find script from another object. 
        AllyProperties propertiesScript = GetComponent<AllyProperties>(); //"this." ó nada retoma las propiedades del mismo objeto de este script.
        mCS = propertiesScript.crouchSpeed; //playerScript cambiado a "propertiesScript".
        mWS = propertiesScript.walkingSpeed;
        ms = propertiesScript.maxspeed;
        roSG = propertiesScript.RateOfSpeedgain;
    }
    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        //input Controls
        {
            //F: follow player
            if (Input.GetKeyDown(KeyCode.F))
            {
                goingToTarget = false;
                if (following == false)
                {
                    //editor only
                    ms = mWS;

                    ismoving = true;
                    following = true;
                }

                else if (following == true)
                {
                    speed = 0f;
                    following = false;
                    ismoving = false;
                }      
            }

            //E: Go to Left click position
            if (Input.GetKeyDown(KeyCode.E))
            {
                following = false;
                if(goingToTarget == false)
                {
                    //editor only
                    ms = mWS;

                    goingToTarget = true;
                    ismoving = true;
                }
                else if(goingToTarget == true)
                {
                    goingToTarget = false;
                    ismoving = false;
                    speed = 0f;
                }
            }
                

            // Q: Crouch
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (canTakeAllyDamage == true)
                {
                    ducked = true;
                    canTakeAllyDamage = false;
                    ms = mCS;
                    if (speed > ms)
                        speed = ms;
                }
                else if (canTakeAllyDamage == false)
                {
                    ducked = false;
                    canTakeAllyDamage = true;
                    ms = mWS;
                }
            }
        }

        //Movement Conditionals
        {
            if (following == true)
            {
                if (Vector3.Distance(transform.position, player.position) > 1.3f)
                    transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            }

            if (goingToTarget == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }

            //Wind-up movement
            if(ismoving == true)
            {
                if (speed < ms)
                    speed += roSG;
            }
        }

        //Escape Conditional
        {
            if(detected == true)
            {
                ismoving = true;
                transform.position = Vector3.MoveTowards(transform.position, enemy.position , -1 * step);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            detected = true;
            Debug.Log("true");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("false");
            detected = false;
        }
    }

    [ExecuteInEditMode]
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AwarenessRadio);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, safeRadio);
    }
}