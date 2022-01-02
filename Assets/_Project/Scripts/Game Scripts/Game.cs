using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
