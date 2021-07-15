#version 330

out vec4 FragColor;
out vec4 outputColor;

in vec2 texCoord;

uniform vec4 objColor;
uniform sampler2D texture0;

void main()
{
    outputColor = texture(texture0, texCoord);
    FragColor = objColor;
}