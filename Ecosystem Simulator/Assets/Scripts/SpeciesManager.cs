using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesManager : MonoBehaviour
{
    List<Animal> animals = new List<Animal>();
    List<Food> foods = new List<Food>();

    public Animal prefabAnimal;
    public Food prefabFood;
    public int startingAnimalSpawns = 1;
    public int startingFoodSpawns = 50;
        
    public float foodSpawnRate = 5f;
    public int foodSpawnNum = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("ListAnimals", 0, 1f);
        InvokeRepeating("SpawnFood", foodSpawnRate, foodSpawnRate);

        for (int i = 0; i < startingFoodSpawns; i++)
        {
            Vector3 location = new Vector3(Random.Range(-49f, 49f), 0, Random.Range(-49f, 49f));
            Food food = Instantiate(prefabFood, location, Quaternion.identity) as Food;
            foods.Add(food);
        }
        for (int i = 0; i < startingAnimalSpawns; i++)
        {
            Vector3 location = new Vector3(Random.Range(-49f, 49f), 0, Random.Range(-49f, 49f));
            Instantiate(prefabAnimal, location, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < animals.Count; i++)
        {
            if (animals[i].foundFood == false)
            {
                float distanceToFood = animals[i].sense;
                for (int u = 0; u < foods.Count; u++)
                {
                    if (foods[u].targeted == false) { 

                        float distance = Vector3.Distance(animals[i].transform.position, foods[u].transform.position);
                        if (distance < distanceToFood)
                        {
                            distanceToFood = distance;
                            animals[i].food = foods[u];
                            animals[i].foundFood = true;                            
                        }
                    }
                }
                if (animals[i].foundFood)
                {
                    animals[i].food.targeted = true;
                }
            }
        }

        for (int i = 0; i < animals.Count; i++)
        {
            animals[i].UpdateAnimal();
        }
    }

    public void AddAnimal(Animal animal)
    {
        animals.Add(animal);
    }

    public void RemoveAnimal(Animal animal)
    {
        animals.Remove(animal);
    }



    public void RemoveFood(Food food)
    {
        foods.Remove(food);
    }

    void SpawnFood()
    {
        for (int i = 0; i < foodSpawnNum; i++)
        {
            Vector3 location = new Vector3(Random.Range(-49f, 49f), 0, Random.Range(-49f, 49f));
            Food food = Instantiate(prefabFood, location, Quaternion.identity) as Food;
            foods.Add(food);

        }
    }

    void ListAnimals()
    {
        Debug.Log(animals.Count);
    }
}
