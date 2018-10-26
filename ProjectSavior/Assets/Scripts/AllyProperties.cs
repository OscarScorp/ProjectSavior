using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyProperties : MonoBehaviour {

    public bool Alive = true;
    int maxHealth = 100;
    [Range(0,100)]
    public int currentHealth;
    public int accuracy;
    public float crouchSpeed = 0.5f; //La velocidad de crouching siempre deberá ser la mitad de walkingSpeed. Oscar edit 16-Oct-18.
    public float walkingSpeed = 1.0f;
    public float maxspeed = 1;
    public float RateOfSpeedgain = 0.01f;

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
        crouchSpeed = walkingSpeed / 2;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentHealth <= 0)
            Alive = false;

        if (Alive == false)
            Destroy(gameObject);
	}
}
