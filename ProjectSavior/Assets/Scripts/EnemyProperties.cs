using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour {

    Rigidbody rb;
    public float speed;
    public int maxHealth;
    public int currHealth;
    
    void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	
	void Update ()
    {
		if (currHealth <= 0)
        {
            Die();
        }
    }
	


    void Die()
    {
        Destroy(gameObject);
    }
}
