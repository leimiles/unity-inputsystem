using UnityEngine;
using UnityEditor;

public static class TextureArrayCreator
{
    [MenuItem("Assets/Create/Texture Array")]
    public static void CreateTextureArray()
    {
        // 获取选中的纹理
        Texture2D[] textures = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);

        if (textures.Length == 0)
        {
            Debug.LogError("No textures selected! Please select textures to create a Texture Array.");
            return;
        }

        // 确保所有纹理尺寸和格式相同
        int width = textures[0].width;
        int height = textures[0].height;
        TextureFormat format = textures[0].format;

        foreach (var tex in textures)
        {
            if (tex.width != width || tex.height != height || tex.format != format)
            {
                Debug.LogError("All textures must have the same size and format!");
                return;
            }
        }

        // 创建 Texture Array
        Texture2DArray textureArray = new Texture2DArray(width, height, textures.Length, format, false);

        // 填充 Texture Array
        for (int i = 0; i < textures.Length; i++)
        {
            Graphics.CopyTexture(textures[i], 0, 0, textureArray, i, 0);
        }

        // 保存 Texture Array 到 Assets 文件夹
        string path = "Assets/TextureArray.asset";
        AssetDatabase.CreateAsset(textureArray, path);
        AssetDatabase.SaveAssets();

        Debug.Log("Texture Array created at " + path);
    }
}
