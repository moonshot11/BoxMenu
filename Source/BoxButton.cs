using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoxMenu
{
    /// <summary>
    /// An abstract button.
    /// </summary>
    public abstract class BoxButton
    {
        internal enum BoxButtonState
        {
            Inactive,
            Active,
            Clicking,
            Hovering
        }

        internal BoxButtonState state = BoxButtonState.Active;

        private int clickTimer = 0;

        internal int TIMER_MAX = 2;

        private object[] arguments;

        /// <summary>
        /// This box contains the on-screen position and dimensions of the button.
        /// </summary>
        public Rectangle BoundingBox { get; internal set; }
        internal Rectangle origBoundingBox;

        /// <summary>
        /// Gets or sets whether this button is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets whether this button is visible.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// This event is called when the mouse is clicked while selecting the item.
        /// </summary>
        internal bool Update(MouseState nowState, bool blocked, Point offset)
        {
            BoundingBox = new Rectangle(origBoundingBox.Location + offset, origBoundingBox.Size);

            bool blocking = false;
            bool contains = BoundingBox.Contains(nowState.X, nowState.Y);

            if (state == BoxButtonState.Clicking && clickTimer > 0)
            {
                clickTimer--;
                blocking = true;

                if (clickTimer == 0)
                {
                    state = BoxButtonState.Active;
                    if (ad != null)
                        ad(arguments);
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
                if (contains && nowState.LeftButton == ButtonState.Pressed && state == BoxButtonState.Hovering)
                {
                    blocking = true;
                    state = BoxButtonState.Clicking;
                }
                else if (state == BoxButtonState.Clicking && nowState.LeftButton == ButtonState.Released)
                {
                    blocking = true;
                    clickTimer = TIMER_MAX;
                }
                else if (contains && nowState.LeftButton == ButtonState.Released)
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
        /// <summary>
        /// Update the appearance of the button
        /// </summary>
        internal abstract void UpdateAppearance();

        internal abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// A delegate with no return type which takes a
        /// "params" array of objects.
        /// </summary>
        /// <param name="args"></param>
        public delegate void ActionDelegate(params object[] args);
        /// <summary>
        /// The function to run when clicked.
        /// </summary>
        private ActionDelegate ad;

        internal BoxButton(Rectangle rectangle,
            ActionDelegate ad, params object[] arguments)
        {
            origBoundingBox = rectangle;
            this.ad = ad;

            this.arguments = arguments;
            Enabled = true;
            Visible = true;
        }

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
    }
}
