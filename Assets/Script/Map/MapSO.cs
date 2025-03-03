using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu()]
public class MapSO : ScriptableObject
{
    public int MapID;
   public List<ChessPieceData> pieceData;
    public GameObject Map;
    public Vector3Int MapSize;
    public float stepLimit;
    public Sprite lvSprite;
    public AssetReference MapAsset;

}
