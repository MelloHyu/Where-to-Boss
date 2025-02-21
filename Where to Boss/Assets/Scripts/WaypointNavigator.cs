using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    private NPCNavigationController controllerNPC;
    public Waypoint currentWaypoint;
    int direction;
    private void Awake()
    {
        controllerNPC = GetComponent<NPCNavigationController>();
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f,1f));
        controllerNPC.SetDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if(controllerNPC.reachedDestination)
        {

            bool shouldBranch = false;
            if(currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
            }
            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
            }
            else
            {
                if (direction == 0)
                {
                    if (currentWaypoint.nextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                        direction = 1;
                    }
                }

                else if (direction == 1)
                {
                    if (currentWaypoint.previousWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                        direction = 0;
                    }
                }

            }

            controllerNPC.SetDestination(currentWaypoint.GetPosition());
          
        }
    }
}
