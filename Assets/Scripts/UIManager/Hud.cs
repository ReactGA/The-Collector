using UnityEngine;

public class Hud : MonoBehaviour
{
    float reverse = 1.5f;
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);

        transform.Rotate(0,0,4 * Time.fixedDeltaTime);
        
    }
}
