using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

    [SerializeField]
    private OrbitalPlayerController _player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
       // this.transform.up = _player.transform.up;
        float dist = Vector2.Distance(transform.position, _player.transform.position);
        if (dist < 1.0f)
            dist = 1.0f;

        float x = Mathf.Lerp(transform.position.x, _player.transform.position.x, Time.deltaTime * dist);
        float y = Mathf.Lerp(transform.position.y, _player.transform.position.y, Time.deltaTime * dist);
        this.transform.position =  new Vector3(x,y, -20.5f); //Vector3.Lerp(transform.position, new Vector3(_player.transform.position.x, _player.transform.position.y, -2.5f),Time.deltaTime);
	}
}
