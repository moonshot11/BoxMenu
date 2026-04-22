using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BoxMenu
{

    /// <summary>
    /// An abstract button.
    /// </summary>
    public abstract class AbstractClickableButton : AbstractButtonFramework
    {
        internal int clickTimer = 0;

        internal int TIMER_MAX = 2;

        /// <summary>
        /// Gets or sets whether this button is visible.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// The function to run when clicked.
        /// </summary>
        internal ActionDelegate actionDelegate;

        /// <summary>
        /// This event is called when the mouse is clicked while selecting the item.
        /// </summary>
        internal override bool InternalUpdate(MouseState nowState, MouseState prevState, bool blocked, Point offset)
        {
            ButtonState mouseTrigger;

            switch (MouseTrigger)
            {
                case MouseAction.LeftClick:
                    mouseTrigger = nowState.LeftButton;
                    break;
                case MouseAction.RightClick:
                    mouseTrigger = nowState.RightButton;
                    break;
                case MouseAction.MiddleClick:
                    mouseTrigger = nowState.MiddleButton;
                    break;
                default:
                    throw new NotImplementedException("Mouse scrolling not yet implemented");
            }

            BoundingBox = new Rectangle(origBoundingBox.Location + offset, origBoundingBox.Size);

            bool blocking = false;
            bool contains = BoundingBox.Contains(nowState.X, nowState.Y);

            if (state == BoxButtonState.Clicking && clickTimer > 0)
            {
                if (ActivationCondition == ActivationCondition.OnPress && clickTimer == TIMER_MAX)
                    actionDelegate?.Invoke(arguments);

                clickTimer--;
                blocking = true;

                if (clickTimer == 0)
                {
                    state = BoxButtonState.Active;
                    if (ActivationCondition == ActivationCondition.OnRelease)
                        actionDelegate?.Invoke(arguments);
                }
            }

            else if (!Enabled || !Visible)
            {
                state = BoxButtonState.Inactive;
            }

            else if (blocked)
            {
                state = BoxButtonState.Active;
            }

            else
            {
                if (contains && mouseTrigger == ButtonState.Pressed && state == BoxButtonState.Hovering)
                {
                    blocking = true;
                    state = BoxButtonState.Clicking;
                    if (ActivationCondition == ActivationCondition.OnPress)
                        clickTimer = TIMER_MAX;
                }
                else if (state == BoxButtonState.Clicking && mouseTrigger == ButtonState.Released)
                {
                    blocking = true;
                    if (ActivationCondition == ActivationCondition.OnRelease)
                        clickTimer = TIMER_MAX;
                }
                else if (contains && mouseTrigger == ButtonState.Released)
                {
                    blocking = true;
                    state = BoxButtonState.Hovering;
                }
                else if (!contains)
                {
                    state = BoxButtonState.Active;
                }
            }

            UpdateAppearance();

            return blocking;
        }

        internal AbstractClickableButton(Rectangle rectangle,
            ActionDelegate actionDelegate, params object[] arguments)
        {
            origBoundingBox = rectangle;

            this.actionDelegate = actionDelegate;
            this.arguments = arguments;

            Enabled = true;
            Visible = true;

            soloCollection = new ButtonCollection();
            soloCollection.Add(this);
        }
    }
}
