using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaveMaze;

public class DescriptionBehaviour : MonoBehaviour
{
    public Camera mainCam;
    public Transform LightSourceOne;
    public Transform LightSourceTwo;

    public void SetLightStatus(bool ShouldBeActive)
    {
        DarknessEffect darkScript = mainCam.GetComponent<DarknessEffect>();
        if (ShouldBeActive)
        {
            darkScript.StaticGlowPos1 = LightSourceOne.position;
            darkScript.StaticGlowRadius1 = 0.25f;
            darkScript.StaticGlowPos2 = LightSourceTwo.position;
            darkScript.StaticGlowRadius2 = 0.25f;
        }

        Debug.Log("LIGHT!");
    }
}
