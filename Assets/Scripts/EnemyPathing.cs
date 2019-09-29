using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // config

    // other
    int targetWaypointIndex = 0;
    List<Transform> waypoints;
    WaveConfig waveConfig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveConfig != null)
        {
            ProcessWaypointNavigationForFrame();
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
        ReallyStart();
    }

    private void ReallyStart()
    {
        waypoints = waveConfig.GetWaypoints();

        targetWaypointIndex = 0;
        jumpToWayPointAtIndex(targetWaypointIndex);
        targetWaypointIndex++;
    }

    private void ProcessWaypointNavigationForFrame()
    {
        if (!targetWaypointExceedsLastWaypoint())
        {
            if (moveToWaypointAtIndex(targetWaypointIndex))
            {
                targetWaypointIndex++;
            }
        }
        else
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
        float movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        return (transform.position == targetPosition);
    }

    bool targetWaypointExceedsLastWaypoint()
    {
        return targetWaypointIndex >= waypoints.Count;
    }
}
