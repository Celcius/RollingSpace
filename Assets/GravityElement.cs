using UnityEngine;
using System.Collections;

public class GravityElement : MonoBehaviour {
    [SerializeField]
    public float _mass = 1.0f;

    private PlayerRevolver _revolverComponent;

    static float gravityConstant = 10.0f;

    bool isPlayerInside = false;
    void Start()
    {
        _revolverComponent = GetComponentInParent<PlayerRevolver>();
    }

    void OnTriggerStay (Collider other)
    {

        if(other.tag == "Player")
       {

           
           OrbitalPlayerController player = other.GetComponent<OrbitalPlayerController>();
           
           if (player.airborn)
                applyGravityForce(other);
/*            if(_revolverComponent != null)
                player.setRevolver(_revolverComponent, force);*/
            
            if(!isPlayerInside)
            {
                isPlayerInside = true;
                player.gravityElementCount++;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            if (isPlayerInside)
            {
                OrbitalPlayerController player = other.GetComponent<OrbitalPlayerController>();
                isPlayerInside = false;
                player.gravityElementCount--;
            }
        }
    }


    public void applyGravityForce(Collider other)
    {
       
        Vector3 vec = (transform.position - other.transform.position);
        float distance_squared = vec.sqrMagnitude;
        float force = (_mass / distance_squared) * gravityConstant;

        other.transform.rigidbody.AddForce(force * vec);
    }

    public void applyInvertedGravityForce(Collider other)
    {
        print("INVERTED");
        Vector3 vec = (transform.position - other.transform.position);
        float distance_squared = vec.sqrMagnitude;
        float force = (_mass / distance_squared) * gravityConstant;

        other.transform.rigidbody.AddForce(1 * vec);
    }

}
