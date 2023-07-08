using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPrediction : MonoBehaviour
{

    public my_active_vehicle my_vehicle;
    public List<Vehicle> surrounding_vehicles = new List<Vehicle>();
    private bool collide;

    public float RADIUS = 1.7f;
    public float time2Collide;
    public float time_threshold = 5f;

    MeshRenderer Capsule;

    // Start is called before the first frame update
    void Start()
    {
        my_vehicle = my_vehicle.GetComponent<my_active_vehicle>();
        Capsule = GameObject.Find("Capsule").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        time2Collide = float.PositiveInfinity;
        collide = false;
        foreach (Vehicle v in surrounding_vehicles)
        {
            if (Math.Abs(my_vehicle.x - v.x) <= RADIUS * 2 && Math.Abs(my_vehicle.z - v.z) <= RADIUS * 2)
            {
                time2Collide = 0;
                Debug.Log("Collision :: touching each other");
                break;
            }
            else if (Math.Abs(v.angle - my_vehicle.angle) == 180 || Math.Abs(v.angle - my_vehicle.angle) == 0)
            {
                Debug.Log("Collision :: Lines are parallel");

                if ((my_vehicle.angle == 90 || my_vehicle.angle == 270) && (v.angle == 90 || v.angle == 270))
                {
                    Debug.Log("Collision :: Angle = 90 or 270");
                    if (Math.Abs(my_vehicle.z - v.z) > RADIUS * 2
                        || ((my_vehicle.angle == 270 && v.angle == 90 && v.x > my_vehicle.x)
                        || (my_vehicle.angle == 90 && v.angle == 270 && v.x < my_vehicle.x)
                        || (my_vehicle.speed == 0 && v.speed == 0)
                        || (my_vehicle.angle == v.angle && my_vehicle.speed == v.speed)
                        || (my_vehicle.angle == v.angle && my_vehicle.speed > v.speed && my_vehicle.x > v.x)
                        || (my_vehicle.angle == v.angle && my_vehicle.speed < v.speed && my_vehicle.x < v.x)
                        && Math.Abs(my_vehicle.x - v.x) > RADIUS * 2))
                    {
                        Debug.Log("Collision :: Angle = 90 or 270 but no collision");
                        continue;
                    }
                }
                else
                {
                    Debug.Log("Collision :: Angle != 90 nor 270");
                    float m = (float)Math.Tan(Mathf.Deg2Rad * my_vehicle.angle);
                    float c = my_vehicle.x - m * my_vehicle.z;
                    float d = (float)Math.Abs((m * v.z - v.x + c) / Math.Sqrt(m * m + 1));
                    // Debug.Log("Collision :: D="+d.ToString());
                    if (d > RADIUS * 2
                        || (((my_vehicle.angle > 180 && my_vehicle.angle < 270 && v.angle >= 0 && v.angle < 90) || (my_vehicle.angle > 270 && my_vehicle.angle < 360 && v.angle > 90 && v.angle <= 180)) && v.z > my_vehicle.z)
                        || (((my_vehicle.angle >= 0 && my_vehicle.angle < 90 && v.angle > 180 && v.angle < 270) || (my_vehicle.angle > 90 && my_vehicle.angle <= 180 && v.angle > 270 && v.angle < 360)) && v.z < my_vehicle.z)
                        || (my_vehicle.speed == 0 && v.speed == 0)
                        || ((my_vehicle.speed == v.speed
                        || ((my_vehicle.angle >= 0 && my_vehicle.angle < 90) || (my_vehicle.angle > 270 && my_vehicle.angle < 360) && my_vehicle.speed > v.speed && my_vehicle.z > v.z)
                        || ((my_vehicle.angle >= 0 && my_vehicle.angle < 90) || (my_vehicle.angle > 270 && my_vehicle.angle < 360) && my_vehicle.speed < v.speed && my_vehicle.z < v.z)
                        || ((my_vehicle.angle > 180 && my_vehicle.angle < 270) || (my_vehicle.angle > 90 && my_vehicle.angle <= 180) && my_vehicle.speed > v.speed && my_vehicle.z < v.z)
                        || ((my_vehicle.angle > 180 && my_vehicle.angle < 270) || (my_vehicle.angle > 90 && my_vehicle.angle <= 180) && my_vehicle.speed < v.speed && my_vehicle.z > v.z))) && my_vehicle.angle == v.angle)
                    {
                        Debug.Log("Collision :: Angle != 90 nor 270 and no collision");
                        continue;
                    }
                }
                if (my_vehicle.angle == v.angle)
                {
                    time2Collide = (float)(Math.Abs(Math.Sqrt(Math.Pow(my_vehicle.x - v.x, 2) + Math.Pow(my_vehicle.z - v.z, 2)) - RADIUS * 2) / Math.Abs(my_vehicle.speed - v.speed));
                    Debug.Log("Collision :: Calculating AngleV = AngleM");
                }
                else
                {
                    time2Collide = (float)(Math.Abs(Math.Sqrt(Math.Pow(my_vehicle.x - v.x, 2) + Math.Pow(my_vehicle.z - v.z, 2)) - RADIUS * 2) / (my_vehicle.speed + v.speed));
                    Debug.Log("Collision :: Calculating AngleV != AngleM");
                }
            }



            if (time2Collide <= time_threshold)
            {
                Debug.Log("Collision @ " + time2Collide.ToString());
                Capsule.material.color = new Color(1, 0, 0);
            }
            else
            {
                Capsule.material.color = new Color(1, 1, 1);
            }
        }

    }

}
