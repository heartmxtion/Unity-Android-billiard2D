using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsTrajectory : MonoBehaviour
{
    private GameObject DirectionalLine;
    private GameObject TrajectoryLine;
    private GameObject HitCircle;


    void Start()
    {
        DirectionalLine = GameObject.FindGameObjectWithTag("DirectionLine");
        TrajectoryLine = GameObject.FindGameObjectWithTag("TrajectoryLine");
        HitCircle = GameObject.FindWithTag("Hit");
    }

    void Update()
    {
        
    }
}
