StyleBlit Unity Plugin
===============================

Example scenes included in this package show all possible uses of the plugin.
When in doubt, just follow the plugin settings on the Golem object and the camera of your preferred stylization method scene.

Basic usage:
-------------------------------

1) Open your project and unpack the StyleBlit package (click it open or drag it into the project).
	For quick showcase, just open either example scene and play it.

2) The most effective usage method is the image effect. Just grab the StyleBlit Multi Image Effect script from StyleBlit/ImageEffect
	folder and attach it to the camera. Then attach the Style ID Script from the same folder on each object you want to stylize.

3) Set your desired source style texture in the inspector of your object. 
	There are some other settings of the effect you might want to experiment with, just see the inspector of the camera.

4) For the flickering effect to take place, a script Jitters in the StyleBlit/Scripts folder must be attached to any scene object 
	(preferably the camera object). The script also adjusts FPS and StyleBlit refresh rate.
	If you don't intend to experiment with the jittering texture, just leave the script to it, it will find the jitter texture itself.

5) And that's about it!

-------------------------------
Material usage:
-------------------------------

o) The image effect only affects the scene, if it's played. There's also another variant of the stylization which utilizes Unity
	materials. You can see an example in the MaterialExample scene. To use it, just swap steps 2 and 3 above with those below.

2) Grab the two materials, StyleBlitA and StyleBlitB, from StyleBlit/Material folder and attach them both to the Mesh Renderer in the 
	object's respective mesh inspector.
	(You'll have to set Materials to the Size of 2. See included Golem object for how-to.)

3) Set your desired source style texture in the inspector of StyleBlitB material. 
	There are some other settings of the materials you might want to experiment with, just see their inspectors.

ad 4) The flickering utilizes the same Jitter texture for all objects with StyleBlit material attached to them, therefore each stylized
	object will share the same flickering effect.

o) The two-component version of the material is preferable to be used, as it is more effective.
	For your convenience though, a standalone shader version and material is included as well.


Pro-tips:
--------------------------------

Multi-material:	You can combine as many styles as you want. For each material, just create a new StyleBlitB clone:
			Use shader StyloB and set it in any style you want. Then proceed as in the step 2, just replacing
			StyleBlitB material with your new one.

LUT generator: 	If you want to experiment with different guidance shapes and patterns, you could make use of LUT generator, providing you
			with different LUT for your source normal textures. You can find it in StyleBlit menu item under LUT generator.

________________________________

Have fun!
			