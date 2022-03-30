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

namespace Rtk.Audio;

public class VSound: IDisposable {

    List<Sound> sounds=new();
    int lastPlayedId=-1;

    public VSound(params string[] paths)
        {
        foreach (string path in paths)
        sounds.Add(new Sound(path));
        }

    public void Play()
        {
        int newId;

        do
        newId=random.Next(0, sounds.Count);
        while (newId==lastPlayedId);

        sounds[newId].Play();

        lastPlayedId=newId;
        }

    public void Clear()
        {
        var localSounds=sounds;
        sounds=new();

        foreach (Sound sound in localSounds)
        sound.Dispose();
        }

    public void Dispose()
        {
        Clear();
        }

    static RtkRandom random=new();

    }
