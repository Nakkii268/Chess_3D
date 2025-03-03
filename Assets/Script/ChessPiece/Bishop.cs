using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    
    public override List<Vector3Int> GetAvailableMoves(ref ChessPiece[,,] board, int tileCountX, int tileCountY, int tileCountZ)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        int tr = Mathf.Min(tileCountX - currentX, currentZ);//top right
        int tl = Mathf.Min(tileCountX - currentX, tileCountZ - currentZ);//top left
        int bl = Mathf.Min(currentX, tileCountZ - currentZ);//bot left
        int br = Mathf.Min(currentX,  currentZ);//bot right
        
        //top right
        r.AddRange(AvailableTopRight(ref board, tileCountX, tileCountY, tileCountZ, tr));
        r.AddRange(AvailableTopLeft(ref board, tileCountX, tileCountY, tileCountZ, tl));
        r.AddRange(AvailableBotLeft(ref board, tileCountX, tileCountY, tileCountZ, bl));
        r.AddRange(AvailableBotRight(ref board, tileCountX, tileCountY, tileCountZ, br));

        return r;
    }
}
