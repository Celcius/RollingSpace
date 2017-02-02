using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTarget : MonoBehaviour {

    protected GravityObject _mainGravityObject;
    bool _isCollidingMain = false;

    ArrayList _gravityObjects;
    Rigidbody2D _body;

	// Use this for initialization
	public void Start () {
        _gravityObjects = new ArrayList();
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update () {
        if(!_isCollidingMain)
        {
            _body.AddForce(_mainGravityObject.getGravity(this)*Time.deltaTime);
        }
        
    }

    public void addGravityObject(GravityObject obj)
    {
        _mainGravityObject = obj;
        _body.AddForce(_mainGravityObject.getGravity(this));
    }

    public void enterCollisionWithGravityObject(GravityObject obj)
    {
        if (obj == _mainGravityObject)
            _isCollidingMain = true;
    }


    public void exitCollisionWithGravityObject(GravityObject obj)
    {
        if (obj == _mainGravityObject)
            _isCollidingMain = false;
    }

    public void removeGravityObject(GravityObject obj)
    {
        /*
        if(_gravityObjects.Count > 2)
        {
            _gravityObjects.Remove(obj);
        }*/
            
    }

    public virtual void setRotateableObject(RotateableObject obj)
    {
      
    }

}
