# Jaywapp.Common

도메인에 독립적인 재사용 가능한 컴포넌트를 제공하는 모듈입니다.

## 확장 메서드

### StringExtensions

```csharp
using Jaywapp.Common.Extensions;

"hello".HasValue();              // true
"".HasValue();                   // false

"abcdef".SafeTruncate(3);       // "abc"
((string)null).SafeTruncate(5); // null

"hello".ToSha256();             // "2cf24dba..."

"line1\r\nline2\rline3".NormalizeLineEndings(); // "line1\nline2\nline3"
```

### CollectionExtensions

```csharp
using Jaywapp.Common.Extensions;

new int[0].IsEmpty();           // true
new[] { 1, 2, 3 }.None();      // false
items.None(x => x > 100);      // 조건 만족 요소 없으면 true

// null-safe ForEach
((IEnumerable<int>)null).SafeForEach(x => Console.Write(x)); // 아무 동작 안함

// 배치 분할
new[] { 1, 2, 3, 4, 5 }.Batch(2); // [[1,2], [3,4], [5]]

// 키 기준 중복 제거
items.DistinctBy(x => x.Name);
```

### TaskExtensions

```csharp
using Jaywapp.Common.Extensions;

// Fire-and-forget (예외 안전)
SomeAsyncMethod().SafeFireAndForget(ex => logger.Error(ex));

// 타임아웃 적용
var result = await longTask.WithTimeout(TimeSpan.FromSeconds(5));

// 재시도
var data = await TaskExtensions.WithRetry(
    () => httpClient.GetAsync(url),
    maxRetries: 3,
    delay: TimeSpan.FromSeconds(1));
```

### DateTimeExtensions

```csharp
using Jaywapp.Common.Extensions;

var now = DateTime.Now;
now.StartOfDay();  // 2024-06-15 00:00:00
now.EndOfDay();    // 2024-06-15 23:59:59.9999999

now.IsInRange(startDate, endDate); // 범위 내 포함 여부

// UTC/Local 안전 변환
unspecified.ToUtcSafe();   // Kind를 UTC로 지정
localTime.ToUtcSafe();     // UTC로 변환
```

### EnumExtensions

```csharp
using Jaywapp.Common.Extensions;

MyEnum.Value.GetDescription();              // DescriptionAttribute 값 반환
EnumExtensions.SafeParse<MyEnum>("Value");  // 안전 파싱 (실패 시 default)
EnumExtensions.GetValues<MyEnum>();         // 모든 값 열거
```

## 가드

```csharp
using Jaywapp.Common.Guards;

Guard.NotNull(param, nameof(param));
Guard.NotNullOrEmpty(name, nameof(name));
Guard.NotNullOrWhiteSpace(description, nameof(description));
Guard.InRange(age, 0, 150, nameof(age));
Guard.NotEmpty(items, nameof(items));
Guard.Requires(count > 0, "Count must be positive");
```

모든 가드 메서드는 값을 반환하므로 Fluent 패턴으로 사용 가능:
```csharp
_name = Guard.NotNullOrEmpty(name, nameof(name));
```

## 모델

### Result 패턴

```csharp
using Jaywapp.Common.Models;

// 성공
var success = Result.Success();
var typed = Result.Success(42);

// 실패
var failure = Result.Failure("ERR01", "오류가 발생했습니다");
var typedFailure = Result.Failure<int>(new Error("ERR02", "찾을 수 없습니다"));

// 사용
if (result.IsSuccess)
    Console.WriteLine(result.Value);
else
    Console.WriteLine(result.Error.Message);
```

### 페이지네이션

```csharp
var request = new PageRequest(page: 1, pageSize: 20,
    sorts: new[] { new SortDefinition("Name", SortDirection.Ascending) });

var result = new PageResult<User>(users, totalCount: 100, page: 1, pageSize: 20);
// result.TotalPages == 5
// result.HasNextPage == true
```

### 범위 및 날짜 기간

```csharp
var range = new Range<int>(1, 100);
range.Contains(50);    // true
range.Overlaps(other); // 범위 겹침 여부

var period = new DatePeriod(start, end, isStartInclusive: true, isEndInclusive: false);
period.Contains(date); // 경계 포함/배제 고려
```

### 트리 노드

```csharp
var root = new TreeNode<string>("Root");
var child1 = root.AddChild("Child1");
var child2 = root.AddChild("Child2");
child1.AddChild("Grandchild");

// 깊이 우선 탐색
foreach (var node in root.Traverse(TraversalOrder.DepthFirst))
    Console.WriteLine(node.Value);

// 너비 우선 탐색
foreach (var node in root.Traverse(TraversalOrder.BreadthFirst))
    Console.WriteLine(node.Value);
```

### 변경 추적

```csharp
// ChangeSet
var changes = new ChangeSet<Item>(added, updated, removed);
if (changes.HasChanges)
    Console.WriteLine($"Total: {changes.TotalChanges}");

// Trackable
var trackable = new Trackable<string>("original");
trackable.Current = "modified";
trackable.IsDirty;        // true
trackable.RejectChanges(); // Current = "original"
trackable.AcceptChanges(); // Original = Current
```
