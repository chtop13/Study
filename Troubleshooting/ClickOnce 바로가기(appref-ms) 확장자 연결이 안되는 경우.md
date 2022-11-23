ClickOnce 바로가기(appref-ms) 확장자 연결이 안되는 경우

증상 확인

- ClickOnce로 설치된 프로그램을 바탕화면에 생성된 바로가기로 실행 시 연결된 확장자를 찾을 수 없다고 나오는 경우 

내용 파악

- XP 환경에서 확인된 원인 중 하나는 appref-ms를 실행시키는 파일이 없어서 발생 (rundll32.exe)


해결

- XP 환경
   - C:\Windows\System32\ 위치에 rundll32.exe 파일을 복사해준다.   
   - rundll32.exe (xp용)


 ※ 확인은 안되었지만 C:\Windows\System32\dfshim.dll 파일이 없어도 문제가 생길것으로 예상되니 위 방법으로 해결 안될시 확인해 볼것

