using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace nobots_proof_of_concept
{
    public class Box : Element
    {
        SpriteBatch spriteBatch;

        Rectangle rectangle;
        public Body body;
        World world;

        public bool isDisposed;

        public Box(Game game, World world)
            : base(game)
        {
            this.world = world;
        }

        public override void Initialize()
        {
            isDisposed = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("wooden-box");
            rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            body = BodyFactory.CreateRectangle(world, 0.02f * rectangle.Width, 0.02f * rectangle.Height, 300000.0f);
            body.Position = new Vector2(5.812996f, 7.583698f);
            body.BodyType = BodyType.Dynamic;
            body.Rotation = -3.236696f;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if(!isDisposed)
                spriteBatch.Draw(texture, 50.0f * body.Position, null, Color.White, body.Rotation, new Vector2(texture.Width/2, texture.Height/2), 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
