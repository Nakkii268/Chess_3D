using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Rook : ChessPiece
{
    public override List<Vector3Int> GetAvailableMoves(ref ChessPiece[,,] board, int tileCountX, int tileCountY,int tileCountZ)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        
        //top
         r.AddRange(AvailableTop(ref board,tileCountX,tileCountY,tileCountZ, (tileCountX - currentX)));
        
        //bot
        
            r.AddRange(AvailableBot(ref board, tileCountX, tileCountY, tileCountZ, currentX));
        
        //left
        
            r.AddRange(AvailableLeft(ref board, tileCountX, tileCountY, tileCountZ, (tileCountZ - currentZ)));
        
        //right
        
            r.AddRange(AvailableRight(ref board, tileCountX, tileCountY, tileCountZ, currentZ));
        
        return r;
    }
}
