using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class CollisionPrediction : MonoBehaviour
{

    public my_active_vehicle my_vehicle;
    public List<Vehicle> surrounding_vehicles = new List<Vehicle>();
    // private bool collide;

    public float RADIUS = 1.7f;

    private float time2Collide = float.PositiveInfinity;
    private float timeThreshold = 3;
    // private bool collide = false;

    public string ipAddress = "127.0.0.1";
    public int port = 8000;
    private TcpClient client;
    private NetworkStream stream;

    GameObject Capsule;

    // Start is called before the first frame update
    void Start()
    {
        my_vehicle = my_vehicle.GetComponent<my_active_vehicle>();
        client = new TcpClient();
        client.Connect(ipAddress, port);
        stream = client.GetStream();
        Capsule = GameObject.Find("Capsule");
        // Start sending data at regular intervals

    }

    // Update is called once per frame
    void Update()
    {

        foreach (Vehicle V in surrounding_vehicles)
        {
            
            if(Vector3.Distance(V.transform.position, my_vehicle.transform.position) >= 25f)
                continue;
            string positionX = (Mathf.Round((my_vehicle.transform.position.x - V.transform.position.x) * 100000) / 100000).ToString();
            string positionZ = (Mathf.Round((my_vehicle.transform.position.z - V.transform.position.z) * 100000) / 100000).ToString();

            string angleM = (Mathf.Round((360f - my_vehicle.GetComponent<my_active_vehicle>().angle + 90f) * Mathf.Deg2Rad * 10) / 10).ToString();
            string angleV = (Mathf.Round((360f - V.GetComponent<Vehicle>().angle + 90f) * Mathf.Deg2Rad * 100000) / 100000).ToString();
            string speedM = (Mathf.Round(my_vehicle.GetComponent<my_active_vehicle>().speed * 100000)/100000).ToString();
            string speedV = (Mathf.Round(V.GetComponent<Vehicle>().speed * 100000)/100000).ToString();

            string textToSend = positionX + "," + positionZ + "," + angleM + "," + angleV + "," + speedM + "," + speedV;

            SendDataToPython(textToSend);

        }



    }


    private void SendDataToPython(string dataToSend)
    {
        // Send text to the server
        byte[] sendData = Encoding.ASCII.GetBytes(dataToSend);
        stream.Write(sendData, 0, sendData.Length);

        // Receive response from the server
        byte[] receiveData = new byte[1024];
        int bytesRead = stream.Read(receiveData, 0, receiveData.Length);
        string receivedText = Encoding.ASCII.GetString(receiveData, 0, bytesRead);
        if(receivedText == "0"){
            Capsule.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
        }
        else{
            Capsule.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
        }


        Debug.Log("Data from Python: " + receivedText);
    }

    private void OnDestroy()
    {
        stream.Close();
        client.Close();
    }




}
