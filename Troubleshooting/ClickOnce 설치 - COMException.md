ClickOnce 설치 - COMException

오류
```
플랫폼 버전 정보
Windows : 10.0.14393.0 (Win32NT)
Common Language Runtime : 4.0.30319.42000
System.Deployment.dll : 4.6.1586.0 built by: NETFXREL2
clr.dll : 4.7.3650.0 built by: NET472REL1LAST_B
dfdll.dll : 4.6.1586.0 built by: NETFXREL2
dfshim.dll : 10.0.14393.0 (rs1_release.160715-1616)

원본
배포 url : http://dl.meshprime.com/pos/PrimePOS.application

오류 요약
다음은 오류에 대한 요약입니다. 이러한 오류의 세부 정보는 나중에 로그에 기록됩니다.
* http://dl.meshprime.com/pos/PrimePOS.application 활성화로 예외가 발생했습니다. 다음 실패 메시지가 발견되었습니다.
+ 참조된 어셈블리가 시스템에 설치되지 않았습니다. (예외가 발생한 HRESULT: 0x800736B3)

구성 요소 저장소 트랜잭션 실패 요약
트랜잭션 오류가 발생하지 않았습니다.

경고
이 작업을 수행하는 동안 경고가 발생하지 않았습니다.

작업 진행 상태
* [2020-09-01 17:50:22]: http://dl.meshprime.com/pos/PrimePOS.application 활성화가 시작되었습니다.

오류 정보
이 작업을 수행하는 동안 다음 오류가 발생했습니다.
* [2020-09-01 17:50:22] System.Runtime.InteropServices.COMException
- 참조된 어셈블리가 시스템에 설치되지 않았습니다. (예외가 발생한 HRESULT: 0x800736B3)
- 원본: System.Deployment
- 스택 추적:
위치: System.Deployment.Internal.Isolation.IStore.GetAssemblyInformation(UInt32 Flags, IDefinitionIdentity DefinitionIdentity, Guid& riid)
위치: System.Deployment.Internal.Isolation.Store.GetAssemblyManifest(UInt32 Flags, IDefinitionIdentity DefinitionIdentity)
위치: System.Deployment.Application.ComponentStore.GetAssemblyManifest(DefinitionIdentity asmId)
위치: System.Deployment.Application.ComponentStore.GetSubscriptionStateInternal(DefinitionIdentity subId)
위치: System.Deployment.Application.SubscriptionStore.GetSubscriptionStateInternal(SubscriptionState subState)
위치: System.Deployment.Application.SubscriptionStore.CheckAndReferenceApplication(SubscriptionState subState, DefinitionAppId appId, Int64 transactionId)
위치: System.Deployment.Application.DownloadManager.DownloadDeploymentManifestDirectBypass(SubscriptionStore subStore, Uri& sourceUri, TempFile& tempFile, SubscriptionState& subState, IDownloadNotification notification, DownloadOptions options, ServerInformation& serverInformation)
위치: System.Deployment.Application.DownloadManager.DownloadDeploymentManifestBypass(SubscriptionStore subStore, Uri& sourceUri, TempFile& tempFile, SubscriptionState& subState, IDownloadNotification notification, DownloadOptions options)
위치: System.Deployment.Application.ApplicationActivator.PerformDeploymentActivation(Uri activationUri, Boolean isShortcut, String textualSubId, String deploymentProviderUrlFromExtension, BrowserSettings browserSettings, String& errorPageUrl)
위치: System.Deployment.Application.ApplicationActivator.ActivateDeploymentWorker(Object state)

구성 요소 저장소 트랜잭션 정보
트랜잭션 정보를 사용할 수 없습니다.
```




해결방법
1. 윈도우 실행창(Window + R) cmd 실행
2. cmd 창에서 rundll32 %SystemRoot%\system32\dfshim.dll CleanOnlineAppCache
3. 제어판 > 프로그램 추가/삭제 에서 부릉POS 삭제
4. 재설치
5. 안된다면 3번 이후에 apps 폴더(윈도우10기준 %localappdata%\Apps) 삭제 후 재설치

Apps 폴더 삭제는 ClickOnce로 설치된 다른 애플리케이션에도 영향을 줄 수 있어서 확인 후 삭제해야 함


