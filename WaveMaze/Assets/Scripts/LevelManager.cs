using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WaveMaze{
	public class LevelManager : MonoBehaviour {
		List<LevelHandler> m_LevelList;
        GameObject m_CurrentLevelGo;
        SpriteRenderer m_SpriteRenderer;
        Sprite m_CurrentLevelGoundTexture;
        int m_CurrentLevel = 0;
        
		// Use this for initialization
		void Start() {
            
            m_CurrentLevelGo = new GameObject("m_CurrentLevelGo");
            m_SpriteRenderer = m_CurrentLevelGo.AddComponent<SpriteRenderer>();
 
            addLevel(1);
            GameManager.Instance.FinishPreloading = true;
            StartLeve(1);
        }
		
		// Update is called once per frame
		void Update(){
			
		}

		void addLevel(int LevelCount){
			if (m_LevelList == null) {
				m_LevelList = new List<LevelHandler>();
			}
			m_LevelList.Add( new LevelHandler(LevelCount));
		}

		public void LoadLevel(int TheLevel){
			addLevel(TheLevel);
		}

        public void StartLeve(int TheLevel) {
            StartTransition();
            m_CurrentLevel = TheLevel;
            Texture2D aTexture = m_LevelList.Find(x => x.m_LevelNumber == TheLevel).LevelGround;
            Debug.Log("Width: " +  aTexture.height.ToString() + "Width: " + aTexture.width.ToString());
            Sprite m_CurrentLevelGoundTexture = Sprite.Create(
                 aTexture,
                 new Rect(0, 0, aTexture.width, aTexture.height), 
                 new Vector2(0.5f, 0.5f),
                 1.0f
                 );

            m_SpriteRenderer.sprite = m_CurrentLevelGoundTexture;
        }

        void StartTransition() {

        }



    }
}
