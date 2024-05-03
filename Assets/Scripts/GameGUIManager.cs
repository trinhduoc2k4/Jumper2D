using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGui, gameGui;
    public TextMeshProUGUI scoreCoutingText;
    public Image powerBarSlider;

    public Dialog achievementDialog, helpDialog, gameoverDialog;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGui(bool isShow)
    {
        if (gameGui) gameGui.SetActive(isShow);
        if (homeGui) homeGui.SetActive(!isShow);  
    }

    public void UpdateScoreCounting(int score)
    {
        if (scoreCoutingText) scoreCoutingText.text = score.ToString(); 
    }

    public void UpdatePowerBar(float curVal, float totalVal)
    {
        if(powerBarSlider) powerBarSlider.fillAmount = curVal / totalVal;
    }

    public void ShowAchievementDialog()
    {
        if (achievementDialog) achievementDialog.Show(true);
    }

    public void ShowHelpDialog()
    {
        if (helpDialog) helpDialog.Show(true);
    }

    public void ShowGameoverDialog()
    {
        if(gameoverDialog) gameoverDialog.Show(true);   
    }
}
