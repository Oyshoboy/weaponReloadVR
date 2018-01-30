# Showcase
<a href="https://www.youtube.com/watch?v=TuSBAilQUN4">![showcase](http://dl3.joxi.net/drive/2018/01/30/0005/1731/378563/63/eb51a70308.jpg)</a> <br><br>

# How to use it ( PistolController.cs ).
1) Download <a href="https://assetstore.unity.com/packages/tools/newtonvr-75712">NVR</a> and <a href="https://assetstore.unity.com/packages/templates/systems/steamvr-plugin-32647">SteamVR</a> assets from AssetStore.

2) Then put on scene NVRPlayer Prefab ( camera rig ).<br>
![Prefab](http://dl4.joxi.net/drive/2018/01/30/0005/1731/378563/63/99aa43ea0c.jpg)<br><br>

3) Install NewtonVR by pressing "Enable SteamVR" in NVRPlayer prefab. <br>
![install](http://dl4.joxi.net/drive/2018/01/30/0005/1731/378563/63/295ebdc043.jpg)<br><br>

4) Inside NVRPlayer(camera rig), select both hands add "SteamVR_TrackedController component(script). <br>
![install](http://dl4.joxi.net/drive/2018/01/30/0005/1731/378563/63/a421820fa4.jpg)<br><br>

5)Now You need a gun with separate slider.<br>
![gun](http://dl3.joxi.net/drive/2018/01/30/0005/1731/378563/63/8afadb97d2.jpg)<br><br>

6)Add Rigidbody and NVRInteractableItem components to the gun.<br>
![gun](http://dl4.joxi.net/drive/2018/01/30/0005/1731/378563/63/5897b84d15.jpg)<br><br>

7) Add all neccessary colliders for the all pistols parts, <b>but do not add colliders on slider yet!</b><br>
![gun](http://dl4.joxi.net/drive/2018/01/30/0005/1731/378563/63/3cd6f5d377.jpg)<br><br>

8) Create empty gameobject and name it "Relative Point". Set on on the Slider height.<br>
![gun](http://dl3.joxi.net/drive/2018/01/30/0005/1731/378563/63/23f423351a.jpg)<br><br>

9) Now make two child colliders inside the slider. First one name "Slider Collider", and second one make a bit bigger and name "Slider Trigger"<br>
![gun](http://dl4.joxi.net/drive/2018/01/30/0005/1731/378563/63/7158192523.jpg)<br><br>

10) "Slider trigger" set as trigger.<br>
![gun](http://dl4.joxi.net/drive/2018/01/30/0005/1731/378563/63/ada10ef192.jpg)<br><br>

11) After that, to the "Slider trigger" add "Pistol Controller" component.<br>
![gun](http://dl4.joxi.net/drive/2018/01/30/0005/1731/378563/63/e6184f1469.jpg)<br><br>

12) Finally set all neccessary variables:<br>
-12.1 <b>Current Object Item</b> - set your gun itself.<br>
-12.2 <b>Relative Point</b> - set your empty gameobject called "Relative point" which we created on the step 8.<br>
-12.3 <b>This gun Slider</b> - add guns Slider gameobject.<br>
![gun](http://dl3.joxi.net/drive/2018/01/30/0005/1731/378563/63/595fb1035f.jpg)<br><br>
<br><br><br>

# Sniper Bolt Controller (sniperBoltController.cs):

All procedure is 80% same as with pistol, but for bolt action we need more movements and different sequence.
So to simplify it for now, I decided to make it using animations.<br><br>

1) You need to add animator controller to the slider (bolt), and record 3 animations: <b>boltIdle, BoltRotate, BoltSlide</b>. Each animation is should be 1 frame length, cuz transition will be automatic.<br>
<b>boltIdle</b> - is just an idle animation.
<b>BoltRotate</b> - is 55 degree rotate animation.
<b>BoltSlide</b> - is the Z axis move animation.

2) In animator controller you need to add 2 float parametrs: <b>BoltRotate, BoltSlide</b>.<br>
![gun](http://dl3.joxi.net/drive/2018/01/30/0005/1731/378563/63/5ad5d2f219.jpg)<br><br>

3) Then add first blend tree and add 2 motions ( <b>boltIdle</b> and <b>BoltRotate</b>) and one another blendtree in "Motion" section. All blending should depend from <b>"BoltRotate"</b> parameter, and set threshold as on screenShot:<br>
![gun](http://dl4.joxi.net/drive/2018/01/30/0005/1731/378563/63/9934c710bd.jpg)<br><br>

4) In second blend tree add 2 motions: (<b>BoltRotate</b> and <b>BoltSlide</b>(I have mistake in animation name - blotSlide :D)) and make them blend under <b>"BoltSlide"</b> parameter.<br>
![gun](http://dl3.joxi.net/drive/2018/01/30/0005/1731/378563/63/e895c13b0b.jpg)<br><br>

That's how it's might look like:<br>
![gun](https://media.giphy.com/media/l3diUcfXWdYiMzHSo/giphy.gif)<br><br>

<b> So, That's it! </b><br>
<b> All contributions are welcome :) </b>
