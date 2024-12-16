using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed =5;
    Rigidbody2D myRiggedbody;
    void Start()
    {
        myRiggedbody = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        myRiggedbody.velocity = new Vector2(moveSpeed, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipFacing();
    }

    void FlipFacing() 
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRiggedbody.velocity.x)), 1f);
    }
}
