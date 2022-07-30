using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    Vector3 CurrentPos;
    public Vector3 position;
    public float speed = 0.5f;
    float movementRange;

    void Start()
    {
        CurrentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycle = Time.time / speed;

        float newCycle = Mathf.Sin(cycle);

        movementRange = newCycle;

        Vector3 newPosition = position * movementRange;

        transform.position = CurrentPos + newPosition;
    }
}
