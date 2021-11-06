using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using OpenTK.Platform;
using System.IO;
namespace Niculica_Bogdan_3132b_Lab3
{
    class Camera
    {
		private static double s_angularSpeed = 1000 / 20.0; // speed of angular camera movement in degrees/s
		private static double s_linearSpeed = 1000 / 7.5;       // speed of linear camera movement in units/s

		private Vector3d m_position;
		private Vector3d m_nVector;
		private Vector3d m_uVector;
		private Vector3d m_vVector;


		public Camera(Vector3d position, Vector3d look, Vector3d up)
		{
			m_position = position;

			m_nVector = look;
			//m_nVector.normalize();
			m_nVector.Normalize();

			m_vVector = up;
			m_vVector.Normalize();

		//	m_uVector = m_vVector.cross(m_nVector).normalize();
			m_uVector = Vector3d.Cross(m_vVector, m_nVector);
			m_uVector.Normalize();
		}
		public void rotate(Vector3d axis, double angle)
		{
			// Note: We try and optimise things a little by observing that there's no point rotating
			// an axis about itself and that generally when we rotate about an axis, we'll be passing
			// it in as the parameter axis, e.g. camera.rotate(camera.get_n(), Math.PI/2).
			//		if (axis != m_nVector) m_nVector = MathUtil.rotate_about_axis(m_nVector, angle, axis);
			//	if (axis != m_uVector) m_uVector = MathUtil.rotate_about_axis(m_uVector, angle, axis);
			//	if (axis != m_vVector) m_vVector = MathUtil.rotate_about_axis(m_vVector, angle, axis);
			
				rotate(m_nVector, angle);
			
		}


	}
}
