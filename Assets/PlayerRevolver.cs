using UnityEngine;
using System.Collections;

public class PlayerRevolver : MonoBehaviour {

	// Update is called once per frame
    public void revolve(float moveX, float moveY,Vector3 playerVec) 
    {
                //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0.0f, acumSpeedY, acumSpeedX));
            /*
                Quaternion deltaRotation = Quaternion.AngleAxis(acumSpeedX, transform.up) * Quaternion.AngleAxis(acumSpeedY, Vector3.right);
                _revolver.transform.rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);*/
            
            
           if(moveX != 0) 
               transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), moveX*5,Space.World);
            if (moveY != 0)
                transform.Rotate(Vector3.Cross(playerVec,new Vector3(0.0f, 0.0f, 1.0f)), moveY * 5, Space.World);
        /*     transform.rigidbody.centerOfMass = new Vector3(0, 0, 0);
        new Vector3(-1.0f, 0.0f, 0.0f)
             Vector3 deltavel = new Vector3(0.0f, moveY, moveX);
             transform.rigidbody.AddTorque(Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(deltavel), Time.deltaTime*100).eulerAngles, ForceMode.VelocityChange);*/
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
