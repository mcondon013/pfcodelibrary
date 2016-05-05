//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomData
{
    /// <summary>
    /// Generates random numeric values for various .NET numeric types.
    /// </summary>
    public class RandomNumber
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private Random _random = new Random();

        //private varialbles for properties

        //constructors
        /// <summary>
        /// Constructor for the class. Does nothing.
        /// </summary>
        public RandomNumber()
        {
            ;
        }

        //properties

        //methods
        private double GenerateRandomInteger(double min, double max)
        {
            return (_random.NextDouble() * (max - min + 1)) + min;
        }

        private double GenerateRandomDouble(double min, double max)
        {
            return (_random.NextDouble() * (max - min)) + min;
        }
        
        /// <summary>
        /// Generates an int32 number in the specified range.
        /// </summary>
        /// <param name="min">Output will be equal to or greater than this number.</param>
        /// <param name="max">Output will be less than this number.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to min and less than or equal to max
        /// </returns>
        /// <remarks>Uses Next method of the .NET Framework Random class.</remarks>
        public int GenerateRandomInt(int min, int max)
        {
            return _random.Next(min, max+1);
        }
        /// <summary>
        /// Generates random integer with a value within min to max range.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>Returns random int32.</returns>
        /// <remarks>Method first generates a random double which it converts to an integer value.</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public int GenerateRandomNumber(int min, int max)
        {
            //return (int)GenerateRandomNumber((double)min, (double)max);
            return (int)GenerateRandomInteger((double)min, (double)max);
        }
        /// <summary>
        /// Generates random double.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>Double</returns>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        /// 
        public double GenerateRandomNumber(double min, double max)
        {
            //return (_random.NextDouble() * (max - min + 1)) + min;
            return GenerateRandomDouble(min, max);
        }

        /// <summary>
        /// Generates random float.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>float</returns>
        public float GenerateRandomNumber(float min, float max)
        {
            return (float)GenerateRandomDouble((double)min, (double)max);
        }

        /// <summary>
        /// Generates random unsigned integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>uint</returns>
        public uint GenerateRandomNumber(uint min, uint max)
        {
            return (uint)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random long integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>long</returns>
        public long GenerateRandomNumber(long min, long max)
        {
            return (long)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random unsigned long integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>ulong</returns>
        public ulong GenerateRandomNumber(ulong min, ulong max)
        {
            return (ulong)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random short integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>short</returns>
        public short GenerateRandomNumber(short min, short max)
        {
            return (short)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random unsigned short integer.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>ushort</returns>
        public ushort GenerateRandomNumber(ushort min, ushort max)
        {
            return (ushort)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random decimal number.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>decimal</returns>
        public decimal GenerateRandomNumber(decimal min, decimal max)
        {
            return (decimal)GenerateRandomDouble((double)min, (double)max);
        }

        /// <summary>
        /// Generates random byte.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>byte</returns>
        public byte GenerateRandomNumber(byte min, byte max)
        {
            return (byte)GenerateRandomInteger((double)min, (double)max);
        }

        /// <summary>
        /// Generates random signed byte.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <returns>sbyte</returns>
        public sbyte GenerateRandomNumber(sbyte min, sbyte max)
        {
            return (sbyte)GenerateRandomInteger((double)min, (double)max);
        }

        //************************************************************************************************
        //Functions to generate arrays of random numbers defined next
        //************************************************************************************************

        /// <summary>
        /// Method generates an array of random int values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>
        /// An array of 32-bit signed integers, each greater than or equal to min and less than or equal to max.
        /// </returns>
        /// <remarks>Uses Next method of the .NET Framework Random class.</remarks>
        public int[] GenerateRandomInt(int min, int max, int arraySize)
        {
            int[] randomNumbers = new int[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = _random.Next(min, max + 1);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>int[] array</returns>
        /// <remarks>Method first generates a random double which it converts to an integer value.</remarks>
        public int[] GenerateRandomNumber(int min, int max, int arraySize)
        {
            int[] randomNumbers = new int[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (int)GenerateRandomInteger((double)min, (double)max); 
            }

            return randomNumbers;
        }


        /// <summary>
        /// Method generates an array of random double values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>double[] array</returns>
        public double[] GenerateRandomNumber(double min, double max, int arraySize)
        {
            double[] randomNumbers = new double[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = GenerateRandomDouble(min, max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random float values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>float[] array</returns>
        public float[] GenerateRandomNumber(float min, float max, int arraySize)
        {
            float[] randomNumbers = new float[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (float)GenerateRandomDouble((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random unsigned integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>uint[] array</returns>
        public uint[] GenerateRandomNumber(uint min, uint max, int arraySize)
        {
            uint[] randomNumbers = new uint[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (uint)GenerateRandomInteger((double)min, (double)max); 
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random long integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>long[] array</returns>
        public long[] GenerateRandomNumber(long min, long max, int arraySize)
        {
            long[] randomNumbers = new long[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (long)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random unsigned long integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>ulong[] array</returns>
        public ulong[] GenerateRandomNumber(ulong min, ulong max, int arraySize)
        {
            ulong[] randomNumbers = new ulong[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (ulong)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random short integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>short[] array</returns>
        public short[] GenerateRandomNumber(short min, short max, int arraySize)
        {
            short[] randomNumbers = new short[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (short)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random unsigned short integer values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>ushort[] array</returns>
        public ushort[] GenerateRandomNumber(ushort min, ushort max, int arraySize)
        {
            ushort[] randomNumbers = new ushort[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (ushort)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }


        /// <summary>
        /// Method generates an array of random unsigned decimal values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>decimal[] array</returns>
        public decimal[] GenerateRandomNumber(decimal min, decimal max, int arraySize)
        {
            decimal[] randomNumbers = new decimal[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (decimal)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random byte values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>byte[] array</returns>
        public byte[] GenerateRandomNumber(byte min, byte max, int arraySize)
        {
            byte[] randomNumbers = new byte[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (byte)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Method generates an array of random signed byte values.
        /// </summary>
        /// <param name="min">Min value to generate.</param>
        /// <param name="max">Max value to generate.</param>
        /// <param name="arraySize">Number of random numbers to generate.</param>
        /// <returns>sbyte[] array</returns>
        public sbyte[] GenerateRandomNumber(sbyte min, sbyte max, int arraySize)
        {
            sbyte[] randomNumbers = new sbyte[arraySize];

            for (int inx = 0; inx < arraySize; inx++)
            {
                randomNumbers[inx] = (sbyte)GenerateRandomInteger((double)min, (double)max);
            }

            return randomNumbers;
        }


        //offset methods follow

        private double OffsetInteger(double numToModify, double minPercent, double maxPercent)
        {
            double min = numToModify * minPercent;
            double max = numToModify * maxPercent;
            double offset = (_random.NextDouble() * (max - min + 1)) + min;
            return numToModify + offset;
        }

        private double OffsetDouble(double numToModify, double minPercent, double maxPercent)
        {
            double min = numToModify * minPercent;
            double max = numToModify * maxPercent;
            double offset = (_random.NextDouble() * (max - min + 1)) + min;
            return numToModify + offset;
        }

        /// <summary>
        /// Modifies an int32 number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A 32-bit signed integer greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public int OffsetInt(int numToModify, double minPercent, double maxPercent)
        {
            return (int)OffsetInteger((double)numToModify, minPercent, maxPercent);
        }

        /// <summary>
        /// Modifies an int32 number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A 32-bit signed integer greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number..</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public int OffsetNumber(int numToModify, double minPercent, double maxPercent)
        {
            return (int)OffsetInteger((double)numToModify, minPercent, maxPercent);
        }


        /// <summary>
        /// Modifies a UInt32 number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A 32-bit unsigned integer greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number..</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public uint OffsetNumber(uint numToModify, double minPercent, double maxPercent)
        {
            return (uint)OffsetInteger((double)numToModify, minPercent, maxPercent);
        }


        /// <summary>
        /// Modifies a long number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A 64-bit signed integer greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number..</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public long OffsetNumber(long numToModify, double minPercent, double maxPercent)
        {
            return (long)OffsetInteger((double)numToModify, minPercent, maxPercent);
        }


        /// <summary>
        /// Modifies an unsigned long number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A 64-bit unsigned integer greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number..</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public ulong OffsetNumber(ulong numToModify, double minPercent, double maxPercent)
        {
            return (ulong)OffsetInteger((double)numToModify, minPercent, maxPercent);
        }

        /// <summary>
        /// Modifies a short number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A 16-bit signed integer greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number..</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public short OffsetNumber(short numToModify, double minPercent, double maxPercent)
        {
            return (short)OffsetInteger((double)numToModify, minPercent, maxPercent);
        }


        /// <summary>
        /// Modifies an unsigned short number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A 16-bit unsigned integer greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number..</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public ushort OffsetNumber(ushort numToModify, double minPercent, double maxPercent)
        {
            return (ushort)OffsetInteger((double)numToModify, minPercent, maxPercent);
        }

        /// <summary>
        /// Modifies a signed byte by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An 8-bit signed byte greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number..</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public sbyte OffsetNumber(sbyte numToModify, double minPercent, double maxPercent)
        {
            return (sbyte)OffsetInteger((double)numToModify, minPercent, maxPercent);
        }


        /// <summary>
        /// Modifies a byte by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An 8-bit unsigned byte greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number..</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        public byte OffsetNumber(byte numToModify, double minPercent, double maxPercent)
        {
            return (byte)OffsetInteger((double)numToModify, minPercent, maxPercent);
        }

        /// <summary>
        /// Modifies a double number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A double number greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        /// 
        public double OffsetNumber(double numToModify, double minPercent, double maxPercent)
        {
            return OffsetDouble(numToModify, minPercent, maxPercent);
        }

        /// <summary>
        /// Modifies a float number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A float number greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        /// 
        public float OffsetNumber(float numToModify, double minPercent, double maxPercent)
        {
            return (float)OffsetDouble((double)numToModify, minPercent, maxPercent);
        }

        /// <summary>
        /// Modifies a decimal number by applying an offset that is within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numToModify">Number which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>A decimal number greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        /// <example>
        /// <code>
        /// </code>
        /// </example>
        /// 
        public decimal OffsetNumber(decimal numToModify, double minPercent, double maxPercent)
        {
            return (decimal)OffsetDouble((double)numToModify, minPercent, maxPercent);
        }




        //array processing methods

        private double[] OffsetInteger(double[] numsToModify, double minPercent, double maxPercent)
        {
            double[] offsetNums = new double[numsToModify.Length];
            double offsetMin = 0.0;
            double offsetMax = 0.0;
            double offset = 0.0;

            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetMin = numsToModify[i] * minPercent;
                offsetMax = numsToModify[i] * maxPercent;
                offset = (_random.NextDouble() * (offsetMax - offsetMin + 1)) + offsetMin;
                offsetNums[i] = numsToModify[i] + offset;
            }
            return offsetNums;
        }

        private double[] OffsetDouble(double[] numsToModify, double minPercent, double maxPercent)
        {
            double[] offsetNums = new double[numsToModify.Length];
            double offsetMin = 0.0;
            double offsetMax = 0.0;
            double offset = 0.0;

            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetMin = numsToModify[i] * minPercent;
                offsetMax = numsToModify[i] * maxPercent;
                offset = (_random.NextDouble() * (offsetMax - offsetMin + 1)) + offsetMin;
                offsetNums[i] = numsToModify[i] + offset;
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of int32 numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of 32-bit signed integers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public int[] OffsetInt(int[] numsToModify, double minPercent, double maxPercent)
        {
            int[] offsetNums = new int[numsToModify.Length];
            double offsetMin = 0.0;
            double offsetMax = 0.0;
            double offset = 0.0;

            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetMin = (double)numsToModify[i] * minPercent;
                offsetMax = (double)numsToModify[i] * maxPercent;
                offset = (_random.NextDouble() * (offsetMax - offsetMin + 1)) + offsetMin;
                offsetNums[i] = numsToModify[i] + (int)offset;
            }
            return offsetNums;
        }


        /// <summary>
        /// Modifies an array of int32 numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of 32-bit signed integers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public int[] OffsetNumber(int[] numsToModify, double minPercent, double maxPercent)
        {
            int[] offsetNums = new int[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetInteger(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (int)res[i];
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of unsigned int32 numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of 32-bit signed integers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public uint[] OffsetNumber(uint[] numsToModify, double minPercent, double maxPercent)
        {
            uint[] offsetNums = new uint[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetInteger(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (uint)res[i];
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of long numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of 64-bit signed integers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public long[] OffsetNumber(long[] numsToModify, double minPercent, double maxPercent)
        {
            long[] offsetNums = new long[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetInteger(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (long)res[i];
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of unsigned long numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of 64-bit unsigned integers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public ulong[] OffsetNumber(ulong[] numsToModify, double minPercent, double maxPercent)
        {
            ulong[] offsetNums = new ulong[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetInteger(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (ulong)res[i];
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of short numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of 16-bit signed integers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public short[] OffsetNumber(short[] numsToModify, double minPercent, double maxPercent)
        {
            short[] offsetNums = new short[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetInteger(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (short)res[i];
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of unsigned short numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of 16-bit unsigned integers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public ushort[] OffsetNumber(ushort[] numsToModify, double minPercent, double maxPercent)
        {
            ushort[] offsetNums = new ushort[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetInteger(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (ushort)res[i];
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of signed bytes by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of 8-bit signed bytes greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public sbyte[] OffsetNumber(sbyte[] numsToModify, double minPercent, double maxPercent)
        {
            sbyte[] offsetNums = new sbyte[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetInteger(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (sbyte)res[i];
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of unsigned bytes by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of 8-bit unsigned bytes greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public byte[] OffsetNumber(byte[] numsToModify, double minPercent, double maxPercent)
        {
            byte[] offsetNums = new byte[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetInteger(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (byte)res[i];
            }
            return offsetNums;
        }
  
        /// <summary>
        /// Modifies an array of double numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of double numbers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public double[] OffsetNumber(double[] numsToModify, double minPercent, double maxPercent)
        {
            double[] offsetNums = new double[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetDouble(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (double)res[i];
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of float numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of double numbers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public float[] OffsetNumber(float[] numsToModify, double minPercent, double maxPercent)
        {
            float[] offsetNums = new float[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetDouble(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (float)res[i];
            }
            return offsetNums;
        }

        /// <summary>
        /// Modifies an array of decimal numbers by applying offsets that are within a minimum percent and maximum percent of the original number.
        /// </summary>
        ///<param name="numsToModify">Number array which will be randomized by having an offset applied to it.</param>
        /// <param name="minPercent">Minimum offset to apply to the original number. Specify the percentage in decimal format. For example, if min is 15 percent then this parameter should have a value of .15.</param>
        /// <param name="maxPercent">Maximum offset to apply to the original number. Specify the percentage in decimal format. For example, if max is 125 percent then this parameter should have a value of 1.25.</param>
        /// <returns>An array of double numbers greater than or equal to the numToModify + (numToModify * minPercent) and less than or equal to the numToModify + (numToModify * maxPercent).</returns>
        /// <remarks>Specify negative percentages for minPercent and/or maxPercent to define an offset that will decrease the original number.
        /// Specify positive percentages for minPercent and/or maxPercent to define an offset that will increase the original number.</remarks>
        public decimal[] OffsetNumber(decimal[] numsToModify, double minPercent, double maxPercent)
        {
            decimal[] offsetNums = new decimal[numsToModify.Length];
            double[] origNums = new double[numsToModify.Length];
            for (int i = 0; i < numsToModify.Length; i++)
            {
                origNums[i] = (double)numsToModify[i];
            }
            double[] res = OffsetDouble(origNums, minPercent, maxPercent);
            for (int i = 0; i < numsToModify.Length; i++)
            {
                offsetNums[i] = (decimal)res[i];
            }
            return offsetNums;
        }




    }//end class
}//end namespace
