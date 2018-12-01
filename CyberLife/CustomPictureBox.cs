using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CustomPictureBox : PictureBox
{
    private InterpolationMode interpolationMode = InterpolationMode.NearestNeighbor;

    public InterpolationMode InterpolationMode
    {
        get => interpolationMode;
        set
        {
            interpolationMode = value;
            this.Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
        pe.Graphics.InterpolationMode = interpolationMode;
        base.OnPaint(pe);
    }
}
