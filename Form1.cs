using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncingSquare
{
    public partial class frmBouncingSquare : Form
    {
        Random rnd = new Random();
        Paddle paddle = null;
        public frmBouncingSquare()
        {
            InitializeComponent();
            
            this.Load += FrmBouncingSquare_Load;
            this.KeyDown += FrmBouncingSquare_KeyDown;
            this.MouseMove += FrmBouncingSquare_MouseMove;
            Cursor.Hide();
          

        }

       

        private void FrmBouncingSquare_Load(object sender, EventArgs e)
        {
            paddle = new Paddle(this, rnd);
        }


        private void FrmBouncingSquare_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Application.Exit();
            }
            else if (e.KeyData == Keys.End)
            {
                Square square = new Square(this, rnd, paddle, lblScore);
            }
            else if (e.KeyData == Keys.Left || e.KeyData == Keys.Right)
            {
                paddle.Key = e.KeyData;
            }
        }
        
        private void FrmBouncingSquare_MouseMove(object sender, MouseEventArgs e)
        {
            paddle.Location = e.Location.X;
        }

    }

}

