using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ChessPieceType
{
    None =0,
    Pawn = 1,
    Rook = 2,
    Knight = 3,
    Bishop = 4,
    Queen = 5,
    King = 6
}
public class ChessPiece : MonoBehaviour
{
    public int team;
    public int currentX;
    public int currentY;
    public int currentZ;
    public ChessPieceType type;
    public int pieceValue;
    private Vector3 desiredPosition;
    private Vector3 desiredScale=Vector3.one;//scle up/down when piece die maybe >.o

    private void Start()
    {

       // transform.rotation = Quaternion.Euler((team == 0 ? Vector3.zero : new Vector3(0, 180, 0)));
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime*10);
        transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime*10);
    }

    public virtual List<Vector3Int> GetAvailableMoves(ref ChessPiece[,,] board,int tileCountX,int tileCountY,int tileCountZ)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        
        
        return r;
    }
    public virtual void SetPosition(Vector3 pos, bool force = false)
    {
        desiredPosition = pos;
        if (force) transform.Translate(desiredPosition*Time.deltaTime);
    }
    public virtual void SetScale(Vector3 scale, bool force = false)
    {
        desiredScale = scale;
        if(force) transform.localScale = desiredScale;
    }
    public virtual void SetRotation(Vector3 angle)
    {
        transform.rotation = Quaternion.Euler(angle);
    }
   
    public List<Vector3Int> AvailableTop(ref ChessPiece[,,] board,int x,int  y,int z,int runTime)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        
        for(int i=1;i <= runTime; i++)
        {


            if (currentX + i < x)
            {
                if (currentY + 1 < y)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX + i, currentY + 1, currentZ))
                    {

                        if (board[currentX + i, currentY + 1, currentZ] == null )
                        {
                            if (ChessBoard.Instance.tiles[currentX + i, currentZ, currentY + 1].gameObject.GetComponent<Tag>().direction == DirectionalSlope.top)
                            {
                                r.Add(new Vector3Int(currentX + i, currentY + 1, currentZ));
                                break;

                            }
                        }
                        else if (board[currentX + i, currentY + 1, currentZ].team != team)
                        {
                            if (ChessBoard.Instance.tiles[currentX + i, currentZ, currentY + 1].gameObject.GetComponent<Tag>().direction == DirectionalSlope.top)
                            {
                                r.Add(new Vector3Int(currentX + i, currentY + 1, currentZ));
                                break;
                            }
                        }else if(board[currentX + i, currentY + 1, currentZ].team == team) { break; }

                    }
                    
                }
                if (currentY - 1 >= 0)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX + i, currentY - 1, currentZ))
                    {
                        if (board[currentX + i, currentY - 1, currentZ] == null)
                        {
                            r.Add(new Vector3Int(currentX + i, currentY - 1, currentZ));
                        }
                        else if (board[currentX + i, currentY - 1, currentZ].team != team)
                        {
                            r.Add(new Vector3Int(currentX + i, currentY - 1, currentZ));
                            break;

                        }
                        else if (board[currentX + i, currentY - 1, currentZ].team == team) { break; }
                    }
                    

                }

                if (ChessBoard.Instance.IsTileExist(currentX + i, currentY, currentZ))
                {
                    if (board[currentX + i, currentY, currentZ] == null)
                    {
                        r.Add(new Vector3Int(currentX + i, currentY, currentZ));
                    }
                    else if (board[currentX + i, currentY, currentZ].team != team)
                    {
                        r.Add(new Vector3Int(currentX + i, currentY, currentZ));
                        break;

                    }
                    else if (board[currentX + i, currentY, currentZ].team == team) { break; }
                }
                else
                {
                    break;
                }
                

            }
            

        }
       
        return r;
    }
    public List<Vector3Int> AvailableBot(ref ChessPiece[,,] board,int x,int  y,int z,int runTime)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        

        for(int i=1;i <= runTime; i++) {
            if (currentX - i >= 0)
            {
                if (currentY + 1 < y)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX - i, currentY + 1, currentZ))
                    {

                        if (board[currentX - i, currentY + 1, currentZ] == null )
                        {
                            if( ChessBoard.Instance.tiles[currentX - i, currentZ, currentY + 1].gameObject.GetComponent<Tag>().direction == DirectionalSlope.bottom)
                            {
                                r.Add(new Vector3Int(currentX - i, currentY + 1, currentZ));
                                break;

                            }
                        }
                        else if (board[currentX - i, currentY + 1, currentZ].team != team)
                        {
                            if (ChessBoard.Instance.tiles[currentX - i, currentZ, currentY + 1].gameObject.GetComponent<Tag>().direction == DirectionalSlope.bottom)
                            {
                                r.Add(new Vector3Int(currentX - i, currentY + 1, currentZ));
                                break;
                            }
                        }
                        else if (board[currentX - i, currentY + 1, currentZ].team == team) { break; }

                    }
                   
                }
                if (currentY - 1 >= 0)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX - i, currentY - 1, currentZ))
                    {
                        if (board[currentX - i, currentY - 1, currentZ] == null)
                        {
                            r.Add(new Vector3Int(currentX - i, currentY - 1, currentZ));
                        }
                        else if (board[currentX - i, currentY - 1, currentZ].team != team)
                        {
                            r.Add(new Vector3Int(currentX - i, currentY - 1, currentZ));
                            break;

                        }
                        else if (board[currentX - i, currentY - 1, currentZ].team == team) { break; }
                    }
                    

                }

                if (ChessBoard.Instance.IsTileExist(currentX - i, currentY, currentZ))
                {
                    if (currentX - i == 0)
                    {
                        Debug.Log("run");
                    }
                    if (board[currentX - i, currentY, currentZ] == null)
                    {
                        r.Add(new Vector3Int(currentX - i, currentY, currentZ));
                    }
                    else if (board[currentX - i, currentY, currentZ].team != team)
                    {
                        r.Add(new Vector3Int(currentX - i, currentY, currentZ));
                        break;
                    }
                    else if (board[currentX - i, currentY, currentZ].team == team) { break; }
                }
                else
                {
                    break;
                }



            }
        }

        return r;
    }
    public List<Vector3Int> AvailableRight(ref ChessPiece[,,] board,int x,int  y,int z,int runTime)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        
        for(int i=1;i <= runTime; i++) {
            if (currentZ - i >= 0)
            {
                if (currentY + 1 < y)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX, currentY + 1, currentZ - i))
                    {

                        if (board[currentX, currentY + 1, currentZ - i] == null )
                        {
                            if(ChessBoard.Instance.tiles[currentX, currentZ - i, currentY + 1].gameObject.GetComponent<Tag>().direction == DirectionalSlope.right)
                            {

                                r.Add(new Vector3Int(currentX, currentY + 1, currentZ - i));
                                break;
                            }
                        }
                        else if (board[currentX, currentY + 1, currentZ - i].team != team )
                        {
                            if (ChessBoard.Instance.tiles[currentX, currentZ - i, currentY + 1].gameObject.GetComponent<Tag>().direction == DirectionalSlope.right)
                            {

                                r.Add(new Vector3Int(currentX, currentY + 1, currentZ - i));
                                break;

                            }

                        }
                        else if (board[currentX, currentY + 1, currentZ - i].team == team) { break; }

                    }
                    
                }
                if (currentY - 1 >= 0)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX, currentY - 1, currentZ - i))
                    {
                        if (board[currentX, currentY - 1, currentZ - i] == null)
                        {
                            r.Add(new Vector3Int(currentX, currentY - 1, currentZ - i));
                        }
                        else if (board[currentX, currentY - 1, currentZ - i].team != team)
                        {
                            r.Add(new Vector3Int(currentX, currentY - 1, currentZ - i));
                            break;

                        }
                        else if (board[currentX, currentY - 1, currentZ - i].team == team) { break; }
                    }
                    

                }

                if (ChessBoard.Instance.IsTileExist(currentX, currentY, currentZ - i))
                {
                    if (board[currentX, currentY, currentZ - i] == null)
                    {
                        r.Add(new Vector3Int(currentX, currentY, currentZ - i));
                    }
                    else if (board[currentX, currentY, currentZ - i].team != team)
                    {
                        r.Add(new Vector3Int(currentX, currentY, currentZ - i));
                        break;

                    }
                    else if (board[currentX, currentY, currentZ - i].team == team) { break; }
                }
                else
                {
                    break;
                }

            }



        }

        return r;
    }
    public List<Vector3Int> AvailableLeft(ref ChessPiece[,,] board,int x,int  y,int z,int runTime)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        
        for(int i=1; i <= runTime; i++)
        {

            if (currentZ + i < z)
            {
                if (currentY + 1 < y)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX, currentY + 1, currentZ + i))
                    {

                        if (board[currentX, currentY + 1, currentZ + i] == null)
                        {
                            if( ChessBoard.Instance.tiles[currentX, currentZ + i, currentY + 1].gameObject.GetComponent<Tag>().direction == DirectionalSlope.left)
                            {
                                r.Add(new Vector3Int(currentX, currentY + 1, currentZ + i));
                                break;

                            }
                        }
                        else if (board[currentX, currentY + 1, currentZ + i].team != team)
                        {
                             
                            if (ChessBoard.Instance.tiles[currentX, currentZ + i, currentY + 1].gameObject.GetComponent<Tag>().direction == DirectionalSlope.left)
                            {
                                r.Add(new Vector3Int(currentX, currentY + 1, currentZ + i));
                                break;
                            }
                               
                        }
                        else if (board[currentX, currentY + 1, currentZ + i].team == team) { break; }

                    }
                    
                }
                if (currentY - 1 >= 0)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX, currentY - 1, currentZ + i))
                    {
                        if (board[currentX, currentY - 1, currentZ + i] == null)
                        {
                            r.Add(new Vector3Int(currentX, currentY - 1, currentZ + i));
                        }
                        else if (board[currentX, currentY - 1, currentZ + i].team != team)
                        {
                            r.Add(new Vector3Int(currentX, currentY - 1, currentZ + i));
                            break;

                        }
                        else if (board[currentX, currentY - 1, currentZ + i].team == team) { break; }
                    }
                    

                }

                if (ChessBoard.Instance.IsTileExist(currentX, currentY, currentZ + i))
                {
                    if (board[currentX, currentY, currentZ + i] == null)
                    {
                        r.Add(new Vector3Int(currentX, currentY, currentZ + i));
                    }
                    else if (board[currentX, currentY, currentZ + i].team != team)
                    {
                        r.Add(new Vector3Int(currentX, currentY, currentZ + i));
                        break;

                    }
                    else if (board[currentX, currentY, currentZ + i].team == team) { break; }
                }
                else
                {
                    break;
                }

            }


        }

        return r;
    }
    public List<Vector3Int> AvailableTopLeft(ref ChessPiece[,,] board,int x,int  y,int z,int runTime)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        
        for (int i = 1; i <= runTime ; i++)
        {


            if (currentZ + i <= z && currentX + i <= x)
            {

                if (currentY - 1 >= 0)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX + i, currentY - 1, currentZ + i) && ChessBoard.Instance.tiles[currentX + i, currentZ + i, currentY - 1].GetComponent<Tag>().direction == DirectionalSlope.noSlope)
                    {
                        if (board[currentX + i, currentY - 1, currentZ + i] == null )
                        {
                            r.Add(new Vector3Int(currentX + i, currentY - 1, currentZ + i));
                        }
                        else if (board[currentX + i, currentY - 1, currentZ + i].team != team)
                        {
                            r.Add(new Vector3Int(currentX + i, currentY - 1, currentZ + i));
                            break;

                        }
                        else if (board[currentX + i, currentY - 1, currentZ + i].team == team) { break; }

                    }
                    else
                    {
                        break;
                    }
                    
                    

                }
                

                if (ChessBoard.Instance.IsTileExist(currentX + i, currentY, currentZ + i) && ChessBoard.Instance.tiles[currentX + i, currentZ + i, currentY].GetComponent<Tag>().direction == DirectionalSlope.noSlope)
                {
                    if (board[currentX + i, currentY, currentZ + i] == null )
                    {
                        r.Add(new Vector3Int(currentX + i, currentY, currentZ + i));
                    }
                    else if (board[currentX + i, currentY, currentZ + i].team != team )
                    {
                        r.Add(new Vector3Int(currentX + i, currentY, currentZ + i));
                        break;

                    }
                    else if (board[currentX + i, currentY, currentZ + i].team == team) { break; }


                }
                else
                {
                    break;
                }





            }
        }

        return r;
    }
    public List<Vector3Int> AvailableTopRight(ref ChessPiece[,,] board,int x,int  y,int z,int runTime)
    {
        List<Vector3Int> r = new List<Vector3Int>();
       

        for (int i = 1; i <= runTime; i++)
        {
            if (currentZ - i >= 0 && currentX + i <= x)
            {

                if (currentY - 1 >= 0)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX + i, currentY - 1, currentZ - i) && ChessBoard.Instance.tiles[currentX + i, currentZ - i, currentY - 1].GetComponent<Tag>().direction == DirectionalSlope.noSlope)
                    {
                        if (board[currentX + i, currentY - 1, currentZ - i] == null)
                        {
                            r.Add(new Vector3Int(currentX + i, currentY - 1, currentZ - i));
                        }
                        else if (board[currentX + i, currentY - 1, currentZ - i].team != team )
                        {
                            r.Add(new Vector3Int(currentX + i, currentY - 1, currentZ - i));
                            break;

                        }
                        else if (board[currentX + i, currentY - 1, currentZ - i].team == team) { break; }


                    }
                    else { break; }


                }
                

                if (ChessBoard.Instance.IsTileExist(currentX + i, currentY, currentZ - i) && ChessBoard.Instance.tiles[currentX + i, currentZ - i, currentY].GetComponent<Tag>().direction == DirectionalSlope.noSlope)
                {
                    if (board[currentX + i, currentY, currentZ - i] == null )
                    {
                        r.Add(new Vector3Int(currentX + i, currentY, currentZ - i));
                    }
                    else if (board[currentX + i, currentY, currentZ - i].team != team )
                    {
                        r.Add(new Vector3Int(currentX + i, currentY, currentZ - i));
                        break;

                    }
                    else if (board[currentX + i, currentY, currentZ - i].team == team) { break; }


                }
                else
                {
                    break;
                }



            }

        }

        return r;
    }
    public List<Vector3Int> AvailableBotRight(ref ChessPiece[,,] board,int x,int  y,int z,int runTime)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        

        for (int i=1;i <= runTime; i++) {
            if (currentZ - i >= 0 && currentX - i >= 0)
            {

                if (currentY - 1 >= 0)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX - i, currentY - 1, currentZ - i) && ChessBoard.Instance.tiles[currentX - i, currentZ - i, currentY - 1].GetComponent<Tag>().direction == DirectionalSlope.noSlope)
                    {
                        if (board[currentX - i, currentY - 1, currentZ - i] == null)
                        {
                            r.Add(new Vector3Int(currentX - i, currentY - 1, currentZ - i));
                        }
                        else if (board[currentX - i, currentY - 1, currentZ - i].team != team )
                        {
                            r.Add(new Vector3Int(currentX - i, currentY - 1, currentZ - i));
                            break;

                        }
                        else if (board[currentX - i, currentY - 1, currentZ - i].team == team) { break; }
                    }
                    else
                    {
                        break;
                    }

                }
                

                if (ChessBoard.Instance.IsTileExist(currentX - i, currentY, currentZ - i) && ChessBoard.Instance.tiles[currentX - i, currentZ - i, currentY].GetComponent<Tag>().direction == DirectionalSlope.noSlope)
                {
                    if (board[currentX - i, currentY, currentZ - i] == null)
                    {
                        r.Add(new Vector3Int(currentX - i, currentY, currentZ - i));
                    }
                    else if (board[currentX - i, currentY, currentZ - i].team != team)
                    {
                        r.Add(new Vector3Int(currentX - i, currentY, currentZ - i));
                        break;

                    }
                    else if (board[currentX - i, currentY, currentZ - i].team == team) { break; }
                }
                else
                {
                    break;
                }



            }
        }

        return r;
    }
    public List<Vector3Int> AvailableBotLeft(ref ChessPiece[,,] board,int x,int  y,int z,int runTime)
    {
        List<Vector3Int> r = new List<Vector3Int>();
        

        for (int i=1;i <= runTime; i++) {
            if (currentZ + i <= z && currentX - i >= 0)
            {

                if (currentY - 1 >= 0)
                {
                    if (ChessBoard.Instance.IsTileExist(currentX - i, currentY - 1, currentZ + i) && ChessBoard.Instance.tiles[currentX - i, currentZ + i, currentY - 1].GetComponent<Tag>().direction == DirectionalSlope.noSlope)
                    {
                        if (board[currentX - i, currentY - 1, currentZ + i] == null)
                        {
                            r.Add(new Vector3Int(currentX - i, currentY - 1, currentZ + i));
                        }
                        else if (board[currentX - i, currentY - 1, currentZ + i].team != team )
                        {
                            r.Add(new Vector3Int(currentX - i, currentY - 1, currentZ + i));
                            break;
                        }
                        else if (board[currentX - i, currentY - 1, currentZ + i].team == team) { break; }
                    }
                    else
                    {
                        break;
                    }

                }
                

                if (ChessBoard.Instance.IsTileExist(currentX - i, currentY, currentZ + i) && ChessBoard.Instance.tiles[currentX - i, currentZ + i, currentY].GetComponent<Tag>().direction == DirectionalSlope.noSlope)
                {
                    if (board[currentX - i, currentY, currentZ + i] == null )
                    {
                        r.Add(new Vector3Int(currentX - i, currentY, currentZ + i));
                    }
                    else if (board[currentX - i, currentY, currentZ + i].team != team )
                    {
                        r.Add(new Vector3Int(currentX - i, currentY, currentZ + i));
                        break;

                    }
                    else if (board[currentX - i, currentY, currentZ + i].team == team) { break; }
                }
                else
                {
                    break;
                }


            }
        }

        return r;
    }
}
