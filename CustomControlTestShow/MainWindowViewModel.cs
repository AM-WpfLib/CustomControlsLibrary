using CustomControlTestShow.Common;
using CustomControlTestShow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CustomControlTestShow
{
    public class MainWindowViewModel : PropertyChangedNotifier
    {
        public MainWindowViewModel()
        {
            //Commands
            ExampleCommand = new DelegateCommand(ExecuteExample, CanExecuteExample);
            IsFlowDirectionInverted = null;

            ItemsControlTestModels = new List<ItemsControlTestModel>()
            {
                new ItemsControlTestModel("Sample Name 1"),
                new ItemsControlTestModel("Sample Name 2"),
                new ItemsControlTestModel("Sample Name 3"),
                new ItemsControlTestModel("Sample Name 4"),
                new ItemsControlTestModel("Sample Name 5"),
            };
        }

        #region Ok command

        public DelegateCommand ExampleCommand { get; }

        private bool CanExecuteExample(object obj)
        {
            return true;
        }

        private void ExecuteExample(object obj)
        {
            // Functionality
        }

        #endregion

        #region Properties

        private bool _isOnlyImageVisible;
        public bool IsOnlyImageVisible
        {
            get { return _isOnlyImageVisible; }
            set { SetField(ref _isOnlyImageVisible, value); }
        }

        private bool _isOnlyCreatedCircuitVisible;
        public bool IsOnlyCreatedCircuitVisible
        {
            get { return _isOnlyCreatedCircuitVisible; }
            set { SetField(ref _isOnlyCreatedCircuitVisible, value); }
        }

        private bool _isImageOverlaidWithCreatedCircuit;
        public bool IsImageOverlaidWithCreatedCircuit
        {
            get { return _isImageOverlaidWithCreatedCircuit; }
            set { SetField(ref _isImageOverlaidWithCreatedCircuit, value); }
        }
        
        private bool? _isFlowDirectionInverted;
        public bool? IsFlowDirectionInverted
        {
            get { return _isFlowDirectionInverted; }
            set { SetField(ref _isFlowDirectionInverted, value); }
        }

        private List<ItemsControlTestModel> _itemsControlTestModels;
        public List<ItemsControlTestModel> ItemsControlTestModels
        {
            get { return _itemsControlTestModels; }
            set { SetField(ref _itemsControlTestModels, value); }
        }

        #endregion

    }
}
