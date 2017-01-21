using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace WaveMaze{
	public class LevelHandler{
		Texture2D m_LevelGround;
		Texture2D m_LevelCollider;
		string m_LevelName;
		int m_LevelNumber = 1;

		const string c_LvlAssets = "Assets/LvlFiles/";  
		const string c_ColliderFolder = "LevelCollider";
		const string c_GroundFolder = "LevelGround";
		const string c_IniFolder = "LevelINI";

		const string c_LEVEL = "LVL";

        string LevelString
        {
			get{ 
				return c_LEVEL + m_LevelNumber.ToString();
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
			LoadLevelCollider();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void LoadLevelGround(){
			#if UNITY_EDITOR 
				m_LevelGround = LoadPNG(Path.GetDirectoryName( GroundFolder ) + "/" + LevelString);
			#else
				m_LevelGround = LoadPNG(Path.GetDirectoryName( c_GroundFolder) + "/" + LevelString);
			#endif

		}

		void LoadLevelCollider(){
			#if UNITY_EDITOR 
				m_LevelCollider = LoadPNG(Path.GetDirectoryName( c_ColliderFolder)  + "/" + LevelString);
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
			     tex = new Texture2D(2, 2);
			     tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
			 }
			 return tex;
		 }
	}
}
