using System;

public class CoinSprite: AnimatedPlayerSprite
{
    public CoinSprite(Texture2D texture) : base(texture, new Point(1, 4))
    {
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 Location, bool isLeft)
    {
        base.Draw(spriteBatch, new Vector2(200, 200), isLeft);
    }
}
