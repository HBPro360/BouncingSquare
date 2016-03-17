using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BouncingSquare
{
    public class Square : Event, IDisposable
    {
        #region Private Members
        private Guid _id = Guid.Empty;
        private Form _form = null;
        private PictureBox _box = null;
        private Timer _timer = null;
        private int _xDir = 1;
        private int _yDir = 1;
        private Random _rnd = null;
        private Paddle _paddle = null;
        private Image _explosion = null;
        private Timer _tmrExplosion = null;
        private int _Value = 0;


        #endregion

        #region Public Properties

        public PictureBox Box
        {
            get { return _box; }
        }
        public int xDir
        {
            get { return _xDir; }
            set {_xDir = value; }
        }
        public int yDir
        {
            get { return _yDir; }
            set { _yDir = value; }
        }
        public Guid Id
        {
            get { return _id; }
        }


        #endregion

        #region Public Methods

        public void SetBackGround()
        {
            _box.BackColor = Color.Black;
            //Dispose();
        }
        public void StartExplosion()
        {
            _tmrExplosion.Enabled = true;
        }


        #endregion

        #region Private Methods
        private void Move()
        {
            Point location = _box.Location;
            location.X += _xDir;
            location.Y += _yDir;
            _box.Location = location;
            if (location.Y >= _form.Height - (_box.Height * 2))
            {
                Dispose();
                ScoreEventArgs e = new ScoreEventArgs(string.Empty, -this._Value);
                RaiseEvent(this, e);
            }
            else if (location.Y <= 0)
            {
                _yDir = -_yDir;
            }
            else if (location.X >= _form.Width - (_box.Width * 2))
            {
                _xDir = -_xDir;
            }
            else if (location.X <= 0)
            {
                _xDir = -_xDir;
            }
            else if (_paddle.Box.Bounds.IntersectsWith(_box.Bounds))
            {
                _yDir = -_yDir;
                ScoreEventArgs e = new ScoreEventArgs(string.Empty, _Value);
                RaiseEvent(this, e);
            }

        }
        #endregion
        #region Event Handlers
        private void _timer_Tick(object sender, EventArgs e)
        {
            Move();
        }

        private void _tmrExplosion_Tick(object sender, EventArgs e)
        {
            _tmrExplosion.Enabled = false;
            Dispose();
        }

 
        #endregion

        #region Construction
        public Square(Form frm, Random rnd, Paddle paddle)
        {
            _paddle = paddle;
            _rnd = rnd;
            _Value = _rnd.Next(1,10);
            _tmrExplosion = new Timer();
            _tmrExplosion.Interval = 2500;
            _tmrExplosion.Tick += _tmrExplosion_Tick;
            
            _form = frm;
            _box = new PictureBox();
            _box.Paint += _box_Paint;
            
            //_box = new PictureBox();
            _box.Width = /*_rnd.Next(10, 50);*/ 25;
            _box.Height = /*_rnd.Next(10, 50);*/ 25;
            _box.BackColor = Color.White;
            //_box.BackColor = Color.FromArgb(_rnd.Next(0, 256), _rnd.Next(0, 256), _rnd.Next(0, 256));

            Point location = new Point();
            location.X  = _rnd.Next(0, _form.Width -_box.Width);
            location.Y = _rnd.Next(0, _form.Height/5 -_box.Height);
            _box.Location = location;
            
            _timer = new Timer();
            _timer.Interval = 1;
            _timer.Enabled = true;
            _timer.Tick += _timer_Tick;

            do
            {
                _xDir = _rnd.Next(2, 3);
            } while (_xDir == 0);

            do
            {
                _yDir = _rnd.Next(2, 3);
            } while (_yDir == 0);


            _form.Controls.Add(_box);
            Bitmap bmp = new Bitmap("Images/Explode.gif");
            _explosion = bmp;

            _id = Guid.NewGuid();


        }

        private void _box_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Times New Roman", 20, FontStyle.Bold))
            {
                e.Graphics.DrawString(_Value.ToString(), myFont, Brushes.Black, new Point(1, -3));
            }
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _timer.Enabled = false;
                    _box.Dispose();
                    _explosion = null;
                    _form.Controls.Remove(_box);
                    _form = null;
                    //_rnd = null;

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Square() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
