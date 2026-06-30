using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Defeat : MonoBehaviour
{
    [SerializeField] private Button tryAgain;

    void Start()
    {
        tryAgain.onClick.AddListener(() => {
            GameManager.Instance.ResetDeathState();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}
