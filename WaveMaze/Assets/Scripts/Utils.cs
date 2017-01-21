using UnityEngine;
using System.Collections;

namespace WaveMaze 
{
	
public class Utils
{
	public static Vector3 Lerp(Vector3 v1, Vector3 v2, float t)
	{
		return new Vector3 (Mathf.Lerp (v1.x, v2.x, t),
							Mathf.Lerp (v1.y, v2.y, t), 
							Mathf.Lerp (v1.z, v2.z, t));
	}

	public static float RandRange(float min, float max)
	{
		return min + Random.value * (max - min);
	}

	public static Vector3 RandRange(Vector3 min, Vector3 max)
	{
		return min + Random.value * (max - min);
	}

	public static void Serialize(string name, string GamePath)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		sb.AppendLine("name=" + name);
		System.IO.StreamWriter writer = new System.IO.StreamWriter(GamePath);
		writer.Write(sb.ToString());
		writer.Close();
	}
  
	public static string DeSerialize(string name, string GamePath)
	{
		System.IO.StreamReader reader = new System.IO.StreamReader(GamePath);
		string line = "Undef";
		while(( line = reader.ReadLine() ) != string.Empty){
			Debug.Log (line);
			string[] id_value = line.Split('=');
			switch (id_value[0]){
				case "name":
					name = id_value[1].ToString();
				break;
				}
			}
		reader.Close();
		return name;
		}
	}
}