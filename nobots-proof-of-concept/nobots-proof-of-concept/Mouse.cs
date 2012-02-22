using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace nobots_proof_of_concept
{
    class Mouse : Character
    {
        SpriteBatch spriteBatch;

        Rectangle rectangle;

        public Mouse(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("mouse");
            rectangle = new Rectangle((int)(Game1.screenWidth / 3), (int)(Game1.screenHeight / 1.3), texture.Width/2, texture.Height/2);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
