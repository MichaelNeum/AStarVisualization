﻿namespace PathFindingVisualization.Core.PathSolvers
{
    public class PathSolverFactory : IPathSolverFactory
    {
        public IPathSolver GetPathSolver(ref Map.Map map, PathSolver pathSolverType, bool diagonalsEnabled)
        {
            return pathSolverType switch
            {
                PathSolver.AStar => new AStarPathSolver(ref map, diagonalsEnabled),
                PathSolver.Dijkstra => throw new System.NotImplementedException(),
                PathSolver.BestFirstSearch => throw new System.NotImplementedException(),
                PathSolver.BreadthFirstSearch => throw new System.NotImplementedException(),
                PathSolver.JumpPointSearch => throw new System.NotImplementedException(),
                PathSolver.OrthogonalJumpPointSearch => throw new System.NotImplementedException(),

                _ => throw new System.Exception($"The requested Pathsolving algorithm ({pathSolverType}) is not implemented")
            };
        }
    }
}
