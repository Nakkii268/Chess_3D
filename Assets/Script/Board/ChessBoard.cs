using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ChessBoard : Singleton<ChessBoard> 
{

    
    [Header("MiniMax")]
    [SerializeField] private MiniMax minimax = new MiniMax();
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private GameObject[] tileList;
    

    [Header("Prefab & team meterial")]
    [field: SerializeField] private GameObject[] prefabs;
    [field: SerializeField] private Transform tileHolder;
    [field: SerializeField] private Material[] teamMaterials;



    private ChessPiece[,,] chessPieces;
    [SerializeField] private ChessPiece currentSelected;
    [SerializeField] private List<Vector3Int> availableMoves = new List<Vector3Int>();
    [SerializeField] private List<ChessPieceData> pieceData = new List<ChessPieceData>();
    



  
    private  int TILE_COUNT_X = 11;
    private  int TILE_COUNT_Y = 11;
    private  int TILE_COUNT_Z = 2;
    [SerializeField]private float step = 0;
    [SerializeField] public GameObject[,,] tiles;
    private Camera currentCamera;
    private Vector3Int currentHover;

    [SerializeField]private bool isWhiteTurn;
    public MapSO map;
    public event EventHandler<EndGameData> IsWinningGame;
    public event EventHandler<float> OnStepReduce;

    private AsyncOperationHandle<GameObject> MapLoadOpHandler;
    

    private void Start() 
    {
        

        tileList = GetChild(tileHolder.gameObject);

        TILE_COUNT_X = map.MapSize.x;//x
        TILE_COUNT_Y = map.MapSize.z;//z
        TILE_COUNT_Z = map.MapSize.y;//y height
        step = map.stepLimit;

        isWhiteTurn = true;



        TilesSetup(TILE_COUNT_X, TILE_COUNT_Z, TILE_COUNT_Y);//format when setup x,z,y
        pieceData = map.pieceData;
        SpawnCertainChessPiece(pieceData);
    }

    private void Update()
    {
        if (!currentCamera)
        {
            currentCamera = Camera.main;
            return;
        }

        RaycastHit info;

        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Tiles", "Hover", "Highlight")))
        {

            Vector3Int hitPos = LookupTileIndex(info.transform.gameObject);
            //fist time hovering a tile
            if (currentHover == -Vector3Int.one)
            {
                currentHover = hitPos;

                tiles[hitPos.x, hitPos.y, hitPos.z].layer = LayerMask.NameToLayer("Hover");//
            }
            //already hovering a tile before
            if (currentHover != hitPos)
            {
                tiles[currentHover.x, currentHover.y, currentHover.z].layer =ContainsValidMoveHightLight(ref availableMoves,currentHover)? LayerMask.NameToLayer("Highlight") : LayerMask.NameToLayer("Tiles");
                currentHover = hitPos;

                tiles[hitPos.x, hitPos.y, hitPos.z].layer = LayerMask.NameToLayer("Hover");
            }
            if (currentSelected == null && Input.GetMouseButtonDown(0))
            {

                if (chessPieces[hitPos.x, hitPos.z, hitPos.y] != null)//
                {

                    if ((chessPieces[hitPos.x, hitPos.z, hitPos.y].team == 0 && isWhiteTurn ) )//|| (chessPieces[hitPos.x, hitPos.z, hitPos.y].team == 1 && !isWhiteTurn
                    { 

                        currentSelected = chessPieces[hitPos.x, hitPos.z, hitPos.y];

                        availableMoves = currentSelected.GetAvailableMoves(ref chessPieces, TILE_COUNT_X, TILE_COUNT_Y, TILE_COUNT_Z);
                        HighlightTile();
                    }
                }
            }
            if (currentSelected != null && Input.GetMouseButtonDown(1))
            {
                bool validMove = MoveTo(currentSelected, hitPos.x, hitPos.z, hitPos.y);
                if (!validMove)
                {

                    currentSelected = null;
                    RemoveHighlightTile();
                }
                else
                {
                    if (tiles[hitPos.x, hitPos.y, hitPos.z].CompareTag("Slope"))
                    {
                        currentSelected.SetPosition(GetSlopeTileStandPos(hitPos.x, hitPos.z, hitPos.y), true);
                        if (tiles[hitPos.x, hitPos.y, hitPos.z].GetComponent<Tag>().direction == DirectionalSlope.top)
                        {
                            currentSelected.SetRotation(new Vector3(0, 0, 45));

                        }
                        if (tiles[hitPos.x, hitPos.y, hitPos.z].GetComponent<Tag>().direction == DirectionalSlope.bottom)
                        {
                            currentSelected.SetRotation(new Vector3(0, 0, -45));

                        }
                        if (tiles[hitPos.x, hitPos.y, hitPos.z].GetComponent<Tag>().direction == DirectionalSlope.left)
                        {
                            currentSelected.SetRotation(new Vector3(-45, 0, 0));

                        }
                        if (tiles[hitPos.x, hitPos.y, hitPos.z].GetComponent<Tag>().direction == DirectionalSlope.right)
                        {
                            currentSelected.SetRotation(new Vector3(45, 0, 0));

                        }
                    }
                    else
                    {

                        currentSelected.SetPosition(GetTileStandPos(hitPos.x, hitPos.z, hitPos.y), true);
                        currentSelected.SetRotation(Vector3.zero);

                    }


                    currentSelected = null;
                    RemoveHighlightTile();
                    StartCoroutine(Delayforsecond(1));
                    
                    
                    
                }

            }



        }
        else
        {
            if (currentHover == -Vector3Int.one)
            {
                tiles[currentHover.x, currentHover.y, currentHover.z].layer = LayerMask.NameToLayer("Tiles");
                currentHover = Vector3Int.one;
            }
        }


        if(step <= 0)
        {
            IsWinningGame?.Invoke(this, new EndGameData
            {
                winteam = 1,
                moveleft = step,
                MapID = map.MapID
            }) ;
        }
    }





    




    //spawn piece

    private void SpawnCertainChessPiece(List<ChessPieceData> data)
    {
        chessPieces = new ChessPiece[TILE_COUNT_X, TILE_COUNT_Y, TILE_COUNT_Z]; //need modify
        for (int i = 0; i < data.Count; i++)
        {
            chessPieces[data[i].x, data[i].y, data[i].z] = SpawnSinglePiece(data[i].type, data[i].team);
            PositionPiece(data[i].x, data[i].y, data[i].z);
            

        }

    }
    private ChessPiece SpawnSinglePiece(ChessPieceType type, int team)
    {
        ChessPiece piece = Instantiate(prefabs[(int)type - 1], transform).GetComponent<ChessPiece>();
        piece.type = type;
        piece.team = team;
        piece.GetComponent<MeshRenderer>().material = teamMaterials[team];

        return piece;
    }
    private void PositionPiece(int x, int y, int z)
    {
        chessPieces[x, y, z].currentX = x;
        chessPieces[x, y, z].currentY = y;
        chessPieces[x, y, z].currentZ = z;
        if (tiles[x, z, y].CompareTag("Slope")) {
            chessPieces[x, y, z].SetPosition(GetSlopeTileStandPos(x, y, z), true);
            if (tiles[x, z, y].GetComponent<Tag>().direction == DirectionalSlope.top)
            {
                chessPieces[x, y, z].SetRotation(new Vector3(0, 0, 45));

            }
            if (tiles[x, z, y].GetComponent<Tag>().direction == DirectionalSlope.bottom)
            {
                chessPieces[x, y, z].SetRotation(new Vector3(0, 0, -45));

            }
            if (tiles[x, z, y].GetComponent<Tag>().direction == DirectionalSlope.left)
            {
                chessPieces[x, y, z].SetRotation(new Vector3(-45, 0, 0));

            }
            if (tiles[x, z, y].GetComponent<Tag>().direction == DirectionalSlope.right)
            {
                chessPieces[x, y, z].SetRotation(new Vector3(45, 0, 0));

            }
        }
        else
        {

            chessPieces[x, y, z].SetPosition(GetTileStandPos(x, y, z), true);
            chessPieces[x, y, z].SetRotation(Vector3.zero);

        }
    }

    private Vector3 GetTileStandPos(int x, int y, int z)
    {

        return new Vector3(x * tileSize, y * tileSize, z * tileSize) + new Vector3(0, tileSize / 2, 0);
    }
    private Vector3 GetSlopeTileStandPos(int x, int y, int z)
    {


        return new Vector3(x * tileSize, y * tileSize, z * tileSize);

    }

    //Highlight tile
    private void HighlightTile()
    {
        for (int i = 0; i < availableMoves.Count; i++)
        {


            tiles[availableMoves[i].x, availableMoves[i].z, availableMoves[i].y].layer = LayerMask.NameToLayer("Highlight");

        }
    }
    private void RemoveHighlightTile()
    {
        for (int i = 0; i < availableMoves.Count; i++)
        {


            tiles[availableMoves[i].x, availableMoves[i].z, availableMoves[i].y].layer = LayerMask.NameToLayer("Tiles");




        }
        availableMoves.Clear();
    }



    //operation
    private Vector3Int LookupTileIndex(GameObject hitInfo)
    {
        for (int i = 0; i < TILE_COUNT_X; i++)
        {
            for (int j = 0; j < TILE_COUNT_Y; j++)
            {
                for (int k = 0; k < TILE_COUNT_Z; k++)
                {

                    if (tiles[i, k, j] == hitInfo) return new Vector3Int(i, k, j);
                }
            }
        }
        return -Vector3Int.one; //invalid value
    }
    private Vector3Int NameConvert(string name)//get name with the format x,y,z and convert to v3int
    {
        string[] nameSplit = name.Split(',');
        Vector3Int result = new Vector3Int(int.Parse(nameSplit[0]), int.Parse(nameSplit[1]), int.Parse(nameSplit[2]));

        return result;
    }

    private GameObject[] GetChild(GameObject gameObject)
    {
        GameObject[] childList = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            childList[i] = gameObject.transform.GetChild(i).gameObject;
        }
        return childList;
    }

    private void TilesSetup(int x, int y, int z)
    {
        tiles = new GameObject[x, y, z];
        for (int i = 0; i < tileList.Length; i++)
        {

            Vector3Int value = NameConvert(tileList[i].name);
            tiles[value.x, value.z, value.y] = tileList[i];
        }
    }



    private bool ContainsValidMove(ref List<Vector3Int> moves, Vector3 pos)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].x == pos.x && moves[i].y == pos.y && moves[i].z == pos.z)
            {
                return true;
            }

        }
        return false;
    }
    private bool ContainsValidMoveHightLight(ref List<Vector3Int> moves, Vector3 pos)
    {
        for (int i = 0; i < moves.Count; i++)
        {
            if (moves[i].x == pos.x && moves[i].z == pos.y && moves[i].y == pos.z)
            {
                return true;
            }

        }
        return false;
    }
    //move
    private bool MoveTo(ChessPiece cp, int x, int y, int z)
    {
        if (!ContainsValidMove(ref availableMoves, new Vector3(x, y, z))) return false;
        Vector3Int previousPos = new Vector3Int(cp.currentX, cp.currentY, cp.currentZ);
        if (chessPieces[x, y, z] != null)
        {
            ChessPiece ocp = chessPieces[x, y, z];
            if (cp.team == ocp.team) { return false; }
            else
            {
                ocp.SetScale(Vector3.zero, true);
                chessPieces[x, y, z] = null;

            }
            if (ocp.type == ChessPieceType.King) { //win
                IsWinningGame?.Invoke(this, new EndGameData
                {
                    winteam = 0,
                    moveleft = step,MapID = map.MapID
                }) ;
                return true;
            }
        }
        chessPieces[x, y, z] = cp;
        chessPieces[previousPos.x, previousPos.y, previousPos.z] = null;
        PositionPiece(x, y, z);
        isWhiteTurn = false;
        step--;
       
        OnStepReduce?.Invoke(this,(step / map.stepLimit));
        return true;
    }

    public bool IsTileExist(int x, int y, int z)
    {
        if (GameObject.Find(x + "," + y + "," + z) != null) return true;
        return false;

    }


    public List<ChessPiece> GetCurrentChessPiece()
    {
        List<ChessPiece> currentcp = new List<ChessPiece>();
        for (int i = 0; i < tileList.Length; i++) {
            Vector3Int value = NameConvert(tileList[i].name);
            if (chessPieces[value.x, value.y, value.z] != null)
            {
                currentcp.Add(chessPieces[value.x, value.y, value.z]);


            }

        }


        return currentcp;
    }
    public List<ChessPiece> GetCurrentChessPieceTeam(int team)
    {
        List<ChessPiece> teamcp = new List<ChessPiece>();
        for (int i = 0; i < tileList.Length; i++) {
            Vector3Int value = NameConvert(tileList[i].name);
            if (chessPieces[value.x, value.y, value.z] != null && chessPieces[value.x, value.y, value.z].team == team)
            {
                teamcp.Add(chessPieces[value.x, value.y, value.z]);


            }

        }

        return teamcp;


    }
    public List<Move> GetAvailbleMove(int team)
    {
        List<ChessPiece> teamcp = GetCurrentChessPieceTeam(team);
        List<Move> availableMove = new();

        foreach (var piece in teamcp)
        {
            List<Vector3Int> moves = piece.GetAvailableMoves(ref chessPieces, TILE_COUNT_X, TILE_COUNT_Y, TILE_COUNT_Z);//modify

            
            for (int j = 0; j < moves.Count; j++)
            {

               
                availableMove.Add(new Move(new Vector3Int(piece.currentX, piece.currentY, piece.currentZ), new Vector3Int(moves[j].x, moves[j].y, moves[j].z), chessPieces[moves[j].x, moves[j].y, moves[j].z]));
                
            }
            
         }
       
       
       
        return availableMove;
    } 
    public int GetAvailableMoveCount(ChessPiece piece)
    {
        return piece.GetAvailableMoves(ref chessPieces, TILE_COUNT_X, TILE_COUNT_Z, TILE_COUNT_Y).Count;
    }
    
    
    public void MakeMove(Move move)
    {
        move.capturedPiece = chessPieces[move.destination.x, move.destination.y, move.destination.z];
        chessPieces[move.destination.x,move.destination.y,move.destination.z ] = chessPieces[move.source.x,move.source.y,move.source.z ];
        chessPieces[move.source.x, move.source.y, move.source.z] = null;
    }
    public void UndoMove(Move move)
    {
        chessPieces[move.source.x, move.source.y, move.source.z] = chessPieces[move.destination.x, move.destination.y, move.destination.z];
        chessPieces[move.destination.x, move.destination.y, move.destination.z] = move.capturedPiece;
    }
    public void BotMove(Vector3Int source , Vector3Int destination)
    {
        
            ChessPiece ocp = chessPieces[destination.x, destination.y, destination.z];
           if(ocp != null)
           {
            ocp.SetScale(Vector3.zero, true);
            chessPieces[destination.x, destination.y, destination.z] = null;
            if (ocp.team == 0 && ocp.type == ChessPieceType.King)
            {
                IsWinningGame?.Invoke(this, new EndGameData
                {
                    winteam = 1,
                    moveleft = step,
                    MapID = map.MapID

                }) ;
            }
        }
           
                

            
        
        chessPieces[destination.x, destination.y, destination.z] = chessPieces[source.x, source.y, source.z] ;
        chessPieces[source.x, source.y, source.z] = null;
        PositionPiece(destination.x, destination.y, destination.z);
        isWhiteTurn = true;
        
      
    }
    public Vector3Int GetBoardSize()
    {
        return new Vector3Int(TILE_COUNT_X, TILE_COUNT_Z, TILE_COUNT_Y);
    }
    IEnumerator Delayforsecond(float time)
    {
        yield return new WaitForSeconds(time);
        Move botmove = minimax.GetBestMove(this,2);
       
        BotMove(botmove.source, botmove.destination);
        
    }
}
public class EndGameData
{
    public int winteam;
    public float moveleft;
    public int MapID;
}