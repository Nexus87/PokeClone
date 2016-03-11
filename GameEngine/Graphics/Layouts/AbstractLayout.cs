using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameEngine.Graphics.Layouts
{
    /// <summary>
    /// This class provides a skeletal implementation for ILayout
    /// </summary>
    /// <remarks>
    /// The abstract implementation already takes care of much of the bookkeeping.
    /// Implementing class need only to implement UpdateComponent. At the point,
    /// the container can be assumed to be not null.
    /// The protected properties Height, Width, X and Y already return the
    /// position and size with regards to the margins.
    /// </remarks>
    public abstract class AbstractLayout : ILayout
    {
        private float marginBottom;
        private float marginLeft;
        private float marginRight;
        private float marginTop;

        private Vector2 position;
        private Vector2 size;
        /// <summary>
        /// Returns the Height of the container with regard to the bottom and top margin
        /// </summary>
        /// <remarks>
        /// Is always >= 0
        /// </remarks>
        protected float Height { get { return Math.Max(0, size.Y - marginTop - marginBottom); } }
        /// <summary>
        /// Returns the Width of the container with regard to the right and left margin
        /// </summary>
        /// <remarks>
        /// Is always >= 0
        /// </remarks>
        protected float Width { get { return Math.Max(0, size.X - marginRight - marginLeft); } }
        /// <summary>
        /// Returns the X coordinate of the container with regards to the left margin
        /// </summary>
        protected float XPosition { get { return position.X + marginLeft; } }
        /// <summary>
        /// Returns the Y coordinate of the container with regards to the top margin
        /// </summary>
        protected float YPosition { get { return position.Y + marginTop; } }

        public void LayoutContainer(Container container)
        {
            container.CheckNull("container");

            position.X = container.XPosition;
            position.Y = container.YPosition;
            size.X = container.Width;
            size.Y = container.Height;

            UpdateComponents(container);
        }

        public void SetMargin(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            marginLeft = left;
            marginRight = right;
            marginTop = top;
            marginBottom = bottom;
        }

        /// <summary>
        /// This function gets during LayoutContainer
        /// </summary>
        /// <remarks>
        /// The container parameter can be assumed as not null.
        /// Instead of using the containers position and size, a subclass
        /// should use the protected properties Height, Width, X and Y
        /// since there is no other way for it, to get the information
        /// about the margin.
        /// </remarks>
        /// <param name="container"></param>
        protected abstract void UpdateComponents(Container container);

    }
}