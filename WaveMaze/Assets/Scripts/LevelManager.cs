using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace WaveMaze{
	public class LevelManager : MonoBehaviour {
		List<LevelHandler> m_LevelList;
        GameObject m_CurrentLevelGo;
        SpriteRenderer m_SpriteRenderer;
        Sprite m_CurrentLevelGoundTexture;
        int m_CurrentLevel = 0;

        Color c_EntryColor = Color.red;
        Color c_ExitColor = Color.green;
        Color c_DontMoveOverColor = Color.black;

        private Sprite m_TransitionGround;
        public Sprite TransitionGround { get { return m_TransitionGround; } }
        private Sprite m_TransitionCollider;
        public Sprite TransitionCollider { get { return m_TransitionCollider; } }

        public LevelData CurrentLevelData{
            get{
                return m_LevelList.Find(x => x.m_LevelNumber == m_CurrentLevel).m_LevelData;
            }
        }

        // Use this for initialization
        void Start() {
            
            m_CurrentLevelGo = new GameObject("CurrentLevelGo");
            m_SpriteRenderer = m_CurrentLevelGo.AddComponent<SpriteRenderer>();
            m_SpriteRenderer.sortingLayerName = "LevelLayer";
            addLevel(1);
            GameManager.Instance.FinishPreloading = true;
            StartLevel(1);
        }
		
		// Update is called once per frame
		void Update(){
			
		}

		void addLevel(int LevelCount)
		{
			if (m_LevelList == null)
			{
				m_LevelList = new List<LevelHandler>();
			}
			m_LevelList.Add( new LevelHandler(LevelCount) );
		}

		public void LoadLevel(int TheLevel){
			addLevel(TheLevel);
		}

        public void StartLevel(int TheLevel) 
		{
            StartTransition();
            m_CurrentLevel = TheLevel;
            m_CurrentLevelGoundTexture = m_LevelList.Find(x => x.m_LevelNumber == TheLevel).LevelGround;
            
            //Debug.Log("Heigth: " + m_CurrentLevelGoundTexture.rect.height.ToString() + "Width: " + m_CurrentLevelGoundTexture.rect.width.ToString());

			m_SpriteRenderer.sprite = m_CurrentLevelGoundTexture;
        }

        void StartTransition() {

        }
    }
}
