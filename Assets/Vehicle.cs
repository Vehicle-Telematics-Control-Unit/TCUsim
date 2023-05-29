using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public static Base tower = null;
    public float lat = 0;
    public float lon = 0;

    void update_location(Transform Base){
        // transform.position.x -= tower.base_station_lon - lon;
        transform.position = new Vector3((lon - tower.base_station_lon) * tower.scale,
                                         transform.position.y,
                                         (lat - tower.base_station_lat) * tower.scale);
    }

    public void update_location(float lat, float lon){
        
    }



    // Start is called before the first frame update
    void Start()
    {
        if(tower == null)
            tower = FindObjectOfType<Base>().gameObject.GetComponent<Base>();
    }

    // Update is called once per frame
    void Update()
    {
        update_location(tower.transform);
    }
}
