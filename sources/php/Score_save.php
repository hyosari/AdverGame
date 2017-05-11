<?php

putenv("NLS_LANG=KOREAN_KOREA.KO16MSWIN949");

//데이터베이스 접속
$db_host = "localhost";
$db_user = "root";
$db_passwd="asdf";
$db_name = "ROOM";

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
  $room_num = $_POST["room_num"];
  $game_num = $_POST["game_num"];
  $stu_num =$_POST["stu_num"];
  $answ=$_POST["answ"];

  // 행 추가 query 생성
  $sql = "INSERT INTO ".$room_num."(stu_num,game_num,answ) values('";
  $sql .= $stu_num."', ";
  $sql .= $game_num.", ";
  $sql .= $answ." )";

  echo $sql; echo "<br>";

  //query 실행
  $result = mysqli_query($conn,$sql);
  if($result){
    echo "행 추가 완료 <br>";
  }else {
    echo "행 추가 오류 <br>";
  }
 //DISCONNECT WITH MYSQL
 mysqli_close($conn);


 ?>
