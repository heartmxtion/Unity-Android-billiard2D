using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public float power = 10f;
    public Rigidbody2D rb;
    public Vector2 minimumPower;
    public Vector2 maximumPower;
    public GameObject MainBall;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;
    Trajectory trLine;

    private Vector2 DirLine = new Vector2();
    private Vector2 CurPos = new Vector2();
    private GameObject DirectionalLine;
    private GameObject TrajectoryLine;
    private GameObject HittedBall;
    void Start()
    {
        cam = Camera.main;
        trLine = GetComponent<Trajectory>();
        DirectionalLine = GameObject.FindGameObjectWithTag("DirectionLine");
        TrajectoryLine = GameObject.FindGameObjectWithTag("TrajectoryLine");
        HittedBall = GameObject.FindWithTag("Hit");
    }

    void Update()
    {
        startPoint = MainBall.transform.position;
        startPoint.z = 15;

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;   
            trLine.RenderLine(startPoint, currentPoint);
            CurPos = currentPoint;

            DirLine = CurPos - new Vector2(MainBall.transform.position.x, MainBall.transform.position.y);
            Vector3 startPos = new Vector3(transform.position.x - DirLine.normalized.x * 0.5f, transform.position.y - DirLine.normalized.y * 0.5f, -1);

            RaycastHit2D hit = Physics2D.Raycast(startPos, -DirLine.normalized);

            Vector2 HittingTrajectory = (new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y) - hit.point).normalized;
            Vector2 BounceTrajectory = Vector2.Perpendicular(HittingTrajectory);

            
            Vector3 endPos = new Vector3(hit.point.x + DirLine.normalized.x * 0.15f, hit.point.y + DirLine.normalized.y * 0.15f, -1);
            Vector3 trajectoryPosition = new Vector3(hit.collider.transform.position.x + HittingTrajectory.x * 0.5f, hit.collider.transform.position.y + HittingTrajectory.y * 0.5f, -1);

            if ((-HittingTrajectory.x > 0 && -HittingTrajectory.y > 0) || (-HittingTrajectory.x < 0 && -HittingTrajectory.y < 0))
            {
                BounceTrajectory = -BounceTrajectory;
            }

            DirectionalLine.GetComponent<LineRenderer>().enabled = true;
            HittedBall.GetComponent<MeshRenderer>().enabled = true;

            DirectionalLine.GetComponent<LineRenderer>().SetPosition(0, startPos);
            DirectionalLine.GetComponent<LineRenderer>().SetPosition(1, endPos);
            if (hit.collider.tag == "Ball")
            {
                TrajectoryLine.GetComponent<LineRenderer>().enabled = true;
                TrajectoryLine.GetComponent<LineRenderer>().SetPosition(0, new Vector3(hit.point.x, hit.point.y, -1));
                TrajectoryLine.GetComponent<LineRenderer>().SetPosition(1, trajectoryPosition);
            }
            else if (hit.collider.tag != "Ball")
            {
                TrajectoryLine.GetComponent<LineRenderer>().enabled = false;
            }
            else if (hit.collider.tag == "Obstacles")
            {
                BounceTrajectory = Vector2.Reflect(endPos.normalized, hit.collider.transform.up);
            }
            Vector3 bouncePosition = new Vector3(endPos.x + BounceTrajectory.x * 0.5f, endPos.y + BounceTrajectory.y * 0.5f, -1);
            DirectionalLine.GetComponent<LineRenderer>().SetPosition(2, bouncePosition);

            HittedBall.transform.position = endPos;
        }
        
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        endPoint.z = 15;

        force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minimumPower.x, maximumPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minimumPower.y, maximumPower.y));

        if (Input.GetMouseButtonUp(0))
        {
            DirectionalLine.GetComponent<LineRenderer>().enabled = false;
            HittedBall.GetComponent<MeshRenderer>().enabled = false;
            TrajectoryLine.GetComponent<LineRenderer>().enabled = false;
            rb.AddForce(force * power, ForceMode2D.Impulse);
            trLine.EndLine(); 
        }
    }

}
