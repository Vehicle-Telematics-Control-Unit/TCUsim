using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPrediction : MonoBehaviour
{

    public my_active_vehicle my_vehicle;
    public List<Vehicle> surrounding_vehicles = new List<Vehicle>();
    private bool collide;

    public float radius = 1.7f; 

    // Start is called before the first frame update
    void Start()
    {
        my_vehicle = my_vehicle.GetComponent<my_active_vehicle>();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Vehicle v in surrounding_vehicles)
        {
            // if (my_vehicle.angle == v.angle || my_vehicle.angle + 180 == v.angle)
            //     continue;

            if ((my_vehicle.angle == 90 || my_vehicle.angle == 270) && (v.angle == 90 || v.angle == 270))
            {
                if ((my_vehicle.z != v.z && Mathf.Abs(my_vehicle.z - v.z) > radius * 2) || my_vehicle.angle != v.angle || my_vehicle.speed == v.speed)
                {
                    continue;
                }
                else
                {
                    
                }
            }
        }

    }

}
