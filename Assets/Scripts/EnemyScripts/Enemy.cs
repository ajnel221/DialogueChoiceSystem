using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public GameObject[] waypoints;
    public float speed;
    private bool canSeePlayer = false;
    public int randomPoint;
    public float time;
    public float counter;
    private Animator animator;
    private Rigidbody2D rigid;
    private SpriteRenderer spriteRen;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        randomPoint = Random.Range(0, waypoints.Length);
        counter = time;
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);

        /*if(rigid.velocity.x <= 0)
        {
            transform.eulerAngles = new Vector2(0, -180);
            
        }

        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            
        }*/

        if(hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            canSeePlayer = true;
        }

        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
            canSeePlayer = false;
        }

        PatrolState();
        MoveToPlayerState();
        Rotate();
    }

    public void Rotate()
    {
        if(!canSeePlayer)
        {
            if(waypoints[randomPoint].transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0,-180,0);
            }

            else
            {
                transform.eulerAngles = new Vector3(0,0,0);
            }
        }

        else
        {
            if(player.transform.position.x < transform.position.x)
            {
                transform.eulerAngles = new Vector3(0,-180,0);
            }

            else
            {
                transform.eulerAngles = new Vector3(0,0,0);
            }
        }
    }

    public void PatrolState()
    {
        //Allow AI to Patrol to Radom Waypoints

        if(!canSeePlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[randomPoint].transform.position, speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            
            if(Vector2.Distance(transform.position, waypoints[randomPoint].transform.position) < .1f)
            {
                if(counter <= 0)
                {
                    randomPoint = Random.Range(0, waypoints.Length);
                    counter = time;
                }

                else
                {
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isIdle", true);
                    counter -= Time.deltaTime;
                }
            }
        }
    }

    public void MoveToPlayerState()
    {
        //Set Animator and Move towards Player

        if(canSeePlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
    }
}