using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public List<Transform> snipperPoints;
    LineRenderer lineShooting;
    // Start is called before the first frame update
    void Start()
    {
        lineShooting = GetComponent<LineRenderer>();
        lineShooting.positionCount = snipperPoints.Count;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float GetWidth(){
        return lineShooting.startWidth;
    }

    public Vector3[] GetPositions(){
        Vector3[] positions = new Vector3[lineShooting.positionCount];
        lineShooting.GetPositions(positions);
        return positions;
    }

    public void SetLine(){
        lineShooting.SetPositions(snipperPoints.ConvertAll(n => n.position - new Vector3(0,0,5)).ToArray());
    }
}
