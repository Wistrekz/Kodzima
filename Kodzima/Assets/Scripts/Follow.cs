using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public bool JustFollow;
    public Camera Camera;
    public Transform objToFollow;
    public GameObject[] SpritesInTriggers;

    public static bool Player_inTrigger;
    public static GameObject TriggerObject;

    private Vector3 deltaPos;
    private Vector3 oldPosition;
    private Vector2 Location;
    

    private float Map_size_x, Map_size_y;
    [SerializeField]
    float leftLimit, rightLimit, UpLimit, DownLimit;

    void OnStartLocation()
    {
        leftLimit += (float)6.7;
        rightLimit -= (float)6.7;
        UpLimit -= (float)5.05;
        DownLimit += (float)5.05;
    }

    void Set_Loc_borders()
    {
        if (Player_inTrigger)
        {
            Location = TriggerObject.transform.position;
            var bounds = TriggerObject.GetComponent<SpriteRenderer>().bounds;
            Map_size_x = bounds.size.x;
            Map_size_y = bounds.size.y;
            leftLimit = Location.x - (Map_size_x / 2);
            rightLimit = Location.x + (Map_size_x / 2);
            UpLimit = Location.y + (Map_size_y / 2);
            DownLimit = Location.y - (Map_size_y / 2);
            OnStartLocation();
        }

        
        //Камера ограничевается границами спрайта
    }

    void Start()
    {
        transform.position = new Vector3(objToFollow.transform.position.x, objToFollow.transform.position.y, transform.position.z);
        deltaPos = transform.position - objToFollow.position;
    }
    void Update()
    {
        if (JustFollow)
        {
            transform.position = objToFollow.position + deltaPos;
            oldPosition = transform.position;
        }
        else
        {


            transform.position = objToFollow.position + deltaPos;
            oldPosition = transform.position;
            Set_Loc_borders();
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, DownLimit, UpLimit), transform.position.z);


        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, UpLimit), new Vector2(rightLimit, UpLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, DownLimit), new Vector2(rightLimit, DownLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, UpLimit), new Vector2(leftLimit, DownLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, UpLimit), new Vector2(rightLimit, DownLimit));
    }
}
