
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;

    void Start()
    {
        int score = PlayerPrefs.GetInt("Score", 0);
        textField.text = "Your score is: " + score;
    }
}
