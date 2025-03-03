using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Knight : ChessPiece
{
    public override  List<Vector3Int> GetAvailableMoves(ref ChessPiece[,,] board, int tileCountX, int tileCountY,int tileCounZ)
    {
        List<Vector3Int> r = new List<Vector3Int>();

        int x = currentX + 1;
        int z = currentZ + 2;
        for(int i=-1;i < 2; i++)
        {
            if (x < tileCountX && z < tileCounZ)
            {
                if (currentY + i < tileCountY)
                {
                    if (ChessBoard.Instance.IsTileExist(x, currentY + i, z))
                    {
                        if (board[x, currentY + i, z] == null || board[x, currentY + i, z].team != team)
                        {
                            r.Add(new Vector3Int(x, currentY + i, z));
                        }
                    }
                }
            }
        }
        



        x = currentX + 2;
        z = currentZ + 1;
        for (int i = -1; i < 2; i++)
        {
            if (x < tileCountX && z < tileCounZ)
            {
                if (currentY + i < tileCountY)
                {
                    if (ChessBoard.Instance.IsTileExist(x, currentY + i, z))
                    {
                        if (board[x, currentY + i, z] == null || board[x, currentY + i, z].team != team)
                        {
                            r.Add(new Vector3Int(x, currentY + i, z));
                        }
                    }
                }
            }
        }



        x = currentX - 2;
        z = currentZ - 1;
        for (int i = -1; i < 2; i++)
        {
            if (x >=0 && z >=0)
            {
                if (currentY + i < tileCountY)
                {
                    if (ChessBoard.Instance.IsTileExist(x, currentY + i, z))
                    {
                        if (board[x, currentY + i, z] == null || board[x, currentY + i, z].team != team)
                        {
                            r.Add(new Vector3Int(x, currentY + i, z));
                        }
                    }
                }
            }
        }



        x = currentX - 1;
        z = currentZ - 2;
        for (int i = -1; i < 2; i++)
        {
            if (x >= 0 && z >= 0)
            {
                if (currentY + i < tileCountY)
                {
                    if (ChessBoard.Instance.IsTileExist(x, currentY + i, z))
                    {
                        if (board[x, currentY + i, z] == null || board[x, currentY + i, z].team != team)
                        {
                            r.Add(new Vector3Int(x, currentY + i, z));
                        }
                    }
                }
            }
        }


        x = currentX + 1;
        z = currentZ - 2;
        for (int i = -1; i < 2; i++)
        {
            if (x <tileCountX && z >= 0)
            {
                if (currentY + i < tileCountY)
                {
                    if (ChessBoard.Instance.IsTileExist(x, currentY + i, z))
                    {
                        if (board[x, currentY + i, z] == null || board[x, currentY + i, z].team != team)
                        {
                            r.Add(new Vector3Int(x, currentY + i, z));
                        }
                    }
                }
            }
        }

        x = currentX + 2;
        z = currentZ - 1;
        for (int i = -1; i < 2; i++)
        {
            if (x < tileCountX && z >= 0)
            {
                if (currentY + i < tileCountY)
                {
                    if (ChessBoard.Instance.IsTileExist(x, currentY + i, z))
                    {
                        if (board[x, currentY + i, z] == null || board[x, currentY + i, z].team != team)
                        {
                            r.Add(new Vector3Int(x, currentY + i, z));
                        }
                    }
                }
            }
        }


        x = currentX - 2;
        z = currentZ + 1;
        for (int i = -1; i < 2; i++)
        {
            if (x >=0 && z < tileCounZ)
            {
                if (currentY + i < tileCountY)
                {
                    if (ChessBoard.Instance.IsTileExist(x, currentY + i, z))
                    {
                        if (board[x, currentY + i, z] == null || board[x, currentY + i, z].team != team)
                        {
                            r.Add(new Vector3Int(x, currentY + i, z));
                        }
                    }
                }
            }
        }


        x = currentX - 1;
        z = currentZ + 2;
        for (int i = -1; i < 2; i++)
        {
            if (x >= 0 && z < tileCounZ)
            {
                if (currentY + i < tileCountY)
                {
                    if (ChessBoard.Instance.IsTileExist(x, currentY + i, z))
                    {
                        if (board[x, currentY + i, z] == null || board[x, currentY + i, z].team != team)
                        {
                            r.Add(new Vector3Int(x, currentY + i, z));
                        }
                    }
                }
            }
        }

        return r;
    }
}
