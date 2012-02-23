using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Input;

namespace nobots_proof_of_concept
{
    public class MainCharacter : Character
    {
        SpriteBatch spriteBatch;

        Rectangle rectangle;
        World world;
        public SpriteEffects effect;

        bool isSpaceDown;

        public MainCharacter(Game game, World world) : base(game)
        {
            this.world = world;
        }

        public override void Initialize()
        {
            effect = SpriteEffects.None;
            isHaunted = true;
            isSpaceDown = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Game.Content.Load<Texture2D>("Girl");
            rectangle = new Rectangle((int)(Game1.screenWidth / 2), (int)(Game1.screenHeight / 1.7), texture.Width, texture.Height);
            body = BodyFactory.CreateCircle(world, 0.02f * rectangle.Width / 4.0f, 50.0f);
            body.Position = new Vector2(0.02f * rectangle.X, 0.02f * rectangle.Y);
            body.BodyType = BodyType.Dynamic;
            body.Friction = 10000.0f;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ProcessKeyboard();  
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, 50.0f * body.Position, null, Color.White, 0.0f, new Vector2(texture.Width / 2, texture.Height - texture.Width / 4), 1.0f, effect, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void ProcessKeyboard()
        {
            KeyboardState keybState = Keyboard.GetState();
            if (isHaunted)
            {
                if (keybState.IsKeyDown(Keys.Left))
                {
                    body.FixedRotation = false;
                    effect = SpriteEffects.FlipHorizontally;
                    body.AngularVelocity = -20.0f;
                }
                else if (keybState.IsKeyDown(Keys.Right))
                {
                    body.FixedRotation = false;
                    effect = SpriteEffects.None;
                    body.AngularVelocity = 20.0f;
                }
                else
                {
                    body.FixedRotation = true;
                    body.AngularVelocity = 0.0f;
                }

                if(keybState.IsKeyDown(Keys.Space))
                    if (!isSpaceDown)
                    {
                        Console.WriteLine("space pressed");
                        isSpaceDown = true;
                        isHaunted = false;
                        Console.WriteLine("ishaunted "+ isHaunted);
                        ((Game1)Game).ghost.Unhaunt(this);
                    }

                if (keybState.IsKeyUp(Keys.Space) && isSpaceDown)
                    isSpaceDown = false;
            }
        }

        public void Raise(Character character)
        {
        }

        public void Drop(Character character)
        {
        }

        public void PutOnBox(Character character)
        {
        }

        public void Die()
        {
            isHaunted = false;
        }
    }
}
