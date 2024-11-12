using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims.Utils
{
	public class WindowController : UIDragHandle
	{
        private Vector3 MousePos;

        private float maX;

        private float maY;

        private float deltaX;

        private float deltaY;

        public UIComponent ComponentToMove;

        public bool Stop = false;

        public override void Start()
		{
			maX = Input.mousePosition.x;
			maY = Input.mousePosition.y;
			deltaX = maX - ComponentToMove.absolutePosition.x;
			deltaY = maY - ComponentToMove.absolutePosition.y * -1f;
		}

		public override void Update()
		{
			bool stop = Stop;
			if (!stop)
			{
				bool mouseButton = Input.GetMouseButton(0);
				if (mouseButton)
				{
					maX = Input.mousePosition.x;
					maY = Input.mousePosition.y;
					MousePos = new Vector3(maX - deltaX, maY * -1f + deltaY);
					ComponentToMove.absolutePosition = MousePos;
				}
			}
		}
	}
}
