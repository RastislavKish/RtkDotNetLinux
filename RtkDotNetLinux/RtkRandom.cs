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

namespace Rtk;

public class RtkRandom {

    Random random=new();

    object accessLock=new();

    public int Next()
        {
        lock (accessLock) {
            return random.Next();
            }
        }
    public int Next(int max)
        {
        lock (accessLock) {
            return random.Next(max);
            }
        }
    public int Next(int min, int max)
        {
        lock (accessLock) {
            return random.Next(min, max);
            }
        }

    public Int64 NextInt64()
        {
        lock (accessLock) {
            return random.NextInt64();
            }
        }
    public Int64 NextInt64(Int64 max)
        {
        lock (accessLock) {
            return random.NextInt64(max);
            }
        }
    public Int64 NextInt64(Int64 min, Int64 max)
        {
        lock (accessLock) {
            return random.NextInt64(min, max);
            }
        }
    }
