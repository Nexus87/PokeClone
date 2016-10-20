namespace GameEngine.Graphics.NewGUI
{
    public class ImageBox : AbstractGraphicComponent
    {
        private IImage image;

        public override double CalculatePreferedWidth()
        {
            return image.ImageWidth;
        }

        public override double CalculatePreferedHeight()
        {
            return image.ImageHeight;
        }
    }
}