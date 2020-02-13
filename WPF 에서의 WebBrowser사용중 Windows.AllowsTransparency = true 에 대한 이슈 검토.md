## WPF 에서의 WebBrowser사용중 Windows.AllowsTransparency = true 에 대한 이슈 검토

#### Windows.AllowsTransparency = true 라면 Webbrowser가 노출되지 않는다.

- 확인된 원인 : Wpf AirSpace 문제 
  - Win32 컨트롤을 올릴수는 있으나 겹쳐지거나 할수 없으며 또한 Transparent Window의 경우에는 Win32 컨트롤이 올라가지 않음

  - 참고 내용
    - WebBrowser 컨트롤은 내부적으로 네이티브 WebBrowser ActiveX 컨트롤을 인스턴스화. [WebBrowser 클래스](https://docs.microsoft.com/ko-kr/dotnet/api/system.windows.controls.webbrowser?view=netframework-4.8)
    - [WPF 및 Windows Forms 상호 운용성](https://docs.microsoft.com/ko-kr/dotnet/framework/wpf/advanced/wpf-and-windows-forms-interoperation)
    - 4.5 beta 에서 이문제를 해결하려고 하는 속성들이 있었으나 4.5정식에서 삭제 되고 그 이후로는 무소식.... [관련링크](https://stackoverflow.com/questions/51649858/net-4-6-and-4-7-already-out-still-no-airspace-fix-for-wpf-hosting-classic-hw)

- **결론 : 일반적인 방법으론 Windows.AllowsTransparency = true 라면 Webbrowser가 노출되지 않는다.**


#### 다른 방법을 찾아보자!

- 윈도우가 Transparent라면 Transparent가 아닌 Popup 컨트롤에 띄어 볼까? 
  - 문제발생!! Popup의 StaysOpen = false 했을경우 상호작용 문제가 있음  (StaysOpen = true일때는 문제 없음)
  - 관련내용:
    - https://blog.dotnetgator.com/2008/08/28/problem-when-using-a-winformshost-in-a-wpf-popup-control/
    - https://stackoverflow.com/questions/1185985/wpf-popup-and-windowsformshost-problem 
      - (StaysOpen = true 로 하고 위치 따라오게 하는등의 우회 방법이 있음)

  - 정리하면 StaysOpen = false 일경우 Popup안에 WindowsFormsHost 가 선택될경우 Mouse.Capture가 원치 않는 동작을 하게 됨


- 또 다른 샘플 발견 : [링크](https://docs.microsoft.com/ko-kr/archive/blogs/changov/webbrowser-control-on-transparent-wpf-window)
  - 확인 결과 : 윈도우 위에다 새 윈도우 띄우고 사이즈와 포지션 따라 다니게 한 것, **그럴싸함**

- 확실한 방법 !! CefSharp.ChromiumWebBrowser 사용
  - Transparent 와 투명도 모두 적용된다. 
  - 하지만 우리는 재설치와 용량 이슈가 ㅠㅠ


- 그외 이런글을 발견 했으나 재현에 실패. 
  - http://www.hoons.net/board/qanet3/content/32197


