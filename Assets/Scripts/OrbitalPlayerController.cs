using UnityEngine;
using System.Collections;

public class OrbitalPlayerController : MonoBehaviour {

    [SerializeField]
    private PlayerRevolver _revolver = null;

    [SerializeField]
    float WALK_FORCE = 0.2f;
    [SerializeField]
    float AIR_WALK_FORCE = 0.8f;
    [SerializeField]
    float JUMP_POWER = 5;

    public bool airborn = true;
    bool jumping = false;
    bool isOutbound = false;
    public int gravityElementCount = 0;
    float beforeAirbornTime = -1;
    ArrayList _collidingPlanets = new ArrayList();
    int collisionCount = 0;
    Vector3 _newUp;

    public void setRevolver(PlayerRevolver revolver)
    {
        /*
        print("setting revolver" + revolver + " " + force;)
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
        computeState();
        handleJump();

        handleMovement();
        handleOutOfBounds();
    }   

    void handleOutOfBounds()
    {
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

    void computeState()
    {
        _newUp = -(_revolver.transform.position - transform.position).normalized;
        Vector3 lerpedUp = Vector3.Lerp(transform.up, _newUp, Time.deltaTime).normalized;

        // Check Airborn
        if (beforeAirbornTime > 0 && collisionCount == 0)
        {
            beforeAirbornTime -= Time.deltaTime;
            if (beforeAirbornTime <= 0)
            {
                beforeAirbornTime = -1;
                airborn = true;
            }
        }

        if (airborn)
        {
            transform.up = lerpedUp;
            _newUp = lerpedUp;
        }

        Debug.DrawLine(transform.position, transform.position + _newUp, Color.green);
    }


    void handleJump()
    {
        
        // Jump
        if (InputService.service().jumpKey() && !jumping && !airborn && _revolver != null)
        {
            transform.rigidbody.AddForce(_newUp * JUMP_POWER, ForceMode.Impulse);
            jumping = true;
        }
    }

    void handleMovement()
    {
        float moveX = InputService.service().horizontalAxis();
        float moveY = InputService.service().verticalAxis();
        
        // animate rotation
        transform.Rotate(new Vector3(0.0f, 0.0f, -moveX * 10));
         
        // Don't rotate planet move normally
        bool horRot = InputService.service().horizontalRotationKey();
        bool verRot =  InputService.service().verticalRotationKey();
        if (!horRot && ! verRot)
        {
            handleSimpleMovement(moveX, moveY);
        }
        else if (_revolver != null)
        {
            moveX = horRot ? moveX : 0;
            moveY = verRot  & ! horRot? moveY : 0;
            handleRevolverMovement(moveX, moveY);
        }

    }

    void handleRevolverMovement(float moveX, float moveY)
    {

        transform.Rotate(Vector3.Cross(_newUp, new Vector3(0.0f, 0.0f, 1.0f)) * moveY * 5);
        Vector3 toPlanet = _revolver.transform.position - transform.position;

        if (!jumping && !airborn) // Revolve planet being stand on
            _revolver.revolve(moveX, moveY, toPlanet);
        else if (moveX != 0)
        {
            // Move in middair
            //this.rigidbody.AddForce(new Vector3(0.0f, 0.0f, moveX) *4, ForceMode.Impulse);
            //  rotateAround(_revolver.transform, -moveX);
        }
    }
    void handleSimpleMovement(float moveX, float moveY)
    {
            Vector3 sideAngle = new Vector3(-_newUp.y, _newUp.x) / Mathf.Sqrt(_newUp.x * _newUp.x + _newUp.y * _newUp.y);
            Debug.DrawLine(transform.position, transform.position + sideAngle, Color.red);

            float force = airborn ? AIR_WALK_FORCE : WALK_FORCE;
            if ( moveX != 0)
            {
                // rotate around planet's surface 
                rotateAround(_revolver.transform, -moveX / force);

                //  manual stop
                if (moveY < 0)
                {
                    transform.rigidbody.velocity = new Vector3(0, 0, 0);
                }
            }
    }

    void OnCollisionEnter (Collision col)
    {
        if(col.collider.GetComponent<WorldCollider>() != null)
            collisionCount++;
        if(collisionCount ==1)
        {
            airborn = false;
            beforeAirbornTime = -1;
            rigidbody.velocity = new Vector3(0, 0, 0);
        }
        /*
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
        }*/

    }

    void OnCollisionExit(Collision col)
    {
        /*
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
         * */
        if (col.collider.GetComponent<WorldCollider>() != null)
            collisionCount--;
        if (collisionCount == 0)
        {
            airborn = true;
            beforeAirbornTime = 0.2f;
        }
    }

    public void rotateAround(Transform planet, float angle)
    {
        //angle = NormalDir ? angle : -angle;

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
