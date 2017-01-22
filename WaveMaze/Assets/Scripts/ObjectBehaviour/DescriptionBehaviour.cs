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
            /*
            darkScript.EnemyGlowPosition = LightSourceOne.position;
            darkScript.EnemyGlowPosition = LightSourceTwo.position;
            Set Radius
            */

        }
    }
}
