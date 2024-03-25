using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float distanceAway = 5;

    [SerializeField]
    private float distanceUp;
    
    [SerializeField]
    private float smoothLook;

    private float maxDistanceUp = 5;
    private float minDistanceUp = 0;
    



    public void lookUpAndDown()
    {
        if(Input.GetAxisRaw("Mouse Y") != 0)
        {
            distanceUp -= Input.GetAxis("Mouse Y") * smoothLook;
            if (distanceUp > maxDistanceUp)
            {
                distanceUp = maxDistanceUp;
            }
            if(distanceUp < minDistanceUp)
            {
                distanceUp = minDistanceUp;
            }
        }
        
    }


    [SerializeField]
    private float smooth = 100;
    
    [SerializeField]
    private Transform followedObject;
    
    private Vector3 toPosition;
    //private Vector3 lookPosition;


    // Update is called once per frame
    void LateUpdate()
    {
        lookUpAndDown();
        toPosition = followedObject.position + Vector3.up  * distanceUp - followedObject.forward* distanceAway;
        //lookPosition = followedObject.position + Vector3.forward * distanceUp - followedObject.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, toPosition,Time.deltaTime * smooth);
        transform.LookAt(followedObject);
    }
}
