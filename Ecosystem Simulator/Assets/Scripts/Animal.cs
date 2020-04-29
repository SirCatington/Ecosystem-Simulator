using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float speed = 5;
    public float rotationSpeed = 5;
    public Food food;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowards(food.transform.position);
    }

    void MoveTowards(Vector3 target)
    {
        Vector3 targetDirection = target - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
