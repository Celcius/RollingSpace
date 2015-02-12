using UnityEngine;
using System.Collections;

public class InputService : MonoBehaviour, ServiceEntity {

    const string id = "InputService";

	// Use this for initialization
	void Start () {
        ServiceLocator.getServiceLocator().registerService(this);
	}

    public string serviceIdentifier()
    {
        return "InputService";
    }

   public static InputService service()
    {
        return (InputService)ServiceLocator.getServiceLocator().getService(id);
    }

   public bool jumpKey()
   {
       return Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0);
   }

   public float horizontalAxis()
   {
       return Input.GetAxis("Horizontal");
   }

   public float verticalAxis()
   {
       return Input.GetAxis("Vertical");
   }

   public Vector2 rotationKeys()
   {
       if (Input.GetKey(KeyCode.LeftShift))
           return new Vector2(1, 1);
       else if (Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.JoystickButton2))
           return new Vector2(1, 0);
       else if (Input.GetKey(KeyCode.JoystickButton3))
           return new Vector2(0, 1);
       return new Vector2(0, 0);
           
   }

   public bool zoomInKey()
   {
       return Input.GetKey(KeyCode.Q) || Input.GetAxis("ZoomIn") == -1;
   }

   public bool zoomOutKey()
   {
       return Input.GetKey(KeyCode.E) || Input.GetAxis("ZoomIn") == 1;
   }

    

}
