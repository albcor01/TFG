using UnityEngine;
using UnityEditor;
using UnityOSC;

using System;
using System.Collections.Generic;

public class PluginTFG : EditorWindow
{	
	/// <summary>
	/// Initializes the OSC Helper and creates an entry in the Unity menu.
	/// </summary>
	[MenuItem("Window/Plugin TFG")]
	static void Init ()
	{
        PluginTFG window = (PluginTFG)EditorWindow.GetWindow (typeof(PluginTFG));
		window.Show();
	}
	
	/// <summary>
	/// Executes OnGUI in the panel within the Unity Editor
	/// </summary>
	void OnGUI ()
	{	
		{
			string _status = "\n Estamos trabajando en ello, \n" +
                "Jaime estaría orgulloso";
			GUILayout.Label(_status, EditorStyles.boldLabel);
		}
	}
	
	/// <summary>
	/// Updates the logs of the running clients and servers.
	/// </summary>
	void Update()
	{
		if(EditorApplication.isPlaying)
		{	
			Repaint();
		}
	}
}