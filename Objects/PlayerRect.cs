using System.ComponentModel;
using JetBrains.Annotations;

namespace PongGame.Objects
{
    /// <summary>
    /// The player rect.
    /// </summary>
    internal class PlayerRect : INotifyPropertyChanged
    {
        private int _yPosition;

        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        public int YPosition
        {
            get
            {
                return _yPosition;
            }
            set
            {
                _yPosition = value;
                OnPropertyChanged("YPosition");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// On property change.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
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