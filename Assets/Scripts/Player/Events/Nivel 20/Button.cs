using UnityEngine;
using TMPro;
using System.Collections;

public class Button : Events
{
    [SerializeField] private TextMeshProUGUI countTxt; 

    private PlayerSoundsController soundsController;

    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject black;

    protected override void Start()
    {
        base.Start();

        soundsController = player.GetComponent<PlayerSoundsController>();
    }

    public void StartCount()
    {
        StartCoroutine(RegresiveCount());
    }

    IEnumerator RegresiveCount()
    {
        countTxt.enabled = true;

        soundsController.PlayCount();

        for (int i = 10; i >= 0; i--)
        {
            countTxt.text = i.ToString();

            yield return new WaitForSeconds(1f);  
        }

        black.SetActive(true);

        soundsController.PlayExplotion();

        yield return new WaitForSeconds(7);

        black.SetActive(false);

        victory.SetActive(true);

        countTxt.enabled = false;
    }
}