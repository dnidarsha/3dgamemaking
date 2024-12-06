using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
    [SerializeField] private int CoinNum = 2;
    private int counter = 0;
    [SerializeField] private TextMeshProUGUI coinText;
    

    public void addCoin()
    {
        if (counter < CoinNum - 1)
        {
            counter++;
        }
        else
        {
            counter++;
            StartCoroutine(startNewLevel());
        }
        coinText.text = "Coin : " + counter;
        
        PlayerPrefs.SetInt("Coin", counter);
    }

    IEnumerator startNewLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);

    }
}