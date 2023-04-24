Start Scene:
- Filename : Assets/Scenes/StartScene


How to play the game:
- The game starts at the top of the cliff. 
- The player's objective is to drive the car downhill and reach the finish line within the given time limit.
- But beware of the police cars present throughout the road ready to chase the player's car and push you down the hill.
- There are power ups to increase or reduce the time remaining which the player might want to collect to make sure they reach the finish line in time.
- There are nitro boosts available to be picked up that allow you to choose when to give your vehicle a speed boost.
- There are various obstacles on the course that you need to avoid - speed breakers, rotating walls, spinning blocks, grassy patches etc.
- The car gets backtracked a few seconds if the player gets off the track.

Key parts to focus:
1. Vehicle physics & animation:
  - The car has dynamic steering which means the slower the car is, the higher the steering angle you will have.
  - The car has drag physics to make it more realistic when handling and stopping.
  - The car has wheel physics which makes turning the car realistic.
  - There are wheel turning and suspension animations using physics to give the user realitic car animations.
  - The car reset uses physics raycast to determine if it is on the road.
2. AI:
 - We have AI states for the police cars which makes them idle or start chasing the player car.
 - AI cars are also governed by vehicle physics.
 - We used NavMesh to calculate the path to the player car. 
 - We use the next generated waypoint and NavMesh raycast to calculate the streering inputs for the AI which then steer the car according to the vehicle physics.
3. Sound:
 - The car's engine noise changes based on speed and acceleration.
4. Lights:
 - The street lamps are player aware. Some of the lamps only light up when the player's car is nearby to illuminate their surroundings.
5. Others:
 - The player can't exit the world because the car is reset back to the track every time it wanders off and it won't have enough time to wander off the world.


Problem areas:
- Audio experience could be enhanced. It's not as polished as we would like. Eg: Audio does not pause when the game is paused.
- The camera can cut through the terrain and give an inside view in some very rare cases. We were not able to perfect the clipping plane for all scenarios.
- The car's wheel traction is not high enough in some cases such as climbing uphill or stopping from reversing which leads to a subpar experience.
- The finish line area is not as polished as we would have liked.


Manifest
1. Team Contributions
	A. Nitish
		- Created the terrain
		- Created the graphical layers on top of the terrain to give it a realistic look
		- Created the road network by hand (placing each node manually)
		- Configured physics for vehicles to make them driveable - Dynamic steering, drag mechanics etc.
		- Added braking lights on the vehicles
		- Created the logic for resetting the car back to the track when it goes off the track using physics raycast
		- Configured the skybox and directional light
		- Worked on the police car AI and prefabs
		- Implemented the AI logic - Navmesh path logic, raycast hit to determine steering inputs etc.
		- Added lighting such as police siren, spotlights on pickups, player aware light lamps on road side
		- Improved the AI mechanics and state machine
		- Improved the car control and animations of the wheels
		- Aided in complete testing and bug bash, including bug fixes
	B. Priyank
		- Added the car model for player's car
		- Added the camera scripts for the camera to follow the player
		- Added car controller script to control the car using car physics
		- Added car dashboard to show car speed, timer and fps
		- Added game playing logic around countdown timer and startline and finishline
		- Polished the car dashboard with new fonts and icons.
		- Added counters for pickups for power ups and nitros.
	C. Jetin
		- Configured the wheel colliders of the AI car and was involved in making the decisions for tracking logic of AI car
		- Made the videos for project submissions
		- Added all the audio of the game
		- Added rotating obstacles to the game
		- Added all the narration scenes (splashscreens) to the game
		- Improved the game over experience by disabling the controls, ai bots chase and sounds
		- Helped the team debug and resolve bugs
	D. Vijayasai
		- Pause Menu design and script
		- Settings menu design and script
		- Credits menu design and script
		- Game rules design and script
		- Start Game menu and script
		- Added axes in input manager for nitro and escape for both keyboard and controller and tested them
		- Added speed bumps and grass patches with grasspatchfriction material with friction of 1000.
		- Modified the credits, game rules and settings in the canvas section.
		- Added restart game script
	E. Rahul
		- Inserted code for power pickups (red and green).
		- Inserted police car.
		- Did initial research for AI for the police car.
		- Adding code and pickup object Nitro
		- Start and finish line graphics
		- Modifying green and red pickup behavior to affect time instead of speed
		- Recording gameplay video
2. Assets/
  A. Material/
 	- Brake Light: Nitish
 	- Default Physics: Priyank
	- bw_texture: Rahul
	- Green Patch Friction: Vijayasai
	- Nitro: Rahul
	- Spinning block: Jetin
	- Start Finish Line: Rahul
  B. Prefabs/
  	- Interceptor: Rahul, Jetin, Nitish
	- Horizontal block: Jetin
	- Spinning block: Jetin
3. Assets/Scripts/
	- GameControls/: All scripts(14) under this folder by Vijayasai
	- CarDriveV2: Priyank, Nitish, Jetin, Rahul
	- CarResetLogic: Nitish
	- HorizontalRotation: Jetin
	- LightHandler: Nitish
	- PoliceAIV2: Nitish, Jetin, Priyank
	- Rotator: Rahul, Nitish
	- SpinningBlock: Jetin
	- ThirdPersonCamera: Priyank