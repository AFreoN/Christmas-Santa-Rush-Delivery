
using UnityEditor;
using UnityEngine;

public class Alpha : Editor
{
	[MenuItem ("Ad Plugin/Clear PlayerPref %#&d", false, -10)]
	public static void Clear ()
	{
		PlayerPrefs.DeleteAll ();
		Debug.Log ("<color=green>PlayerPref Cleared</color>");
	}

}
