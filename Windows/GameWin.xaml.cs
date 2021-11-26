using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;
using PongGame.Objects;

namespace PongGame.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWin : Window
    {
        public GameWin()
        {
            InitializeComponent();
            SetDataContext();
            StartTimer();
        }

        #region VARS

        private double _angle = 155;
        private double _speed = 5;
        private int _rectSpeed = 10;
        private int _angle180 = 180;
        private DispatcherTimer timer = new DispatcherTimer();

        #endregion VARS

        #region METH

        /// <summary>
        /// Sets the data context.
        /// </summary>
        private void SetDataContext()
        {
            try
            {
                rectP1.DataContext = _rectLeft;
                rectP2.DataContext = _rectRight;
                ball.DataContext = _ball;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        private void StartTimer()
        {
            try
            {
                timer.Interval = TimeSpan.FromMilliseconds(1);
                timer.Start();
                timer.Tick += Timer_Tick;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #region DEFAULT STUFF

        private Ball _ball = new Ball
        {
            X = 380, //canvas width / 2 - rectangle width
            Y = 210, //approx. canvas / 2 - rectable width
            IsMovingRight = true
        };

        private PlayerRect _rectLeft = new PlayerRect
        {
            YPosition = 200
        };

        private PlayerRect _rectRight = new PlayerRect
        {
            YPosition = 200
        };

        #endregion DEFAULT STUFF

        /// <summary>
        /// Timer tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Timer_Tick( object sender, EventArgs e )
        {
            try
            {
                double rectWidth = rectP1.Width;

                if (_rectLeft.YPosition <= 5)
                {
                    _rectLeft.YPosition = 5;
                } 
                
                if (_rectRight.YPosition <= 5)
                {
                    _rectRight.YPosition = 5;
                }

                if (_rectLeft.YPosition >= 340)
                {
                    _rectLeft.YPosition = 340;
                }

                if (_rectRight.YPosition >= 340)
                {
                    _rectRight.YPosition = 340;
                }
                //UPPER bound
                if (_ball.Y <= 0)
                {
                    _angle += (_angle180 - 2 * _angle);
                }

                //LOWER bound
                if (_ball.Y >= mainCanvas.ActualHeight - rectWidth)
                {
                    _angle += (_angle180 - 2 * _angle);
                }

                //Collision check
                if (CheckCollision())
                {
                    ChangeAngle();
                    ChangeDirection();
                }

                double r = Math.PI / _angle180 * _angle;

                //Vector for direct coordinates
                Vector vector = new()
                {
                    X = Math.Sin(r),
                    Y = -Math.Cos(r)
                };

                _ball.X += vector.X * _speed;
                _ball.Y += vector.Y * _speed;

                ResetBallPosition();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the ball position.
        /// </summary>
        private void ResetBallPosition()
        {
            try
            {
                double halfRect = rectP2.Width / 2; //because ball.X reset position is half canvas - rectangle width
                double boundR = mainCanvas.Width - rectP2.Width + halfRect;
                double boundL = rectP2.Width - halfRect;

                //Reset position if out of right bound
                if (_ball.X >= boundR)
                {
                    _ball.LeftResult += 1;
                    ResetGame();
                }

                //Reset position if out of left bound
                if (_ball.X <= boundL)
                {
                    _ball.RightResult += 1;
                    ResetGame();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Resets the game.
        /// </summary>
        private void ResetGame()
        {
            try
            {
                //Standard ball pos.
                _ball.Y = 210;
                _ball.X = 380;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Changes the angle.
        /// </summary>
        private void ChangeAngle()
        {
            try
            {
                double rightAngle = 90;

                if (_ball.IsMovingRight)
                {
                    _angle = ( rightAngle * 3 ) - (_ball.Y + 10 - (_rectRight.YPosition + 40));
                }
                else _angle = rightAngle + (_ball.Y + 10 - (_rectLeft.YPosition + 40));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Changes the direction.
        /// </summary>
        private void ChangeDirection()
        {
            try
            {
                if (_ball.IsMovingRight)
                {
                    _ball.IsMovingRight = false;
                }
                else _ball.IsMovingRight = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Checks the collision.
        /// </summary>
        /// <returns>A bool.</returns>
        private bool CheckCollision()
        {
            try
            {
                bool collisionResult = false;
                double rectHeight = rectP1.Height;
                double rectWidth = rectP1.Width;
                bool temp;

                if (_ball.IsMovingRight)
                {
                    // ball.X is in right field and colliding with right rectangle.Y
                    temp = _ball.Y > _rectRight.YPosition - rectWidth && _ball.Y < _rectRight.YPosition + rectHeight;
                    collisionResult = _ball.X >= 760 && temp;
                }

                if (!_ball.IsMovingRight)
                {
                    // ball.X is in left field and colliding with left rectangle.Y
                    temp = _ball.Y > _rectLeft.YPosition - rectWidth && _ball.Y < _rectLeft.YPosition + rectHeight;
                    collisionResult = _ball.X <= rectWidth && temp;
                }

                return collisionResult;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// When second set of controls are pressed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void SecondControlsDown( object sender, KeyboardEventArgs e )
        {
            if (Keyboard.IsKeyDown(Key.W))
            {
                _rectLeft.YPosition -= _rectSpeed;
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                _rectLeft.YPosition += _rectSpeed;
            }
            if (Keyboard.IsKeyDown(Key.Up))
            {
                _rectRight.YPosition -= _rectSpeed;
            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                _rectRight.YPosition += _rectSpeed;
            }
        }

        #endregion METH
    }
}