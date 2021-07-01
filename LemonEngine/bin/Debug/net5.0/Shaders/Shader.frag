#version 330 core
out vec4 FragColor;



void main()
{
    FragColor = RGBA(0, 0, 0, 255);
}

vec4 RGBA(float Red, float Green, float Blue, float Alpha) {
    return vec4(Red/255f, Green/255f, Blue/255f, Alpha/255f);
}