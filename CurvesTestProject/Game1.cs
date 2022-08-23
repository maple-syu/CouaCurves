using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using CouaCurves;

namespace CurvesTestProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        class Sprite
        {
            public Texture2D texture;
            public Vector2 position;
            public int size;

            public Sprite(Texture2D texture, Vector2 position, int size)
            {
                this.texture = texture;
                this.position = position;
                this.size = size;
            }
        }

        Texture2D defaultTexture;
        Sprite[] sprites;
        int spriteSize = 32;

        Vector2[] sprite0Path = new Vector2[]
        {
            new Vector2(32, 64),
            new Vector2(532, 64)
        };
        Vector2[] sprite1Path = new Vector2[]
        {
            new Vector2(32, 128),
            new Vector2(532, 128)
        };

        Vector2[] sprite2Path = new Vector2[]
        {
            new Vector2(32, 192),
            new Vector2(532, 192)
        };

        Vector2[] sprite3Path = new Vector2[]
        {
            new Vector2(32, 256),
            new Vector2(532, 256)
        };

        CouaBezier hillCurve = new CouaBezier();
        CouaBezier sineCurve = new CouaBezier();
        CouaBezier sCurve = new CouaBezier();

        float timer;
        float maxTimer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //Debug.WriteLine(graphics.GraphicsDevice.IsDisposed);
            Content.RootDirectory = "Content";

        }
        protected override void Initialize()
        {
            base.Initialize();

            hillCurve.SetPointA(0f, 0f);
            hillCurve.SetPointB(0f, 1f);
            hillCurve.SetPointC(1f, 1f);
            hillCurve.SetPointD(1f, 0f);

            sineCurve.SetPointA(0f, 0f);
            sineCurve.SetPointB(0.1f, 0.8f);
            sineCurve.SetPointC(0.2f, 0.9f);
            sineCurve.SetPointD(1f, 1f);

            sCurve.SetPointA(0f, 0f);
            sCurve.SetPointB(0.5f, 0f);
            sCurve.SetPointC(0.5f, 1f);
            sCurve.SetPointD(1f, 1f);

            timer = 0f;
            maxTimer = 2f;

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            defaultTexture = new Texture2D(graphics.GraphicsDevice, spriteSize, spriteSize);

            Color[] color = new Color[spriteSize * spriteSize];
            for (int q = 0; q < color.Length; q++)
            {
                color[q] = Color.White;
            }
            defaultTexture.SetData<Color>(color);


            sprites = new Sprite[]
            {
                new Sprite(defaultTexture, sprite0Path[0], spriteSize),
                new Sprite(defaultTexture, sprite1Path[0], spriteSize),
                new Sprite(defaultTexture, sprite2Path[0], spriteSize),
                new Sprite(defaultTexture, sprite3Path[0], spriteSize),
            };
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > maxTimer)
            {
                timer -= maxTimer;
            }

            float t = timer / maxTimer;

            //sprite[0]
            Vector2 sprite0Position = new Vector2(
                //starting position, plus the change in x/y multiplied by t
                sprite0Path[0].X + ((sprite0Path[1].X - sprite0Path[0].X) * t),
                sprite0Path[0].Y + ((sprite0Path[1].Y - sprite0Path[0].Y) * t));
            sprites[0].position = sprite0Position;

            //sprite[1]
            Vector2 sprite1Position = new Vector2(
                //starting position, plus the change in x multiplied by the evaluated value of our hill curve along the Y axis.
                //starting positoin, plus the change in y multiplied by t.
                sprite1Path[0].X + ((sprite1Path[1].X - sprite1Path[0].X) * hillCurve.EvaluateY(t)),
                sprite1Path[0].Y + ((sprite1Path[1].Y - sprite1Path[0].Y) * t));
            sprites[1].position = sprite1Position;

            Vector2 sprite2Position = new Vector2(
                //starting position, plus the change in x multiplied by the evaluated value of our hill curve along the Y axis.
                //starting positoin, plus the change in y multiplied by t.
                sprite2Path[0].X + ((sprite2Path[1].X - sprite2Path[0].X) * sineCurve.EvaluateY(t)),
                sprite2Path[0].Y + ((sprite2Path[1].Y - sprite2Path[0].Y) * t));
            sprites[2].position = sprite2Position;

            Vector2 sprite3Position = new Vector2(
                //starting position, plus the change in x multiplied by the evaluated value of our hill curve along the Y axis.
                //starting positoin, plus the change in y multiplied by t.
                sprite3Path[0].X + ((sprite3Path[1].X - sprite3Path[0].X) * sCurve.EvaluateY(t)),
                sprite3Path[0].Y + ((sprite3Path[1].Y - sprite3Path[0].Y) * t));
            sprites[3].position = sprite3Position;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(sprites[0].texture, new Rectangle((int)sprites[0].position.X, (int)sprites[0].position.Y, sprites[0].size, sprites[0].size), Color.White);
            spriteBatch.Draw(sprites[1].texture, new Rectangle((int)sprites[1].position.X, (int)sprites[1].position.Y, sprites[1].size, sprites[1].size), Color.White);
            spriteBatch.Draw(sprites[2].texture, new Rectangle((int)sprites[2].position.X, (int)sprites[2].position.Y, sprites[2].size, sprites[2].size), Color.White);
            spriteBatch.Draw(sprites[3].texture, new Rectangle((int)sprites[3].position.X, (int)sprites[3].position.Y, sprites[3].size, sprites[3].size), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
