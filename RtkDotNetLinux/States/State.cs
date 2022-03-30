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

public class State {

    public string Title
        {
        get => title;
        set {
            title=value;
            stateManager.SetTitle(title);
            }
        }
    private string title="";

    protected ShortcutManager shortcuts=new();
    protected StateManager stateManager;

    public State(StateManager stateManager)
        {
        this.stateManager=stateManager;
        }

    public virtual void Activate()
        {

        }
    public virtual void Deactivate()
        {

        }

    public virtual void KeyPressHandler(KeyPressEventArgs e)
        {
        shortcuts.ExecuteShortcut(Shortcut.FromKeyPressEventArgs(e));
        }

    }
