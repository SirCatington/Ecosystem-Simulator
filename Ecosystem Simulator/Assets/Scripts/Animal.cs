using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float speed = 5;
    public float sense = 10;
    public float hunger;

    public float rotationSpeed = 5;
    
    public SpeciesManager manager;

    public bool foundFood;

    public Food food;
    private Transform transform;
    Vector3 point;

    

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<SpeciesManager>();
        transform = gameObject.GetComponent<Transform>();

        manager.AddAnimal(this);

        hunger = 0;
        point = new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f));
        foundFood = false;
    }

    
    public void UpdateAnimal()
    {

        if (foundFood)
        {
            if (food != null)
            {                
                MoveTowards(food.transform.position);
                float distToFood = Vector3.Distance(food.transform.position, transform.position);
                if (distToFood < 1f)
                {
                    if (hunger <= 50)
                    {
                        Reproduce();
                    }
                    else
                    {
                        hunger = 0;
                    }
                    
                    manager.RemoveFood(food);
                    Destroy(food.gameObject);
                    foundFood = false;
                }
            }
            else
            {
                foundFood = false;
            }
        }
        else
        {
            float dist = (point - transform.position).magnitude;
            if (dist < 1)
            {
                point = new Vector3(Random.Range(-50f, 50f), 0, Random.Range(-50f, 50f));
            }
            MoveTowards(point);
        }

        transform.position += transform.forward * speed * Time.deltaTime;

        if (hunger >= 100)
        {
            if (foundFood)
            {
                food.targeted = false;
            }
            manager.RemoveAnimal(this);
            Destroy(gameObject);
        }
        hunger += 10 * Time.deltaTime;

    }

    void MoveTowards(Vector3 target)
    {
        target.y = transform.position.y;
        Vector3 targetDirection = target - transform.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void Reproduce()
    {
        Instantiate(this);
    }

    

}
