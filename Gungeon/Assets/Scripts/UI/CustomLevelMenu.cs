using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class CustomLevelMenu : MonoBehaviour
{
    [SerializeField] public TMP_InputField _rooms;
    [SerializeField] public TMP_InputField _minEnemies;
    [SerializeField] public TMP_InputField _maxEnemies;
    [SerializeField] public TMP_InputField _minDrops;
    [SerializeField] public TMP_InputField _maxDrops;
    [SerializeField] public TextMeshProUGUI _alertLabel;

    private static int rooms;
    private static int minEnemies;
    private static int maxEnemies;
    private static int minDrops;
    private static int maxDrops;
    public static int Rooms { get => rooms; set => rooms = value; }
    public static int MinEnemies { get => minEnemies; set => minEnemies = value; }
    public static int MaxEnemies { get => maxEnemies; set => maxEnemies = value; }
    public static int MinDrops { get => minDrops; set => minDrops = value; }
    public static int MaxDrops { get => maxDrops; set => maxDrops = value; }

    public void InitCustomGame()
    {
        string message = "";
        message = AssignValues();
        if (message != "")
        {
            _alertLabel.text = message;
            return;
        }
        message = CheckDrops();
        if (message != "")
        {
            _alertLabel.text = message;
            return;
        }
        _alertLabel.text = message;
        GameController.CurrentState = CreationStates.Custom;
        Debug.Log("Create");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public string CheckDrops()
    {
        if (rooms<1)
        {
            return "Rooms number must be greater than 1";
        }
        if (minEnemies<0)
        {
            return "Min enemies number must be greater or equal than 0";
        }
        if (maxEnemies<minEnemies)
        {
            return "Max enemies number must be greater or equal than Min enemies";
        }
        if (minDrops<0)
        {
            return "Min enemies number must be greater or equal than 0";
        }
        if (maxDrops<minDrops)
        {
            return "Max enemies number must be greater or equal than Min enemies";
        }
        return "";
    }

    public string AssignValues()
    {
        try
        {
            rooms = Int32.Parse(_rooms.text);
        }
        catch(FormatException)
        {
            return "Rooms is empty";
        }
        try
        {
            minEnemies = Int32.Parse(_minEnemies.text);
        }
        catch(FormatException)
        {
            return "Min enemies is empty";
        }
        try
        {
            maxEnemies = Int32.Parse(_maxEnemies.text);
        }
        catch(FormatException)
        {
            return "Max enemies is empty";
        }
        try
        {
            minDrops = Int32.Parse(_minDrops.text);
        }
        catch(FormatException)
        {
            return "Min drops is empty";
        }
        try
        {
            maxDrops = Int32.Parse(_maxDrops.text);
        }
        catch(FormatException)
        {
            return "Max drops is empty";
        }
        return "";
    }
}
