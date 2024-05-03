using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementDialog : Dialog
{
    public TextMeshProUGUI bestScoreText;

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        if(bestScoreText) bestScoreText.text = Prefs.bestScore.ToString();
    }
}
