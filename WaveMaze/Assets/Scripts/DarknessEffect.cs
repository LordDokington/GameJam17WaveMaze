using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveMaze {

public class DarknessEffect : MonoBehaviour {

	public Material material;

	private Vector3 m_enemyGlowPosition;
	
	private Vector4 m_staticGlow1;
	private Vector4 m_staticGlow2;

	public Vector3 EnemyGlowPosition
	{
		set { m_enemyGlowPosition = value; }
	}
	
	public Vector3 StaticGlowPos1
	{
		set 
		{ 
			m_staticGlow1.x = value.x;
			m_staticGlow1.y = value.y;			
		}
	}
	
	public Vector3 StaticGlowPos2
	{
		set 
		{ 
			m_staticGlow2.x = value.x;
			m_staticGlow2.y = value.y;			
		}
	}
	
	
	public float StaticGlowRadius1
	{
		set { m_staticGlow1.z = value; }
	}

	public float StaticGlowRadius2
	{
		set { m_staticGlow2.z = value; }
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
		
		Vector2 normalizedGlowPos1 = NormalizedPos (m_staticGlow1);
		Vector2 normalizedGlowPos2 = NormalizedPos (m_staticGlow2);
		m_staticGlow1.x = normalizedGlowPos1.x;
		m_staticGlow1.y = normalizedGlowPos1.y;
		
		m_staticGlow2.x = normalizedGlowPos2.x;
		m_staticGlow2.y = normalizedGlowPos2.y;
		
		material.SetVector( "_StaticHighlight1", m_staticGlow1);
		material.SetVector( "_StaticHighlight2", m_staticGlow2);
	}
}

}