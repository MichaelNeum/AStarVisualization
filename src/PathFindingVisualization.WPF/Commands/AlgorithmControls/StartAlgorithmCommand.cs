﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using PathFindingVisualization.Core.Map;
using PathFindingVisualization.Core.PathSolvers;
using PathFindingVisualization.WPF.Models;
using PathFindingVisualization.WPF.ViewModels;

// TODO: clean up this command
namespace PathFindingVisualization.WPF.Commands.AlgorithmControls
{
    public class StartAlgorithmCommand : ICommand
    {
        private ApplicationState _appState;
        private PathSolverController _pathSolverController;
        private MainViewModel _mainViewModel;
        private AlgorithmControlViewModel _algorithmControlViewModel;

        public StartAlgorithmCommand(ApplicationState appState, PathSolverController pathSolverController, MainViewModel mainViewModel, AlgorithmControlViewModel algorithmControlViewModel)
        {
            _appState = appState;
            _pathSolverController = pathSolverController;
            _mainViewModel = mainViewModel;
            _algorithmControlViewModel = algorithmControlViewModel;
            _appState.PropertyChanged += UpdateCanExecute;
        }

        public bool CanExecute(object parameter) => _appState.State == AppState.MapDesignPhase;
        public void Execute(object parameter)
        {
            _appState.State = AppState.AlgorithmActive;

            Map map = _mainViewModel.Map;
            bool diagonalsEnabled = _algorithmControlViewModel.DiagonalPathsEnabled;
            PathSolver pathsolverType = _algorithmControlViewModel.PathSolverType;

            _pathSolverController.StartPathSolver(map, pathsolverType, diagonalsEnabled);
        }

        private void UpdateCanExecute(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }
        public event EventHandler CanExecuteChanged;
    }
}
