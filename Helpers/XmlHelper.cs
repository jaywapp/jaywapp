using System.Xml.Linq;

namespace Jaywapp.Infrastructure.Helpers
{
    public static class XmlHelper
    {
        /// <summary>
        /// XML내 Element의 Attribute 이름에 매칭되는 Attribute Value를 가져오는 함수
        /// </summary>
        /// <param name="element">xml 내 element</param>
        /// <param name="attrName">어트리뷰트 이름</param>
        /// <param name="defaultValue">찾아지지 않았을때 가져올 기본 값</param>
        /// <returns></returns>
        public static string GetAttributeValue(this XElement element, string attrName, string defaultValue = null)
        {
            return element.Attribute(attrName)?.Value?.Trim() ?? defaultValue;
        }

        /// <summary>
        /// XML내 Element의 Attribute 이름에 매칭되는 Attribute Value를 가져오는 함수
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attrName"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGetAttributeValue(this XElement element, string attrName, out string result)
        {
            result = GetAttributeValue(element, attrName);
            return result != null;
        }

        /// <summary>
        /// XML내 Element의 Attribute 이름에 매칭되는 Attribute Value를 가져오는 함수
        /// </summary>
        /// <param name="element">xml 내 element</param>
        /// <param name="attrName">어트리뷰트 이름</param>
        /// <returns></returns>
        public static string GetAttributeValueOrEmpty(this XElement element, string attrName)
        {
            return element.GetAttributeValue(attrName, "");
        }
    }
}
