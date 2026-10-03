# 프로필 README 개편 — 작업 계획

orchestrator: Claude

| # | 작업 | owner | model | effort | depends_on | parallel_group | files | verification | status |
|---|---|---|---|---|---|---|---|---|---|
| T0 | 질문 답변·계획 승인 | 사용자 | - | - | - | - | - | 승인 기록 | completed |
| T1 | 공개 저장소 메타데이터 수집(설명·언어·스타·최근 활동) | Claude | haiku | low | T0 | A | (없음, 결과는 analysis에 기록) | README 대조 (API 차단) | completed |
| T2 | 링크·배지 상태 점검 | Claude | haiku | low | T0 | A | (없음) | HTTP 상태 (외부 egress 차단 → 구현 후 재검증) | completed (부분) |
| T3 | 콘셉트 3종 작성 | Claude | sonnet | medium | T1, T2 | B | docs/ux-concepts/profile-readme-refresh/concept-01..03 | 아티팩트 미리보기 (index.html) | completed |
| T4 | 콘셉트 선택 | 사용자 | - | - | T3 | - | - | 선택 기록 | blocked |
| T5 | README.md 본 구현 | Claude | sonnet | medium | T4 | C | README.md | 링크 전수 점검·렌더 확인 | pending |
| T5b | GitHub Actions 워크플로(블로그 RSS, 통계 SVG, 스네이크) | Claude | sonnet | medium | T4 | C | .github/workflows/profile-refresh.yml, scripts/ | YAML 검증·스크립트 로컬 실행 | pending |
| T6 | 최종 리뷰·문서 갱신 | Claude | opus | high | T5 | D | README.md, docs/profile-readme-refresh-* | diff 리뷰 | pending |
| T7 | `main` 브랜치 생성(master 기준)·PR(base=main)·기본 브랜치 변경 안내 | Claude | sonnet | low | T5b | GitHub Actions 워크플로(블로그 RSS, 통계 SVG, 스네이크) | Claude | sonnet | medium | T4 | C | .github/workflows/profile-refresh.yml, scripts/ | YAML 검증·스크립트 로컬 실행 | pending |
| T6 | E | - | PR 확인 | pending |

- T1·T2는 독립 읽기 작업이라 병렬(A). T3 이후는 같은 파일 또는 사용자 결정에 의존하므로 순차.
- 모델 선택 근거: 단순 수집은 haiku, 작성은 sonnet, 최종 리뷰는 opus (AGENTS.md 기본 배정).
- 참고: `impeccable`, `design-taste-frontend` 스킬은 현재 세션에 설치되어 있지 않음 → T3에서 해당 지침 대신 artifact-design 원칙을 적용하고 그 사실을 기록.
