using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChessPieceData 
{
    [field: SerializeField] public ChessPieceType type;
    [field: SerializeField] public int x;
    [field: SerializeField] public int y;
    [field: SerializeField] public int z;
    [field: SerializeField] public int team;

    public ChessPieceData(ChessPieceType type, int x, int y, int z, int team)
    {
        this.type = type;
        this.x = x;
        this.y = y;
        this.z = z;
        this.team = team;
    }
}
