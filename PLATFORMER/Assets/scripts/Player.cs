﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    [SerializeField] float runspeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float ClimbSpeed = 5f;
    [SerializeField] Vector2 deathkick = new Vector2(0f, 30f);
    [SerializeField] float bulletHorizontalSpeed = 5f;
    [SerializeField] float bulletVerticalSpeed = 5f;
    [SerializeField] GameObject ShootProjectile;
   
    bool isAlive = true;
    Rigidbody2D myrigidbody2D;
    CapsuleCollider2D myBody;
    BoxCollider2D myFeet;
    float gravityMultiplier;
    
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        myBody = GetComponentInChildren<CapsuleCollider2D>();
        myFeet = GetComponentInChildren<BoxCollider2D>();
        gravityMultiplier = myrigidbody2D.gravityScale;
       
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        else
        {
            Run();
            ClimbLadder();
            jump();
            shoot();
            FlipSprite();
            die();
        }
        
    }

    private void Run()
    {
       

        float ControlThrow = CrossPlatformInputManager.GetAxis("Horizontal")  ;
        Vector2 playerVelocity = new Vector2(ControlThrow * runspeed, myrigidbody2D.velocity.y);
        myrigidbody2D.velocity = playerVelocity;
        ChangeAnimationState();
    }

    

    private void jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("ground")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpvelocity = new Vector2(myrigidbody2D.velocity.x, jumpSpeed);
            myrigidbody2D.velocity = jumpvelocity;
            
        }

    }

    private void ClimbLadder()
    {
        if(!myBody.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            GetComponent<Animator>().SetBool("Climbing", false);
            myrigidbody2D.gravityScale = gravityMultiplier;
            return;
        }
        float ControlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 PlayerVelocity = new Vector2(myrigidbody2D.velocity.x, ControlThrow * ClimbSpeed);
        myrigidbody2D.velocity = PlayerVelocity;
        myrigidbody2D.gravityScale = 0f;
        bool PlayerIsClimbing = Mathf.Abs(PlayerVelocity.y) > Mathf.Epsilon;
        if(PlayerIsClimbing)
        {
            GetComponent<Animator>().SetBool("Climbing",true);
        }
        else
        {
            
            GetComponent<Animator>().SetBool("Climbing", false);
        }
       
    }

    private void FlipSprite()
    {
        bool PlayerIsMovingHorizontal = Mathf.Abs(myrigidbody2D.velocity.x) > Mathf.Epsilon;
        if(PlayerIsMovingHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody2D.velocity.x), 1f);
           
        }
    }

    private void ChangeAnimationState()
    {
        bool PlayerIsMovingHorizontal = Mathf.Abs(myrigidbody2D.velocity.x) > Mathf.Epsilon;
        if (PlayerIsMovingHorizontal)
        {
            GetComponent<Animator>().SetBool("Running", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
    }

   private void die()
    {
        if(myBody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        { 
            isAlive = false;
            GetComponent<Animator>().SetTrigger("Die");
            myrigidbody2D.velocity = deathkick;
            FindObjectOfType<GameSession>().ProcessDeath();
        }
        else
        {
            return;
        }
    }

    public void shoot()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(ShootProjectile, transform.position, Quaternion.identity);
            if (transform.localScale.x == 1f)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletHorizontalSpeed, bulletVerticalSpeed);
            }

            else
            {
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletHorizontalSpeed, bulletVerticalSpeed);
            }

            Destroy(bullet, 1f);
            


        }

    }
       
}

