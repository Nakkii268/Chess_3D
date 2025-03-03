using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMax 
{

    private const int pawnValue = 100;
    private const int knighValue = 320;
    private const int bishopValue = 330;
    private const int rookValue = 400;
    private const int queenValue = 900;
    private const int kingValue = 2000;
    private int Minimax(ref ChessBoard board,int depth,bool isMaximizing)
    {
       
        if(depth==0)
        {
            return EvaluateBoard(board);
        }
        if (isMaximizing)
        {
            int maxEval = int.MinValue; //let the start value is -infinity
            foreach (var move in board.GetAvailbleMove(1))
            {
                board.MakeMove(move);

                int eval = Minimax(ref board, depth - 1,false);
                board.UndoMove(move);

                maxEval = Math.Max(maxEval, eval);    
            }
            Debug.Log("max" + maxEval);

            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in board.GetAvailbleMove(0))
            {
                board.MakeMove(move);
                int eval = Minimax(ref board, depth - 1, true);
                board.UndoMove(move);
                minEval = Math.Min(minEval, eval);
                Debug.Log("min" + minEval);
            }

           // Debug.Log("max" + minEval + "depth" + depth);

            return minEval;
        }
    }

    public Move GetBestMove(ChessBoard board, int depth) {
        Move bestMove = null;
        int bestValue = int.MinValue;
            foreach(var move in board.GetAvailbleMove(1))
        {
            board.MakeMove(move);
            
            int moveValue = Minimax(ref board,depth - 1, false);
            board.UndoMove(move);
            if(moveValue > bestValue)
            {

                bestValue = moveValue;
                Debug.Log(bestValue);
                bestMove = move;

            }
        }
            Debug.Log("best" + bestMove);
            return bestMove;
    }


    private int EvaluateBoard(ChessBoard board)
    {
        int score = 0;
        List<ChessPiece> pieces = board.GetCurrentChessPiece();
        foreach (var piece in pieces)
        {
            if(piece != null)
            {
                if(piece.type == ChessPieceType.Pawn)
                {
                    if (piece.team ==0) { 
                        score -= (pawnValue)  ; 
                    }
                    else {
                            
                            score += pawnValue ;
                    }
                }
                if (piece.type == ChessPieceType.Rook)
                {
                    if (piece.team == 0) {
                        score -= (rookValue ) ; 
                    }
                    else{
                            

                        score += rookValue;
                    }
                }
                if (piece.type == ChessPieceType.Knight)
                {
                    if (piece.team == 0) {
                        score -= (knighValue )  ; 
                    }
                    else {
                            

                        score += knighValue  ; }
                }
                if (piece.type == ChessPieceType.Bishop)
                {
                    if (piece.team == 0) {
                        score -= bishopValue;
                    }
                    else
                    {
                            

                        score += bishopValue;
                    }   
                }
                if (piece.type == ChessPieceType.Queen)
                {
                    if (piece.team == 0) {
                        score -= (queenValue) ;
                    }
                    else
                    {
                           

                        score += queenValue +board.GetAvailableMoveCount(piece) * 50;
                    }
                }
                if (piece.type == ChessPieceType.King)
                {
                    if (piece.team == 0) {
                        score -=( kingValue +500) ;
                    }
                    else
                    {
                            

                        score += (kingValue + board.GetAvailableMoveCount(piece)*100) ;
                    }
                }
            }
        }
       
       

        return score;
        
    }
}
public class Move
{
   public Vector3Int source;
   public Vector3Int destination;
    public ChessPiece capturedPiece;

    public Move (Vector3Int source, Vector3Int destination,ChessPiece capturedPiece)
    {
        this.source = source;
        this.destination = destination;
        this.capturedPiece = capturedPiece;
    }
}
