#ifdef GL_ES
    precision mediump float;
#endif

varying vec4 v_Colour;
varying vec2 v_Position;

uniform float m_thickness;
uniform float m_radius;
uniform bool m_filled;
uniform vec2 m_centerpos;
uniform vec2 m_size;

float udRoundBox(vec2 p, vec2 b, float r, float thickness)
{
	float f1 = length(max(abs(p) - b + r, 0.0)) - r;
	float f2 = length(max(abs(p) - (b - thickness) + r, 0.0)) - r;

	return m_filled ? f1 : f1 * f2;
}

void main(void)
{
	float b = udRoundBox(v_Position - m_centerpos, m_size / 2, m_radius, m_thickness);

    vec4 c = mix(v_Colour, vec4(0.0, 0.0, 0.0, 1.0), smoothstep(0.0, 1.0, b));
        
    gl_FragColor = c;
}