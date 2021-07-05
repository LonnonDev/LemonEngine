#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 4) in vec4 FragColor;

out vec4 frag_color;



void main(void)
{
    gl_Position = vec4(aPosition, 1.0);
    frag_color = FragColor;
}