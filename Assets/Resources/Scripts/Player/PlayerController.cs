using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    RotateableObject _rotationObject;
    Rigidbody2D _body;

    [SerializeField]
    float _moveSpeed = 15.0f;
    [SerializeField]
    float _jumpForce = 1.0f;

    // Use this for initialization
    void Start () {
        _body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        handleMovement();

        handleRotation();

		
	}

    void handleMovement()
    {
        Debug.Log(_rotationObject);
        if (_rotationObject == null || Input.GetKey(KeyCode.LeftShift))
            return;


        if (Input.GetKey(KeyCode.LeftArrow))
        {
             transform.RotateAround(_rotationObject.transform.position, Vector3.forward, _moveSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(_rotationObject.transform.position, Vector3.forward, -_moveSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("JUMp");
            _body.AddForce((transform.position - _rotationObject.transform.position) * _jumpForce,ForceMode2D.Impulse);
        }

    }

    void handleRotation()
    {
        if (_rotationObject == null || !Input.GetKey(KeyCode.LeftShift))
            return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rotationObject.rotate(RotationDir.LEFT);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _rotationObject.rotate(RotationDir.RIGHT);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            _rotationObject.rotate(RotationDir.UP);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _rotationObject.rotate(RotationDir.DOWN);
        }
        else
            _rotationObject.rotate(RotationDir.NONE);
    }

    public void setRotateableObject(RotateableObject obj)
    {
        _rotationObject = obj;
    }
}
