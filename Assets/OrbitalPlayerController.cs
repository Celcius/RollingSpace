using UnityEngine;
using System.Collections;

public class OrbitalPlayerController : MonoBehaviour {

    [SerializeField]
    private PlayerRevolver _revolver = null;

    public bool airborn = true;
    bool jumping = false;
    bool isOutbound = false;
    public int gravityElementCount = 0;
    float jumpPower = 5;
    float beforeAirbornTime = -1;
    ArrayList _collidingPlanets = new ArrayList();
    public void setRevolver(PlayerRevolver revolver)
    {
        /*
        print("setting revolver" + revolver + " " + force);
        if (_revolver == revolver)
            _currentRevolverForce = force;
        else if(force >_currentRevolverForce || _revolver == null)
        {
            _currentRevolverForce = force;
            _revolver = revolver;
        }*/
        airborn = false;
        jumping = false;
        _revolver = revolver;
    }

	// Use this for initialization
	void Start () {
        airborn = true;
	}

    float acumSpeedX;
    float acumSpeedY;
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if(beforeAirbornTime > 0 && _collidingPlanets.Count == 0)
        {
            beforeAirbornTime -= Time.deltaTime;
            if(beforeAirbornTime <=0)
            {
                beforeAirbornTime = -1;
                airborn = true;
            }

        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && !jumping && ! airborn && _revolver != null)
        {
            transform.rigidbody.AddForce((_revolver.transform.position - transform.position * -jumpPower), ForceMode.Impulse);
            jumping = true;
        }

        if(_revolver != null)
        { 
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
          
            
            Vector3 toPlanet = _revolver.transform.position-transform.position;
            if (!jumping && !airborn) // Revolve planet being stand on
                _revolver.revolve(moveX, moveY,toPlanet);
            else 
            {
                // Move in middair
                this.rigidbody.AddForce(new Vector3(0.0f, 0.0f, moveX) *4, ForceMode.Impulse);
            }
        }

        // Apply out of bounds gravity

        if (gravityElementCount == 0)
        {
            if (isOutbound)
            {



                rigidbody.velocity = new Vector3(0, 0, 0);
                isOutbound = true;
            }
            GravityElement element = _revolver.GetComponentInChildren<GravityElement>();
            element.applyInvertedGravityForce(this.collider);

            if (element == null)
                print("Null Gravity Element outside of borders");
        }
        else
            isOutbound = false;
    }   

    void OnCollisionEnter (Collision col)
    {
        Collider other = col.collider;
        if(other.tag == "Planet")
        {
            if (!_collidingPlanets.Contains(other))
            {
                _collidingPlanets.Add(other);
                airborn = false;
                beforeAirbornTime = -1;
               rigidbody.velocity = new Vector3(0, 0, 0);
            }

            
        }
    }

    void OnCollisionExit(Collision col)
    {
        Collider other = col.collider;
        if (other.tag == "Planet")
        {
            if (_collidingPlanets.Contains(other))
                _collidingPlanets.Remove(other);

            if (_collidingPlanets.Count <= 0)
            {
                beforeAirbornTime = 0.2f;
         
            }
        }
    }
}
