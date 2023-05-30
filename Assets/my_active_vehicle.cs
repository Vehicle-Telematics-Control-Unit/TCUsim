using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class my_active_vehicle : MonoBehaviour
{
    // Start is called before the first frame update

    public float updateRateSeconds = 1;

    public float lon = 0, lat = 0, angle = 0;


    void Start()
    {
        Invoke("broadcast", updateRateSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3((lon - tower.base_station_lon) * tower.scale,
        //                          transform.position.y,
        //                          (lat - tower.base_station_lat) * tower.scale);

        Base tower = Vehicle.tower;
        lat = this.transform.position.x / tower.scale + tower.base_station_lat;
        lon = this.transform.position.z / tower.scale + tower.base_station_lon;
        angle = transform.rotation.eulerAngles.y; 

    }

    void broadcast()
    {
        var socket = Sock.socket;
        Debug.Log(socket);
        if (socket != null)
        {
            string message = lat.ToString() + "," + lon.ToString() + "," + angle.ToString();
            byte[] data = Encoding.ASCII.GetBytes(message);
            socket.Send(data);
        }
        Invoke("broadcast", updateRateSeconds);
    }
}
