﻿using PathFindingVisualization.Core;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.WPF.ViewModels;
using Ninject;
using System.Reflection;
using System.Windows;

namespace PathFindingVisualization.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            // TODO: use xaml to inject the viewmodels instead
            var mapViewModel = new MapCanvasViewModel();
            mapCanvas.DataContext = mapViewModel;

            // TODO: remove mock data
            //var map = new Map(2, 3)
            //{
            //    Data = new Node[][]
            //    {
            //        new Node[] { new Node(NodeState.Wall), new Node(NodeState.Ground), new Node(NodeState.Wall), new Node(NodeState.Ground), new Node(NodeState.Ground) },
            //        new Node[] { new Node(NodeState.Start), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground) },
            //        new Node[] { new Node(NodeState.Wall), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground) },
            //        new Node[] { new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground) },
            //        new Node[] { new Node(NodeState.Wall), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Ground), new Node(NodeState.Goal) },
            //    }
            //};

            //mapViewModel.Map = map;
            //mapViewModel.Path = new List<Node>() { map[1, 0], map[1, 1], map[2, 1], map[3, 1], map[4, 1], map[4, 2] };

            //IPathSolver pathsolver = new AStarPathSolver(ref map, false);
            //pathsolver.FindPath();
        }
    }
}
