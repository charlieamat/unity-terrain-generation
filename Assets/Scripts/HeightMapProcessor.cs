using UnityEngine;

public class HeightmapProcessor
{
    public virtual float[,] GetHeightmapSamples(Texture2D heightmapTexture)
    {
        var heightmapSamples = new float[heightmapTexture.width, heightmapTexture.height];
        for (var y = 0; y < heightmapTexture.height; y++)
            for (var x = 0; x < heightmapTexture.width; x++)
                heightmapSamples[x, y] = heightmapTexture.GetPixel(x, y).grayscale;
        return heightmapSamples;
    }
}