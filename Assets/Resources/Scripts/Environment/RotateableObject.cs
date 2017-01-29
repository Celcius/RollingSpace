using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateableObject : MonoBehaviour {

    [SerializeField]
    float rotateSpeed = 100.0f;

    [SerializeField]
    Transform _reference;

	public void rotate(float xAxis, float yAxis)
    {
        if (_reference == null)
            return;
        
        Vector3 up = (_reference.position - transform.position).normalized;
        Vector3 left = Vector3.Cross(up, Vector3.forward).normalized;

        transform.Rotate(gameObject.transform.InverseTransformDirection(left), yAxis * rotateSpeed);
        transform.Rotate(gameObject.transform.InverseTransformDirection(-Vector3.forward), xAxis * rotateSpeed);
    }
}
