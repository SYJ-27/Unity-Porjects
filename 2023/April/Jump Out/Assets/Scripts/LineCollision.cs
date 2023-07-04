using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollision : MonoBehaviour
{
    LineController lineController;
    PolygonCollider2D polygonCollider;
    List<Vector2> colliderPoints = new List<Vector2>();
    void Start(){
        lineController = GetComponent<LineController>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void Update(){
        // Invoke("ActiveLineCollision", 1);
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.black;
        if(colliderPoints != null){
            colliderPoints.ForEach(p => Gizmos.DrawSphere(p, 0.1f));
        }
    }

    List<Vector2> CalculateColliderPoints(){
        Vector3[] positions = lineController.GetPositions();
        float width = lineController.GetWidth();

        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width/2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width/2f) * (1 / Mathf.Pow(m * m + 1, 0.5f));

        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        List<Vector2> colliderPositions = new List<Vector2>{
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1],
        };

        return colliderPositions;
    }

    public void ActiveLineCollision(){
        lineController.SetLine();
        colliderPoints = CalculateColliderPoints();
        polygonCollider.SetPath(0, colliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
    }

}
