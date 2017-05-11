<?php



  //데이터베이스 접속
  $db_host = "localhost";
  $db_user = "root";
  $db_passwd="asdf";
  $db_name = "STUDENT";

  //데이터 베이스 연결 저장
  $conn = mysqli_connect($db_host,$db_user,$db_passwd,$db_name);

  if(mysqli_connect_errno($conn)){
    echo "데이터 베이스 연결 실패: " . mysqli_connect_errno();
    echo " <br>";
  }else {
    echo "야호! 성공 ";
    echo "<br>";
  }

 mysqli_set_charset($conn, 'utf8');

  //POST 방식으로 전달된 파라미터 변수 저장
  $student_id =$_POST["student_id"];

  // sutdent ID Database 존재 유무

  $sql = "SELECT stu_num FROM UserInfo Where stu_num ='".$student_id."'";

  //저장할 학번 예시
  /*$ex_stuid = 5678;
  $sql = "SELECT stu_num FROM UserInfo Where stu_num =".$ex_stuid;*/

  echo $sql;
  echo "<br>";

  //sql 쿼리 실행
  $result = mysqli_query($conn,$sql);

 if($row = mysqli_fetch_array($result))
  {
    echo "이미 있으니 저장 안하겠습니다 ";
  }
  else {
    echo "새로운 학생입니다";
    $sql = "INSERT INTO UserInfo(stu_num) values('".$student_id."')";

    //예시저장 쿼리
    //$sql = "INSERT INTO UserInfo(stu_num) values(".$ex_stuid.")";

    echo $sql; echo "<br>";
    $result = mysqli_query($conn,$sql);
    if($result)
    {
      echo "저장완료"; echo "<br>";
    }
    else {
      echo " 저장에러"; echo "<br>";
    }
  }


 //DISCONNCT WITH MYSQL 
  mysqli_close($conn);



 ?>
