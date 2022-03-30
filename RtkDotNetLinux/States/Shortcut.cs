/*
* Copyright (C) 2022 Rastislav Kish
*
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU Lesser General Public License as published by
* the Free Software Foundation, version 2.1.
*
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU Lesser General Public License for more details.
*
* You should have received a copy of the GNU Lesser General Public License
* along with this program. If not, see <https://www.gnu.org/licenses/>.
*/

using Gtk;

namespace Rtk.States;

public class Shortcut {

    public bool Control {get; private set;}
    public bool Shift {get; private set;}
    public bool Alt {get; private set;}
    public ushort Key {get; private set;}

    public Shortcut(bool control, bool shift, bool alt, States.Key key)
        {
        Control=control;
        Shift=shift;
        Alt=alt;
        Key=(ushort)key;
        }
    public Shortcut(bool control, bool shift, bool alt, ushort key)
        {
        Control=control;
        Shift=shift;
        Alt=alt;
        Key=key;
        }

    public override int GetHashCode()
        {
        return Key.GetHashCode();
        }
    public override bool Equals(Object? obj)
        {
        if (obj is not Shortcut)
            {
            return false;
            }
        Shortcut shortcut=(Shortcut)obj;

        return (Key==shortcut.Key && Control==shortcut.Control && Shift==shortcut.Shift && Alt==shortcut.Alt);
        }

    public static Shortcut FromKeyPressEventArgs(KeyPressEventArgs e)
        {
        bool control=(e.Event.State & Gdk.ModifierType.ControlMask)==Gdk.ModifierType.ControlMask;
        bool shift=(e.Event.State & Gdk.ModifierType.ShiftMask)==Gdk.ModifierType.ShiftMask;
        bool alt=(e.Event.State & Gdk.ModifierType.Mod1Mask)==Gdk.ModifierType.Mod1Mask;
        ushort key=e.Event.HardwareKeycode;

        return new Shortcut(control, shift, alt, key);
        }
    }
