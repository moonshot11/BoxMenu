using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq.Expressions;

namespace BoxMenu
{

    /// <summary>
    /// An abstract button.
    /// </summary>
    public class HoldButton : AbstractButtonFramework
    {
        private ActionDelegate pressDelegate;
        private ActionDelegate releaseDelegate;

        /// <summary>
        /// This event is called when the mouse is clicked while selecting the item.
        /// </summary>
        internal override bool InternalUpdate(MouseState nowState, MouseState prevState, bool blocked, Point offset)
        {
            ButtonState mouseTrigger, prevTrigger;

            switch (MouseTrigger)
            {
                case MouseAction.LeftClick:
                    mouseTrigger = nowState.LeftButton;
                    prevTrigger = prevState.LeftButton;
                    break;
                case MouseAction.RightClick:
                    mouseTrigger = nowState.RightButton;
                    prevTrigger = prevState.RightButton;
                    break;
                case MouseAction.MiddleClick:
                    mouseTrigger = nowState.MiddleButton;
                    prevTrigger = prevState.MiddleButton;
                    break;
                default:
                    throw new NotImplementedException("Mouse scrolling not yet implemented");
            }

            BoundingBox = new Rectangle(origBoundingBox.Location + offset, origBoundingBox.Size);

            bool blocking = false;
            bool containsCursor = BoundingBox.Contains(nowState.X, nowState.Y);

            if (!Enabled)
            {
                state = BoxButtonState.Inactive;
            }

            else if (blocked)
            {
                state = BoxButtonState.Active;
            }

            else
            {
                if (state == BoxButtonState.Active && containsCursor &&
                    mouseTrigger == ButtonState.Pressed && prevTrigger == ButtonState.Released)
                {
                    pressDelegate?.Invoke(arguments);
                    state = BoxButtonState.Clicking;
                    blocking = true;
                }
                else if (state == BoxButtonState.Clicking &&
                    (!containsCursor || mouseTrigger == ButtonState.Released))
                {
                    releaseDelegate?.Invoke(arguments);
                    state = BoxButtonState.Active;
                }
                else if (state == BoxButtonState.Clicking)
                {
                    blocking = true;
                }
            }

            return blocking;
        }

        public HoldButton(Rectangle rectangle,
            ActionDelegate pressDelegate, ActionDelegate releaseDelegate,
            params object[] arguments)
        {
            origBoundingBox = rectangle;

            this.pressDelegate = pressDelegate;
            this.releaseDelegate = releaseDelegate;
            this.arguments = arguments;

            Enabled = true;

            soloCollection = new ButtonCollection();
            soloCollection.Add(this);
        }

        internal override void UpdateAppearance() { }
        public override void Draw(SpriteBatch sb) { }
    }
}
