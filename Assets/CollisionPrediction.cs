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
            // else if (Math.Abs(Math.Round(v.angle) - Math.Round(my_vehicle.angle)) == 180 || Math.Abs(Math.Round(v.angle) - Math.Round(my_vehicle.angle)) == 0)
            // {
            //     Debug.Log("Collision :: Lines are parallel");

            //     if ((Math.Round(my_vehicle.angle) == 90 || Math.Round(my_vehicle.angle) == 270) && (Math.Round(v.angle) == 90 || Math.Round(v.angle) == 270))
            //     {
            //         Debug.Log("Collision :: Angle = 90 or 270");
            //         if (Math.Abs(my_vehicle.z - v.z) > RADIUS * 2
            //             || ((Math.Round(my_vehicle.angle) == 270 && Math.Round(v.angle) == 90 && v.x > my_vehicle.x)
            //             || (Math.Round(my_vehicle.angle) == 90 && Math.Round(v.angle) == 270 && v.x < my_vehicle.x)
            //             || (my_vehicle.speed == 0 && v.speed == 0)
            //             || (Math.Round(my_vehicle.angle) == Math.Round(v.angle) && my_vehicle.speed == v.speed)
            //             || (Math.Round(my_vehicle.angle) == Math.Round(v.angle) && my_vehicle.speed > v.speed && my_vehicle.x > v.x)
            //             || (Math.Round(my_vehicle.angle) == Math.Round(v.angle) && my_vehicle.speed < v.speed && my_vehicle.x < v.x)
            //             && Math.Abs(my_vehicle.x - v.x) > RADIUS * 2))
            //         {
            //             Debug.Log("Collision :: Angle = 90 or 270 but no collision");
            //             continue;
            //         }
            //     }
            //     else
            //     {
            //         Debug.Log("Collision :: Angle != 90 nor 270");
            //         float m = (float)Math.Tan(Mathf.Deg2Rad * Math.Round(my_vehicle.angle));
            //         float c = my_vehicle.x - m * my_vehicle.z;
            //         float d = (float)Math.Abs((m * v.z - v.x + c) / Math.Sqrt(m * m + 1));
            //         Debug.Log("Collision :: " + (bool)(((Math.Round(my_vehicle.angle) >= 0 && Math.Round(my_vehicle.angle) < 90 && Math.Round(v.angle) > 180 && Math.Round(v.angle) < 270) || (Math.Round(my_vehicle.angle) > 90 && Math.Round(my_vehicle.angle) <= 180 && Math.Round(v.angle) > 270 && Math.Round(v.angle) < 360)) && v.z < my_vehicle.z));
            //         // Debug.Log("Collision :: D="+d.ToString());
            //         if (d > RADIUS * 2
            //             || (((Math.Round(my_vehicle.angle) > 180 && Math.Round(my_vehicle.angle) < 270 && Math.Round(v.angle) >= 0 && Math.Round(v.angle) < 90) || (Math.Round(my_vehicle.angle) > 90 && Math.Round(my_vehicle.angle) <= 180 && Math.Round(v.angle) > 270 && Math.Round(v.angle) < 360)) && v.z > my_vehicle.z)
            //             || (((Math.Round(my_vehicle.angle) >= 0 && Math.Round(my_vehicle.angle) < 90 && Math.Round(v.angle) > 180 && Math.Round(v.angle) < 270) || (Math.Round(my_vehicle.angle) > 270 && Math.Round(my_vehicle.angle) < 360 && Math.Round(v.angle) > 90 && Math.Round(v.angle) <= 180)) && v.z < my_vehicle.z)
            //             || (my_vehicle.speed == 0 && v.speed == 0)
            //             || (((my_vehicle.speed == v.speed
            //             || (((Math.Round(my_vehicle.angle) >= 0 && Math.Round(my_vehicle.angle) < 90) || (Math.Round(my_vehicle.angle) > 270 && Math.Round(my_vehicle.angle) < 360)) && my_vehicle.speed > v.speed && my_vehicle.z > v.z)
            //             || (((Math.Round(my_vehicle.angle) >= 0 && Math.Round(my_vehicle.angle) < 90) || (Math.Round(my_vehicle.angle) > 270 && Math.Round(my_vehicle.angle) < 360)) && my_vehicle.speed < v.speed && my_vehicle.z < v.z)
            //             || (((Math.Round(my_vehicle.angle) > 180 && Math.Round(my_vehicle.angle) < 270) || (Math.Round(my_vehicle.angle) > 90 && Math.Round(my_vehicle.angle) <= 180)) && my_vehicle.speed > v.speed && my_vehicle.z < v.z)
            //             || (((Math.Round(my_vehicle.angle) > 180 && Math.Round(my_vehicle.angle) < 270) || (Math.Round(my_vehicle.angle) > 90 && Math.Round(my_vehicle.angle) <= 180)) && my_vehicle.speed < v.speed && my_vehicle.z > v.z))) && Math.Round(my_vehicle.angle) == Math.Round(v.angle)))
            //         {
            //             Debug.Log("Collision :: Angle != 90 nor 270 and no collision");
            //             continue;
            //         }
            //     }
            //     if (Math.Round(my_vehicle.angle) == Math.Round(v.angle))
            //     {
            //         time2Collide = (float)(Math.Abs(Math.Sqrt(Math.Pow(my_vehicle.x - v.x, 2) + Math.Pow(my_vehicle.z - v.z, 2)) - RADIUS * 2) / Math.Abs(my_vehicle.speed - v.speed));
            //         Debug.Log("Collision :: Calculating AngleV = AngleM");
            //     }
            //     else
            //     {
            //         time2Collide = (float)(Math.Abs(Math.Sqrt(Math.Pow(my_vehicle.x - v.x, 2) + Math.Pow(my_vehicle.z - v.z, 2)) - RADIUS * 2) / (my_vehicle.speed + v.speed));
            //         Debug.Log("Collision :: Calculating AngleV != AngleM");
            //     }
            // }
            else
            {
                if (!(my_vehicle.speed == 0 && v.speed == 0))
                {
                    // Debug.Log("Colliison :: both the vehicles are not stationary");
                    if (my_vehicle.speed == 0)
                    {
                        // Debug.Log("Colliison :: our vehicle is Stationary");
                        if (v.angle == 90 || v.angle == 270)
                        {
                            // Debug.Log("Colliison :: surrounding vehicle angle is 90 || 270");
                            if (!(Mathf.Abs(my_vehicle.z - v.z) > RADIUS * 2
                                || (my_vehicle.x - v.x > 0 && v.angle == 270)
                                || (my_vehicle.x - v.x < 0 && v.angle == 90)))
                            {
                                // Debug.Log("Colliison :: potential collision");
                                time2Collide = (Mathf.Abs(my_vehicle.x - v.x) - RADIUS * 2) / v.speed;
                            }
                        }
                        else
                        {
                            // Debug.Log("Colliison :: surrounding vehicle angle is not 90 || 270");
                            float m = Mathf.Tan(Mathf.Deg2Rad * v.angle);
                            float c = v.x - m * v.z;
                            float d = Mathf.Abs((m * my_vehicle.z - my_vehicle.x + c) / Mathf.Sqrt(m * m + 1));
                            if (!(d > RADIUS * 2
                                || (v.x - my_vehicle.x >= 0 && v.z - my_vehicle.z > 0 && v.angle >= 0 && v.angle < 90)
                                || (v.x - my_vehicle.x >= 0 && v.z - my_vehicle.z < 0 && v.angle > 90 && v.angle <= 180)
                                || (v.x - my_vehicle.x < 0 && v.z - my_vehicle.z < 0 && v.angle > 180 && v.angle < 270)
                                || (v.x - my_vehicle.x < 0 && v.z - my_vehicle.z > 0 && v.angle > 270 && v.angle < 360)))
                            {
                                // Debug.Log("Colliison :: potential collision");
                                time2Collide = (Mathf.Sqrt(Mathf.Pow(my_vehicle.x - v.x, 2) + Mathf.Pow(my_vehicle.z - v.z, 2)) - RADIUS * 2) / v.speed;
                            }
                        }
                    }
                    else if (v.speed == 0)
                    {
                        // Debug.Log("Colliison :: surrounding vehicle is Stationary");
                        if (my_vehicle.angle == 90 || my_vehicle.angle == 270)
                        {
                            // Debug.Log("Colliison :: our vehicle angle is 90 || 270");
                            if (!(Mathf.Abs(my_vehicle.z - v.z) > RADIUS * 2
                                || (v.x - my_vehicle.x > 0 && my_vehicle.angle == 270)
                                || (v.x - my_vehicle.x < 0 && my_vehicle.angle == 90)))
                            {
                                // Debug.Log("Colliison :: potential collision");
                                time2Collide = (Mathf.Abs(my_vehicle.x - v.x) - RADIUS * 2) / my_vehicle.speed;
                            }
                        }
                        else
                        {
                            // Debug.Log("Colliison :: our vehicle angle is not 90 || 270");
                            float m = Mathf.Tan(Mathf.Deg2Rad * my_vehicle.angle);
                            float c = my_vehicle.x - m * my_vehicle.z;
                            float d = Mathf.Abs((m * v.z - v.x + c) / Mathf.Sqrt(m * m + 1));
                            if (!(d > RADIUS * 2
                                || (my_vehicle.x - v.x >= 0 && my_vehicle.z - v.z > 0 && my_vehicle.angle >= 0 && my_vehicle.angle < 90)
                                || (my_vehicle.x - v.x >= 0 && my_vehicle.z - v.z < 0 && my_vehicle.angle > 90 && my_vehicle.angle <= 180)
                                || (my_vehicle.x - v.x < 0 && my_vehicle.z - v.z < 0 && my_vehicle.angle > 180 && my_vehicle.angle < 270)
                                || (my_vehicle.x - v.x < 0 && my_vehicle.z - v.z > 0 && my_vehicle.angle > 270 && my_vehicle.angle < 360)))
                            {
                                // Debug.Log("Colliison :: potential collision");
                                time2Collide = (Mathf.Sqrt(Mathf.Pow(my_vehicle.x - v.x, 2) + Mathf.Pow(my_vehicle.z - v.z, 2)) - RADIUS * 2) / my_vehicle.speed;
                            }
                        }
                    }
                    else
                    {
                        float zIntersect = 0;
                        float xIntersect = 0;
                        float c = 0;
                        float m = 0;
                        float c1 = 0;
                        float m1 = 0;
                        bool m_in_range;
                        bool m1_in_range;
                        bool m_checked = false;
                        bool m1_checked = false;
                        float time2Collide_my_vehicle_max;
                        float time2Collide_my_vehicle_min;
                        float time2Collide_v_max;
                        float time2Collide_v_min;

                        if (my_vehicle.angle == 90 || my_vehicle.angle == 270)
                        {
                            Debug.Log("Collision :: our vehicle angle is 90 || 270");
                            zIntersect = my_vehicle.z;
                            m1 = Mathf.Tan(v.angle);
                            c1 = v.x - m1 * v.z;
                            xIntersect = m1 * zIntersect + c1;
                            // Debug.Log("Collision :: xIntersect = "+xIntersect.ToString());
                            // Debug.Log("Collision :: m.x = "+my_vehicle.x.ToString());
                            if ((xIntersect > my_vehicle.x && my_vehicle.angle == 270)
                                || (xIntersect < my_vehicle.x && my_vehicle.angle == 90))
                            {
                                Debug.Log("Collision :: our vehicle is not facing the surrounding Vehicle");
                                m_in_range = false;
                            }
                            else
                            {
                                Debug.Log("Collision :: our vehicle is facing the surrounding Vehicle");
                                m_in_range = true;
                            }
                            m_checked = true;
                        }
                        else if (v.angle == 90 || v.angle == 270)
                        {
                            Debug.Log("Collision :: surrounding vehicle angle is 90 || 270");
                            zIntersect = v.z;
                            m = Mathf.Tan(my_vehicle.angle);
                            c = my_vehicle.x - m * my_vehicle.z;
                            xIntersect = m * zIntersect + c;
                            if ((xIntersect > v.x && v.angle == 270)
                                || (xIntersect < v.x && v.angle == 90))
                            {
                                Debug.Log("Collision :: our vehicle is not facing the surrounding Vehicle");
                                m1_in_range = false;
                            }
                            else
                            {
                                Debug.Log("Collision :: our vehicle is facing the surrounding Vehicle");
                                m1_in_range = true;
                            }
                            m1_checked = true;
                        }
                        else
                        {
                            m = Mathf.Tan(my_vehicle.angle);
                            c = my_vehicle.x - m * my_vehicle.z;
                            m1 = Mathf.Tan(v.angle);
                            c1 = v.x - m1 * v.z;
                            zIntersect = (c1 - c) / (m - m1);
                            xIntersect = m * zIntersect + c;
                        }
                        // if (m_checked == false)
                        // {
                        //     if (m == 0)
                        //     {
                        //         Debug.Log("Collision :: m=0");
                        //         if ((v.z - my_vehicle.z >= 0 && my_vehicle.angle == 0) || (v.z - my_vehicle.z < 0 && my_vehicle.angle == 180))
                        //         {
                        //             Debug.Log("Collision :: ");
                        //             m_in_range = true;
                        //         }
                        //         else
                        //         {
                        //             m_in_range = false;
                        //         }
                        //     }
                        //     else
                        //     {
                        //         float mPerp = -1 / m;
                        //         float cPerp = my_vehicle.x - mPerp * my_vehicle.z;
                        //         if ((mPerp > 0 && ((xIntersect * -1 + mPerp * zIntersect + cPerp > 0 && my_vehicle.angle > 180 && my_vehicle.angle < 360) || (xIntersect * -1 + mPerp * zIntersect + cPerp < 0 && my_vehicle.angle > 0 && my_vehicle.angle < 180)))
                        //         || (mPerp < 0 && ((xIntersect * -1 + mPerp * zIntersect + cPerp < 0 && my_vehicle.angle > 180 && my_vehicle.angle < 360) || (xIntersect * -1 + mPerp * zIntersect + cPerp > 0 && my_vehicle.angle > 0 && my_vehicle.angle < 180))))
                        //             m_in_range = false;
                        //         else
                        //         {
                        //             m_in_range = true;
                        //         }
                        //     }
                        // }
                        // if (m1_checked == false)
                        // {
                        //     if (m1 == 0)
                        //     {
                        //         if ((my_vehicle.z - v.z >= 0 && v.angle == 0) || (my_vehicle.z - v.z < 0 && v.angle == 180))
                        //             m1_in_range = true;
                        //         else
                        //             m1_in_range = false;
                        //     }
                        //     else
                        //     {
                        //         float mPerp = -1 / m1;
                        //         float cPerp = v.x - mPerp * v.z;
                        //         if ((mPerp > 0 && ((xIntersect * -1 + mPerp * zIntersect + cPerp > 0 && v.angle > 180 && v.angle < 360) || (xIntersect * -1 + mPerp * zIntersect + cPerp < 0 && v.angle > 0 && v.angle < 180)))
                        //         || (mPerp < 0 && ((xIntersect * -1 + mPerp * zIntersect + cPerp < 0 && v.angle > 180 && v.angle < 360) || (xIntersect * -1 + mPerp * zIntersect + cPerp > 0 && v.angle > 0 && v.angle < 180))))
                        //             m1_in_range = false;
                        //         else
                        //         {
                        //             m1_in_range = true;
                        //         }
                        //     }
                        // }

                    }
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
