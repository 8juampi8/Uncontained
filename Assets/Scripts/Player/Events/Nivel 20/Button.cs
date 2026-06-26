using UnityEngine;
using TMPro;
using System.Collections;

public class Button : Events
{
    [SerializeField] private TextMeshProUGUI countTxt;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject black;
    [SerializeField] private AudioClip countSound;
    [SerializeField] private AudioClip explosionSound;

    protected override void Start()
    {
        base.Start();
    }

    public void StartCount()
    {
        StartCoroutine(RegresiveCount());
    }

    IEnumerator RegresiveCount()
    {
        countTxt.enabled = true;

        if (countSound != null) AudioManager.Instance.PlaySFX(countSound);

        for (int i = 10; i >= 0; i--)
        {
            countTxt.text = i.ToString();

            yield return new WaitForSeconds(1f);  
        }

        black.SetActive(true);

        if (explosionSound != null) AudioManager.Instance.PlaySFX(explosionSound);

        yield return new WaitForSeconds(7);

        black.SetActive(false);

        victory.SetActive(true);

        countTxt.enabled = false;
    }
}