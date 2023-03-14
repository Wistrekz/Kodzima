using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Rigidbody2D Player;
    public float movespeed, jumpspeed;

    public static bool CanMoving = true;

    private bool IsFall = true;

    [SerializeField]
    Transform M_GroundCheck_pos;
    [SerializeField]
    Transform L_GroundCheck_pos;
    [SerializeField]
    Transform R_GroundCheck_pos;

    private void Update()
    {
        //JumpCheck

        if(Physics2D.Linecast(transform.position, M_GroundCheck_pos.position, 1<< LayerMask.NameToLayer("Flat")) ||
           Physics2D.Linecast(transform.position, L_GroundCheck_pos.position, 1 << LayerMask.NameToLayer("Flat")) ||
           Physics2D.Linecast(transform.position, R_GroundCheck_pos.position, 1 << LayerMask.NameToLayer("Flat")))
        {
            IsFall = true;
        }
        else
        {
            IsFall = false;
        }

        //Move_and_Jump
        if(CanMoving)
        {
            if (Input.GetKey("right"))
            {
                Player.velocity = new Vector2(movespeed, Player.velocity.y);
            }
            else if (Input.GetKey("left"))
            {
                Player.velocity = new Vector2(-movespeed, Player.velocity.y);
            }
            if ((Input.GetKey("up") && IsFall) || (Input.GetKey(KeyCode.W) && IsFall))
            {
                Player.velocity = new Vector2(Player.velocity.x, jumpspeed);
            }
        }
        
    }

    public static void StopMoving()
    {
        CanMoving = false;
    }
    public static void StartMoving()
    {
        CanMoving = true;
    }
}
