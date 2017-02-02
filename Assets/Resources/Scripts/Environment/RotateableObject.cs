using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDir { LEFT, RIGHT, UP, DOWN, NONE };

public class RotateableObject : MonoBehaviour {

    [SerializeField]
    Transform _reference;

    float rotateSegmentSeconds = 0.3f;
    float segmentAngle = 15.0f;

    bool rotating = false;
    Quaternion rotationTarget;


    public void rotate(RotationDir dir)
    {
        Vector2 axis = parseDir(dir);
        if (_reference == null)
            return;

        if (!rotating)
        {
            if (axis.x != 0)
            {
                float targetAngle = axis.x * segmentAngle;
                rotating = true;
                
                Vector3 xRotation = gameObject.transform.InverseTransformDirection(-Vector3.forward);
                rotationTarget = transform.rotation * Quaternion.Euler(xRotation * targetAngle);
            }
            else if (axis.y != 0)
            {
                float targetAngle = axis.y * segmentAngle;
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

    private Vector2 parseDir(RotationDir dir)
    {
        switch(dir)
        {
            case RotationDir.LEFT:
                return new Vector2(-1, 0);
            case RotationDir.RIGHT:
                return new Vector2(1, 0);
            case RotationDir.UP:
                return new Vector2(0, 1);
            case RotationDir.DOWN:
                return new Vector2(0, -1);
            case RotationDir.NONE:
                return new Vector2(0, 0);
        }

        return new Vector2(0, 0);
    }
}
