using System;
using Cairo;
using Gdk;
using Gtk;

namespace Pinta.Core
{
	public class ToolMouseEventArgs : EventArgs
	{
		/// <summary>
		/// Specifies whether the Alt key is currently pressed.
		/// </summary>
		public bool IsAltPressed => State.HasFlag (ModifierType.Mod1Mask);

		/// <summary>
		/// Specifies whether the Control key is currently pressed.
		/// </summary>
		public bool IsControlPressed => State.HasFlag (ModifierType.ControlMask);

		/// <summary>
		/// Specifies whether the left mouse button is currently pressed.
		/// </summary>
		public bool IsLeftMousePressed => State.HasFlag (ModifierType.Button1Mask);

		/// <summary>
		/// Specifies whether the right mouse button is currently pressed.
		/// </summary>
		public bool IsRightMousePressed => State.HasFlag (ModifierType.Button3Mask);

		/// <summary>
		/// Specifies whether the Shift key is currently pressed.
		/// </summary>
		public bool IsShiftPressed => State.HasFlag (ModifierType.ShiftMask);

		public ModifierType State { get; init; }

		/// <summary>
		/// The mouse button being pressed or released, when applicable.
		/// </summary>
		public MouseButton MouseButton { get; init; }

		/// <summary>
		/// The cursor location in canvas coordinates.
		/// </summary>
		public Cairo.Point Point => new Cairo.Point ((int)PointDouble.X, (int)PointDouble.Y);

		/// <summary>
		/// The cursor location in canvas coordinates.
		/// </summary>
		public PointD PointDouble { get; init; }

		public PointD Root { get; init; }

		/// <summary>
		/// The cursor location in window coordinates.
		/// </summary>
		public PointD WindowPoint { get; init; }

		public static ToolMouseEventArgs FromButtonPressEventArgs (Document document, ButtonPressEventArgs e)
		{
			return new ToolMouseEventArgs {
				State = e.Event.State,
				MouseButton = (MouseButton) e.Event.Button,
				PointDouble = document.Workspace.WindowPointToCanvas (e.Event.X, e.Event.Y),
				WindowPoint = e.Event.GetPoint (),
				Root = new PointD (e.Event.XRoot, e.Event.YRoot)
			};
		}

		public static ToolMouseEventArgs FromButtonReleaseEventArgs (Document document, ButtonReleaseEventArgs e)
		{
			return new ToolMouseEventArgs {
				State = e.Event.State,
				MouseButton = (MouseButton) e.Event.Button,
				PointDouble = document.Workspace.WindowPointToCanvas (e.Event.X, e.Event.Y),
				WindowPoint = e.Event.GetPoint (),
				Root = new PointD (e.Event.XRoot, e.Event.YRoot)
			};
		}

		public static ToolMouseEventArgs FromMotionNotifyEventArgs (Document document, MotionNotifyEventArgs e)
		{
			return new ToolMouseEventArgs {
				State = e.Event.State,
				MouseButton = MouseButton.None,
				PointDouble = document.Workspace.WindowPointToCanvas (e.Event.X, e.Event.Y),
				WindowPoint = new PointD (e.Event.X, e.Event.Y),
				Root = new PointD (e.Event.XRoot, e.Event.YRoot)
			};
		}

	}
}
