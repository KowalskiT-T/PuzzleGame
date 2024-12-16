
using Grid;
using PuzzleData;
using System.Collections.Generic;

public class PuzzleSavingDataBuilder
{
    private int _puzzleID;
    private GridSO _puzzleGrid;
    private List<PieceSavingData> _puzzlePieces;
    private List<PieceGroupsSavingData> _puzzlePiecesGroups;
    private bool _puzzleRotation;
    private int _puzzleSeed;

    private PuzzleSavingDataBuilder()
    {

    }

    public static PuzzleSavingDataBuilder Empty() => new();

    public PuzzleSavingDataBuilder WithPuzzleID(int id)
    {
        _puzzleID = id;

        return this;
    }

    public PuzzleSavingDataBuilder WithGrid(GridSO grid)
    {
        _puzzleGrid = grid;

        return this;
    }

    public PuzzleSavingDataBuilder WithPieces(List<PieceSavingData> pieces)
    {
        _puzzlePieces = pieces;

        return this;
    }

    public PuzzleSavingDataBuilder WithPiecesGroups(List<PieceGroupsSavingData> piecesGroups)
    {
        _puzzlePiecesGroups = piecesGroups;

        return this;
    }

    public PuzzleSavingDataBuilder WithRotation(bool rotation)
    {
        _puzzleRotation = rotation;

        return this;
    }

    public PuzzleSavingDataBuilder WithSeed(int seed)
    {
        _puzzleSeed = seed;

        return this;
    }

    public PuzzleSavingData Build()
    {
        return new PuzzleSavingData
        {
            ID = _puzzleID,
            Grid = _puzzleGrid,
            Pieces = _puzzlePieces,
            PiecesGroups = _puzzlePiecesGroups,
            RotationEnabled = _puzzleRotation,
            LevelSeed = _puzzleSeed
        };
    }
}
