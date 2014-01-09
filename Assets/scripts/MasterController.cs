using UnityEngine;
using System.Collections;

public class MasterController : MonoBehaviour {
	const float MSG_WIDTH = 0.4f;
	const float MSG_HEIGHT = 0.2f;	
	public static GameState state = GameState.STARTED;
	static string endMessage = null;

	public static void endGame(bool win, string endMessage) {
		if (win) 
		{
			state = GameState.WON;
		} else
		{
			state = GameState.LOST;
		}
		Time.timeScale = 0;
		MasterController.endMessage = endMessage;
	}


	public static string GetEndGameMessage() {
		string message;
		if (state == GameState.WON) {
			message = "You've escaped the floor!";
		} else {
			if (endMessage != null) {
				message = string.Format(endMessage);
			} else {
				message = "Sorry, you have died :(";
            }
        }
		return message;
    }
    // Use this for initialization
	void Start () {
		/**Temporary commenting this out for tomorrow.
		 * GameObject boulder = Resources.Load ("Assets/prefab/boulder") as GameObject;
		for (int i = 0; i < 20; ++i) {
			GameObject copy = Instantiate (boulder) as GameObject;
			copy.transform.position = new Vector3(Random.Range (-20, 20), Random.Range (5, 10), Random.Range(-20, 20));
		}
		 */
	}

	void EndGameWindow(int windowId) {
		GUILayout.BeginVertical ();
		GUILayout.Label (GetEndGameMessage ());
		if (GUILayout.Button ("Restart")) {
			Application.LoadLevel("main");
			state = GameState.STARTED;
			Time.timeScale = 1;
		}
		GUILayout.EndVertical ();
	}

	void OnGUI() {
		if (state != GameState.STARTED) {
		GUILayout.Window (0, new Rect((Screen.width - Screen.width * MSG_WIDTH)/2.0f, (Screen.height - Screen.height * MSG_HEIGHT)/2.0f, Screen.width * MSG_WIDTH, Screen.height * MSG_HEIGHT),
			        EndGameWindow, "");

		}
	}

	// Update is called once per frame
	void Update () {

	}
}
