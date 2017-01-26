using UnityEngine;
using System.Collections;

public class GravityElement : MonoBehaviour {
    [SerializeField]
    public float _mass = 1.0f;

    private PlayerRevolver _revolverComponent;

    static float gravityConstant = 10.0f;
    float accumTime = 0.0f;

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

           if (player.ungrounded && player.isRevolver(_revolverComponent))
           {
               accumTime = player.airTime;
               applyGravityForce(other);
           }
           else
               accumTime = 0.0f;

/*            if(_revolverComponent != null)
                player.setRevolver(_revolverComponent, force);*/
            
            if(!isPlayerInside)
            {
                isPlayerInside = true;
                player.gravityElementCount++;
                player.registerRevolver(_revolverComponent);
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
                player.unregisterRevolver(_revolverComponent);
            }
        }
    }


    public void applyGravityForce(Collider other)
    {
        accumTime += Time.deltaTime;
        Vector3 vec = (transform.position - other.transform.position);
        //float distance_squared = vec.sqrMagnitude;
        //float force = (_mass / distance_squared) * gravityConstant;

//        other.transform.rigidbody.AddForce(force * vec);
        other.transform.GetComponent<Rigidbody>().AddForce((_mass+accumTime)*vec);
    }

    public void applyInvertedGravityForce(Collider other)
    {
        Debug.Log("INVERTED");
        Vector3 vec = (transform.position - other.transform.position);
        float distance_squared = vec.sqrMagnitude;
        float force = (_mass / distance_squared) * gravityConstant;

        other.transform.GetComponent<Rigidbody>().AddForce(1 * vec);
    }

}
