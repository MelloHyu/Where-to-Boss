using JetBrains.Annotations;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private Vector3 destination;
    [SerializeField] private float stopDistance;
    private bool reachedDestination;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
        }
    }
        public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
    

}
