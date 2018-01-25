using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ionic.Zip;

public class SceneTestMxl : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		Debug.Log( "SceneTestMxl - Start" );

		ZipFile zipfile = new ZipFile( Application.streamingAssetsPath + "/89280-Twinkle_Twinkle_Little_Star.mxl", System.Text.Encoding.UTF8 );
		for( int index = 0 ; index < zipfile.EntryFileNames.Count ; ++index )
		{
			Debug.Log( "EntryFileName " + zipfile.EntryFileNames[index] );
		}

		Debug.Log( "SceneTestMxl - End" );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
