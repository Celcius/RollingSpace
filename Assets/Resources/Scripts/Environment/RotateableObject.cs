using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateableObject : MonoBehaviour {

    [SerializeField]
    Transform _reference;

    float rotateSegmentSeconds = 0.3f;
    float segmentAngle = 15.0f;

    bool rotating = false;
    Quaternion rotationTarget;


    public void rotate(float xAxis, float yAxis)
    {
        if (_reference == null)
            return;

        if (!rotating)
        {
            if (xAxis != 0)
            {
                float targetAngle = xAxis * segmentAngle;
                rotating = true;
                
                Vector3 xRotation = gameObject.transform.InverseTransformDirection(-Vector3.forward);
                rotationTarget = transform.rotation * Quaternion.Euler(xRotation * targetAngle);
            }
            else if (yAxis != 0)
            {
                float targetAngle = yAxis * segmentAngle;
                rotating = true;

                Vector3 up = (_reference.position - transform.position).normalized;
                Vector3 yRotation = gameObject.transform.InverseTransformDirection(Vector3.Cross(up, Vector3.forward));
                rotationTarget = transform.rotation * Quaternion.Euler(yRotation * targetAngle);
            }
        }

        if(rotating)
        {
            float step = (segmentAngle / rotateSegmentSeconds) * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, step);

            if (transform.rotation == rotationTarget)
            {
                transform.rotation = rotationTarget;
                rotating = false;
            }
        }
    }
}
