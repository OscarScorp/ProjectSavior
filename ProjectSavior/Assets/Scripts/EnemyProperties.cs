using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour {
    Rigidbody rb;
    Collider2D m_Collider;
    [Range(0.01f, 5.0f)]
        public float walkSpeed;
    [Range(1.00f, 10.0f)]
        public float runSpeed;
    public int maxHealth, currHealth;
    public enum EnemyBehavior { Lv1_Patrol, Smart }; //Enemy type.
    public EnemyBehavior MoveType;
    SpriteRenderer m_SpriteRenderer;
    bool facingRight = true; //Flipping Sprite
    float alphaFade = 1f;

    void Start (){
        rb = gameObject.GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider2D>();
        currHealth = maxHealth;
    }
	
	void Update (){
		if (currHealth <= 0)
            Die();

        if (currHealth <= 0){
            Die();
            alphaFade -= 0.01f;
            m_SpriteRenderer.color = new Color(1f, 1f, 1f, alphaFade); //is about 100 % transparent(Cant be seen at all, but still active)
            if (alphaFade <= 0f)
                Destroy();
        }
    }

    public void Die(){
        transform.Rotate(new Vector3(0, 0, -90));
        m_Collider.enabled = false;
        //animator.SetBool("die", true);
    }

    void Destroy(){
        Destroy(gameObject);
    }
}
