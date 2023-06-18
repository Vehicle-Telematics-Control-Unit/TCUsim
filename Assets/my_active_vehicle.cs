using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class my_active_vehicle : MonoBehaviour
{
    // Start is called before the first frame update

    public float updateRateSeconds = 10;

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

        Base tower = Base.tower;
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
            // sscanf(buffer, "l:%f,%f&&h:%f&&s:%d&&b:%d", lat, lon, heading, speed, brakes);
            lat = Mathf.Round(lat * 100000) / 100000;
            lon = Mathf.Round(lon * 100000) / 100000;
            angle = Mathf.Round(angle * 100000) / 100000;
            string message = "l:"+ lat.ToString() + "," + lon.ToString() + "&&h:" + angle.ToString() + "&&s:50&&b:0"; // lat.ToString() + "," + lon.ToString() + "," + angle.ToString();
            Debug.Log(message);
            byte[] data = Encoding.ASCII.GetBytes(message);
            socket.Send(data);
        }
        Invoke("broadcast", updateRateSeconds);
    }
}
