using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text bulletsText;
    public TMP_Text gameEndersText;

    private int collectedBullets = 0;
    private int collectedGameEnders = 0;

    private void Start()
    {
        UpdateBulletsText();
        UpdateGameEndersText();
    }

    public void AddBullet()
    {
        collectedBullets++;
        UpdateBulletsText();
    }

    public void AddGameEnder()
    {
        collectedGameEnders++;
        UpdateGameEndersText();
    }

    private void UpdateBulletsText()
    {
        bulletsText.text = "Bullets: " + collectedBullets;
    }

    private void UpdateGameEndersText()
    {
        gameEndersText.text = "Game Enders: " + collectedGameEnders;
    }
}
