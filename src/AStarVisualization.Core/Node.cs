﻿using System;

namespace AStarVisualization.Core
{
    public class Node : IEquatable<Node>
    {
        public double Heuristic { get; set; }
        public double MovementCost { get; set; }
        public NodeState State { get; set; }
        public Node Parent { get; set; }
        public bool IsWalkable => State != NodeState.Wall;
        public bool AlreadyVisited => (State != NodeState.Ground) || (State != NodeState.Goal);
        public double TotalCost => Heuristic + MovementCost; // TODO: 500 used to be here
        public int RowIndex => _rowIndex;
        public int ColIndex => _colIndex;

        private int _rowIndex;
        private int _colIndex;

        public Node(NodeState state, Node parentNode = null)
        {
            this.State = state;
        }

        public void SetIndices(int rowIndex, int colIndex)
        {
            this._rowIndex = rowIndex;
            this._colIndex = colIndex;
        }

        // TODO: follow the tutorial at https://medium.com/@pjbgf/asserting-equality-in-your-c-unit-tests-837b423024bf
        public override bool Equals(object other)
        {
            if (other.GetType() != this.GetType()) return false;
            var node = (Node)other;
            if (node is null) return false;
            if (ReferenceEquals(this, node)) return true;

            return (this.RowIndex == node.RowIndex) && (this.ColIndex == node.ColIndex);
        }
        public bool Equals(Node other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return (this.RowIndex == other.RowIndex) && (this.ColIndex == other.ColIndex);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (RowIndex.ToString() + ColIndex.ToString()).GetHashCode();
            }
        }

    }
}
