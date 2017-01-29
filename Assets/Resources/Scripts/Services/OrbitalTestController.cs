using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalTestController : MonoBehaviour {

    RotateableObject _obj;
	// Use this for initialization
	void Start () {
        _obj = GetComponent<RotateableObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A))
        {
            _obj.rotate(-1*Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _obj.rotate(1 * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _obj.rotate(0, 1 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _obj.rotate(0, -1 * Time.deltaTime);
        }
    }
}
