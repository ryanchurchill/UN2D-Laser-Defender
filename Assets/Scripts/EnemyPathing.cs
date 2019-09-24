using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // config
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] float moveSpeed = 2;

    // other
    int targetWaypointIndex = 0;
    List<Transform> waypoints;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();

        targetWaypointIndex = 0;
        jumpToWayPointAtIndex(targetWaypointIndex);
        targetWaypointIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessWaypointNavigationForFrame();
    }

    private void ProcessWaypointNavigationForFrame()
    {
        if (!targetWaypointExceedsLastWaypoint())
        {
            if (moveToWaypointAtIndex(targetWaypointIndex))
            {
                targetWaypointIndex++;
            }
        } else
        {
            Destroy(gameObject);
        }
    }

    void jumpToWayPointAtIndex(int index)
    {
        transform.position = waypoints[index].transform.position;
    }

    // return true once we reach destination
    bool moveToWaypointAtIndex(int index)
    {
        Vector3 targetPosition = waypoints[index].transform.position;
        float movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        return (transform.position == targetPosition);
    }

    bool targetWaypointExceedsLastWaypoint()
    {
        return targetWaypointIndex >= waypoints.Count;
    }
}
