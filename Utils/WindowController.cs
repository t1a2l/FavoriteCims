using ColossalFramework.UI;
using UnityEngine;

namespace FavoriteCims
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
			this.maX = Input.mousePosition.x;
			this.maY = Input.mousePosition.y;
			this.deltaX = this.maX - this.ComponentToMove.absolutePosition.x;
			this.deltaY = this.maY - this.ComponentToMove.absolutePosition.y * -1f;
		}

		public override void Update()
		{
			bool stop = this.Stop;
			if (!stop)
			{
				bool mouseButton = Input.GetMouseButton(0);
				if (mouseButton)
				{
					this.maX = Input.mousePosition.x;
					this.maY = Input.mousePosition.y;
					this.MousePos = new Vector3(this.maX - this.deltaX, this.maY * -1f + this.deltaY);
					this.ComponentToMove.absolutePosition = this.MousePos;
				}
			}
		}
	}
}
