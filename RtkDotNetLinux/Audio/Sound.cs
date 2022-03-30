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

using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenTK.Audio.OpenAL.Extensions.EXT.Float32;
using OpenTK.Audio.OpenAL.Extensions.EXT.FloatFormat;

using NVorbis;

namespace Rtk.Audio;

public class Sound: IDisposable {

    public bool Playing
        {
        get {
            AL.GetSource(source, ALGetSourcei.SourceState, out var state);
            return (ALSourceState)state==ALSourceState.Playing;
            }
        }

    int source;
    int buffer;

    bool looping=false;

    public Sound()
        {
        buffer=AL.GenBuffer();
        source=AL.GenSource();
        AL.Source(source, ALSourcei.Buffer, buffer);
        }
    public Sound(string path)
        {
        buffer=AL.GenBuffer();
        source=AL.GenSource();

        Load(path);
        }

    public void Load(string path)
        {
        if (!path.StartsWith("/") && soundsDirectory is not null)
        path=Path.Combine(soundsDirectory, path);

        using var reader=new VorbisReader(path);

        reader.ClipSamples=false;

        var channels=reader.Channels;
        var samplingRate=reader.SampleRate;

        var soundData=new float[channels*reader.TotalSamples];
        reader.ReadSamples(soundData, 0, soundData.Length);

        EXTFloat32.BufferData(buffer, (channels==2) ? FloatBufferFormat.Stereo: FloatBufferFormat.Mono, soundData, samplingRate);
        AL.Source(source, ALSourcei.Buffer, buffer);
        }

    public void Play()
        {
        AL.SourcePlay(source);
        }
    public void PlayLooped()
        {
        looping=true;

        AL.Source(source, ALSourceb.Looping, true);
        Play();
        }
    public void PlaySync()
        {
        Play();
        while (Playing)
        Thread.Sleep(10);
        }
    public void Replay()
        {
        Stop();
        Play();
        }
    public void ReplaySync()
        {
        Stop();
        PlaySync();
        }
    public void Stop()
        {
        if (looping) {
            AL.Source(source, ALSourceb.Looping, false);
            looping=false;
            }
        AL.SourceStop(source);
        }

    public void Dispose()
        {
        AL.SourceStop(source);
        AL.DeleteSource(source);
        AL.DeleteBuffer(buffer);
        }

    // Static part

    static ALDevice? device=null;
    static string? soundsDirectory=null;

    public static void Initialize()
        {
        device=ALC.OpenDevice(null);
        var context=ALC.CreateContext(device ?? throw new Exception("Device not initialized"), new ALContextAttributes());
        ALC.MakeContextCurrent(context);
        }
    public static void Deinitialize()
        {
        if (device is not null) {
            ALC.CloseDevice(device ?? throw new Exception("Device not initialized"));
            device=null;
            }
        }

    public static void SetSoundsDirectory(string path)
    => soundsDirectory=path;

    }
