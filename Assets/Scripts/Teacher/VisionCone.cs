using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{

    public float fov;
    public float max_view_distance;
    public GameObject teacher;

    private Transform teacherTransform;
    [SerializeField]
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        teacherTransform = teacher.GetComponent<Transform>();
        transform.position = teacherTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector3> linePositions = new List<Vector3>();
        teacherTransform = teacher.GetComponent<Transform>();
        transform.position = teacherTransform.position;

        Debug.Log("Vision Cone position: " + transform.position.ToString());

        // calculate starting vector
        Vector3 start_vector = Quaternion.AngleAxis(360-(fov/2), teacherTransform.up) * teacherTransform.forward;
        start_vector.Normalize();

        Debug.Log("Teacher Forward: " + teacherTransform.forward.ToString());
        Debug.Log("Start vector: " + start_vector.ToString());

        // calculate origin point
        Vector3 origin_point = teacherTransform.position - (gameObject.transform.up * (teacherTransform.lossyScale.y)/2);

        Debug.Log("Origin: " + origin_point.ToString());

        Vector3 iterated_vector = start_vector;
        for (float vert_angle  = 1; vert_angle <= 70; vert_angle++)
        {

            for (float hori_angle = 1; hori_angle <= fov; hori_angle++)
            {
                RaycastHit hit;
                bool hitconfirm = Physics.Linecast(origin_point, iterated_vector * max_view_distance, out hit);

                if (vert_angle - 1 == 0)
                {

                    if (hitconfirm)
                    {
                        linePositions.Add(hit.point);
                    }
                    else
                    {
                        linePositions.Add(iterated_vector * max_view_distance);
                    }

                    iterated_vector = Quaternion.AngleAxis(hori_angle, teacherTransform.right) * iterated_vector;
                    iterated_vector.Normalize();
                }
            }

           
            iterated_vector = Quaternion.AngleAxis(vert_angle, teacherTransform.up) * start_vector;
            iterated_vector.Normalize();
        }
        DrawLine(linePositions.ToArray());
   
    }

    void DrawLine(Vector3[] positions)
    {

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }
}
