using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DefaultInfo {

	Player player;
	DataConsistence dataConsistence;

	public void PrimerUso(){
		player = new Player ();
		player.experiencia = 100;
		player.semestre = 1;
		player.papa = 0.0f;
		player.nivel = 1;
		niveles ();
		player.expEsperada = 400;
		player.cantidadMaterias = 4;
		materias ();
		player.bolsaDeCreditos = 180;
		player.energiaTotal = 100;
		player.energiaActual = 100;
		player.estresTotal = 100;
		player.estresActual = 100;
		player.dinero = 100;

		GameControl.control.playerData = player;
		SavePlayer ();


		dataConsistence = new DataConsistence ();
		dataConsistence.dia = 6;
		dataConsistence.semana = 16;
		dataConsistence.dias_trancurridos = 0;

		GameControl.control.dataConsistence = dataConsistence;
		SaveDataConsistence ();

		SaveInstalacion ();
	}

	private void niveles(){
		player.niveles = new Nivel [10];

		player.niveles [0] = new Nivel ("Primiparo", 400);
		player.niveles [1] = new Nivel ("Nivel1", 700);
		player.niveles [2] = new Nivel ("Nivel2", 1000);
		player.niveles [3] = new Nivel ("Nivel3", 1300);
		player.niveles [4] = new Nivel ("Nivel4", 1600);
		player.niveles [5] = new Nivel ("Nivel5", 1900);
		player.niveles [6] = new Nivel ("Nivel6", 2200);
		player.niveles [7] = new Nivel ("Nivel7", 2500);
		player.niveles [8] = new Nivel ("Nivel8", 2800);
		player.niveles [9] = new Nivel ("Mas viejo que Dario", 3100);

	}

	private void materias(){
		player.materias = new Materia[4];

		player.materias [0] = new Materia ("Calculo", "Fundamentacion", 4, 3);
		player.materias [0].parciales = new Parcial[3]{ new Parcial(33.3f, 8, "Calculo"), new Parcial(33.3f, 30, "Calculo"), new Parcial(33.3f, 45, "Calculo")};

		player.materias [1] = new Materia ("Fisica", "Disciplinar", 4, 4);
		player.materias [1].parciales = new Parcial[4]{ new Parcial(30f, 10, "Fisica"), new Parcial(30f, 30, "Fisica"), new Parcial(20f, 45, "Fisica"), new Parcial(20f, 60, "Fisica")};

		player.materias [2] = new Materia ("Idiomas I", "Idiomas", 3, 3);
		player.materias [2].parciales = new Parcial[3]{ new Parcial(25f, 15, "Idiomas I"), new Parcial(25f, 30, "Idiomas I"), new Parcial(25f, 45, "Idiomas I")};

		player.materias [3] = new Materia ("Electiva", "Libre eleccion", 3, 3);
		player.materias [3].parciales = new Parcial[3]{ new Parcial(25f, 15, "Electiva"), new Parcial(25f, 30, "Electiva"), new Parcial(25f, 45, "Electiva")};

		player.bolsaDeCreditos -= 14;
	}


	private void SaveInstalacion(){
		try{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (GameControl.control.path + "IntroUNGame.dat");	
			bf.Serialize (file, "Instalado");
			file.Close ();
		}catch(Exception e){
			Debug.Log(e.Message);
		}
	}

	private void SavePlayer(){
		try{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (GameControl.control.path + "Player.dat");	
			bf.Serialize (file, player);
			file.Close ();
		}catch(Exception e){
			Debug.Log(e.Message);
		}
	}

	private void SaveDataConsistence(){
		try{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (GameControl.control.path + "DataConsistence.dat");	
			bf.Serialize (file, dataConsistence);
			file.Close ();
		}catch(Exception e){
			Debug.Log(e.Message);
		}
	}
}
