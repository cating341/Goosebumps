// -------------------------------------------------------------------
// - A custom asset importer for Unity 3D game engine by Sarper Soher-
// - http://www.sarpersoher.com                                      -
// -------------------------------------------------------------------
// - This script utilizes the file names of the imported assets      -
// - to change the import settings automatically as described        -
// - in this script                                                  -
// -------------------------------------------------------------------

#if UNITY_EDITOR_OSX
using UnityEngine;

using UnityEditor;  // Most of the utilities we are going to use are contained in the UnityEditor namespace

using System.IO;
using System.Collections.Generic;
// We inherit from the AssetPostProcessor class which contains all the exposed variables and event triggers for asset importing pipeline
internal sealed class PixelAssetImporter : AssetPostprocessor {
    // Couple of constants used below to be able to change from a single point, you may use direct literals instead of these consts to if you please


    #region Methods

    //-------------Pre Processors

    // This event is raised when a texture asset is imported
    void OnPostprocessSprites(Texture2D texture, Sprite[] sprites)
	{
		TextureImporter importer = assetImporter as TextureImporter;

		// only change sprite import settings on first import, so we can change those settings for individual files
        
		string name = importer.assetPath.ToLower();
		if (File.Exists(AssetDatabase.GetTextMetaFilePathFromAssetPath(name)))
		{
			return;
		}
        
        
		// adjust values for pixel art

		importer.spritePixelsPerUnit = 1;
		importer.mipmapEnabled = false;
		importer.filterMode = FilterMode.Point;
		importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;

		// access the TextureImporterSettings to change the spriteAlignment value
        
		TextureImporterSettings textureSettings = new TextureImporterSettings();
		importer.ReadTextureSettings(textureSettings);

        textureSettings.spriteMode = (int)SpriteImportMode.Multiple;
		textureSettings.spritePivot = new Vector2(0.5f, 0f);
		textureSettings.spriteAlignment = (int)SpriteAlignment.BottomCenter;
        
		importer.SetTextureSettings(textureSettings);
		



        List<SpriteMetaData> newData = new List<SpriteMetaData>();
        
        int SliceWidth = 48;
        int SliceHeight = 48;
        if(texture.height%64==0)
        {
            SliceWidth = 64;
            SliceHeight = 64;
        } 
        
        int cnt = 0;
        
             for(int j = texture.height; j > 0;  j -= SliceHeight)
             {
                 for (int i = 0; i < texture.width; i += SliceWidth)
                {
                 SpriteMetaData smd = new SpriteMetaData();
                 smd.pivot = new Vector2(0f, 0f);
                 smd.alignment = 9;
                 smd.name = Path.GetFileNameWithoutExtension(importer.assetPath)+"_"+cnt++;
                 smd.rect = new Rect(i, j-SliceHeight, SliceWidth, SliceHeight);
 
                 newData.Add(smd);
                }
            }
 
         importer.spritesheet = newData.ToArray();

         importer.SaveAndReimport();
         //AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
	}

    
   
    #endregion
}
#endif