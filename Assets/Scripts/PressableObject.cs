using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PressableObject : MonoBehaviour
{
    public GameLogic gameLogic;

    public abstract void Press();

    public abstract void Restore();
}
