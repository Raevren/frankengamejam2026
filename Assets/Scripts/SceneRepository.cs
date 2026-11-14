using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
sealed class SceneRepository : ScriptableObject {
    public void LoadSceneByName(string sceneName) => SceneManager.LoadSceneAsync(sceneName);
}