using UnityEngine;

public class worldButtonTrigger : MonoBehaviour
{
    void OnTriggerExit(Collider c)
    {
        if (c.tag == "hand")
        {
            transform.GetChild(0).GetChild(0).GetComponent<WorldButton>().hasPressed = false;
        }
    }
}
