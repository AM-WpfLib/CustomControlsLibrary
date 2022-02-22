using CustomControlTestShow.Common;
using System;
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
            IsOnlyCreatedCircuitVisible = true;
            IsHorizontalFlowActive = false;
            IsHorizontalFlowBrushBlue = true;
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

        private bool _isHorizontalFlowActive;
        public bool IsHorizontalFlowActive
        {
            get { return _isHorizontalFlowActive; }
            set { SetField(ref _isHorizontalFlowActive, value); }
        }

        private bool? _isFlowDirectionInverted;
        public bool? IsFlowDirectionInverted
        {
            get { return _isFlowDirectionInverted; }
            set { SetField(ref _isFlowDirectionInverted, value); }
        }


        private bool _isHorizontalFlowBrushBlue;
        public bool IsHorizontalFlowBrushBlue
        {
            get { return _isHorizontalFlowBrushBlue; }
            set { SetField(ref _isHorizontalFlowBrushBlue, value); }
        }
        #endregion

    }
}
