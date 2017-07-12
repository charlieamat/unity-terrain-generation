using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests
{
    public class TerrainDeformerTests
    {
        private TerrainDeformer _terrainDeformer;
        private Terrain _terrain;
        private HeightmapProcessor _heightmapProcessor;

        [SetUp]
        public void BeforeEveryTest()
        {
            _heightmapProcessor = Substitute.For<HeightmapProcessor>();
            _terrain = Terrain.CreateTerrainGameObject(new TerrainData()).GetComponent<Terrain>();
            _terrainDeformer = new TerrainDeformer
            {
                Terrain = _terrain,
                HeightmapProcessor = _heightmapProcessor
            };
        }

        [Test]
        public void _Terrain_Dimensions_Match_Heightmap_Samples_Array_Dimensions()
        {
            _heightmapProcessor.GetHeightmapSamples(Arg.Any<Texture2D>())
                .Returns(new float[3, 5]);

            _terrainDeformer.Deform();

            Assert.AreEqual(3, _terrain.terrainData.size.x);
            Assert.AreEqual(5, _terrain.terrainData.size.z);
        }

        [Test]
        public void _Terrain_Heights_Come_From_Heightmap_Processor()
        {
            _heightmapProcessor.GetHeightmapSamples(Arg.Any<Texture2D>())
                .Returns(new float[,] {{1, 0}, {0, 1}});

            _terrainDeformer.Deform();

            CollectionAssert.AreEqual(
                new float[,] {{1, 0}, {0, 1}},
                _terrain.terrainData.GetHeights(0, 0, 2, 2));
        }

        [Test]
        public void _Terrain_Object_Height_Is_Affected_By_Heart_Property()
        {
            _terrainDeformer.Height = 15;

            _terrainDeformer.Deform();

            Assert.AreEqual(15, _terrain.terrainData.size.y);
        }
    }
}