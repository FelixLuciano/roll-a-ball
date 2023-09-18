using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float SmoothTime = 0.15f;

    private Vector3 positionOffset;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        positionOffset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = Player.transform.position + positionOffset;
        Quaternion targetRotation = Quaternion.LookRotation(Player.transform.position - transform.position);

        targetPosition.y = Player.transform.position.y + 5;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}
