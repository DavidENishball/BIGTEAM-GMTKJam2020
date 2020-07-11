using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerMoves
{
    WAIT,
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NORTHWEST,
    SOUTHWEST,
    SOUTHEAST,
    NORTHEAST,
    SWORD,
    SHEATHE,
    CANCEL,
    ERROR,

    //
    NONE
}
public enum EMoveResult
{
    SUCCESS,
    NEUTRAL,
    FAILURE,
    DAMAGE,
    SYSTEM_ERROR
}