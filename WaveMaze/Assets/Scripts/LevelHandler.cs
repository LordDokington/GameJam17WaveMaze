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

		const string c_ColliderFolder = "LevelCollider";
		const string c_GroundFolder = "LevelGround";
		const string c_LEVEL = "LVL";

		string LevelString{
			get{ 
				return c_LEVEL + m_LevelNumber.ToString();
			}
		}

		public LevelHandler(int LevelNumber){
			m_LevelNumber = LevelNumber;
			load();
		}

		// Use this for initialization
		void load() {
			LoadLevelINI();
			LoadLevelGround();
			LoadLevelCollider();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void LoadLevelGround(){
			m_LevelGround = LoadPNG(Path.GetDirectoryName( c_GroundFolder) + "/" + LevelString);
		}

		void LoadLevelCollider(){
			m_LevelCollider = LoadPNG(Path.GetDirectoryName( c_ColliderFolder)  + "/" + LevelString);
		}

		void LoadLevelINI(){
			m_LevelName = Utils.DeSerialize("Name","  " + LevelString);
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
