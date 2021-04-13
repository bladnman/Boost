using UnityEngine;

public class ApplicationQuit : MonoBehaviour {
  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Debug.Log("Application Quit!");
      Application.Quit();
    }
  }
}
