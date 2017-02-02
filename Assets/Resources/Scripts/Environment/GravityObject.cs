using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityObject : MonoBehaviour {

    [SerializeField]
    float gravityForce = 100.0f;

    RotateableObject _rotateComponent;
    // Use this for initialization
    void Start () {
        _rotateComponent = GetComponent<RotateableObject>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (_rotateComponent == null)
            return;

        GravityTarget target = col.collider.GetComponent<GravityTarget>();
        if(target != null)
        {
            target.setRotateableObject(_rotateComponent);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (_rotateComponent == null)
            return;

        Debug.Log("Leave");

        GravityTarget target = col.collider.GetComponent<GravityTarget>();
        if (target != null)
        {
            target.setRotateableObject(null);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GravityTarget target = collider.GetComponent<GravityTarget>();
        if (target != null)
        {
            target.addGravityObject(this);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GravityTarget target = collider.GetComponent<GravityTarget>();
        if (target != null)
        {
            target.removeGravityObject(this);
        }
    }

    public Vector2 getGravity(GravityTarget target)
    {
        return (transform.position - target.transform.position) * gravityForce;
    }
}
