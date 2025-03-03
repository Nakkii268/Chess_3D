using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public override  List<Vector3Int> GetAvailableMoves(ref ChessPiece[,,] board, int tileCountX, int tileCountY,int tileCountZ)
    {
        List<Vector3Int> r = new List<Vector3Int>();


        r.AddRange(AvailableTop(ref board, tileCountX, tileCountY, tileCountZ, 1));
        r.AddRange(AvailableBot(ref board, tileCountX, tileCountY, tileCountZ, 1));
        r.AddRange(AvailableLeft(ref board, tileCountX, tileCountY, tileCountZ, 1));
        r.AddRange(AvailableRight(ref board, tileCountX, tileCountY, tileCountZ, 1));
        r.AddRange(AvailableTopRight(ref board, tileCountX, tileCountY, tileCountZ, 1));
        r.AddRange(AvailableTopLeft(ref board, tileCountX, tileCountY, tileCountZ, 1));
        r.AddRange(AvailableBotLeft(ref board, tileCountX, tileCountY, tileCountZ, 1));
        r.AddRange(AvailableBotRight(ref board, tileCountX, tileCountY, tileCountZ, 1));

        return r;
    }
}
