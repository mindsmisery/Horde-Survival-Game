using UnityEngine;
using System.Collections;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeElapsed = 1f;
    public TMP_Text startText;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        startText.text = (timeElapsed).ToString("0");
    }
}
