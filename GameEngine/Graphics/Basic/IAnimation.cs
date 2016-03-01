using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Basic
{
    /// <summary>
    /// Interface to play animations on IGraphicComponents
    /// </summary>
    /// <remarks>
    /// An animation may take multiple Update calls, to finish.
    /// When its done, it should call the AnimationFinished event, so
    /// that the IGraphicComponent can stop updating it.
    /// </remarks>
    /// <see cref="IGraphicComponent.PlayAnimation"/>
    public interface IAnimation
    {
        /// <summary>
        /// This event is called, if the animation is done.
        /// </summary>
        event EventHandler AnimationFinished;
        /// <summary>
        /// Updates the animation 
        /// </summary>
        /// <remarks>
        /// The meaning of the component parameter depends on
        /// the implementation. It might as well be null.
        /// </remarks>
        /// <param name="time">Game time</param>
        /// <param name="component">Graphic component</param>
        void Update(GameTime time, IGraphicComponent component);
    }
}
