using UnityEngine;

public class PathGuide : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
