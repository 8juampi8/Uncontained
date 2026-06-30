using UnityEngine;
using UnityEngine.UI;

public class Defeat : MonoBehaviour
{
    [SerializeField] private Button tryAgain;

    void Start()
    {
        tryAgain.onClick.AddListener(GameManager.Instance.ResetDeathState);
    }
}
