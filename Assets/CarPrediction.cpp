#include <CarPrediction.hpp>
#include <math.h>

bool CarPrediction::isWarn()
{
    time2Collide = INFINITY;
    collide=false;
    for (Vehicle v : surrounding_vehicles)
    {
        if ((my_vehicle.angle == 90 || my_vehicle.angle == 270) && (v.angle == 90 || v.angle == 270))
        {
            if (abs(my_vehicle.z - v.z) > RADIUS * 2 
            || ((my_vehicle.angle == 270 && v.angle == 90 && v.x > my_vehicle.x) 
            || (my_vehicle.angle == 90 && v.angle == 270 && v.x < my_vehicle.x) 
            || (my_vehicle.speed == 0 && v.speed == 0)
            || (my_vehicle.angle == v.angle && my_vehicle.speed == v.speed) 
            || (my_vehicle.angle == 90 && v.angle == 90 && my_vehicle.speed > v.speed && my_vehicle.x > v.x)
            || (my_vehicle.angle == 90 && v.angle == 90 && my_vehicle.speed < v.speed && my_vehicle.x < v.x)
            || (my_vehicle.angle == 270 && v.angle == 270 && my_vehicle.speed > v.speed && my_vehicle.x < v.x)
            || (my_vehicle.angle == 270 && v.angle == 270 && my_vehicle.speed < v.speed && my_vehicle.x > v.x)
            && abs(my_vehicle.x - v.x) > RADIUS * 2))
                continue;
            else
            {
                if (abs(my_vehicle.x - v.x) <= RADIUS * 2)
                {
                    time2Collide = 0;
                    break;
                }
                else
                {
                    if (my_vehicle.angle == v.angle)
                        time2Collide=abs(my_vehicle.x-v.x-RADIUS*2)/abs(my_vehicle.speed-v.speed);
                    else
                        time2Collide=abs(my_vehicle.x-v.x-RADIUS*2)/(my_vehicle.speed+v.speed);
                }
            }
        }
        else
        {
           if (tan(my_vehicle.angle) == tan(v.angle))
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
            else
            {
                if (abs(my_vehicle.x - v.x) <= RADIUS * 2 && abs(my_vehicle.z - v.z) <= RADIUS * 2)
                {
                    time2Collide = 0;
                    break;
                }
                else
                {
                    if (my_vehicle.angle == v.angle)
                        time2Collide=abs(sqrt(pow(my_vehicle.x-v.x,2)+pow(my_vehicle.z-v.z,2))-RADIUS*2)/abs(my_vehicle.speed-v.speed);
                    else
                        time2Collide=abs(sqrt(pow(my_vehicle.x-v.x,2)+pow(my_vehicle.z-v.z,2))-RADIUS*2)/(my_vehicle.speed+v.speed);
                }
            }
           } 
           else
           {
            if (my_vehicle.speed == 0 && v.speed == 0 && !(abs(my_vehicle.x - v.x) <= RADIUS * 2 && abs(my_vehicle.z - v.z) <= RADIUS * 2))
                continue;
            else if (my_vehicle.speed == 0)
            {
                float m=tan(v.angle);
                float c=v.x-m*v.z;
                float d=abs((m*my_vehicle.z-my_vehicle.x+c)/sqrt(m*m+1));
                if (d > RADIUS*2)
                    continue;
                if (abs(my_vehicle.x - v.x) <= RADIUS * 2 && abs(my_vehicle.z - v.z) <= RADIUS * 2)
                {
                    time2Collide = 0;
                    break;
                } 
                float mPerp=1/m*-1;
                float cPerp=v.x-mPerp*v.z;
                if (mPerp == 0 && (((v.angle >= 0 && v.angle <90) || (v.angle > 270 && v.angle <= 359) && v.z < 0) || ((v.angle > 90 && v.angle <= 180) || (v.angle >= 180 && v.angle < 270) && v.z > 0)))
                    continue;
                else if (mPerp > 0 && ((my_vehicle.x*-1+mPerp*my_vehicle.z+cPerp > 0 && v.angle > 180 && v.angle < 360) || (my_vehicle.x*-1+mPerp*my_vehicle.z+cPerp < 0 && v.angle > 0 && v.angle < 180)))
                    continue;
                else if (mPerp < 0 && ((my_vehicle.x*-1+mPerp*my_vehicle.z+cPerp < 0 && v.angle > 180 && v.angle < 360) || (my_vehicle.x*-1+mPerp*my_vehicle.z+cPerp > 0 && v.angle > 0 && v.angle < 180)))
                    continue;
                else
                {
                    time2Collide=(sqrt(pow(my_vehicle.x-v.x,2)+pow(my_vehicle.z-v.z,2))-RADIUS*2)/v.speed;
                }
            }
            else if (v.speed == 0)
            {
                float m=tan(my_vehicle.angle);
                float c=my_vehicle.x-m*my_vehicle.z;
                float d=abs((m*v.z-v.x+c)/sqrt(m*m+1));
                if (d > RADIUS*2)
                    continue;
                if (abs(my_vehicle.x - v.x) <= RADIUS * 2 && abs(my_vehicle.z - v.z) <= RADIUS * 2)
                {
                    time2Collide = 0;
                    break;
                } 
                float mPerp=1/m*-1;
                float cPerp=my_vehicle.x-mPerp*my_vehicle.z;
                if (mPerp == 0 && (((my_vehicle.angle >= 0 && my_vehicle.angle <90) || (my_vehicle.angle > 270 && my_vehicle.angle <= 359) && my_vehicle.z < 0) || ((my_vehicle.angle > 90 && my_vehicle.angle <= 180) || (my_vehicle.angle >= 180 && my_vehicle.angle < 270) && my_vehicle.z > 0)))
                    continue;
                else if (mPerp > 0 && ((v.x*-1+mPerp*v.z+cPerp > 0 && my_vehicle.angle > 180 && my_vehicle.angle < 360) || (v.x*-1+mPerp*v.z+cPerp < 0 && my_vehicle.angle > 0 && my_vehicle.angle < 180)))
                    continue;
                else if (mPerp < 0 && ((v.x*-1+mPerp*v.z+cPerp < 0 && my_vehicle.angle > 180 && my_vehicle.angle < 360) || (v.x*-1+mPerp*v.z+cPerp > 0 && my_vehicle.angle > 0 && my_vehicle.angle < 180)))
                    continue;
                else
                {
                    time2Collide=(sqrt(pow(my_vehicle.x-v.x,2)+pow(my_vehicle.z-v.z,2))-RADIUS*2)/my_vehicle.speed;
                }
            }
            else
            {
            float m=tan(my_vehicle.angle);
            float c=my_vehicle.x-m*my_vehicle.z;
            float m1=tan(v.angle);
            float c1=v.x-m1*v.z;
            float xIntersect=(c1-c)/(m-m1);
            float yIntersect=m*xIntersect+c;
            if ()
            }
           }
        }
    }
}