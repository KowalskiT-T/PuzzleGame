using System.Collections.Generic;
using UnityEngine;

namespace PuzzlePiece
{
    public class PuzzleGroup : MonoBehaviour, ISnappable
    {
        private const string PUZZLE_GROUP = "PuzzleGroup";
        private Draggable _draggable;
        private List<Piece> _pieces = new List<Piece>();
        public Transform Transform => transform;
        public List<Piece> Pieces => _pieces;


        public void InitializeGroup(List<Piece> pieces)
        {
            List<Piece> piecesToAdd = pieces;

            foreach (Piece piece in piecesToAdd)
            {
                AddPieceToGroup(piece);
            }

            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;

            CompositeCollider2D compositeCollider = gameObject.AddComponent<CompositeCollider2D>();
            compositeCollider.geometryType = CompositeCollider2D.GeometryType.Polygons;
            compositeCollider.generationType = CompositeCollider2D.GenerationType.Synchronous;

            _draggable = gameObject.AddComponent<Draggable>();
        }

        public static PuzzleGroup CreateGroup(List<Piece> pieces)
        {
            Vector3 centerPoint = Vector3.zero;
            foreach (Piece piece in pieces)
            {
                centerPoint += piece.transform.position;
            }
            centerPoint /= pieces.Count;

            Transform parent = pieces[0].transform.parent;

            GameObject groupObject = new GameObject(PUZZLE_GROUP);
            groupObject.transform.parent = parent;
            groupObject.transform.position = centerPoint;

            PuzzleGroup group = groupObject.AddComponent<PuzzleGroup>();
            group.InitializeGroup(pieces);

            return group;
        }
   
        public bool TrySnapToGrid()
        {
            bool snap = false;

            foreach (Piece piece in _pieces)
            {
                if (piece.TrySnapToGrid())
                {
                    snap = true;
                }
            }

            if (snap) Destroy(_draggable);
            
            return snap;
        }

        public ISnappable CombineWith(Piece otherPiece)
        {
            SnapGroupPosition(otherPiece);

            PuzzleGroup neighbourGroup = otherPiece.Group;

            if (neighbourGroup != null)
            {
                MergeGroup(neighbourGroup);
            }
            else
            {
                AddPieceToGroup(otherPiece);
            }

            return this;
        }

        public Piece GetNeighbourPiece()
        {
            foreach (Piece piece in _pieces)
            {
                List<Piece> neighbours = piece.GetNeighbours();

                foreach (Piece neighbour in neighbours)
                {
                    if (!IsTheSameGroup(neighbour.Group))
                    {
                        return neighbour;
                    }
                }
            }
            return null;
        }

        public void SnapGroupPosition(Piece referencePiece)
        {
            foreach (Piece piece in _pieces)
            {
                Vector3 distance = piece.CorrectPosition - referencePiece.CorrectPosition;
                piece.transform.position = referencePiece.transform.position + distance;
            }
        }

        public void AddPieceToGroup(Piece piece)
        {
            piece.SetGroup(this);

            piece.SetupForGroup();

            _pieces.Add(piece);
        }


        private bool IsTheSameGroup(PuzzleGroup group)
        {
            return group == this;
        } 

        public void MergeGroup(PuzzleGroup otherGroup)
        {
            List<Transform> children = new List<Transform>();

            foreach (Transform child in otherGroup.transform)
            {
                children.Add(child);
            }

            foreach (Transform child in children)
            {
                child.SetParent(transform, true);
            }

            foreach (Piece piece in otherGroup.Pieces)
            {
                piece.SetGroup(this);
                _pieces.Add(piece);
            }

            Destroy(otherGroup.gameObject);
        }

        public void UpdateZPosition(int zPosition)
        {
            Vector3 position = transform.position;
            position.z = zPosition;
            transform.position = position;

            foreach (Piece piece in _pieces)
            {
                piece.UpdateZPosition(zPosition);
            }
        }

    }
}