
# CustomWIndow


## 개요

윈도우 10 초기에는 윈도우의 테두리를 따로 설정하는 기능이 있었지만 이후 업데이트를 거치면서 사용할 수 없게된 기능입니다.
이를 다시 사용하게 하여 더욱 다양하게 윈도우를 꾸며기 위한 프로그램입니다.

## 필요 라이브러리

- .NET 7
- Windows App Runtime

## 빌드 환경

- Visual Studio 2022
- Windows 11

## 사용할 때 참조

- 이 프로그램은 윈도우에서 기본으로 제공하는 DWM (Desktop Window Manager)의 기능을 활용합니다.
- <em><u>수동으로 DWM을 비활성화하거나 커스텀된 DWM을 이용하는 경우, 이 프로그램의 일부 기능들을 사용할 수 없거나 일부 프로그램의 창 설정이 변경되지 않습니다.</u></em>
- 관리자 권한을 가지고 있는 프로그램(ex. 작업 관리자)은 CustomWIndow 프로그램을 관리자 권한으로 실행하면 창의 설정을 자동으로 변경합니다.


## 기능
[사이트](https://minepacu.github.io/posts/CustomWIndow-%EA%B8%B0%EB%8A%A5/)를 참조하세요.<br/>


## 로드맵
### UI
<table>
  <thead>
    <tr>
      <th colspan="3"> 윈도우 설정 </th>
    </tr>
  </thead>
  <thead>
    <tr>
      <th></th>
      <th>기능</th>
      <th>기타 사항</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>&check;</td>
      <td>창 모서리 설정</td>
      <td></td>
    </tr>
    <tr>
      <td>&check;</td>
      <td>작업 표시줄 설정</td>
      <td></td>
    </tr>
    <tr>
      <td>&check;</td>
      <td>Context menu 설정</td>
      <td></td>
    </tr>
  </tbody>
  <thead>
    <tr>
      <th colspan="3"> 트레이 아이콘 </th>
    </tr>
  </thead>
  <thead>
    <tr>
      <th></th>
      <th>기능</th>
      <th>기타 사항</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>&check;</td>
      <td>창 닫으면 트레이 아이콘 최소화 설정</td>
      <td></td>
    </tr>
    <tr>
      <td>&check;</td>
      <td>트레이 Context menu 구성</td>
      <td></td>
    </tr>
  </tbody>
</table>

### 로직
<table>
  <thead>
    <tr>
      <th colspan="3"> 창 커스터마이징 </th>
    </tr>
  </thead>
  <thead>
    <tr>
      <th></th>
      <th>기능</th>
      <th>기타 사항</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>&check;</td>
      <td>색깔 커스텀마이징</td>
      <td></td>
    </tr>
    <tr>
      <td>&check;</td>
      <td>프로그램 구분</td>
      <td></td>
    </tr>
    <tr>
      <td></td>
      <td>창 구분 (작업 표시줄, Context menu 등)</td>
      <td></td>
    </tr>
  </tbody>
  <thead>
    <tr>
      <th colspan="3">내장 프로그램</th>
    </tr>
  </thead>
  <thead>
    <tr>
      <th></th>
      <th>기능</th>
      <th>기타 사항</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>&check;</td>
      <td>.Net</td>
      <td></td>
    </tr>
    <tr>
      <td>&check;</td>
      <td>Windows App Sdk Runtime</td>
      <td></td>
    </tr>
  </tbody>
  <thead>
    <tr>
      <th colspan="3">권한</th>
    </tr>
  </thead>
  <thead>
    <tr>
      <th></th>
      <th>기능</th>
      <th>기타 사항</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>&check;</td>
      <td>관리자 권한으로 다시 시작</td>
      <td></td>
    </tr>
    <tr>
      <td>&check;</td>
      <td>프로그램 실행 시, 권한 자동 부여</td>
      <td></td>
    </tr>
  </tbody>
</table>
