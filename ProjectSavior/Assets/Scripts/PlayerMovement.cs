using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    /* Editado por Oscar. 10/3/2018.
    Formatting: variables deberán iniciar con minúscula, mientras que métodos y funciones deberán iniciar con mayúscula.
    Algunas variables movidas a Privadas ya que no son requeridas en públicas.
    crouchSpeed siempre será la mitad de walkingSpeed.
    Comentado windUpSprint ya que no se está utilizando.
    Agregado comentarios con valores que parecen ser los que encajan mejor para el estilo de juego, al menos por ahora.
    En Start, se debe inicializar la velocidad de speed ya que inicia con 0. El jugador no puede moverse caminando. Debe iniciar corriendo para que la velocidad de Speed tenga más de 0.
    */
    //Editado por Oscar. 10/4/2018: Incio de animaciones. Falta por terminar.

    public float walkingSpeed; //3
    public float runningSpeed; //4.5

    public float speed; //Public para testing.
    float crouchSpeed; //Siempre será la mitad de Walking speed.

    //public float windUpSprint; //No se está utilizando.
    public float windUpMultip; //0.1f // valor por el que time.deltatime se divide para que no sea de golpe. mientras más sea el valor, más tiempo tardará el windup

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

        if (Input.GetKey("left shift"))
        {
            //windUpSprint = windUpSprint + (Time.deltaTime)/ windUpMultip;
            
            
            speed = speed + WindupManager(windUpMultip);
            if (speed > runningSpeed)
            {
                speed = runningSpeed;
                //windUpSprint = 0;
            }



        }

        if (Input.GetKeyUp("left shift"))
        {
            //windUpSprint = 0;
            speed = walkingSpeed;


        }





        if (Input.GetKeyDown("left ctrl"))
        {

            speed = crouchSpeed;


        }

        if (Input.GetKeyUp("left ctrl"))
        {

            speed = walkingSpeed;


        }

        //Animations:
        animator.SetFloat("moveSpeed", speed); //Sprite animation changes depending on speed's value: 0: Idle. >0 && <4: Walk. >4: Running.
    }

  float WindupManager( float windupModifier)
    {
        float windupAm =+ (Time.deltaTime) / windupModifier;
        return windupAm;
    }
}
