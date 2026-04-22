using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoxMenu
{
    /// <summary>
    /// An invisible, purely functional button.
    /// </summary>
    public class HiddenButton : AbstractClickableButton
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle">The bounding box of the button.</param>
        /// <param name="activationCondition">When the actionDelegate is triggered.</param>
        /// <param name="actionDelegate">The delegate to raise when pressing this button.</param>
        /// <param name="arguments">Any arguments to pass to the delegate.</param>
        public HiddenButton(Rectangle rectangle,
            ActionDelegate actionDelegate, params object[] arguments)
            : base(rectangle, actionDelegate, arguments)
        {

        }

        internal override void UpdateAppearance() { }
        public override void Draw(SpriteBatch spriteBatch) { }
    }
}
