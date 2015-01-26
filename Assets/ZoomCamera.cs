using UnityEngine;
using System.Collections;

enum ZoomState { NONE, ZOOM_IN, ZOOM_OUT};

public class ZoomCamera : MonoBehaviour {

    private Camera _cam;
    private const float MIN_ZOOM = 0.5f;
    private const float MAX_ZOOM = 4.0f;

    private const float ZOOM_INCREMENT = 1;

    private const float FULL_ZOOM_TIME = 3.0f;
    
    private ZoomState zoomState = ZoomState.NONE;

    
	// Use this for initialization
	void Start () {
        _cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        bool zoomOut = Input.GetKey(KeyCode.E);
        bool zoomIn = Input.GetKey(KeyCode.Q);

        // Zoom in
        if(zoomIn && zoomState != ZoomState.ZOOM_OUT)
        {
            zoomState = ZoomState.ZOOM_IN;
            float size = _cam.orthographicSize;
            size -= size*Mathf.Exp(ZOOM_INCREMENT)*Time.deltaTime;
            if (size <= MIN_ZOOM)
                size = MIN_ZOOM;
            _cam.orthographicSize = size;
        }



        // Zoom out
        if (zoomOut && zoomState != ZoomState.ZOOM_IN)
        {
            zoomState = ZoomState.ZOOM_OUT;
            float size = _cam.orthographicSize;
            size += size * Mathf.Exp(ZOOM_INCREMENT) * Time.deltaTime;
            if (size >= MAX_ZOOM)
                size = MAX_ZOOM;
            _cam.orthographicSize = size;
        }

        if (!zoomOut && !zoomIn)
            zoomState = ZoomState.NONE;
	 
	}
}
