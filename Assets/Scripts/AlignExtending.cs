﻿using UnityEngine;
using System.Collections;
using System;

public class AlignExtending : MonoBehaviour
{
    public Painting painting;

    private Renderer rend;
    private bool loaded = false;

	void Start()
    {
        rend = GetComponent<Renderer>();

	}
	
	void Update()
    {
        // This is called in update to ensure the painting object is already initialized.
        if (!loaded)
            LoadMaterial();
	}

    // Loads the extension material.
    void LoadMaterial()
    {
        // Use the name of the painting.
        string paintingName = painting.paintingName;
        if (paintingName != "")
        {
            Material material = Resources.Load("Materials/Illusions/Extending/" + paintingName, typeof(Material)) as Material;
            rend.material = material;

            ScaleTexture();
            loaded = true;
        }
    }

    void ScaleTexture()
    {
        // Calculate the pixels per unit for the current extension texture.
        Vector3 wallSize = rend.bounds.size;
        float ratioX = (float)rend.material.mainTexture.width / wallSize.x;
        float ratioY = (float)rend.material.mainTexture.height / wallSize.y;

        // Calculate the pixels per unit for the painting texture.
        Vector3 paintingSize = painting.rend.bounds.size;
        float maxSize = Math.Max(paintingSize.x, paintingSize.y);
        float paintingRatio = Math.Max(painting.rend.material.mainTexture.width, painting.rend.material.mainTexture.height) / maxSize;

        // Determine the scale for the extension texture in X and Y direction.
        float xs = paintingRatio / ratioX;
        float ys = paintingRatio / ratioY;

        // Scale and center the texture.
        rend.material.mainTextureScale = new Vector2(xs, ys);
        rend.material.mainTextureOffset = new Vector2(-xs / 2 + 0.5f, -ys / 2 + 0.5f);
    }
}
