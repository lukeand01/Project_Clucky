using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHolder : MonoBehaviour
{
    public static UIHolder instance;

    public PauseUI pause;
    public VictoryUI victory;
    public DeathUI death;
    public PlayerGUI player;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }



}
