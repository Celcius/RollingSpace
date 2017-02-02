using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTargetPlayer : GravityTarget {

    PlayerController _controller;
	// Use this for initialization
	new public void Start () {
        base.Start();
        _controller = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	public void Update () {
        base.Update();

        transform.up = transform.position - _mainGravityObject.transform.position;
    }

    public override void setRotateableObject(RotateableObject obj)
    {
        if(obj  != null)
            _controller.setRotateableObject(obj);
    }
}
