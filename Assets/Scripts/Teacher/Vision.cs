using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{

    public float fov;
    public float max_view_distance;

    private GameObject teacher;
    private Transform teacherTransform;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private float maxAggressionTimer;
    private bool chasing;
    private Renderer renderer;
    private float aggressionTimer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        teacherTransform = gameObject.GetComponentInParent<Transform>();
        transform.localPosition = Vector3.zero;
        chasing = false;
        aggressionTimer = 0.0f;
       
    }

    // Update is called once per frame
    void Update()
    {

        // instantiate variables
        List<Vector3> linePositions = new List<Vector3>();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Debug.Log("Vision Cone position: " + transform.position.ToString());

        // check if the teacher is chasing someone
        if (chasing) {

            // if the aggressionTimer is greater than 0, decrese it
            if (aggressionTimer > 0)
            {
                aggressionTimer -= 1 * Time.deltaTime;
            }

            // else set chasing to false, because the aggression is over, and turn the vision cone's color back to yellow
            else
            {
                chasing = false;
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }        

        // calculate starting vector, which should be half of the field-of-view degrees to the left of the forward vector of the transform
        Vector3 start_vector = Quaternion.AngleAxis(360 - (fov / 2), transform.up) * transform.forward;
        start_vector.Normalize();

        Debug.Log("Teacher Forward: " + teacherTransform.forward.ToString());
        Debug.Log("Start vector: " + start_vector.ToString());
        Debug.Log("Angle: " + Vector3.Angle(teacherTransform.forward, start_vector).ToString());

        // calculate origin point of the vision cone - should originate from the bottom of the teacher
        Vector3 origin_point = transform.position - (transform.up * (teacherTransform.lossyScale.y) / 2);
        linePositions.Add(origin_point);
        Debug.Log("Origin: " + origin_point.ToString());

        // perform linecasts in cone shape
        Vector3 iteratedVector = start_vector;

        // iterate over the vertical dimension of the cone
        for (float vert_angle = 1; vert_angle <= 70; vert_angle++)
        {
            Vector3 iteratedVertVector = iteratedVector;

            // iterate over the horizontal dimension of the cone
            for (float hori_angle = 2; hori_angle <= fov; hori_angle += 2)
            {
                // shoot a single ray, if it hits on the lowest vertical level, ---
                RaycastHit hit;
                bool hitconfirm = Physics.Linecast(origin_point, origin_point + iteratedVertVector * max_view_distance, out hit);

                if (vert_angle - 1 == 0)
                {
                    // --- then add the hitpoint to the linerenderer, change teacher into chasing mode, turn the vision cone red and start the aggression timer
                    if (hitconfirm)
                    {
                        linePositions.Add(hit.point);
                        if (hit.collider.gameObject.tag == "Player")
                        {
                            chasing = true;
                            gameObject.GetComponent<Renderer>().material.color = Color.red;
                            aggressionTimer = maxAggressionTimer;
                        }
                    }

                    // --- else just add the end point of the linecast to the linerenderer
                    else
                    {
                        linePositions.Add(origin_point + iteratedVertVector * max_view_distance);
                    }

                    
                }
                // calculate the new horizontal vector for the next inner for-loop iteration
                iteratedVertVector = Quaternion.AngleAxis(hori_angle, transform.up) * iteratedVector;
                iteratedVertVector.Normalize();
            }

            // calculate the new vertical vector for the next outer for-loop iteration
            iteratedVector = Quaternion.AngleAxis(vert_angle, transform.forward) * start_vector;
            iteratedVector.Normalize();
        }

        // draw the actual vision cone
        DrawLine(linePositions.ToArray());

    }

    void DrawLine(Vector3[] positions)
    {
        // set the number of positions 
        lineRenderer.positionCount = positions.Length;
        // set the positions
        lineRenderer.SetPositions(positions);
    }

    // this is basically just a getter
    public bool isChasing()
    {
        return chasing;
    }
}
