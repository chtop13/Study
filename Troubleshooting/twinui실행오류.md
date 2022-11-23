Windows 10 LTSB(C) 1607   닷넷 프로그램 실행오류현상

현상

- 이벤트뷰어 오류메시지는  아래와같음

![image](https://user-images.githubusercontent.com/29750468/203474645-1d6de468-0a5e-4866-b03e-3f4cf202eadd.png)


해결방법

-관리도구(실행-> gpedit.msc)에서 “컴퓨터구성 > Windows 설정 > 보안설정 > 로컬 정책 > 보안옵션” 진입

-“사용자 계정 컨트롤: 관리 승인 모드에서 모든 관리자 실행” 옵션을 “사용” 으로 변경

-실행 확인

-안되면 재부팅 후 실행 확인
