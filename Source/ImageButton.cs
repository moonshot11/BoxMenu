using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoxMenu
{
    /// <summary>
    /// A button whose four states are described by four separate Texture2Ds.
    /// </summary>
    public class ImageButton : AbstractButton
    {
        private Texture2D inactiveTexture, activeTexture, hoverTexture, clickTexture;

        private Texture2D currentTexture;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="boundingBox">The bounding box of the button.</param>
        /// <param name="inactiveTexture">The texture when the button is disabled (but visible!).</param>
        /// <param name="activeTexture">The texture when the button is enabled.</param>
        /// <param name="hoverTexture">The texture when the mouse is hovering over the button.</param>
        /// <param name="clickTexture">The texture when the mouse is clicking on the button.</param>
        /// <param name="actionFunction">The delegate to the function raised by the button.</param>
        /// <param name="arguments">Any arguments to pass to the function raised by the button.</param>
        public ImageButton(Rectangle boundingBox,
            Texture2D inactiveTexture,
            Texture2D activeTexture,
            Texture2D hoverTexture,
            Texture2D clickTexture,
            ActionDelegate actionFunction, params object[] arguments)
            : base(boundingBox, actionFunction, arguments)
        {
            this.inactiveTexture = inactiveTexture;
            this.activeTexture = activeTexture;
            this.hoverTexture = hoverTexture;
            this.clickTexture = clickTexture;

            currentTexture = activeTexture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
                return;

            spriteBatch.Draw(currentTexture,
                BoundingBox,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);
        }

        internal override void UpdateAppearance()
        {
            switch (state)
            {
                case BoxButtonState.Active:
                    currentTexture = activeTexture;
                    break;
                case BoxButtonState.Inactive:
                    currentTexture = inactiveTexture;
                    break;
                case BoxButtonState.Hovering:
                    currentTexture = hoverTexture;
                    break;
                case BoxButtonState.Clicking:
                    currentTexture = clickTexture;
                    break;
            }
        }
    }
}
