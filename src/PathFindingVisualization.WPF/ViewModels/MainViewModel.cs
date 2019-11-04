﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Ninject;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.Node;

namespace PathFindingVisualization.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        [Inject, Named("PlaceTileCommand")]
        public ICommand PlaceTileCommand { get; set; }
        [Inject, Named("RemoveTileCommand")]
        public ICommand RemoveTileCommand { get; set; }
        [Inject, Named("ProcessMouseMovementCommand")]
        public ICommand ProcessMouseMovementCommand { get; set; }
        [Inject, Named("ClearMapCommand")]
        public ICommand ClearMapCommand { get; set; }
        [Inject, Named("PlaceStartCommand")]
        public ICommand PlaceStartCommand { get; set; }
        [Inject, Named("PlaceGoalCommand")]
        public ICommand PlaceGoalCommand { get; set; }

        private IEnumerable<ICommand> _commands;

        public Map Map
        {
            get => _map;
            set
            {
                if (_map == value)
                    return;
                _map = value;
                OnPropertyChanged("Map");
            }
        }
        private Map _map = new Map();
        public List<Node> Path
        {
            get => _path;
            set
            {
                if (_path == value)
                    return;
                _path = value;
                OnPropertyChanged("Path");
            }
        }
        private List<Node> _path = new List<Node>();

        // the commands that are dependent on this variable do not update when it changes
        public bool MapDesignPhaseActive
        {
            get => _mapDesignPhaseActive;
            set
            {
                if (_mapDesignPhaseActive == value)
                    return;
                _mapDesignPhaseActive = value;
                OnPropertyChanged("MapDesignPhaseActive");

                CommandManager.InvalidateRequerySuggested();
                //foreach (ICommand command in _commands)
                //command.CanExecute(null);
            }
        }
        private bool _mapDesignPhaseActive = true;
        public NodeState PlacementMode
        {
            get => _placementMode;
            set
            {
                if (_placementMode == value)
                    return;
                _placementMode = value;
                OnPropertyChanged("StartPlacementActive");
                OnPropertyChanged("GoalPlacementActive");
            }
        }
        private NodeState _placementMode = NodeState.Wall;

        // TODO: handle these pyoperties differently
        public bool StartPlacementActive => (PlacementMode == NodeState.Start);
        public bool GoalPlacementActive => (PlacementMode == NodeState.Goal);
        public Node Start = null;
        public Node Goal = null;

        public MainViewModel(IEnumerable<ICommand> commands)
        {
            _commands = commands;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
