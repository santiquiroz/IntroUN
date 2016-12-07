using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PruebaGUI : EditorWindow {

	public Player playerData;
	public string ruta;

	[MenuItem ("Window/Game data Editor")]
	static void Init(){
		PruebaGUI window = (PruebaGUI)EditorWindow.GetWindow (typeof(PruebaGUI));
	}

	void OnGUI(){
		if (playerData != null) {
			SerializedObject serializedObject = new SerializedObject (this);
			SerializedProperty serailizedProperty = serializedObject.FindProperty ("playerData");

			EditorGUILayout.PropertyField (serailizedProperty, true);

			serializedObject.ApplyModifiedProperties ();

			if (GUILayout.Button ("Save game")) {
				ruta = Application.persistentDataPath + Path.DirectorySeparatorChar + "InfoBase.dat";
				Save (ruta);
			}
		}

		if (GUILayout.Button ("Load game")) {
			ruta = Application.persistentDataPath + Path.DirectorySeparatorChar + "InfoBase.dat";
			Load (ruta);
		}
	}

	public void Save(string ruta){

		try{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (ruta);	
			bf.Serialize (file, playerData);
			file.Close ();
		}catch(Exception e){
			Debug.Log(e.Message);
		}
	}

	public void Load(string ruta){
		if (File.Exists (ruta)) {
			try{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (ruta, FileMode.Open);
				playerData = (Player) bf.Deserialize (file);
				file.Close ();
			}catch(Exception e){
				Debug.Log(e.Message);
			}
		}

	}
}
