using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    public override List<Vector3Int> GetAvailableMoves(ref ChessPiece[,,] board, int tileCountX, int tileCountY, int tileCountZ)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        int tr = Mathf.Min(tileCountX - currentX, currentZ);//top right
        int tl = Mathf.Min(tileCountX - currentX, tileCountZ - currentZ);//top left
        int bl = Mathf.Min(currentX, tileCountZ - currentZ);//bot left
        int br = Mathf.Min(currentX, currentZ);//bot right
        //top
        r.AddRange(AvailableTop(ref board, tileCountX, tileCountY, tileCountZ, (tileCountX - currentX)));

        //bot

        r.AddRange(AvailableBot(ref board, tileCountX, tileCountY, tileCountZ, currentX));

        //left

        r.AddRange(AvailableLeft(ref board, tileCountX, tileCountY, tileCountZ, (tileCountZ - currentZ)));

        //right

        r.AddRange(AvailableRight(ref board, tileCountX, tileCountY, tileCountZ, currentZ));
       

      
        r.AddRange(AvailableTopRight(ref board, tileCountX, tileCountY, tileCountZ, tr));
        r.AddRange(AvailableTopLeft(ref board, tileCountX, tileCountY, tileCountZ, tl));
        r.AddRange(AvailableBotLeft(ref board, tileCountX, tileCountY, tileCountZ, bl));
        r.AddRange(AvailableBotRight(ref board, tileCountX, tileCountY, tileCountZ, br));


        return r;
    }
}
