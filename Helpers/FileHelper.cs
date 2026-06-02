using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Text;

namespace Jaywapp.Infrastructure.Helpers
{
    /// <summary>
    /// 파일 및 디렉토리 관련 유틸리티 메서드를 제공하는 정적 클래스입니다.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 주어진 파일 경로에서 모든 라인을 지연 실행(lazy evaluation) 방식으로 읽어옵니다.
        /// </summary>
        /// <param name="path">읽을 파일의 경로입니다.</param>
        /// <returns>파일의 각 라인을 담고 있는 <see cref="IEnumerable{T}"/> 컬렉션입니다.</returns>
        /// <exception cref="ArgumentNullException">path가 null인 경우 발생합니다.</exception>
        /// <exception cref="ArgumentException">path가 비어 있거나 잘못된 형식인 경우 발생합니다.</exception>
        /// <exception cref="FileNotFoundException">파일을 찾을 수 없는 경우 발생합니다.</exception>
        public static IEnumerable<string> ReadLines(this string path)
        {
            using (var reader = new StreamReader(path))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                    yield return line;
            }
        }

        /// <summary>
        /// 지정된 ZIP 파일을 주어진 디렉터리에 압축 해제합니다.
        /// </summary>
        /// <param name="zipPath">압축 해제할 ZIP 파일의 경로입니다.</param>
        /// <param name="dirPath">압축 해제된 파일들이 저장될 디렉터리 경로입니다.</param>
        /// <exception cref="ArgumentException">zipPath 또는 dirPath가 잘못된 형식인 경우 발생합니다.</exception>
        /// <exception cref="FileNotFoundException">zipPath에 해당하는 파일을 찾을 수 없는 경우 발생합니다.</exception>
        public static void Decompress(this string zipPath, string dirPath)
        {
            ZipFile.ExtractToDirectory(zipPath, dirPath);
        }

        /// <summary>
        /// 지정된 디렉터리 내의 모든 파일과 하위 디렉터리를 삭제한 후, 해당 디렉터리를 다시 생성합니다.
        /// </summary>
        /// <param name="path">정리할 디렉터리의 경로입니다.</param>
        public static void ClearDirectory(this string path)
        {
            if (!Directory.Exists(path))
                return;

            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 지정된 디렉터리와 모든 하위 디렉터리 내의 모든 파일 경로를 재귀적으로 가져옵니다.
        /// </summary>
        /// <param name="dirPath">파일을 검색할 디렉터리 경로입니다.</param>
        /// <returns>찾은 파일들의 경로를 담고 있는 <see cref="List{T}"/>입니다.</returns>
        public static List<string> GetFiles(string dirPath)
        {
            var result = new List<string>();
            var members = Directory.GetFileSystemEntries(dirPath);

            foreach (var member in members)
            {
                var isFile = File.Exists(member);

                // File
                if (isFile)
                    result.Add(member);
                // Folder
                else
                    result.AddRange(GetFiles(member));
            }

            return result;
        }

        /// <summary>
        /// 주어진 파일의 확장자가 특정 확장자와 일치하는지 여부를 확인합니다.
        /// 확장자 비교 시 대소문자를 구분하지 않습니다.
        /// </summary>
        /// <param name="path">확장자를 확인할 파일의 경로입니다.</param>
        /// <param name="extension">비교할 확장자 문자열입니다. 예: ".zip", ".txt".</param>
        /// <returns>파일의 확장자가 지정된 확장자와 일치하면 true, 그렇지 않으면 false를 반환합니다.</returns>
        public static bool IsExtension(string path, string extension)
        {
            if (!File.Exists(path))
                return false;

            var ext = Path.GetExtension(path);
            return !string.IsNullOrEmpty(ext)
                && string.Equals(extension, ext, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 지정된 파일을 대상 디렉터리에 복사합니다.
        /// </summary>
        /// <param name="filePath">원본 파일의 경로입니다.</param>
        /// <param name="dir">파일이 복사될 대상 디렉터리의 경로입니다.</param>
        public static void Copy(string filePath, string dir)
        {
            File.Copy(filePath, $@"{dir}\{Path.GetFileName(filePath)}");
        }

        /// <summary>
        /// 지정된 파일을 대상 <see cref="DirectoryInfo"/> 객체가 나타내는 디렉터리에 복사합니다.
        /// </summary>
        /// <param name="filePath">원본 파일의 경로입니다.</param>
        /// <param name="directory">파일이 복사될 대상 디렉터리 정보 객체입니다.</param>
        public static void Copy(string filePath, DirectoryInfo directory)
        {
            Copy(filePath, directory.FullName);
        }

        /// <summary>
        /// 여러 파일을 지정된 이름의 ZIP 파일로 압축합니다.
        /// 압축 과정에서 임시 디렉터리를 생성한 후 삭제합니다.
        /// </summary>
        /// <param name="zipPath">생성할 ZIP 파일의 전체 경로입니다. 예: "C:\temp\archive.zip".</param>
        /// <param name="filePaths">압축할 파일들의 경로 목록입니다.</param>
        public static void Compress(string zipPath, params string[] filePaths)
        {
            // 파일 이름 수집
            var fileName = Path.GetFileNameWithoutExtension(zipPath);
            // zip 파일이 위치할 폴더 경로 수집
            var dirPath = Path.GetDirectoryName(zipPath);
            // 임시 폴더 생성 (파일 이름과 동일)
            var dir = Directory.CreateDirectory($@"{dirPath}\{fileName}");

            // 입력된 파일들을 모두 임시 폴더로 복사
            foreach (var sourcePath in filePaths)
                Copy(sourcePath, dir);

            // 임시 폴더를 기반으로 압축파일 생성
            ZipFile.CreateFromDirectory(dir.FullName, zipPath);

            // 임시 폴더 제거
            Directory.Delete(dir.FullName, true);
        }

        /// <summary>
        /// 시스템의 TEMP 폴더에 지정된 이름의 파일을 생성하고 해당 경로를 반환합니다.
        /// 파일이 이미 존재하면 삭제 후 새로 생성합니다.
        /// </summary>
        /// <param name="fileName">TEMP 폴더에 생성할 파일의 이름입니다.</param>
        /// <returns>생성된 임시 파일의 전체 경로입니다.</returns>
        public static string GetTempFile(string fileName)
        {
            var dir = Path.GetTempPath();
            var path = $@"{dir}{fileName}";

            if (File.Exists(path))
                File.Delete(path);

            return path;
        }
    }
}