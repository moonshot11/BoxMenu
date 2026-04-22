using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BoxMenu
{

    public enum ActivationCondition
    {
        OnPress,
        OnRelease
    }

    public enum MouseAction
    {
        LeftClick,
        RightClick,
        MiddleClick,
        ScrollUp,
        ScrollDown
    }

    internal enum BoxButtonState
    {
        Inactive,
        Active,
        Clicking,
        Hovering
    }

    /// <summary>
    /// An abstract button.
    /// </summary>
    public abstract class AbstractButtonFramework
    {

        internal BoxButtonState state = BoxButtonState.Active;

        internal object[] arguments;

        /// <summary>
        /// This box contains the on-screen position and dimensions of the button.
        /// </summary>
        public Rectangle BoundingBox { get; internal set; }
        internal Rectangle origBoundingBox;

        /// <summary>
        /// Gets or sets whether this button is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        public ActivationCondition ActivationCondition { get; set; } = ActivationCondition.OnRelease;
        public MouseAction MouseTrigger { get; set; } = MouseAction.LeftClick;

        internal ButtonCollection soloCollection;

        /// <summary>
        /// A delegate with no return type which takes a
        /// "params" array of objects.
        /// </summary>
        /// <param name="args"></param>
        public delegate void ActionDelegate(params object[] args);

        /// <summary>
        /// Update only this button. Do not use if also updating button through a ButtonCollection.
        /// </summary>
        public void Update()
        {
            soloCollection.Update();
        }

        /// <summary>
        /// This event is called when the mouse is clicked while selecting the item.
        /// </summary>
        internal abstract bool InternalUpdate(MouseState nowState, MouseState prevState, bool blocked, Point offset);

        /// <summary>
        /// Re-set the parameters of this button.  Useful when
        /// an argument is a reference which may have changed
        /// since initialization.
        /// </summary>
        /// <param name="arguments"></param>
        public void SetArguments(params object[] arguments)
        {
            this.arguments = arguments;
        }

        /// <summary>
        /// Update the appearance of the button
        /// </summary>
        internal abstract void UpdateAppearance();

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
