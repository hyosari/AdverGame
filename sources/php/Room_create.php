<?php
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

mysql_query('set names utf8');

//POST 방식으로 전달된 파라미터 변수 저장
 $room_num= $_POST["room_num"];

  //$ex_room = "HYO_123";

  //쿼리 생성
  $sql = "CREATE TABLE ".$room_num."(";
  $sql .="\nid int not null AUTO_INCREMENT primary key ,";
  $sql .="\n room_num char(10) default '".$room_num."',";
  $sql .="\n stu_num char(10) ,";
  $sql .="\n game_num int ,";
  $sql .="\n answ int ,";
  $sql .="\n answ_time TIMESTAMP default CURRENT_TIMESTAMP";
  $sql .="\n )";

  echo $sql;
  echo "<br>";
  //쿼리 실행
  $result = mysqli_query($conn,$sql);

  if($result)
  {
    echo $room_num."으로 TABLE이 만들어 졌습니다"; echo "<br>";
  }
  else {
    echo "TABLE 생성 에러"; echo "<br>";
  }

  // 접속 종료
   mysqli_close($conn);


 ?>
