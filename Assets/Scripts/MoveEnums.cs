using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerMoves
{
    WAIT = 0,
    UP = 1,
    DOWN = 2,
    LEFT = 3,
    RIGHT = 4,
    NORTHWEST = 5,
    SOUTHWEST = 6,
    SOUTHEAST = 7,
    NORTHEAST = 8,
    SWORD = 9,
    SHEATHE = 10,
    CANCEL = 11,
    ERROR = 12,

    //
    NONE = 99
}
public enum EMoveResult
{
    SUCCESS,
    NEUTRAL,
    FAILURE,
    DAMAGE,
    SYSTEM_ERROR
}

public enum ECombatResult
{
	SUCCESS,
	NEUTRAL,
	FAILURE,
	DAMAGE,
	SYSTEM_ERROR
}