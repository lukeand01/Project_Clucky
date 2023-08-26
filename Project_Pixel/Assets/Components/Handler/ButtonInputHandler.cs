using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInputHandler : MonoBehaviour
{
    public static ButtonInputHandler instance;

    GameObject holder;

    [Separator("ESSENTIAL COMPONENTS")]
    public InputButton jumpInputButton;
    public InputButton leftInputButton;
    public InputButton rightInputButton;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        holder = transform.GetChild(0).gameObject;
    }

    public void ControlVisibilityOnInputs(bool choice)
    {
        holder.SetActive(choice);
    }

    public void Pause()
    {
        UIHolder.instance.pause.Control();
    }
}
