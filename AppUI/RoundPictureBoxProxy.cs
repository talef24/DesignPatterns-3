using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppUI
{
    public class RoundPictureBoxProxy : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(this.DisplayRectangle);
            this.Region = new Region(gp);
        }
    }
}