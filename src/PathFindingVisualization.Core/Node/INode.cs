﻿using System;

namespace PathFindingVisualization.Core.Node
{
    public interface INode : IEquatable<INode>
    {
        NodeState State { get; set; }
        INode Parent { get; set; }
        bool IsWalkable { get; }
        int RowIndex { get; }
        int ColIndex { get; }

        void SetIndices(int rowIndex, int colIndex);

        event EventHandler<(int rowIndex, int colIdndex)> StateChanged;
    }
}
