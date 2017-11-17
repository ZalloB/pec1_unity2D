using UnityEngine;

public class SceneLoadManager : MonoBehaviour {

    public void LoadScreen(string name) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
