using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float speedUpAndDown;

    private Vector3 PointA;
    private Vector3 PointB;
    private Vector3 currentTarget;
    private Vector3 currentStart;
    private float distance = 0;

    private void moveAndRotate()
    {
        transform.Rotate(0,  rotationSpeed , 0, Space.Self);
        distance += speedUpAndDown * Time.deltaTime;
        transform.position = Vector3.Lerp(currentStart, currentTarget, distance);
        if (distance > 1)
        {
            distance = 0;
            Vector3 oldTarget = currentTarget;
            currentTarget = currentStart;
            currentStart = oldTarget;
        }
    }

    void Start()
    {
        PointA = transform.position;
        PointB = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        currentTarget = PointB;
        currentStart = PointA;
    }


    // Update is called once per frame
    void Update()
    {
        moveAndRotate();
    }
}
