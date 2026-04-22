using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace BoxMenu
{
    /// <summary>
    /// A collection of buttons (i.e. a "screen").
    /// </summary>
    public class ButtonCollection
    {
        private List<AbstractButtonFramework> buttons = [];
        private MouseState nowState, prevState;

        /// <summary>
        /// Visual offset of all buttons from their original positions.
        /// </summary>
        public Point Offset;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ButtonCollection() {
            nowState = Mouse.GetState();
            prevState = Mouse.GetState();
        }

        /// <summary>
        /// Gets the number of buttons in this collection.
        /// </summary>
        public int Count
        {
            get { return buttons.Count; }
        }

        /// <summary>
        /// Add a button to this collection.
        /// </summary>
        /// <param name="button"></param>
        public void Add(AbstractButtonFramework button)
        {
            buttons.Add(button);
        }

        /// <summary>
        /// Remove a button from the collection.
        /// </summary>
        /// <param name="button"></param>
        public void Remove(AbstractButtonFramework button)
        {
            buttons.Remove(button);
        }

        /// <summary>
        /// Remove a button at the given index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            buttons.RemoveAt(index);
        }

        /// <summary>
        /// Removes all buttons from the collection.
        /// </summary>
        public void Clear()
        {
            buttons.Clear();
        }

        /// <summary>
        /// Update all the buttons in this button collection.
        /// </summary>
        public void Update()
        {
            prevState = nowState;
            nowState = Mouse.GetState();

            bool blocked = false;

            // Go in reverse order, so upper buttons block lower buttons.
            for (int i = buttons.Count - 1; i >= 0; i--)
            {
                if (buttons[i].InternalUpdate(nowState, prevState, blocked, Offset))
                    blocked = true;
            }
        }

        /// <summary>
        /// Draw all the buttons in this collection.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Draw(spriteBatch);
        }

        /// <summary>
        /// Set the Enabled property of every button in the collection.
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabledAll(bool enabled)
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Enabled = enabled;
        }

        /// <summary>
        /// Set the Visible property of every button in the collection.
        /// </summary>
        /// <param name="visible"></param>
        public void SetVisibleAll(bool visible)
        {
            foreach (AbstractClickableButton button in buttons.Where(x => x is AbstractClickableButton))
                button.Visible = visible;
        }

        /// <summary>
        /// Toggle the individual Enabled property of each button.
        /// </summary>
        public void ToggleEnabledAll()
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Enabled = !buttons[i].Enabled;
        }

        /// <summary>
        /// Toggle the individual Visible property of each button.
        /// </summary>
        public void ToggleVisibleAll()
        {
            foreach (AbstractClickableButton button in buttons.Where(x => x is AbstractClickableButton))
                button.Visible = !button.Visible;
        }

        /// <summary>
        /// Foreach construct.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<AbstractButtonFramework> GetEnumerator()
        {
            for (int i = 0; i < buttons.Count; i++)
                yield return buttons[i];
        }

        /// <summary>
        /// Gets the button at this index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AbstractButtonFramework this[int index]
        {
            get { return buttons[index]; }
        }
    }
}
