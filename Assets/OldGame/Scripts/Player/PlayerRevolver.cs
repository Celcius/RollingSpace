using UnityEngine;
using System.Collections;

public class PlayerRevolver : MonoBehaviour {

    private const float SCALE_TILE_RATIO = 0.1f;
    private float rotationOffsetDegrees;
    private const float TILES_PER_SECOND = 0.3f;
    private float checkTimeX = 0.0f;
    private float checkTimeY = 0.0f;
   
    [SerializeField]
    private Moon[] moons;

    int currentX = 0;
    int currentY = 0;
    int xDir = 1;
    int yDir = 1;
    float baseXRotation = 0.0f;
    float baseYRotation = 0.0f;
    Vector3 _playerVec;
    void Start()
    {
        baseXRotation = transform.rotation.eulerAngles.z;
        baseYRotation = -transform.rotation.eulerAngles.x;
        rotationOffsetDegrees = 360.0f/(transform.localScale.x / SCALE_TILE_RATIO);
    }

	// Update is called once per frame
    public void revolve(float moveX, float moveY,Vector3 playerVec) 
    {
        _playerVec = playerVec;

                //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0.0f, acumSpeedY, acumSpeedX));
            /*
                Quaternion deltaRotation = Quaternion.AngleAxis(acumSpeedX, transform.up) * Quaternion.AngleAxis(acumSpeedY, Vector3.right);
                _revolver.transform.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);*/

        if(moveX != 0 && checkTimeX <=0)
        {
            checkTimeX = TILES_PER_SECOND;
            xDir = (int)Mathf.Sign(moveX);
            currentX += xDir;
           
        }

        if (moveY != 0 && checkTimeY <=0)
        {
            checkTimeY = TILES_PER_SECOND;
            yDir = (int)Mathf.Sign(moveY);
            currentY += yDir;
           //transform.Rotate(Vector3.Cross(playerVec, new Vector3(0.0f, 0.0f, 1.0f)), rotationOffsetDegrees * Mathf.Sign(moveY), Space.World);
        }
        /*     transform.rigidbody.centerOfMass = new Vector3(0, 0, 0);
        new Vector3(-1.0f, 0.0f, 0.0f)
             Vector3 deltavel = new Vector3(0.0f, moveY, moveX);
             transform.rigidbody.AddTorque(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(deltavel), Time.deltaTime*100).eulerAngles, ForceMode.VelocityChange);*/
	}

    void Update()
    {
        
        if(checkTimeX > 0)
        {
            checkTimeX -= Time.deltaTime;
            float timer = Time.deltaTime;


            if(checkTimeX<=0)
            {
                timer -= -checkTimeX;
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,baseXRotation + rotationOffsetDegrees * currentX);
                checkTimeX = 0;
            }
            float angle = Mathf.Lerp(0, xDir * rotationOffsetDegrees / TILES_PER_SECOND, timer);
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), angle, Space.World);
        
            foreach(Moon moon in moons)
            {
                moon.rotateMoonAround(transform, angle);
            }

        }
        
        if (checkTimeY > 0)
        {
            checkTimeY -= Time.deltaTime;
            float timer = Time.deltaTime;
            if (checkTimeY <= 0)
            {
                timer -= -checkTimeY;
                // transform.eulerAngles = new Vector3(baseYRotation + rotationOffsetDegrees * currentY, transform.eulerAngles.y, transform.eulerAngles.x);
                checkTimeY = 0;
            }
            float angle = Mathf.Lerp(0, yDir * rotationOffsetDegrees / TILES_PER_SECOND, timer);
            transform.Rotate(Vector3.Cross(_playerVec, new Vector3(0.0f, 0.0f, 1.0f)), angle, Space.World);
      
        }
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.collider.tag == "Player")
        {
            OrbitalPlayerController player = other.collider.GetComponent<OrbitalPlayerController>();
            if (player != null)
                player.setRevolver(this);
      
        }

    }

}
