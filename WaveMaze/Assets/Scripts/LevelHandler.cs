using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace WaveMaze{
	public class LevelHandler : IEquatable<LevelHandler>
    {
        private Sprite m_LevelGround;
        public Sprite LevelGround { get { return m_LevelGround; } }
        private Sprite m_LevelCollider;
        public Sprite LevelCollider { get { return m_LevelCollider; } }
        string m_LevelName;
		public readonly int m_LevelNumber = 1;

		const string c_LvlAssets = "LvlFiles/";  
		const string c_ColliderFolder = "LevelCollider";
		const string c_GroundFolder = "LevelGround";
		const string c_IniFolder = "LevelINI";

		//const string c_LEVEL = "LVL##.png";
        const string c_LEVEL = "LVL##";

        string LevelString
        {
			get{ 
				return c_LEVEL.Replace("##", m_LevelNumber.ToString() ) ;
			}
		}

		string ColliderFolder{
			get{
				return c_LvlAssets + c_ColliderFolder + "/";
			}
		}
		string GroundFolder{
			get{
				return c_LvlAssets + c_GroundFolder + "/";
			}
		}
		string INIFolder{
			get{
				return c_LvlAssets + c_IniFolder + "/";
			}
		}


		public LevelHandler(int LevelNumber){
			m_LevelNumber = LevelNumber;

			load();
		}

		// Use this for initialization
		void load(){
			LoadLevelName();
			LoadLevelGround();
			//LoadLevelCollider();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void LoadLevelGround(){
            
#if UNITY_EDITOR
            Debug.Log("LVLGoundPath: " + GroundFolder + LevelString);
            Texture2D tex = Resources.Load("LvlFiles/LevelGround/" + "LVL1") as Texture2D;

            Debug.LogWarning("Texture is null?" + (tex == null));

            m_LevelGround = Sprite.Create(tex, new Rect(0, 0, tex.width , tex.height), new Vector2(0.5f, 0.5f));

            if (m_LevelGround == null) { Debug.Log("m_LevelGround = null"); }
            #else
				m_LevelGround = LoadPNG(Path.GetDirectoryName( c_GroundFolder) + "/" + LevelString);
#endif

        }

		void LoadLevelCollider(){
            m_LevelCollider = new Sprite();
#if UNITY_EDITOR
            m_LevelCollider = Resources.Load<Sprite>( c_ColliderFolder + LevelString);
			#else
			m_LevelCollider = LoadPNG(Path.GetDirectoryName( c_ColliderFolder)  + "/" + LevelString);
			#endif
		}

		void LoadLevelName(){
			#if UNITY_EDITOR
				//m_LevelName = Utils.DeSerialize("Name", Path.GetDirectoryName( INIFolder ) + "/" + LevelString + ".ini" );
			#else
				m_LevelName = Utils.DeSerialize("Name", Path.GetDirectoryName( INIFolder ) + "/" + LevelString + ".ini" );
			#endif
		}

	    public static Texture2D LoadPNG(string filePath) {
			Texture2D tex = null;
			byte[] fileData;

			if (File.Exists(filePath)){
			     fileData = File.ReadAllBytes(filePath);
			     tex = new Texture2D(1920, 1080);
			     tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
			 }
			 return tex;
		 }

        public bool Equals(LevelHandler other)
        {
            if (other == null) return false;
            return (this.m_LevelNumber.Equals(other.m_LevelNumber));
        }
    }
}
