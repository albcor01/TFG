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

public class Deberia : EditorWindow
{
    /// <summary>
    /// Initializes the OSC Helper and creates an entry in the Unity menu.
    /// </summary>
    [MenuItem("Window/Deberia")]
    static void Init()
    {
        Deberia window = (Deberia)EditorWindow.GetWindow(typeof(Deberia));
        window.Show();
    }
}

public class irme : EditorWindow
{
    /// <summary>
    /// Initializes the OSC Helper and creates an entry in the Unity menu.
    /// </summary>
    [MenuItem("Window/irme")]
    static void Init()
    {
        irme window = (irme)EditorWindow.GetWindow(typeof(irme));
        window.Show();
    }
}

public class a : EditorWindow
{
    /// <summary>
    /// Initializes the OSC Helper and creates an entry in the Unity menu.
    /// </summary>
    [MenuItem("Window/a")]
    static void Init()
    {
        a window = (a)EditorWindow.GetWindow(typeof(a));
        window.Show();
    }
}

public class la : EditorWindow
{
    /// <summary>
    /// Initializes the OSC Helper and creates an entry in the Unity menu.
    /// </summary>
    [MenuItem("Window/la")]
    static void Init()
    {
        la window = (la)EditorWindow.GetWindow(typeof(la));
        window.Show();
    }
}

public class cama : EditorWindow
{
    /// <summary>
    /// Initializes the OSC Helper and creates an entry in the Unity menu.
    /// </summary>
    [MenuItem("Window/cama")]
    static void Init()
    {
        cama window = (cama)EditorWindow.GetWindow(typeof(cama));
        window.Show();
    }
}



