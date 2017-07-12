using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests
{
    public class HightMapProcessorTests
    {
        [Test]
        public void _Heightmap_Samples_Array_Dimensions_Match_Texture_Dimension()
        {
            var heightmapProcessor = new HeightmapProcessor();
            var texture = new Texture2D(2, 4);

            var heightmapSamples = heightmapProcessor.GetHeightmapSamples(texture);
            
            Assert.AreEqual(2, heightmapSamples.GetLength(0));
            Assert.AreEqual(4, heightmapSamples.GetLength(1));
        }

        [Test]
        public void _Returns_Array_Of_1s_For_White_Texture()
        {
            var heightmapProcessor = new HeightmapProcessor();
            var whiteTexture = new Texture2D(1, 1);
            whiteTexture.SetPixel(0, 0, Color.white);

            var heightmapSamples = heightmapProcessor.GetHeightmapSamples(whiteTexture);

            CollectionAssert.AreEqual(new [,] {{1}}, heightmapSamples);
        }
    }
}