using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPrediction : MonoBehaviour
{

    public my_active_vehicle my_vehicle;
    public List<CarController> surrounding_vehicles = new List<CarController>();
    //private bool collide;

    public float radius = 3f;
    public float time2Collide;
    public float time_threshold = 5;

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
        //collide = false;
        foreach (CarController v in surrounding_vehicles)
        {
            if (Math.Abs(my_vehicle.x - v.x) <= radius * 2 && Math.Abs(my_vehicle.z - v.z) <= radius * 2)
            {
                time2Collide = 0;
                //Debug.Log("Collision :: touching each other : "+ radius);
                break;
            }
            else if (Math.Abs(Math.Round(v.angle) - Math.Round(my_vehicle.angle)) == 180 || Math.Abs(Math.Round(v.angle) - Math.Round(my_vehicle.angle)) == 0)
            {
                //Debug.Log("Collision :: Lines are parallel");
                //Debug.Log("Collision :: " + (bool)(Math.Round(my_vehicle.angle) == Math.Round(v.angle) && my_vehicle.speed < v.speed && my_vehicle.x < v.x));
                if ((Math.Round(my_vehicle.angle) == 90 || Math.Round(my_vehicle.angle) == 270) && (Math.Round(v.angle) == 90 || Math.Round(v.angle) == 270))
                {
                    //Debug.Log("Collision :: Angle = 90 or 270");
                    if (Math.Abs(my_vehicle.z - v.z) > radius * 2
                        || (((Math.Round(my_vehicle.angle) == 270 && Math.Round(v.angle) == 90 && v.x > my_vehicle.x)
                        || (Math.Round(my_vehicle.angle) == 90 && Math.Round(v.angle) == 270 && v.x < my_vehicle.x)
                        || (my_vehicle.speed == 0 && v.speed == 0)
                        || (Math.Round(my_vehicle.angle) == Math.Round(v.angle) && my_vehicle.speed == v.speed)
                        || (Math.Round(my_vehicle.angle) == 270 && Math.Round(v.angle) == 270 && my_vehicle.speed < v.speed && my_vehicle.x > v.x)
                        || (Math.Round(my_vehicle.angle) == 90 && Math.Round(v.angle) == 90 && my_vehicle.speed < v.speed && my_vehicle.x < v.x))
                        && Math.Abs(my_vehicle.x - v.x) > radius * 2))
                    {
                        //Debug.Log("Collision :: Angle = 90 or 270 but no collision : " + radius);
                        continue;
                    }
                }
                else
                {
                    Debug.Log("Collision :: Angle != 90 nor 270");
                    float m = (float)Math.Tan(Mathf.Deg2Rad * Math.Round(my_vehicle.angle));
                    float c = my_vehicle.x - m * my_vehicle.z;
                    float d = (float)Math.Abs((m * v.z - v.x + c) / Math.Sqrt(m * m + 1));
                    //Debug.Log("Collision :: " + (bool)(((Math.Round(my_vehicle.angle) >= 0 && Math.Round(my_vehicle.angle) < 90 && Math.Round(v.angle) > 180 && Math.Round(v.angle) < 270) || (Math.Round(my_vehicle.angle) > 90 && Math.Round(my_vehicle.angle) <= 180 && Math.Round(v.angle) > 270 && Math.Round(v.angle) < 360)) && v.z < my_vehicle.z));
                    // Debug.Log("Collision :: D="+d.ToString());
                    if (d > radius * 2
                        || (((Math.Round(my_vehicle.angle) > 180 && Math.Round(my_vehicle.angle) < 270 && Math.Round(v.angle) >= 0 && Math.Round(v.angle) < 90) || (Math.Round(my_vehicle.angle) > 90 && Math.Round(my_vehicle.angle) <= 180 && Math.Round(v.angle) > 270 && Math.Round(v.angle) < 360)) && v.z > my_vehicle.z)
                        || (((Math.Round(my_vehicle.angle) >= 0 && Math.Round(my_vehicle.angle) < 90 && Math.Round(v.angle) > 180 && Math.Round(v.angle) < 270) || (Math.Round(my_vehicle.angle) > 270 && Math.Round(my_vehicle.angle) < 360 && Math.Round(v.angle) > 90 && Math.Round(v.angle) <= 180)) && v.z < my_vehicle.z)
                        || (my_vehicle.speed == 0 && v.speed == 0)
                        || (((my_vehicle.speed == v.speed
                        || (((Math.Round(my_vehicle.angle) >= 0 && Math.Round(my_vehicle.angle) < 90) || (Math.Round(my_vehicle.angle) > 270 && Math.Round(my_vehicle.angle) < 360)) && my_vehicle.speed > v.speed && my_vehicle.z > v.z)
                        || (((Math.Round(my_vehicle.angle) >= 0 && Math.Round(my_vehicle.angle) < 90) || (Math.Round(my_vehicle.angle) > 270 && Math.Round(my_vehicle.angle) < 360)) && my_vehicle.speed < v.speed && my_vehicle.z < v.z)
                        || (((Math.Round(my_vehicle.angle) > 180 && Math.Round(my_vehicle.angle) < 270) || (Math.Round(my_vehicle.angle) > 90 && Math.Round(my_vehicle.angle) <= 180)) && my_vehicle.speed > v.speed && my_vehicle.z < v.z)
                        || (((Math.Round(my_vehicle.angle) > 180 && Math.Round(my_vehicle.angle) < 270) || (Math.Round(my_vehicle.angle) > 90 && Math.Round(my_vehicle.angle) <= 180)) && my_vehicle.speed < v.speed && my_vehicle.z > v.z))) && Math.Round(my_vehicle.angle) == Math.Round(v.angle)))
                    {
                        //Debug.Log("Collision :: Angle != 90 nor 270 and no collision");
                        continue;
                    }
                }
                if (Math.Round(my_vehicle.angle) == Math.Round(v.angle))
                {
                    time2Collide = (float)(Math.Abs(Math.Sqrt(Math.Pow(my_vehicle.x - v.x, 2) + Math.Pow(my_vehicle.z - v.z, 2)) - radius * 2) / Math.Abs(my_vehicle.speed - v.speed));
                    //Debug.Log("Collision :: Calculating AngleV = AngleM");
                }
                else
                {
                    time2Collide = (float)(Math.Abs(Math.Sqrt(Math.Pow(my_vehicle.x - v.x, 2) + Math.Pow(my_vehicle.z - v.z, 2)) - radius * 2) / (my_vehicle.speed + v.speed));
                    //Debug.Log("Collision :: Calculating AngleV != AngleM");
                }
            }
            else
            {
                if (!(Mathf.Round(my_vehicle.speed) == 0 && Mathf.Round(v.speed) == 0))
                {
                     //Debug.Log("Colliison :: both the vehicles are not stationary");
                    if (Math.Round(my_vehicle.speed) == 0)
                    {
                        //Debug.Log("Colliison :: our vehicle is Stationary");
                        if (Mathf.Round(v.angle) == 90 || Mathf.Round(v.angle) == 270)
                        {
                            //Debug.Log("Colliison :: surrounding vehicle angle is 90 || 270");
                            if (!(Mathf.Abs(my_vehicle.z - v.z) > radius * 2
                                || (my_vehicle.x - v.x > 0 && Mathf.Round(v.angle) == 270)
                                || (my_vehicle.x - v.x < 0 && Mathf.Round(v.angle) == 90)))
                            {
                                //Debug.Log("Colliison :: potential collision");
                                time2Collide = (Mathf.Abs(my_vehicle.x - v.x) - radius * 2) / v.speed;
                            }
                        }
                        else
                        {
                            //Debug.Log("Colliison :: surrounding vehicle angle is not 90 || 270");
                            float m = Mathf.Tan(Mathf.Deg2Rad * v.angle);
                            float c = v.x - m * v.z;
                            float d = Mathf.Abs((m * my_vehicle.z - my_vehicle.x + c) / Mathf.Sqrt(m * m + 1));
                            if (d > radius * 2)
                                continue;
                            if((my_vehicle.x - v.x >= 0 && my_vehicle.z - v.z > 0 && v.angle >= 0 && v.angle < 90)
                                || (my_vehicle.x - v.x >= 0 && my_vehicle.z - v.z < 0 && v.angle > 90 && v.angle <= 180)
                                || (my_vehicle.x - v.x < 0 && my_vehicle.z - v.z < 0 && v.angle > 180 && v.angle < 270)
                                || (my_vehicle.x - v.x < 0 && my_vehicle.z - v.z > 0 && v.angle > 270 && v.angle < 360))
                            {
                                //Debug.Log("Colliison :: potential collision");
                                time2Collide = (Mathf.Sqrt(Mathf.Pow(my_vehicle.x - v.x, 2) + Mathf.Pow(my_vehicle.z - v.z, 2)) - radius * 2) / v.speed;
                            }
                        }
                    }
                    else if (Math.Round(v.speed) == 0)
                    {
                        // Debug.Log("Colliison :: surrounding vehicle is Stationary");
                        if (Mathf.Round(my_vehicle.angle) == 90 || Mathf.Round(my_vehicle.angle) == 270)
                        {
                            // Debug.Log("Colliison :: our vehicle angle is 90 || 270");
                            if (!(Mathf.Abs(my_vehicle.z - v.z) > radius * 2
                                || (v.x - my_vehicle.x > 0 && Mathf.Round(my_vehicle.angle) == 270)
                                || (v.x - my_vehicle.x < 0 && Mathf.Round(my_vehicle.angle) == 90)))
                            {
                                // Debug.Log("Colliison :: potential collision");
                                time2Collide = (Mathf.Abs(my_vehicle.x - v.x) - radius * 2) / my_vehicle.speed;
                            }
                        }
                        else
                        {
                            // Debug.Log("Colliison :: our vehicle angle is not 90 || 270");
                            float m = Mathf.Tan(Mathf.Deg2Rad * my_vehicle.angle);
                            float c = my_vehicle.x - m * my_vehicle.z;
                            float d = Mathf.Abs((m * v.z - v.x + c) / Mathf.Sqrt(m * m + 1));
                            if (d > radius * 2)
                                continue;
                            if ((v.x - my_vehicle.x >= 0 && v.z - my_vehicle.z > 0 && my_vehicle.angle >= 0 && my_vehicle.angle < 90)
                                || (v.x - my_vehicle.x >= 0 && v.z - my_vehicle.z < 0 && my_vehicle.angle > 90 && my_vehicle.angle <= 180)
                                || (v.x - my_vehicle.x < 0 && v.z - my_vehicle.z < 0 && my_vehicle.angle > 180 && my_vehicle.angle < 270)
                                || (v.x - my_vehicle.x < 0 && v.z - my_vehicle.z > 0 && my_vehicle.angle > 270 && my_vehicle.angle < 360))
                            {
                                // Debug.Log("Colliison :: potential collision");
                                time2Collide = (Mathf.Sqrt(Mathf.Pow(my_vehicle.x - v.x, 2) + Mathf.Pow(my_vehicle.z - v.z, 2)) - radius * 2) / my_vehicle.speed;
                            }
                        }
                    }
                    else
                    {
                        float zintersect = 0;
                        float xintersect = 0;
                        float c = 0;
                        float m = 0;
                        float c1 = 0;
                        float m1 = 0;
                        bool m_in_range = false;
                        bool m1_in_range = false;
                        bool m_checked = false;
                        bool m1_checked = false;
                        float time2Collide_my_vehicle_max;
                        float time2Collide_my_vehicle_min;
                        float time2Collide_v_max;
                        float time2Collide_v_min;

                        if (Mathf.Round(my_vehicle.angle) == 90 || Mathf.Round(my_vehicle.angle) == 270)
                        {
                            //Debug.Log("collision :: our vehicle angle is 90 || 270");
                            zintersect = my_vehicle.z;
                            m1 = Mathf.Tan(Mathf.Round(v.angle) * Mathf.Deg2Rad);
                            c1 = v.x - m1 * v.z;
                            xintersect = m1 * zintersect + c1;
                            if ((xintersect > my_vehicle.x && Mathf.Round(my_vehicle.angle) == 270)
                                || (xintersect < my_vehicle.x && Mathf.Round(my_vehicle.angle) == 90))
                            {
                                //Debug.Log("collision :: our vehicle is not facing the surrounding vehicle");
                                m_in_range = false;
                            }
                            else
                            {
                                //Debug.Log("collision :: our vehicle is facing the surrounding vehicle");
                                m_in_range = true;
                            }
                            m_checked = true;
                        }
                        else if (v.angle == 90 || v.angle == 270)
                        {
                            //Debug.Log("collision :: surrounding vehicle angle is 90 || 270");
                            zintersect = v.z;
                            m = Mathf.Tan(Mathf.Round(my_vehicle.angle) * Mathf.Deg2Rad);
                            c = my_vehicle.x - m * my_vehicle.z;
                            xintersect = m * zintersect + c;
                            if ((xintersect > v.x && Mathf.Round(v.angle) == 270)
                                || (xintersect < v.x && Mathf.Round(v.angle) == 90))
                            {
                                //Debug.Log("collision :: our vehicle is not facing the surrounding vehicle");
                                m1_in_range = false;
                            }
                            else
                            {
                                //Debug.Log("collision :: our vehicle is facing the surrounding vehicle");
                                m1_in_range = true;
                            }
                            m1_checked = true;
                        }
                        else
                        {
                            m = Mathf.Tan(my_vehicle.angle * Mathf.Deg2Rad);
                            c = my_vehicle.x - m * my_vehicle.z;
                            m1 = Mathf.Tan(Mathf.Round(v.angle) * Mathf.Deg2Rad);
                            //Debug.Log("collision :: m1 = " + m1.tostring());
                            c1 = v.x - m1 * v.z;
                            zintersect = (c1 - c) / (m - m1);
                            xintersect = m * zintersect + c;
                            //Debug.Log("Not a special case!!!");
                        }
                        if (m_checked == false)
                        {
                            if (Mathf.Round(my_vehicle.angle) == 0 || Mathf.Round(my_vehicle.angle) == 180)
                            {
                                //Debug.Log("collision :: m=0");
                                if ((my_vehicle.z > zintersect && Mathf.Round(my_vehicle.angle) == 0) || (my_vehicle.z < zintersect && Mathf.Round(my_vehicle.angle) == 180))
                                {
                                    //Debug.Log("collision :: our vehicle is not facing the surrounding vehicle");
                                    m_in_range = false;
                                }
                                else
                                {
                                    //Debug.Log("collision :: our vehicle is facing the surrounding vehicle");
                                    m_in_range = true;
                                }
                            }
                            else
                            {
                                //Debug.Log("collision :: not one of the special cases in m");
                                float mperp = -1 / m;
                                float cperp = my_vehicle.x - mperp * my_vehicle.z;
                                if ((mperp > 0 && ((xintersect * -1 + mperp * zintersect + cperp < 0 && my_vehicle.angle > 180 && my_vehicle.angle < 360) || (xintersect * -1 + mperp * zintersect + cperp > 0 && my_vehicle.angle > 0 && my_vehicle.angle < 180)))
                                || (mperp < 0 && ((xintersect * -1 + mperp * zintersect + cperp < 0 && my_vehicle.angle > 180 && my_vehicle.angle < 360) || (xintersect * -1 + mperp * zintersect + cperp > 0 && my_vehicle.angle > 0 && my_vehicle.angle < 180))))
                                {
                                    //Debug.Log("collision :: our vehicle is not facing the surrounding vehicle");
                                    m_in_range = false;
                                }
                                else
                                {
                                    //Debug.Log("collision :: our vehicle is facing the surrounding vehicle");
                                    m_in_range = true;
                                }
                            }
                        }
                        //Debug.Log("collision :: zintersect = " + zintersect.tostring());
                        if (m1_checked == false)
                        {
                            if (Mathf.Round(v.angle) == 0 || Mathf.Round(v.angle) == 180)
                            {
                                //Debug.Log("collision :: m1 = 0");
                                if ((v.z > zintersect && Math.Round(v.angle) == 0) || (v.z < zintersect && Math.Round(v.angle) == 180))
                                {
                                    //Debug.Log("collision :: our vehicle is not facing the surrounding vehicle");
                                    m1_in_range = false;
                                }
                                else
                                {
                                    //Debug.Log("collision :: our vehicle is facing the surrounding vehicle");
                                    m1_in_range = true;
                                }
                            }
                            else
                            {
                                //Debug.Log("collision :: not one of the special cases in m1");
                                float mperp = -1 / m1;
                                float cperp = v.x - mperp * v.z;
                                if ((mperp > 0 && ((xintersect * -1 + mperp * zintersect + cperp < 0 && v.angle > 180 && v.angle < 360) || (xintersect * -1 + mperp * zintersect + cperp > 0 && v.angle > 0 && v.angle < 180)))
                                || (mperp < 0 && ((xintersect * -1 + mperp * zintersect + cperp < 0 && v.angle > 180 && v.angle < 360) || (xintersect * -1 + mperp * zintersect + cperp > 0 && v.angle > 0 && v.angle < 180))))
                                {
                                    //Debug.Log("collision :: our vehicle is not facing the surrounding vehicle");
                                    m1_in_range = false;
                                }
                                else
                                {
                                    //Debug.Log("collision :: our vehicle is facing the surrounding vehicle");
                                    m1_in_range = true;
                                }
                            }
                        }
                        Debug.Log("m_in_range = " + (bool)m_in_range);
                        Debug.Log("m1_in_range = " + (bool)m1_in_range);
                        if (m_in_range == true && m1_in_range == true)
                        {
                            float time4radius_my_vehicle = radius / my_vehicle.speed;
                            float time4radius_v = radius / v.speed;
                            float disTance_my_vehicle = (float)Math.Sqrt(Math.Pow(my_vehicle.x - xintersect, 2) + Math.Pow(my_vehicle.z - zintersect, 2));
                            float disTance_v = (float)Math.Sqrt(Math.Pow(v.x - xintersect, 2) + Math.Pow(v.z - zintersect, 2));
                            time2Collide_my_vehicle_max = disTance_my_vehicle / my_vehicle.speed + time4radius_my_vehicle;
                            time2Collide_my_vehicle_min = disTance_my_vehicle / my_vehicle.speed - time4radius_my_vehicle;
                            time2Collide_v_max = disTance_v / v.speed + time4radius_v;
                            time2Collide_v_min = disTance_v / v.speed - time4radius_v;
                            if (time2Collide_my_vehicle_min > time2Collide_v_min && time2Collide_my_vehicle_min < time2Collide_v_max)
                                time2Collide = time2Collide_my_vehicle_min;
                            else if (time2Collide_v_min > time2Collide_my_vehicle_min && time2Collide_v_min < time2Collide_my_vehicle_max)
                                time2Collide = time2Collide_v_min;
                        }
                        else if (m_in_range == true && m1_in_range == false)
                        {
                            float time4radius_my_vehicle = radius / my_vehicle.speed;
                            float disTance_v = (float)Math.Sqrt(Math.Pow(v.x - xintersect, 2) + Math.Pow(v.z - zintersect, 2));
                            float extra_disTance;
                            float disTance_my_vehicle = (float)Math.Sqrt(Math.Pow(my_vehicle.x - xintersect, 2) + Math.Pow(my_vehicle.z - zintersect, 2));
                            float time;
                            if (disTance_v <= radius * 2)
                            {
                                time = disTance_my_vehicle / my_vehicle.speed;
                                time2Collide_my_vehicle_max = time + time4radius_my_vehicle;
                                time2Collide_my_vehicle_min = time - time4radius_my_vehicle;
                                extra_disTance = v.speed * time;
                                if (disTance_v + extra_disTance <= radius * 2)
                                    time2Collide = time2Collide_my_vehicle_min;
                            }
                        }
                        else if (m_in_range == false && m1_in_range == true)
                        {
                            float time4radius_v = radius / v.speed;
                            float disTance_v = (float)Math.Sqrt(Math.Pow(v.x - xintersect, 2) + Math.Pow(v.z - zintersect, 2));
                            float extra_disTance;
                            float disTance_my_vehicle = (float)Math.Sqrt(Math.Pow(my_vehicle.x - xintersect, 2) + Math.Pow(my_vehicle.z - zintersect, 2));
                            float time;
                            if (disTance_my_vehicle <= radius * 2)
                            {
                                time = disTance_v / v.speed;
                                time2Collide_v_max = time + time4radius_v;
                                time2Collide_v_min = time - time4radius_v;
                                extra_disTance = my_vehicle.speed * time;
                                if (disTance_my_vehicle + extra_disTance <= radius * 2)
                                    time2Collide = time2Collide_v_min;
                            }
                        }

                    }
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
            Debug.Log("No Collision");
            Capsule.material.color = new Color(1, 1, 1);
        }

    }

}
