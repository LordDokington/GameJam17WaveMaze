using UnityEngine;
using System.Collections;

namespace WaveMaze 
{

    public class CameraFollow : MonoBehaviour 
    {
	    public GameObject target;

	    private float interpolationSpeed = 0.14f;
	
	    // Update is called once per frame
	    void Update ()
	    {
		    Vector3 deltaVec = target.transform.position - transform.position;
		
		    if (deltaVec.magnitude < 0.02f)
			    return;

		    float transformY = transform.position.y;
            float transformZ = transform.position.z;

            Vector3 newPos = Utils.Lerp( transform.position, target.transform.position, interpolationSpeed);
		    newPos.z = transformZ;
            newPos.y = transformY;
            transform.position = newPos;
	    }
    }
}
