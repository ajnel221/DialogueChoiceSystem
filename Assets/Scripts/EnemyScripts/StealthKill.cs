using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthKill : MonoBehaviour
{
    public GameObject enemy;
    public BoxCollider2D stealthTrigger;
    public bool canDie = false;
    public GameObject indicator;

    private void Start()
    {
        indicator.SetActive(false);    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canDie = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canDie = false;
    }

    private void Update()
    {
        indicator.SetActive(true);
        if(canDie == true)
        {
            if(Input.GetKey(KeyCode.F))
            {
                Debug.Log("Is Dead");
                Destroy(enemy);
            }
        }

        else
        {
            indicator.SetActive(false);
        }
    }
}
