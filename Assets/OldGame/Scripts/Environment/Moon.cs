using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {

    [SerializeField]
    bool NormalDir;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void rotateMoonAround(Transform planet, float angle)
    {
        angle = NormalDir ? angle : -angle;

        float curX = transform.position.x;
        float curY = transform.position.y;
        curX -= planet.position.x;
        curY -= planet.position.y;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * curX - Mathf.Sin(angle * Mathf.Deg2Rad) * curY;
        float y = Mathf.Cos(angle * Mathf.Deg2Rad) * curY + Mathf.Sin(angle * Mathf.Deg2Rad) * curX;
        x += planet.position.x;
        y += planet.position.y;
        transform.position = new Vector3(x, y, transform.position.z);


    }
}
