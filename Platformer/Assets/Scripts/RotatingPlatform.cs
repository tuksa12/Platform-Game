using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float timer;

    private float smoothRotation = 0.01f;
    private float time = 0;

    public void randomRotation()
    {
        if(time >= timer)
        {
            smoothRotation = Random.Range(-0.05f, 0.05f);
            time = 0;
        }
        else
        {
            time += Time.deltaTime;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        randomRotation();
        transform.Rotate(0, transform.position.y * rotationSpeed * smoothRotation, 0, Space.Self);
    }
}
