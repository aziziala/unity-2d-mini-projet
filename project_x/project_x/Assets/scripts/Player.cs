using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance ;
    private Rigidbody2D myrigidbody;
    private Animator myanimator;
    [SerializeField]
    private float mspeed;
    private bool facing;
    //cpmbat

    public int health = 8;
    public float invincibleTimeAfterHurt = 2;
    public GameObject[] hearts;
    [HideInInspector]
    Collider2D[] mycolls;

    //score

    private int thescore;
    public Text scoreText;

    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    private bool isGrounded;
    private bool jump;
    [SerializeField]
    private float jumpForce;
    public float jumpVelocity = 10;

    // Use this for initialization
    void Start () {
       
        instance = this;
        mycolls = this.GetComponents<Collider2D>();
        facing = true;
        myrigidbody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
		
	}

    void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate () {
        float horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        isGrounded = IsGrounded();
        Movments(horizontal);
        Flip(horizontal);
        HandleLayers();
        ResetValues();

    }

    private void Movments(float horizontal)
    {
        if (myrigidbody.velocity.y < 0)
        {
            myanimator.SetBool("land", true);
        }
        if (isGrounded && jump)
        {
            isGrounded = false;
            myrigidbody.AddForce(new Vector2(0, jumpForce));
            myanimator.SetTrigger("jump");

        }
        myrigidbody.velocity = new Vector2(horizontal * mspeed, myrigidbody.velocity.y);
        myanimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleInput()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            jump = true;

        }

    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facing || horizontal < 0 && facing)
        {
            facing = !facing;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

    }

    private void ResetValues()
    {
        jump = false;
    }

    private bool IsGrounded()
    {
        if (myrigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myanimator.ResetTrigger("jump");
                        myanimator.SetBool("land", false);

                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            myanimator.SetLayerWeight(1, 1);
        }

        else
        {
            myanimator.SetLayerWeight(1, 0);
        }

    }

    void TriggerHurt(float hurtTime)
    {
        StartCoroutine(HurtBlinker(hurtTime));
    }

    IEnumerator HurtBlinker(float hurtTime)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int playerLayer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer);
        foreach(Collider2D collider in Player.instance.mycolls)
        {
            collider.enabled = false;
           collider.enabled = true;

        }
        myanimator.SetLayerWeight(2, 2);

        yield return new WaitForSeconds(hurtTime);

        Physics2D.IgnoreLayerCollision(enemyLayer, playerLayer, false);
        myanimator.SetLayerWeight(2, 0);
    }

    void Hurt()
    {
        health--;


        if (health == 5)
        { hearts[5].SetActive(false); }

        if (health == 4)
        { hearts[4].SetActive(false); }

        if (health == 3)
        { hearts[3].SetActive(false); }

        if (health == 2)
        { hearts[2].SetActive(false); }

        if (health == 1)
        { hearts[1].SetActive(false); }

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            TriggerHurt(invincibleTimeAfterHurt);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AI_1 enemy = collision.collider.GetComponent<AI_1>();

        if (enemy != null)
        {

            Hurt();

        }

        //Coin

        if (collision.gameObject.tag == "Coin")
        {
            thescore = thescore + 1;
            scoreText.text = " " + thescore;
            Destroy(collision.gameObject);
        }



    }

}
