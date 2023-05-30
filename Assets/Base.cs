using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Base : MonoBehaviour
{
    // Start is called before the first frame update
    public static Base tower;
    public float base_station_lat = 0;
    public float base_station_lon = 0;
    public float base_station_heading = 0;
    public float scale = 1;

    void Awake()
    {
        tower = this;
    }

    void Start(){

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
