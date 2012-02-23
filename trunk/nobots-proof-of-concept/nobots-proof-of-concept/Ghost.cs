using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Input;
using nobots_proof_of_concept.ParticleSystem;

namespace nobots_proof_of_concept
{
    public class Ghost : Character
    {
        
        SpriteBatch spriteBatch;

        Rectangle rectangle;
        Vector2 position;
        ParticleEmitter particleEmitter;

        Character prey;

        bool isSpaceDown;

        public Ghost(Game game)
            : base(game)
        {
            isHaunted = false;
        }

        public override void Initialize()
        {
            position = new Vector2(100,100);
            particleEmitter = new ParticleEmitter(PlasmaExplosionParticleSystem.LastInstance, 20, new Vector3(position.X, position.Y, 0));
            stepSize = 2;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
         //   if (isHaunted)
            ProcessKeyboard();
            particleEmitter.Update(gameTime, new Vector3(position.X, position.Y, 0));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
       //     if(isHaunted)
             //   spriteBatch.Draw(texture, 50.0f * body.Position, null, Color.White, body.Rotation, new Vector2(texture.Width / 2, texture.Height / 2),
              //      0.5f, SpriteEffects.None, 0);
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
                    position.X -= stepSize;
                }

                if (keybState.IsKeyDown(Keys.Right))
                {
                    position.X += stepSize;
                }

                if (keybState.IsKeyDown(Keys.Up))
                {
                    position.Y -= stepSize;
                }

                if (keybState.IsKeyDown(Keys.Down))
                {
                    position.Y += stepSize;
                }

                if (keybState.IsKeyDown(Keys.Space))
                    if (!isSpaceDown && (prey = checkHauntingPossibility())!= null)
                    {
                        isSpaceDown = true;
                        isHaunted = false;
                        if(prey is Mouse)
                            Console.WriteLine("mouse");

                        haunt(prey);
                    }

                if (keybState.IsKeyUp(Keys.Space) && isSpaceDown)
                    isSpaceDown = false;
            }
        }

        private Character checkHauntingPossibility()
        {
            Vector2 characterPos;
            int characterWidth;
            int characterHeight;

            characterPos = ((Game1)Game).mainCharacter.body.Position * 50;
            characterWidth = ((Game1)Game).mainCharacter.texture.Width;
            characterHeight = ((Game1)Game).mainCharacter.texture.Height;
            if (characterPos.X - 20 <= position.X && characterPos.X + characterWidth + 20 >= position.X &&
                characterPos.Y - characterHeight/2 - 20 <= position.Y && characterPos.Y + characterHeight/2 + 20 >= position.Y)
                return ((Game1)Game).mainCharacter;

            characterPos = ((Game1)Game).mouse.body.Position * 50;
            characterWidth = ((Game1)Game).mouse.texture.Width;
            characterHeight = ((Game1)Game).mouse.texture.Height;
            if (characterPos.X - 20 <= position.X && characterPos.X + characterWidth + 20 >= position.X &&
                characterPos.Y - characterHeight / 2 - 20 <= position.Y && characterPos.Y + characterHeight / 2 + 20 >= position.Y)
                return ((Game1)Game).mouse;

            return null;
        }

        private void haunt(Character character)
        {
            isHaunted = false;
            character.isHaunted = true;
        }
    }
}
