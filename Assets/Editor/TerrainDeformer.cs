using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class TerrainDeformer : EditorWindow
    {
        public Terrain Terrain;
        public Texture2D HeightMapTexture;
        public int Height;

        public HeightmapProcessor HeightmapProcessor { get; set; }

        [MenuItem("Window/Terrain Deformer")]
        private static void ShowTerrainDeformer()
        {
            var window = GetWindow<TerrainDeformer>();
            window.titleContent.text = "Terrain Deformer";
            window.Show();
        }

        public void Deform()
        {
            if (HeightmapProcessor == null)
                HeightmapProcessor = new HeightmapProcessor();

            var heightmapSamples = HeightmapProcessor.GetHeightmapSamples(HeightMapTexture);

            Terrain.terrainData.size = new Vector3(heightmapSamples.GetLength(0), Height, heightmapSamples.GetLength(1));
            Terrain.terrainData.SetHeights(0, 0, heightmapSamples);
        }

        private void OnGUI()
        {
            Terrain = EditorGUILayout.ObjectField(Terrain, typeof(Terrain), true) as Terrain;
            HeightMapTexture = EditorGUILayout.ObjectField(HeightMapTexture, typeof(Texture2D), true) as Texture2D;
            Height = EditorGUILayout.IntField(Height);

            if (GUILayout.Button("Deform"))
            {
                Deform();
            }
        }
    }
}