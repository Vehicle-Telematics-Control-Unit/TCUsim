using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public static Base tower;
    public float x = 0;
    public float z = 0;

    public float angle = 0;
    public float speed = 0;

    void update_location(){
        // transform.position.x -= tower.base_station_lon - lon;
        transform.position = new Vector3((z - tower.base_station_lon) * tower.scale,
                                         transform.position.y,
                                         (x - tower.base_station_lat) * tower.scale);

    }

    public void update_heading(){
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f); 

    }

    void Awake() {   
        tower = Base.tower;
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
        update_location();
        update_heading();
    }
}
