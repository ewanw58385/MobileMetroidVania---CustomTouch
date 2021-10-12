
ScreenWidth / 2 to make sure touch is only registered in left part of screen   


Get position of initial touch (touch.began)

Translate to vector2 using "camera screen to world point"

get position of touch.moved and translate to vector2 same as above ^


get position of gameObject (middle point of joystick)

get offset of position (position touched - gameObject position)

Move player using this vector offset 


