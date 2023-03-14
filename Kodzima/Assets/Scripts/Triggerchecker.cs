using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerchecker : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(234);
        if(collision.gameObject.name == "Kodzima")
        {
            Follow.Player_inTrigger = true;
            Follow.TriggerObject = gameObject;
        }
    }
}
