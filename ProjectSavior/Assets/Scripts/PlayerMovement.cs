using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkingSpeed; //3
    public float runningSpeed; //4.5

    public bool isMoving;
    public bool isRunning;
    public bool isCrouching;

    public float speed; //Public para testing.
    float crouchSpeed; //Siempre será la mitad de Walking speed.

    //public float windUpSprint; //No se está utilizando.
    public float windUpMultip; //0.1f // valor por el que time.deltatime se divide para que no sea de golpe. mientras más sea el valor, más tiempo tardará el windup

    bool facingRight = true;

    Animator animator; //Animator
    public GameObject child_spr_Body;

    void Start()
    {
        crouchSpeed = walkingSpeed / 2;
        speed = walkingSpeed;

        //Animator
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //deltaTime == haciendo transición del objeto al destino (1 adelante) pero poco a poco, de lo contrario, es de golpe.
        transform.position = new Vector2(transform.position.x + horizontalAxis * (speed * Time.deltaTime), transform.position.y + verticalAxis * (speed * Time.deltaTime));

        if (Input.GetKeyDown(KeyCode.D)) //right
        {
            if(!facingRight)
                Flip();
        } else if (Input.GetKeyDown(KeyCode.A)) //left
        {
            if (facingRight)
                Flip();
        }


            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        { animator.SetBool("isMoving", true); }
        else
        { animator.SetBool("isMoving", false); }

        if (isCrouching == false)
        {
            if (Input.GetKey("left shift"))
                    {
                        isRunning = true;
                        //windUpSprint = windUpSprint + (Time.deltaTime)/ windUpMultip;
            
            
                        speed = speed + WindupManager(windUpMultip);
                        if (speed > runningSpeed)
                        {
                            speed = runningSpeed;
                        animator.SetBool("isRunning", true); // animación de correr entra
                    //windUpSprint = 0;
                        }



                    }

                    if (Input.GetKeyUp("left shift"))
                    {
                        isRunning = false;
                        //windUpSprint = 0;
                        speed = walkingSpeed;
                        animator.SetBool("isRunning", false); // animación de correr sale, el personaje vuelve a caminar o idle.


                    }
        }
        




        if (isRunning == false)
        {
            if (Input.GetKeyDown("left ctrl"))
            {
                isCrouching = true;
                speed = crouchSpeed;
                animator.SetBool("isCrouching", isCrouching);
            }

            if (Input.GetKeyUp("left ctrl"))
            {
                isCrouching = false;
                speed = walkingSpeed;
                animator.SetBool("isCrouching", isCrouching);
            }

        }
        

        //Animations:
        animator.SetFloat("moveSpeed", speed); //Sprite animation changes depending on speed's value: 0: Idle. >0 && <4: Walk. >4: Running.
        
    }

    float WindupManager( float windupModifier)
    {
        float windupAm =+ (Time.deltaTime) / windupModifier;
        return windupAm;
    }

    void Flip() //Función para flipear el sprite del personaje al voltear.
    {
        facingRight = !facingRight;
        Vector3 theScale = child_spr_Body.transform.localScale;
        theScale.x *= -1;
        child_spr_Body.transform.localScale = theScale;
    }
}
