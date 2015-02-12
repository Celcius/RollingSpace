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

    public bool ungrounded = true;
    bool isOutbound = false;
    public int gravityElementCount = 0;
    float beforeAirbornTime = -1;
    ArrayList _collidingPlanets = new ArrayList();
    int collisionCount = 0;
    Vector3 _newUp;
    Vector3 _newSideAngle;

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
        ungrounded = false;
        _revolver = revolver;
    }

	// Use this for initialization
	void Start () {
        ungrounded = true;
	}

    float acumSpeedX;
    float acumSpeedY;
	
    void Update()
    {

        float moveX = InputService.service().horizontalAxis();
        float moveY = InputService.service().verticalAxis();
        handleJump(moveX, moveY);
        handleMovement(moveX, moveY);
    }
	// Update is called once per frame
    void FixedUpdate()
    {


        computeState();

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
       // Vector3 lerpedUp = Vector3.Lerp(transform.up, _newUp, Time.deltaTime).normalized;

        // Check Airborn
        if (beforeAirbornTime > 0 && collisionCount == 0)
        {
            beforeAirbornTime -= Time.deltaTime;
            if (beforeAirbornTime <= 0)
            {
                beforeAirbornTime = -1;
                ungrounded = true;
            }
        }

        if (ungrounded)
        {
            transform.up = _newUp;//lerpedUp;
            _newUp = _newUp;//lerpedUp;
        }

        _newSideAngle = new Vector3(-_newUp.y, _newUp.x) / Mathf.Sqrt(_newUp.x * _newUp.x + _newUp.y * _newUp.y);
        //Debug.DrawLine(transform.position, transform.position + _newSideAngle, Color.red);

        Debug.DrawLine(transform.position, transform.position + _newUp, Color.green);


    }


    void handleJump(float moveX,float moveY)
    {

        Debug.DrawRay(transform.position, -_newUp*1.0F);
        // Jump
        if (InputService.service().jumpKey())
        {
            bool airborn = true;
            // Downward feeler for non planet floor
            UnityEngine.RaycastHit hitRay;
            if (Physics.Raycast(transform.position, -_newUp, out hitRay, 1.0F))
            {
                /*
                if (hitRay.collider.tag == "Planet")
                {
                    Debug.Log("canJump");
                    airborn = false;
                }
                else
                {
                    Debug.Log("NOP");
                    airborn = true;
                }*/
                airborn = false;
            }

            if (!airborn) // regular jump
            {
                transform.rigidbody.AddForce(_newUp * JUMP_POWER, ForceMode.Impulse);
            }
            else if (ungrounded)
            {
               
                if (moveX == 0)
                    return;
               
                // check side vectors
                 Vector3 side = moveX > 0 ? -_newSideAngle : _newSideAngle; 
                 Debug.DrawLine(transform.position, transform.position + side * 0.7F, Color.yellow);
                    if (Physics.Raycast(transform.position, side, out hitRay, 2.0F))

                    {
                        
                       if(hitRay.collider.tag == "Planet")
                       {
                           Debug.Log("Wall Jump");
                           transform.rigidbody.AddForce(_newUp * JUMP_POWER, ForceMode.Impulse);
                           transform.rigidbody.AddForce(-side * JUMP_POWER, ForceMode.Impulse);
                       }
		            }

                    
            }
        }
    }

    void handleMovement(float moveX,float moveY)
    {

        
        // animate rotation
        transform.Rotate(new Vector3(0.0f, 0.0f, -moveX * 10));
         

        Vector2 moveKeys = InputService.service().rotationKeys();

        if (moveKeys == Vector2.zero)
        {
            // Don't rotate planet move normally
            handleSimpleMovement(moveX, moveY);
        }
        else if (_revolver != null)
        {
            moveX = moveKeys.x * moveX;
            moveY = moveKeys.y * moveY;

            handleRevolverMovement(moveX, moveY);
        }

    }

    void handleRevolverMovement(float moveX, float moveY)
    {

        transform.Rotate(Vector3.Cross(_newUp, new Vector3(0.0f, 0.0f, 1.0f)) * moveY * 5);
        Vector3 toPlanet = _revolver.transform.position - transform.position;

        if (!ungrounded) // Revolve planet being stand on
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


            float force = ungrounded ? AIR_WALK_FORCE : WALK_FORCE;
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
            ungrounded = false;
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
            ungrounded = true;
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
