# Infrastructure 헬퍼

기존 프로젝트에서 제공하는 유틸리티 헬퍼 클래스 가이드입니다.

## CollectionHelper

`ICollection<T>`에 대한 확장 메서드를 제공합니다.

```csharp
using Jaywapp.Infrastructure.Helpers;

ICollection<int> collection = new ObservableCollection<int> { 1, 2 };
collection.AddRange(new[] { 3, 4, 5 });
// collection: [1, 2, 3, 4, 5]
```

## DataTableHelper

`DataColumnCollection`과 `DataRowCollection`을 `List<T>`로 변환합니다.

```csharp
using Jaywapp.Infrastructure.Helpers;

List<DataColumn> columns = dataTable.Columns.ToList();
List<DataRow> rows = dataTable.Rows.ToList();
```

## EnumHelper

Enum 타입에 대한 리플렉션 기반 유틸리티입니다.

```csharp
using Jaywapp.Infrastructure.Helpers;

// 모든 값 열거
var values = EnumHelper.GetValues<MyEnum>();

// Description 가져오기
EnumHelper.TryGetDescription(MyEnum.Value, out string desc);
MyEnum.Value.GetDescriptionOrToString();

// Description으로 역파싱
"설명".GetValueFromDescription<MyEnum>();
EnumHelper.TryParseValueFromDescription<MyEnum>("설명", out var result);
```

## EnumerableHelper

`IEnumerable<T>`에 대한 확장 메서드를 제공합니다.

```csharp
using Jaywapp.Infrastructure.Helpers;

// Null 또는 빈 목록 확인
items.IsNullOrEmpty();

// Null 요소 제거
items.IgnoreNull();

// ObservableCollection 변환
var observable = items.ToObservableCollection();

// 연쇄 페어링
new[] { 1, 2, 3, 4 }.ChainPairing();
// [(1,2), (2,3), (3,4)]

new[] { 1, 2, 3 }.ChainPairing(isCircular: true);
// [(1,2), (2,3), (3,1)]
```

## FileHelper

파일 및 디렉토리 관련 유틸리티입니다.

```csharp
using Jaywapp.Infrastructure.Helpers;

// 파일 읽기 (lazy)
IEnumerable<string> lines = "path/to/file.txt".ReadLines();

// 압축/해제
"archive.zip".Decompress("output/dir");
FileHelper.Compress("output.zip", "file1.txt", "file2.txt");

// 디렉토리 정리
"path/to/dir".ClearDirectory();

// 재귀적 파일 목록
List<string> files = FileHelper.GetFiles("path/to/dir");

// 확장자 확인
FileHelper.IsExtension("file.zip", ".zip"); // true

// 임시 파일
string tempPath = FileHelper.GetTempFile("temp.txt");
```

## HashSetHelper

`HashSet<T>`에 대한 확장 메서드를 제공합니다.

```csharp
using Jaywapp.Infrastructure.Helpers;

var set = new HashSet<int> { 1, 2, 3 };
set.AddRange(new[] { 4, 5 });    // {1, 2, 3, 4, 5}
set.RemoveRange(new[] { 1, 2 }); // {3, 4, 5}
```

## RandomHelper

`System.Random`에 대한 확장 메서드를 제공합니다.

```csharp
using Jaywapp.Infrastructure.Helpers;

var random = new Random();

bool flag = random.NextBoolean();
string str = random.NextString(maxLength: 50);
char ch = random.NextCharacter();          // ASCII 33~126 범위 문자
MyEnum val = random.Next<MyEnum>();         // 무작위 Enum 값
Color color = random.NextColor();           // 무작위 ARGB 색상
Point point = random.NextPoint();

// 랜덤 selector 실행
var result = random.Next(() => "A", () => "B");
```

## XmlHelper

`XElement` 어트리뷰트 접근 유틸리티입니다.

```csharp
using Jaywapp.Infrastructure.Helpers;

XElement element = ...;

string value = element.GetAttributeValue("name");                     // 없으면 null
string value = element.GetAttributeValue("name", "default");         // 없으면 "default"
string value = element.GetAttributeValueOrEmpty("name");              // 없으면 ""

if (element.TryGetAttributeValue("name", out string result))
    Console.WriteLine(result);
```
