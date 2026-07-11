# 필터링 시스템

Jaywapp.Infrastructure에 내장된 동적 필터링 시스템입니다.

## 개요

객체의 프로퍼티를 기반으로 동적 필터링을 수행하는 Composite 패턴 기반 시스템입니다.

## 구성 요소

### 열거형

```csharp
// 논리 연산자
public enum eLogicalOperator { AND, OR }

// 필터링 연산자
public enum eFilteringOperator
{
    Equal, NotEqual,
    LessThan, LessEqual, GreaterThan, GreaterEqual,
    MatchRegex, Contains, NotContains, StartsWith, EndsWith
}

// 필터링 대상 타입
public enum eFilteringType { String, Enum, Number }
```

### IFilter 인터페이스

```csharp
public interface IFilter
{
    eLogicalOperator Logical { get; }
    bool IsFiltered(object target);
}
```

### IFilterPropertySelector

```csharp
public interface IFilterPropertySelector
{
    string Name { get; }
    eFilteringType Type { get; }
    object Select(object target);
}
```

## 사용법

### 1. PropertySelector 구현

```csharp
public class NameSelector : IFilterPropertySelector
{
    public string Name => "Name";
    public eFilteringType Type => eFilteringType.String;

    public object Select(object target)
    {
        return ((Person)target).Name;
    }
}
```

### 2. 단일 필터 생성

```csharp
var filter = new Filter
{
    Logical = eLogicalOperator.AND,
    Selector = new NameSelector(),
    Operator = eFilteringOperator.Contains,
    Expect = "Kim"
};

bool match = filter.IsFiltered(person); // person.Name에 "Kim" 포함 여부
```

### 3. 필터 그룹 (Composite)

```csharp
var group = new FilterGroup
{
    Logical = eLogicalOperator.AND,
    Children = new List<IFilter>
    {
        new Filter
        {
            Logical = eLogicalOperator.AND,
            Selector = new NameSelector(),
            Operator = eFilteringOperator.StartsWith,
            Expect = "K"
        },
        new Filter
        {
            Logical = eLogicalOperator.AND,
            Selector = new AgeSelector(),
            Operator = eFilteringOperator.GreaterEqual,
            Expect = "20"
        }
    }
};

bool match = group.IsFiltered(person); // 이름 "K"로 시작 AND 나이 >= 20
```

### 4. 필터 목록 적용

```csharp
using Jaywapp.Infrastructure.Interfaces;

IEnumerable<IFilter> filters = ...;
bool match = filters.IsFiltered(target);
```

## FilterableAttribute

Enum 필드에 필터링 가능 여부를 표시하는 어트리뷰트입니다.

```csharp
public enum MyOperator
{
    [Filterable(eFilteringType.String)]
    [Filterable(eFilteringType.Number)]
    Equal,

    [Filterable(eFilteringType.Number)]
    LessThan,
}

// 사용
MyOperator.Equal.IsTargetField(eFilteringType.String);  // true
MyOperator.LessThan.IsTargetField(eFilteringType.String); // false
```

## 지원 연산자별 타입

| 연산자 | String | Number | Enum |
|--------|--------|--------|------|
| Equal | O | O | O |
| NotEqual | O | O | O |
| LessThan | | O | |
| LessEqual | | O | |
| GreaterThan | | O | |
| GreaterEqual | | O | |
| MatchRegex | O | O | O |
| Contains | O | O | O |
| NotContains | O | O | O |
| StartsWith | O | O | O |
| EndsWith | O | O | O |
