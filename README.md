# TeamProjectB04
팀프로젝트 B04조 - 死신

# 개발 환경

유니티 : 2022.3.2f1(LTS)

기본 해상도 : 760 : 1280

# UI 프레임

![42 PNG](https://github.com/koju2005/TeamProjectB04/assets/141552941/3c54e2e4-a2d6-4962-8ac7-968d23722e75)

# 와이어 프레임

1. 씬 플로우
   ![Untitled (1)](https://github.com/koju2005/TeamProjectB04/assets/141552941/00486818-5699-40d8-9f8f-4566826c1f00)
2. 데이터 구조(Scriptable)
   ![Untitled (2)](https://github.com/koju2005/TeamProjectB04/assets/141552941/92ebb049-9bff-4adb-bf65-e883ac9a1791)
3. 프리팹
   
  ![image (1)](https://github.com/koju2005/TeamProjectB04/assets/141552941/02e381f4-4c80-4681-997b-73e20bf66fde)
  
5. 스크립터블 오브젝트

  ![image](https://github.com/koju2005/TeamProjectB04/assets/141552941/e48ba7f9-94f6-4b8c-93c9-b8d576d20d6c)

# 역할분담

- 이경현 : 게임 플레이 화면
- 김민석 : 로딩 UI , 게임시작화면 UI
- 노동균 : 데이터(ScriptableObject), 시스템 설계 담당
- 김강현 : 레벨 선택 씬
- 박민혁 : 게임 레벨디자인

# 씬 구성
![씬구성](https://github.com/koju2005/TeamProjectB04/assets/141552941/2e59299f-66ee-4ebe-a4e2-a057f7b4de4a)

# Scriptable Object
![캡처](https://github.com/koju2005/TeamProjectB04/assets/141552941/f193ac7e-fb2d-4119-8e85-b3dd90d3dc9d)
![캡처](https://github.com/koju2005/TeamProjectB04/assets/141552941/9264ff26-adba-46f3-ad1f-c5b7c6f134f5)

스크립트를 오브젝트화 시켜 사용하려는 객체에 필요한 기능만 부여해줄 수 있어 기능 유지보수가 용이하다.

# 사용된 리소스

   1.Art
   
      (1).StartScene
         
      (2).SelectScene
            StageObj          -itch.io/ animated pixel-art assets/elenetari
            BackGround Image  - Zuhria Alfitra
      (3).LoadingScene
      
      (4).Stage
               ㄱ.김강현
                  Bullet  - https://kr.freepik.com
                  Monster - https://www.flaticon.com
               ㄴ.김민석

               ㄷ.박민혁
                  background - pixabay.com - Darkmoon_Art
                  monster -Characters Cards pack 01 - 유니티 에셋스토어
               ㄹ.이경현
               
               ㅁ.노동균

   2.Sound

      (1).StartScene
         배경음악 - 유니티 에셋스토어 \ Unity Technologies
      (2).SelectScene
         오브젝트효과음 - https://pgtd.tistory.com
      (3).LoadingScene
      
      (4).Stage
               ㄱ.김강현
                 배경음악 - amb_bell
                 몬스터 효과음 - https://pgtd.tistory.com
               ㄴ.김민석
         
               ㄷ.박민혁
                  배경음악 -  Horror Elements - 유니티 에셋스토어
                  Amb_scary.wav
               ㄹ.이경현
               
               ㅁ.노동균
                  ActionRPGBattleMusic - 유니티 에셋스토어 \ Unity Technologies
                     05 Horns Of War Loop
                     06 The Ambush Loop
                     07 Enemy Approaches Loop
                  Horror Elements - 유니티 에셋스토어 \ Unity Technologies
                     Amb_Run_1
                     Amb_Run_2
                     SR_Piano_room
      (5) 그외
               Weapon :
                  FREE Casual Game SFX Pack - 유니티 에셋스토어 \ Unity Technologies
                     DM-CGS-39
               UI :
                  FREE Casual Game SFX Pack - 유니티 에셋스토어 \ Unity Technologies
                     DM-CGS-48


# Trouble Shooting / 기록용

   1. Merge 전 비슷한 기능을 담당하는 메서드 및 필드 다수 존재 
       -
      
       ->  모호한 업무 분담으로 인한 기능적 충돌 / 함수 참조 위치가 정리되지 않고 불특정함
      
           -> 명확한 업무 분담 후 불필요한 요소 모두 정리 후 해당 기능을 담당하는 Manager로 따로 정리해서 생성

  2. Dialog Typer 사용 시 Unity 작동 중지
     -

      -> 팀원 간 하드웨어 차이로 인한 코루틴 내 오류 

         -> 해당 기능 사용 하는 메서드 내에서 시간을 멈추는 기능을 제거하고 다른곳에 이식해줌 - > AMD , Intel CPU 차이로 인한것

 3. PlayUIManager를 통한 Time.Scale 수정 시 게임 내 시간이 정지됨
    -

      -> Win, Lose UI가 Active될 때 이벤트를 통해 관리 하고있어 그대로 씬이 전환되면 GameManager에 그대로 전달되어 Time.Scale이 돌아오지않음

        -> OnDisalbe() 통해 오브젝트가 비활성화될때는 이벤트에서 삭제할 수 있게 변경함.

4. 기능 추가 후 해당 .cs를 사용하고 있는 모든 오브젝트에서 NullPoint 발생
   -

    ->  추가된 기능이 특정 컴포넌트에만 영향을 끼치게 되있어 Awake()될 때 사용하는 오브젝트가 특정 컴포넌트를 소지하지 않고있어 발생
   
       -> 기능 추가 구현 시 기능을 명확하게 명시 후 , 위와 같은 문제 발생을 피하기위해 모든 작업에 예외처리를 하게됨
