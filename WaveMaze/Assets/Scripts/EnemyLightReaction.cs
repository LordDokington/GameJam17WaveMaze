using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLightReaction : MonoBehaviour {

	public Material material;

	Vector3 m_enemyPosition;

	float t = 0.5f;

	public Vector3 EnemyPosition1 { set { m_enemyPosition = value; } }

	void Start()
	{
		m_enemyPosition = new Vector3 (0, 0, 0);
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, material);
	}

	void Update()
	{
		t = Mathf.Repeat (Time.time * 0.4f, 1);
		material.SetFloat ("_Radius", t);

		Vector2 enemyPos = NormalizedPos (m_enemyPosition);

		material.SetVector ("_Position", new Vector4(enemyPos.x, enemyPos.y, 0, 0));
	}

	Vector2 NormalizedPos(Vector3 pos)
	{
		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		Vector2 transformedPos = ( (Vector2) (pos - transform.position) + new Vector2 (5, 5)) / 10f;
		return transformedPos;
	}
}
