namespace DeJong.Utilities.Binary
{
    using System;
    using Core;

    /// <summary>
    /// Contains general functions used for reading binary data.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerStepThrough]
#endif
    public static class BitReader
    {
        /// <summary>
        /// Read a full or partial byte from a specified buffer.
        /// </summary>
        /// <param name="source"> The source buffer. </param>
        /// <param name="offset"> The starting point (in bits). </param>
        /// <param name="length"> The amount of bits to read. </param>
        /// <returns> The bits that have been read in a byte. </returns>
        /// <remarks>
        /// Check if length in withing range
        /// Check if the offset if byte alligned and we need the full byte (possible return)
        /// Get the needed part of the first byte
        /// Check if the next byte is needed (possible return)
        /// Get the needed part of the next byte
        /// Compute result byte
        /// </remarks>
        public static byte ReadByte(byte[] source, int offset, int length)
        {
            CheckOverflow(nameof(ReadByte), length, 8);

            int byteIndex = offset >> 3;
            int start = offset - (byteIndex << 3);
            if (start == 0 && length == 8) return source[byteIndex];

            byte result = (byte)(source[byteIndex] >> start);
            int bitsInNextByte = length - (8 - start);
            if (bitsInNextByte < 1) return (byte)(result & (255 >> (8 - length)));

            byte nextByte = source[byteIndex + 1];
            nextByte &= (byte)(255 >> (8 - bitsInNextByte));
            return (byte)(result | (byte)(nextByte << (length - bitsInNextByte)));
        }

        /// <summary>
        /// Reads a full or partial ushort from a specified buffer.
        /// </summary>
        /// <param name="source"> The source buffer. </param>
        /// <param name="offset"> The starting point (int bits). </param>
        /// <param name="length"> The amount of bits to read. </param>
        /// <returns> The bits that have been read in a ushort. </returns>
        public static ushort ReadUInt16(byte[] source, int offset, int length)
        {
            CheckOverflow(nameof(ReadUInt16), length, 16);
            return (ushort)ReadVariableBytes(source, offset, length, sizeof(ushort));
        }

        /// <summary>
        /// Reads a full or partial uint from a specified buffer.
        /// </summary>
        /// <param name="source"> The source buffer. </param>
        /// <param name="offset"> The starting point (int bits). </param>
        /// <param name="length"> The amount of bits to read. </param>
        /// <returns> The bits that have been read in a uint. </returns>
        public static uint ReadUInt32(byte[] source, int offset, int length)
        {
            CheckOverflow(nameof(ReadUInt32), length, 32);
            return (uint)ReadVariableBytes(source, offset, length, sizeof(uint));
        }

        /// <summary>
        /// Reads a full or partial ulong from a specified buffer.
        /// </summary>
        /// <param name="source"> The source buffer. </param>
        /// <param name="offset"> The starting point (int bits). </param>
        /// <param name="length"> The amount of bits to read. </param>
        /// <returns> The bits that have been read in a ulong. </returns>
        public static ulong ReadUInt64(byte[] source, int offset, int length)
        {
            CheckOverflow(nameof(ReadUInt64), length, 64);
            return ReadVariableBytes(source, offset, length, sizeof(ulong));
        }

        /// <summary>
        /// Reads a specified amount of bytes from a specified buffer.
        /// </summary>
        /// <param name="source"> The source bufer. </param>
        /// <param name="srcOffset"> The starting point (in bits). </param>
        /// <param name="length"> The amount of bytes to read. </param>
        /// <param name="destination"> The destination buffer. </param>
        /// <param name="destOffset"> The starting point (in bytes). </param>
        public static void ReadBytes(byte[] source, int srcOffset, int length, byte[] destination, int destOffset)
        {
            int byteIndex = srcOffset >> 3;
            int start = srcOffset - (byteIndex << 3);

            if (start == 0) Buffer.BlockCopy(source, srcOffset, destination, destOffset, length);
            else
            {
                int nextPartLen = 8 - start;
                int nextMask = 255 >> nextPartLen;

                for (int i = 0; i < length; i++)
                {
                    int first = source[byteIndex++] >> start;
                    int second = source[byteIndex] & nextMask;
                    destination[destOffset++] = (byte)(first | (second << nextPartLen));
                }
            }
        }

        private static ulong ReadVariableBytes(byte[] source, int offset, int length, int numBytes)
        {
            ulong result = 0;
            for (int i = 0; i < numBytes && length > 0; i++, length -= 8, offset += 8)
            {
                if (length <= 8) result |= (ulong)ReadByte(source, offset, length) << (i << 3);
                else result |= (ulong)ReadByte(source, offset, 8) << (i << 3);
            }
            return result;
        }

        private static void CheckOverflow(string func, int check, int max)
        {
            LoggedException.RaiseIf(check < 1 || check > max, nameof(BitReader), $"{func} can only read between 1 and {max} bits");
        }
    }
}