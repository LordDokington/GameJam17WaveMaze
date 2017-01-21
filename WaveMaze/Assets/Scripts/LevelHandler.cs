using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using Assets.Scripts;


namespace WaveMaze{
	public class LevelHandler
    {
        private Sprite m_LevelGround;
        public Sprite LevelGround { get { return m_LevelGround; } }
        private Sprite m_LevelCollider;
        public Sprite LevelCollider { get { return m_LevelCollider; } }
        private Sprite m_EndGround;
        public Sprite EndGround { get { return m_EndGround; } }
        private Sprite m_EndCollider;
        public Sprite EndCollider { get { return m_EndCollider; } }
        private Sprite m_EntryGround;
        public Sprite EntryGround { get { return m_EntryGround; } }
        private Sprite m_EntryCollider;
        public Sprite EntryCollider { get { return m_EntryCollider; } }
        string m_LevelName;
		public readonly int m_LevelNumber = 1;

        public LevelData m_LevelData;

        string LevelString
        {
			get{ 
				return Defines.c_LEVEL.Replace("##", m_LevelNumber.ToString() ) ;
			}
		}

		string ColliderFolder{
			get{
				return Defines.c_LvlAssets + Defines.c_ColliderFolder + "/";
			}
		}
		string GroundFolder{
			get{
				return Defines.c_LvlAssets + Defines.c_GroundFolder + "/";
			}
		}
		string INIFolder{
			get{
				return Defines.c_LvlAssets + Defines.c_IniFolder + "/";
			}
		}


		public LevelHandler(int LevelNumber){
			m_LevelNumber = LevelNumber;
            m_LevelData = GameManager.Instance.GetGameData.m_LevelDataList.FirstOrDefault(x => x.m_LevelNumber == LevelNumber);
            load();
		}

		// Use this for initialization
		void load(){
			LoadLevelGround();
			LoadLevelCollider();
            if (m_LevelData.HasEntry) {
                LoadEntryGround();
                LoadEntryCollider();
            }
            if (m_LevelData.HasEnd)
            {
                LoadEndGround();
                LoadEndCollider();
            }
        }
		
		// Update is called once per frame
		void Update () {
			
		}

        private Sprite LoadTexture(string ThePath) {
            Debug.Log(ThePath);
            Texture2D tex = Resources.Load<Texture2D>(ThePath);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }

		void LoadLevelGround(){

#if UNITY_EDITOR
            string aPath = GroundFolder + LevelString;
#else
			string aPath = GroundFolder + LevelString;
#endif
            m_LevelGround = LoadTexture(aPath);
        }

		void LoadLevelCollider(){
#if UNITY_EDITOR
            string aPath = ColliderFolder + LevelString;
#else
			string aPath = ColliderFolder + LevelString;
#endif
            m_LevelCollider = LoadTexture(aPath);
            
        }

        void LoadEntryGround()
        {
#if UNITY_EDITOR
            string aPath = GroundFolder + LevelString + Defines.EntryString ;
#else
			string aPath = GroundFolder + LevelString + Defines.EntryString;
#endif
            m_EntryGround = LoadTexture(aPath);
        }


        void LoadEntryCollider()
        {
#if UNITY_EDITOR
            string aPath = ColliderFolder + LevelString + Defines.EntryString;
#else
			string aPath = ColliderFolder + LevelString + Defines.EntryString;
#endif
            m_EntryCollider = LoadTexture(aPath);
        }

        void LoadEndGround()
        {
#if UNITY_EDITOR
            string aPath = GroundFolder + LevelString + Defines.EndString;
#else
			string aPath = GroundFolder + LevelString + Defines.EndString;
#endif
            m_EndGround = LoadTexture(aPath);
        }

        void LoadEndCollider()
        {
#if UNITY_EDITOR
            string aPath = ColliderFolder + LevelString + Defines.EndString;
#else
			string aPath = ColliderFolder + LevelString + Defines.EndString;
#endif
            m_EndCollider = LoadTexture(aPath);
        }

    }
}
