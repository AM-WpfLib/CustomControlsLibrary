using CustomControls.Elements.Interfaces;
using CustomControls.Elements.Models;
using CustomControlTestShow.Common;
using System.Collections.Generic;

namespace CustomControlTestShow
{
    public class MainWindowViewModel : PropertyChangedNotifier
    {
        public MainWindowViewModel()
        {
            //Commands
            ExampleCommand = new DelegateCommand(ExecuteExample, CanExecuteExample);
            IsFlowDirectionInverted = null;

            ItemsControlTestModels = new List<IItemsPresenterModel>()
            {
                new ItemsPresenterModel(0, "Sample Name 1", 32.0),
                new ItemsPresenterModel(1, "Sample Name 2", 32.0),
                new ItemsPresenterModel(2, "Sample Name 3", 32.0),
                new ItemsPresenterModel(3, "Sample Name 4", 32.0),
                new ItemsPresenterModel(4, "Sample Name 5", 32.0),
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

        private List<IItemsPresenterModel> _itemsControlTestModels;
        public List<IItemsPresenterModel> ItemsControlTestModels
        {
            get { return _itemsControlTestModels; }
            set { SetField(ref _itemsControlTestModels, value); }
        }

        #endregion

    }
}
