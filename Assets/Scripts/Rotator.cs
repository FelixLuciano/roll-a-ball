using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float movementAmplitude = 0.3f;
    public float movementFrequency = 0.2f;

    private Vector3 basePosition;
    private float movementPhasis;

    void Start()
    {
        basePosition = transform.position;
        movementPhasis = Random.Range(0, 2 * Mathf.PI);
    }

    void Update()
    {
        float positionY = basePosition.y + movementAmplitude * Mathf.Sin(Time.time * 2 * Mathf.PI * movementFrequency + movementPhasis);

        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        transform.position = new Vector3(basePosition.x, positionY, basePosition.z);
    }
}
