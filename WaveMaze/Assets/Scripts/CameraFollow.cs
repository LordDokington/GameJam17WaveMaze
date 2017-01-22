using UnityEngine;
using System.Collections;

namespace WaveMaze 
{

    public class CameraFollow : MonoBehaviour 
    {
	    public GameObject target1;
		public GameObject target2;

	    public float interpolationSpeed = 0.10f;
	
	    // Update is called once per frame
	    void Update ()
	    {
			Vector3 targetPos;
			// target 1 or both != null: 1 or center pos
			if (target1 != null) 
			{
				if (target2 != null) 
				{
					targetPos = (target1.transform.position + target2.transform.position) / 2;
				}
				else targetPos = target1.transform.position;
			}
			// target 2 != null: 2
			else if (target2 != null) 
			{
				targetPos = target2.transform.position;
			} 
			// both null, nothing to do here!
			else { return; }

			Vector3 deltaVec = targetPos - transform.position;

			if (deltaVec.magnitude < 0.02f)
				return;

			float transformY = transform.position.y;
			float transformZ = transform.position.z;

			Vector3 newPos = Utils.Lerp( transform.position, targetPos, interpolationSpeed);
		    newPos.z = transformZ;
            newPos.y = transformY;
            transform.position = newPos;
	    }
    }
}
