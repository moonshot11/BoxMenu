using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoxMenu
{
    /// <summary>
    /// A button whose states are described by different color highlights
    /// over the same Texture2D.
    /// </summary>
    public class ColorButton : BoxButton
    {
        /// <summary>
        /// Texture
        /// </summary>
        public Texture2D Texture { get; set; }
        private readonly Color activeColor, inactiveColor, hoverColor, clickColor;
        private Color currentColor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle">The bounding box of the button.</param>
        /// <param name="texture">The texture drawn in the bounding box.</param>
        /// <param name="inactiveColor">The color when the button is disabled, but visible.</param>
        /// <param name="activeColor">The color when the button is enabled.</param>
        /// <param name="hoverColor">The color when the mouse is hovering over the button.</param>
        /// <param name="clickColor">The color when the mouse is clicking the button.</param>
        /// <param name="ad">The delegate to raise when pressing this button.</param>
        /// <param name="arguments">Any arguments to pass to the delegate.</param>
        public ColorButton(Rectangle rectangle,
            Texture2D texture,
            Color inactiveColor, Color activeColor,
            Color hoverColor, Color clickColor,
            ActionDelegate ad, params object[] arguments)
            : base(rectangle, ad, arguments)
        {
            this.Texture = texture;

            this.activeColor = activeColor;
            this.inactiveColor = inactiveColor;
            this.hoverColor = hoverColor;
            this.clickColor = clickColor;

            currentColor = activeColor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="texture"></param>
        /// <param name="colorArray">Colors: inactive, active, hovering, clicking.</param>
        /// <param name="ad"></param>
        /// <param name="arguments"></param>
        public ColorButton(Rectangle rectangle,
            Texture2D texture, Color[] colorArray,
            ActionDelegate ad, params object[] arguments)
            : this(rectangle, texture,
            colorArray[0], colorArray[1], colorArray[2], colorArray[3],
            ad, arguments)
        { }

        internal override void UpdateAppearance()
        {
            switch (state)
            {
                case BoxButtonState.Active:
                    currentColor = activeColor;
                    break;
                case BoxButtonState.Inactive:
                    currentColor = inactiveColor;
                    break;
                case BoxButtonState.Hovering:
                    currentColor = hoverColor;
                    break;
                case BoxButtonState.Clicking:
                    currentColor = clickColor;
                    break;
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;

            spriteBatch.Draw(Texture,
                BoundingBox,
                null,
                currentColor,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);
        }
    }
}
