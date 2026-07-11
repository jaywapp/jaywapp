using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Jaywapp.Infrastructure.Helpers
{
    public static class RandomHelper
    {
        /// <summary>
        /// <see cref="true"/> 혹은 <see cref="false"/>를 무작위로 반환합니다.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static bool NextBoolean(this Random random) => random.Next(0, 2) == 1;

        /// <summary>
        /// 무작위로 문자열을 반환합니다.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static string NextString(this Random random, int maxLength = 100)
        {
            var length = random.Next(1, maxLength);

            var builder = new StringBuilder();
            var chars = new List<char>();

            for (int i = 0; i < length; i++)
                chars.Add(NextCharacter(random));

            foreach (var c in chars)
                builder.Append(c);

            return builder.ToString();
        }

        /// <summary>
        /// 무작위로 문자를 반환합니다.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static char NextCharacter(this Random random)
        {
            var min = 33;
            var max = 126;
            var ascii = random.Next(min, max);

            return (char)ascii;
        }

        /// <summary>
        /// <typeparamref name="TEnum"/> 형식의 Enum값 중 하나를 무작위로 반환합니다.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="random"></param>
        /// <returns></returns>
        public static TEnum Next<TEnum>(this Random random)
            where TEnum : struct, IComparable
        {
            var values = EnumHelper.GetValues<TEnum>();
            var count = values.Count();
            var idx = random.Next(0, count - 1);

            return values.ElementAt(idx);
        }

        /// <summary>
        /// 무작위로 색상을 반환합니다.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Color NextColor(this Random random)
        {
            var a = (byte)random.Next(0, 255);
            var r = (byte)random.Next(0, 255);
            var g = (byte)random.Next(0, 255);
            var b = (byte)random.Next(0, 255);

            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// 무작위로 점을 생성하여 반환합니다.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Point NextPoint(this Random random)
        {
            return new Point(
                (int)random.NextDouble(),
                (int)random.NextDouble());
        }

        /// <summary>
        /// <paramref name="selectors"/>중 무작위로 하나를 수행하여 반환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <param name="selectors"></param>
        /// <returns></returns>
        public static T Next<T>(this Random random, params Func<T>[] selectors)
        {
            var count = selectors.Count();
            var idx = random.Next(0, count - 1);
            var selector = selectors.ElementAt(idx);

            return selector.Invoke();
        }
    }
}
