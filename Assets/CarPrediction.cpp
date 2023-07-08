#include <CarPrediction.hpp>
#include <math.h>

bool CarPrediction::isWarn()
{
    time2Collide = INFINITY;
    collide=false;
    for (Vehicle v : surrounding_vehicles)
    {
        if (abs(my_vehicle.x - v.x) <= RADIUS * 2 && abs(my_vehicle.z - v.z) <= RADIUS * 2)
        {
            time2Collide = 0;
            break;
        }
        else if (abs(v.angle-my_vehicle.angle) == 180 || abs(v.angle-my_vehicle.angle) == 0)
        {
            if ((my_vehicle.angle == 90 || my_vehicle.angle == 270) && (v.angle == 90 || v.angle == 270))
            {
                if (abs(my_vehicle.z - v.z) > RADIUS * 2 
            || ((my_vehicle.angle == 270 && v.angle == 90 && v.x > my_vehicle.x) 
            || (my_vehicle.angle == 90 && v.angle == 270 && v.x < my_vehicle.x) 
            || (my_vehicle.speed == 0 && v.speed == 0)
            || (my_vehicle.angle == v.angle && my_vehicle.speed == v.speed) 
            || (my_vehicle.angle == v.angle && my_vehicle.speed > v.speed && my_vehicle.x > v.x)
            || (my_vehicle.angle == v.angle && my_vehicle.speed < v.speed && my_vehicle.x < v.x)
            && abs(my_vehicle.x - v.x) > RADIUS * 2))
                continue; 
            }
            else
            {
                float m=tan(my_vehicle.angle);
            float c=my_vehicle.x-m*my_vehicle.z;
            float d=abs((m*v.z-v.x+c)/sqrt(m*m+1));
            if (d>RADIUS*2 
            || ((((my_vehicle.angle > 180 && my_vehicle.angle < 270 && v.angle >=0 && v.angle < 90) || (my_vehicle.angle > 270 && my_vehicle.angle < 360 && v.angle > 90 && v.angle <= 180)) && v.z > my_vehicle.z)
            || (((my_vehicle.angle >= 0 && my_vehicle.angle < 90 && v.angle >180 && v.angle < 270) || (my_vehicle.angle > 90 && my_vehicle.angle <= 180 && v.angle > 270 && v.angle < 360)) && v.z < my_vehicle.z)
            || (my_vehicle.speed == 0 && v.speed == 0)
            || (my_vehicle.speed == v.speed 
            || ((my_vehicle.angle >= 0 && my_vehicle.angle < 90) || (my_vehicle.angle > 270 && my_vehicle.angle < 360) && my_vehicle.speed > v.speed && my_vehicle.z > v.z)
            || ((my_vehicle.angle >= 0 && my_vehicle.angle < 90) || (my_vehicle.angle > 270 && my_vehicle.angle < 360) && my_vehicle.speed < v.speed && my_vehicle.z < v.z)
            || ((my_vehicle.angle > 180 && my_vehicle.angle < 270) || (my_vehicle.angle > 90 && my_vehicle.angle <= 180) && my_vehicle.speed > v.speed && my_vehicle.z < v.z)
            || ((my_vehicle.angle > 180 && my_vehicle.angle < 270) || (my_vehicle.angle > 90 && my_vehicle.angle <= 180) && my_vehicle.speed < v.speed && my_vehicle.z > v.z) && my_vehicle.angle == v.angle)
            && abs(my_vehicle.x - v.x) > RADIUS * 2 && abs(my_vehicle.z - v.z) > RADIUS * 2))
            continue;
            }
            if (my_vehicle.angle == v.angle)
                time2Collide=abs(sqrt(pow(my_vehicle.x-v.x,2)+pow(my_vehicle.z-v.z,2))-RADIUS*2)/abs(my_vehicle.speed-v.speed);
            else
                time2Collide=abs(sqrt(pow(my_vehicle.x-v.x,2)+pow(my_vehicle.z-v.z,2))-RADIUS*2)/(my_vehicle.speed+v.speed);
        }
        else
        {
            if (!(my_vehicle.speed == 0 && v.speed == 0))
            {
            if (my_vehicle.speed == 0)
            {
                if (v.angle == 90 || v.angle == 270)
                {
                    if (!(abs(my_vehicle.z-v.z) > RADIUS*2 
                    || (my_vehicle.x-v.x > 0 && v.angle ==270)
                    || (my_vehicle.x-v.x < 0 && v.angle == 90)))
                        time2Collide=(abs(my_vehicle.z-v.z)-RADIUS*2)/v.speed;
                }
                else
                {
                float m=tan(v.angle);
                float c=v.x-m*v.z;
                float d=abs((m*my_vehicle.z-my_vehicle.x+c)/sqrt(m*m+1));
                if (!(d > RADIUS*2
                || (v.x-my_vehicle.x >= 0 && v.z-my_vehicle.z > 0 && v.angle >= 0 && v.angle < 90) 
                || (v.x-my_vehicle.x >= 0 && v.z-my_vehicle.z < 0 && v.angle > 90 && v.angle <= 180)
                || (v.x-my_vehicle.x < 0 && v.z-my_vehicle.z < 0 && v.angle > 180 && v.angle < 270)
                || (v.x-my_vehicle.x < 0 && v.z-my_vehicle.z > 0 && v.angle > 270 && v.angle < 360)))
                    time2Collide=(sqrt(pow(my_vehicle.x-v.x,2)+pow(my_vehicle.z-v.z,2))-RADIUS*2)/v.speed;
                }
            }
            else if (v.speed == 0)
            {
                if (my_vehicle.angle == 90 || my_vehicle.angle == 270)
                {
                    if (!(abs(my_vehicle.z-v.z) > RADIUS*2 
                    || (v.x-my_vehicle.x > 0 && my_vehicle.angle ==270)
                    || (v.x-my_vehicle.x < 0 && my_vehicle.angle == 90)))
                        time2Collide=(abs(my_vehicle.z-v.z)-RADIUS*2)/my_vehicle.speed;
                }
                else
                {
                float m=tan(my_vehicle.angle);
                float c=my_vehicle.x-m*my_vehicle.z;
                float d=abs((m*v.z-v.x+c)/sqrt(m*m+1));
                if (!(d > RADIUS*2 
                || (my_vehicle.x-v.x >= 0 && my_vehicle.z-v.z > 0 && my_vehicle.angle >= 0 && my_vehicle.angle < 90) 
                || (my_vehicle.x-v.x >= 0 && my_vehicle.z-v.z < 0 && my_vehicle.angle > 90 && my_vehicle.angle <= 180)
                || (my_vehicle.x-v.x < 0 && my_vehicle.z-v.z < 0 && my_vehicle.angle > 180 && my_vehicle.angle < 270)
                || (my_vehicle.x-v.x < 0 && my_vehicle.z-v.z > 0 && my_vehicle.angle > 270 && my_vehicle.angle < 360)))
                    time2Collide=(sqrt(pow(my_vehicle.x-v.x,2)+pow(my_vehicle.z-v.z,2))-RADIUS*2)/my_vehicle.speed;
                }
            }
            else
            {
            float zIntersect;
            float xIntersect;
            float c;
            float m;
            float c1;
            float m1;
            bool m_in_range;
            bool m1_in_range;
            bool m_checked=false;
            bool m1_checked=false;
            float time2Collide_my_vehicle_max;
            float time2Collide_my_vehicle_min;
            float time2Collide_v_max;
            float time2Collide_v_min;

            
            if (my_vehicle.angle == 90 || my_vehicle.angle == 270)
            {
                zIntersect=my_vehicle.z;
                m1=tan(v.angle);
                c1=v.x-m*v.z;
                xIntersect=m1*zIntersect+c1;
                if (abs(my_vehicle.z-v.z) > RADIUS*2 
                    || (v.x-my_vehicle.x > 0 && my_vehicle.angle ==270)
                    || (v.x-my_vehicle.x < 0 && my_vehicle.angle == 90))
                    m_in_range=false;
                else
                    m_in_range=true;
                m_checked=true;
            }
            else if (v.angle == 90 || v.angle == 270)
            {
                zIntersect=v.z;
                m=tan(my_vehicle.angle);
                c=my_vehicle.x-m*my_vehicle.z;
                xIntersect=m*zIntersect+c;
                if (abs(my_vehicle.z-v.z) > RADIUS*2 
                    || (my_vehicle.x-v.x > 0 && v.angle ==270)
                    || (my_vehicle.x-v.x < 0 && v.angle == 90))
                    m1_in_range=false;
                else
                    m1_in_range=true;
                m1_checked=true;
            }
            else
            {
            m=tan(my_vehicle.angle);
            c=my_vehicle.x-m*my_vehicle.z;
            m1=tan(v.angle);
            c1=v.x-m1*v.z;
            zIntersect=(c1-c)/(m-m1);
            xIntersect=m*zIntersect+c;
            }
            if (m_checked == false)
            {
            if (m == 0)
            {
                if ((v.z-my_vehicle.z >= 0 && my_vehicle.angle == 0) || (v.z-my_vehicle.z < 0 && my_vehicle.angle == 180))
                    m_in_range=true;
                else
                    m_in_range=false;
            }
            else
            {
                float mPerp=1/m*-1;
                float cPerp=my_vehicle.x-mPerp*my_vehicle.z;
                if ((mPerp > 0 && ((xIntersect*-1+mPerp*zIntersect+cPerp > 0 && my_vehicle.angle > 180 && my_vehicle.angle < 360) || (xIntersect*-1+mPerp*zIntersect+cPerp < 0 && my_vehicle.angle > 0 && my_vehicle.angle < 180)))
                || (mPerp < 0 && ((xIntersect*-1+mPerp*zIntersect+cPerp < 0 && my_vehicle.angle > 180 && my_vehicle.angle < 360) || (xIntersect*-1+mPerp*zIntersect+cPerp > 0 && my_vehicle.angle > 0 && my_vehicle.angle < 180))))
                    m_in_range=false;
                else
                {
                    m_in_range=true;
                }
            }
            }
            if (m1_checked == false)
            {
            if (m1 == 0)
            {
                if ((my_vehicle.z-v.z >= 0 && v.angle == 0) || (my_vehicle.z-v.z < 0 && v.angle == 180))
                    m1_in_range=true;
                else
                    m1_in_range=false;
            }
            else
            {
                float mPerp=1/m1*-1;
                float cPerp=v.x-mPerp*v.z;
                if ((mPerp > 0 && ((xIntersect*-1+mPerp*zIntersect+cPerp > 0 && v.angle > 180 && v.angle < 360) || (xIntersect*-1+mPerp*zIntersect+cPerp < 0 && v.angle > 0 && v.angle < 180)))
                || (mPerp < 0 && ((xIntersect*-1+mPerp*zIntersect+cPerp < 0 && v.angle > 180 && v.angle < 360) || (xIntersect*-1+mPerp*zIntersect+cPerp > 0 && v.angle > 0 && v.angle < 180))))
                    m1_in_range=false;
                else
                {
                    m1_in_range=true;
                }
            }
            }
            if (m_in_range == true && m1_in_range == true)
            {
                float time4radius_my_vehicle=RADIUS/my_vehicle.speed;
                float time4radius_v=RADIUS/v.speed;
                float distance_my_vehicle=sqrt(pow(my_vehicle.x-xIntersect,2)+pow(my_vehicle.z-zIntersect,2));
                float distance_v=sqrt(pow(v.x-xIntersect,2)+pow(v.z-zIntersect,2));
                time2Collide_my_vehicle_max=distance_my_vehicle/my_vehicle.speed+time4radius_my_vehicle;
                time2Collide_my_vehicle_min=distance_my_vehicle/my_vehicle.speed-time4radius_my_vehicle;
                time2Collide_v_max=distance_v/v.speed+time4radius_v;
                time2Collide_v_min=distance_v/v.speed-time4radius_v;
                if (time2Collide_my_vehicle_min > time2Collide_v_min && time2Collide_my_vehicle_min < time2Collide_v_max)
                    time2Collide=time2Collide_my_vehicle_min;
                else if (time2Collide_v_min > time2Collide_my_vehicle_min && time2Collide_v_min < time2Collide_my_vehicle_max)
                    time2Collide=time2Collide_v_min;
                
            }
            else if (m_in_range == true && m1_in_range == false)
            {
                float time4radius_my_vehicle=RADIUS/my_vehicle.speed;
                float distance_v=sqrt(pow(v.x-xIntersect,2)+pow(v.z-zIntersect,2));
                float extra_distance;
                float distance_my_vehicle=sqrt(pow(my_vehicle.x-xIntersect,2)+pow(my_vehicle.z-zIntersect,2));
                float time;
                if (distance_v > RADIUS*2)
                    continue;
                else
                {
                    time=distance_my_vehicle/my_vehicle.speed;
                    time2Collide_my_vehicle_max=time+time4radius_my_vehicle;
                    time2Collide_my_vehicle_min=time-time4radius_my_vehicle;
                    extra_distance=v.speed*time;
                    if (distance_v+extra_distance>RADIUS*2)
                        continue;
                    else
                        time2Collide=time2Collide_my_vehicle_min;
                }
            }
            else if (m_in_range == false && m1_in_range == true)
            {
                float time4radius_v=RADIUS/v.speed;
                float distance_v=sqrt(pow(v.x-xIntersect,2)+pow(v.z-zIntersect,2));
                float extra_distance;
                float distance_my_vehicle=sqrt(pow(my_vehicle.x-xIntersect,2)+pow(my_vehicle.z-zIntersect,2));
                float time;
                if (distance_my_vehicle > RADIUS*2)
                    continue;
                else
                {
                    time=distance_v/v.speed;
                    time2Collide_v_max=time+time4radius_v;
                    time2Collide_v_min=time-time4radius_v;
                    extra_distance=my_vehicle.speed*time;
                    if (distance_my_vehicle+extra_distance>RADIUS*2)
                        continue;
                    else
                        time2Collide=time2Collide_v_min;
                }
            }
           }
            }
        }
    }
}