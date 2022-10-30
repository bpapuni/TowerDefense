using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public static Status instance;
    WaveSpawner waveSpawner;
    [Header("Starting Gold")]
    public int gold;

    private Text GoldAmount;
    private Text StatusMessage;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than 1");
            return;
        }

        instance = this;
    }

    void Start()
    {
        waveSpawner = WaveSpawner.instance;
        GoldAmount = GameObject.FindGameObjectsWithTag("StatusText")[0].GetComponent<Text>();
        StatusMessage = GameObject.FindGameObjectsWithTag("StatusText")[1].GetComponent<Text>();
        UpdateGold(0);
        InvokeRepeating("WaveSpawnTime", 0f, 0.5f);
    }

    void WaveSpawnTime()
    {
        if (waveSpawner.waveCountdown > 0)
            StatusMessage.text = "Time Until First Wave: " + Mathf.Round(waveSpawner.waveCountdown).ToString();
        else
        {
            StatusMessage.text = "";
            CancelInvoke("WaveSpawnTime");
        }
    }

    public void UpdateGold(int goldChange)
    {
        gold += goldChange;
        GoldAmount.text = gold.ToString();
    }

    public void ShowMessage(string message)
    {
        StatusMessage.text = message;
    }

    public void ShowMessage(string message, int displayTime)
    {
        StartCoroutine(Display(message, displayTime));
    }

    public void ClearMessage()
    {
        StatusMessage.text = "";
    }

    IEnumerator Display(string message, int displayTime)
    {
        StatusMessage.text = message;
        yield return new WaitForSeconds(displayTime);
        StatusMessage.text = "";
    }
}
