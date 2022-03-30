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

namespace Rtk.States;

public class ShortcutManager {

    private Dictionary<Shortcut, Action> shortcuts=new();

    public void AddShortcut(bool control, bool shift, bool alt, Key key, Action action)
        {
        shortcuts[new Shortcut(control, shift, alt, key)]=action;
        }
    public void ExecuteShortcut(Shortcut shortcut)
        {
        if (shortcuts.TryGetValue(shortcut, out var action))
        action();
        }
    }
