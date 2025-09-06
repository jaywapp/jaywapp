#!/usr/bin/env python3
import re
import sys
from pathlib import Path


TYPE_PATTERN = re.compile(r"^\s*(public|internal)\s+(?:static\s+)?(?:partial\s+)?(class|struct|interface|enum)\s+([A-Za-z_][A-Za-z0-9_]*)\b")
METHOD_PATTERN = re.compile(r"^\s*(public|internal)\s+(?:static\s+|virtual\s+|override\s+|sealed\s+|async\s+|new\s+)*([A-Za-z0-9_<>,\[\]\.\?]+)\s+([A-Za-z_][A-Za-z0-9_]*)\s*\(([^)]*)\)\s*(?:\{|=>|where|$)")
PROPERTY_PATTERN = re.compile(r"^\s*(public|internal)\s+(?:static\s+)?([A-Za-z0-9_<>,\[\]\.\?]+)\s+([A-Za-z_][A-Za-z0-9_]*)\s*\{.*")


def find_preceding_xml_block(lines, idx):
    """If there is an XML doc block immediately above, return (start, end).
    Otherwise return None.
    Also detect placeholder blocks (summary with '설명') so we can replace them.
    """
    j = idx - 1
    # Skip blank lines and attributes
    while j >= 0:
        s = lines[j]
        st = s.lstrip()
        if st.startswith("///"):
            # collect contiguous /// block upwards
            end = j
            start = j
            while start - 1 >= 0 and lines[start - 1].lstrip().startswith("///"):
                start -= 1
            return (start, end)
        if st.strip() == "" or (st.startswith("[") and not st.startswith("///")):
            j -= 1
            continue
        break
    return None


def build_summary_block(summary_text: str):
    return [
        "/// <summary>\n",
        f"/// {summary_text}\n",
        "/// </summary>\n",
    ]


def build_method_block(summary_text: str, return_type: str, params: str, return_hint: str = None, param_overrides: dict | None = None, returns_override: str | None = None):
    block = build_summary_block(summary_text)
    params = [p.strip() for p in params.split(',')] if params.strip() else []
    for p in params:
        if not p:
            continue
        # extract parameter name: handle modifiers and default values
        # examples: "int count", "ref string s", "CancellationToken ct = default"
        raw = p
        # remove attributes in params [Attr]
        raw = re.sub(r"\[[^\]]*\]", "", raw).strip()
        # remove default assignment by splitting on '=' first
        left = raw.split('=')[0].strip()
        parts = [part for part in left.split() if part not in {"ref", "out", "in", "params", "this"}]
        if len(parts) == 0:
            continue
        last = parts[-1].strip()
        # for tuples like (int a, int b), above still works
        if param_overrides and last in param_overrides:
            desc = param_overrides[last]
        else:
            desc = param_korean_desc(last)
        block.append(f"/// <param name=\"{last}\">{desc}</param>\n")
    if return_type and return_type != "void":
        ret_desc = returns_override or return_hint or return_korean_desc_by_name(summary_text)
        block.append(f"/// <returns>{ret_desc}</returns>\n")
    return block


def split_identifier(name: str):
    parts = re.findall(r"[A-Z]?[a-z]+|[A-Z]+(?![a-z])|\d+", name)
    return [p for p in parts if p]


def to_korean_noun(tokens):
    # Simple join; you can customize rules if needed
    return " ".join(tokens)


def guess_summary_for_member(name: str, kind: str, file_text: str, return_type: str = None, decl_kind: str = None):
    tokens = split_identifier(name)
    lower = name.lower()
    starts = lambda p: lower.startswith(p)
    if kind == "type":
        if decl_kind == "interface":
            if name.startswith("I") and len(tokens) > 1:
                return f"{to_korean_noun(tokens[1:])} 인터페이스를 정의합니다."
            return "인터페이스를 정의합니다."
        if decl_kind == "enum":
            return "열거형을 정의합니다."
        if "Converters" in file_text or "IValueConverter" in file_text:
            return "값 변환기를 제공합니다."
        if "Helpers" in file_text:
            return "유틸리티 메서드를 제공합니다."
        if name.endswith("Attribute"):
            return "사용자 지정 특성을 정의합니다."
        if name.endswith("Extensions") or name.endswith("Ext"):
            return "확장 메서드를 제공합니다."
        return f"{to_korean_noun(tokens)}를(을) 제공합니다."

    # member methods/properties
    if starts("get"):
        return f"{to_korean_noun(tokens[1:])}를(을) 가져옵니다." if len(tokens) > 1 else "값을 가져옵니다."
    if starts("set"):
        return f"{to_korean_noun(tokens[1:])}를(을) 설정합니다." if len(tokens) > 1 else "값을 설정합니다."
    if starts("add"):
        return f"{to_korean_noun(tokens[1:])}를(을) 추가합니다."
    if starts("remove"):
        return f"{to_korean_noun(tokens[1:])}를(을) 제거합니다."
    if starts("update"):
        return f"{to_korean_noun(tokens[1:])}를(을) 업데이트합니다."
    if starts("create") or starts("build"):
        return f"{to_korean_noun(tokens[1:])}를(을) 생성합니다."
    if starts("delete"):
        return f"{to_korean_noun(tokens[1:])}를(을) 삭제합니다."
    if starts("find") or starts("search"):
        return f"{to_korean_noun(tokens[1:])}를(을) 찾습니다."
    if starts("load"):
        return f"{to_korean_noun(tokens[1:])}를(을) 로드합니다."
    if starts("save"):
        return f"{to_korean_noun(tokens[1:])}를(을) 저장합니다."
    if starts("parse"):
        return f"{to_korean_noun(tokens[1:])}를(을) 구문 분석합니다."
    # Special-case WPF converter members
    if name in ("Convert", "ConvertBack") and ("IValueConverter" in file_text or "System.Windows.Data" in file_text):
        return "값을 변환합니다." if name == "Convert" else "대상 값을 원본으로 변환합니다."
    if starts("to") or starts("convert"):
        return f"{to_korean_noun(tokens[1:])}로 변환합니다."
    if starts("try"):
        return f"{to_korean_noun(tokens[1:])}를(을) 시도하고, 성공 여부를 반환합니다."
    if starts("is"):
        return f"{to_korean_noun(tokens[1:])}인지 여부를 확인합니다."
    if starts("has"):
        return f"{to_korean_noun(tokens[1:])}를(을) 보유하는지 여부를 확인합니다."
    if name in ("Convert", "ConvertBack") and ("IValueConverter" in file_text or "System.Windows.Data" in file_text):
        return "값을 변환합니다." if name == "Convert" else "대상 값을 원본으로 변환합니다."
    if name in ("Contains", "StartsWith", "EndsWith"):
        mapping = {"Contains": "포함 여부를 확인합니다.", "StartsWith": "시작 여부를 확인합니다.", "EndsWith": "끝나는지 여부를 확인합니다."}
        return mapping[name]
    # Property default
    if kind == "property":
        return f"{to_korean_noun(tokens)}를(을) 가져오거나 설정합니다."
    # Fallback
    return "동작을 수행합니다."


def param_korean_desc(name: str):
    key = name.lower()
    mapping = {
        "value": "입력 값",
        "targettype": "대상 형식",
        "param": "매개 변수",
        "parameter": "매개 변수",
        "culture": "문화권 정보",
        "target": "대상",
        "type": "형식",
        "name": "이름",
        "collection": "컬렉션",
        "items": "항목들",
        "str": "문자열",
        "color": "색",
        "attr": "특성",
        "actual": "실제값",
        "expect": "기대값",
        "op": "연산자",
        "obj": "대상",
    }
    return mapping.get(key, "매개 변수")


def return_korean_desc_by_name(summary_text: str):
    # If summary mentions 여부 or 성공, craft returns accordingly
    if "여부" in summary_text:
        if "성공" in summary_text:
            return "성공하면 true를 반환합니다."
        return "조건을 만족하면 true를 반환합니다."
    if "변환" in summary_text:
        return "변환 결과를 반환합니다."
    if "가져옵니다" in summary_text or "찾습니다" in summary_text or "생성합니다" in summary_text:
        return "결과를 반환합니다."
    return "결과를 반환합니다."


# Repository-specific overrides for higher-quality docs
DOC_OVERRIDES = {
    "Helpers/CollectionHelper.cs": {
        "method": {
            "AddRange": {
                "summary": "지정한 항목들을 컬렉션에 순차적으로 추가합니다.",
                "params": {"collection": "대상 컬렉션", "items": "추가할 항목 시퀀스"},
            }
        }
    },
    "Helpers/ColorHelper.cs": {
        "method": {
            "GetColorName": {
                "summary": "입력 색상과 일치하는 미리 정의된 색상 이름을 반환합니다. 없으면 색상 문자열을 반환합니다.",
                "params": {"color": "색상 값"},
                "returns": "색상 이름 또는 색상 문자열"
            },
            "ToColor": {
                "summary": "문자열 표현을 Color로 변환합니다. 실패 시 기본 색을 반환합니다.",
                "params": {"str": "색상 문자열", "defaultColor": "파싱 실패 시 반환할 기본 색"},
                "returns": "변환된 색상 값"
            },
            "TryConvertColor": {
                "summary": "문자열을 Color로 변환을 시도합니다.",
                "params": {"str": "색상 문자열", "color": "변환 성공 시 결과 색상"},
                "returns": "성공하면 true, 실패하면 false"
            },
        }
    },
    "Helpers/DataTableHelper.cs": {
        "method": {
            "ToList": {
                "summary": "컬렉션의 항목을 목록(List)으로 변환합니다.",
                # param names differ by overload; we keep generic
            }
        }
    },
}


def match_override(path: Path, member_name: str, kind: str):
    rel = "/".join(path.as_posix().split("/")[-2:]) if len(path.parts) >= 2 else path.as_posix()
    # Try exact path match
    if rel in DOC_OVERRIDES and kind in DOC_OVERRIDES[rel] and member_name in DOC_OVERRIDES[rel][kind]:
        return DOC_OVERRIDES[rel][kind][member_name]
    # Try filename-only match
    fname = path.name
    if fname in DOC_OVERRIDES and kind in DOC_OVERRIDES[fname] and member_name in DOC_OVERRIDES[fname][kind]:
        return DOC_OVERRIDES[fname][kind][member_name]
    # Try directory/filename match
    for key, spec in DOC_OVERRIDES.items():
        if key.endswith(fname) and kind in spec and member_name in spec[kind]:
            return spec[kind][member_name]
    return None


def process_file(path: Path) -> bool:
    text = path.read_text(encoding="utf-8")
    lines = text.splitlines(keepends=True)
    changed = False
    current_type = None

    i = 0
    while i < len(lines):
        line = lines[i]
        # Skip regions and using/namespace lines
        # Try type declarations
        m_type = TYPE_PATTERN.match(line)
        if m_type:
            current_type = m_type.group(3)
            block_pos = find_preceding_xml_block(lines, i)
            summary = guess_summary_for_member(current_type, "type", text, decl_kind=m_type.group(2))
            if block_pos:
                start, end = block_pos
                block_text = "".join(lines[start:end+1])
                # Replace if placeholder or if this is an interface with generic text
                if ("<summary>" in block_text and "/// 설명" in block_text) or (m_type.group(2) == "interface"):
                    # replace placeholder
                    del lines[start:end+1]
                    block = build_summary_block(summary)
                    lines[i - (end - start + 1):i - (end - start + 1)] = block
                    i = i - (end - start + 1) + len(block)
                    changed = True
            else:
                block = build_summary_block(summary)
                lines[i:i] = block
                i += len(block)
                changed = True
            i += 1
            continue

        m_method = METHOD_PATTERN.match(line)
        if m_method:
            ret = m_method.group(2)
            name = m_method.group(3)
            params = m_method.group(4)
            # Heuristic: treat constructors as methods without returns
            is_ctor = current_type is not None and name == current_type
            if is_ctor:
                ret_for_doc = "void"
            else:
                ret_for_doc = ret
            block_pos = find_preceding_xml_block(lines, i)
            # Repository-specific override
            override = match_override(path, name, "method")
            if override:
                summary = override.get("summary") or guess_summary_for_member(name, "method", text, ret_for_doc)
                param_over = override.get("params")
                returns_over = override.get("returns")
            else:
                summary = guess_summary_for_member(name, "method", text, ret_for_doc)
                param_over = None
                returns_over = None
            return_hint = None
            if name.startswith("Try") or name in ("Contains", "StartsWith", "EndsWith") or name.startswith("Is") or name.startswith("Has"):
                return_hint = "조건을 만족하면 true를 반환합니다."
            if block_pos:
                start, end = block_pos
                block_text = "".join(lines[start:end+1])
                # Replace if we have an explicit override or placeholder/low-quality text
                replace = bool(override)
                if "<summary>" in block_text and "/// 설명" in block_text:
                    replace = True
                if name in ("Convert", "ConvertBack") and ("로 변환합니다." in block_text or "Back로 변환합니다." in block_text):
                    replace = True
                if replace:
                    del lines[start:end+1]
                    block = build_method_block(summary, ret_for_doc, params, return_hint, param_over, returns_over)
                    lines[i - (end - start + 1):i - (end - start + 1)] = block
                    i = i - (end - start + 1) + len(block)
                    changed = True
            else:
                block = build_method_block(summary, ret_for_doc, params, return_hint, param_over, returns_over)
                lines[i:i] = block
                i += len(block)
                changed = True
            i += 1
            continue

        m_prop = PROPERTY_PATTERN.match(line)
        if m_prop and '(' not in line:
            block_pos = find_preceding_xml_block(lines, i)
            name = m_prop.group(3)
            summary = guess_summary_for_member(name, "property", text)
            if block_pos:
                start, end = block_pos
                block_text = "".join(lines[start:end+1])
                if "<summary>" in block_text and "/// 설명" in block_text:
                    del lines[start:end+1]
                    block = build_summary_block(summary)
                    lines[i - (end - start + 1):i - (end - start + 1)] = block
                    i = i - (end - start + 1) + len(block)
                    changed = True
            else:
                block = build_summary_block(summary)
                lines[i:i] = block
                i += len(block)
                changed = True
            i += 1
            continue

        i += 1

    if changed:
        path.write_text(''.join(lines), encoding="utf-8")
    return changed


def main():
    root = Path(sys.argv[1]) if len(sys.argv) > 1 else Path(".")
    cs_files = [p for p in root.rglob("*.cs") if p.is_file()]
    # Skip common generated patterns
    skip_suffixes = (".designer.cs", ".g.cs", ".g.i.cs")
    cs_files = [p for p in cs_files if not any(str(p).lower().endswith(s) for s in skip_suffixes)]

    total = 0
    changed = 0
    for p in cs_files:
        total += 1
        if process_file(p):
            changed += 1
    print(f"Processed {total} files, changed {changed} files.")


if __name__ == "__main__":
    main()
