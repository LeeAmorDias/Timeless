using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagemening : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
