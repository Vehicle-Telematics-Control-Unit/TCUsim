// This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
// To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/ 
// or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System.IO;
using Cinemachine;

public class Sock : MonoBehaviour
{
    // public Vehicle myCar;
    public GameObject new_car_prefab;

    public CinemachineTargetGroup camera_targets;

    public static Socket socket;

    Vehicle my_vechicle;
    public Base tower;

    private Thread thread;

    bool locked = false;

    Dictionary<string, Vehicle> mac_vehicle_mapper = new Dictionary<string, Vehicle>();

    void sock_main()
    {
        Debug.Log("Sock inside");
        while (true)
        {
            try
            {
                // Create a new socket instance
                socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);

                // Define the IPC socket path
                string socketPath = "sock";
                Debug.Log("IPC socket file specified");

                // check file access possibility :)
                // FileInfo file = new System.IO.FileInfo(socketPath);
                // long fileSize = file.Length;
                // Debug.Log("filesize : " + fileSize);

                // Connect to the IPC socket
                socket.Connect(new UnixDomainSocketEndPoint(socketPath));
                Debug.Log("IPC socket connected");

                // Send data to the server
                // string message = "Hello from C# client";
                // byte[] data = Encoding.ASCII.GetBytes(message);
                // socket.Send(data);

                while (true)
                {
                    // Receive a response from the server
                    byte[] buffer = new byte[1024];
                    int bytesRead = socket.Receive(buffer);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Debug.Log("Received message from server: " + response);
                    level1_decoder(response);
                }

                // Close the socket
                socket.Close();
            }
            catch (Exception e)
            {
                Debug.Log("Exception: " + e.Message);
            }
            Thread.Sleep(1000);
        }
    }

    void Start()
    {

        // my_vechicle = myCar.GetComponent<Vehicle>();

        Debug.Log("Sock about to start");
        thread = new Thread(sock_main);
        Debug.Log("Sock thread create");
        thread.Start();
        Debug.Log("Sock thread started");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.playmodeStateChanged = delegate ()
        {
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode &&
                UnityEditor.EditorApplication.isPlaying)
            {
                if (thread != null)
                {
                    thread.Abort();
                }
                Debug.Log(string.Format("[{0}] Exiting playmode.", GetType().Name));
            }
#endif
        };

    }

    void Update()
    {
        foreach (KeyValuePair<string, Vehicle> entry in mac_vehicle_mapper)
        {
            // do something with entry.Value or entry.Key
            Debug.Log("entry #> " + entry.Key + " : " + entry.Value);
            if (entry.Value is null)
            {
                GameObject go = Instantiate(new_car_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                mac_vehicle_mapper[entry.Key] = go.GetComponent<Vehicle>();
                camera_targets.AddMember(go.transform, 1, 3.96f);
                locked = false;
                break;
            }
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("sock thread aborted on application quit!");
        thread.Abort();
    }

    void quit()
    {

    }

    void level1_decoder(string buffer)
    {
        Debug.Log("level1_decoder started: (" + buffer + ")");
        if (buffer.Length == 0)
        {
            return;
        }
        string macAddr = buffer.Substring(0, 12);
        // Debug.Log(macAddr);
        string msg = buffer.Substring(12);
        // Debug.Log(msg);
        Debug.Log("level1_decoder of " + buffer + " about to exit;");
        level2_decoder(macAddr, msg);
    }

    void level2_decoder(string macAddr, string l1payload)
    {
        Debug.Log("level2_decoder started: (" + macAddr + "," + l1payload + ")");
        char type = l1payload[0];
        string payload = l1payload.Substring(1);
        switch (type)
        {
            case 'l':
                location_decoder(macAddr, payload);
                break;

            case 'h':
                heading_decoder(macAddr, payload);
                break;

            default:
                break;
        }
    }

    void location_decoder(string macAddr, string payload)
    {
        Debug.Log("location_decoder started: (" + macAddr + "," + payload + ")");
        // if (macAddr == "000000000000")
        // {
        //     // my car
        //     // Debug.Log("float parser test:" + float.Parse(payload.Split(',')[0]));
        //     my_vechicle.lat = float.Parse(payload.Split(',')[0]);
        //     my_vechicle.lon = float.Parse(payload.Split(',')[1]);
        // }
        if (macAddr == "basebasebase")
        {
            tower.base_station_lat = float.Parse(payload.Split(',')[0]);
            tower.base_station_lon = float.Parse(payload.Split(',')[1]);
        }
        else
        {
            // not my car
            does_vehicle_exist(macAddr);

            while (mac_vehicle_mapper.ContainsKey(macAddr) == false || locked == true) ;
            // mac_vehicle_mapper[macAddr].GetComponent<Vehicle>().update_location(float.Parse(payload.Split(',')[0]), float.Parse(payload.Split(',')[1]));
            while (mac_vehicle_mapper[macAddr] == null) ;

            mac_vehicle_mapper[macAddr].lat = float.Parse(payload.Split(',')[0]);
            mac_vehicle_mapper[macAddr].lon = float.Parse(payload.Split(',')[1]);
        }
    }

    void heading_decoder(string macAddr, string payload)
    {
        Debug.Log("heading_decoder started: (" + macAddr + "," + payload + ")");
        // if (macAddr == "000000000000")
        // {
        //     // my car
        //     my_vechicle.heading = float.Parse(payload);
        // }
        if (macAddr == "basebasebase")
        {
            tower.base_station_heading = float.Parse(payload);
        }
        else
        {
            // not my car
            does_vehicle_exist(macAddr);

            while (mac_vehicle_mapper.ContainsKey(macAddr) == false || locked == true) ;
            while (mac_vehicle_mapper[macAddr] == null) ;

            mac_vehicle_mapper[macAddr].heading = float.Parse(payload);
        }
    }

    void does_vehicle_exist(string mac)
    {
        // if (mac_vehchile_mapper.ContainsKey(mac) == true)
        // {
        //     return;
        // }

        if (mac_vehicle_mapper.ContainsKey(mac) == true)
        {
            return;
        }

        locked = true;
        mac_vehicle_mapper.Add(mac, null);

        // Debug.Log("about to instantiate!");
        // Invoke("test", 0);
        // mac_vehchile_mapper[mac] = 
        // Instantiate(new_car_prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

}