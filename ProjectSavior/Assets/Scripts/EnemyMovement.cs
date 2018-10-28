using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //----- Animations -----
    SpriteRenderer m_SpriteRenderer;
    Color m_NewColor;
    Animator animator;
    Collider2D m_Collider;
    Vector3 initialPosition;
    float movSpd, walkSpd, runSpd, currHP, step;

    //----- Chasing AI -----
    NavMeshAgent agent;
    AudioSource alertSFX;
    [Range(1.0f, 10.0f)] //Esto afecta a la siguiente línea, como un if
    public float chaseRadius;
    private bool isFollowing = false;
    private bool canAlert = false;
    private bool canMove = true;

    Vector3 direction;
    public Transform target;
    float distance;
    bool facingRight = true; //Flipping Sprite
    private int movDir = 1;

    void Start(){
        m_Collider = GetComponent<Collider2D>();
        alertSFX = GetComponent<AudioSource>(); //----- Chasing AI -----
        animator = GetComponent<Animator>(); //----- Animations -----
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        EnemyProperties propertiesScript = GetComponent<EnemyProperties>();
        walkSpd = propertiesScript.walkSpeed;
        runSpd = propertiesScript.runSpeed;
        currHP = propertiesScript.currHealth;
        movSpd = walkSpd;
    }

    void Update(){

        step = movSpd * Time.deltaTime;

        if (currHP <= 0)
            canMove = false;

        if (target) { //----- Chasing Player -----
        direction = target.position - transform.position; //vector director
        distance = Vector3.Distance(transform.position, target.position); //h^2 = co^2 + ca^2 //distancia
        direction.Normalize(); //normalizar, es hacer la dimensión: 1
        }

        if (canMove == true) { //Frozen? Stunned? or Death animation playing

            if (distance <= chaseRadius) //Chasing
            {
                movSpd = runSpd;
                //transform.Translate(direction * movSpd / 15);
                transform.position = Vector3.MoveTowards(transform.position, direction, movSpd);

                if (target.position.x > transform.position.x && !facingRight)
                    Flip();
                else if (target.position.x < transform.position.x && facingRight)
                    Flip();
            }
            else
                movSpd = walkSpd;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    [ExecuteInEditMode]
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) //Only run in Editor, otherwise, this makes Enemies go wildddddddddddddddd
            initialPosition = transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

    public void TakeDamage(int damage)
    {
        currHP -= damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Savior")
        {
            //PlayerStats playerScript = other.gameObject.GetComponent<PlayerStats>();
            //other.gameObject.GetComponent<PlayerStats>().TakeDamage(attackDamage);
        }

        if (other.gameObject.tag == "InvisibleWall")
        {
            Physics2D.IgnoreCollision(other.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}