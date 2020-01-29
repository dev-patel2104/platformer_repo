using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton : MonoBehaviour
{
    Player player;
    float distanceApart;
    Vector3 orgScale;
    CapsuleCollider2D myBody;

    void Start()
    {
        orgScale = transform.localScale;
        player = FindObjectOfType<Player>();
        myBody = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SkelDeath();
        distanceApart = Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x);
        if (distanceApart <= 4.3f)
        {
            changeAnimationStateToReact();
            changeAnimationStateToAttack();
        }
        
    }

    public void changeAnimationStateToReact()
    {
        
        float distanceApart;
        distanceApart = Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x);
        if(distanceApart <= 2.1f)
        {
            return;
        }
        if(distanceApart<= 4.2f )
        {   
            Vector3 scale = transform.localScale;
            scale.x = -(player.transform.localScale.x);
            transform.localScale = scale;
            GetComponent<Animator>().SetBool("React", true);
        }
        if(distanceApart >= 4.2f)
        {
            transform.localScale = orgScale;
            GetComponent<Animator>().SetBool("React", false);
        }

    }

    public void changeAnimationStateToAttack()
    {
        float distanceApart;
        distanceApart = Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x);
        if (distanceApart <= 2.1f)
        {
            GetComponent<Animator>().SetBool("Attack", true);
        }

        if (distanceApart >= 2.5f && distanceApart <= 4.3f)
        {
          
            GetComponent<Animator>().SetBool("Attack", false);
        }
    }

   public void PlayerDeath()
    {
        float distanceApart;
        distanceApart = Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x);
        if(distanceApart <= 1.103)
        {
            FindObjectOfType<Player>().GetComponent<Animator>().SetTrigger("Die");
            FindObjectOfType<GameSession>().ProcessDeath();
        }
    }


    public void SkelDeath()
    {
        if (myBody.IsTouchingLayers(LayerMask.GetMask("bullet")))
        {
            FindObjectOfType<DamageDealer>().DoDamage();
            GetComponent<Animator>().SetTrigger("die");
            Destroy(gameObject,1.2f);
        }

       
    }

    
}
