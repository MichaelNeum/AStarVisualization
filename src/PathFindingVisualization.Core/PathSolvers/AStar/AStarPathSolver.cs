﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;
using PathFindingVisualization.DataStructures;

// TODO: do you actually need ref for passing the map
// TODO: measure performance, if necessary replace linq for more efficient methods

namespace PathFindingVisualization.Core.PathSolvers.AStar
{
    public class AStarPathSolver : IPathSolver
    {
        public bool AlgorithmDone { get; private set; }
        public List<INode> Path { get; private set; }

        private bool _diagonalsEnabled;
        private AStarMap _map;

        private int _step = 0;
        private MinPriorityQueue<double, AStarNode> _openSet;
        private HashSet<AStarNode> _closedSet;
        private AStarNode _startNode;
        private AStarNode _goalNode;
        private AStarNode _currentNode;

        public AStarPathSolver(ref IMap map, bool diagonalsEnabled)
        {
            _map = (AStarMap)map;
            _diagonalsEnabled = diagonalsEnabled;
        }

        public async Task PerformAlgorithmStep()
        {
            switch (_step)
            {
                case 0:
                    await Task.Run(PerformFirstStep);
                    break;
                default:
                    await Task.Run(PerformStep);
                    break;
            }

            _step++;
        }

        private void PerformFirstStep()
        {
            SetUpDataStructures(_map);
            ComputeHeuristicCosts(_map);

            (INode startNode, INode goalNode) = _map.GetStartAndGoal();
            _startNode = new AStarNode(startNode);
            _goalNode = new AStarNode(goalNode);

            _currentNode = _startNode;
            _currentNode.MovementCost = 0;
            _openSet.Add(_currentNode.TotalCost, _currentNode);
        }
        private void PerformStep()
        {
            if (_openSet.Count == 0 || _currentNode == _goalNode)
            {
                StopAlgorithm();
                return;
            }

            _currentNode = _openSet.Pop().Value;

            // get the neighbors
            IEnumerable<INode> neighbors = _map.GetNeighbors(_currentNode.RowIndex, _currentNode.ColIndex, _diagonalsEnabled);
            // get the successors out of the neighbors (already visited, wall, similar,...)
            List<AStarNode> successors = neighbors
                .Select(n => (AStarNode)n)
                .Where(n => (n.State == NodeState.Ground) || (n.State == NodeState.Goal))
                .ToList<AStarNode>();
            // set the movementcost of all the successors
            successors.ForEach(n => SetSuccessorMovementCost(_currentNode, n));
            // add all of the successors to the _openSet
            foreach (var successor in successors)
            {
                successor.Parent = _currentNode;
                if (successor.State != NodeState.Goal)
                    successor.State = NodeState.GroundToBeVisited;

                _openSet.Add(successor.TotalCost, successor);
            }

            // add the currentnode to the closedSet
            if (_currentNode.State != NodeState.Goal && _currentNode.State != NodeState.Start)
                _currentNode.State = NodeState.GroundVisited;
            _closedSet.Add(_currentNode);
        }

        private void SetUpDataStructures(AStarMap map)
        {
            int numNodes = map.GetLength(0) * map.GetLength(1);
            _openSet = new MinPriorityQueue<double, AStarNode>(numNodes);
            _closedSet = new HashSet<AStarNode>();
        }
        private void ComputeHeuristicCosts(AStarMap map, double D = 1000.0) // TODO: do something with D
        {
            (int goalRowIdx, int goalColIdx) = GetGoalIndices();

            foreach (AStarNode[] nodes in map)
                foreach (AStarNode node in nodes)
                {
                    int rowIdx = node.RowIndex;
                    int colIdx = node.ColIndex;
                    // this particular heuristic is the Manhattan distance which is used for grid layouts
                    node.Heuristic = D * (Math.Abs(rowIdx - goalRowIdx) + Math.Abs(colIdx - goalColIdx));
                }

            (int, int) GetGoalIndices()
            {
                int rowIdx = -1;
                int colIdx = -1;

                for (int i = 0; i < map.GetLength(0); i++)
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        var node = (AStarNode)map[i, j];
                        if (node.State == NodeState.Goal)
                        {
                            rowIdx = i;
                            colIdx = j;
                        }
                    }

                return (rowIdx, colIdx);
            }
        }
        private void SetSuccessorMovementCost(AStarNode current, AStarNode successor)
        {
            int dx = _currentNode.ColIndex - successor.ColIndex;
            int dy = _currentNode.RowIndex - successor.RowIndex;

            successor.MovementCost = Math.Sqrt(dx * dx + dy * dy);
        }
        private void StopAlgorithm()
        {
            AlgorithmDone = true;
        }
    }
}