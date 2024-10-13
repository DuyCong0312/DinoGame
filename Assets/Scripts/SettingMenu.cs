using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Localization.Settings;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropQuality;
    private bool active = false;
    private const string QualityLevel = "QualityLevel";
    private const string LocaleKey = "LocaleKey";

    private void Start()
    {
        dropQuality.value = PlayerPrefs.GetInt(QualityLevel, 0);
        int ID = PlayerPrefs.GetInt(LocaleKey, 0);
        ChangeLocale(ID);
    }
    public void SetQuanlity(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(QualityLevel, qualityIndex);
    }

    public void ChangeLocale(int loacleID)
    {
        if (active == true)
        {
            return;
        }
        StartCoroutine(SetLocale(loacleID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt(LocaleKey, localeID);
        active = false;
    }
}
