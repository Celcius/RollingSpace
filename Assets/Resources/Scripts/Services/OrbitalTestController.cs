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
        if (Input.GetKey(KeyCode.A))
        {
            _obj.rotate(RotationDir.LEFT);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _obj.rotate(RotationDir.RIGHT);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _obj.rotate(RotationDir.UP);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _obj.rotate(RotationDir.DOWN);
        }
        else
            _obj.rotate(RotationDir.NONE);
    }
}
