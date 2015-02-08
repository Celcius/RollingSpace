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
        this.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -20.5f); //Vector3.Lerp(transform.position, new Vector3(_player.transform.position.x, _player.transform.position.y, -2.5f),Time.deltaTime);
	}
}
