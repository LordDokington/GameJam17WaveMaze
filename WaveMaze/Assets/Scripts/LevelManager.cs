using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveMaze{
	public class LevelManager : MonoBehaviour {
		List<LevelHandler> m_LevelList;
		// Use this for initialization
		void Start() {
			addLevel(1);
		}
		
		// Update is called once per frame
		void Update() {
			
		}

		void addLevel(int LevelCount){
			if (m_LevelList == null) {
				m_LevelList = new List<LevelHandler> ();
			}
			m_LevelList.Add(new LevelHandler(LevelCount));
		}
	}
}
