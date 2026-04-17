using System.Collections.Generic;
using UnityEngine;

// Thank you to Soul's Game Dev Journey on youtube for their Hex Tile how-to videos
public static class BlobRules
{
    public static int GetMinDistanceToBlob(List<TileState> blobTiles, Vector2Int target)
    {
        int minDistance = int.MaxValue;

        foreach (var tile in blobTiles)
        {
            Vector2Int coord = new(tile.X, tile.Z);
            int distance = HexMath.Distance(coord, target);

            if (distance < minDistance)
                minDistance = distance;
        }

        return minDistance;
    }

    public static List<Vector2Int> GetCandidateJumpOffsets(List<TileState> blobTiles, Vector2Int target)
    {
        List<Vector2Int> offsets = new();

        foreach (var tile in blobTiles)
        {
            Vector2Int source = new(tile.X, tile.Z);

            if (HexMath.Distance(source, target) == 2)
            {
                Vector2Int offset = target - source;

                if (!offsets.Contains(offset))
                    offsets.Add(offset);
            }
        }

        return offsets;
    }

    public static bool CanJump(BoardState board, PlayerId player, Vector2Int offset)
    {
        List<TileState> blobTiles = board.GetPlayerTiles(player);
        HashSet<Vector2Int> currentPositions = new();
        HashSet<Vector2Int> destinationPositions = new();

        foreach (var tile in blobTiles)
        {
            currentPositions.Add(new Vector2Int(tile.X, tile.Z));
        }

        foreach (var tile in blobTiles)
        {
            Vector2Int destination = new(tile.X + offset.x, tile.Z + offset.y);

            if (!board.HasTile(destination.x, destination.y))
                return false;

            destinationPositions.Add(destination);
        }

        foreach (var destination in destinationPositions)
        {
            TileState tile = board.GetTile(destination.x, destination.y);

            if (tile == null)
                return false;

            if (tile.Owner != PlayerId.None && !currentPositions.Contains(destination))
                return false;
        }

        return true;
    }

    public static MoveDecision ClassifyMove(BoardState board, PlayerId player, Vector2Int target)
    {
        TileState targetTile = board.GetTile(target.x, target.y);

        if (targetTile == null || targetTile.Owner != PlayerId.None)
        {
            return new MoveDecision { MoveType = MoveType.Invalid, Target = target };
        }

        List<TileState> blob = board.GetPlayerTiles(player);
        int minDistance = GetMinDistanceToBlob(blob, target);

        if (minDistance == 1)
        {
            return new MoveDecision
            {
                MoveType = MoveType.Flip,
                Target = target
            };
        }

        if (minDistance == 2)
        {
            List<Vector2Int> offsets = GetCandidateJumpOffsets(blob, target);

            List<Vector2Int> validOffsets = new();

            foreach (var offset in offsets)
            {
                if (CanJump(board, player, offset))
                    validOffsets.Add(offset);
            }

            if (validOffsets.Count == 1)
            {
                return new MoveDecision
                {
                    MoveType = MoveType.Jump,
                    Target = target,
                    JumpOffset = validOffsets[0]
                };
            }
        }

        

        return new MoveDecision
        {
            MoveType = MoveType.Invalid,
            Target = target
        };
    }

    public static bool HasAnyValidMove(BoardState board, PlayerId player)
{
    foreach (var tile in board.AllTiles())
    {
        if (tile.Owner != PlayerId.None) continue;

        Vector2Int target = new(tile.X, tile.Z);
        MoveDecision decision = ClassifyMove(board, player, target);

        if (decision.MoveType != MoveType.Invalid)
            return true;
    }
    return false;
}
}