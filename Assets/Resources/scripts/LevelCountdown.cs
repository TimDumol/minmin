using UnityEngine;
using System.Collections;

/**
 * This class creates a GUI square that shows the time limit for the level
 */

public class LevelCountdown : MonoBehaviour {
	public static int timerInSeconds = 50;

	const float GUI_WIDTH = 0.08f;
	const float GUI_HEIGHT = 0.05f;
	
	void Start () {
		InvokeRepeating ("Countdown", 1.0f, 1.0f);
	}

	public static void AddTime (int time) {
		timerInSeconds += time;
	}

	void Countdown() {
		if (--timerInSeconds <= 0) {
			CancelInvoke ("Countdown");
			MasterController.endGame(false, "Time's up! Hurry up next time!");
		}
	}
	void CountdownWindow(int windowId) {
		//var gui = new OVRGUI ();
		//gui.StereoBox(1, 1, Screen.width * GUI_WIDTH, Screen.height * GUI_HEIGHT, "time 
		GUILayout.BeginVertical ();
		GUILayout.Label ("Time Left: " + timerInSeconds);
		GUILayout.EndVertical ();
	}
	
	void OnGUI() {
	//	if (MasterController.state == GameState.STARTED) {
			const float baseX = 150f;
			const float baseY = 150f;
			GUILayout.Window (0, new Rect(baseX, baseY, Screen.width * GUI_WIDTH, Screen.height * GUI_HEIGHT), CountdownWindow, "");	
			GUILayout.Window (1, new Rect(baseX+0.4f*Screen.width, baseY, Screen.width * GUI_WIDTH, Screen.height * GUI_HEIGHT), CountdownWindow, "");	
	//	}
	}
}
