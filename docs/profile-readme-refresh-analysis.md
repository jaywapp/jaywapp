# 프로필 README 개편 — 분석

status: blocked (콘셉트 선택 대기)
orchestrator: Claude

## 요청

- GitHub 프로필(`github.com/jaywapp`)에 노출되는 `README.md`를 세련되게 개편하고 데이터를 최신화한다.

## 현재 상태와 근거 (2026-10-03 확인)

| 항목 | 현재 README | 실제 상태 | 문제 |
|---|---|---|---|
| Hits 배지 | `hits.seeyoufarm.com` | 서비스 종료로 배지 깨짐 가능성 | 교체/삭제 필요 |
| 배지 링크 | `http://img.shields.io`, `&link=` 파라미터 | shields.io는 https 권장, `link=`는 README에서 동작 안 함 | 정리 필요 |
| Crewith | `jaywapp/Crewith` | 저장소명 `jaywapp/crewith` | 링크 정규화 |
| baby_calendar | `jaywapp/baby_calendar` | `jaywapp/baby-calendar`, **private** | 방문자에게 404 |
| AssetManagement | `jaywapp/AssetManagement` | `jaywapp/asset-management` | 링크 정규화 |
| More 링크 | `jaywapp/Projects` | `jaywapp/projects` | 링크 정규화 |
| 프로젝트 목록 | 5개 | 공개 저장소 40여 개, 최근 활동은 AI·개발 생산성 도구 중심 | 대표 프로젝트 누락 |

최근 활동이 많은 공개 저장소(대표 후보): `gyungchung`, `vlytics`, `wam-releases`(+ `wam-plugin-*`), `toss-readonly-mcp`, `gyungchung-mcp`, `claude-skills`, `claude-agent-teams`, `jaywapp-marketplace`, `task-token-meter`, `cc-jsonl-monitor`, `p4-harness`, `code-virtualize`, `UnrealEditorBridge`, `AiUsageWidget`, `card-radar`, `ai-debate`, `ai-native-team-system`, `jaywapp-libs`, `csharp-tip`, `wiki`.
각 저장소의 설명·언어·스타 수는 구현 단계에서 GitHub API로 수집해 검증한다(추측 기재 금지).

## 부족한 정보와 질문

1. 언어: 영어 / 한국어 / 영·한 병기 중 무엇으로 할지
2. 대표 프로젝트 선정: 위 후보 중 자동 선정(최근 활동·공개·설명 유무 기준) 위임 여부, 꼭 넣거나 뺄 저장소
3. 경력·직함: Smilegate 재직 및 "Dev Productivity / Tooling Engineer" 직함 유지 여부, 추가할 경력·소개 문구
4. 동적 요소: GitHub 통계 카드, 블로그 최신 글 자동 갱신(GitHub Actions) 등 외부 서비스·워크플로 도입 여부
5. UX 게이트: 저장소 규칙상 주요 UX 변경은 콘셉트 3종 선택 후 구현 — 그대로 진행할지, 면제할지
6. 반영 방식: 승인 후 이 브랜치에 커밋·푸시·Draft PR 생성까지 진행해도 되는지

## 답변 (2026-10-03)

1. 언어: 영·한 병기
2. 대표 프로젝트: Claude가 공개 저장소 중 최근 활동 기준으로 선정
3. 경력·직함: Smilegate 재직, "Dev Productivity / Tooling Engineer" 유지
4. 동적 요소: 도입 (통계 카드, 블로그 최신 글 자동 갱신, 기여 그래프)
5. UX 게이트: 콘셉트 3종을 아티팩트로 제안 → https://claude.ai/artifact/WSBqLYbVt2U9JeJNpCanTA
6. 반영: 기본 브랜치를 `main`으로 하고 `main`에 반영 (현재 기본 브랜치는 `master`, `main` 브랜치 없음)

## 수집 결과 (T1·T2)

- 세션 네트워크 정책상 GitHub REST API(저장소 목록)·shields.io·tistory·외부 통계 서비스에 접근 불가. 공개 저장소 README(raw.githubusercontent.com)로 설명을 확인했다. 언어·스타 수는 확인하지 못했다.
- 링크 실시간 상태 확인은 이 환경에서 불가 → 구현 후 GitHub Actions 또는 사용자 확인으로 대체한다.
- 선정한 대표 프로젝트(공개, README로 설명 확인): p4-harness, task-token-meter, cc-jsonl-monitor, toss-readonly-mcp, jaywapp-marketplace, wam-releases(WAM), UnrealEditorBridge, jaywapp-libs, gyungchung(+gyungchung-mcp), vlytics, asset-management.
- 외부 통계 서비스(github-readme-stats 공개 인스턴스 등)는 가용성 위험이 있어, 통계 SVG와 스네이크를 이 저장소의 Actions가 `output` 브랜치에 직접 생성하는 방식으로 설계한다.

## 가정

- 비공개 저장소는 링크하지 않는다.
- 이메일·포트폴리오·블로그·LinkedIn 연락 수단은 유지한다.

## 범위 / 비범위

- 범위: `README.md`, `docs/profile-readme-refresh-*`, `docs/ux-concepts/profile-readme-refresh/`
- 비범위: `Jaywapp.Infrastructure`, `Jaywapp.Wpf` 코드, 다른 저장소, 포트폴리오 사이트

## 완료 기준

- 모든 링크가 공개 대상으로 200 응답, 깨진 배지 없음
- 데이터(경력·프로젝트·스택)가 2026-10 기준 확인값과 일치
- 라이트·다크 테마, 모바일 GitHub 화면에서 레이아웃 정상
