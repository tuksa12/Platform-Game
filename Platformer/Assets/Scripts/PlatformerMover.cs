using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMover : MonoBehaviour
{
    [SerializeField]
    private Transform PointA;

    [SerializeField]
    private Transform PointB;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float distance;

    private Transform currentTarget;

    private Transform currentStart;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = PointA.position;
        currentTarget = PointB;
        currentStart = PointA;
    }

    // Update is called once per frame
    void Update()
    {
        distance += speed * Time.deltaTime;
        transform.position = Vector3.Lerp(currentStart.position, currentTarget.position, distance);
        if (distance > 1)
        {
            distance = 0;
            Transform oldTarget = currentTarget;
            currentTarget = currentStart;
            currentStart = oldTarget;
        }
    }
}
