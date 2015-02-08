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

   public bool horizontalRotationKey()
   {
       return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.JoystickButton2);
   }

   public bool verticalRotationKey()
   {
       return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton3);
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
