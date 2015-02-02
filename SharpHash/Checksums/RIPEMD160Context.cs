﻿//
//  RIPEMD160Context.cs
//
//  Author:
//       Natalia Portillo <claunia@claunia.com>
//
//  Copyright (c) 2015 © Claunia.com
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SharpHash.Checksums
{
    /// <summary>
    /// Provides a UNIX similar API to .NET RIPEMD160.
    /// </summary>
    public class RIPEMD160Context
    {
        RIPEMD160 _ripemd160Provider;

        /// <summary>
        /// Initializes the RIPEMD160 hash provider
        /// </summary>
        public void Init()
        {
            _ripemd160Provider = RIPEMD160.Create();
        }

        /// <summary>
        /// Updates the hash with data.
        /// </summary>
        /// <param name="data">Data buffer.</param>
        /// <param name="len">Length of buffer to hash.</param>
        public void Update(byte[] data, uint len)
        {
            _ripemd160Provider.TransformBlock(data, 0, (int)len, data, 0);
        }

        /// <summary>
        /// Updates the hash with data.
        /// </summary>
        /// <param name="data">Data buffer.</param>
        public void Update(byte[] data)
        {
            Update(data, (uint)data.Length);
        }

        /// <summary>
        /// Returns a byte array of the hash value.
        /// </summary>
        public byte[] Final()
        {
            _ripemd160Provider.TransformFinalBlock(new byte[0], 0, 0);
            return _ripemd160Provider.Hash;
        }

        /// <summary>
        /// Returns a hexadecimal representation of the hash value.
        /// </summary>
        public string End()
        {
            _ripemd160Provider.TransformFinalBlock(new byte[0], 0, 0);
            StringBuilder ripemd160Output = new StringBuilder();

            for (int i = 0; i < _ripemd160Provider.Hash.Length; i++)
            {
                ripemd160Output.Append(_ripemd160Provider.Hash[i].ToString("x2"));
            }

            return ripemd160Output.ToString();
        }

        /// <summary>
        /// Gets the hash of a file
        /// </summary>
        /// <param name="filename">File path.</param>
        public byte[] File(string filename)
        {
            FileStream fileStream = new FileStream(filename, FileMode.Open);
            return _ripemd160Provider.ComputeHash(fileStream);
        }

        /// <summary>
        /// Gets the hash of a file in hexadecimal and as a byte array.
        /// </summary>
        /// <param name="filename">File path.</param>
        /// <param name="hash">Byte array of the hash value.</param>
        public string File(string filename, out byte[] hash)
        {
            FileStream fileStream = new FileStream(filename, FileMode.Open);
            hash = _ripemd160Provider.ComputeHash(fileStream);
            StringBuilder ripemd160Output = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                ripemd160Output.Append(hash[i].ToString("x2"));
            }

            return ripemd160Output.ToString();
        }

        /// <summary>
        /// Gets the hash of the specified data buffer.
        /// </summary>
        /// <param name="data">Data buffer.</param>
        /// <param name="len">Length of the data buffer to hash.</param>
        /// <param name="hash">Byte array of the hash value.</param>
        public string Data(byte[] data, uint len, out byte[] hash)
        {
            hash = _ripemd160Provider.ComputeHash(data, 0, (int)len);
            StringBuilder ripemd160Output = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                ripemd160Output.Append(hash[i].ToString("x2"));
            }

            return ripemd160Output.ToString();
        }

        /// <summary>
        /// Gets the hash of the specified data buffer.
        /// </summary>
        /// <param name="data">Data buffer.</param>
        /// <param name="hash">Byte array of the hash value.</param>
        public string Data(byte[] data, out byte[] hash)
        {
            return Data(data, (uint)data.Length, out hash);
        }
    }
}

