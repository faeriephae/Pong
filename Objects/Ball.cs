using System.ComponentModel;
using JetBrains.Annotations;

namespace PongGame.Objects
{
    /// <summary>
    /// The ball.
    /// </summary>
    public class Ball : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private bool _movingRight;
        private int _leftResult;
        private int _rightResult;

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                OnPropertyChanged("X");
            }
        }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                OnPropertyChanged("Y");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether moving is right.
        /// </summary>
        public bool IsMovingRight
        {
            get
            {
                return _movingRight;
            }
            set
            {
                _movingRight = value;
                OnPropertyChanged("MovingRight");
            }
        }

        /// <summary>
        /// Gets or sets the left result.
        /// </summary>
        public int LeftResult
        {
            get
            {
                return _leftResult;
            }
            set
            {
                _leftResult = value;
                OnPropertyChanged("LeftResult");
            }
        }

        /// <summary>
        /// Gets or sets the right result.
        /// </summary>
        public int RightResult
        {
            get
            {
                return _rightResult;
            }
            set
            {
                _rightResult = value;
                OnPropertyChanged("RightResult");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged( string propertyName )
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}