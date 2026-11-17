using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    private Transform playerPosition;
    private float RotationSpeed = 3f;

    private Quaternion lookRotation;
	private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = (playerPosition.position - this.transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
    }
}
