using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    /// <summary>
    /// Interface for classes, that can process user input
    /// </summary>
    /// <remarks>
    /// The HandleInput method is only called, when a key is pressed.
    /// It can be called multiple times if multiple keys are pressed.
    /// Releasing a key will not trigger this function.
    /// </remarks>
    public interface IInputHandler
    {
        /// <summary>
        /// This function is called, whenever a new key is pressed.
        /// Returns true, if the input was processed.
        /// </summary>
        /// <remarks>
        /// The return value signals, if the input was consumed. If false
        /// is returned, the game engine will search for an other InputHandler
        /// that will handle the input.
        /// </remarks>
        /// <param name="key">Pressed key.</param>
        /// <returns>True if the input was consumed.</returns>
        bool HandleInput(CommandKeys key);
    }
}
