using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class BySeedMenu : MonoBehaviour
{
    [SerializeField] public TMP_InputField _seed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitGameBySeed()
    {
        SetSeed();
        GameController.CurrentState = CreationStates.BySeed;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetSeed(){
        int seed;
        try
        {
            seed = Int32.Parse(_seed.text);
        }
        catch(FormatException)
        {
            seed = 4;
        }
        Debug.Log(seed);
        GameController.Seed = seed;
        GameController.TemporalSeed = seed;
    }
}
