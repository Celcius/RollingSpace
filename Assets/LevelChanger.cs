using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

    [SerializeField]
    private string nextLevel;
    
    void OnCollisionEnter(Collision other)
    {

        if (other.collider.tag == "Player")
        {
            print("changing");
            Application.LoadLevel(nextLevel);
            
        }
    }

}
