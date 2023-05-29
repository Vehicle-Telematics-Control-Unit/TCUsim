using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_script : MonoBehaviour
{
   	public GameObject Target = null;
	public GameObject T = null;
	public float speed = 1.5f;

    void Start()
    {
		// Target = GameObject.FindGameObjectWithTag("Player");
		// T = GameObject.FindGameObjectWithTag("Target");
    }

  
    void FixedUpdate()
    {
		this.transform.LookAt(Target.transform);
		float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, T.transform.position) * speed);
		this.transform.position = Vector3.MoveTowards(this.transform.position, T.transform.position, car_Move * Time.deltaTime);
    }
}