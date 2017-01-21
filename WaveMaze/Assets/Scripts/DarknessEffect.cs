using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveMaze {

public class DarknessEffect : MonoBehaviour {

	public Material material;

	private Vector3 m_enemyGlowPosition;

	public Vector3 EnemyGlowPosition
	{
		set { m_enemyGlowPosition = value; }
	}

	public float EnemyGlowRadius
	{
		set { material.SetFloat ("_EnemyRadius", value); }
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }

	Vector2 NormalizedPos(Vector3 pos)
	{
		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		Vector2 transformedPos = ( (Vector2) (pos - transform.position) + new Vector2 (5, 5)) / 10f;
		transformedPos.y = 1 - transformedPos.y;
		return transformedPos;
	}

	void Start()
	{
		EnemyGlowPosition = new Vector3(0, 0, 0);
	}
			
	void Update()
	{
		Vector2 normalizedPos = NormalizedPos (m_enemyGlowPosition);
		material.SetVector( "_EnemyPos1", new Vector4(normalizedPos.x, normalizedPos.y, 0, 0) );
	}
}

}